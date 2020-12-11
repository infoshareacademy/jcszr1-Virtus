using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VirtusFitApi.Models
{
    public class SearchStringAction
    {
        public int Id { get; set; }
        public SearchActionType SearchType { get; set; }
        public string Username { get; set; }
        public DateTime Created { get; set; }
        public string SearchString { get; set; }
    }
}
