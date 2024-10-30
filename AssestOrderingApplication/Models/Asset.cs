using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;  // For IConfiguration

namespace AssestOrderingApplication.Models
{
    public class Asset
    {
        public int Id { get; set; } // Primary Key
        public string Name { get; set; }
        public string Type { get; set; }
        public string Category { get; set; }
        public string Location { get; set; }
        public bool inCart { get; set; }
        public string InsertedBy { get; set; }
        public DateTime InsertedTime { get; set; } = DateTime.Now; // Auto-set on creation
        public DateTime UpdatedTime { get; set; } = DateTime.Now;  // Auto-set on update

    }

}
