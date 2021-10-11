using System;
using System.ComponentModel.DataAnnotations;

namespace web
{
    public class RaceCarDto{
        [Required]
        [MinLength(3)]
        public string Nickname {set; get;}
        [Required]
        public CarModels Model {set; get;}
        [Required]
        public int Year {set; get;}
        public int TopSpeed {set; get;}
        [Required]
        public Status Status {set; get;}
        [Required]
        public CarTypes CarType {set; get;}
    }
}