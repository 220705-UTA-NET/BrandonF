using System;

namespace MusicClasses
{
    public class SongDTO
    {
        // public int Id { get; set; }
        public string Title { get; set; }
        public string Artist { get; set; }
        public string? Album { get; set; }

        public SongDTO() { }

        public SongDTO(string title, string artist, string? album)
        {
            Title = title;
            Artist = artist;
            Album = album;
        }
    }
}