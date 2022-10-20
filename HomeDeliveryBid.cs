using System;
namespace AuctionHouse
{
    /// <summary>
    /// Bid with home delivery selected.
    /// </summary>
    public class HomeDeliveryBid : Bid
    {
        static private decimal saleCharge = 20M;
        static private decimal saleTaxExtraCharge = 5M;

        /// <summary>
        /// Creates a new home delivery bid.
        /// </summary>
        /// <param name="bidPrice">Price of the bid.</param>
        /// <param name="bidder">Instance of the client who placed the bid.</param>
        public HomeDeliveryBid(decimal bidPrice, Client bidder) : base(bidPrice, bidder)
        {
        }

        /// <summary>
        /// Gets the sale charge applicable for the bid.
        /// </summary>
        public override decimal SaleCharge
        {
            get
            {
                return saleCharge;
            }
        }

        /// <summary>
        /// Calculates the sale tax applicable for the bid.
        /// </summary>
        public override decimal SaleTax
        {
            get
            {
                return BidPrice * (decimal)taxRate + saleTaxExtraCharge;
            }
        }
    }
}
