using System.ComponentModel.DataAnnotations;

namespace backend.requestmodels
{
    public class UserRequest
    {
        [Required(ErrorMessage = "O nome é obrigatório")]
        [StringLength(100, MinimumLength = 5, ErrorMessage = "O nome deve ter entre 5 e 100 caracteres")]
        public string name { get; set; }

        [Required(ErrorMessage = "O Email é obrigatório")]
        [EmailAddress(ErrorMessage = "O email informado não é valido")]
        public string email { get; set; }

        [Required(ErrorMessage = "O Password é obrigatório")]
        [StringLength(40, MinimumLength = 5, ErrorMessage = "A senha deve ter entre 5 e 40 caracteres")]
        public string password { get; set; }
    }
}