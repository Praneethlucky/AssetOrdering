namespace AssestOrderingApplication.Models
{
    public class Admin
    {
        public int AdminId { get; set; } // Primary Key
        public string AdminName { get; set; }
        public int AddedByEmployeeId { get; set; } // Foreign Key from Employees
        public DateTime AddedTime { get; set; } = DateTime.Now; // Auto-set on addition

        // Constructor
        public Admin()
        {
            AddedTime = DateTime.Now;
        }
    }

}
