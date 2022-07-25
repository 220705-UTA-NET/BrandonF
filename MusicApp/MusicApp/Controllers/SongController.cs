using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace MusicApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SongController : ControllerBase
    {
        private readonly IRepository repo;
        private readonly ILogger<SongController> logger;

        public SongController(IRepository repo, ILogger<SongController> logger)
        {
            this.repo = repo;
            this.logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SongController>>> GetAllSongs()
        {
            IEnumerable<SongController> songs;
            try
            {
                await repo.GetAll();
            }
            catch (Exception)
            {
                this.logger.LogError(e, e.message);
                this.logger.LogInformation("Song Controller couldn't run GetAll method");
                return StatusCode(500);
            }
        }

        [HttpGet("")]
        public ContentResult Testing()
        {
            return Content("Message received. Sending a response back");
        }

        [HttpGet("{title}")]
        public ActionResult GetSong(string title)
        {
            return View("/html/test.html");
        }
    }
}
