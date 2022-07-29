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

        // fields
        public HttpClient _httpClient;
        public readonly string serverURL = "https://localhost:7257/api/";

        // constructor
        public Listener()
        {
            _httpClient = new HttpClient();

        }


        // main method
        static async Task Main(string[] args)
        {
            string dbURL = File.ReadAllText("C:/Users/brand/Revature/connection.txt");

            Listener user = new Listener();
            user.StartServer(user.serverURL);
            string? choice = "";

            // loop that lets the user continuously make API calls
            while (choice != "-1")
            {

                Console.WriteLine("[1] Insert a song");
                Console.WriteLine("[2] Get a song");
                Console.WriteLine("[3] Get all songs");

                choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        await user.GetUserInput(choice);
                        break;
                    case "2":
                        // await user.InsertSongRequest();
                        break;
                    case "-1":
                        Console.WriteLine("Exiting...");
                        break;
                    default:
                        break;
                }
            }
        }


        // method gets a users' input and executes the specified action
        public async Task GetUserInput(string choice)
        {

            string? title;
            string? artist;
            string? album;

            switch (choice)
            {
                case "1":// get a song by artist
                    Console.WriteLine("Enter song Title:");
                    title = Console.ReadLine();
                    Console.WriteLine("Enter the Artist's name:");
                    artist = Console.ReadLine();
                    Console.WriteLine("Enter the Album's name");
                    album = Console.ReadLine();
                    if (title == null || artist == null || album == null) { Console.WriteLine("Input is needed for all attributes. Some input were invalid. Exiting..."); return; }
                    var task1 = await InsertSongRequest(title, artist, album);
                    break;
                case "2":
                    Console.WriteLine("Enter song Title:");
                    title = Console.ReadLine();
                    Console.WriteLine("Enter the Artist's name:");
                    artist = Console.ReadLine();
                    if (title == null || artist == null) { Console.WriteLine("Input is needed for all attributes. Some input were invalid. Exiting..."); return; }
                    var task2 = await GetSongRequest(title, artist);
                    break;
                case "3":
                    break;
                case "4":
                    break;
                default:
                    break;
            }




        }

        // method sends a GET and POST request to the API to insert a new song into the database
        public async Task<string> InsertSongRequest(string title, string artist, string album)
        {
            // first check to see if the song exists already
            try
            {
                HttpResponseMessage doesSongExist = await _httpClient.GetAsync($"Song/{title}/{artist}");
                if (doesSongExist.IsSuccessStatusCode)
                {
                    Console.WriteLine("Song already exists!");
                    return "";
                }
            }
            catch (System.Exception ex)
            {
                Console.WriteLine("Sending a GET request from InsertSongRequest produced an error. Exiting...");
                Console.WriteLine(ex.Message);
                return "";
            }

            // create a song, convert & serialize it to JSON, then send a POST request to add it to the database
            SongDTO song = new SongDTO(title, artist, album);
            string toInsert = Newtonsoft.Json.JsonConvert.SerializeObject(song);
            StringContent toSend = new StringContent(toInsert, System.Text.Encoding.UTF8, "application/json");

            try
            {
                // send a post request with the serialized song object
                HttpResponseMessage response = await _httpClient.PostAsync("Song/addsong", toSend);

                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("Song added successfully!");

                    // check if the Album already exists in the database. If not then add it to the Album table

                }
                else
                {
                    Console.WriteLine("Song not added!");
                }

            }
            catch (System.Exception ex)
            {
                Console.WriteLine("Insertion unsuccessful!");
                Console.WriteLine(ex.Message);
            }

            return "";
        }

        // method sends a GET request to the API with the title and artist name
        public async Task<string> GetSongRequest(string title, string artist)
        {
            HttpResponseMessage response = await _httpClient.GetAsync($"Song/{title}/{artist}");

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
                    return "Error 404: no song found";
                }
            }
            Console.WriteLine("Bad GET song request");
            return "Error 404: no song found";
        }

        // method will format a song
        public string FormatSong(SongDTO song)
        {
            Console.WriteLine(song.Artist);
            StringBuilder s = new StringBuilder();
            s.Append($"Title: {song.Title}\nArtist: {song.Artist}\nAlbum: {song.Album}\n");

            return s.ToString();
        }
        // method starts the server
        public void StartServer(string serverURL)
        {
            _httpClient.BaseAddress = new Uri(serverURL); // set the base URI address of the web API address
            _httpClient.DefaultRequestHeaders.Accept.Clear(); // clear the request headers
            _httpClient.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json")
            ); // set the request header content type to Json
        }
    }
}
