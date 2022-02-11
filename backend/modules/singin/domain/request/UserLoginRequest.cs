using System.ComponentModel.DataAnnotations;

namespace backend.requestmodels
{
    public class UserLoginRequest
    {
       [Required(ErrorMessage = "O e-mail é obrigatório")]
        public string email { get; set; }

       [Required(ErrorMessage = "A senha é obrigatória")]
        public string password { get; set; }

    }
}