using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MusicApp.Logic;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicApp.Database
{
    public class SQLRepository : IRepository
    {
        // Fields
        private readonly string _connectionString;
        private readonly ILogger<SQLRepository> _logger;

        // Constructor
        public SQLRepository(string connectionString, ILogger<SQLRepository> logger)
        {
            _connectionString = connectionString;
            _logger = logger;
        }

        // -----------------------------------------------------------------------------------------------


        // action method will retrieve all songs
        public async Task<IEnumerable<Song>> GetAllSongsAsync()
        {
            List<Song> result = new();

            using SqlConnection connection = new(_connectionString);
            await connection.OpenAsync();

            string cmdText = "SELECT Id, Title, Artist, Album FROM Song;";

            using SqlCommand cmd = new(cmdText, connection);

            using SqlDataReader reader = await cmd.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                //int id = reader.GetInt32(0);
                string title = reader.GetString(1);
                string artist = reader.GetString(2);
                string? album = reader.GetString(3);
                //reader.IsDBNull(3) ? "" : reader.GetString(3);
                Song song = new(title, artist, album);
                result.Add(song);
            }

            await connection.CloseAsync();

            _logger.LogInformation("Executed GetAllSongsAsync");

            return result;
        }

        // -----------------------------------------------------------------------------------------------


        // action method will retrieve a specific song based on the name of the song and the artist name
        public async Task<Song> GetSongAsync(string title, string artist)
        {
            using SqlConnection connection = new(_connectionString);
            await connection.OpenAsync();

            string cmdText = "SELECT * FROM Song WHERE Title=@title AND Artist=@artist;";

            using SqlCommand cmd = new(cmdText, connection);
            cmd.Parameters.AddWithValue("@title", title);
            cmd.Parameters.AddWithValue("@artist", artist);

            using SqlDataReader reader = await cmd.ExecuteReaderAsync();

            try
            {
                await reader.ReadAsync();
            }
            catch (Exception e)
            {
                _logger.LogError("GetSongAsync couldn't read from database.");
                _logger.LogInformation(e.Message);
                return null;
            }

          
            string Title = reader.GetString(1);
            string Artist = reader.GetString(2);
            string Album = reader.GetString(3);
            // reader.IsDBNull(3) ? "" : reader.GetString(3);

            Song song = new(Title, Artist, Album);

            await connection.CloseAsync();

            _logger.LogInformation("Executed GetSongAsync");
            return song;
        }

        // -----------------------------------------------------------------------------------------------


        public async Task<StatusCodeResult> InsertSongAsync(string title, string artist, string album)
        {
            // check whether any of these are null in the main console app, not here

            using SqlConnection connection = new(_connectionString);

            string cmdText =
                "INSERT INTO Song(Title, Artist, Album) VALUES(@title, @artist, @album);";

            using SqlCommand cmd = new(cmdText, connection);
            cmd.Parameters.AddWithValue("@title", title);
            cmd.Parameters.AddWithValue("@artist", artist);
            cmd.Parameters.AddWithValue("@album", album);
            cmd.Connection = connection;

            try
            {
                await connection.OpenAsync();
                await cmd.ExecuteNonQueryAsync();
            }
            catch (Exception e)
            {

                _logger.LogError("Error in InsertSongAsync while trying to open a connection or execute non query"); ;
                _logger.LogInformation(e.Message);
                return new StatusCodeResult(500);
            }

            await connection.CloseAsync();
            _logger.LogInformation("Executed InsertSongAsync");
            return new StatusCodeResult(200);
        }


        // -----------------------------------------------------------------------------------------------

        public async Task<Album> GetAlbumAsync(string title, string artist)
        {
            using SqlConnection connection = new(_connectionString);
            await connection.OpenAsync();

            string cmdText = "SELECT * FROM Album WHERE Title=@title AND Artist=@artist;";

            using SqlCommand cmd = new(cmdText, connection);
            cmd.Parameters.AddWithValue("@title", title);
            cmd.Parameters.AddWithValue("@artist", artist);

            using SqlDataReader reader = await cmd.ExecuteReaderAsync();

            try
            {
                await reader.ReadAsync();
            }
            catch (Exception e)
            {
                _logger.LogError("GetAlbumAsync couldn't read from database.");
                _logger.LogInformation(e.Message);
                return null;
            }


            string Title = reader.GetString(1);
            string Artist = reader.GetString(2);

            Album album = new(Title, Artist);

            await connection.CloseAsync();

            _logger.LogInformation("Executed GetAlbumAsync");
            return album;
        }


        // -----------------------------------------------------------------------------------------------


        public async Task<StatusCodeResult> InsertAlbumAsync(string title, string artist)
        {
            using SqlConnection connection = new(_connectionString);

            string cmdText = "INSERT INTO Album(Title, Artist) VALUES(@title, @artist);";

            using SqlCommand cmd = new(cmdText, connection);
            cmd.Parameters.AddWithValue("@title", title);
            cmd.Parameters.AddWithValue("@artist", artist);
            cmd.Connection = connection;
            
            try
            {
                await connection.OpenAsync();
                await cmd.ExecuteNonQueryAsync();
            }
            catch (Exception e)
            {
                _logger.LogError("Error in InsertAlbumAsync while trying to open a connection or execute non query"); ;
                _logger.LogError(e.Message);
                return new StatusCodeResult(500);
            }

            await connection.CloseAsync();
            _logger.LogInformation("Executed InsertAlbumAsync");
            return new StatusCodeResult(200);
        }


        // -----------------------------------------------------------------------------------------------

        public async Task<StatusCodeResult> DeleteSongAsync(string title, string artist, string album)
        {
            using SqlConnection connection = new(_connectionString);

            string cmdText = "DELETE FROM Song WHERE Title=@title AND Artist=@artist AND Album=@album;";

            using SqlCommand cmd = new(cmdText, connection);
            cmd.Parameters.AddWithValue("@title", title);
            cmd.Parameters.AddWithValue("@artist", artist);
            cmd.Parameters.AddWithValue("@album", album);
            cmd.Connection = connection;

            try
            {
                await connection.OpenAsync();
                await cmd.ExecuteNonQueryAsync();
            }
            catch (Exception e)
            {
                _logger.LogError("Error in DeleteSongAsync while trying to open a connection or execute non query"); ;
                _logger.LogError(e.Message);
                return new StatusCodeResult(500);
            }

            await connection.CloseAsync();
            _logger.LogInformation("Executed DeleteSongAsync");
            return new StatusCodeResult(200);
        }

        // -----------------------------------------------------------------------------------------------

        public async Task<StatusCodeResult> DeleteAlbumAsync(string title, string artist)
        {
            using SqlConnection connection = new(_connectionString);

            string cmdText = "DELETE FROM Album WHERE Title=@title AND Artist=@artist;";

            using SqlCommand cmd = new(cmdText, connection);
            cmd.Parameters.AddWithValue("@title", title);
            cmd.Parameters.AddWithValue("@artist", artist);
            cmd.Connection = connection;

            try
            {
                await connection.OpenAsync();
                await cmd.ExecuteNonQueryAsync();
            }
            catch (Exception e)
            {
                _logger.LogError("Error in DeleteAlbumAsync while trying to open a connection or execute non query"); ;
                _logger.LogError(e.Message);
                return new StatusCodeResult(500);
            }

            await connection.CloseAsync();
            _logger.LogInformation("Executed DeleteAlbumAsync");
            return new StatusCodeResult(200);
        }

        // -----------------------------------------------------------------------------------------------
        public async Task<IEnumerable<Song>> GetSongsFromAlbumAsync(string title, string artist)
        {
            List<Song> result = new();

            using SqlConnection connection = new(_connectionString);
            await connection.OpenAsync();

            string cmdText = "SELECT * FROM Song JOIN Album on Album.Title=Song.Album AND Album.Artist=Song.Artist WHERE Album.Title=@title AND Album.Artist=@artist;";

            using SqlCommand cmd = new(cmdText, connection);
            cmd.Parameters.AddWithValue("@title", title);
            cmd.Parameters.AddWithValue("@artist", artist);

            using SqlDataReader reader = await cmd.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
               
                string Title = reader.GetString(1);
                string Artist = reader.GetString(2);
                
                Song song = new(Title, artist, title);
                result.Add(song);
            }

            await connection.CloseAsync();

            _logger.LogInformation("Executed GetAllSongsAsync");

            return result;
        }

        // -----------------------------------------------------------------------------------------------

        public async Task<IEnumerable<Song>> GetSongsByArtistAsync(string artist)
        {
            List<Song> result = new();

            using SqlConnection connection = new(_connectionString);
            await connection.OpenAsync();

            string cmdText = "SELECT * FROM Song WHERE Song.Artist=@artist;";

            using SqlCommand cmd = new(cmdText, connection);
            cmd.Parameters.AddWithValue("@artist", artist);

            using SqlDataReader reader = await cmd.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {

                string Title = reader.GetString(1);
                string Artist = reader.GetString(2);
                string Album = reader.GetString(3);

                Song song = new(Title, Artist, Album);
                result.Add(song);
            }

            await connection.CloseAsync();

            _logger.LogInformation("Executed GetSongsByArtistAsync");

            return result;
        }

        //public async Task<StatusCodeResult> ArtistExistsAsync(string artist)
        //{
        //    using SqlConnection connection = new(_connectionString);
        //    await connection.OpenAsync();

        //    string cmdText = "SELECT * FROM Artist WHERE Artist.Name=@artist;";

        //    using SqlCommand cmd = new(cmdText, connection);
        //    cmd.Parameters.AddWithValue("@title", title);
        //    cmd.Parameters.AddWithValue("@artist", artist);

        //    using SqlDataReader reader = await cmd.ExecuteReaderAsync();

        //    try
        //    {
        //        await reader.ReadAsync();
        //    }
        //    catch (Exception e)
        //    {
        //        _logger.LogError("GetAlbumAsync couldn't read from database.");
        //        _logger.LogInformation(e.Message);
        //        return null;
        //    }


        //    string Title = reader.GetString(1);
        //    string Artist = reader.GetString(2);

        //    Album album = new(Title, Artist);

        //    await connection.CloseAsync();

        //    _logger.LogInformation("Executed GetAlbumAsync");
        //    return album;

        //}
    }
}
