using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.FlowAnalysis.DataFlow;
using Newtonsoft.Json.Linq;
using PL.Models;

namespace PL.Controllers
{
    public class MovieController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly Microsoft.AspNetCore.Hosting.IHostingEnvironment hostingEnvironment;

        public MovieController(IConfiguration configuration, Microsoft.AspNetCore.Hosting.IHostingEnvironment hostingEnvironment)
        {
            _configuration = configuration;
            this.hostingEnvironment = hostingEnvironment;
        }
        public IActionResult GetPopularPelis()
        {
            PL.Models.Movie movie = new PL.Models.Movie();
            try
            {
                using(var client = new HttpClient())
                {
                    string urlApi = _configuration["urlApi"];
                    client.BaseAddress = new Uri(urlApi);

                    var responseTask = client.GetAsync("popular?api_key=0167b7182579b9e82c9ca21acbd024f6&language=es-ES");
                    responseTask.Wait();

                    var result = responseTask.Result;

                    if(result.IsSuccessStatusCode)
                    {
                        var readTask = result.Content.ReadAsStringAsync();
                        dynamic resultJSON = JObject.Parse(readTask.Result.ToString());
                        readTask.Wait();

                        movie.Movies = new List<object>();
                        foreach(var resultItem in resultJSON.results)
                        {
                            Models.Movie movieItem = new Models.Movie();
                            movieItem.IdMovie = resultItem.id;
                            movieItem.Title = resultItem.original_title;
                            movieItem.Description = resultItem.overview;
                            movieItem.Image = "https://www.themoviedb.org/t/p/w600_and_h900_bestv2" + resultItem.backdrop_path;

                            movie.Movies.Add(movieItem);
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return View(movie);
        }


        public IActionResult PelisFavoritas()
        {
            PL.Models.Movie movie = new PL.Models.Movie();

            try
            {
                using (var client = new HttpClient())
                {
                    string urlApi = _configuration["urlFavApi"];
                    client.BaseAddress = new Uri(urlApi);

                    var responseTask = client.GetAsync("18702738/favorite/movies?api_key=0167b7182579b9e82c9ca21acbd024f6&session_id=73eddaf6b24e8627dd1b778bacda49b2961a49a4&language=es-ES&sort_by=created_at.asc");
                    responseTask.Wait();
                    
                    var result = responseTask.Result;

                    if (result.IsSuccessStatusCode)
                    {
                        var readTask = result.Content.ReadAsStringAsync();
                        dynamic resultJSON = JObject.Parse(readTask.Result.ToString());
                        readTask.Wait();

                        movie.Movies = new List<object>();

                        foreach (var resultItem in resultJSON.results)
                        {
                            Models.Movie movieItem = new Models.Movie();
                            movieItem.IdMovie = resultItem.id;
                            movieItem.Title = resultItem.original_title;
                            movieItem.Description = resultItem.overview;
                            movieItem.Image = "https://www.themoviedb.org/t/p/w600_and_h900_bestv2" + resultItem.backdrop_path;

                            movie.Movies.Add(movieItem);
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return View(movie);
        }

        //[HttpPost]
        public IActionResult AñadirFavoritas(int IdMovie)
        {
            Models.Favorito movie = new Models.Favorito();

            movie.media_type = "movie";
            movie.media_id = IdMovie;
            movie.Favorite = true;

            try
            {
                using (var client = new HttpClient())
                {
                    string urlApi = _configuration["urlFavApi"];
                    client.BaseAddress = new Uri(urlApi);

                    var responseTask = client.PostAsJsonAsync("18702738/favorite?api_key=0167b7182579b9e82c9ca21acbd024f6&session_id=73eddaf6b24e8627dd1b778bacda49b2961a49a4", movie);
                    responseTask.Wait();

                    var result = responseTask.Result;

                    if(result.IsSuccessStatusCode)
                    {
                        var readTask = result.Content.ReadAsStringAsync();
                        dynamic resultJSON = JObject.Parse(readTask.Result.ToString());
                        readTask.Wait();

                    }

                  
                }
            }
            catch (Exception ex)
            {

            }
            return View("Modal");
        }

        //[HttpPost]
        public IActionResult QuitarFavoritos(int IdMovie)
        {
            Models.Favorito movie = new Models.Favorito();

            movie.media_type = "movie";
            movie.media_id = IdMovie;
            movie.Favorite = false;

            try
            {
                using (var client = new HttpClient())
                {
                    string urlApi = _configuration["urlFavApi"];
                    client.BaseAddress = new Uri(urlApi);

                    var responseTask = client.PostAsJsonAsync("18702738/favorite?api_key=0167b7182579b9e82c9ca21acbd024f6&session_id=73eddaf6b24e8627dd1b778bacda49b2961a49a4", movie);
                    responseTask.Wait();

                    var result = responseTask.Result;

                    if (result.IsSuccessStatusCode)
                    {
                        var readTask = result.Content.ReadAsStringAsync();
                        dynamic resultJSON = JObject.Parse(readTask.Result.ToString());
                        readTask.Wait();
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return View("Modal");
        }
    }
}
