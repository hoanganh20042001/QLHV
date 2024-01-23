using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using QLHV_API.Entities;
using QLHV_API.Models;

namespace QLHV_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ThongKeController : ControllerBase
    {
        QLHVContext _context = new QLHVContext();
        AppSetting _appSettings = new AppSetting();
        Support.support support = new Support.support();
        connectString connect = new connectString();
        [HttpGet("getDiem/{id}")]
        [Authorize(Roles = "admin")]
        public IActionResult getDiem(Guid id)
        {
            try
            {
                var authorize_userID = User.FindFirst("Id")?.Value.ToString();
                string function = "select [dbo].[getTKDiem]";
                var json = JsonConvert.SerializeObject(connect.getById(function, authorize_userID,id));

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
        [HttpGet("getChuyenCan/{id}")]
        [Authorize(Roles = "admin")]
        public IActionResult getChuyenCan(Guid id)
        {
            try
            {
                var authorize_userID = User.FindFirst("Id")?.Value.ToString();
                string function = "select [dbo].[getTKChuyenCan]";
                var json = JsonConvert.SerializeObject(connect.getById(function, authorize_userID,id));

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
        [HttpGet("getDV")]
        [Authorize(Roles = "admin")]
        public IActionResult getDV()
        {
            try
            {
                var authorize_userID = User.FindFirst("Id")?.Value.ToString();
                string function = "select [dbo].[getDVByUser]";
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
    }
}
