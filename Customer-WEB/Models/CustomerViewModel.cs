using System.ComponentModel.DataAnnotations;

namespace Customer_WEB.Models
{
    public class CustomerViewModel
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(30)]
        public string FirstName { get; set; }
        [Required]
        [MaxLength(30)]
        public string LastName { get; set; }

        public string Password { get; set; }
        public string LoginUser { get; set; }
        public string Email { get; set; }
        public string Phonenumber { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime UpdatedDate { get; set; } = DateTime.Now;


    }
}