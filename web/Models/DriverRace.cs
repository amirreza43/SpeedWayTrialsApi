using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations.Schema;

namespace web
{
    [Table("DriverRace")]
    public class DriverRace{
        public Race Race {set; get;}
        public Driver Driver {set; get;}
        public DriverRace(){
            Race = null;
            Driver = null;
        }
    }
}