using MusicApp.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicApp.Database
{
    public interface IRepository
    {
        Task<IEnumerable<Song>> GetAllSongsAsync();
        Task<Song> GetSongAsync(string name, string artist);

        InsertSong(int id, string name, string artist, string album);
    }
}
