using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using BLL;
using VirtusFitApi.Reports.Models;

namespace VirtusFitWeb.Services
{
    public class ReportService : IReportService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IAdminService _adminService;
        public ReportService(IHttpClientFactory httpClientFactory, IAdminService adminService)
        {
            _httpClientFactory = httpClientFactory;
            _adminService = adminService;
        }
        public async Task<OverallReport> FetchOverallReport()
        {
            var client = _httpClientFactory.CreateClient();


            var response = await client.GetAsync("https://localhost:5001/VirtusFit/report/overall");

            var content = await response.Content.ReadAsStringAsync();

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };


            return JsonSerializer.Deserialize<OverallReport>(content, options);
        }

        public async Task<OverallReport> FetchDateSpecifiedReport(DateTime start, DateTime finish)
        {
            var client = _httpClientFactory.CreateClient();

            var request = new HttpRequestMessage(HttpMethod.Get, "https://localhost:5001/VirtusFit/report/daily");

            request.Headers.Add("start", Convert.ToString(start, CultureInfo.CurrentCulture));
            request.Headers.Add("finish", Convert.ToString(finish, CultureInfo.CurrentCulture));

            //var response = await client.GetAsync($"https://localhost:5001/VirtusFit/report/user/{username}");


            var response = await client.SendAsync(request);

            var content = await response.Content.ReadAsStringAsync();

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };

            return JsonSerializer.Deserialize<OverallReport>(content, options);
        }

        public async Task<UserReport> FetchUserReport(string username)
        {
            var client = _httpClientFactory.CreateClient();

            //var request = new HttpRequestMessage(HttpMethod.Get, "https://localhost:5001/VirtusFit/user/{username}");
            //request.Headers.Add("username", username);

            var response = await client.GetAsync($"https://localhost:5001/VirtusFit/report/user/{username}");


            //var response = await client.SendAsync(request);

            var content = await response.Content.ReadAsStringAsync();

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };

            return JsonSerializer.Deserialize<UserReport>(content, options);
        }

        public List<string> ListOfUsernames()
        {
            var users = _adminService.ListAllUsers().Select(user => user.UserName).ToList();

            return users;
        }
    }
}
