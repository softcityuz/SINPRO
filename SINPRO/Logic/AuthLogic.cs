using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using SINPRO.Entity;
using SINPRO.Entity.DataModels;
using SINPRO.Helpers;
using SINPRO.InputTypes;
using SINPRO.Middleware;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SINPRO.Logic
{
	public class AuthLogic : IAuthLogic
	{
		private readonly SINContext _authContext;
		private readonly TokenSettings _tokenSettings;
		//private readonly RequestDelegate _next;
		public AuthLogic(SINContext authContext, IOptions<TokenSettings> tokenSettings
			//,RequestDelegate next
			)
		{
			_authContext = authContext;
			_tokenSettings = tokenSettings.Value;
			//_next = next;
		}
		public string accessToken { get; set; }
		private string ResigstrationValidations(RegisterInputType registerInput)
		{
			if (string.IsNullOrEmpty(registerInput.Email))
			{
				return "Eamil can't be empty";
			}

			if (string.IsNullOrEmpty(registerInput.Password)
				|| string.IsNullOrEmpty(registerInput.ConfirmPassword))
			{
				return "Password Or ConfirmPassword Can't be empty";
			}

			if (registerInput.Password != registerInput.ConfirmPassword)
			{
				return "Invalid confirm password";
			}

			string loginRules = @"[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?";
			if (!Regex.IsMatch(registerInput.Email, loginRules))
			{
				return "Not a valid email";
			}
			var login = _authContext.mUser.Where(p => p.email == registerInput.Email);
			if (login.Count() != 0)
			{
				return "A user is registered with such a email!!";
			}
			// atleast one lower case letter
			// atleast one upper case letter
			// atleast one special character
			// atleast one number
			// atleast 8 character length
			//string passwordRules = @"^.*(?=.{8,})(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[!*@#$%^&+=]).*$";
			string passwordRules = @"^.*(?=.{8,})";
			if (!Regex.IsMatch(registerInput.Password, passwordRules))
			{
				return "Not a valid password";
			}

			return string.Empty;
		}

		public string Register(RegisterInputType registerInput)
		{
			string errorMessage = ResigstrationValidations(registerInput);
			if (!string.IsNullOrEmpty(errorMessage))
			{
				return errorMessage;
			}

			var newUser = new mUser
			{
				email = registerInput.Email,
				password = PasswordHash(registerInput.ConfirmPassword),
				inserted = DateTime.Now,
				updated = DateTime.Now,
				fName=registerInput.Name,
				sName=registerInput.Lastname,
				phone=registerInput.Phone,
				statusDate=registerInput.statusDate,
				status = 1,
				roleId = 2,
			};
			//var counterparty = new mainCounterparty()
			//{
			//	Lastname = registerInput.Lastname,
			//	Name = registerInput.Name,
			//	Middlename = registerInput.Middlename,
			//	Address = registerInput.Address,
			//	Email = registerInput.Email,
			//	Phone = registerInput.Phone,
			//	Inserted = DateTime.Now,
			//	Updated = DateTime.Now,
			//	Deleted = 0,
			//	Status = 1
			//};
			var user = _authContext.mUser.Where(p => p.email.Contains(registerInput.Email)).Count();
			if (user > 0)
			{
				throw new Exception("A_USER_IS_REGISTERED_WITH_SUCH_A_EMAIL");
			}
			//_authContext.mainCounterparty.Add(counterparty);
			//_authContext.SaveChanges();
			//var cId = _authContext.mainCounterparty.OrderByDescending(_ => _.Id).Take(1).Single().Id;
			//newUser.CounterpartyId = cId;
			_authContext.mUser.Add(newUser);
			_authContext.SaveChanges();

			// default role on registration
			//var newUserRoles = new mRoles
			//{
			//    Name = "admin",
			//    UserId = newUser.UserId
			//};

			//_authContext.UserRoles.Add(newUserRoles);
			//_authContext.SaveChanges();

			return "Registration success";
		}

		private string PasswordHash(string password)
		{
			byte[] salt;
			new RNGCryptoServiceProvider().GetBytes(salt = new byte[16]);

			var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 1000);
			byte[] hash = pbkdf2.GetBytes(20);

			byte[] hashBytes = new byte[36];
			Array.Copy(salt, 0, hashBytes, 0, 16);
			Array.Copy(hash, 0, hashBytes, 16, 20);

			return Convert.ToBase64String(hashBytes);
		}

		private bool ValidatePasswordHash(string password, string dbPassword)
		{
			byte[] hashBytes = Convert.FromBase64String(dbPassword);

			byte[] salt = new byte[16];
			Array.Copy(hashBytes, 0, salt, 0, 16);

			var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 1000);
			byte[] hash = pbkdf2.GetBytes(20);

			for (int i = 0; i < 20; i++)
			{
				if (hashBytes[i + 16] != hash[i])
				{
					return false;
				}
			}

			return true;
		}
		//, List<mainUserRoles> roles
		private string GetJWTAuthKey(mUser user)
		{
			var securtityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_tokenSettings.Key));

			var credentials = new SigningCredentials(securtityKey, SecurityAlgorithms.HmacSha256);

			var claims = new List<Claim>();
			string ResourceName = _authContext.mRole.FirstOrDefault(_ => _.id == user.roleId).resourceName;
			claims.Add(new Claim(ClaimTypes.NameIdentifier, user.id.ToString()));
			claims.Add(new Claim(ClaimTypes.Role, ResourceName));
			claims.Add(new Claim("UserId", user.id.ToString()));
			claims.Add(new Claim("RoleId", user.roleId.ToString()));
			//claims.Add(new Claim("UserName", user.UserName));
			claims.Add(new Claim("Email", user.email));
			//claims.Add(new Claim("ManagerId", user.ManagerId.ToString()));
			//claims.Add(new Claim("CounterpartyId", user.CounterpartyId.ToString()));
			//if ((roles?.Count ?? 0) > 0)
			//{
			//	foreach (var role in roles)
			//	{
			//		claims.Add(new Claim(ClaimTypes.Role, role.ResourceName));
			//	}
			//}

			var jwtSecurityToken = new JwtSecurityToken(
				issuer: _tokenSettings.Issuer,
				audience: _tokenSettings.Audience,
				expires: DateTime.Now.AddMinutes(1800),
				signingCredentials: credentials,
				claims: claims
			);

			return new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
		}

		public LoginViewModel Login(LoginInputType loginInput)
		{
			var result = new LoginViewModel { Message = "Success" };
			if (string.IsNullOrEmpty(loginInput.Email)
			|| string.IsNullOrEmpty(loginInput.Password))
			{

				result.Message = "Invalid Credentials";
				return result;

			}

			var user = _authContext.mUser.ToList().Where(_ => _.email == loginInput.Email && ValidatePasswordHash(loginInput.Password, _.password)).FirstOrDefault();
			if (user == null)
			{
				result.Message = "Invalid Credentials";
				return result;
			}
			//if (user.Deleted == 1)
			//{
			//	result.Message = "User deleted";
			//	return result;
			//}

			if (!ValidatePasswordHash(loginInput.Password, user.password))
			{
				result.Message = "Invalid Credentials";
				return result;
			}

			//var roles = _authContext.UserRoles.Where(_ => _.UserId == user.UserId).ToList();
			var roles = user.roleId;
			//var permission = _authContext.mainRoles.Where(_ => _.Id == roles).ToList();
			result.AccessToken = GetJWTAuthKey(user);
			result.RefreshToken = GenerateRefreshToken();
			user.refreshToken = result.RefreshToken;
			user.refershTokenExpiration = DateTime.Now.AddDays(7);
			_authContext.SaveChanges();
			user.password = "***";
			result.User = user;
			//_session.SetString("token", userModel.token);

			return result;
		}
		private ClaimsPrincipal GetClaimsFromExpiredToken(string accessToken)
		{
			if (accessToken == null)
				return null;
			var tokenValidationParameter = new TokenValidationParameters
			{
				ValidIssuer = _tokenSettings.Issuer,
				ValidateIssuer = true,
				ValidAudience = _tokenSettings.Audience,
				ValidateAudience = true,
				IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_tokenSettings.Key)),
				ValidateLifetime = false // ignore expiration
			};

			var jwtHandler = new JwtSecurityTokenHandler();
			var principal = jwtHandler.ValidateToken(accessToken, tokenValidationParameter, out SecurityToken securityToken);

			var jwtScurityToken = securityToken as JwtSecurityToken;
			if (jwtScurityToken == null)
			{
				return null;
			}
			return principal;
		}
		HttpContext _context;
		public UserViewModel GetUserbyToken(string accessToken = null)
		{
			//accessToken = AuthenticationMiddleware.accessToken;
			//_context = AuthenticationMiddleware.GetHttpContext;
			if (accessToken == null)
				return null;
			var user = new UserViewModel();
			var principal = GetClaimsFromExpiredToken(accessToken);
			if (principal == null)
			{
				return null;
			}
			user.Email = principal.Claims.Where(_ => _.Type == "Email").Select(_ => _.Value).FirstOrDefault();
			string val = principal.Claims.Where(_ => _.Type == "UserId").Select(_ => _.Value).FirstOrDefault();
			val = val == "" || val == null ? "0" : val;
			user.UserId = int.Parse(val);
			val = principal.Claims.Where(_ => _.Type == "RoleId").Select(_ => _.Value).FirstOrDefault();
			val = val == "" || val == null ? "0" : val;
			user.RoleId = int.Parse(val);
			//user.UserName = principal.Claims.Where(_ => _.Type == "UserName").Select(_ => _.Value).FirstOrDefault();
			val = principal.Claims.Where(_ => _.Type == "ManagerId").Select(_ => _.Value).FirstOrDefault();
			val = val == "" || val == null ? "0" : val;
			user.ManagerId = null;
			if (val != "0")
				user.ManagerId = int.Parse(val);
			val = principal.Claims.Where(_ => _.Type == "CounterpartyId").Select(_ => _.Value).FirstOrDefault();
			val = val == "" || val == null ? "0" : val;
			user.CounterpartyId = null;
			if (val != "0")
				user.CounterpartyId = int.Parse(val);
			try
			{
				//_context.Session.SetString("SessionUser", JsonConvert.SerializeObject(user));

			}
			catch (Exception)
			{

			}
			return user;
		}
		private string GenerateRefreshToken()
		{
			var randomNumber = new byte[32];
			using (var generator = RandomNumberGenerator.Create())
			{
				generator.GetBytes(randomNumber);
				return Convert.ToBase64String(randomNumber);
			}
		}
		public LoginViewModel RenewAccessToken(RenewTokenInputType renewToken)
		{
			var result = new LoginViewModel { Message = "Success" };

			ClaimsPrincipal principal = GetClaimsFromExpiredToken(renewToken.AccessToken);

			if (principal == null)
			{
				result.Message = "Invalid Token";
				return result;
			}
			string email = principal.Claims.Where(_ => _.Type == "Email").Select(_ => _.Value).FirstOrDefault();
			if (string.IsNullOrEmpty(email))
			{
				result.Message = "Invalid Token";
				return result;
			}

			var user = _authContext.mUser
			.Where(_ => _.email == email && _.refreshToken == renewToken.RefreshToken && _.refershTokenExpiration > DateTime.Now).FirstOrDefault();
			if (user == null)
			{
				result.Message = "Invalid Token";
				return result;
			}


			result.AccessToken = GetJWTAuthKey(user);

			result.RefreshToken = GenerateRefreshToken();

			user.refreshToken = result.RefreshToken;
			user.refershTokenExpiration = DateTime.Now.AddDays(7);

			_authContext.SaveChanges();

			return result;

		}
		public string LogOut(string accessToken = null)
		{
			//var user = GetUserbyToken(accessToken); 
			if (accessToken == null)
				return null;
			var tokenValidationParameter = new TokenValidationParameters
			{
				ValidIssuer = _tokenSettings.Issuer,
				ValidateIssuer = true,
				ValidAudience = _tokenSettings.Audience,
				ValidateAudience = true,
				IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_tokenSettings.Key)),
				ValidateLifetime = false // ignore expiration
			};

			var jwtHandler = new JwtSecurityTokenHandler();
			var principal = jwtHandler.ValidateToken(accessToken, tokenValidationParameter, out SecurityToken securityToken);

			var jwtScurityToken = securityToken as JwtSecurityToken;

			if (jwtScurityToken == null)
			{
				return null;
			}
			//ClaimsPrincipal principal = GetClaimsFromExpiredToken(accessToken);

			if (principal == null)
			{
				return "Invalid Token";
			}
			string email = principal.Claims.Where(_ => _.Type == "Email").Select(_ => _.Value).FirstOrDefault();
			if (string.IsNullOrEmpty(email))
			{
				return "Invalid Token";
			}

			var user = _authContext.mUser
			.Where(_ => _.email == email).FirstOrDefault();
			if (user == null)
			{
				return "Invalid Token";
			}

			_authContext.SaveChanges();
			return null;
		}

        public string Register(mUser mUser)
        {
			RegisterInputType registerInputType = new RegisterInputType
			{
				Email = mUser.email,
				Lastname = mUser.sName,
				Name = mUser.fName,
				Password = mUser.password,
				ConfirmPassword = mUser.password,
				Phone = mUser.phone,
				statusDate=mUser.statusDate
			};
			return Register(registerInputType);
        }

        UserViewModel IAuthLogic.User
			=> GetUserbyToken();
	}
}
