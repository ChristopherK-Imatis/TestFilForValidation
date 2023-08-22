using System.ComponentModel.DataAnnotations;

namespace TestFilForValidation.Data
{
    public class Employee
    {
        [Required]
        public string Firstname { get; set; }

        [Required]
        public string Lastname { get; set; }

        public string Fullname { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public DateTime? Birthday { get; set; }

        [Required]
        public int PhoneNumber { get; set; }

        [Required]
        public Sex Sex { get; set; }

        [Required]
        public int EmployeeID { get; set; }


    }
    public enum Sex
    {
        NotSpesified,
        Male,
        Female
    }
}
