using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QLHV_API.Models;
using QLHV_API.Entities;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Authorization;

namespace QLHV_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DonViController : ControllerBase
    {

        QLHVContext _context = new QLHVContext();
        AppSetting _appSettings = new AppSetting();
        Support.support support = new Support.support();
        connectString connect = new connectString();
        [HttpGet("get")]
        [Authorize(Roles = "sa")]
        public IActionResult get()
        {
            try
            {
                //var authorize_userID = User.FindFirst("Id")?.Value.ToString();
                string function = "select [dbo].[getDV]()";
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
        [HttpGet("getDVByPC/{ma}")]
        [Authorize(Roles = "sa")]
        public IActionResult getDVByPC(string ma)
        {
            try
            {
                string function = "select [dbo].[getDVByPC]";
                var json = JsonConvert.SerializeObject(connect.get(function,ma));

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
        [HttpGet("getById/{id}")]
        [Authorize(Roles = "sa")]
        public IActionResult getById(Guid id)
        {
            try
            {
                var authorize_userID = User.FindFirst("Id")?.Value.ToString();
                string function = "select [dbo].[getDVById]";
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
        [HttpPost("create")]
        [Authorize(Roles = "sa")]
        public async Task<IActionResult> create(DonViModel dv)
        {
            try
            {
                var authorize_userID = User.FindFirst("Id")?.Value.ToString();

                if (dv != null)
                {

                    int value = await _context.Database.
                   ExecuteSqlInterpolatedAsync($"EXEC dbo.insertDonVi {dv.Ten},{dv.Sdt},{dv.Diachi},{dv.Chihuy},{dv.MaDv},{dv.Thuoc},{dv.Trangthai},{dv.Loai},{authorize_userID}");
                   
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
        [HttpPut("update/{id}")]
        [Authorize(Roles = "sa")]
        public async Task<IActionResult> update(Guid id,DonViModel dv)
        {
            try
            {
                var authorize_userID = User.FindFirst("Id")?.Value.ToString();

                if (dv != null)
                {

                    int value = await _context.Database.
                   ExecuteSqlInterpolatedAsync($"EXEC dbo.updateDonVi {id},{dv.Ten},{dv.Sdt},{dv.Diachi},{dv.Chihuy},{dv.MaDv},{dv.Thuoc},{dv.Trangthai},{dv.Loai},{authorize_userID}");

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
        [HttpGet("getChiHuy/{cccd}")]
        [Authorize(Roles = "sa")]
        public IActionResult getChiHuy(string cccd)
        {
            try
            {
                var chihuy = _context.Nguoidungs.Where(m => m.Cccd == cccd).FirstOrDefault();
                
                return Ok(chihuy);

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

                
                int student = await _context.Database.ExecuteSqlInterpolatedAsync($"EXEC deleteDonVi {id},{authorize_userID}");

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
                // Xử lý ngoại lệ và trả về mã trạng thái 500 Internal Server Error

                return StatusCode(500, "Đã xảy ra lỗi trong quá trình thêm người dùng.");
            }

        }
        [HttpGet("getDVThuocQuyen/{id}")]
        [Authorize(Roles = "sa")]
        public IActionResult getDVThuocQuyen(Guid id)
        {
            try
            {
                var authorize_userID = User.FindFirst("Id")?.Value.ToString();
                string function = "select [dbo].[getDVThuocQuyen]";
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
    }
}