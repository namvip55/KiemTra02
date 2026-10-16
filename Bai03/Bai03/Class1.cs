using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;

namespace Bai3_MDI_QuanLySieuThi
{
    public class Product : INotifyPropertyChanged
    {
        private string _name;
        private string _category;
        private decimal _price;
        private int _quantity;
        private string _imagePath;

        public string ProductId { get; set; }

        public string Name
        {
            get { return _name; }
            set { _name = value; OnPropertyChanged("Name"); }
        }

        public string Category
        {
            get { return _category; }
            set { _category = value; OnPropertyChanged("Category"); }
        }

        public decimal Price
        {
            get { return _price; }
            set { _price = value; OnPropertyChanged("Price"); }
        }

        public int Quantity
        {
            get { return _quantity; }
            set { _quantity = value; OnPropertyChanged("Quantity"); OnPropertyChanged("StockValue"); }
        }

        public string ImagePath
        {
            get { return _imagePath; }
            set { _imagePath = value; OnPropertyChanged("ImagePath"); }
        }

        public decimal StockValue { get { return Price * Quantity; } }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class InventoryTransaction
    {
        public string TransactionId { get; set; }
        public string ProductId { get; set; }
        public string ProductName { get; set; }
        public string Type { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public DateTime CreatedAt { get; set; }
        public decimal Amount { get { return Quantity * UnitPrice; } }
    }

    public class RevenueReport
    {
        public decimal TotalRevenue { get; set; }
        public int TransactionCount { get; set; }
        public List<RevenueLine> Lines { get; set; }

        public RevenueReport()
        {
            Lines = new List<RevenueLine>();
        }
    }

    public class RevenueLine
    {
        public string TransactionId { get; set; }
        public string ProductName { get; set; }
        public DateTime CreatedAt { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Revenue { get; set; }
    }

    public class SyncLog
    {
        public DateTime Time { get; set; }
        public string Message { get; set; }
        public int NewOrderCount { get; set; }
    }

    // Nguồn dữ liệu dùng chung trong bộ nhớ cho toàn bộ Form.
    public static class SupermarketRepository
    {
        private static readonly object _syncRoot = new object();
        private static int _nextProductId = 4;

        public static BindingList<Product> Products { get; private set; }
        public static BindingList<InventoryTransaction> Transactions { get; private set; }

        static SupermarketRepository()
        {
            Products = new BindingList<Product>
            {
                new Product { ProductId = "SP001", Name = "Gạo thơm 5kg", Category = "Thực phẩm", Price = 125000m, Quantity = 30 },
                new Product { ProductId = "SP002", Name = "Sữa tươi hộp", Category = "Đồ uống", Price = 32000m, Quantity = 45 },
                new Product { ProductId = "SP003", Name = "Nước rửa chén", Category = "Gia dụng", Price = 28000m, Quantity = 20 }
            };

            Transactions = new BindingList<InventoryTransaction>();
            Transactions.Add(new InventoryTransaction
            {
                TransactionId = "GD0001", ProductId = "SP001", ProductName = "Gạo thơm 5kg",
                Type = "Xuất", Quantity = 2, UnitPrice = 125000m, CreatedAt = DateTime.Now.AddHours(-2)
            });
            Transactions.Add(new InventoryTransaction
            {
                TransactionId = "GD0002", ProductId = "SP002", ProductName = "Sữa tươi hộp",
                Type = "Xuất", Quantity = 5, UnitPrice = 32000m, CreatedAt = DateTime.Now.AddHours(-1)
            });
        }

        public static string CreateProductId()
        {
            lock (_syncRoot)
            {
                return "SP" + (_nextProductId++).ToString("000");
            }
        }

        public static string CreateTransactionId()
        {
            lock (_syncRoot)
            {
                return "GD" + (Transactions.Count + 1).ToString("0000");
            }
        }

        public static void AddTransaction(InventoryTransaction transaction)
        {
            lock (_syncRoot)
            {
                Transactions.Add(transaction);
            }
        }

        // Tạo bản sao dữ liệu để tác vụ nền không đọc BindingList trực tiếp.
        public static List<InventoryTransaction> GetTransactionSnapshot()
        {
            lock (_syncRoot)
            {
                return Transactions.Select(t => new InventoryTransaction
                {
                    TransactionId = t.TransactionId,
                    ProductId = t.ProductId,
                    ProductName = t.ProductName,
                    Type = t.Type,
                    Quantity = t.Quantity,
                    UnitPrice = t.UnitPrice,
                    CreatedAt = t.CreatedAt
                }).ToList();
            }
        }

        public static decimal GetStockValue()
        {
            lock (_syncRoot)
            {
                return Products.Sum(p => p.Price * p.Quantity);
            }
        }

        public static int GetTotalQuantity()
        {
            lock (_syncRoot)
            {
                return Products.Sum(p => p.Quantity);
            }
        }
    }
}
