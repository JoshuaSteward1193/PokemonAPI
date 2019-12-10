using System;
using System.Collections.Generic;
using System.Text;

namespace PokeAPI.Models
{

    public class Berry
    {
        public Firmness firmness { get; set; }
        public Flavor[] flavors { get; set; }
        public int growth_time { get; set; }
        public int id { get; set; }        
        public int max_harvest { get; set; }
        public string name { get; set; }
        public int natural_gift_power { get; set; }
        public Natural_Gift_Type natural_gift_type { get; set; }
        public int size { get; set; }
        public int smoothness { get; set; }
        public int soil_dryness { get; set; }
    }

    public class Firmness
    {
        public string name { get; set; }
        public string url { get; set; }
    }   

    public class Natural_Gift_Type
    {
        public string name { get; set; }
        public string url { get; set; }
    }

    public class Flavor
    {
        public Flavor1 flavor { get; set; }
        public int potency { get; set; }
    }

    public class Flavor1
    {
        public string name { get; set; }
        public string url { get; set; }
    }

}
