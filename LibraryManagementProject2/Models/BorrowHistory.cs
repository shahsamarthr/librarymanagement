using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LibraryManagementProject2.Models
{
    public class BorrowHistory
    {
        public int BorrowHistoryId { get; set; }

        [Required]
        [Display(Name = "Book")]
        public int BookId { get; set; }

        public virtual Books Books { get; set; }

        [Required]
        [Display(Name = "User")]
        public int UserId { get; set; }

        public virtual User User { get; set; }

        [Display(Name = "Borrow Date")]
        public DateTime BorrowDate { get; set; }

        [Display(Name = "Return Date")]
        public DateTime? ReturnDate { get; set; }
        //1 User has 1 BorrowHistory
    }
}