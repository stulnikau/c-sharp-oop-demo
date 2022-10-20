using System;
namespace AuctionHouse
{
    /// <summary>
    /// Bid with click and collect option selected.
    /// </summary>
    public class ClickAndCollectBid : Bid
    {
        static private decimal saleCharge = 10M;

        /// <summary>
        /// Creates a new home delivery bid.
        /// </summary>
        /// <param name="bidPrice">Price of the bid.</param>
        /// <param name="bidder">Instance of the client who placed the bid.</param>
        public ClickAndCollectBid(decimal bidPrice, Client bidder) : base(bidPrice, bidder)
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
                return BidPrice * (decimal)taxRate;
            }
        }
    }
}
