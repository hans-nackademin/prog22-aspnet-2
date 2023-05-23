namespace ConsoleApp.Services
{
    public class CalculatorService
    {
        public decimal Total { get; set; }

        public void Add(decimal value)
        {
            Total += value;
        }
    }
}
