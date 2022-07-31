using MusicApp.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace MusicApp.Database
{
    public interface IRepository
    {
        Task<IEnumerable<Song>> GetAllSongsAsync();
        Task<Song> GetSongAsync(string title, string artist);
        Task<Album> GetAlbumAsync(string title, string artist);
        Task<IEnumerable<Song>> GetSongsFromAlbumAsync(string title, string artist);
        Task<StatusCodeResult> InsertAlbumAsync(string title, string artist);
        Task<StatusCodeResult> InsertSongAsync(string title, string artist, string album);
        Task<StatusCodeResult> DeleteSongAsync(string title, string artist, string album);
        Task<StatusCodeResult> DeleteAlbumAsync(string title, string artist);
        Task<IEnumerable<Song>> GetSongsByArtistAsync(string artist);

    }
}
