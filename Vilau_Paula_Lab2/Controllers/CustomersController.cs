using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Vilau_Paula_Lab2.Data;
using LibraryModel.Models;
using Newtonsoft.Json;
using System.Text;
using System.Drawing.Text;

namespace Vilau_Paula_Lab2.Controllers
{
    public class CustomersController : Controller
    {
        private readonly LibraryContext _context;
        private string _baseUrl = "https://localhost:7262/api/Customers";
        private string _cityUrl = "https://localhost:7262/api/Cities";

        public CustomersController(LibraryContext context)
        {
            _context = context;
        }

        public async Task<ActionResult> Index()
        {
            var client = new HttpClient();

            // Fetch customers
            var customersResponse = await client.GetAsync(_baseUrl);
            if (!customersResponse.IsSuccessStatusCode)
            {
                return NotFound();
            }
            var customers = JsonConvert.DeserializeObject<List<Customer>>(await customersResponse.Content.ReadAsStringAsync());

            // Fetch cities
            var cityResponse = await client.GetAsync(_cityUrl);
            if (!cityResponse.IsSuccessStatusCode)
            {
                return NotFound();
            }
            var cities = JsonConvert.DeserializeObject<List<City>>(await cityResponse.Content.ReadAsStringAsync());

            // Map customers to their respective cities based on CityId
            var customersWithCities = customers.Select(customer =>
            {
                customer.City = cities.FirstOrDefault(city => city.Id == customer.CityId);
                return customer;
            }).ToList();

            return View(customersWithCities);
        }

        // GET: Customers/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new BadRequestResult();
            }
            var client = new HttpClient();
            var customerResponse = await client.GetAsync($"{_baseUrl}/{id.Value}");

            if (!customerResponse.IsSuccessStatusCode)
            {
                return NotFound();
            }

            var customer = JsonConvert.DeserializeObject<Customer>(await customerResponse.Content.ReadAsStringAsync());

            string _cityUrl = $"https://localhost:7262/api/Cities/{customer.CityId}";
            var cityResponse = await client.GetAsync(_cityUrl);

            var city = JsonConvert.DeserializeObject<City>(await cityResponse.Content.ReadAsStringAsync());

            customer.City = city;
            if (!customerResponse.IsSuccessStatusCode)
            {
                return NotFound();
            }

            return View(customer);   
        }

        // GET: Customers/Create
        public async Task<ActionResult> Create()
        {
            var client = new HttpClient();
            var response = await client.GetAsync(_cityUrl);
            if (!response.IsSuccessStatusCode)
            {
                return NotFound();
            }
            var cities = JsonConvert.DeserializeObject<List<City>>(await response.Content.ReadAsStringAsync());
            ViewData["Cities"] = new SelectList(cities, "Id", "CityName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind("CustomerID,Name,Adress,BirthDate,CityId")]Customer customer)
        {
            if (!ModelState.IsValid) return View(customer);
            try
            {
                var client = new HttpClient();
                string json = JsonConvert.SerializeObject(customer);
                var response = await client.PostAsync(_baseUrl,
                new StringContent(json, Encoding.UTF8, "application/json"));
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"Unable to create record: { ex.Message}");
            }
            return View(customer);
        }

        // GET: Customers/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new BadRequestResult();
            }
            var client = new HttpClient();
            var response = await client.GetAsync(_cityUrl);
            if (!response.IsSuccessStatusCode)
            {
                return NotFound();
            }
            var cities = JsonConvert.DeserializeObject<List<City>>(await response.Content.ReadAsStringAsync());
            ViewData["Cities"] = new SelectList(cities, "Id", "CityName");

            var clientResponse = await client.GetAsync($"{_baseUrl}/{id.Value}");
            if (!clientResponse.IsSuccessStatusCode)
            {
                return new NotFoundResult();
            }
            var customer = JsonConvert.DeserializeObject<Customer>(
            await clientResponse.Content.ReadAsStringAsync());
            return View(customer);

        }

        // POST: Customers/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind("CustomerID,Name,Adress,BirthDate,CityId")]Customer customer)
        {
            if (!ModelState.IsValid) return View(customer);
            var client = new HttpClient();
            string json = JsonConvert.SerializeObject(customer);
            var response = await client.PutAsync($"{_baseUrl}/{customer.CustomerID}",
            new StringContent(json, Encoding.UTF8, "application/json"));
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View(customer);
        }

        // GET: Customers/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new BadRequestResult();
            }
            var client = new HttpClient();
            var customerResponse = await client.GetAsync($"{_baseUrl}/{id.Value}");

            if (!customerResponse.IsSuccessStatusCode)
            {
                return NotFound();
            }

            var customer = JsonConvert.DeserializeObject<Customer>(await customerResponse.Content.ReadAsStringAsync());

            string _cityUrl = $"https://localhost:7262/api/Cities/{customer.CityId}";
            var cityResponse = await client.GetAsync(_cityUrl);

            var city = JsonConvert.DeserializeObject<City>(await cityResponse.Content.ReadAsStringAsync());

            customer.City = city;
            if (!customerResponse.IsSuccessStatusCode)
            {
                return NotFound();
            }
            return View(customer);
        }

        // POST: Customers/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete([Bind("CustomerID")] Customer customer)
        {
            try
            {
                var client = new HttpClient();
                HttpRequestMessage request =
                new HttpRequestMessage(HttpMethod.Delete,
               $"{_baseUrl}/{customer.CustomerID}")
                {
                    Content = new StringContent(JsonConvert.SerializeObject(customer),
               Encoding.UTF8, "application/json")
                };
                var response = await client.SendAsync(request);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"Unable to delete record:{ ex.Message}");
            }
            return View(customer);
        }


    private bool CustomerExists(int id)
        {
          return (_context.Customers?.Any(e => e.CustomerID == id)).GetValueOrDefault();
        }
    }
}
