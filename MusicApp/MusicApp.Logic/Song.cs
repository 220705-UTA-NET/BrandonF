using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicApp.Logic
{
    public class Song
    {
        //public int? Id { get; set; }
        public string Title { get; set; }
        public string Artist { get; set; }
        public string Album { get; set; }

        public Song() { }

        public Song(string title, string artist, string album)
        {
            Title = title;
            Artist = artist;
            Album = album;
        }
    }
}
