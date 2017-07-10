using Microsoft.EntityFrameworkCore;

namespace FirstApp.Entities
{
    public class TaskInfoContext : DbContext
    {
        public TaskInfoContext(DbContextOptions<TaskInfoContext> options): base(options)
        {
            Database.Migrate();
        }

        public DbSet<Task> Tasks { get; set; }

        public DbSet<RelatedProject> RelatedProjects { get; set; }
    }
}
