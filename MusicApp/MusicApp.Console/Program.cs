using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Text.Json;
using MusicApp.Logic;

namespace MusicApp.App
{
    class Listener
    {
        private readonly static HttpClient _httpClient = new HttpClient();
        private readonly string _url = Environment.dbURL;

        static async Task Main(string[] args)
        {
            Console.WriteLine("Testing serialization");
            Listener user = new Listener();
            user.startServer();

            try
            {
                await user.testing();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void startServer()
        {
            _httpClient.BaseAddress = new Uri("http://localhost:7257/"); // set the base URI address of the web API address
            _httpClient.DefaultRequestHeaders.Accept.Clear(); // clear the request headers
            _httpClient.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json")
            ); // set the request header content type to Json
        }

        public async Task testing()
        {
            var song1 = new Song
            {
                Id = 1,
                Name = "Fleet week",
                Artist = "Bennet Coast",
                Date = DateTime.Now
            };

            var options = new JsonSerializerOptions { WriteIndented = true };
            string jsonstring = JsonSerializer.Serialize(song1, options);

            //Console.WriteLine(jsonstring);

            await _httpClient.GetAsync(jsonstring);
        }
    }
}
