using backend.model;

namespace backend.response
{
    public class UserRegisterResponse
    {
        public string Id { get; set; }


        public string Name { get; set; }

        public string Email { get; set; }

        public string Token { get; set; }

        public string CNPJ { get; set; }


    }
}