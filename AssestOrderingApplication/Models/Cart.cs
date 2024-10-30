namespace AssestOrderingApplication.Models
{
    public class Cart
    {
        public int AssetId { get; set; }
        public string AssetName { get; set; }
        public int AssetQuantity { get; set; }
        public double TotalCost { get; set; }
        public string EmployeeName { get; set; }

    }

}
