namespace AssestOrderingApplication.Models
{
    public class OrderStatus
{
    public int StatusId { get; set; } // Primary Key
    public int OrderId { get; set; } // Foreign Key from Orders
    public int StatusTypeId { get; set; } // Foreign Key from OrderStatusTypes
    public DateTime StatusDate { get; set; } = DateTime.Now; // Auto-set on status update

    // Navigation Properties
    public Order Order { get; set; }
    public OrderStatusType StatusType { get; set; }

    // Constructor
    public OrderStatus()
    {
        StatusDate = DateTime.Now;
    }
}

}
