using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Master.Entity.Domain;
using Master.Entity.Infra;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Api.Master.Controllers
{
    [Authorize]
    [ApiController]
    public partial class MasterController : ControllerBase
    {
        public LocalNetwork network { get; set; }        

        public MasterController(IOptions<LocalNetwork> _network)
        {
            if (_network != null)
                this.network = _network.Value;            
        }

        [NonAction]
        protected DtoUser GetCurrentAuthenticatedUser()
        {
            #region - code -

            var handler = new JwtSecurityTokenHandler();
                        
            var authHeader = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

            if (string.IsNullOrEmpty(authHeader))
                return null;

            var jsonToken = handler.ReadToken(authHeader);
            var tokenS = handler.ReadToken(authHeader) as JwtSecurityToken;
            var claims = tokenS.Claims;

            return new DtoUser
            {
                id = claims.FirstOrDefault(claim => claim.Type == "_id")?.Value,
                name = claims.FirstOrDefault(claim => claim.Type == "_name")?.Value,
            };

            #endregion
        }
        
        [NonAction]
        public string ComposeTokenForSession(DtoUser usr)
        {
            #region - code - 

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(LocalNetwork.Secret);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim("_id", usr.id.ToString()),
                    new Claim("_name", usr.name),
                }),
                Expires = DateTime.UtcNow.AddHours(24),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                                                                SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);

            #endregion
        }

        [NonAction]
        public long I(string myNumber)
        {
            return Convert.ToInt64(myNumber);
        }

        [NonAction]
        public DateTime D(string myDate)
        {
            return new DateTime((int)I(myDate.Substring(6)),
                                (int)I(myDate.Substring(3, 2)),
                                (int)I(myDate.Substring(0, 2)));
        }

        [NonAction]
        public string GetCacheFile(string tag)
        {
            tag += ".txt";

            if (System.IO.File.Exists(tag))
                return System.IO.File.ReadAllText(tag);

            return null;            
        }

        [NonAction]
        public void SaveCacheFile(string tag, string json)
        {
            tag += ".txt";

            if (System.IO.File.Exists(tag))
                System.IO.File.Delete(tag);

            System.IO.File.WriteAllText(tag, json);
        }

        [NonAction]
        public void DeleteCacheFile(string tag)
        {
            tag += ".txt";
            if (System.IO.File.Exists(tag))
                System.IO.File.Delete(tag);
        }

        /*
        [NonAction]
        public void SendEmail(string _email, string subject, string texto, LocalNetwork network, List<string> attachs = null)
        {
            #region - code - 
            
            MailMessage email = new MailMessage
            {
                From = new MailAddress("<" + network._emailSmtp + ">")
            };
            email.To.Add(_email);
            email.Priority = MailPriority.Normal;
            email.IsBodyHtml = false;
            email.Subject = subject;
            email.Body = texto;

            email.SubjectEncoding = Encoding.GetEncoding("ISO-8859-1");
            email.BodyEncoding = Encoding.GetEncoding("ISO-8859-1");
            SmtpClient emailSmtp = new SmtpClient
            {
                Credentials = new System.Net.NetworkCredential(network._emailSmtp, network._passwordSmtp),//e-mail e senha do remetente
                Host = network._hostSmtp, // "smtp." + "nanojs.com.br",
                Port = (int)I(network._smtpPort) // 587
            };

            try
            {
                emailSmtp.Send(email);
            }
            catch (Exception erro)
            {
                throw new Exception("erro: " + erro.Message);
            }
            #endregion
        }
        */
    }
}
