using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using QLHV_API.Entities;
using QLHV_API.Models;
using System.Text.Json.Nodes;

namespace QLHV_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuanLyTochucController : ControllerBase
    {

        QLHVContext _context = new QLHVContext();
        AppSetting _appSettings = new AppSetting();
        Support.support support = new Support.support();
        connectString connect = new connectString();

        [HttpGet("get")]
        //[Authorize(Roles = "sa")]
        public IActionResult get()
        {
            try
            {
                string function = "select dbo.getDV()";
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
                return StatusCode(500, "Đã xảyvccv ra lỗi.");
            }
        }
    }
}
