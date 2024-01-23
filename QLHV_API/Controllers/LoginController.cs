using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using QLHV_API.Entities;
using QLHV_API.Models;
using QLHV_API.Support;
using System.IdentityModel.Tokens.Jwt;
//using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;


namespace QLHV_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        QLHVContext _context = new QLHVContext();
        AppSetting _appSettings = new AppSetting();
        Support.support support = new Support.support();
        string formattedDateTime = "";
        string linkUrl = "";
        public LoginController(IOptionsMonitor<AppSetting> optionsMonitor)
        {
            _appSettings = optionsMonitor.CurrentValue;
        }
        [HttpPost("LoginHV")]
        [Authorize]
        public async Task<IActionResult> LoginHV(LoginModel model)
        {
            var ipAddress = HttpContext.Connection.RemoteIpAddress.ToString();

            try
            {
                var user = _context.DangNhapHvs.SingleOrDefault(p => p.TenDn == model.UserName && p.Matkhau == support.ComputeSha256Hash(model.Password));
                //LichSuDangNhapHv lichSuDangNhapStudent = new LichSuDangNhapHv();
                if (user == null) //không đúng
                {
                    var hv = _context.DangNhapHvs.SingleOrDefault(p => p.TenDn == model.UserName);
                    if (hv == null) { return BadRequest("Dữ liệu người dùng không hợp lệ"); };
                    var hocvien = new LichSuDangNhapHv
                    {
                        IdDn = hv.Id,
                        Thoigian = DateTime.Now,
                        Trangthai = false,
                        Note = "Mật khẩu không đúng",
                        DiachiIp = ipAddress,


                    };
                    _context.LichSuDangNhapHvs.Add(hocvien);
                    await _context.SaveChangesAsync();

                    return Ok(new ApiResponse
                    {
                        Success = false,
                        Message = "Invalid username/password"
                    });
                }
                else
                {
                    var hv = _context.Hocviens.SingleOrDefault(m => m.Id == user.IdNguoidung);
                    if (hv != null && hv.Trangthai != "Del")
                    {
                        //cấp token
                        var token = await GenerateTokenHV(user);

                        var hocvien = new LichSuDangNhapHv
                        {
                            IdDn = user.Id,
                            Thoigian = DateTime.Now,
                            Trangthai = true,
                            Note = "Đăng nhập thành công",
                            DiachiIp = ipAddress,


                        };
                        _context.LichSuDangNhapHvs.Add(hocvien);
                        await _context.SaveChangesAsync();

                        return Ok(new ApiResponse
                        {
                            Success = true,
                            Message = "Authenticate success",
                            Data = token
                            //DataInfo = new InfoResponse
                            //{
                            //    Id = user.Id,
                            //    Username = user.StudentCode,
                            //    DisplayName = user.DisplayName,
                            //    OfficeId = user.OfficeId,
                            //    OfficeName = office.OfficeName,
                            //    Role = "Học sinh"
                            //}
                        });
                    }

                    else
                    {
                        return Ok(new ApiResponse
                        {
                            Success = false,
                            Message = "Dữ liệu nhập vào sai"
                        });
                    }
                }


            }
            catch (Exception ex)
            {
                return Ok(new ApiResponse
                {
                    Success = false,
                    Message = "Not found student",
                });
            }

        }
        [HttpPost("LoginUser")]
        //[Authorize]
        public async Task<IActionResult> LoginUser(LoginModel model)
        {
            var ipAddress = HttpContext.Connection.RemoteIpAddress.ToString();

            try
            {
                var user = _context.DangNhapNds.SingleOrDefault(p => p.TenDn == model.UserName && p.Matkhau == support.ComputeSha256Hash(model.Password));
                //LichSuDangNhapHv lichSuDangNhapStudent = new LichSuDangNhapHv();
                if (user == null) //không đúng
                {
                    var nd = _context.DangNhapNds.SingleOrDefault(p => p.TenDn == model.UserName);
                    if (nd == null) { return BadRequest("Dữ liệu người dùng không hợp lệ"); };
                    var nguoidung = new LichSuDangNhapNd
                    {
                        IdDn = nd.Id,
                        Thoigian = DateTime.Now,
                        Trangthai = false,
                        Note = "Mật khẩu không đúng",
                        DiachiIp = ipAddress,


                    };
                    _context.LichSuDangNhapNds.Add(nguoidung);
                    await _context.SaveChangesAsync();

                    return Ok(new ApiResponse
                    {
                        Success = false,
                        Message = "Invalid username/password"
                    });
                }
                else
                {
                    var nguoidung = _context.Nguoidungs.SingleOrDefault(m => m.Id == user.IdNguoidung);
                    if (nguoidung != null && nguoidung.Trangthai != "Del")
                    {
                        //cấp token
                        var token = await GenerateTokenUser(user);

                        var nd = new LichSuDangNhapNd
                        {
                            IdDn = user.Id,
                            Thoigian = DateTime.Now,
                            Trangthai = true,
                            Note = "Đăng nhập thành công",
                            DiachiIp = ipAddress,


                        };
                        _context.LichSuDangNhapNds.Add(nd);
                        await _context.SaveChangesAsync();

                        return Ok(new ApiResponse
                        {
                            Success = true,
                            Message = "Authenticate success",
                            Data = token
                            //DataInfo = new InfoResponse
                            //{
                            //    Id = user.Id,
                            //    Username = user.StudentCode,
                            //    DisplayName = user.DisplayName,
                            //    OfficeId = user.OfficeId,
                            //    OfficeName = office.OfficeName,
                            //    Role = "Học sinh"
                            //}
                        });
                    }

                    else
                    {
                        return Ok(new ApiResponse
                        {
                            Success = false,
                            Message = "Dữ liệu nhập vào sai"
                        });
                    }
                }


            }
            catch (Exception ex)
            {
                return Ok(new ApiResponse
                {
                    Success = false,
                    Message = "Not found user",
                });
            }

        }
        private async Task<tokenModel> GenerateTokenHV(DangNhapHv hv)
        {
            var jwtTokenHandler = new System.IdentityModel.Tokens.Jwt.JwtSecurityTokenHandler();

            var secretKeyBytes = Encoding.UTF8.GetBytes(_appSettings.SecretKey);

            var tokenDescription = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] {
                    new Claim(ClaimTypes.Name, hv.TenDn),
                    //new Claim(JwtRegisteredClaimNames.Sub, officeID),
                    ////new Claim(JwtRegisteredClaimNames.Jti, studentLogin.Id.ToString()),
                    new Claim("UserName",hv.TenDn),

                    new Claim("Id", hv.IdNguoidung.ToString()),
                    //new Claim("OfficeID", officeID),
                    new Claim("Role", "HV"),
                    new Claim(ClaimTypes.Role, "HV"),

                    //roles
                }),
                Expires = DateTime.UtcNow.AddMinutes(30),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secretKeyBytes), SecurityAlgorithms.HmacSha512Signature)
            };

            var token = jwtTokenHandler.CreateToken(tokenDescription);
            var accessToken = jwtTokenHandler.WriteToken(token);
            var refreshToken = GenerateRefreshToken();

            //Lưu database

            if (hv != null)
            {
                hv.ResetToken = refreshToken;
                hv.ResetTime = DateTime.UtcNow;
                _context.DangNhapHvs.Update(hv);
            }
            await _context.SaveChangesAsync();
            tokenModel TOKEN = new tokenModel
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken
            };
            return TOKEN;
        }
        private async Task<tokenModel> GenerateTokenUser(DangNhapNd user)
        {
            var info = _context.Nguoidungs.FirstOrDefault(m => m.Id == user.IdNguoidung);
            var jwtTokenHandler = new System.IdentityModel.Tokens.Jwt.JwtSecurityTokenHandler();

            var secretKeyBytes = Encoding.UTF8.GetBytes(_appSettings.SecretKey);

            var tokenDescription = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] {
                    new Claim(ClaimTypes.Name, user.TenDn),
                    //new Claim(JwtRegisteredClaimNames.Sub, officeID),
                    //new Claim(ClaimTypes.Id,user.IdNguoidung.ToString()),
                    new Claim("UserName",user.TenDn),
                    new Claim("Id", user.IdNguoidung.ToString()),
                    //new Claim("OfficeID", officeID),
                    new Claim("Role",info.IdQuyen),
                    new Claim(ClaimTypes.Role,info.IdQuyen),

                    //roles
                }),
                Expires = DateTime.UtcNow.AddMinutes(30),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secretKeyBytes), SecurityAlgorithms.HmacSha512Signature)
            };

            var token = jwtTokenHandler.CreateToken(tokenDescription);
            var accessToken = jwtTokenHandler.WriteToken(token);
            var refreshToken = GenerateRefreshToken();

            //Lưu database

            if (user != null)
            {
                user.ResetToken = refreshToken;
                user.ResetTime = DateTime.UtcNow;
                _context.DangNhapNds.Update(user);
            }
            await _context.SaveChangesAsync();
            tokenModel TOKEN = new tokenModel
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken
            };
            return TOKEN;
        }


        private string GenerateRefreshToken()
        {
            var random = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(random);

                return Convert.ToBase64String(random);
            }
        }

        [HttpPost("RenewToken")]
        public async Task<IActionResult> RenewToken(tokenModel model)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();
            var secretKeyBytes = Encoding.UTF8.GetBytes(_appSettings.SecretKey);
            var tokenValidateParam = new TokenValidationParameters
            {
                //tự cấp token
                ValidateIssuer = false,
                ValidateAudience = false,

                //ký vào token
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(secretKeyBytes),

                ClockSkew = TimeSpan.Zero,

                ValidateLifetime = false //ko kiểm tra token hết hạn
            };
            try
            {
                //check 1: AccessToken valid format
                var tokenInVerification = jwtTokenHandler.ValidateToken(model.AccessToken, tokenValidateParam, out var validatedToken);

                //check 2: Check alg
                if (validatedToken is JwtSecurityToken jwtSecurityToken)
                {
                    var result = jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha512, StringComparison.InvariantCultureIgnoreCase);
                    if (!result)//false
                    {
                        return Ok(new ApiResponse
                        {
                            Success = false,
                            Message = "Invalid token"
                        });
                    }
                }

                //check 3: Check accessToken expire?
                var utcExpireDate = long.Parse(tokenInVerification.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Exp).Value);

                var expireDate = ConvertUnixTimeToDateTime(utcExpireDate);
                if (expireDate > DateTime.UtcNow)
                {
                    return Ok(new ApiResponse
                    {
                        Success = false,
                        Message = "Access token has not yet expired"
                    });
                }

                //check 4: Check refreshtoken exist in DB
                var storedToken = _context.DangNhapNds.FirstOrDefault(x => x.ResetToken == model.RefreshToken);
                if (storedToken == null)
                {
                    return Ok(new ApiResponse
                    {
                        Success = false,
                        Message = "Refresh token does not exist"
                    });
                }

                _context.Update(storedToken);
                await _context.SaveChangesAsync();

                //create new token
                var user = await _context.DangNhapNds.SingleOrDefaultAsync(nd => nd.Id == storedToken.Id);
                var token = await GenerateTokenUser(user);

                return Ok(new ApiResponse
                {
                    Success = true,
                    Message = "Renew token success",
                    Data = token
                });
            }
            catch
            {
                return BadRequest(new ApiResponse
                {
                    Success = false,
                    Message = "Something went wrong"
                });
            }
        }
        private DateTime ConvertUnixTimeToDateTime(long utcExpireDate)
        {
            var dateTimeInterval = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            //dateTimeInterval.AddSeconds(utcExpireDate).ToUniversalTime();
            var resultDateTime = dateTimeInterval.AddSeconds(utcExpireDate).ToUniversalTime();

            return resultDateTime;
        }
    }
}
