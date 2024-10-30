namespace AssestOrderingApplication.Models
{
    public class Order
    {
        public int OrderId { get; set; } // Primary Key
        public List<int> AssetId { get; set; } // Foreign Key from Assets
        public string AssetNames { get; set; } // Foreign Key from Assets
        public string EmployeeId { get; set; } // Employee placing the order
        public string ManagerEmployeeId { get; set; } // Employee placing the order
        public string OfficeAddress { get; set; } // Employee placing the order
        public string HomeAddress { get; set; } // Employee placing the order
        public DateTime OrderDate { get; set; } = DateTime.Now; // Auto-set on order creation
        public int Quantity { get; set; }
        public string ProductFamily { get; set; }
        public string DeliverTo { get; set; }
        public string Country { get; set; }
        public string PhoneNumber { get; set; }
        public string Comments { get; set; }
        public string OrderStatus { get; set; }
        
        // Navigation Properties
        public List<OrderStatus> OrderStatuses { get; set; }

        // Constructor
        public Order()
        {
            OrderDate = DateTime.Now;
        }
    }

}
