using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Localization;
using BulkyBookAgain.Services;
using Bulky.Models.Models;

namespace BulkyBookAgain.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private string baseURL = "https://localhost:7069/";


        public async Task<IActionResult> Index()
        {
            List<Category> categories = new List<Category>();
            using (var _httpClient = new HttpClient())
            {
                _httpClient.BaseAddress = new Uri(baseURL + "api/Category");
                _httpClient.DefaultRequestHeaders.Accept.Clear();
                _httpClient.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage getData = await _httpClient.GetAsync("");

                if (getData.IsSuccessStatusCode)
                {
                    var content = getData.Content.ReadAsStringAsync().Result;
                    categories = JsonConvert.DeserializeObject<List<Category>>(content);
                }
            }

            return View(categories);
        }

        public async Task<IActionResult> Create()
        {
            return View();
        }

        public async Task<IActionResult> CreateCategory(Category category)
        {
            using (var _httpClient = new HttpClient())
            {
                _httpClient.BaseAddress = new Uri(baseURL + "api/Category/");
                _httpClient.DefaultRequestHeaders.Accept.Clear();
                _httpClient.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage getData = await _httpClient.PostAsJsonAsync("", category);

                if (getData.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return View("ErrorPage");
                }
            }
        }

        public async Task<IActionResult> Edit(int id)
        {
            Category category = new Category();
            using (var _httpClient = new HttpClient())
            {
                _httpClient.BaseAddress = new Uri(baseURL + "api/Category/");
                _httpClient.DefaultRequestHeaders.Accept.Clear();
                _httpClient.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage getData = await _httpClient.GetAsync($"GetSingleCategory/{id}");

                if (getData.IsSuccessStatusCode)
                {
                    var content = getData.Content.ReadAsStringAsync().Result;
                    category = JsonConvert.DeserializeObject<Category>(content);
                }
                else
                {
                    return View("ErrorPage");
                }
            }

            return View(category);
        }

        public async Task<IActionResult> UpdateCategory(Category category, int id)
        {
            using (var _httpClient = new HttpClient())
            {
                _httpClient.BaseAddress = new Uri(baseURL + "api/Category/");
                _httpClient.DefaultRequestHeaders.Accept.Clear();
                _httpClient.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage getData = await _httpClient.PostAsJsonAsync($"UpdateCategoryById/{id}", category);

                if (getData.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return View("ErrorPage");
                }
            }
        }

        public async Task<IActionResult> Delete(int id)
        {
            Category category = new Category();
            using (var _httpClient = new HttpClient())
            {
                _httpClient.BaseAddress = new Uri(baseURL + "api/Category/");
                _httpClient.DefaultRequestHeaders.Accept.Clear();
                _httpClient.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage getData = await _httpClient.GetAsync($"GetSingleCategory/{id}");

                if (getData.IsSuccessStatusCode)
                {
                    var content = getData.Content.ReadAsStringAsync().Result;
                    category = JsonConvert.DeserializeObject<Category>(content);
                }
                else
                {
                    return View("ErrorPage");
                }
            }

            return View(category);
        }

        public async Task<IActionResult> DeleteCategory(int id)
        {
            using (var _httpClient = new HttpClient())
            {
                _httpClient.BaseAddress = new Uri(baseURL + "api/Category/");
                _httpClient.DefaultRequestHeaders.Accept.Clear();
                _httpClient.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage getData = await _httpClient.DeleteAsync($"DeleteCategoryById/{id}");

                if (getData.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return View("ErrorPage");
                }
            }
        }

        public IActionResult ErrorPage()
        {
            return View();
        }
    }
}
