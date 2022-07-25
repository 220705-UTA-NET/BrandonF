using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicApp.Logic
{
    public class Song
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Artist { get; set; }
        public string? Album { get; set; }

        public Song() { }

        public Song(int id, string name, string artist, string? album)
        {
            Id = id;
            Name = name;
            Artist = artist;
            Album = album;
        }
    }
}
