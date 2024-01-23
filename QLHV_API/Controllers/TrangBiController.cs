using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using QLHV_API.Models;
using QLHV_API.Entities;
using Microsoft.EntityFrameworkCore;

namespace QLHV_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrangBiController : ControllerBase
    {
        QLHVContext _context = new QLHVContext();
        AppSetting _appSettings = new AppSetting();
        Support.support support = new Support.support();
        connectString connect = new connectString();
        [HttpGet("get")]
        [Authorize(Roles = "admin")]
        public IActionResult get()
        {
            try
            {
                var authorize_userID = User.FindFirst("Id")?.Value.ToString();
                string function = "select [dbo].[getTB]";
                var json = JsonConvert.SerializeObject(connect.get(function,authorize_userID));

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
        //[HttpGet("getDVByPC/{id}")]
        //[Authorize(Roles = "sa")]
        //public IActionResult getDVByPC(int id)
        //{
        //    try
        //    {
        //        string function = "select [dbo].[getTBById]";
        //        var json = JsonConvert.SerializeObject(connect.get(function, ma));

        //        if (json == null)
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            return Content(json, "application/json");

        //        }


        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, "Đã xảy ra lỗi.");

        //    }
        //}
        [HttpGet("getOptionDV")]
        [Authorize(Roles = "admin")]
        public IActionResult getOptionDV()
        {
            try
            {
                var authorize_userID = User.FindFirst("Id")?.Value.ToString();
                string function = "select [dbo].[getOptionDV]";
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
        [HttpGet("getById/{id}")]
        [Authorize(Roles = "admin")]
        public IActionResult getById(int id)
        {
            try
            {
                var authorize_userID = User.FindFirst("Id")?.Value.ToString();
                string function = "select [dbo].[getTBById]";
                var json = JsonConvert.SerializeObject(connect.getById(function,authorize_userID, id));

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
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> create(TrangBiModel dv)
        {
            try
            {
                var authorize_userID = User.FindFirst("Id")?.Value.ToString();

                if (dv != null)
                {

                    int value = await _context.Database.
                   ExecuteSqlInterpolatedAsync($"EXEC dbo.InsertVatChat {dv.Ten},{dv.Donvi},{dv.Loai},{dv.Trangthai},{dv.Mota},{dv.Soluong}");

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
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> update(int id, TrangBiModel dv)
        {
            try
            {
                var authorize_userID = User.FindFirst("Id")?.Value.ToString();

                if (dv != null)
                {

                    int value = await _context.Database.
                   ExecuteSqlInterpolatedAsync($"EXEC dbo.UpdateVatChat {id},{dv.Ten},{dv.Donvi},{dv.Loai},{dv.Trangthai},{dv.Mota},{dv.Soluong},{authorize_userID}");

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
        [HttpDelete("delete/{id}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> delete(int id)
        {
            try
            {
                var authorize_userID = User.FindFirst("Id")?.Value.ToString();


                int student = await _context.Database.ExecuteSqlInterpolatedAsync($"EXEC dbo.deleteVatChat {id},{authorize_userID}");

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

        [HttpGet("getType")]
        [Authorize(Roles = "admin")]
        public IActionResult getType()
        {
            try
            {

                var user = _context.LoaiVcs.Select(m => new
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
        [HttpGet("getState")]
        [Authorize(Roles = "admin")]
        public IActionResult getState()
        {
            try
            {

                var user = _context.TrangThaiVcs.Where(m=>m.Ma!="Del").Select(m => new
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
    }
}
