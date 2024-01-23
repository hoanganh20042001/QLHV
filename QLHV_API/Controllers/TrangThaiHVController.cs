using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QLHV_API.Entities;
using QLHV_API.Models;

namespace QLHV_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrangThaiHVController : ControllerBase
    {
        QLHVContext _context = new QLHVContext();
        AppSetting _appSettings = new AppSetting();
        Support.support support = new Support.support();
        [HttpGet("get")]
        //[Authorize]
        public IActionResult get()
        {
            try
            {
                var state = _context.TrangThaiHvs.FromSqlInterpolated($"EXEC gettrangthaiHV").AsEnumerable().Select(m => new
                {
                    value = m.Ma,
                    label = m.Ten
                }).OrderByDescending(x => x.label);
                return Ok(state);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Đã xảy ra lỗi.");
            }
        }
        [HttpGet("getById/{ma}")]
        //[Authorize]
        public IActionResult getById(string ma)
        {
            try
            {
                var state = _context.TrangThaiHvs.FromSqlInterpolated($"EXEC GetTrangThaiHVById {ma}").AsEnumerable().Select(m => new
                {
                    value = m.Ma,
                    label = m.Ten
                }).SingleOrDefault();
                return Ok(state);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Đã xảy ra lỗi.");
            }
        }
    }
}
