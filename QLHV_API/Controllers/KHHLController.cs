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
    public class KHHLController : ControllerBase
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
                string function = "select [dbo].[getKHHL]";
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
        [HttpGet("getCapTren")]
        [Authorize(Roles = "admin")]
        public IActionResult getCapTren()
        {
            try
            {
                var authorize_userID = User.FindFirst("Id")?.Value.ToString();
                string function = "select [dbo].[getKHHLCapTren]";
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
        [HttpGet("getCapMinh")]
        [Authorize(Roles = "admin")]
        public IActionResult getCapMinh()
        {
            try
            {
                var authorize_userID = User.FindFirst("Id")?.Value.ToString();
                string function = "select [dbo].[getKHHLCapMinh]";
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
        //[HttpGet("getOptionDV")]
        //[Authorize(Roles = "admin")]
        //public IActionResult getOptionDV()
        //{
        //    try
        //    {
        //        var authorize_userID = User.FindFirst("Id")?.Value.ToString();
        //        string function = "select [dbo].[getOptionDV]";
        //        var json = JsonConvert.SerializeObject(connect.get(function, authorize_userID));

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
        [HttpGet("getById/{id}")]
        [Authorize(Roles = "admin")]
        public IActionResult getById(int id)
        {
            try
            {
                var authorize_userID = User.FindFirst("Id")?.Value.ToString();
                string function = "select [dbo].[getKHHLById]";
                var json = JsonConvert.SerializeObject(connect.getById(function, authorize_userID, id));

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
        [HttpGet("getDiemByKhhl/{id}")]
        [Authorize(Roles = "admin")]
        public IActionResult getDiemByKhhl(int id)
        {
            try
            {
                var authorize_userID = User.FindFirst("Id")?.Value.ToString();
                string function = "select [dbo].[getDiemByKhhl]";
                var json = JsonConvert.SerializeObject(connect.getById(function, authorize_userID, id));

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
        [HttpGet("getCtKhhtlByKhhl/{id}")]
        [Authorize(Roles = "admin")]
        public IActionResult getCtKhhlByKhhl(int id)
        {
            try
            {
                var authorize_userID = User.FindFirst("Id")?.Value.ToString();
                string function = "select [dbo].[getCTKHHLByKhhl]";
                var json = JsonConvert.SerializeObject(connect.getById(function, authorize_userID, id));

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
        [HttpGet("getDiemDanh/{id}")]
        [Authorize(Roles = "admin")]
        public IActionResult getDiemDanh(int id)
        {
            try
            {
                var authorize_userID = User.FindFirst("Id")?.Value.ToString();
                string function = "select [dbo].[getDiemDanh]";
                var json = JsonConvert.SerializeObject(connect.getById(function, authorize_userID, id));

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
        public async Task<IActionResult> create(KHHLModel khhl)
        {
            try
            {
                var authorize_userID = User.FindFirst("Id")?.Value.ToString();

                if (khhl != null)
                {

                    int value = await _context.Database.
                   ExecuteSqlInterpolatedAsync($"EXEC dbo.InsertKHHL {khhl.Noidung},{khhl.NgayBd},{khhl.NgayKt},{khhl.Sobuoi},{khhl.Sotiet},{khhl.Note},{authorize_userID}");

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
        public async Task<IActionResult> update(int id, KHHLModel khhl)
        {
            try
            {
                var authorize_userID = User.FindFirst("Id")?.Value.ToString();

                if (khhl != null)
                {


                    int value = await _context.Database.
                   ExecuteSqlInterpolatedAsync($"EXEC dbo.updateKHHL {id},{khhl.Noidung},{khhl.NgayBd},{khhl.NgayKt},{khhl.Sobuoi},{khhl.Sotiet},{khhl.Note},{authorize_userID}");

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
        [HttpPut("updateCT/{id}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> updateCT(int id, CTKHHL khhl)

        {
            try
            {
                var authorize_userID = User.FindFirst("Id")?.Value.ToString();

                if (khhl != null)
                {


                    int value = await _context.Database.
                   ExecuteSqlInterpolatedAsync($"EXEC dbo.updateCTKHHL {id},{khhl.Ngay},{khhl.ThoigianBd},{khhl.ThoigianKt},{authorize_userID}");

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
        [HttpPut("updateDiemHL")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> updateDiemHL(DiemHL diem)
        {
            try
            {
                var authorize_userID = User.FindFirst("Id")?.Value.ToString();

                if (diem != null)
                {


                    int value = await _context.Database.
                   ExecuteSqlInterpolatedAsync($"EXEC dbo.UpdateDiemHL {diem.IdHv},{diem.IdKhhl},{diem.Diem}");

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
        [HttpPut("updateDiemDanh")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> updateDiemDanh(DiemDanh diemdanh)
        {
            try
            {
                var authorize_userID = User.FindFirst("Id")?.Value.ToString();

                if (diemdanh != null)
                {


                    int value = await _context.Database.
                   ExecuteSqlInterpolatedAsync($"EXEC dbo.UpdateDiemDanh {diemdanh.IdHv},{diemdanh.IdCtkhhl},{diemdanh.Trangthai}");

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


                int student = await _context.Database.ExecuteSqlInterpolatedAsync($"EXEC dbo.deleteKHHL {id},{authorize_userID}");

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
    }
}
