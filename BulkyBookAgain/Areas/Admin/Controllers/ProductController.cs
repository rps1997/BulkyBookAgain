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
    public class ProductController : Controller
    {
        private string baseURL = "https://localhost:7069/";


        public async Task<IActionResult> Index()
        {
            List<Product> categories = new List<Product>();
            using (var _httpClient = new HttpClient())
            {
                _httpClient.BaseAddress = new Uri(baseURL + "api/Product");
                _httpClient.DefaultRequestHeaders.Accept.Clear();
                _httpClient.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage getData = await _httpClient.GetAsync("");

                if (getData.IsSuccessStatusCode)
                {
                    var content = getData.Content.ReadAsStringAsync().Result;
                    categories = JsonConvert.DeserializeObject<List<Product>>(content);
                }
            }

            return View(categories);
        }

        public async Task<IActionResult> Create()
        {
            return View();
        }

        public async Task<IActionResult> CreateProduct(Product Product)
        {
            using (var _httpClient = new HttpClient())
            {
                _httpClient.BaseAddress = new Uri(baseURL + "api/Product/");
                _httpClient.DefaultRequestHeaders.Accept.Clear();
                _httpClient.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage getData = await _httpClient.PostAsJsonAsync("", Product);

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
            Product Product = new Product();
            using (var _httpClient = new HttpClient())
            {
                _httpClient.BaseAddress = new Uri(baseURL + "api/Product/");
                _httpClient.DefaultRequestHeaders.Accept.Clear();
                _httpClient.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage getData = await _httpClient.GetAsync($"GetSingleProduct/{id}");

                if (getData.IsSuccessStatusCode)
                {
                    var content = getData.Content.ReadAsStringAsync().Result;
                    Product = JsonConvert.DeserializeObject<Product>(content);
                }
                else
                {
                    return View("ErrorPage");
                }
            }

            return View(Product);
        }

        public async Task<IActionResult> UpdateProduct(Product Product, int id)
        {
            using (var _httpClient = new HttpClient())
            {
                _httpClient.BaseAddress = new Uri(baseURL + "api/Product/");
                _httpClient.DefaultRequestHeaders.Accept.Clear();
                _httpClient.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage getData = await _httpClient.PostAsJsonAsync($"UpdateProductById/{id}", Product);

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
            Product Product = new Product();
            using (var _httpClient = new HttpClient())
            {
                _httpClient.BaseAddress = new Uri(baseURL + "api/Product/");
                _httpClient.DefaultRequestHeaders.Accept.Clear();
                _httpClient.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage getData = await _httpClient.GetAsync($"GetSingleProduct/{id}");

                if (getData.IsSuccessStatusCode)
                {
                    var content = getData.Content.ReadAsStringAsync().Result;
                    Product = JsonConvert.DeserializeObject<Product>(content);
                }
                else
                {
                    return View("ErrorPage");
                }
            }

            return View(Product);
        }

        public async Task<IActionResult> DeleteProduct(int id)
        {
            using (var _httpClient = new HttpClient())
            {
                _httpClient.BaseAddress = new Uri(baseURL + "api/Product/");
                _httpClient.DefaultRequestHeaders.Accept.Clear();
                _httpClient.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage getData = await _httpClient.DeleteAsync($"DeleteProductById/{id}");

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
