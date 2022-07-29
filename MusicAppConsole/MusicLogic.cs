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

    public class AlbumDTO
    {

        public string Title { get; set; }
        public string Artist { get; set; }

        public AlbumDTO() { }

        public AlbumDTO(string title, string artist)
        {
            Title = title;
            Artist = artist;
        }
    }

    public class ArtistDTO
    {
        public string Name { get; set; }
        public ArtistDTO() { }
        public ArtistDTO(string name)
        {
            Name = name;
        }
    }
}