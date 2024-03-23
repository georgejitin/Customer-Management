using Customer_WEB.Models;
using Microsoft.AspNetCore.DataProtection.Repositories;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using System.Text.Json.Serialization;

namespace Customer_WEB.Controllers
{
    public class CustomerController : Controller
    {
        Uri baseAddress = new Uri("https://localhost:7195/api");

        private readonly HttpClient _client;

        public CustomerController()
        {
            _client = new HttpClient();
            _client.BaseAddress = baseAddress;
        }

        [HttpGet]
        public IActionResult Index()
        {
            List<CustomerViewModel> customers = new List<CustomerViewModel>();

            HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "/Customer").Result;

            if (response.IsSuccessStatusCode)
            {
                string date = response.Content.ReadAsStringAsync().Result;
                customers = JsonConvert.DeserializeObject<List<CustomerViewModel>>(date);
            }

            return View(customers);
        }

        [HttpGet]
        public IActionResult Create() {
            return View();
        }

        [HttpPost]
        public IActionResult Create(CustomerViewModel model) {
            try
            {

                string data = JsonConvert.SerializeObject(model);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");

                HttpResponseMessage response = _client.PostAsync(_client.BaseAddress + "/Customer", content).Result;

                if (response.IsSuccessStatusCode)
                {
                    TempData["successMessage"] = "Customer Created";
                    return RedirectToAction("Index");
                        }

            } catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View();
            }
            return View();
        }
    }
}
