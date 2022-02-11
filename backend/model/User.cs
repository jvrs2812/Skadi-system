using System.ComponentModel.DataAnnotations;
using backend.requestmodels;

namespace backend.model
{
    public class User
    {

        [Required]
        public string id { get; set; }
        [Required]
        public string name { get; set; }
        public DateTime CreatedAt { get; set; }
        [Required]
        public string email { get; set; }
        [Required]
        public string password { get; set; }
        public string Role { get; set; }
        public bool emailConfirmation { get; set; }
    }
}