using System;

namespace CabInvoiceGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            /*
            Console.WriteLine("Welcome To Cab Invoice Generator");
            Console.WriteLine("================================");
            InvoiceGenerator invoiceGenerator = new InvoiceGenerator(RideType.PREMIUM);
            double fare = invoiceGenerator.CalculateFare(2.0, 5);
            Console.WriteLine($"Fare : {fare}");
            */
            InvoiceGenerator invoiceGenerator = new InvoiceGenerator();
            double fare = invoiceGenerator.CalculateFare(2.0, 5);
        }
    }
}
