using ConsoleApp.Services;

namespace ConsoleApp.Tests__xUnit
{
    public class CalculatorServiceTests
    {

        [Fact]
        public void Add__Should_Add_OneNumber_To_Total()
        {
            // Arrange
            CalculatorService calculatorService = new CalculatorService();
            calculatorService.Total = 5;

            // Act
            calculatorService.Add(1);

            // Assert
            Assert.Equal(6, calculatorService.Total);
        }


        [Theory]
        [InlineData(0.1, 0.1, 0.1, 0.3)]
        [InlineData(1, 1, 1, 3)]
        [InlineData(10, 10, 20, 40)]
        public void Add__Should_Add_Three_Numbers_To_Total(decimal value_1, decimal value_2, decimal value_3, decimal expected)
        {
            // Arrange
            CalculatorService calculatorService = new CalculatorService();
            calculatorService.Total = 0;

            // Act
            calculatorService.Add(value_1);
            calculatorService.Add(value_2);
            calculatorService.Add(value_3);

            // Assert
            Assert.Equal(expected, calculatorService.Total);
        }


        public static IEnumerable<object[]> TestData()
        {
            yield return new object[] { 0.3m, new decimal[] { 0.1m, 0.1m, 0.1m } };
            yield return new object[] { 10, new decimal[] { 4, 6 } };
            yield return new object[] { 100, new decimal[] { 10, 10, 20, 60 } };
        }


        [Theory]
        [MemberData(nameof(TestData))]
        public void Add__Should_Add_Multiple_Number_To_Total(decimal expected, params decimal[] values)
        {
            // Arrange
            CalculatorService calculatorService = new CalculatorService();
            calculatorService.Total = 0;

            // Act
            foreach(var value in values)
                calculatorService.Add(value);

            // Assert
            Assert.Equal(expected, calculatorService.Total);
        }
    }
}