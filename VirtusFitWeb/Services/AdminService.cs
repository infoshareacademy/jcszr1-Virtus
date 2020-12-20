using System;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using VirtusFitApi.Models;
using VirtusFitWeb.DAL;

namespace VirtusFitWeb.Services
{
    public class AdminService : IAdminService
    {
        private readonly IAdminRepository _repository;
        private readonly IHttpClientFactory _httpClientFactory;

        public AdminService(IAdminRepository repository, IHttpClientFactory httpClientFactory)
        {
            _repository = repository;
            _httpClientFactory = httpClientFactory;
        }

        public List<IdentityUser> ListAllUsers()
        {
            return _repository.GetAllUsers();
        }

        public async Task ChangePassword(string email, string newPassword)
        {
            var client = _httpClientFactory.CreateClient();
            var action = CreateAction(email, UserAccountActionType.PasswordChanged);
            await client.PostAsync("https://localhost:5001/VirtusFit/user",
                new StringContent(JsonSerializer.Serialize(action), Encoding.UTF8, "application/json"));
            _repository.ChangePassword(email,newPassword);
        }

        public async Task BlockUser(string email)
        {
            var client = _httpClientFactory.CreateClient();
            var action = CreateAction(email, UserAccountActionType.UserLocked);
            await client.PostAsync("https://localhost:5001/VirtusFit/user",
                new StringContent(JsonSerializer.Serialize(action), Encoding.UTF8, "application/json"));
            _repository.BlockUser(email);
        }

        public async Task UnblockUser(string email)
        {
            var client = _httpClientFactory.CreateClient();
            var action = CreateAction(email, UserAccountActionType.UserUnlocked);
            await client.PostAsync("https://localhost:5001/VirtusFit/user",
                new StringContent(JsonSerializer.Serialize(action), Encoding.UTF8, "application/json"));
            _repository.UnblockUser(email);
        }

        public async Task DeleteUser(string email)
        {
            var client = _httpClientFactory.CreateClient();
            var action = CreateAction(email, UserAccountActionType.UserDeleted);
            await client.PostAsync("https://localhost:5001/VirtusFit/user",
                new StringContent(JsonSerializer.Serialize(action), Encoding.UTF8, "application/json"));
            _repository.DeleteUser(email);
        }

        public CreateUserAccountAction CreateAction(string userName, UserAccountActionType type)
        {
            var action = new CreateUserAccountAction
            {
                UserId = userName,
                ActionType = type,
                Created = DateTime.UtcNow
            };

            return action;

        }
    }
}
