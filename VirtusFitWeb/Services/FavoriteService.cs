using System;
using BLL;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using VirtusFitApi.Models;
using IProductRepository = VirtusFitWeb.DAL.IProductRepository;

namespace VirtusFitWeb.Services
{
    public class FavoriteService : IFavoriteService
    {
        private readonly IProductRepository _repository;
        private readonly IHttpClientFactory _httpClientFactory;

        public FavoriteService(IProductRepository repository, IHttpClientFactory httpClientFactory)
        {
            _repository = repository;
            _httpClientFactory = httpClientFactory;
        }

        private CreateProductAction CreateAction(ActionType type, int productId, string name, string username)
        {
            var action = new CreateProductAction
            {
                Username = username,
                ProductId = productId,
                ProductName = name,
                Action = type,
                Created = DateTime.UtcNow
            };

            return action;
        }

        public List<Product> GetAll(string userId)
        {
            return _repository.GetProducts(userId).Where(product => product.IsFavorite).ToList();
        }

        public Product GetById(int id)
        {
            return _repository.GetProductById(id);
        }

        public void DeleteFromFavorites(Product favorite, string userid, string username)
        {
            var fav = _repository.GetProductById(favorite.ProductId);
            fav.IsFavorite = false;
            _repository.UpdateProduct(fav);
            _repository.Save();

            var client = _httpClientFactory.CreateClient();
            var action = CreateAction(ActionType.ProductRemovedFromFavorites, fav.ProductId, fav.ProductName, username);
            client.PostAsync("https://localhost:5001/VirtusFit/product",
                new StringContent(JsonSerializer.Serialize(action), Encoding.UTF8, "application/json"));
        }

        public void AddToFavorites(Product favorite, string userid, string username)
        {
            var fav = _repository.GetProductById(favorite.ProductId);
            fav.IsFavorite = true;
            _repository.UpdateProduct(fav);
            _repository.Save();

            var client = _httpClientFactory.CreateClient();
            var action = CreateAction(ActionType.ProductAddedToFavorites, fav.ProductId, fav.ProductName, username);
            client.PostAsync("https://localhost:5001/VirtusFit/product",
                new StringContent(JsonSerializer.Serialize(action), Encoding.UTF8, "application/json"));
        }
    }
}
