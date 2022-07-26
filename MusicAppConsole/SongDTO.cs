using System;

namespace MusicClasses
{
    public class SongDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Artist { get; set; }
        public string? Album { get; set; }

        public SongDTO() { }

        public SongDTO(int id, string name, string artist, string? album)
        {
            Id = id;
            Name = name;
            Artist = artist;
            Album = album;
        }
    }
}