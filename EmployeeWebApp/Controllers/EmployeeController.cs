using EmployeeWebApp.DTO;
using EmployeeWebApp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Net.Http.Headers;
using System.Text.Json;

namespace EmployeeWebApp.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public EmployeeController(IHttpClientFactory httpClientFactory, IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            _httpClient = httpClientFactory.CreateClient();
            _httpClient.BaseAddress = new Uri("https://localhost:7176");
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            // Recuperar el token de la sesión y agregarlo a las cabeceras
            var token = _httpContextAccessor.HttpContext?.Session.GetString("JwtToken");
            if (!string.IsNullOrEmpty(token))
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }
        }

        //Este método se llama al cargar la vista principal
        public async Task<IActionResult> Index()
        {
            var response = await _httpClient.GetAsync("api/Employees");
            if (response.IsSuccessStatusCode)
            {
                var employees = await response.Content.ReadAsAsync<List<Employee>>();
                return View(employees);
            }
            else
            {
                return View("Error");
            }
        }

        //Se implemento la libreria DataTable para la busqueda, pero por requisito de la prueba se creo el metodo Search_By_ID
        public async Task<IActionResult> Search(int id)
        {
            var response = await _httpClient.GetAsync($"api/Employees/{id}");
            if (response.IsSuccessStatusCode)
            {
                var employee = await response.Content.ReadAsAsync<Employee>();
                return View(employee);
            }
            else
            {
                return View("Error");
            }
        }

        #region Endpoints para crear un registro
        //Este método se llama al cargar la vista de creación
        public IActionResult Create()
        {
            return View();
        }

        //Este método se llama al confirmar la creación de un registro
        [HttpPost]
        public async Task<IActionResult> Create(EmployeeDTO employeeDTO)
        {
            var response = await _httpClient.PostAsJsonAsync("api/Employees", employeeDTO);
            if (response.IsSuccessStatusCode)
            {
                return Json(new { success = true, message = "Empleado creado con éxito." });
            }
            else
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                string errorMessage = "An error occurred.";

                try
                {
                    var errorObj = JsonSerializer.Deserialize<ApiErrorResponse>(errorContent, new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });

                    if (errorObj?.Errors != null)
                    {
                        errorMessage = string.Join("<br>", errorObj.Errors.SelectMany(e => e.Value));
                    }
                    else if (!string.IsNullOrEmpty(errorObj?.Title))
                    {
                        errorMessage = errorObj.Title;
                    }
                }
                catch
                {
                    errorMessage = errorContent;
                }

                var errorModel = new ErrorViewModel
                {
                    RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier,
                    Message = errorMessage
                };

                return Json(new { success = false, message = errorMessage });
            }
        }
        #endregion

        #region Endpoints para editar un registro
        //Este método se llama al cargar la vista de edición
        public async Task<IActionResult> Edit(string id)
        {
            var response = await _httpClient.GetAsync($"api/Employees/{id}");
            if (response.IsSuccessStatusCode)
            {
                var employee = await response.Content.ReadAsAsync<Employee>();
                return View(employee);
            }
            else
            {
                return View("Error");
            }
        }

        //Este método se llama al confirmar la edición de un registro
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Employee empleado)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/Employees/{id}", empleado);
            if (response.IsSuccessStatusCode)
            {
                return Json(new { success = true, message = "Empleado actualizado con éxito." });
            }
            else
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                string errorMessage = "An error occurred.";

                try
                {
                    var errorObj = JsonSerializer.Deserialize<ApiErrorResponse>(errorContent, new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });

                    if (errorObj?.Errors != null)
                    {
                        errorMessage = string.Join("<br>", errorObj.Errors.SelectMany(e => e.Value));
                    }
                    else if (!string.IsNullOrEmpty(errorObj?.Title))
                    {
                        errorMessage = errorObj.Title;
                    }
                }
                catch
                {
                    errorMessage = errorContent;
                }

                var errorModel = new ErrorViewModel
                {
                    RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier,
                    Message = errorMessage
                };

                return Json(new { success = false, message = errorMessage });
            }
        }
        #endregion

        #region Endpoints para eliminar un registro
        //Este método se llama al cargar la vista de eliminación
        public async Task<IActionResult> Delete(string id)
        {
            var response = await _httpClient.GetAsync($"api/Employees/{id}");
            if (response.IsSuccessStatusCode)
            {
                var employee = await response.Content.ReadAsAsync<Employee>();
                return View(employee);
            }
            else
            {
                return View("Error");
            }
        }

        //Este método se llama al confirma la eliminación de un registro
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var response = await _httpClient.DeleteAsync($"api/Employees/{id}");
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View("Error");
            }
        }
        #endregion

        // Clase para mapear la estructura de error de la API
        public class ApiErrorResponse
        {
            public string? Title { get; set; }
            public Dictionary<string, List<string>>? Errors { get; set; }
        }
    }
}