using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using backend.db;
using backend.exception;
using backend.model;
using backend.refreshtoken;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace backend.settings
{
    public class TokenService
    {
        private SKADIDBContext _context;
        private TokenValidationParameters _tokenValidationParameters;
        private IOptionsMonitor<Settings> optionsMonitor;
        public TokenService(SKADIDBContext context, TokenValidationParameters tokenValidationParameters)
        {
            this._context = context;
            this._tokenValidationParameters = tokenValidationParameters;
        }
        public AuthResult GenerateToken(User user)
        {
            var date = DateTime.UtcNow.AddHours(2);
            var tokenHandler = new JwtSecurityTokenHandler();

            var key = Encoding.ASCII.GetBytes(Settings.Secret);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]{
                    new Claim("Id", user.id),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Sub, user.email),
                    new Claim(JwtRegisteredClaimNames.Email, user.email)
                }),
                Expires = date,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var jwtToken = tokenHandler.WriteToken(token);


            var refreshToken = new RefreshToken()
            {
                JwtId = token.Id,
                IsUsed = false,
                UserId = user.id,
                AddedDate = DateTime.UtcNow,
                ExpiryDate = DateTime.UtcNow.AddYears(1),
                IsRevoked = false,
                Token = RandomString(25) + Guid.NewGuid()
            };

            _context.RefreshTokens.Add(refreshToken);
            _context.SaveChanges();

            return new AuthResult()
            {
                Token = jwtToken,
                RefreshToken = refreshToken.Token,
                ExpiredDate = date,
            };
        }

        private static string RandomString(int length)
        {
            var random = new Random();
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
            .Select(s => s[random.Next(s.Length)]).ToArray());
        }
        public AuthResult VerifyToken(TokenRequest tokenRequest)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();

            try
            {
                // This validation function will make sure that the token meets the validation parameters
                // and its an actual jwt token not just a random string
                var principal = jwtTokenHandler.ValidateToken(tokenRequest.RefreshToken, _tokenValidationParameters, out var validatedToken);

                // Now we need to check if the token has a valid security algorithm
                if (validatedToken is JwtSecurityToken jwtSecurityToken)
                {
                    var result = jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase);

                    if (result == false)
                    {
                        return null;
                    }
                }

                // Will get the time stamp in unix time
                var utcExpiryDate = long.Parse(principal.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Exp).Value);

                // we convert the expiry date from seconds to the date
                var expDate = UnixTimeStampToDateTime(utcExpiryDate);

                if (expDate > DateTime.UtcNow)
                {
                    throw new ValidateException("token não expirado");
                }

                // Check the token we got if its saved in the db
                var storedRefreshToken = _context.RefreshTokens.FirstOrDefault(x => x.Token == tokenRequest.RefreshToken);

                if (storedRefreshToken == null)
                {
                    throw new ValidateException("token não existe");
                }

                // Check the date of the saved token if it has expired
                if (DateTime.UtcNow > storedRefreshToken.ExpiryDate)
                {
                    throw new ValidateException("token expirado, relogue");
                }

                // check if the refresh token has been used
                if (storedRefreshToken.IsUsed)
                {
                    throw new ValidateException("token já utilizado");
                }

                // Check if the token is revoked
                if (storedRefreshToken.IsRevoked)
                {
                    throw new ValidateException("token já revogado");
                }

                // we are getting here the jwt token id
                var jti = principal.Claims.SingleOrDefault(x => x.Type == JwtRegisteredClaimNames.Jti).Value;

                // check the id that the recieved token has against the id saved in the db
                if (storedRefreshToken.JwtId != jti)
                {
                    throw new ValidateException("o token não corresponde ao token salvo");
                }

                storedRefreshToken.IsUsed = true;
                _context.RefreshTokens.Update(storedRefreshToken);
                _context.SaveChanges();

                var dbUser = _context.User.FirstOrDefault(x => x.id == storedRefreshToken.UserId);
                return GenerateToken(dbUser);
            }
            catch (ValidateException err)
            {
                throw new ValidateException(err.Message);
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
                return null;
            }
        }

        private DateTime UnixTimeStampToDateTime(double unixTimeStamp)
        {
            // Unix timestamp is seconds past epoch
            System.DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
            dtDateTime = dtDateTime.AddSeconds(unixTimeStamp).ToUniversalTime();
            return dtDateTime;
        }
    }
}
