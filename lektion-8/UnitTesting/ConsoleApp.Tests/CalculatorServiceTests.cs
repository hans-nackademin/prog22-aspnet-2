using ConsoleApp.Services;

namespace ConsoleApp.Tests
{
    [TestClass]
    public class CalculatorServiceTests
    {
        [TestMethod]
        public void Add__Should_Add_OneNumber_To_Total()
        {
            // Arrange - förberedelser
            CalculatorService calculatorService = new CalculatorService();
            calculatorService.Total = 0;

            
            // Act - utförandet
            calculatorService.Add(0.1m);


            // Assert - test av resultatet
            Assert.AreEqual(0.1m, calculatorService.Total);

        }
    }
}