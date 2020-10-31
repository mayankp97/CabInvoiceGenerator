using CabInvoiceGenerator;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Reflection.PortableExecutable;
using System.Text;

namespace CabInvoiceGeneratorTests
{

    [TestFixture]
    class InvoiceGeneratorTest
    {
        /*
        private readonly double _normalFarePerKm = 10;
        private readonly double _normalCostPerTime = 1;
        private readonly double _normalMinFare = 5;

        private readonly double _premiumFarePerKm = 15;
        private readonly double _premiumCostPerTime = 2;
        private readonly double _premiumMinFare = 20;
        */




        [Test]
        [TestCase(RideType.NORMAL,21.00)]
        [TestCase(RideType.PREMIUM, 32.00)]
        public void CalculateFare_IfValidParameters_ReturnsFare(RideType rideType, double expectedFare)
        {
            var invoiceGenerator = new InvoiceGenerator(rideType);

            var result = invoiceGenerator.CalculateFare(2.0, 1);

            Assert.That(result, Is.EqualTo(expectedFare));
        }

        [Test]
        public void CalculateFare_IfInvalidDistance_ThrowsException()
        {
            var invoiceGenerator = new InvoiceGenerator();

            var exception = Assert.Throws<CabInvoiceException>(() => invoiceGenerator.CalculateFare(-1.0, 1));
            Assert.That(exception.type, Is.EqualTo(CabInvoiceException.ExceptionType.INVALID_DISTANCE));
        }

        [Test]
        public void CalculateFare_IfInvalidTime_ThrowsException()
        {
            var invoiceGenerator = new InvoiceGenerator();

            var exception = Assert.Throws<CabInvoiceException>(() => invoiceGenerator.CalculateFare(1.0, -1));
            Assert.That(exception.type, Is.EqualTo(CabInvoiceException.ExceptionType.INVALID_TIME));
        }

        [Test]
        [TestCase(RideType.NORMAL, 32.00)]
        [TestCase(RideType.PREMIUM, 52.00)]
        public void CalculateFare_IfMultipleRidesPassed_ReturnsInvoiceSummary(RideType rideType, double expectedFare)
        {
            Ride[] rides = { new Ride(1.0, 1), new Ride(2.0, 1) };
            var invoiceGenerator = new InvoiceGenerator(rideType);

            var result = invoiceGenerator.CalculateFare(rides);
            var expected = new InvoiceSummary(2, expectedFare);

            Assert.That(result, Is.EqualTo(expected));
            
        }
        [Test]
        public void CalculateFare_IfNullRides_ThrowsException()
        {
            var invoiceGenerator = new InvoiceGenerator();

            var exception = Assert.Throws<CabInvoiceException>(() => invoiceGenerator.CalculateFare(null));
            Assert.That(exception.type, Is.EqualTo(CabInvoiceException.ExceptionType.NULL_RIDES));
        }

        [Test]
        [TestCase(RideType.NORMAL, 32.00)]
        [TestCase(RideType.PREMIUM, 52.00)]
        public void GetInvoiceSummary_WhenPassedUserId_ReturnsInvoiceSummary(RideType rideType, double expectedFare)
        {
            Ride[] rides = { new Ride(1.0, 1), new Ride(2.0, 1) };
            var invoiceGenerator = new InvoiceGenerator(rideType);
            var userID = "1";

            invoiceGenerator.AddRides(userID, rides);
            var result = invoiceGenerator.GetInvoiceSummary(userID);
            var expected = new InvoiceSummary(2, expectedFare);

            Assert.That(result, Is.EqualTo(expected));
        }

        [Test]
        public void GetInvoiceSummary_WhenPassedInvalidUserId_ReturnsInvoiceSummary()
        {
            Ride[] rides = { new Ride(1.0, 1), new Ride(2.0, 1) };
            var invoiceGenerator = new InvoiceGenerator(RideType.NORMAL);
            var userID = "1";

            invoiceGenerator.AddRides("2", rides);
            var exception = Assert.Throws<CabInvoiceException>(() => invoiceGenerator.GetInvoiceSummary(userID));
            Assert.That(exception.type, Is.EqualTo(CabInvoiceException.ExceptionType.INVALID_USER_ID));
        }


    }
}
