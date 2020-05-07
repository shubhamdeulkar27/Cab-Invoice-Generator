using NUnit.Framework;
using CabInvoiceGenerator;

namespace CabInvoiceGeneratorTest
{
    /// <summary>
    /// Class For Test Cases.
    /// </summary>
    public class Tests
    {
        //InvoiceGenerator Reference.
        InvoiceGenerator invoiceGenerator = null;
        
        /// <summary>
        /// Test Case for Checking Calculate Fare Function.
        /// </summary>
        [Test]
        public void GivenDistanceAndTimeSouldReturnTotalFare()
        {
            //Creating Instance of InvoiceGenerator For Normal Ride.
            invoiceGenerator = new InvoiceGenerator(RideType.NORMAL);
            double distance = 2.0;
            int time = 5;

            //Calculating Fare.
            double fare = invoiceGenerator.CalculateFare(distance, time);
            double expected = 25;

            //Asserting Values.
            Assert.AreEqual(expected, fare);
        }

        /// <summary>
        /// Given Invalid RideType Should Throw Custom Exception.
        /// </summary>
        [Test]
        public void GivenInvalidRideTypeShouldThrowCustomeException()
        {
            //Creating Instance of InvoiceGenerator.
            invoiceGenerator = new InvoiceGenerator();
            double distance = 2.0;
            int time = 5;
            string expected = "Invalid Ride Type";
            try
            {
                //Calculating Fare.
                double fare = invoiceGenerator.CalculateFare(distance, time);
            }
            catch (CabInvoiceException exception)
            {
                //Assertig Values.
                Assert.AreEqual(expected, exception);
            }
        }

        /// <summary>
        /// Given Invalid Distance Should Throw Custom Exception.
        /// </summary>
        [Test]
        public void GivenInvalidDistanceShouldThrowCustomException()
        {
            //Creating Instance of InvoiceGenerator For Normal Ride.
            invoiceGenerator = new InvoiceGenerator(RideType.NORMAL);
            double distance = -2.0;
            int time = 5;
            string expected = "Invalid Distance";
            try
            {
                //Calculating Fare.
                double fare = invoiceGenerator.CalculateFare(distance, time);
            }
            catch (CabInvoiceException exception)
            {
                //Asserting Values.
                Assert.AreEqual(expected,exception.Message);
            }
            
        }

        /// <summary>
        /// Given Invalid Time Should Throw Custom Exception.
        /// </summary>
        [Test]
        public void GivenInvalidTimeShouldThrowCustomException()
        {
            //Creating Instance of InvoiceGenerator For Normal Ride.
            invoiceGenerator = new InvoiceGenerator(RideType.NORMAL);
            double distance = 2.0;
            int time = -46;
            string expected = "Invalid Time";
            try
            {
                //Calculating Fare.
                double fare = invoiceGenerator.CalculateFare(distance, time);
            }
            catch (CabInvoiceException exception)
            {
                //Asserting Values.
                Assert.AreEqual(expected,exception.Message);
            }
        }

        /// <summary>
        /// Test Case For Checking Calculate Fare Function For Minimum Time And Distance.
        /// </summary>
        [Test]
        public void GivenLessDistanceOrTimeShouldReturnMinimumFare()
        {
            //Creating Instance of InvoiceGenerator For Normal Ride.
            invoiceGenerator = new InvoiceGenerator(RideType.NORMAL);
            double distance = 0.1;
            int time = 1;

            //Calculating Fare.
            double fare = invoiceGenerator.CalculateFare(distance, time);
            double expected = 5;

            //Asserting Values.
            Assert.AreEqual(expected, fare);
        }

        /// <summary>
        /// Test Case For Checking Calculate Fare Function For Multiple Rides Summary.
        /// </summary>
        [Test]
        public void GivenMultipleRidesShouldReturnInvoiceSummary()
        {
            //Creating Instance of InvoiceGenerator For Normal Ride.
            invoiceGenerator = new InvoiceGenerator(RideType.NORMAL);
            Ride[] rides = { new Ride(2.0, 5), new Ride(0.1, 1) };

            //Generating Summary For Rides.
            InvoiceSummary summary = invoiceGenerator.CalculateFare(rides);
            InvoiceSummary expectedSummary = new InvoiceSummary(2, 30.0);

            //Asserting Values.
            Assert.AreEqual(expectedSummary, summary);
        }

        /// <summary>
        /// Given Invalid Rides Should Throw Custom Exception.
        /// </summary>
        [Test]
        public void GivenInvalidMultipleRidesShouldThrowCustomeException()
        {
            //Creating Instance of InvoiceGenerator For Normal Ride.
            invoiceGenerator = new InvoiceGenerator(RideType.NORMAL);
            Ride[] rides = { };
            string expected = "Rides Are Null";
            try
            {
                //Generating Summary For Rides.
                InvoiceSummary summary = invoiceGenerator.CalculateFare(rides);
                InvoiceSummary expectedSummary = new InvoiceSummary(2, 30.0);
            }
            catch (CabInvoiceException exception)
            {
                //Asserting Values.
                Assert.AreEqual(expected,exception.Message);
            }
        }

        /// <summary>
        /// Test Case for Checking Summary For Specific User.
        /// </summary>
        [Test]
        public void GivenUserIdAndNormalRidesShouldReturnInvoiceSummary()
        {
            //Creating Instance of InvoiceGenerator For Normal Ride.
            invoiceGenerator = new InvoiceGenerator(RideType.NORMAL);
            string userId = "a@b.com";
            Ride[] rides = { new Ride(2.0, 5), new Ride(0.1, 1) };
            invoiceGenerator.AddRides(userId, rides);

            //Generating Summary For Rides.
            InvoiceSummary summary = invoiceGenerator.GetInvoiceSummary(userId);
            InvoiceSummary expectedSummary = new InvoiceSummary(2, 30.0);

            //Asserting Values.
            Assert.AreEqual(expectedSummary, summary);
        }

        /// <summary>
        /// Given Invalid Or Empty Rides Should Throw Custom Exception.
        /// </summary>
        [Test]
        public void GivenEmptyNormalRidesShouldThrowCustomException()
        {
            //Creating Instance of InvoiceGenerator For Normal Ride.
            invoiceGenerator = new InvoiceGenerator(RideType.NORMAL);
            string userId = "a@b.com";
            Ride[] rides = {};
            string expected = "Rides Are Null";
            try
            {
                //Adding Rides For Specified UserId.
                invoiceGenerator.AddRides(userId, rides);

                //Generating Summary For Specified UserId Rides.
                InvoiceSummary summary = invoiceGenerator.GetInvoiceSummary(userId);
            }
            catch (CabInvoiceException exception)
            {
                //Asserting Values.
                Assert.AreEqual(expected,exception.Message);
            }
        }

        /// <summary>
        /// Given Invalid UserId Should Throw Custom Exception.
        /// </summary>
        [Test]
        public void GivenInvalidUserIdShouldThrowCustomException()
        {
            //Creating Instance of InvoiceGenerator For Normal Ride.
            invoiceGenerator = new InvoiceGenerator(RideType.NORMAL);
            string userId = "a@b.com";
            Ride[] rides = { new Ride(2.0, 5), new Ride(0.1, 1) };
            string expected = "Invalid UserID";
            try
            {
                //Adding Rides For Specified UserId.
                invoiceGenerator.AddRides(userId, rides);

                //Generating Summary For Specified UserId Rides.
                InvoiceSummary summary = invoiceGenerator.GetInvoiceSummary("b@d.com");
                InvoiceSummary expectedSummary = new InvoiceSummary(2, 30.0);
            }
            catch (CabInvoiceException exception)
            {
                //Asserting Values.
                Assert.AreEqual(expected,exception.Message);
            }
        }

        /// <summary>
        /// Test Case for Checking Calculate Fare Function.
        /// </summary>
        [Test]
        public void GivenDistanceAndTimeForPremiumSouldReturnTotalFare()
        {
            //Creating Instance of InvoiceGenerator For Premium Ride.
            invoiceGenerator = new InvoiceGenerator(RideType.PREMIUM);
            double distance = 2.0;
            int time = 5;

            //Calculating Fare.
            double fare = invoiceGenerator.CalculateFare(distance, time);
            double expected = 40;

            //Asserting Values.
            Assert.AreEqual(expected, fare);
        }

        /// <summary>
        /// Test Case for Checking Summary For Specific User.
        /// </summary>
        [Test]
        public void GivenUserIdAndPremiumRidesShouldReturnInvoiceSummary()
        {
            //Creating Instance of InvoiceGenerator For Premium Ride.
            invoiceGenerator = new InvoiceGenerator(RideType.PREMIUM);
            string userId = "s@d.com";
            Ride[] rides = {new Ride(2.0,5), new Ride(0.1,1) };

            //Adding Rides For Specified UserId.
            invoiceGenerator.AddRides(userId, rides);

            //Generating Summary For Specified UserId Rides.
            InvoiceSummary summary = invoiceGenerator.GetInvoiceSummary(userId);
            InvoiceSummary expectedSummary = new InvoiceSummary(2,60.0);

            //Asserting Values.
            Assert.AreEqual(expectedSummary,summary);
        }
    }
}
