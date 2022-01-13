using Microsoft.AspNetCore.Mvc;
using SINPRO.Entity.DataModels;
using SINPRO.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SINPRO.Controllers
{
    //[Route("api/[controller]")]
    //[ApiController]
    //public class BannerApiController : ControllerBase
    //{
    //    private readonly ImBannerService _bannerService;

    //    public BannerApiController(ImBannerService bannerService)
    //    {
    //        _bannerService = bannerService;
    //    }

    //    // GET: api/<SINApiController>
    //    [HttpGet]
    //    public IEnumerable<mBanner> GetBanners()
    //    {
    //        return _bannerService.GetAll();
    //    }

    //    // GET api/<SINApiController>/5
    //    [HttpGet("{id}")]
    //    public mBanner Get(int id)
    //    {
    //        return _bannerService.GetByID(id);
    //    }

    //    // POST api/<SINApiController>
    //    [HttpPost]
    //    public void Post([FromBody] mBanner item)
    //    {
    //        _bannerService.Insert(item);
    //    }

    //    // PUT api/<SINApiController>/5
    //    [HttpPut("{id}")]
    //    public void Put(int id, mBanner item)
    //    {
    //        if (id != 0)
    //        {
    //            _bannerService.Update(item);
    //        }
    //    }

    //    // DELETE api/<SINApiController>/5
    //    [HttpDelete("{id}")]
    //    public void Delete(int id)
    //    {
    //        _bannerService.Delete(id);
    //    }
    //}
}
