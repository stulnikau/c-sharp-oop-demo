using System;
using System.Collections.Generic;

namespace AuctionHouse
{
    /// <summary>
    /// Collection of static methods that run the auction house.
    /// </summary>
    class Program
    {
        static readonly AuctionHouse auctionHouse = new AuctionHouse();
        static Client currentClient;

        /// <summary>
        /// Creates a new client in the auction house using details provided by the user.
        /// </summary>
        static void AddClient()
        {
            Client newClient = new Client();

            try
            {
                newClient.Name = UserInterface.GetInput("Full name");
                newClient.Email = UserInterface.GetInput("Email");
                newClient.Password = UserInterface.GetPassword("Password");
                newClient.Address = UserInterface.GetInput("Address");

                auctionHouse.AddClient(newClient);
                UserInterface.Message(newClient.Name + " registered successfully.");
            }
            catch (ArgumentException e)
            {
                UserInterface.Error("Registration unsuccessful. " + e.Message);
            }
        }

        /// <summary>
        /// Adds a new product to the auction house using details provided by the user.
        /// </summary>
        static void AddProduct()
        {
            try
            {
                Product newProduct = new Product((decimal)UserInterface.GetDouble("Initial bid"), currentClient);
                newProduct.Type = UserInterface.GetInput("Type");
                newProduct.Name = UserInterface.GetInput("Description");

                auctionHouse.AddProduct(newProduct);
                UserInterface.Message(newProduct + " registered successfully.");
            }
            catch (ArgumentException e)
            {
                UserInterface.Error("Product not registered. " + e.Message);
            }
        }

        /// <summary>
        /// List the products currently advertised by the current client.
        /// </summary>
        /// <returns>Returns null if makeSelection is false. Otherwise, returns
        /// the selected product. Throws a NullReferenceException if no products were found.</returns>
        static Product ListClientProducts(bool makeSelection)
        {
            try
            {
                List<Product> clientProducts = auctionHouse.GetProducts(currentClient);
                if (makeSelection)
                {
                    int itemNum = UserInterface.GetOption("Please select one of the following", clientProducts.ToArray());
                    return clientProducts[itemNum];
                }
                else
                {
                    UserInterface.DisplayOptions($"Items owned by {currentClient.Name}", clientProducts.ToArray());
                    return null;
                }
            }
            catch (NullReferenceException)
            {
                throw;
            }
        }

        /// <summary>
        /// Lists products with types matching the search term provided by the client.
        /// </summary>
        /// <returns>Returns null if makeSelection is false. Otherwise, returns
        /// the selected product. Throws a NullReferenceException if no products were found.</returns>
        static Product SearchProducts(bool makeSelection)
        {
            string productType = UserInterface.GetInput("Type");
            try
            {
                List<Product> foundProducts = auctionHouse.GetProducts(productType);
                if (makeSelection)
                {
                    int itemNum = UserInterface.GetOption("Please select one of the following", foundProducts.ToArray());
                    return foundProducts[itemNum];
                }
                else
                {
                    UserInterface.DisplayOptions("Items found", foundProducts.ToArray());
                    return null;
                }
            }
            catch (NullReferenceException)
            {
                throw;
            }
        }

        /// <summary>
        /// Places a bid on a product selected by the client. If the new bid is
        /// higher than the current price, updates the price of the product to
        /// the new bid price and assigns the current client as the highest bidder
        /// for the product.
        /// </summary>
        static void PlaceBid()
        {
            try
            {
                Product selectedProduct = SearchProducts(makeSelection: true);
                decimal newBid = (decimal)UserInterface.GetDouble($"Enter bid ($). Current highest bid: {selectedProduct.Price:C2}");
                bool homeDelivery = UserInterface.GetBool("Home delivery:");
                selectedProduct.Bid(newBid, currentClient, homeDelivery);
                UserInterface.Message(selectedProduct.LatestBid);
            }
            catch (NullReferenceException e) // No products found
            {
                UserInterface.Message(e.Message);
            }
            catch (ArgumentOutOfRangeException e) // Bid price is invalid
            {
                UserInterface.Error("Bid unsuccessful. " + e.Message);
            }
        }

        /// <summary>
        /// Sells an item selected by the client to the highest current bidder.
        /// </summary>
        static void SellProduct()
        {
            try
            {
                Product selectedProduct = ListClientProducts(makeSelection: true);
                if (auctionHouse.RemoveProduct(selectedProduct))
                {
                    UserInterface.Message($"{selectedProduct} sold to {selectedProduct.LatestBid.Bidder} for {selectedProduct.LatestBid.BidPrice}");
                }
            }
            catch (NullReferenceException e) // No products found
            {
                UserInterface.Message(e.Message);
            }
            catch (InvalidOperationException e) // No bids have been placed yet on the product
            {
                UserInterface.Message("Cannot sell item. " + e.Message);
            }
        }

        /// <summary>
        /// List all of the bids made on a selected product advertised by the client.
        /// </summary>
        static void ListBids()
        {
            try
            {
                Product selectedProduct = ListClientProducts(makeSelection: true);
                if (selectedProduct.HasBids)
                {
                    List<Bid> productBids = selectedProduct.BidLog;
                    UserInterface.DisplayOptions($"Bids received for {selectedProduct}:", productBids.ToArray());
                }
                else
                {
                    UserInterface.Message("No bids have been placed yet.");
                }
            }
            catch (NullReferenceException e) // No products have been advertised yet
            {
                UserInterface.Message(e.Message);
            }
        }

        /// <summary>
        /// Sets the current client to the one that matches the provided credentials
        /// </summary>
        static void Login()
        {
            string email = UserInterface.GetInput("Email");
            string password = UserInterface.GetPassword("Password");

            try
            {
                currentClient = auctionHouse.GetClient(email, password);
                UserInterface.Message("Welcome " + currentClient);
                ShowLoggedInMenu();
            }
            catch (ArgumentOutOfRangeException e)
            {
                UserInterface.Error(e.Message);
            }
        }

        /// <summary>
        /// Displays a logout message.
        /// </summary>
        static void Logout()
        {
            UserInterface.Message(currentClient.Name + " logged out");
        }

        /// <summary>
        /// Shows the level 1 menu of the program before a client has logged in.
        /// Runs an action based on client's selection.
        /// </summary>
        static void ShowStartupMenu()
        {
            bool showMenu = true;
            string menuPrompt = "Please select one of the following";
            string[] menuOptions = { "Register as a new client", "Log in as an existing client", "Exit" };

            const int ADD_CLIENT = 0, LOGIN = 1, EXIT = 2;

            while (showMenu)
            {
                switch (UserInterface.GetOption(menuPrompt, menuOptions))
                {
                    case ADD_CLIENT: { AddClient(); break; }
                    case LOGIN: { Login(); break; }
                    case EXIT: { showMenu = false; break; }
                }
            }
        }

        /// <summary>
        /// Shows the level 2 menu of the program after a client has logged in.
        /// Runs an action based on client's selection.
        /// </summary>
        static void ShowLoggedInMenu()
        {
            bool showMenu = true;
            string menuPrompt = "Please select one of the following";
            string[] menuOptions = {
                "Register item for sale",
                "List my items",
                "Search items",
                "Place a bid on an item",
                "List bids received for my items",
                "Sell one of my items to the highest bidder",
                "Log out"
            };

            const int ADD_PRODUCT = 0,
                      LIST_ITEMS = 1,
                      SEARCH_ITEMS = 2,
                      PLACE_BID = 3,
                      LIST_BIDS = 4,
                      SELL_ITEM = 5,
                      LOG_OUT = 6;

            while (showMenu)
            {
                switch (UserInterface.GetOption(menuPrompt, menuOptions))
                {
                    case ADD_PRODUCT: { AddProduct(); break; }
                    case LIST_ITEMS: { ListClientProducts(makeSelection: false); break; }
                    case SEARCH_ITEMS: { SearchProducts(makeSelection: false); break; }
                    case PLACE_BID: { PlaceBid(); break; }
                    case LIST_BIDS: { ListBids(); break; }
                    case SELL_ITEM: { SellProduct(); break; }
                    case LOG_OUT: { Logout(); showMenu = false; break; }
                }
            }
        }

        /// <summary>
        /// Entry point of the program. Displays a greeting and starts the initial client menu.
        /// </summary>
        static void Main()
        {
            UserInterface.Message("Welcome to Auction House!");
            ShowStartupMenu();
        }
    }
}
