using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LibraryManagementProject2.Models
{
    public class Books
    {
        [Key]
        public int BookId { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string SerialNumber { get; set; }

        public string Author { get; set; }

        public string Publisher { get; set; }
        [Required]
        public int qty { get; set; }
        [Required]
        public bool IsAvailable { get; set; }

        public ICollection<BorrowHistory> BorrowHistories { get; set; } 
        // 1 Book can have many BaorrowHistory
    }
}