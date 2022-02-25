using System;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;
using System.Collections.Generic;

// See https://aka.ms/new-console-template for more information

namespace StudioGhibliAPI // Note: actual namespace depends on the project name.
{
    class Film
    {
        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("original_title")]
        public string Original_title { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("director")]
        public string Director { get; set; }

        [JsonProperty("producer")]
        public string Producer { get; set; }

        [JsonProperty("release_date")]
        public string Release_date { get; set; }
    }
    class Program
    {
        private static readonly HttpClient client = new HttpClient();

        static async Task Main(string[] args)
        {
            await ProcessRepositories();
        }
        private static async Task ProcessRepositories()
        {
            try
            {
                Console.WriteLine("Studio Ghibli Films Overview:");

                var result = await client.GetAsync("https://ghibliapi.herokuapp.com/films/");
                var resultRead = await result.Content.ReadAsStringAsync();

                var films = JsonConvert.DeserializeObject<List<Film>>(resultRead);

                films.ForEach(t =>
                {
                    Console.WriteLine("-----");
                    Console.WriteLine("Released in " + t.Release_date);
                    Console.WriteLine(t.Title);
                    Console.WriteLine("   (" + t.Original_title + ")");
                    Console.WriteLine("Director: " + t.Director);
                    Console.WriteLine("Producer: " + t.Producer);
                    Console.WriteLine("Overview: " + t.Description);
                    Console.WriteLine("\n-----");
                });
            }
            catch (Exception)
            {
                Console.WriteLine("ERROR. Please try again!");
            }
        }
    }
}
