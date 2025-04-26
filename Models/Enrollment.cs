using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace university_management.Models
{
    public class Enrollment
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Please select a student")]
        public int StudentId { get; set; }
        
        [ValidateNever]
        public Student? Student { get; set; }

        [Required(ErrorMessage = "Please select a course")]
        public int CourseId { get; set; }
        
        [ValidateNever]
        public Course? Course { get; set; }

        public DateTime EnrollmentDate { get; set; } = DateTime.UtcNow;

        [Required(ErrorMessage = "Please select a A, B, C, D, F grade")]
        [Display(Name = "Grade")]
        public Grade? Grade { get; set; }
    }

    public enum Grade
    {
        A, B, C, D, F
    }
}