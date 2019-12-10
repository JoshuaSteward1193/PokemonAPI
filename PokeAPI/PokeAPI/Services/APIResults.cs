using System;
using System.Collections.Generic;
using System.Text;

namespace PokeAPI.Services
{
    public class APIResults<T>
    {
        public APIResults()
        {
        }

        public string previous
        {
            get;
            set;
        }

        public string next
        {
            get;
            set;
        }

        /*public string previousPageNo
        {
            get;
            set;
        }

        public string nextPageNo
        {
            get;
            set;
        }*/

        public long count
        {
            get;
            set;
        }

        public List<T> results
        {
            get;
            set;
        }
    }
}
