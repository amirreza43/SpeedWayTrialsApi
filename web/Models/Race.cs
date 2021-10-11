using System;
using System.Collections.Generic;

namespace web
{
    public class Race{
        public Guid Id {set; get;}
        public string Name {set; get;}
        public DateTime Date {set; get;}
        public DateTime BestTime {set; get;}
        public Driver Winner {set; get;}
        public List<Driver> Participants {set; get;}
        public RaceCategories RaceCategory{set; get;}
    }
}