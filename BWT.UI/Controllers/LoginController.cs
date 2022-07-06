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
    public class LoginController : Controller
    {
        public static string Usertoken { get; private set; }
        HttpClientHandler _hadler = new HttpClientHandler();
        private readonly IConfiguration _configuration;
        private string apiBaseUrl;
        public LoginController(IConfiguration configuration)
        {
            _configuration = configuration;
            apiBaseUrl = _configuration.GetValue<string>("WebApiBaseUrl");
            _hadler.ServerCertificateCustomValidationCallback = (
                sender, cert, chain, ssLPolicyError) =>
            { return true; };

        }
        public IActionResult Login()
        {
            //if (HttpContext.Session.GetString("Token") == null)
            //{
            //    TempData["message"] = "Usuario no valido";
            //    return Redirect("~/Home/Index");
            //}
            //HttpContext.Session.SetString(Usertoken, "12345");
            //HttpContext.Session.GetString(Usertoken);
            return View();
        }
        [HttpPost]

        public async Task<IActionResult> Login(Access access)

        {
            ApiResponse<Access> data;
            using (var httpClient = new HttpClient(_hadler))
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(access), Encoding.UTF8, "application/json");

                using (var response = await httpClient.PostAsync(apiBaseUrl + "access/validation", content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    data = JsonConvert.DeserializeObject<ApiResponse<Access>>(apiResponse);

                }
            }

            if (data.Data != null)
            {
                HttpContext.Session.SetString("Token", data.Data.TokenValidation);
                HttpContext.Session.SetInt32("Id", data.Data.Id);
                HttpContext.Session.SetString("Email", data.Data.EmailAddress);
                HttpContext.Session.SetInt32("Rol", data.Data.FkRol);
            }
            else
            {
                return Redirect("~/Home/Privacy");
            }

            return Redirect("~/Home/Privacy");
        }

    }
}