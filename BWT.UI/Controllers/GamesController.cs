using BWT.UI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace BWT.UI.Controllers
{
    public class GamesController : Controller
    {
        HttpClientHandler _hadler = new HttpClientHandler();
        private readonly IConfiguration _configuration;
        private string apiBaseUrl;

        public GamesController(IConfiguration configuration)
        {
            _configuration = configuration;
            apiBaseUrl = _configuration.GetValue<string>("WebApiBaseUrl");
            _hadler.ServerCertificateCustomValidationCallback = (
                sender, cert, chain, ssLPolicyError) => { return true; };
        }

        #region vista
        // GET: GamesController/
        public async Task<IActionResult> ListGames(Games games)
        {
            if (HttpContext.Session.GetString("Token") == "vacio")
            {
                TempData["message"] = "Inicia sesión para acceder";
                return Redirect("~/User/Validation");
            }
            using (var httpClient = new HttpClient(_hadler))
            {
                string parameters = $"?NameGame={games.NameGame}&DescriptionGame={games.DescriptionGame}";
                httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("Token"));

               
                using (var response = await httpClient.GetAsync(apiBaseUrl + "Games/all/" + parameters))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    ViewBag.listgame = JsonConvert.DeserializeObject<ApiResponse<IEnumerable<Games>>>(apiResponse);
                }
            }

            return View();
        }
        [HttpGet]
        // GET: GamesController/Create
        public IActionResult Create()
        {
            if (HttpContext.Session.GetString("Token") == "vacio")
            {
                TempData["message"] = "Inicia sesión para acceder";
                return Redirect("~/User/Validation");
            }
            return View();
        }
        #endregion

        #region Consumo
        [HttpPost]
        public async Task<IActionResult> Create(Games games)
        {
            ApiResponse<bool> request;
            games.ImageGame = "https://i.ibb.co/GHbMfCw/Black-Warriors-Team.png";
            using (var httpClient = new HttpClient(_hadler))
            {
                httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("Token"));

                StringContent content = new StringContent(JsonConvert.SerializeObject(games), Encoding.UTF8, "application/json");
                using(var response = await httpClient.PostAsync(apiBaseUrl + "Games/new", content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    request = JsonConvert.DeserializeObject<ApiResponse<bool>>(apiResponse);
                }
            }
            if (request.Data == false) TempData["message"] = "Juego no creado";
            else return Redirect("~/Games/ListGames");
                return View();
        }


        #endregion
    }
}
