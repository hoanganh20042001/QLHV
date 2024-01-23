using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QLHV_API.Entities;
using QLHV_API.Models;

namespace QLHV_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LopController : ControllerBase
    {
        QLHVContext _context = new QLHVContext();
        AppSetting _appSettings = new AppSetting();
        Support.support support = new Support.support();
        [HttpGet("get_c/{d}")]
        //[Authorize]
        public IActionResult get_c(Guid d)
        {
            try
            {
                var c = _context.Donvis.Where(m => m.Thuoc == d && m.Loai == "c").Select(m => new { value = m.Id, label = m.Ten }).ToList();

                return Ok(c);


            }
            catch (Exception ex)
            {
                return StatusCode(500, "Đã xảy ra lỗi.");
            }
        }
        [HttpGet("get_d")]
        //[Authorize]
        public IActionResult get_d()
        {
            try
            {
                var d = _context.Donvis.Where(m => m.Loai == "d").Select(m => new { value = m.Id, label = m.Ten }).ToList();

                return Ok(d);


            }
            catch (Exception ex)
            {
                return StatusCode(500, "Đã xảy ra lỗi.");
            }
        }
        [HttpGet("get/{c}")]
        //[Authorize]
        public IActionResult get(Guid c)
        {
            try
            {
                var lop = _context.Donvis.Where(m => m.Thuoc == c && m.Loai == "Lop").Select(m => new { value = m.Id, label = m.Ten }).ToList();

                return Ok(lop);


            }
            catch (Exception ex)
            {
                return StatusCode(500, "Đã xảy ra lỗi.");
            }
        }
        //[HttpGet("getById")]
        ////[Authorize]
        //public IActionResult getById(string ma)
        //{
        //    try
        //    {
        //        var state = _context.TrangThaiHvs.FromSqlInterpolated($"EXEC GetTrangThaiHVById {ma}").AsEnumerable().SingleOrDefault();

        //        return Ok(state);


        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, "Đã xảy ra lỗi.");
        //    }
        //}
    }
}
