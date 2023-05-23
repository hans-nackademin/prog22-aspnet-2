using ConsoleApp.Services;
using Microsoft.Extensions.DependencyInjection;
using System.ComponentModel;

namespace ConsoleApp.Tests__nUnit
{
    public class Tests
    {
        private CalculatorService calculatorService;
        //private CalculatorService _calculatorService;

        [SetUp]
        public void Setup()
        {
            //var container = new Container();
            //container.Register<CalculatorService>();
            //_calculatorService = container.Resolve<CalculatorService>();

            //var services = new ServiceCollection();
            //services.AddSingleton<CalculatorService>();

            //var serviceProvider = services.BuildServiceProvider();
            //_calculatorService = serviceProvider.GetRequiredService<CalculatorService>();
            //_calculatorService.Total = 0;


            // Arrange
            calculatorService = new CalculatorService();
            calculatorService.Total = 0;

        }

        [Test]
        public void Add__Should_Add_OneNumber_To_Total()
        {
            // Act
            calculatorService.Add(1);


            // Assert
            Assert.That(calculatorService.Total, Is.EqualTo(1));
        }


        [TestCase(1, 1, 1, 3)]
        [TestCase(10, 10, 20, 40)]
        [TestCase(0.1, 0.1, 0.1, 0.3)]
        public void Add__Should_Add_Three_Numbers_To_Total(decimal value1, decimal value2, decimal value3, decimal expected)
        {
            // Act
            calculatorService.Add(value1);
            calculatorService.Add(value2);
            calculatorService.Add(value3);

            // Assert
            Assert.That(calculatorService.Total, Is.EqualTo(expected));
        }
    }
}