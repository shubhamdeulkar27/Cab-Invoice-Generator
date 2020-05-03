using NUnit.Framework;
using CabInvoiceGenerator;

namespace CabInvoiceGeneratorTest
{
    public class Tests
    {
        InvoiceGenerator invoiceGenerator = null;
        
        /// <summary>
        /// Test Case for Checking Calculate Fare Function.
        /// </summary>
        [Test]
        public void GivenDistanceAndTimeSouldReturnTotalFare()
        {
            invoiceGenerator = new InvoiceGenerator(InvoiceGenerator.Class.NORMAL);
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
            invoiceGenerator = new InvoiceGenerator(InvoiceGenerator.Class.NORMAL);
            double distance = 0.1;
            int time = 1;
            double fare = invoiceGenerator.CalculateFare(distance, time);
            double expected = 5;
            Assert.AreEqual(expected, fare);
        }

        /// <summary>
        /// Test Case For Checking Calculate Fare Function For Multiple Rides Summary.
        /// </summary>
        [Test]
        public void GivenMultipleRidesShouldReturnInvoiceSummary()
        {
            invoiceGenerator = new InvoiceGenerator(InvoiceGenerator.Class.NORMAL);
            Ride[] rides = { new Ride(2.0, 5), new Ride(0.1, 1) };
            InvoiceSummary summary = invoiceGenerator.CalculateFare(rides);
            InvoiceSummary expectedSummary = new InvoiceSummary(2, 30.0);
            Assert.AreEqual(expectedSummary, summary);
        }

        /// <summary>
        /// Test Case for Checking Summary For Specific User.
        /// </summary>
        [Test]
        public void GivenUserIdAndNormalRidesShouldReturnInvoiceSummary()
        {
            invoiceGenerator = new InvoiceGenerator(InvoiceGenerator.Class.NORMAL);
            string userId = "a@b.com";
            Ride[] rides = { new Ride(2.0, 5), new Ride(0.1, 1) };
            invoiceGenerator.AddRides(userId, rides);
            InvoiceSummary summary = invoiceGenerator.GetInvoiceSummary(userId);
            InvoiceSummary expectedSummary = new InvoiceSummary(2, 30.0);
            Assert.AreEqual(expectedSummary, summary);
        }

        /// <summary>
        /// Test Case for Checking Calculate Fare Function.
        /// </summary>
        [Test]
        public void GivenDistanceAndTimeForPremiumSouldReturnTotalFare()
        {
            invoiceGenerator = new InvoiceGenerator(InvoiceGenerator.Class.PREMIUM);
            double distance = 2.0;
            int time = 5;
            double fare = invoiceGenerator.CalculateFare(distance, time);
            double expected = 40;
            Assert.AreEqual(expected, fare);
        }

        /// <summary>
        /// Test Case for Checking Summary For Specific User.
        /// </summary>
        [Test]
        public void GivenUserIdAndPremiumRidesShouldReturnInvoiceSummary()
        {
            invoiceGenerator = new InvoiceGenerator(InvoiceGenerator.Class.PREMIUM);
            string userId = "s@d.com";
            Ride[] rides = {new Ride(2.0,5), new Ride(0.1,1) };
            invoiceGenerator.AddRides(userId, rides);
            InvoiceSummary summary = invoiceGenerator.GetInvoiceSummary(userId);
            InvoiceSummary expectedSummary = new InvoiceSummary(2,60.0);
            Assert.AreEqual(expectedSummary,summary);
        }
    }
}
