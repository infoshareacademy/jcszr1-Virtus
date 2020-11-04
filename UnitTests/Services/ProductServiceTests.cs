using BLL;
using FluentAssertions;
using Moq;
using System.Collections.Generic;
using VirtusFitWeb.DAL;
using VirtusFitWeb.Services;
using Xunit;
using ProductService = VirtusFitWeb.Services.ProductService;

namespace UnitTests
{
    public class ProductServiceTests
    {
        private ProductService _sut;
        private readonly IProductRepository _productRepository;
        private readonly IDietPlanRepository _dietPlanRepository;
        private readonly IProductInPlanService _productInPlanService;

        //public ProductServiceTests(IProductRepository productRepository, IDietPlanRepository dietPlanRepository, IProductInPlanService productInPlanService)
        //{
        //    _productRepository = productRepository;
        //    _dietPlanRepository = dietPlanRepository;
        //    _productInPlanService = productInPlanService;
        //}


        //[Theory]

        [Fact]
        public void LoadProducts_ValidCall()
        {
            var productRepositoryMock = new Mock<IProductRepository>();
            productRepositoryMock.Setup(repository => repository.GetProducts()).Returns(new List<Product>());
            var dietPlanRepositoryMock = new Mock<IDietPlanRepository>();
            var productInPlanServiceMock = new Mock<IProductInPlanService>();
            var _sut = new ProductService(productRepositoryMock.Object,dietPlanRepositoryMock.Object,productInPlanServiceMock.Object);
            _sut.GetAll().Should().BeOfType(typeof(List<Product>));
        }
    }
}