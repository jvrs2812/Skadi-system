namespace backend.UseCases.model
{
    public class EmailSend
    {
        public string email { get; set; }
        public string servidor { get; set; }
        public string password { get; set; }

        public int port { get; set; }

        public EmailSend(WebApplicationBuilder builder)
        {
            email = builder.Configuration["EmailSettings:UsernameEmail"].ToString();
            servidor = builder.Configuration["EmailSettings:PrimaryDomain"].ToString();
            port = int.Parse(builder.Configuration["EmailSettings:PrimaryPort"]);
            password = builder.Configuration["EmailSettings:UsernamePassword"];
        }

    }
}