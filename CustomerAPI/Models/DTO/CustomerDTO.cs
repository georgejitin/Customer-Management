using System.ComponentModel.DataAnnotations;

namespace CustomerAPI.Models.DTO
{
    public class CustomerDTO
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
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}
