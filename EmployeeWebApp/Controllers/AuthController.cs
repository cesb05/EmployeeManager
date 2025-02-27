using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Text.Json;

namespace EmployeeWebApp.Controllers
{
    public class AuthController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public AuthController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        #region Login
        // Muestra la vista del login
        public IActionResult Login()
        {
            return View();
        }

        // Acción para procesar el login
        [HttpPost]
        public async Task<IActionResult> Login(string email, string password)
        {
            var client = _httpClientFactory.CreateClient();
            var request = new HttpRequestMessage(HttpMethod.Post, "https://localhost:7176/api/auth/login")
            {
                Content = new StringContent(JsonSerializer.Serialize(new { email, password }), Encoding.UTF8, "application/json")
            };

            var response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                var jsonDoc = JsonDocument.Parse(responseContent);
                var token = jsonDoc.RootElement.GetProperty("token").GetString();

                HttpContext.Session.SetString("JwtToken", token ?? string.Empty); // Usar un valor vacío si el token es nulo
                return RedirectToAction("Index", "Employee"); // Redirige al Home
            }

            ViewBag.Error = "Usuario o contraseña incorrectos"; // Mostrar mensaje de error
            return View();
        }
        #endregion

        #region Register
        // Muestra la vista de registro
        public IActionResult Register()
        {
            return View();
        }

        // Acción para procesar el registro
        [HttpPost]
        public async Task<IActionResult> Register(string email, string password, string confirmPassword)
        {
            if (password != confirmPassword)
            {
                ViewBag.Error = "Las contraseñas no coinciden";
                return View();
            }

            var client = _httpClientFactory.CreateClient();
            var request = new HttpRequestMessage(HttpMethod.Post, "https://localhost:7176/api/auth/register")
            {
                Content = new StringContent(JsonSerializer.Serialize(new { email, password }), Encoding.UTF8, "application/json")
            };

            var response = await client.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                ViewBag.Success = "Usuario registrado correctamente.";
                return View();
            }
            var responseContent = await response.Content.ReadAsStringAsync();

            try
            {
                var errorList = JsonSerializer.Deserialize<List<Dictionary<string, string>>>(responseContent);
                if (errorList != null && errorList.Any())
                {
                    ViewBag.Error = string.Join("<br/>", errorList.Select(e => e["description"]));
                }
                else
                {
                    ViewBag.Error = "Hubo un error al registrar el usuario. " + responseContent;
                }
            }
            catch (JsonException)
            {
                ViewBag.Error = "Hubo un error al registrar el usuario. " + responseContent;
            }

            return View();
        }
        #endregion

        // Acción para hacer logout
        public IActionResult Logout()
        {
            HttpContext.Session.Remove("JwtToken");
            return RedirectToAction("Login");
        }
    }
}