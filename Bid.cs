using System;
namespace AuctionHouse
{
    /// <summary>
    /// Holds the bidder's client instance and the price of a placed bid.
    /// </summary>
    public abstract class Bid
    {
        private decimal bidPrice;
        private Client bidder;
        static protected double taxRate = 0.15;

        /// <summary>
        /// Creates a new bid instance.
        /// </summary>
        /// <param name="bidPrice">Price of the bid.</param>
        /// <param name="bidder">Instance of the client who placed the bid.</param>
        public Bid(decimal bidPrice, Client bidder)
        {
            BidPrice = bidPrice;
            Bidder = bidder;
        }

        /// <summary>
        /// Gets the name of the bidder and the bid price of a bid.
        /// </summary>
        /// <returns>String containing the bidder name and the bid price.</returns>
        public override string ToString()
        {
            return $"{Bidder} bid {BidPrice:C2}";
        }

        /// <summary>
        /// Get the sale charge applicable for the bid.
        /// </summary>
        /// <returns>Value of the sale charge</returns>
        public abstract decimal SaleCharge { get; }

        /// <summary>
        /// Get the sale tax applicable for the bid.
        /// </summary>
        public abstract decimal SaleTax { get; }

        /// <summary>
        /// Gets the price of a placed bid
        /// </summary>
        public decimal BidPrice
        {
            get
            {
                return bidPrice;
            }
            private set
            {
                bidPrice = value;
            }
        }

        /// <summary>
        /// Gets the instance of the client that placed the bid.
        /// </summary>
        public Client Bidder
        {
            get
            {
                return bidder;
            }
            private set
            {
                bidder = value;
            }
        }
    }
}
