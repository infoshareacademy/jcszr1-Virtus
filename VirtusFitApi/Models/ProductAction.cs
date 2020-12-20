using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace VirtusFitApi.Models
{
    public class ProductAction
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string Username { get; set; }
        public string ProductName { get; set; }
        public ActionType Action { get; set; }
        public DateTime Created { get; set; }
     }
}
