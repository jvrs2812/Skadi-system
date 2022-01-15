using System.ComponentModel.DataAnnotations;
using backend.requestmodels;

namespace backend.model
{
    public class User
    {

        [Required]
        public string Id { get; set; }
        [Required]
        public string Name { get; set; }
        public DateTime CreatedAt { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }

        [StringLength(14)]
        public string CNPJ { get; set; }

        public string Role { get; set; }
        public UserRequest UserModelRequest { get; }
    }
}