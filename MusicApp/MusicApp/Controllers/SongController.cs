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
        [HttpGet("songs")]
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
                return StatusCode(500, "Songs couldn't be retrieved!");
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
                return StatusCode(500, "Song couldn't be retrieved!");
            }

            return song;
        }

        // -------------------------------------------------------------------------------

        [HttpPost("addsong")]
        public async Task<ActionResult> InsertSong([FromBody] Song song)
        {

            try
            {
                StatusCodeResult rep = await _repo.InsertSongAsync(song.Title, song.Artist, song.Album);
                if (rep.StatusCode == 500) return StatusCode(500, "Song couldn't be inserted!");
            }
            catch (Exception e)
            {
                _logger.LogInformation("Error encountered: connecting to database in InsertSong");
                _logger.LogError(e.Message);
                return StatusCode(500, "Song couldn't be inserted!");
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
                if (album == null) return StatusCode(500, "Album couldn't be found!");
            }
            catch (Exception e)
            {
                _logger.LogInformation("Error encountered: connecting to database in GetAlbum");
                _logger.LogError(e.Message);
                return StatusCode(500, "Song couldn't be retrieved!");
            }

            return album;
        }

        // -------------------------------------------------------------------------------

        [HttpPost("addalbum")]
        public async Task<ActionResult> InsertAlbum([FromBody] Album album)
        {
            try
            {
                StatusCodeResult rep = await _repo.InsertAlbumAsync(album.Title, album.Artist);
                if (rep.StatusCode == 500) return StatusCode(500, "Album couldn't be inserted!");
            }
            catch (Exception e)
            {
                _logger.LogInformation("Error encountered: connecting to database in InsertAlbum");
                _logger.LogError(e.Message);
                return StatusCode(500, "Album couldn't be inserted!");
            }
            return StatusCode(200);
        }

        // -------------------------------------------------------------------------------

        [HttpDelete("deletesong/{title}/{artist}/{album}")]
        public async Task<ActionResult> DeleteSong(string title, string artist, string album)
        {
            try
            {
                StatusCodeResult rep = await _repo.DeleteSongAsync(title, artist, album);
                if (rep.StatusCode == 500) return StatusCode(500, "Song couldn't be deleted!");
            }
            catch (Exception e)
            {
                _logger.LogInformation("Error encountered: connecting to database in DeleteSong");
                _logger.LogError(e.Message);
                return StatusCode(500, "Song couldn't be deleted!");
            }
            return StatusCode(200);
        }

        // -------------------------------------------------------------------------------

        [HttpDelete("deletealbum/{title}/{artist}")]
        public async Task<ActionResult> DeleteAlbum(string title, string artist)
        {
            try
            {
                StatusCodeResult rep = await _repo.DeleteAlbumAsync(title, artist);
                if (rep.StatusCode == 500) return StatusCode(500, "Album couldn't be deleted!");
            }
            catch (Exception e)
            {
                _logger.LogInformation("Error encountered: connecting to database in DeleteAlbum");
                _logger.LogError(e.Message);
                return StatusCode(500, "Album couldn't be deleted!");
            }
            return StatusCode(200);
        }

        // -------------------------------------------------------------------------------

        [HttpGet("albumsongs/{title}/{artist}")]
        public async Task<ActionResult<IEnumerable<Song>>> GetAllSongsFromAlbum(string title, string artist)
        {
            IEnumerable<Song> songs;
            try
            {
                songs = await _repo.GetSongsFromAlbumAsync(title, artist);
                if (songs == null || songs.Any() == false) return StatusCode(500, "Album didn't have any songs!");
            }
            catch (Exception e)
            {
                _logger.LogInformation("Error encountered: connecting to database in GetAllSongs");
                _logger.LogError(e.Message);
                return StatusCode(500, "Songs couldn't be retrieved!");
            }
            return songs.ToList();
        }

        // -------------------------------------------------------------------------------

        [HttpGet("songsby/{artist}")]
        public async Task<ActionResult<IEnumerable<Song>>> GetSongsByArtist(string artist)
        {
            IEnumerable<Song> songs;
            try
            {
                songs = await _repo.GetSongsByArtistAsync(artist);
                if (songs == null || songs.Any() == false) return StatusCode(500, "Artist didn't have any songs or doesn't exist!");
            }
            catch (Exception e)
            {
                _logger.LogInformation("Error encountered: connecting to database in GetSongsByArtist");
                _logger.LogError(e.Message);
                return StatusCode(500, "Songs couldn't be retrieved!");
            }
            return songs.ToList();
        }

        //public async Task<StatusCodeResult> ArtistExist(string artist)
    }
}
