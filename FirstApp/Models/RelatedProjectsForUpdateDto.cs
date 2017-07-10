using System.ComponentModel.DataAnnotations;

namespace FirstApp.Models
{
    public class RelatedProjectsForUpdateDto
    {
        [Required(ErrorMessage = "You should provide a new value.")]
        [MaxLength(50)]
        public string Name { get; set; }

        [MaxLength(200)]
        public string Description { get; set; }
    }
}
