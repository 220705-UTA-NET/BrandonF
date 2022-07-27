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

        // action method will retrieve all songs
        public async Task<IEnumerable<Song>> GetAllSongsAsync()
        {
            List<Song> result = new();

            using SqlConnection connection = new(_connectionString);
            await connection.OpenAsync();

            string cmdText = "SELECT Id, Name, Artist, Album FROM MusicCollection.Songs;";

            using SqlCommand cmd = new(cmdText, connection);

            using SqlDataReader reader = await cmd.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                int id = reader.GetInt32(0);
                string name = reader.GetString(1);
                string artist = reader.GetString(2);
                string? album = reader.IsDBNull(3) ? "" : reader.GetString(3);
                Song song = new Song(id, name, artist, album);
                result.Add(song);
            }

            await connection.CloseAsync();

            _logger.LogInformation("Executed GetAllSongsAsync, returned {0} results", result.Count);

            return result;
        }

        // action method will retrieve a specific song based on the name of the song and the artist name
        public async Task<Song> GetSongAsync(string name, string artist)
        {
            using SqlConnection connection = new(_connectionString);
            await connection.OpenAsync();

            string cmdText =
                "SELECT * FROM MusicCollection.Songs WHERE Name=@name AND Artist=@artist;";

            using SqlCommand cmd = new(cmdText, connection);
            cmd.Parameters.AddWithValue("@Name", name);
            cmd.Parameters.AddWithValue("@Artist", artist);

            using SqlDataReader reader = await cmd.ExecuteReaderAsync();

            try
            {
                await reader.ReadAsync();
            }
            catch (Exception e)
            {
                _logger.LogError("GetSongAsync couldn't read from database.");
                return null;
            }

            int id = reader.GetInt32(0);
            string album = reader.IsDBNull(3) ? "" : reader.GetString(3);

            Song song = new Song(id, name, artist, album);

            await connection.CloseAsync();

            _logger.LogInformation("Executed GetSong");

            return song;
        }

        public async Task InsertSongAsync(string name, string artist, string album)
        {
            // check whether any of these are null in the main console app, not here

            using SqlConnection connection = new(_connectionString);
            await connection.OpenAsync();

            string cmdText =
                "INSERT INTO MusicCollection.Songs(Name, Artist, Album) VALUES(@name, @artist, @album);";

            using SqlCommand cmd = new(cmdText, connection);
            cmd.Parameters.AddWithValue("@Name", name);
            cmd.Parameters.AddWithValue("@Artist", artist);
            cmd.Parameters.AddWithValue("@Album", album);


            try
            {
            await cmd.Connection.OpenAsync();
            await cmd.ExecuteNonQueryAsync();

            }
            catch (Exception e)
            {

                _logger.LogError("Error in InsertSongAsync while trying to open a connection or execute non query"); ;
                _logger.LogInformation(e.Message);
            }


            return;
        }
    }
}
