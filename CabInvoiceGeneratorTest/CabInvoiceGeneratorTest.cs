using NUnit.Framework;
using CabInvoiceGenerator;

namespace CabInvoiceGeneratorTest
{
    public class Tests
    {
        InvoiceGenerator invoiceGenerator = null;
        [SetUp]
        public void Setup()
        {
            invoiceGenerator = new InvoiceGenerator();
        }

        /// <summary>
        /// Test Case for Checking Calculate Fare Function.
        /// </summary>
        [Test]
        public void GivenDistanceAndTimeSouldReturnTotalFare()
        {
            double distance = 2.0;
            int time = 5;
            double fare = invoiceGenerator.CalculateFare(distance, time);
            double expected = 25;
            Assert.AreEqual(expected, fare);
        }

        /// <summary>
        /// Test Case For Checking Calculate Fare Function For Minimum Time And Distance.
        /// </summary>
        [Test]
        public void GivenLessDistanceOrTimeShouldReturnMinimumFare()
        {
            double distance = 0.1;
            int time = 1;
            double fare = invoiceGenerator.CalculateFare(distance, time);
            double expected = 5;
            Assert.AreEqual(expected, fare);
        }

        /// <summary>
        /// Test Case For Checking Calculate Fare Function For Multiple Rides.
        /// </summary>
        [Test]
        public void GivenMultipleRidesShouldReturnTotalFare()
        {
            Ride[] rides = { new Ride(2.0, 5), new Ride(0.1, 1) };
            double expected = 30;
            double fare = invoiceGenerator.CalculateFare(rides);
            Assert.AreEqual(expected, fare);
        }

    }
}
