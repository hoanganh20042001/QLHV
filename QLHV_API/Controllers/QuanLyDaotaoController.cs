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
    public class QuanLyDaotaoController : ControllerBase
    {
        QLHVContext _context = new QLHVContext();
        AppSetting _appSettings = new AppSetting();
        Support.support support = new Support.support();
        connectString connect = new connectString();
        [HttpGet("getD")]
        [Authorize(Roles = "sa")]
        public IActionResult getD()
        {
            try
            {
                var authorize_userID = User.FindFirst("Id")?.Value.ToString(); // lấy id người dùng của token

                var authorize_role = User.FindFirst("Role")?.Value.ToString();
                var authorize_UserName = User.FindFirst("UserName")?.Value.ToString();
                var student = _context.Hocviens.FromSqlInterpolated($"EXEC gethv");

                return Ok(student);


            }
            catch (Exception ex)
            {
                return StatusCode(500, "Đã xảy ra lỗi.");
            }
        }


        [HttpPost("getGV")]
        //[Authorize(Roles = "sa")]
        public IActionResult getGV(searchGV gv)
        {
            try
            {
               
                var json = JsonConvert.SerializeObject(connect.getGV(gv.monhoc,gv.giangvien,gv.hocky));

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
        [HttpGet("getDiembyLop/{id}")]
        //[Authorize(Roles = "sa")]
        public IActionResult getDiemByLop(int id)
        {
            try
            {

                var json = JsonConvert.SerializeObject(connect.getDiembyLop(id));

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
    }
}
