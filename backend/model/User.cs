using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
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
        public bool emailConfirmation { get; set; }
        public List<Enterprise> enterprises { get; set; }

        public List<Claim> GetClaims()
        {
            List<Claim> Claim = new List<Claim>();


            return Claim;
        }
    }
}