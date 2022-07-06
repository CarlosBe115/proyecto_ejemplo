using BWT.UI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace BWT.UI.Controllers
{
    public class DashController : Controller
    {
        HttpClientHandler _hadler = new HttpClientHandler();
        private readonly IConfiguration _configuration;
        private string apiBaseUrl;

        public DashController(IConfiguration configuration)
        {
            _configuration = configuration;
            apiBaseUrl = _configuration.GetValue<string>("WebApiBaseUrl");
            _hadler.ServerCertificateCustomValidationCallback = (
                sender, cert, chain, ssLPolicyError) => { return true; };
        }
        // GET: DashController
        public async Task<IActionResult> Index()
        {
            if (HttpContext.Session.GetString("Token") == "vacio" )
            {
                TempData["message"] = "Inicia sesión para acceder";
                return Redirect("~/User/Validation");
            }
            using (var httpClient = new HttpClient(_hadler))
            {
                string parameters = $"?NameClan=&DescriptionClan=&CurrentUser=&Abbreviation=&LimitUser=&FKUserCreator={HttpContext.Session.GetInt32("Id")}";
                httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("Token"));
                using (var response = await httpClient.GetAsync(apiBaseUrl + "Clans/all/" + parameters))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    ViewBag.listclan = JsonConvert.DeserializeObject<ApiResponse<IEnumerable<Clans>>>(apiResponse);
                }
            }

            return View();
        }
      
        public ActionResult RegisterClan()
        {
            return View();
        }

    }
}
