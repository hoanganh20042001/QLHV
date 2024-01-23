using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using QLHV_API.Entities;
using QLHV_API.Models;

namespace QLHV_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NguoiDungController : ControllerBase
    {

        QLHVContext _context = new QLHVContext();
        AppSetting _appSettings = new AppSetting();
        Support.support support = new Support.support();
        connectString connect = new connectString();

        [HttpPost("create")]
        [Authorize(Roles = "sa")]
        public async Task<IActionResult> create(NguoiDungModel user)
        {
            try
            {


                if (user != null)
                {
                    var authorize_userID = User.FindFirst("Id")?.Value.ToString();

                    int value = await _context.Database.
                   ExecuteSqlInterpolatedAsync($"EXEC InsertNguoiDung {user.Ten},{user.Sdt},{user.Diachi},{user.Donvi},{user.Gioitinh},{user.Ngaysinh},{user.Quequan},{user.IdQuyen},{user.chucvu},{user.quanham},{authorize_userID},{user.Email},{user.Cccd}");
                    var st = _context.Nguoidungs.SingleOrDefault(m => m.Cccd == user.Cccd);
                    var login = new DangNhapNd
                    {
                        IdNguoidung = st.Id,
                        TenDn = st.Email,
                        Matkhau = support.ComputeSha256Hash(st.Cccd)
                    };
                    _context.DangNhapNds.Add(login);
                    await _context.SaveChangesAsync();
                    if (value > 0)
                    {
                        return Ok("Change Statecode success!"); // Record updated successfully
                    }
                    else
                    {
                        return NotFound("Student not found"); // Record not found or not updated
                    }

                }
                else
                {
                    return BadRequest("Dữ liệu người dùng không hợp lệ."); // Trả về mã trạng thái 400 Bad Request nếu dữ liệu không hợp lệ
                }
            }
            catch (Exception ex)
            {
                // Xử lý ngoại lệ và trả về mã trạng thái 500 Internal Server Error

                return StatusCode(500, "Đã xảy ra lỗi trong quá trình thêm người dùng.");
            }
        }


        [HttpGet("search/{ma}")]
        [Authorize(Roles = "sa")]
        public IActionResult search(string ma)
        {
            try
            {

                var authorize_userID = User.FindFirst("Id")?.Value.ToString();
                string function = "select [dbo].[searchNguoiDung]";
                var json = JsonConvert.SerializeObject(connect.get(function, ma));

                if (json == null)
                {
                    return NotFound();
                }
                else
                {
                    return Content(json, "application/json");

                }


            }
            catch (Exception ex)
            {
                return StatusCode(500, "Đã xảy ra lỗi.");
            }
        }
        [HttpGet("get")]
        //[Authorize(Roles = "sa")]
        public IActionResult get()
        {
            try
            {
                string function = "select [dbo].[getNguoiDung]()";
                var json = JsonConvert.SerializeObject(connect.get(function));

                if (json == null)
                {
                    return NotFound();
                }
                else
                {
                    return Content(json, "application/json");

                }

            }
            catch (Exception ex)
            {
                return StatusCode(500, "Đã xảy ra lỗi.");
            }
        }


        [HttpDelete("delete/{id}")]
        [Authorize(Roles = "sa")]
        public async Task<IActionResult> delete(Guid id)
        {
            try
            {
                var authorize_userID = User.FindFirst("Id")?.Value.ToString();
                int student = await _context.Database.ExecuteSqlInterpolatedAsync($"EXEC DeleteNguoiDung {id},{authorize_userID}");

                if (student > 0)
                {
                    return Ok("Change Statecode success!"); // Record updated successfully
                }
                else
                {
                    return NotFound("Student not found"); // Record not found or not updated
                }


            }
            catch (Exception ex)
            {
                return StatusCode(500, "Đã xảy ra lỗi.");
            }
        }

        [HttpPut("update/{id}")]
        [Authorize(Roles = "sa")]
        public async Task<IActionResult> update(Guid id, NguoiDungModel user)
        {
            try
            {
                var authorize_userID = User.FindFirst("Id")?.Value.ToString();
                int value = await _context.Database.
                 ExecuteSqlInterpolatedAsync($"EXEC UpdateNguoiDung {id},{user.Ten},{user.Sdt},{user.Diachi},{user.Donvi},{user.Gioitinh},{user.Ngaysinh},{user.Quequan},{user.Trangthai},{user.IdQuyen},{authorize_userID},{user.Email},{user.Cccd},{user.chucvu},{user.quanham}");
              
                await _context.SaveChangesAsync();
                if (value > 0)
                {
                    return Ok("Change Statecode success!"); // Record updated successfully
                }
                else
                {
                    return NotFound("Student not found"); // Record not found or not updated
                }


            }
            catch (Exception ex)
            {
                return StatusCode(500, "Đã xảy ra lỗi.");
            }
        }

        [HttpGet]
        //[Authorize(Roles = "sa")]
        [Route("getById/{id}")]
        public IActionResult getById(Guid id)
        {
            try
            {
                var authorize_userID = User.FindFirst("Id")?.Value.ToString();
                string function = "select [dbo].[getNguoiDungById]";
                var json = JsonConvert.SerializeObject(connect.getById(function, id));

                if (json == null)
                {
                    return NotFound();
                }
                else
                {
                    return Content(json, "application/json");

                }

            }
            catch (Exception ex)
            {
                return StatusCode(500, "Đã xảyvccv ra lỗi.");
            }
        }
        [HttpGet("getAccount")]
        [Authorize(Roles = "sa")]
        public IActionResult getAccount()
        {
            try
            {
                string function = "select [dbo].[getAccount]()";
                var json = JsonConvert.SerializeObject(connect.get(function));

                if (json == null)
                {
                    return NotFound();
                }
                else
                {
                    return Content(json, "application/json");

                }


            }
            catch (Exception ex)
            {
                return StatusCode(500, "Đã xảy ra lỗi.");
            }
        }
        [HttpGet("getAccountById/{id}")]
        [Authorize(Roles = "sa")]
        public IActionResult getAccountbyId(int id)
        {
            try
            {

                var authorize_userID = User.FindFirst("Id")?.Value.ToString();
                string function = "select [dbo].[getAccountById]";
                var json = JsonConvert.SerializeObject(connect.getById(function, id));

                if (json == null)
                {
                    return NotFound();
                }
                else
                {
                    return Content(json, "application/json");

                }

            }
            catch (Exception ex)
            {
                return StatusCode(500, "Đã xảy ra lỗi.");
            }
        }
        [HttpGet("getRole")]
        //[Authorize(Roles = "sa")]
        public IActionResult getRole()
        {
            try
            {

                var user = _context.Quyens.Select(m => new
                {
                    value = m.Ma,
                    label = m.Ten
                }).OrderByDescending(x => x.label);

                return Ok(user);



            }
            catch (Exception ex)
            {
                return StatusCode(500, "Đã xảy ra lỗi.");
            }
        }
        [HttpGet("getChucVu")]
        //[Authorize(Roles = "sa")]
        public IActionResult getChucVu()
        {
            try
            {

                var user = _context.ChucVus.Select(m => new
                {
                    value = m.Id,
                    label = m.Ten
                }).OrderByDescending(x => x.label);

                return Ok(user);



            }
            catch (Exception ex)
            {
                return StatusCode(500, "Đã xảy ra lỗi.");
            }
        }
        [HttpGet("getQuanHam")]
        //[Authorize(Roles = "sa")]
        public IActionResult getQuanHam()
        {
            try
            {

                var user = _context.QuanHams.Select(m => new
                {
                    value = m.Ma,
                    label = m.Ten
                }).OrderByDescending(x => x.label);

                return Ok(user);



            }
            catch (Exception ex)
            {
                return StatusCode(500, "Đã xảy ra lỗi.");
            }
        }
        [HttpGet("getPhanCap")]
        //[Authorize(Roles = "sa")]
        public IActionResult getPhancap()
        {
            try
            {

                var user = _context.LoaiDvs.Where(m => m.Ten != "Lop").Select(m => new
                {
                    value = m.Ma,
                    label = m.Ten
                }).OrderByDescending(x => x.label);

                return Ok(user);



            }
            catch (Exception ex)
            {
                return StatusCode(500, "Đã xảy ra lỗi.");
            }
        }
        [HttpGet("getDonViByLoai/{ma}")]
        //[Authorize(Roles = "sa")]
        public IActionResult getDonViByLoai(string ma)
        {
            try
            {

                var user = _context.Donvis.Where(m => m.Loai == ma).Select(m => new
                {
                    value = m.Id,
                    label = m.Ten
                }).OrderByDescending(x => x.label);

                return Ok(user);



            }
            catch (Exception ex)
            {
                return StatusCode(500, "Đã xảy ra lỗi.");
            }
        }
        [HttpGet("getDMQuyen")]
        //[Authorize(Roles = "sa")]
        public IActionResult getDMQuyen()
        {
            try
            {

                var user = _context.Quyens.FromSqlInterpolated($"EXEC GetDmQuyen ");

                return Ok(user);


            }
            catch (Exception ex)
            {
                return StatusCode(500, "Đã xảy ra lỗi.");

            }

        }
    }
}