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

        // Methods
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
    }
}
