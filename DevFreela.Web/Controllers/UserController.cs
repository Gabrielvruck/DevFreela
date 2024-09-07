using DevFreela.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace DevFreela.Web.Controllers
{
    public class UserController : Controller
    {
        private readonly HttpClient _httpClient;

        public UserController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(UserModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // Fazer o POST para a API Web
            var response = await _httpClient.PostAsJsonAsync("https://localhost:7028/api/Users", model);

            if (response.IsSuccessStatusCode)
            {
                ViewBag.Message = "User created successfully!";
                return View();
            }
            else
            {
                // Pegar erros de validação retornados pela API e exibi-los
                var errors = await response.Content.ReadFromJsonAsync<Dictionary<string, string[]>>();

                foreach (var error in errors)
                {
                    foreach (var errorMessage in error.Value)
                    {
                        ModelState.AddModelError(error.Key, errorMessage);
                    }
                }

                return View(model);
            }
        }
    }
}
