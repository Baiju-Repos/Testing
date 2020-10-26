using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Okta_Web.Helpers;
using Okta_Web.Models;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Okta_Web.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private readonly HttpClientHelper _httpClientHelper;
        private readonly IConfiguration _configuration;
        public UserController(HttpClientHelper httpClientHelper, IConfiguration configuration)
        {
            _httpClientHelper = httpClientHelper;
            _configuration = configuration;
        }
        public async Task<IActionResult> Index()
        {
            var response = await _httpClientHelper.ApiCaller($"{_configuration["Okta:UserUrl"]}?filter=status eq \"ACTIVE\" or status eq \"DEPROVISIONED\"", HttpMethod.Get, null, string.Empty);
            var result = await response?.Content?.ReadAsStringAsync();
            var users = new List<User>();
            if (!string.IsNullOrEmpty(result) && response.IsSuccessStatusCode)
                users = JsonConvert.DeserializeObject<List<User>>(result);
            return View(users);
        }
        public IActionResult CreateUser()
        {
            return View();
        }
        [HttpPost]
        public async Task<JsonResult> CreateUser(CreateUser model)
        {
            if (model == null || !ModelState.IsValid)
                return new JsonResult(new 
                { 
                    success = false, 
                    message = string.Join(",", ModelState?.Values.SelectMany(v => v.Errors)?.Where(y => !string.IsNullOrEmpty(y.ErrorMessage))) 
                });
            var content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
            var response = await _httpClientHelper.ApiCaller($"{_configuration["Okta:UserUrl"]}?activate=true", HttpMethod.Post, content, string.Empty);
            var result = await response?.Content?.ReadAsStringAsync();
            return new JsonResult(new { success = response?.IsSuccessStatusCode, message = response?.IsSuccessStatusCode == false ? "Error occured while creating user." : "User created successfully."});
        }
        [HttpPut("[action]/{userId}/{userStatus}")]
        public async Task<JsonResult> UpdateUserStatus(string userId, bool userStatus)
        {
            if (string.IsNullOrEmpty(userId))
                return new JsonResult(new
                {
                    success = false,
                    message = "Please provide valid input"
                });
            var response = await _httpClientHelper.ApiCaller($"{_configuration["Okta:UserUrl"]}/{userId}", HttpMethod.Post, null, userStatus ? _configuration["Okta:ActivationUrl"] : _configuration["Okta:DeActivationUrl"]);
            return new JsonResult(new { success = response?.IsSuccessStatusCode, message = response?.IsSuccessStatusCode == false ? "Error occured while updating user status." : "User status updated successfully." });
        }

        [HttpDelete("[action]/{userId}")]
        public async Task<IActionResult> DeleteUser(string userId)
        {
            if (string.IsNullOrEmpty(userId))
                return new JsonResult(new
                {
                    success = false,
                    message = "Please provide valid input"
                });
            var response = await _httpClientHelper.ApiCaller($"{_configuration["Okta:UserUrl"]}/{userId}", HttpMethod.Delete, null, userId);
            return new JsonResult(new { success = response?.IsSuccessStatusCode, message = response?.IsSuccessStatusCode == false ? "Error occured while deleting user status." : "User deleted successfully." });
        }

        [HttpGet("[action]/{userId}")]
        public async Task<IActionResult> Details(string userId)
        {
            if (string.IsNullOrEmpty(userId))
                return new JsonResult(new
                {
                    success = false,
                    message = "Please provide valid input"
                });
            var response = await _httpClientHelper.ApiCaller($"{_configuration["Okta:UserUrl"]}", HttpMethod.Get, null, userId);
            var result = await response?.Content?.ReadAsStringAsync();
            var user = new User();
            if (!string.IsNullOrEmpty(result))
                user = JsonConvert.DeserializeObject<User>(result);
            return View(user);
        }
        [HttpGet("[action]")]
        public async Task<IActionResult> GetToken()
        {
            var model = new
            {
                username = "TEST",
                password = "TEST123"
            };
            var content = new StringContent(JsonConvert.SerializeObject(model), Encoding.Default, "application/x-www-form-urlencoded");
            var response = await _httpClientHelper.ApiCaller($"{_configuration["Okta:UserUrl"]}", HttpMethod.Post, content, string.Empty);
            var result = await response?.Content?.ReadAsStringAsync();
            var user = new User();
            if (!string.IsNullOrEmpty(result))
                user = JsonConvert.DeserializeObject<User>(result);
            return View(user);
        }
    }
}
