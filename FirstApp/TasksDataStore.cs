using FirstApp.Models;
using System.Collections.Generic;

namespace FirstApp
{
    public class TasksDataStore
    {
        public static TasksDataStore Current { get; } = new TasksDataStore();

        public List<TaskDto> Tasks { get; set; }

        public TasksDataStore()
        {
            Tasks = new List<TaskDto>
            {
                new TaskDto
                {
                    Id = 1,
                    Name = "Esystem Task",
                    Description = "Runs in email user to send emails",
                    RelatedProjects = new List<RelatedProjectsDto>
                    {
                        new RelatedProjectsDto
                        {
                            Id = 1,
                            Name = "Live System",
                            Description = "This is the project that all the buttons are added"
                        },

                        new RelatedProjectsDto
                        {
                            Id = 2,
                            Name = "Terminal Server",
                            Description = "This is our old server that had email task in it"
                        }
                    }
                },

                new TaskDto
                {
                    Id = 2,
                    Name = "Sync Task",
                    Description = "Sync Access database to SQL database",
                    RelatedProjects = new List<RelatedProjectsDto>
                    {
                        new RelatedProjectsDto
                        {
                            Id = 1,
                            Name = "Developer App",
                            Description = "This is the app runs on developer pc"
                        },

                        new RelatedProjectsDto
                        {
                            Id = 2,
                            Name = "RDP System",
                            Description = "App that runs on developer RDP system"
                        },

                        new RelatedProjectsDto
                        {
                            Id = 3,
                            Name = "Managers Project",
                            Description = "The is the project that is special for IT manager"
                        }
                    }
                },

                new TaskDto
                {
                    Id = 3,
                    Name = "Amazon Task",
                    Description = "Generates reports for our Amazon inventory",
                    RelatedProjects = new List<RelatedProjectsDto>
                    {
                        new RelatedProjectsDto
                        {
                            Id = 1,
                            Name = "Amazon Server",
                            Description = "This is the only server that runs the Amazon application"
                        }
                    }
                }
            };
        }
    }
}
