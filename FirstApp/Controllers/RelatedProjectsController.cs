using FirstApp.Models;
using FirstApp.Services;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;

namespace FirstApp.Controllers
{
    [Route("api/tasks")]
    public class RelatedProjectsController : Controller
    {
        private ILogger<RelatedProjectsController> _logger;
        private IMailService _mailService;

        public RelatedProjectsController(ILogger<RelatedProjectsController> logger,
            IMailService mailService)
        {
            _logger = logger;
            _mailService = mailService;
        }

        [HttpGet("{taskId}/relatedprojects")]
        public IActionResult GetRelatedProjects(int taskId)
        {
            try
            {
                var task = TasksDataStore.Current.Tasks.FirstOrDefault(t => t.Id == taskId);

                if (task == null)
                {
                    _logger.LogInformation($"Task with id {taskId} wasnt found when accessing the related projects.");
                    return NotFound();
                }

                return Ok(task.RelatedProjects);
            }
            catch (Exception e)
            {
                _logger.LogCritical($"Exception while getting points of interest for task with id: {taskId}.", e);
                return StatusCode(500, "A problem happened while handling your request");
            }
        }

        [HttpGet("{taskId}/relatedprojects/{projectId}", Name = "GetRelatedProject")]
        public IActionResult GetRelatedProject(int taskId, int projectId)
        {
            var task = TasksDataStore.Current.Tasks.FirstOrDefault(t => t.Id == taskId);

            if (task == null)
            {
                return NotFound();
            }

            var relatedProject = task.RelatedProjects.FirstOrDefault(r => r.Id == projectId);

            if (relatedProject == null)
            {
                return NotFound();
            }

            return Ok(relatedProject);
        }

        [HttpPost("{taskId}/relatedprojects")]
        public IActionResult CreateRelatedProject(int taskId, 
            [FromBody]RelatedProjectsForCreateDto relatedProject)
        {
            if (relatedProject == null)
            {
                return BadRequest();
            }

            if (relatedProject.Name == relatedProject.Description)
            {
                ModelState.AddModelError("Description", "The provided description should be different from the name");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var task = TasksDataStore.Current.Tasks.FirstOrDefault(t => t.Id == taskId);

            if (task == null)
            {
                return NotFound();
            }

            var maxRelatedPorjectId = TasksDataStore.Current.Tasks.SelectMany(t => t.RelatedProjects).Max(r => r.Id);

            var finalRelatedProject = new RelatedProjectsDto
            {
                Id = ++maxRelatedPorjectId,
                Name = relatedProject.Name,
                Description = relatedProject.Description
            };

            task.RelatedProjects.Add(finalRelatedProject);

            return CreatedAtRoute("GetRelatedProject",
                new {taskId, projectId = finalRelatedProject.Id }, finalRelatedProject);
        }


        [HttpPut("{taskId}/relatedprojects/{projectId}")]
        public IActionResult UpdateRelatedProject(int taskId, int projectId,
            [FromBody]RelatedProjectsForCreateDto updatedRelatedProject)
        {
            if (updatedRelatedProject == null)
            {
                return BadRequest();
            }

            if (updatedRelatedProject.Name == updatedRelatedProject.Description)
            {
                ModelState.AddModelError("Description", "The provided description should be different from the name");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var task = TasksDataStore.Current.Tasks.FirstOrDefault(t => t.Id == taskId);

            if (task == null)
            {
                return NotFound();
            }

            var relatedProject = task.RelatedProjects.FirstOrDefault(r => r.Id == projectId);

            if (relatedProject == null)
            {
                return NotFound();
            }

            relatedProject.Name = updatedRelatedProject.Name;
            relatedProject.Description = updatedRelatedProject.Description;

            return NoContent();
        }

        [HttpPatch("{taskId}/relatedprojects/{projectId}")]
        public IActionResult PartiallyUpdatePointOfInterest(int taskId, int projectId,
            [FromBody] JsonPatchDocument<RelatedProjectsForUpdateDto> patchDoc)
        {
            if (patchDoc == null)
            {
                return BadRequest();
            }

            var task = TasksDataStore.Current.Tasks.FirstOrDefault(t => t.Id == taskId);

            if (task == null)
            {
                return NotFound();
            }

            var relatedProject = task.RelatedProjects.FirstOrDefault(r => r.Id == projectId);

            if (relatedProject == null)
            {
                return NotFound();
            }

            var relatedProjectToPatch = new RelatedProjectsForUpdateDto
            {
                Name = relatedProject.Name,
                Description = relatedProject.Description
            };

            patchDoc.ApplyTo(relatedProjectToPatch, ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (relatedProjectToPatch.Description == relatedProjectToPatch.Name)
            {
                ModelState.AddModelError("Description", "The provided description should be different from the name");
            }

            TryValidateModel(relatedProjectToPatch);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            relatedProject.Name = relatedProjectToPatch.Name;
            relatedProject.Description = relatedProjectToPatch.Description;

            return NoContent();
        }

        [HttpDelete("{taskId}/relatedProjects/{projectId}")]
        public IActionResult DeleteRelatedProject(int taskId, int projectId)
        {
            var task = TasksDataStore.Current.Tasks.FirstOrDefault(t => t.Id == taskId);

            if (task == null)
            {
                return NotFound();
            }

            var relatedProject = task.RelatedProjects.FirstOrDefault(r => r.Id == projectId);

            if (relatedProject == null)
            {
                return NotFound();
            }

            task.RelatedProjects.Remove(relatedProject);

            _mailService.Send("Related Project Deleted",$"Related project {relatedProject.Name} with id {relatedProject.Id} was deleted.");

            return NoContent();
        }

    }
}

