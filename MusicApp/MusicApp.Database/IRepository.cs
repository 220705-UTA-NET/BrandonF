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
        Task<Song> GetSongAsync(string title, string artist);
        Task InsertSongAsync(string title, string artist, string album);

        //Task<Album> GetAlbumAsync(string title, string artist);
    }
}
