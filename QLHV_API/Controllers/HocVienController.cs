using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Newtonsoft.Json;
using QLHV_API.Entities;
using QLHV_API.Models;
using QLHV_API.Support;
using System.Data;




namespace QLHV_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HocVienController : ControllerBase
    {
        QLHVContext _context = new QLHVContext();
        AppSetting _appSettings = new AppSetting();
        Support.support support = new Support.support();
        connectString connect = new connectString();
        //[HttpGet("GetAllTypeCTDT")]
        ////[Authorize]
        //public IActionResult GetAllTypeCTDT()
        //{
        //    var states = _context.CtdtTypes.ToList();
        //    return Ok(states);
        //}
        [HttpPost("create")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> create(HocVienModel student)
        {
            try
            {
                var authorize_userID = User.FindFirst("Id")?.Value.ToString();

                if (student != null)
                {

                    int value = await _context.Database.
                   ExecuteSqlInterpolatedAsync($"EXEC [dbo].[AddHocVien] {student.Hoten},{student.Sdt},{student.Diachi},{student.Quequan},{student.Ngaysinh},{student.Gioitinh},{student.Loai},{student.Note},{authorize_userID},{student.Cccd}");
                    //var st = _context.Hocviens.SingleOrDefault(m => m.Cccd == student.Cccd);
                    //var login = new DangNhapHv
                    //{
                    //    IdNguoidung = st.Id,
                    //    TenDn = st.MaHv,
                    //    Matkhau = support.ComputeSha256Hash(st.Cccd)
                    //};
                    //_context.DangNhapHvs.Add(login);
                    //await _context.SaveChangesAsync();
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
        [Authorize]
        [Authorize(Roles = "admin")]
        public IActionResult search(string ma)
        {
            try
            {

                var student = _context.Hocviens.FromSqlInterpolated($"EXEC searchhv {ma}");

                return Ok(student);


            }
            catch (Exception ex)
            {
                return StatusCode(500, "Đã xảy ra lỗi.");
            }
        }
        [HttpGet("get")]
        [Authorize(Roles = "admin,sa")]
        public IActionResult get()
        {
            try
            {
                var authorize_userID = User.FindFirst("Id")?.Value.ToString(); // lấy id người dùng của token

                //var authorize_role = User.FindFirst("Role")?.Value.ToString();
                //var authorize_UserName = User.FindFirst("UserName")?.Value.ToString();
                var student = _context.Hocviens.FromSqlInterpolated($"EXEC dbo.gethv {authorize_userID}");

                return Ok(student);


            }
            catch (Exception ex)
            {
                return StatusCode(500, "Đã xảy ra lỗi.");
            }
        }
        [HttpGet("getB")]
        [Authorize(Roles = "admin")]
        public IActionResult getB()
        {
            try
            {
                
                var authorize_userID = User.FindFirst("Id")?.Value.ToString();
                string function = "select [dbo].[getOptionb]";
                var json = JsonConvert.SerializeObject(connect.get(function, authorize_userID));

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
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> delete(Guid id)
        {
            try
            {
                var authorize_userID = User.FindFirst("Id")?.Value.ToString();
                int student = await _context.Database.ExecuteSqlInterpolatedAsync($"EXEC deleteHV {id},{authorize_userID}");

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
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> update(Guid id, [FromBody] HocVienModel student)
        {
            try
            {
                var authorize_userID = User.FindFirst("Id")?.Value.ToString();

                int value = await _context.Database.
                    ExecuteSqlInterpolatedAsync($"EXEC updatehocvien {id},{student.Hoten},{student.Sdt},{student.Diachi},{student.Quequan},{student.Ngaysinh},{student.Gioitinh},{student.Trangthai},{student.Loai},{student.Note},{authorize_userID},{student.Cccd}");

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

        [Route("getById/{id}")]
        [Authorize(Roles = "admin")]
        public IActionResult getById(Guid id)
        {
            try
            {

                //var student = _context.Hocviens.FromSqlRaw("exec getDetailHV @para", id).AsEnumerable().SingleOrDefault();
                var student = _context.Hocviens
           .FromSqlInterpolated($"EXEC getDetailHV {id}").AsEnumerable()
           .FirstOrDefault();
                return Ok(student);
                //var student = connect.get("GetDetailHV", id);
                //var json = JsonConvert.SerializeObject(student);

                //return Content(json, "application/json");

            }
            catch (Exception ex)
            {
                return StatusCode(500, "Đã xảy ra lỗi.");
            }
        }



    }
}
