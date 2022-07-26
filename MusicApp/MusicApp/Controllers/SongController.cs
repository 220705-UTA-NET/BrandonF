using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MusicApp.Database;
using MusicApp.Logic;
using System.Text.Json;

namespace MusicApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SongController : ControllerBase
    {
        private readonly IRepository _repo;
        private readonly ILogger<SongController> _logger;

        public SongController(IRepository repo, ILogger<SongController> logger)
        {
            _repo = repo;
            _logger = logger;
        }

        [HttpGet("/songs")]
        public async Task<ActionResult<IEnumerable<Song>>> GetAllSongs()
        {
            IEnumerable<Song> songs;
            try
            {
                songs = await _repo.GetAllSongsAsync();
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                _logger.LogInformation("Song Controller couldn't run GetAll method");
                return StatusCode(500);
            }
            return songs.ToList();
        }

        [HttpGet("{name}/{artist}")]
        public async Task<ActionResult<Song>> GetSong(string name, string artist)
        {
            _logger.LogInformation("Name is {0} and artist is {1}", name, artist);
            Song song;
            try
            {
                song = await _repo.GetSongAsync(name, artist);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                _logger.LogInformation("Song Controller couldn't run GetAll method");
                return StatusCode(500);
            }

            return song;
        }

        //[HttpGet("")]
        //public ContentResult Testing()
        //{
        //    return Content("Message received. Sending a response back");
        //}

        //[HttpGet("{title}")]
        //public ActionResult GetSong(string title)
        //{
        //    return Content("/html/test.html");
        //}
    }
}
