using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VirtusFitApi.Models;

namespace VirtusFitApi.DTO
{
    public class ProductActionDto
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string Username { get; set; }
        public ActionType Action { get; set; }
        public DateTime Created { get; set; }
    }
}
