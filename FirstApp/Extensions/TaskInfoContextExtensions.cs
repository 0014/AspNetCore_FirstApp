using FirstApp.Entities;
using System.Collections.Generic;
using System.Linq;

namespace FirstApp.Extensions
{
    public static class TaskInfoContextExtensions
    {
        public static void EnsureSeedDataForContext(this TaskInfoContext context)
        {
            if (context.Tasks.Any())
            {
                return;
            }

            var tasks = new List<Task>()
            {
                new Task
                {
                    Name = "Esystem Task",
                    Description = "Runs in email user to send emails",
                    RelatedProjects = new List<RelatedProject>
                    {
                        new RelatedProject
                        {
                            Name = "Live System",
                            Description = "This is the project that all the buttons are added"
                        },

                        new RelatedProject
                        {
                            Name = "Terminal Server",
                            Description = "This is our old server that had email task in it"
                        }
                    }
                },

                new Task
                {
                    Name = "Sync Task",
                    Description = "Sync Access database to SQL database",
                    RelatedProjects = new List<RelatedProject>
                    {
                        new RelatedProject
                        {
                            Name = "Developer App",
                            Description = "This is the app runs on developer pc"
                        },

                        new RelatedProject
                        {
                            Name = "RDP System",
                            Description = "App that runs on developer RDP system"
                        },

                        new RelatedProject
                        {
                            Name = "Managers Project",
                            Description = "The is the project that is special for IT manager"
                        }
                    }
                },

                new Task
                {
                    Name = "Amazon Task",
                    Description = "Generates reports for our Amazon inventory",
                    RelatedProjects = new List<RelatedProject>
                    {
                        new RelatedProject
                        {
                            Name = "Amazon Server",
                            Description = "This is the only server that runs the Amazon application"
                        }
                    }
                }
            };

            context.Tasks.AddRange(tasks);
            context.SaveChanges();
        }
    }
}
