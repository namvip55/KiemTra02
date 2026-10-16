using System;

namespace InventoryManager
{
    public class Product
    {
        public string ProductId { get; set; }

        public string ProductName { get; set; }

        public int Quantity { get; set; }

        public decimal UnitPrice { get; set; }

        public string ImagePath { get; set; }

        public decimal TotalValue
        {
            get { return Quantity * UnitPrice; }
        }

        public Product()
        {
            ProductId = string.Empty;
            ProductName = string.Empty;
            Quantity = 0;
            UnitPrice = 0;
            ImagePath = string.Empty;
        }

        public Product(
            string productId,
            string productName,
            int quantity,
            decimal unitPrice,
            string imagePath)
        {
            ProductId = productId;
            ProductName = productName;
            Quantity = quantity;
            UnitPrice = unitPrice;
            ImagePath = imagePath;
        }
    }
}