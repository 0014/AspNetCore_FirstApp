using System.Collections.Generic;

namespace FirstApp.Models
{
    public class TaskDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int NumberOfRelatedProjects => RelatedProjects.Count;

        public List<RelatedProjectsDto> RelatedProjects { get; set; } = new List<RelatedProjectsDto>( );
    }
}
