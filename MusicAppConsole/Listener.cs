using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text;
using MusicClasses;
using Newtonsoft.Json;

namespace MusicApclep.App
{
    public class Listener
    {

        public HttpClient _httpClient;
        public readonly string serverURL = "https://localhost:7257/api/";



        public Listener()
        {
            _httpClient = new HttpClient();

        }

        // public void AddSong(SongDTO song)
        // {

        // }

        // public List<SongDTO> GetFavorites()
        // {

        // }


        static async Task Main(string[] args)
        {


            string dbURL = File.ReadAllText("C:/Users/brand/Revature/connection.txt");

            Listener user = new Listener();
            user.StartServer(user.serverURL);
            string? choice = "";

            // loop that lets the user continuously make API calls
            while (choice != "-1")
            {

                Console.WriteLine("[1] to send a get a song");
                choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        Console.WriteLine("Enter a song title:");
                        string? title = Console.ReadLine();
                        Console.WriteLine("Enter a the artist name");
                        string? artist = Console.ReadLine();
                        if (title == null || artist == null) { Console.WriteLine("Song title or artist name is invalid"); continue; }
                        var song = await user.GetSongRequest(title, artist);
                        Console.WriteLine(song.ToString());
                        break;
                    case "-1":
                        Console.WriteLine("Exiting...");
                        break;
                    default:
                        break;
                }
            }
        }

        // public


        // method starts the server
        public void StartServer(string serverURL)
        {
            _httpClient.BaseAddress = new Uri(serverURL); // set the base URI address of the web API address
            _httpClient.DefaultRequestHeaders.Accept.Clear(); // clear the request headers
            _httpClient.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json")
            ); // set the request header content type to Json
        }

        public async Task<string> GetSongRequest(string name, string artist)
        {

            HttpResponseMessage response = await _httpClient.GetAsync($"song/{name}/{artist}");

            if (response.IsSuccessStatusCode)
            {
                try
                {
                    string? obj = await response.Content.ReadAsStringAsync();
                    SongDTO? song = JsonConvert.DeserializeObject<SongDTO>(obj);
                    return FormatSong(song);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}. Couldn't read response");
                    return "";
                }
            }
            else
            {
                Console.WriteLine("No such song titled {0} by {1} was found", name, artist);
            }
            return "";
        }

        // method will format a song
        public string FormatSong(SongDTO song)
        {
            Console.WriteLine(song.Artist);
            StringBuilder s = new StringBuilder();
            s.Append($"ID: {song.Id}\nTitle: {song.Name}\nArtist: {song.Artist}\nAlbum: {song.Album}\n");

            return s.ToString();
        }
    }
}
