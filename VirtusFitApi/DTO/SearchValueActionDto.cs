using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VirtusFitApi.Models;

namespace VirtusFitApi.DTO
{
    public class SearchValueActionDto
    {
        public SearchActionType SearchType { get; set; }
        public string Username { get; set; }
        public DateTime Created { get; set; }
        public double SearchValue { get; set; }
    }
}
