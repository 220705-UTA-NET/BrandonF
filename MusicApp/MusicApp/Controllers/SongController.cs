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

        // ACTION METHODS ----------------------------------------------------------------
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
                _logger.LogInformation("Error encountered: connecting to database in GetAllSongs");
                _logger.LogError(e.Message);
                return BadRequest(500);
            }
            return songs.ToList();
        }

        // -------------------------------------------------------------------------------

        [HttpGet("song/{title}/{artist}")]
        public async Task<ActionResult<Song>> GetSong(string title, string artist)
        {

            Song song;
            try
            {
                song = await _repo.GetSongAsync(title, artist);
            }
            catch (Exception e)
            {
                _logger.LogInformation("Error encountered: connecting to database in GetSong");
                _logger.LogError(e.Message);
                return BadRequest(500);
            }

            return song;
        }

        // -------------------------------------------------------------------------------

        [HttpPost("addsong")]
        public async Task<ActionResult> InsertSong([FromBody] Song song)
        {

            try
            {
                await _repo.InsertSongAsync(song.Title, song.Artist, song.Album);
            }
            catch (Exception e)
            {
                _logger.LogInformation("Error encountered: connecting to database in InsertSong");
                _logger.LogError(e.Message);
                return BadRequest(500);
            }
            return StatusCode(200);
        }

        // -------------------------------------------------------------------------------

        [HttpGet("album/{title}/{artist}")]
        public async Task<ActionResult<Album>> GetAlbum(string title, string artist)
        {
            Album album;
            try
            {
                album = await _repo.GetAlbumAsync(title, artist);
            }
            catch (Exception e)
            {
                _logger.LogInformation("Error encountered: connecting to database in GetAlbum");
                _logger.LogError(e.Message);
                return StatusCode(500);
            }

            return album;
        }

        // -------------------------------------------------------------------------------

        [HttpPost("addalbum")]
        public async Task<ActionResult> InsertAlbum([FromBody] Album album)
        {
            try
            {
                await _repo.InsertAlbumAsync(album.Title, album.Artist);
            }
            catch (Exception e)
            {
                _logger.LogInformation("Error encountered: connecting to database in InsertAlbum");
                _logger.LogError(e.Message);
                return BadRequest(500);
            }
            return StatusCode(200);
        }

        // -------------------------------------------------------------------------------


    }
}
