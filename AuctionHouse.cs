using System;
using System.Collections.Generic;

namespace AuctionHouse
{
    /// <summary>
    /// Stores the clients, products and information about an auction house.
    /// </summary>
    public class AuctionHouse
    {
        /// <summary>
        /// List of all clients assigned to the auction house.
        /// </summary>
        private List<Client> clients = new List<Client>();

        /// <summary>
        /// List of all products advertised on the auction house.
        /// </summary>
        private List<Product> products = new List<Product>();

        /// <summary>
        /// Signs up a new client.
        /// </summary>
        /// <param name="newClient">Client instance to be added</param>
        public void AddClient(Client newClient)
        {
            clients.Add(newClient);
        }

        /// <summary>
        /// Adds a new product to the auction house.
        /// </summary>
        /// <param name="newProduct">Instance of a product to be added.</param>
        public void AddProduct(Product newProduct) => products.Add(newProduct);

        /// <summary>
        /// Returns the instance of a client that matches the provided credentials.
        /// Throws an ArgumentOutOfRangeException if a client with the given
        /// credentials is not found.
        /// </summary>
        /// <param name="email">Client's email</param>
        /// <param name="password">Client's password</param>
        /// <returns>Instance of a client matching the credentials.</returns>
        public Client GetClient(string email, string password)
        {
            foreach(Client client in clients)
            {
                if (client.Email == email && client.Password == password) { return client; }
            }
            throw new ArgumentOutOfRangeException("", "Username or password incorrect");
        }

        /// <summary>
        /// Returns a list of all products advertised by a given client.
        /// </summary>
        /// <param name="client">Client instance to search for</param>
        /// <returns>List of products</returns>
        public List<Product> GetProducts(Client client)
        {
            List<Product> clientProducts = new List<Product>();
            foreach(Product product in products)
            {
                if (product.Client == client) { clientProducts.Add(product); }
            }
            if (clientProducts.Count == 0) { throw new NullReferenceException("No items found."); }
            return clientProducts;
        }

        /// <summary>
        /// Returns a list of products that match the provided type.
        /// </summary>
        /// <param name="type">Product type to search for</param>
        /// <returns>List of products</returns>
        public List<Product> GetProducts(string type)
        {
            List<Product> foundProducts = new List<Product>();
            foreach(Product product in products)
            {
                if (product.Type == type) { foundProducts.Add(product); }
            }
            if (foundProducts.Count == 0) { throw new NullReferenceException("No items found."); }
            return foundProducts;
        }

        /// <summary>
        /// Removes a product from the auction house if it is currently advertised.
        /// </summary>
        /// <param name="toBeSold"></param>
        /// <returns>True if product was successfully removed from sale, false if
        /// product was not removed due to already being removed. Throws an InvalidOperationException
        /// if the product has not yet received any bids.</returns>
        public bool RemoveProduct(Product toBeSold)
        {
            if (toBeSold.HasBids)
            {
                return products.Remove(toBeSold);
            }
            else
            {
                throw new InvalidOperationException("No bids have been placed yet.");
            }
        }
    }
}
