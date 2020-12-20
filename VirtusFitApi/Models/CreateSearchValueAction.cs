using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VirtusFitApi.Models
{
    public class CreateSearchValueAction
    {
        public SearchActionType SearchType { get; set; }
        public string Username { get; set; }
        public DateTime Created { get; set; }
        public double SearchValue { get; set; }
    }
}
