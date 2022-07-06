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
    public class UserController : Controller
    {
        HttpClientHandler _hadler = new HttpClientHandler();
        private readonly IConfiguration _configuration;
        private string apiBaseUrl;

        public UserController(IConfiguration configuration)
        {
            _configuration = configuration;
            apiBaseUrl = _configuration.GetValue<string>("WebApiBaseUrl");
            _hadler.ServerCertificateCustomValidationCallback = (
                sender, cert, chain, ssLPolicyError) => { return true; };
        }

        #region Vistas
       
        public IActionResult Validation()
        {
            return View();
        }
        public IActionResult Register()
        {
            return View();
        }
        public IActionResult RegisterInfo()
        {
            return View();
        }
        public IActionResult Recover()
        {
            return View();
        }
        public IActionResult Restore(string token)
        {
            ViewBag.Token = token;
            return View();
        }
        public async Task<IActionResult> ListUser(Access access)
        {
            if (HttpContext.Session.GetString("Token") == "vacio")
            {
                TempData["message"] = "Inicia sesión para acceder";
                return Redirect("~/User/Validation");
            }
            using (var httpClient = new HttpClient(_hadler))
            {
                string parameters = $"?NameGame={access.EmailAddress}&DescriptionGame={access.EmailPassword}";
                httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("Token"));


                using (var response = await httpClient.GetAsync(apiBaseUrl + "Access/all/" + parameters))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    ViewBag.listuser = JsonConvert.DeserializeObject<ApiResponse<IEnumerable<Access>>>(apiResponse);
                }
            }

            return View();
        }

        #endregion

        #region Consumo
        //INICIO DE SESION
        [HttpPost]
        public async Task<IActionResult> Validation(Access access)
        {
            ApiResponse<Access> data;
            ApiResponse<IEnumerable<Clans>> clans;
            ApiResponse< IEnumerable<Clans>> clan;
            ApiResponse<UserInfo> info;
            //ApiResponse<IEnumerable<UserClan>> userclans;
            //ApiResponse<UserClan> userclan;
            using (var httpClient = new HttpClient(_hadler))
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(access), Encoding.UTF8, "application/json");

                using (var response = await httpClient.PostAsync(apiBaseUrl + "access/validation", content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    data = JsonConvert.DeserializeObject<ApiResponse<Access>>(apiResponse);
                }
                if (data.Data != null)
                {
                    FillData(data.Data);

                    string parameters = $"?NameClan=&DescriptionClan=&CurrentUser=&Abbreviation=&LimitUser=&FKUserCreator={HttpContext.Session.GetInt32("Id")}";
                    //string parameters = $"?NameClan=&DescriptionClan=&CurrentUser=&Abbreviation=&LimitUser=&FKUserCreator=";
                    httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("Token"));

                    using (var response = await httpClient.GetAsync(apiBaseUrl + "Clans/all/" + parameters))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        clans = JsonConvert.DeserializeObject<ApiResponse<IEnumerable<Clans>>>(apiResponse);
                        clan = JsonConvert.DeserializeObject<ApiResponse<IEnumerable<Clans>>>(apiResponse);

                        //clan = (ApiResponse<IEnumerable<Clans>>)clans.Data.Where(x => x.FKUserCreator == (int)HttpContext.Session.GetInt32("Id"));
                    }
                    using (var response = await httpClient.GetAsync(apiBaseUrl + "UserInfo/one/?Id=" + HttpContext.Session.GetInt32("Id")))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        info = JsonConvert.DeserializeObject<ApiResponse<UserInfo>>(apiResponse);
                    }
                    //if (clan == null)
                    //{
                    //    using (var response = await httpClient.GetAsync(apiBaseUrl + "UserClan/all/"))
                    //    {
                    //        string apiResponse = await response.Content.ReadAsStringAsync();
                    //        userclans = JsonConvert.DeserializeObject<ApiResponse<IEnumerable<UserClan>>>(apiResponse);
                    //        userclan = (ApiResponse<UserClan>)userclans.Data.Where(x => x.FkUser == (int)HttpContext.Session.GetInt32("Id"));
                    //    }
                    //    var clanes = (ApiResponse<IEnumerable<Clans>>)clans.Data.Where(x => x.Id == userclan.Data.FkClan);
                    //    InfoSession(clanes, info);
                    //}
                    //else
                    //{
                    //    IEnumerable<Clans> t = (IEnumerable<Clans>)clan.Data;
                    //    var clanes = new ApiResponse<IEnumerable<Clans>>(t);
                        InfoSession(clan/*clanes*/, info);
                    //    }

                }

            }

            if (data.Data != null)  { return Redirect("~/Dash/Index"); }
            else { TempData["message"] = "Información incorrecta"; return Redirect("~/User/Validation"); }

        }
        public ActionResult InfoSession(ApiResponse<IEnumerable<Clans>> clans, ApiResponse<UserInfo> info)
        {
       
            if ( clans.Data.Count() != 1 )
            {
                HttpContext.Session.SetInt32("IsOwnerClan", 2);

            }
            else
            {

                HttpContext.Session.SetInt32("IsOwnerClan", 1);
                HttpContext.Session.SetInt32("clan", clans.Data.Select(x => x.Id).FirstOrDefault());



            }
            if (info.Data == null)
            {
                HttpContext.Session.SetString("names", "No hay información");
                HttpContext.Session.SetString("nametag", "No hay información");
                TempData["message"] = "Completa tu información personal aquí."; return Redirect("~/User/RegisterInfo");
            }
            else
            {
                HttpContext.Session.SetInt32("IdUser", info.Data.Id);
                HttpContext.Session.SetString("names", info.Data.FullNames +" "+ info.Data.LastNames);
                HttpContext.Session.SetString("nametag", clans.Data.Select(x => x.Abbreviation).FirstOrDefault() + " " + info.Data.NameTag);

            }
            return View();

        }
        //REGISTRO DE SESION
        [HttpPost]
        public async Task<ActionResult> Register(Access access)
        {
            ApiResponse<bool> request;
            access.FkRol = 1;
            access.IsValid = true;
            using (var httpClient = new HttpClient(_hadler))
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(access), Encoding.UTF8, "application/json");
                using (var response = await httpClient.PostAsync(apiBaseUrl + "access/new", content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    request = JsonConvert.DeserializeObject<ApiResponse<bool>>(apiResponse);

                }
            }
            if (request == null) TempData["message"] = "Usuario no creado";
            else { TempData["message"] = "Credenciales guardadas, porfavor inicie sesión."; return Redirect("~/User/Validation"); }

            return View();
        }
        //REGISTRAR INFORMACION PERSONAL
        [HttpPost]
        public async Task<ActionResult> RegisterInfo(UserInfo userinfo)
        {
            ApiResponse<bool> request;
            userinfo.FkAccess = (int)HttpContext.Session.GetInt32("Id");
            userinfo.ImageProfile = "uno";
            using (var httpClient = new HttpClient(_hadler))
            {
                httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("Token"));

                StringContent content = new StringContent(JsonConvert.SerializeObject(userinfo), Encoding.UTF8, "application/json");
                using (var response = await httpClient.PostAsync(apiBaseUrl + "UserInfo/new", content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    request = JsonConvert.DeserializeObject<ApiResponse<bool>>(apiResponse);

                }
            }
            if (request == null) TempData["message"] = "Información no guardada";
            else { TempData["message"] = "Información guardada, los cambios se verán reflejados en tu próximo inicio de sesión."; return Redirect("~/Dash/Index"); }
            
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Recover(Access access)
        {
            ApiResponse<string> data;
            using (var httpClient = new HttpClient(_hadler))
            {
                using (var response = await httpClient.GetAsync(apiBaseUrl + "access/initialpr/?email=" + access.EmailAddress))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    data = JsonConvert.DeserializeObject<ApiResponse<string>>(apiResponse);
                }
            }

            if (data.Data != null) return RedirectToAction("Restore", "User", new { @token = data.Data });
            else return View("Recover");

        }

        [HttpPost]
        public async Task<ActionResult> Restore(string TokenValidation, string EmailPassword1, string EmailPassword2)
        {
            string parameters = $"?token={TokenValidation}&password1={EmailPassword1}&password2={EmailPassword2}";
            ApiResponse<bool> data;

            using (var httpClient = new HttpClient(_hadler))
            {
                using (var response = await httpClient.GetAsync(apiBaseUrl + "access/finalpr/" + parameters))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    data = JsonConvert.DeserializeObject<ApiResponse<bool>>(apiResponse);
                }
            }

            if (data.Data) return RedirectToAction("Validation", "User");
            else return RedirectToAction("Restore", "User", new { @token = TokenValidation });

        }
        [HttpGet]
        public async Task<IActionResult> InfoPersonal(UserInfo info)
        {
           
            
            if (HttpContext.Session.GetString("Token") == "vacio")
            {
                TempData["message"] = "Inicia sesión para acceder";
                return Redirect("~/User/Validation");
            }
            using (var httpClient = new HttpClient(_hadler))
            {

                
                httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("Token"));

               
                using (var response = await httpClient.GetAsync(apiBaseUrl + "UserInfo/one/?Id=" + HttpContext.Session.GetInt32("Id")))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    ViewBag.info = JsonConvert.DeserializeObject<ApiResponse<UserInfo>>(apiResponse);
                }

            }
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> UpdateInfo(UserInfo info)
        {
           
            if (HttpContext.Session.GetString("Token") == "vacio")
            {
                TempData["message"] = "Inicia sesión para acceder";
                return Redirect("~/User/Validation");
            }
            ApiResponse<bool> data;
            info.FkAccess = (int)HttpContext.Session.GetInt32("Id");
            info.ImageProfile = "uno";
            using (var httpClient = new HttpClient(_hadler))
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(info), Encoding.UTF8, "application/json");

                httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("Token"));
                using (var response = await httpClient.PutAsync(apiBaseUrl + "UserInfo/update",content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    data = JsonConvert.DeserializeObject<ApiResponse<bool>>(apiResponse);
                }

               
            }
            if (data.Data != false)
            {
                TempData["message"] = "Información guardada."; return Redirect("~/User/InfoPersonal");
            }
            return View();
        }

        #endregion

        #region Metodos
        public ActionResult Logout()
        {
            HttpContext.Session.SetString("Token", "vacio");
            HttpContext.Session.SetInt32("Id", 0);
            HttpContext.Session.SetString("Email", string.Empty);
            HttpContext.Session.SetInt32("Rol", 0);
            HttpContext.Session.SetString("names", string.Empty);
            HttpContext.Session.SetString("nametag", string.Empty);
            HttpContext.Session.SetInt32("IsOwnerClan", 0);
            HttpContext.Session.SetInt32("clan", 0);
            HttpContext.Session.SetInt32("IdUser", 0);

            return Redirect("~/User/Validation");
        }
        public void FillData(Access data)
        {
            HttpContext.Session.SetString("Token", data.TokenValidation);
            HttpContext.Session.SetInt32("Id", data.Id);
            HttpContext.Session.SetString("Email", data.EmailAddress);
            HttpContext.Session.SetInt32("Rol", data.FkRol);
        }

        
        #endregion
    }

}
