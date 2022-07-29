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
                Console.WriteLine("[4] Get Album");
                Console.WriteLine("[5] Insert Album");


                choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        await user.GetUserInput(choice);
                        break;
                    case "2":
                        await user.GetUserInput(choice);
                        break;
                    case "3":
                        await user.GetUserInput(choice);
                        break;
                    case "4":
                        await user.GetUserInput(choice);
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
                case "1":// insert a song
                    Console.WriteLine("Enter song Title:");
                    title = Console.ReadLine();
                    Console.WriteLine("Enter the Artist's name:");
                    artist = Console.ReadLine();
                    Console.WriteLine("Enter the Album's name");
                    album = Console.ReadLine();
                    if (title == null || artist == null || album == null) { Console.WriteLine("Input is needed for all attributes. Some input were invalid. Exiting..."); return; }
                    var task1 = await InsertSongRequest(title, artist, album);
                    break;
                case "2":// get a specific song by title and artist
                    Console.WriteLine("Enter song Title:");
                    title = Console.ReadLine();
                    Console.WriteLine("Enter the Artist's name:");
                    artist = Console.ReadLine();
                    if (title == null || artist == null) { Console.WriteLine("Input is needed for all attributes. Some input were invalid. Exiting..."); return; }
                    var task2 = await GetSongRequest(title, artist);
                    break;
                case "3":// get all songs
                    var task3 = await GetAllSongsRequest();
                    break;
                case "4":
                    break;
                default:
                    break;
            }




        }



        public async Task<string> GetAllSongsRequest()
        {
            try
            {
                HttpResponseMessage songsList = await _httpClient.GetAsync($"Song/songs");
                if (!songsList.IsSuccessStatusCode)
                {
                    Console.WriteLine("Songs don't exist!");
                    return "Error-1";
                }

                List<SongDTO>? l = Newtonsoft.Json.JsonConvert.DeserializeObject<List<SongDTO>>(await songsList.Content.ReadAsStringAsync());
                Console.WriteLine(FormatSong(l));
            }
            catch (System.Exception ex)
            {
                Console.WriteLine("Sending a GET request from InsertSongRequest produced an error. Exiting...");
                Console.WriteLine(ex.Message);
                return "Error-1";
            }

            return "Done";
        }
        // method sends a GET and POST request to the API to insert a new song into the database
        public async Task<string> InsertSongRequest(string title, string artist, string album)
        {
            // first check to see if the song exists already
            try
            {
                HttpResponseMessage doesSongExist = await _httpClient.GetAsync($"Song/song/{title}/{artist}");
                if (doesSongExist.IsSuccessStatusCode)
                {
                    Console.WriteLine("Song already exists!");
                    return "Error-1";
                }
            }
            catch (System.Exception ex)
            {
                Console.WriteLine("Sending a GET request from InsertSongRequest produced an error. Exiting...");
                Console.WriteLine(ex.Message);
                return "Error-1";
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
                    HttpResponseMessage doesAlbumExist = await _httpClient.GetAsync($"Song/album/{title}/{artist}");

                    // check if album exists, if not then create the album
                    if (!doesAlbumExist.IsSuccessStatusCode)
                    {
                        // create the album, serialize the album to a string, convert the string to StringContent
                        AlbumDTO serializeAlbum = new AlbumDTO(title, artist);
                        string convertedAlbum = Newtonsoft.Json.JsonConvert.SerializeObject(serializeAlbum);
                        StringContent albumToSend = new StringContent(convertedAlbum, System.Text.Encoding.UTF8, "application/json");
                        // send a POST request
                        HttpResponseMessage postAlbum = await _httpClient.PostAsync("Song/addalbum", albumToSend);

                        if (!postAlbum.IsSuccessStatusCode)
                        {
                            Console.WriteLine("Album not added!");
                        }
                    }
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

            return "Done";
        }

        // method sends a GET request to the API with the title and artist name
        public async Task<string> GetSongRequest(string title, string artist)
        {
            HttpResponseMessage response = await _httpClient.GetAsync($"Song/song/{title}/{artist}");

            if (response.IsSuccessStatusCode)
            {
                try
                {
                    string? obj = await response.Content.ReadAsStringAsync();
                    SongDTO? song = JsonConvert.DeserializeObject<SongDTO>(obj);
                    Console.WriteLine(FormatSong(new List<SongDTO> { song }));
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}. Couldn't read response");
                    return "Error-1";
                }
            }
            else
            {
                Console.WriteLine("No song with that title and artist!");
                return "";
            }
            return "Done";
        }

        // method will format a song
        public string FormatSong(List<SongDTO> songs)
        {
            if (songs == null) return "Error-1";
            int c = 1;
            StringBuilder str = new StringBuilder();
            foreach (var s in songs)
            {
                str.Append($"{c++}. {s.Title} - {s.Artist} # Featured in: {s.Album}\n\n");

            }
            return str.ToString();
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
