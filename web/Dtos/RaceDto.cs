using System;
using System.ComponentModel.DataAnnotations;

namespace web
{
    public class RaceDto{
        [Required]
        [MinLength(3)]
        public string Name {set; get;}
        [Required]
        public DateTime Date {set; get;}
        public DateTime BestTime {set; get;}
        public string WinnerName {set; get;}
        [Required]
        public RaceCategories RaceCategory{set; get;}
    }
}