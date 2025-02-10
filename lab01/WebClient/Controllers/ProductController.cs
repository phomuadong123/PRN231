using BusinessObjects;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using WebClient.Models;

namespace WebClient.Controllers
{
    public class ProductController : Controller
    {
        private readonly HttpClient _httpClient;

        public ProductController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
            _httpClient.BaseAddress = new System.Uri("http://localhost:5274/api/Product"); // Đổi URL nếu cần
        }

        // Lấy danh sách sản phẩm
        public async Task<IActionResult> Index()
        {
            var response = await _httpClient.GetAsync("");
            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsStringAsync();
                var products = JsonSerializer.Deserialize<List<Product>>(data, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                return View(products);
            }

            return View("Error");
        }

        // Hiển thị chi tiết sản phẩm
        public async Task<IActionResult> Details(int id)
        {
            var response = await _httpClient.GetAsync($"/{id}");
            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsStringAsync();
                var product = JsonSerializer.Deserialize<Product>(data, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                return View(product);
            }

            return NotFound();
        }

        // Hiển thị form tạo sản phẩm
        public IActionResult Create()
        {
            return View();
        }

        // Tạo sản phẩm mới
        [HttpPost]
        public async Task<IActionResult> Create(ProductRequest product)
        {
            var content = new StringContent(JsonSerializer.Serialize(product), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("", content);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }

            return View(product);
        }

        // Hiển thị form sửa sản phẩm
        public async Task<IActionResult> Edit(int id)
        {
            var response = await _httpClient.GetAsync($"/{id}");
            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsStringAsync();
                var product = JsonSerializer.Deserialize<Product>(data, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                return View(product);
            }

            return NotFound();
        }

        // Cập nhật sản phẩm
        [HttpPost]
        public async Task<IActionResult> Edit(int id, ProductRequest product)
        {
            var content = new StringContent(JsonSerializer.Serialize(product), Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync($"/{id}", content);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }

            return View(product);
        }

        // Xóa sản phẩm
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _httpClient.DeleteAsync($"/{id}");
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }

            return NotFound();
        }
    }
}
