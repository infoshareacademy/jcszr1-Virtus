using BLL;
using FluentAssertions;
using Moq;
using System.Collections.Generic;
using VirtusFitWeb.DAL;
using VirtusFitWeb.Services;
using Xunit;

namespace UnitTests
{
    public class FavoriteServiceTests
    {
        [Fact]
        public void GetAll_IsOfTypeProductList()
        {
            var productRepositoryMock = new Mock<IProductRepository>();
            productRepositoryMock.Setup(repository => repository.GetProducts("DummyId")).Returns(new List<Product>());

            var sut = new FavoriteService(productRepositoryMock.Object);

            sut.GetAll("DummyId").Should().BeOfType(typeof(List<Product>));
        }

        [Fact]
        public void GetAll_CallsGetProducts()
        {
            var productRepositoryMock = new Mock<IProductRepository>();
            productRepositoryMock.Setup(repository => repository.GetProducts("DummyId")).Returns(new List<Product>());

            var sut = new FavoriteService(productRepositoryMock.Object);

            sut.GetAll("DummyId");

            productRepositoryMock.Verify(repository => repository.GetProducts("DummyId"));
        }

        [Theory]
        [InlineData(1)]
        [InlineData(25)]
        [InlineData(943233)]
        public void AddToFavorites_CallsGetProductById(int id)
        {
            var product = new Product { ProductId = id };
            var productRepositoryMock = new Mock<IProductRepository>();
            productRepositoryMock.Setup(repository => repository.GetProductById(id)).Returns(product);

            var sut = new FavoriteService(productRepositoryMock.Object);

            sut.AddToFavorites(product);

            productRepositoryMock.Verify(repository => repository.GetProductById(id));
        }

        [Theory]
        [InlineData(1)]
        [InlineData(25)]
        [InlineData(943233)]
        public void AddToFavorites_CallsUpdateProduct(int id)
        {
            var product = new Product { ProductId = id };
            var productRepositoryMock = new Mock<IProductRepository>();
            productRepositoryMock.Setup(repository => repository.GetProductById(id)).Returns(product);

            var sut = new FavoriteService(productRepositoryMock.Object);

            sut.AddToFavorites(product);

            productRepositoryMock.Verify(repository => repository.UpdateProduct(product));
        }

        [Theory]
        [InlineData(1)]
        [InlineData(25)]
        [InlineData(943233)]
        public void AddToFavorites_IsAddedToFavorites(int id)
        {
            var product = new Product { ProductId = id, IsFavorite = false };
            var productRepositoryMock = new Mock<IProductRepository>();
            productRepositoryMock.Setup(repository => repository.GetProductById(id)).Returns(product);

            var sut = new FavoriteService(productRepositoryMock.Object);

            sut.AddToFavorites(product);

            product.IsFavorite.Should().BeTrue();
        }

        [Theory]
        [InlineData(1)]
        [InlineData(25)]
        [InlineData(943233)]
        public void GetById_CallsGetProducts(int id)
        {
            var product = new Product { ProductId = id };
            var list = new List<Product> { product };
            var productRepositoryMock = new Mock<IProductRepository>();
            productRepositoryMock.Setup(repository => repository.GetProducts("DummyId")).Returns(list);

            var sut = new FavoriteService(productRepositoryMock.Object);

            sut.GetById(id);

            productRepositoryMock.Verify(repository => repository.GetProducts("DummyId"));
        }

        [Theory]
        [InlineData(1)]
        [InlineData(25)]
        [InlineData(943233)]
        public void GetById_CheckIsNullIfNotFavorite(int id)
        {
            var product = new Product { ProductId = id, IsFavorite = false};
            var list = new List<Product> { product };
            var productRepositoryMock = new Mock<IProductRepository>();
            productRepositoryMock.Setup(repository => repository.GetProducts("DummyId")).Returns(list);

            var sut = new FavoriteService(productRepositoryMock.Object);

            var result = sut.GetById(id);

            result.Should().BeNull();
        }

        [Theory]
        [InlineData(1)]
        [InlineData(25)]
        [InlineData(943233)]
        public void GetById_IsProductIdValid(int id)
        {
            var product = new Product { ProductId = id, IsFavorite = true};
            var list = new List<Product> { product };
            var productRepositoryMock = new Mock<IProductRepository>();
            productRepositoryMock.Setup(repository => repository.GetProducts("DummyId")).Returns(list);

            var sut = new FavoriteService(productRepositoryMock.Object);

            var result =sut.GetById(id);

            result.Should().BeOfType<Product>().And.Match(p => ((Product) p).ProductId == id);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(25)]
        [InlineData(943233)]
        public void GetById_IsResultProductFavorite(int id)
        {
            var product = new Product { ProductId = id, IsFavorite = true};
            var list = new List<Product> { product };
            var productRepositoryMock = new Mock<IProductRepository>();
            productRepositoryMock.Setup(repository => repository.GetProducts("DummyId")).Returns(list);

            var sut = new FavoriteService(productRepositoryMock.Object);

            var result = sut.GetById(id);

            result.Should().BeOfType<Product>().And.Match(p=> ((Product)p).IsFavorite);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(25)]
        [InlineData(943233)]
        public void DeleteFromFavorites_CallsGetProductById(int id)
        {
            var product = new Product { ProductId = id };
            var productRepositoryMock = new Mock<IProductRepository>();
            productRepositoryMock.Setup(repository => repository.GetProductById(id)).Returns(product);

            var sut = new FavoriteService(productRepositoryMock.Object);

            sut.DeleteFromFavorites(product);

            productRepositoryMock.Verify(repository => repository.GetProductById(id));
        }

        [Theory]
        [InlineData(1)]
        [InlineData(25)]
        [InlineData(943233)]
        public void DeleteFromFavorites_CallsUpdateProduct(int id)
        {
            var product = new Product { ProductId = id };
            var productRepositoryMock = new Mock<IProductRepository>();
            productRepositoryMock.Setup(repository => repository.GetProductById(id)).Returns(product);

            var sut = new FavoriteService(productRepositoryMock.Object);

            sut.DeleteFromFavorites(product);

            productRepositoryMock.Verify(repository => repository.UpdateProduct(product));
        }

        [Theory]
        [InlineData(1)]
        [InlineData(25)]
        [InlineData(943233)]
        public void DeleteFromFavorites_IsRemovedFromFavorites(int id)
        {
            var product = new Product { ProductId = id, IsFavorite = true };
            var productRepositoryMock = new Mock<IProductRepository>();
            productRepositoryMock.Setup(repository => repository.GetProductById(id)).Returns(product);

            var sut = new FavoriteService(productRepositoryMock.Object);

            sut.DeleteFromFavorites(product);

            product.IsFavorite.Should().BeFalse();
        }

    }
}