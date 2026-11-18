using System;
using System.Collections.Generic;
using System.Text;

namespace ProductOrderingSystem
{
    public class Address
    {
        private string _streetAddress;
        private string _city;
        private string _stateProvince;
        private string _country;

        public Address(string streetAddress, string city, string stateProvince, string country)
        {
            _streetAddress = streetAddress;
            _city = city;
            _stateProvince = stateProvince;
            _country = country;
        }

        public bool IsInUs()
        {
            string trimmedCountry = _country.Trim();
            return trimmedCountry.Equals("USA", StringComparison.OrdinalIgnoreCase) ||
                   trimmedCountry.Equals("United States", StringComparison.OrdinalIgnoreCase) ||
                   trimmedCountry.Equals("United States of America", StringComparison.OrdinalIgnoreCase);
        }

        public string GetFullAddressString()
        {
            return $"{_streetAddress}\n{_city}, {_stateProvince}\n{_country}";
        }
    }

    public class Customer
    {
        private string _name;
        private Address _address;

        public Customer(string name, Address address)
        {
            _name = name;
            _address = address;
        }

        public string GetName()
        {
            return _name;
        }

        public Address GetAddress()
        {
            return _address;
        }

        public bool LivesInUs()
        {
            return _address.IsInUs();
        }
    }

    public class Product
    {
        private string _name;
        private string _productId;
        private decimal _pricePerUnit;
        private int _quantity;

        public Product(string name, string productId, decimal pricePerUnit, int quantity)
        {
            _name = name;
            _productId = productId;
            _pricePerUnit = pricePerUnit;
            _quantity = quantity;
        }

        public string GetName()
        {
            return _name;
        }

        public string GetProductId()
        {
            return _productId;
        }

        public decimal GetTotalCost()
        {
            return _pricePerUnit * _quantity;
        }
    }

    public class Order
    {
        private List<Product> _products;
        private Customer _customer;

        public Order(Customer customer)
        {
            _customer = customer;
            _products = new List<Product>();
        }

        public void AddProduct(Product product)
        {
            _products.Add(product);
        }

        public decimal CalculateTotalCost()
        {
            decimal productsTotal = 0;
            foreach (var product in _products)
            {
                productsTotal += product.GetTotalCost();
            }

            decimal shippingCost = _customer.LivesInUs() ? 5.0m : 35.0m;
            return productsTotal + shippingCost;
        }

        public string GetPackingLabel()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("--- Packing Label ---");
            foreach (var product in _products)
            {
                sb.AppendLine($"Product: {product.GetName()} | Product ID: {product.GetProductId()}");
            }
            return sb.ToString().TrimEnd();
        }

        public string GetShippingLabel()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("--- Shipping Label ---");
            sb.AppendLine(_customer.GetName());
            sb.AppendLine(_customer.GetAddress().GetFullAddressString());
            return sb.ToString().TrimEnd();
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Address address1 = new Address("742 Evergreen Terrace", "Springfield", "OR", "USA");
            Customer customer1 = new Customer("Homer Simpson", address1);
            Order order1 = new Order(customer1);

            order1.AddProduct(BoxProduct("Duff Beer 6-Pack", "DB-01", 8.99m, 2));
            order1.AddProduct(BoxProduct("Box of Donuts", "DN-05", 5.49m, 1));

            Address address2 = new Address("221B Baker Street", "London", "Greater London", "United Kingdom");
            Customer customer2 = new Customer("Sherlock Holmes", address2);
            Order order2 = new Order(customer2);

            order2.AddProduct(BoxProduct("Magnifying Glass", "MG-99", 24.50m, 1));
            order2.AddProduct(BoxProduct("Notebook", "NB-12", 4.99m, 3));
            order2.AddProduct(BoxProduct("Tobacco Pipe", "TP-08", 45.00m, 1));

            Console.WriteLine(order1.GetShippingLabel());
            Console.WriteLine();
            Console.WriteLine(order1.GetPackingLabel());
            Console.WriteLine();
            Console.WriteLine($"Total Order Cost: ${order1.CalculateTotalCost():F2}");
            Console.WriteLine(new string('=', 40));
            Console.WriteLine();

            Console.WriteLine(order2.GetShippingLabel());
            Console.WriteLine();
            Console.WriteLine(order2.GetPackingLabel());
            Console.WriteLine();
            Console.WriteLine($"Total Order Cost: ${order2.CalculateTotalCost():F2}");
        }

        private static Product BoxProduct(string name, string id, decimal price, int qty)
        {
            return new Product(name, id, price, qty);
        }
    }
}