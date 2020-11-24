using BLL;
using BLL.Db_Models;
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
        public void LoadProducts_GetProductsCalledInRepositoryAndReturnsListOfProducts()
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
        public void GetProductById_GetProductByIdCalledInRepositoryAndReturnsProduct(int id)
        {
            // arrange
            var productRepositoryMock = new Mock<IProductRepository>();
            productRepositoryMock.Setup(repository => repository.GetProductById(id)).Returns(new Product{ProductId = id});
            var dietPlanRepositoryMock = new Mock<IDietPlanRepository>();
            var productInPlanServiceMock = new Mock<IProductInPlanService>();
            var sut = new ProductService(productRepositoryMock.Object, dietPlanRepositoryMock.Object, productInPlanServiceMock.Object);

            sut.GetById(id).Should().BeOfType(typeof(Product));
        }

        [Theory]
        [InlineData(0)]
        [InlineData(100)]
        [InlineData(23152)]
        public void DeleteProductById_DeleteProductCalledInRepository(int id)
        {
            var product = new Product{ProductId = id};
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
        public void CreateProduct_InsertProductCalledInRepository_NewObjectTypeOfProductCreated()
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
        public void Update_UpdateProductCalledInRepository(int id)
        {
            var product = new Product{ProductId = id};
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
        [InlineData(100)]
        [InlineData(23152)]

        public void DeleteFromFavorites_ProductIsFavouriteIsSetToFalse_UpdateProductCalledInRepository_SaveCalledInRepository(int id)
        {
            var product = new Product {ProductId = id, IsFavourite = true};
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
        [InlineData(100)]
        [InlineData(23152)]

        public void AddtoFavorites_ProductIsFavouriteIsSetToTrue_UpdateProductCalledInRepository_SaveCalledInRepositor(int id)
        {
            var product = new Product {ProductId = id};
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
        [Theory]
        [InlineData("Item1")]
        [InlineData("xxx")]
        [InlineData("Item321314")]
        public void SearchByName_ProductIsReturned(string name)
        {

            var ExpectedProduct = new Product { ProductName = name };
            var productRepositoryMock = new Mock<IProductRepository>();
            productRepositoryMock.Setup(repository => repository.GetProducts())
                .Returns(new List<Product> { ExpectedProduct });
            var dietPlanRepositoryMock = new Mock<IDietPlanRepository>();
            var productInPlanServiceMock = new Mock<IProductInPlanService>();
            var sut = new ProductService(productRepositoryMock.Object, dietPlanRepositoryMock.Object, productInPlanServiceMock.Object);

            var actual = sut.SearchByName(name);

            productRepositoryMock.Verify(repository => repository.GetProducts());
            actual.Should().Equal(ExpectedProduct);
        }

        [Theory]
        [InlineData(0, 15)]
        [InlineData(10, 25)]
        [InlineData(15, 20)]
        public void SearchByFat_OneProductInRange_ProductIsReturned(double minfat, double maxfat)
        {

            var ExpectedProduct = new Product { Fat = 15 };
            var productRepositoryMock = new Mock<IProductRepository>();
            productRepositoryMock.Setup(repository => repository.GetProducts())
                .Returns(new List<Product> { ExpectedProduct });
            var dietPlanRepositoryMock = new Mock<IDietPlanRepository>();
            var productInPlanServiceMock = new Mock<IProductInPlanService>();
            var sut = new ProductService(productRepositoryMock.Object, dietPlanRepositoryMock.Object, productInPlanServiceMock.Object);


            var actual = sut.SearchByFat(minfat, maxfat);

            productRepositoryMock.Verify(repository => repository.GetProducts());
            actual.Should().Equal(ExpectedProduct);
        }

        [Theory]
        [InlineData(50, 250)]
        [InlineData(100, 300)]
        [InlineData(0, 500)]
        public void SearchByCalories_OneProductInRange_ProductIsReturned(double minenergy, double maxenergy)
        {

            var ExpectedProduct = new Product { Energy = 200 };
            var productRepositoryMock = new Mock<IProductRepository>();
            productRepositoryMock.Setup(repository => repository.GetProducts())
                .Returns(new List<Product> { ExpectedProduct });
            var dietPlanRepositoryMock = new Mock<IDietPlanRepository>();
            var productInPlanServiceMock = new Mock<IProductInPlanService>();
            var sut = new ProductService(productRepositoryMock.Object, dietPlanRepositoryMock.Object, productInPlanServiceMock.Object);


            var actual = sut.SearchByCalories(minenergy, maxenergy);

            productRepositoryMock.Verify(repository => repository.GetProducts());
            actual.Should().Equal(ExpectedProduct);
        }

        [Theory]
        [InlineData(5, 25)]
        [InlineData(10, 30)]
        [InlineData(0, 15)]
        public void SearchByCarbohydrates_OneProductInRange_ProductIsReturned(double mincarb, double maxcarb)
        {

            var ExpectedProduct = new Product { Carbohydrates = 15 };
            var productRepositoryMock = new Mock<IProductRepository>();
            productRepositoryMock.Setup(repository => repository.GetProducts())
                .Returns(new List<Product> { ExpectedProduct });
            var dietPlanRepositoryMock = new Mock<IDietPlanRepository>();
            var productInPlanServiceMock = new Mock<IProductInPlanService>();
            var sut = new ProductService(productRepositoryMock.Object, dietPlanRepositoryMock.Object, productInPlanServiceMock.Object);


            var actual = sut.SearchByCarbohydrates(mincarb, maxcarb);

            productRepositoryMock.Verify(repository => repository.GetProducts());
            actual.Should().Equal(ExpectedProduct);
        }

        [Theory]
        [InlineData(5, 25)]
        [InlineData(10, 30)]
        [InlineData(0, 15)]
        public void SearchByProteins_OneProductInRange_ProductIsReturned(double minprotein, double maxprotein)
        {

            var ExpectedProduct = new Product { Protein = 15 };
            var productRepositoryMock = new Mock<IProductRepository>();
            productRepositoryMock.Setup(repository => repository.GetProducts())
                .Returns(new List<Product> { ExpectedProduct });
            var dietPlanRepositoryMock = new Mock<IDietPlanRepository>();
            var productInPlanServiceMock = new Mock<IProductInPlanService>();
            var sut = new ProductService(productRepositoryMock.Object, dietPlanRepositoryMock.Object, productInPlanServiceMock.Object);


            var actual = sut.SearchByProteins(minprotein, maxprotein);

            productRepositoryMock.Verify(repository => repository.GetProducts());
            actual.Should().Equal(ExpectedProduct);
        }

        [Fact]
        public void DeleteFromExistingPlan()
        {
            var testProduct = new Product(){ProductName = "Product1", Energy = 50, ProductId = 1};
            var dietPlan = new DietPlan(){Id = 1};
            var productInDietPlan = new ProductInDietPlan(){Product = testProduct, Id = testProduct.ProductId};
            var productListForDay = new List<ProductInDietPlan>();
            productListForDay.Add(productInDietPlan);
            var dailyDietPlan = new DailyDietPlan(){ProductListForDay = productListForDay};
            var dailyDietPlanList = new List<DailyDietPlan>(){dailyDietPlan};
            dietPlan.DailyDietPlanList = dailyDietPlanList;
            var listOfDietPlans = new List<DietPlan>(){dietPlan};
            var productFromDB = new ProductInDietPlanDb() {DailyDietPlanId = dietPlan.Id, ProductId = testProduct.ProductId};
            var productRepositoryMock = new Mock<IProductRepository>();
            productRepositoryMock.Setup(repository => repository.GetProductById(1)).Returns(testProduct);
            var productInPlanServiceMock = new Mock<IProductInPlanService>();
            var dietPlanRepositoryMock = new Mock<IDietPlanRepository>();
            dietPlanRepositoryMock.Setup(repository => repository.ListAllDietPlans())
                .Returns(listOfDietPlans);
            dietPlanRepositoryMock.Setup(repository => repository.ListDailyDietPlans(1)).Returns(dailyDietPlanList);
            dietPlanRepositoryMock.Setup(repository => repository.ListDbProductsInDailyDietPlan(dailyDietPlan)).Returns(new List<ProductInDietPlanDb>(){productFromDB});

            var sut = new ProductService(productRepositoryMock.Object, dietPlanRepositoryMock.Object, productInPlanServiceMock.Object);

            sut.DeleteById(testProduct.ProductId);

            dietPlanRepositoryMock.Verify(repository => repository.DeleteProductInPlan(productFromDB));

        }
    }
}
