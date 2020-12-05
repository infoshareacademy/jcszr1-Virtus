using System;
using System.Collections.Generic;
using System.Text;
using BLL;
using FluentAssertions;
using Moq;
using VirtusFitWeb.DAL;
using VirtusFitWeb.Services;
using Xunit;

namespace UnitTests.Services
{
    public class DietPlanServiceTests
    {
        [Fact]
        public void ListAllDietPlans_IsOfTypeDietPlanList()
        {
            var dietPlanRepositoryMock = new Mock<IDietPlanRepository>();
            dietPlanRepositoryMock.Setup(repository => repository.ListAllDietPlans()).Returns(new List<DietPlan>());

            var sut = new DietPlanService(dietPlanRepositoryMock.Object);

            sut.ListAllDietPlans().Should().BeOfType(typeof(List<DietPlan>));
        }

        [Fact]
        public void ListAllDietPlans_CallsListAllDietPlans()
        {
            var dietPlanRepositoryMock = new Mock<IDietPlanRepository>();
            dietPlanRepositoryMock.Setup(repository => repository.ListAllDietPlans()).Returns(new List<DietPlan>());

            var sut = new DietPlanService(dietPlanRepositoryMock.Object);

            sut.ListAllDietPlans();

            dietPlanRepositoryMock.Verify(repository => repository.ListAllDietPlans());
        }

        [Theory]
        [InlineData(10)]
        public void ListAllDietPlans_IsListContainingCorrectNumberOfElements(int dietPlansCount)
        {
            var listOfThreePlans = new List<DietPlan>();
            for (int i = 0; i < dietPlansCount; i++)
            {
                listOfThreePlans.Add(new DietPlan());
            }

            var dietPlanRepositoryMock = new Mock<IDietPlanRepository>();
            dietPlanRepositoryMock.Setup(repository => repository.ListAllDietPlans()).Returns(listOfThreePlans);

            var sut = new DietPlanService(dietPlanRepositoryMock.Object);

            sut.ListAllDietPlans().Should().HaveCount(dietPlansCount);

        }


        [Theory]
        [InlineData(1)]
        [InlineData(25)]
        [InlineData(943233)]
        public void GetDietPlan_IsOfTypeDietPlan(int id)
        {
            var dietPlan = new DietPlan { Id = id };
            var dietPlanRepositoryMock = new Mock<IDietPlanRepository>();
            dietPlanRepositoryMock.Setup(repository => repository.GetDietPlanById(id)).Returns(dietPlan);

            var sut = new DietPlanService(dietPlanRepositoryMock.Object);

            sut.GetDietPlan(id).Should().BeOfType(typeof(DietPlan));

        }

        [Theory]
        [InlineData(1)]
        [InlineData(25)]
        [InlineData(943233)]
        public void GetDietPlan_IsResultIdValid(int id)
        {
            var dietPlan = new DietPlan { Id = id };
            var dietPlanRepositoryMock = new Mock<IDietPlanRepository>();
            dietPlanRepositoryMock.Setup(repository => repository.GetDietPlanById(id)).Returns(dietPlan);

            var sut = new DietPlanService(dietPlanRepositoryMock.Object);

            sut.GetDietPlan(id).Should().Match(dietPlan => ((DietPlan)dietPlan).Id == id);

        }

        [Theory]
        [InlineData(1)]
        [InlineData(25)]
        [InlineData(943233)]
        public void GetDietPlan_CallsGetDietPlanById(int id)
        {
            var dietPlan = new DietPlan { Id = id };
            var dietPlanRepositoryMock = new Mock<IDietPlanRepository>();
            dietPlanRepositoryMock.Setup(repository => repository.GetDietPlanById(id)).Returns(dietPlan);

            var sut = new DietPlanService(dietPlanRepositoryMock.Object);

            sut.GetDietPlan(id);

            dietPlanRepositoryMock.Verify(repository => repository.GetDietPlanById(id));
        }

        [Theory]
        [InlineData(1)]
        [InlineData(25)]
        [InlineData(943233)]
        public void Create_CallsInsertDietPlan(int id)
        {
            var newDietPlan = new DietPlan { Id = id };
            var dietPlanRepositoryMock = new Mock<IDietPlanRepository>();
            //the invocation below was unnecessary
            //dietPlanRepositoryMock.Setup(repository => repository.InsertDietPlan(newDietPlan)); 

            var sut = new DietPlanService(dietPlanRepositoryMock.Object);

            sut.Create(newDietPlan);

            dietPlanRepositoryMock.Verify(repository => repository.InsertDietPlan(newDietPlan));
        }

        [Theory]
        [InlineData(1)]
        [InlineData(25)]
        [InlineData(943233)]
        public void Create_IsResultIdValid(int id)
        {
            var newDietPlan = new DietPlan { Id = id };
            var dietPlanRepositoryMock = new Mock<IDietPlanRepository>();

            var sut = new DietPlanService(dietPlanRepositoryMock.Object);

            sut.Create(newDietPlan).Should().Match(newDietPlan => ((DietPlan)newDietPlan).Id == id);
        }

        [Theory]
        [InlineData(2020, 12, 15, 2020, 12, 20)]
        public void Create_AreGeneratedDailyDietPlansValid(int startYear, int startMonth, int startDay,
            int endYear, int endMonth, int endDay)
        {
             DateTime startDate = new DateTime(startYear, startMonth,startDay);
             DateTime endDate = new DateTime(endYear, endMonth, endDay);
            var newDietPlan = new DietPlan
            {
                Id = 1,
                StartDate = startDate,
                EndDate = endDate,
            };
            var dietPlanRepositoryMock = new Mock<IDietPlanRepository>();

            var sut = new DietPlanService(dietPlanRepositoryMock.Object);

            DietPlan createResult = sut.Create(newDietPlan);
            TimeSpan duration = endDate - startDate + new TimeSpan(1,0,0,0);

            createResult.DailyDietPlanList.Count.Should().Be(duration.Days);

            foreach (var dailyDietPlan in createResult.DailyDietPlanList)
            {
                TimeSpan daysFromStart = new TimeSpan( dailyDietPlan.DayNumber - 1);
                dailyDietPlan.Date.Should().Be(startDate + daysFromStart);
            }
        }
    }
}
