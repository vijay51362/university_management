using System;
using System.ComponentModel.DataAnnotations;

namespace university_management.Models
{
    public class Student
    {
        public int Id { get; set; }
        
        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;
        
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
        
        [DataType(DataType.Date)]
        public DateTime EnrollmentDate { get; set; } = DateTime.UtcNow; // UTC by default
    }
}