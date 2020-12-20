using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VirtusFitApi.Models
{
    public class UserAccountAction
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public UserAccountActionType ActionType { get; set; }
        public DateTime Created { get; set; }
        
    }
}
