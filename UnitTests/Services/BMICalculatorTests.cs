using System;
using System.Collections.Generic;
using VirtusFitWeb.Services;
using Xunit;

namespace UnitTests
{
    public class BMICalculatorTests
    {
        private BMICalculatorService _sut;

        [Theory]
        [MemberData(nameof(TestData.Data), MemberType = typeof(TestData))]
        public void CalculateCorrectBmi(double height, double weight, double expected)
        {
            _sut = new BMICalculatorService();

            var actual = Math.Round(_sut.CalculateBMI(height, weight), 2);

            Assert.Equal(expected, actual);
        }

        public class TestData
        {
            public static IEnumerable<object[]> Data =>
            new List<object[]>()
            {
                new object[] {180, 70, 21.60},
                new object[] {200, 100, 25 },
                new object[] {150, 30, 13.33 }
            };
        }


        //[Theory]
        ////[InlineData("x", 4)]
        ////[MemberData(nameof(TestData2.Data), MemberType = typeof(TestData2))]
        //public void ThrowExceptionIfIncorrectFormatOfArgumentProvided(double height, double weight)
        //{
        //    void TryToCalculate()
        //    {
        //        _sut.CalculateBMI(height, weight);
        //    }

        //    var ex = Assert.Throws<ArgumentException>(TryToCalculate);
        //}



        //public class TestData2
        //{
        //    public static IEnumerable<object[]> Data =>
        //        new List<object[]>()
        //        {
        //            new object[] {0, 0},
        //            //new object[] {190, "x"},
        //            //new object[] {"x", "x"}
        //        };
        //}

    }
}
