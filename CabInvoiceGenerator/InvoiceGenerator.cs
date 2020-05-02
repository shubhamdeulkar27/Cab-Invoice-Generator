using System;
using System.Collections.Generic;
using System.Text;

namespace CabInvoiceGenerator
{
    /// <summary>
    /// InvoiceGenerator Class To Generate The Invoice.
    /// </summary>
    public class InvoiceGenerator
    {
        //Constants.
        private static readonly double MINIMUM_COST_PER_KM = 10;
        private static readonly int COST_PER_TIME = 1;
        private static readonly double MINIMUM_FARE = 5;
        private RideRepository rideRepository;

        /// <summary>
        /// Constrcutor To Create RideRepository instance.
        /// </summary>
        public InvoiceGenerator()
        {
            this.rideRepository = new RideRepository();
        }

        /// <summary>
        /// Function to Calculate Fare.
        /// </summary>
        /// <param name="distance"></param>
        /// <param name="time"></param>
        /// <returns></returns>
        public double CalculateFare(double distance, int time)
        {
            double totalFare = distance * MINIMUM_COST_PER_KM + time * COST_PER_TIME;
            return Math.Max(totalFare, MINIMUM_FARE);
        }

        /// <summary>
        /// Function To Calculate Total Fare and Generating Summary For Multiple Rides.
        /// </summary>
        /// <param name="rides"></param>
        /// <returns></returns>
        public InvoiceSummary CalculateFare(Ride[] rides)
        {
            double totalFare = 0;
            foreach (Ride ride in rides)
            {
                totalFare += this.CalculateFare(ride.distance, ride.time);
            }
            return new InvoiceSummary(rides.Length, totalFare);
        }

        /// <summary>
        /// Function to Add Rides For UserId.
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="rides"></param>
        public void AddRides(string userId, Ride[] rides)
        {
            rideRepository.AddRide(userId, rides);
        }

        /// <summary>
        /// Function to Get Summary By UserId.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public InvoiceSummary GetInvoiceSummary(string userId)
        {
            return this.CalculateFare(rideRepository.GetRides(userId));
        }
    }
}
