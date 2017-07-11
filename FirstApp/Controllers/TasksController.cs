using FirstApp.Models;
using FirstApp.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace FirstApp.Controllers
{
    [Route("api/tasks")]
    public class TasksController : Controller
    {
        private ITaskInfoRepository _taskInfoRepository;

        public TasksController(ITaskInfoRepository taskInfoRepository)
        {
            _taskInfoRepository = taskInfoRepository;
        }

        [HttpGet]
        public IActionResult GetTasks()
        {
            //return Ok(TasksDataStore.Current.Tasks);
            var taskEntities = _taskInfoRepository.GetTasks();

            var results = new List<TaskWithoutRelatedProjectsDto>();

            foreach (var taskEntity in taskEntities)
            {
                results.Add(new TaskWithoutRelatedProjectsDto
                {
                    Id = taskEntity.Id,
                    Name = taskEntity.Name,
                    Description = taskEntity.Description
                });
            }

            return Ok(results);
        }

        [HttpGet("{id}")]
        public IActionResult GetTask(int id, bool includeRelatedProjects = false)
        {
            var task = _taskInfoRepository.GetTask(id, includeRelatedProjects);

            if (task == null)
            {
                return NotFound();
            }

            if (includeRelatedProjects)
            {
                var taskResult = new TaskDto
                {
                    Id = task.Id,
                    Name = task.Name,
                    Description = task.Description
                };

                foreach (var relatedProject in task.RelatedProjects)
                {
                    taskResult.RelatedProjects.Add(
                        new RelatedProjectsDto
                        {
                            Id = relatedProject.Id,
                            Name = relatedProject.Name,
                            Description = relatedProject.Description
                        });
                }

                return Ok(taskResult);
            }

            return Ok(new TaskWithoutRelatedProjectsDto
            {
                Id = task.Id,
                Name = task.Name,
                Description = task.Description
            });

            //var task = TasksDataStore.Current.Tasks.FirstOrDefault(t => t.Id == id);

            //if (task == null)
            //{
            //    return NotFound();
            //}

            //return Ok(task);
        }
    }
}
