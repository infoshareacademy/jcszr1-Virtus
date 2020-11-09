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

        [Fact]
        public void LoadProducts_ValidCall()
        {

            var productRepositoryMock = new Mock<IProductRepository>();
            productRepositoryMock.Setup(repository => repository.GetProducts()).Returns(new List<Product>());
            var dietPlanRepositoryMock = new Mock<IDietPlanRepository>();
            var productInPlanServiceMock = new Mock<IProductInPlanService>();
            var _sut = new ProductService(productRepositoryMock.Object, dietPlanRepositoryMock.Object, productInPlanServiceMock.Object);

            _sut.GetAll().Should().BeOfType(typeof(List<Product>));
        }

        [Theory]
        [InlineData(0)]
        [InlineData(100)]
        [InlineData(23152)]
        public void GetProductById_ValidCall(int id)
        {
            // arrange
            var productRepositoryMock = new Mock<IProductRepository>();
            productRepositoryMock.Setup(repository => repository.GetProductById(id)).Returns(new Product());
            var dietPlanRepositoryMock = new Mock<IDietPlanRepository>();
            var productInPlanServiceMock = new Mock<IProductInPlanService>();
            var sut = new ProductService(productRepositoryMock.Object, dietPlanRepositoryMock.Object, productInPlanServiceMock.Object);

            sut.GetById(id).Should().BeOfType(typeof(Product));
        }

        [Theory]
        [InlineData(0)]
        [InlineData(100)]
        [InlineData(23152)]
        public void DeleteProductById_ValidCall(int id)
        {
            var product = new Product();
            var productRepositoryMock = new Mock<IProductRepository>();
            productRepositoryMock.Setup(repository => repository.GetProductById(id)).Returns(product);
            productRepositoryMock.Setup(repository => repository.DeleteProduct(It.IsAny<Product>()));
            var dietPlanRepositoryMock = new Mock<IDietPlanRepository>();
            dietPlanRepositoryMock.Setup(dietPlanRepo => dietPlanRepo.ListAllDietPlans()).Returns(new List<DietPlan>());
            var productInPlanServiceMock = new Mock<IProductInPlanService>();
            var sut = new ProductService(productRepositoryMock.Object, dietPlanRepositoryMock.Object, productInPlanServiceMock.Object);

            sut.DeleteById(id);

            productRepositoryMock.Verify(repository => repository.DeleteProduct(product));

        }

        [Fact]
        public void CreateProduct_ValidCall()
        {
            var product = new Product();
            var productRepositoryMock = new Mock<IProductRepository>();
            var dietPlanRepositoryMock = new Mock<IDietPlanRepository>();
            var productInPlanServiceMock = new Mock<IProductInPlanService>();
            var sut = new ProductService(productRepositoryMock.Object, dietPlanRepositoryMock.Object, productInPlanServiceMock.Object);

            sut.Create(product);

            productRepositoryMock.Verify(repository => repository.InsertProduct(product));
            sut.Create(product).Should().BeOfType(typeof(Product));
        }

        [Theory]
        [InlineData(0)]
        [InlineData(100)]
        [InlineData(23152)]
        public void Update_ValidCall(int id)
        {
            var product = new Product();
            var productRepositoryMock = new Mock<IProductRepository>();
            productRepositoryMock.Setup(repository => repository.GetProductById(id)).Returns(product);
            var dietPlanRepositoryMock = new Mock<IDietPlanRepository>();
            var productInPlanServiceMock = new Mock<IProductInPlanService>();
            var sut = new ProductService(productRepositoryMock.Object, dietPlanRepositoryMock.Object, productInPlanServiceMock.Object);

            sut.Update(id, product);

            productRepositoryMock.Verify(repository => repository.UpdateProduct(product));
        }

        [Theory]
        [InlineData(0)]
        //[InlineData(100)]
        //[InlineData(23152)]

        public void DeleteFromFavorites_ValidCall(int id)
        {
            var product = new Product();
            var productRepositoryMock = new Mock<IProductRepository>();
            productRepositoryMock.Setup(repository => repository.GetProductById(id)).Returns(product);
            var dietPlanRepositoryMock = new Mock<IDietPlanRepository>();
            var productInPlanServiceMock = new Mock<IProductInPlanService>();
            var sut = new ProductService(productRepositoryMock.Object, dietPlanRepositoryMock.Object, productInPlanServiceMock.Object);

            sut.DeleteFromFavorites(product);

            product.IsFavourite.Should().BeFalse();
            productRepositoryMock.Verify(repository => repository.UpdateProduct(product));
            productRepositoryMock.Verify(repository => repository.Save());
        }

        [Theory]
        [InlineData(0)]
        //[InlineData(100)]
        //[InlineData(23152)]

        public void AddFromFavorites_ValidCall(int id)
        {
            var product = new Product();
            var productRepositoryMock = new Mock<IProductRepository>();
            productRepositoryMock.Setup(repository => repository.GetProductById(id)).Returns(product);
            var dietPlanRepositoryMock = new Mock<IDietPlanRepository>();
            var productInPlanServiceMock = new Mock<IProductInPlanService>();
            var sut = new ProductService(productRepositoryMock.Object, dietPlanRepositoryMock.Object, productInPlanServiceMock.Object);

            sut.AddToFavorites(product);

            product.IsFavourite.Should().BeTrue();
            productRepositoryMock.Verify(repository => repository.UpdateProduct(product));
            productRepositoryMock.Verify(repository => repository.Save());
        }
    }
}
