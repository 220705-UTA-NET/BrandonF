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

        [HttpGet("{title}/{artist}")]
        public async Task<ActionResult<Song>> GetSong(string title, string artist)
        {
            //_logger.LogInformation("Name is {0} and artist is {1}", title, artist);
            Song song;
            try
            {
                song = await _repo.GetSongAsync(title, artist);
                //if(song == null) return StatusCode(500);
            }
            catch (Exception e)
            {
                _logger.LogInformation("Song Controller couldn't run GetSong method");
                _logger.LogError(e.Message);
                return StatusCode(500);
            }

            return song;
        }

        [HttpPost("addsong")]
        public async Task InsertSong([FromBody] Song song)
        {

            _logger.LogInformation(song.Title + " found song");
            // song.Album == null ? "" : song.Album
            try
            {
                await _repo.InsertSongAsync(song.Title, song.Artist, song.Album);
            }
            catch (Exception e)
            {
                _logger.LogInformation("Encountered error in InsertSong while trying to insert a song.");
                _logger.LogError(e.Message);
                return;
            }
        }
    }
}
