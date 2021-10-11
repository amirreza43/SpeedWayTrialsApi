using System;
using System.Collections.Generic;

namespace web
{
    public class Driver{
        public Guid Id {set; get;}
        public string FirstName {set; get;}
        public string LastName {set; get;}
        public string Nickname {set; get;}
        public int Age {set; get;}
        public int Wins {set; get;}
        public int Losses {set; get;}
        public DateTime BirthDate {set; get;}
        public List<RaceCar> Cars {set; get;}
    }
}