using System;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using VirtusFitApi.Models;

namespace VirtusFitWeb.Services
{
    public class BMICalculatorService: IBMICalculatorService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public BMICalculatorService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        private CreateBmiAction CreateAction(double height, double weight, double bmi)
        {
            var action = new CreateBmiAction
            {
                Bmi = bmi,
                Height = height,
                Weight = weight,
                Created = DateTime.UtcNow
            };

            return action;
        }

        public double CalculateBMI(double height, double weight)
        {
            var bmi = weight / (height / 100 * height / 100);
            var client = _httpClientFactory.CreateClient();
            var action = CreateAction(height, weight, bmi);
            client.PostAsync("https://localhost:5001/VirtusFit/plan/bmi",
                new StringContent(JsonSerializer.Serialize(action), Encoding.UTF8, "application/json"));
            return bmi;
        }
    }
}
