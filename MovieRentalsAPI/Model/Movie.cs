using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MovieRentalsAPI.Model
{
    public class Movie
    {
        [Key]
        public int MovieId { get; set; }

        [Required]
        public string MovieTitle { get; set; }

        public string MovieDescription { get; set; }

        public bool IsRented { get; set; } = false;

        public DateTime? RentalDate { get; set; }

        public bool IsDeleted { get; set; } = false;
    }
}
