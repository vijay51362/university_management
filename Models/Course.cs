using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace university_management.Models
{
    public class Course
    {
        public int Id { get; set; }
        
        [Required(ErrorMessage = "Course title is required")]
        public string Title { get; set; } = string.Empty;
        
        [Range(1, 5, ErrorMessage = "Credits must be between 1 and 5")]
        public int Credits { get; set; } = 3;
        
        public ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();
    }
}