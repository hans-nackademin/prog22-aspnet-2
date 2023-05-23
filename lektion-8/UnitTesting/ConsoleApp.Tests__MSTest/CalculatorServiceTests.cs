using ConsoleApp.Services;
using Microsoft.Extensions.DependencyInjection;

namespace ConsoleApp.Tests__MSTest
{
    [TestClass]
    public class CalculatorServiceTests
    {
        private CalculatorService? _calculatorService;

        [TestInitialize]
        public void Setup()
        {
            var services = new ServiceCollection();
            services.AddSingleton<CalculatorService>();

            var serviceProvider = services.BuildServiceProvider();
            _calculatorService = serviceProvider.GetRequiredService<CalculatorService>();
        }



        [TestMethod]
        public void Add__Should_Add_OneNumber_To_Total()
        {
            // Arrange - f�rberedelser
            CalculatorService calculatorService = new CalculatorService();
            calculatorService.Total = 0;


            // Act - utf�randet
            calculatorService.Add(0.1m);


            // Assert - test av resultatet
            Assert.AreEqual(0.1m, calculatorService.Total);

        }

        [TestMethod]
        public void Add__Should_Add_OneNumber_To_Total_Using_DI()
        {
            // Arrange - f�rberedelser
            _calculatorService!.Total = 0;


            // Act - utf�randet
            _calculatorService!.Add(2);


            // Assert - test av resultatet
            Assert.AreEqual(2, _calculatorService.Total);

        }

    }
}