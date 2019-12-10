using System;
using System.Collections.Generic;
using System.Text;

namespace PokeAPI.Models
{

    public class RootObject
    {
        public int count { get; set; }
        public string next { get; set; }
        public object previous { get; set; }
        public Result[] results { get; set; }
    }

    public class Result
    {
        public string name { get; set; }
        public string url { get; set; }
    }

}
