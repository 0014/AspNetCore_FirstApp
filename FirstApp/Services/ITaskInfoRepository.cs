using FirstApp.Entities;
using System.Collections.Generic;

namespace FirstApp.Services
{
    public interface ITaskInfoRepository
    {
        bool TaskExists(int taskId);

        IEnumerable<Task> GetTasks();

        Task GetTask(int taskId, bool includeRelatedProjects);

        IEnumerable<RelatedProject> GetRelatedProjects(int taskId);

        RelatedProject GetRelatedProject(int taskId, int relatedProjectId);
    }
}
