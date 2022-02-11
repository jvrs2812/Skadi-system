using System.ComponentModel.DataAnnotations;

public class TokenRequest
{
    [Required(ErrorMessage = "Refresh Token não informado")]
    public string RefreshToken { get; set; }
}