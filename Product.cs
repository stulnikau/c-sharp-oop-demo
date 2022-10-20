using System;
using System.Collections.Generic;

namespace AuctionHouse
{
    /// <summary>
    /// Holds the name, type and price of a product.
    /// </summary>
    public class Product
    {
        private string name;
        private string type;
        private decimal initialPrice;
        private Client client;
        private List<Bid> bidLog = new List<Bid>();
        private bool hasBids;

        /// <summary>
        /// Creates a new product defined by its initial price and the advertising client.
        /// Throws an ArgumentOutOfRangeException if initial price is negative.
        /// </summary>
        /// <param name="initialPrice">The initial bid price.</param>
        /// <param name="client">The instance of a client that is advertising the product.</param>
        public Product(decimal initialPrice, Client client)
        {
            HasBids = false;
            Client = client;
            if (initialPrice >= 0M)
            {
                this.initialPrice = initialPrice;
            }
            else
            {
                throw new ArgumentOutOfRangeException("", "The initial price must not be negative.");
            }
        }

        /// <summary>
        /// Places a new bid on the product if the new bid is higher than the current highest.
        /// Raises an ArgumentOutOfRangeException if the new bid is lower than the
        /// current price.
        /// </summary>
        /// <param name="bidPrice">The new bid price.</param>
        /// <param name="bidder">Instance of a client that placed the bid.</param>
        public void Bid(decimal bidPrice, Client bidder, bool homeDelivery)
        {
            if (bidPrice > Price)
            {
                if (homeDelivery) bidLog.Add(new HomeDeliveryBid(bidPrice, bidder));
                else bidLog.Add(new ClickAndCollectBid(bidPrice, bidder));
            }
            else
            {
                throw new ArgumentOutOfRangeException("", "The new bid price must be higher than the current highest bid");
            }
        }

        /// <summary>
        /// Displays the type, name and latest price of a product.
        /// </summary>
        /// <returns>Type, name and formatted price of the product</returns>
        public override string ToString()
        {
            return $"{Type}, {Name}: {Price:C2}";
        }

        /// <summary>
        /// Sets or gets a validated product name. Returns an ArgumentException if set to a
        /// zero-length string.
        /// </summary>
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                if (value.Length == 0)
                {
                    throw new ArgumentException("You have not entered a name");
                }
                name = value;
            }
        }

        /// <summary>
        /// Sets or gets a validated product type. Returns an ArgumentException if set to a
        /// zero-length string.
        /// </summary>
        public string Type
        {
            get
            {
                return type;
            }
            set
            {
                if (value.Length == 0)
                {
                    throw new ArgumentException("You have not entered a type");
                }
                type = value;
            }
        }

        public bool HasBids
        {
            get
            {
                if (BidLog.Count > 0) return true;
                return false;
            }
            private set
            {
                hasBids = value;
            }
        }
        /// <summary>
        /// Gets the latest bid for the product, or initial price if no
        /// bids were made.
        /// Returns an ArgumentOutOfRangeException if set to a negative value.
        /// </summary>
        public decimal Price
        {
            get
            {
                if (HasBids)
                {
                    return LatestBid.BidPrice;
                }
                else
                {
                    return initialPrice;
                }
            }
        }

        /// <summary>
        /// Gets the instance of the client that is advertising the product.
        /// </summary>
        public Client Client
        {
            get
            {
                return client;
            }
            private set
            {
                client = value;
            }
        }

        /// <summary>
        /// Gets a list of instances of all bids placed on the product.
        /// </summary>
        public List<Bid> BidLog
        {
            get
            {
                return bidLog;
            }
        }

        /// <summary>
        /// Gets the latest bid placed on the product. Throw 
        /// </summary>
        public Bid LatestBid
        {
            get
            {
                if (!HasBids) { throw new NullReferenceException("No bids have been placed yet."); }
                return BidLog[BidLog.Count - 1];
            }
        }
    }
}
