namespace AssestOrderingApplication.Models
{
    public class OrderStatusType
    {
        public int StatusTypeId { get; set; } // Primary Key
        public string StatusTypeName { get; set; } // Unique status name (e.g., Placed, In Cart, etc.)

        public ICollection<OrderStatus> OrderStatuses { get; set; }

    }

}
