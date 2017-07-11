using FirstApp.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace FirstApp.Services
{
    public class TaskInfoRepository : ITaskInfoRepository
    {
        private TaskInfoContext _context;

        public TaskInfoRepository(TaskInfoContext context)
        {
            _context = context;
        }

        public IEnumerable<Task> GetTasks()
        {
            return _context.Tasks.OrderBy(o => o.Name).ToList();
        }

        public Task GetTask(int taskId, bool includeRelatedProjects)
        {
            if (includeRelatedProjects)
            {
                return _context.Tasks.Include(t => t.RelatedProjects).FirstOrDefault(t => t.Id == taskId);
            }

            return _context.Tasks.FirstOrDefault(t => t.Id == taskId);
        }

        public IEnumerable<RelatedProject> GetRelatedProjects(int taskId)
        {
            return _context.RelatedProjects.Where(r => r.TaskId == taskId).ToList();
        }

        public RelatedProject GetRelatedProject(int taskId, int relatedProjectId)
        {
            return _context.RelatedProjects.FirstOrDefault(t => t.Id == taskId && t.Id == relatedProjectId);
            
        }
    }
}
