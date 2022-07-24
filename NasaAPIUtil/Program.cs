using System;
using System.Net.Http;
using System.Net;
using System.Text.Json;
using Newtonsoft.Json;
using NASAImage;

namespace APIUtilizer
{

    class NASAUtil
    {

        static readonly HttpClient client = new HttpClient();

        static async Task Main(string[] args)
        {

            string uri = "https://api.nasa.gov/planetary/apod?api_key=hEe2MsSg9WfR2d8GcQtsZxapgrEUYa2a2rDewzDt";
            try
            {
                var response = await client.GetAsync(uri);
                Console.WriteLine(response);
                var content = await response.Content.ReadAsStringAsync();
                // Console.WriteLine(content);

                var img = JsonConvert.DeserializeObject<Image>(content);
                Console.WriteLine(img.url);
                using (WebClient cl = new WebClient())
                {
                    cl.DownloadFile(img.url, "image.jpg");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            Console.WriteLine("Done");
        }
    }
}


