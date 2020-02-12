using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data.DB_Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Services.IRepository;
using Services.Repository;
using Services.ViewModel;

namespace APITest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlbumController : ControllerBase
    {
        private IAlbumDetails _albumDetails;
        private readonly ILogger _log;       
        public AlbumController(ILogger<AlbumController> log, IAlbumDetails albumDetails)
        {
            _log = log;           
            _albumDetails = albumDetails;          
        }
        
        [HttpPost("GetAlbumDetails")]
        public async Task<IActionResult> GetAlbumDetails([FromBody] IdViewModel model)
        {
            try
            {
              var list= await _albumDetails.GetAlbumDetails(model);
                if (list == null)
                {
                    return BadRequest("SomethingWrong");
                }
                return Ok(list);
            }
            catch (Exception ex)
            {
                _log.LogError("Error", ex.StackTrace);
                return BadRequest(ex.ToString());
            }


        }

    }
}