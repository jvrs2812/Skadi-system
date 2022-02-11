using backend.model;

namespace backend.response
{
    public class UserLoginResponse
    {
        public string? id { get; set; }
        public string? name { get; set; }

        public string? email { get; set; }

        public string? cnpj { get; set; }

        public string? token { get; set; }
    }
}