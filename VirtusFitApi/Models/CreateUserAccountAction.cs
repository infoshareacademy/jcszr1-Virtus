using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VirtusFitApi.Models
{
    public class CreateUserAccountAction
    {
        public string UserId { get; set; }
        public UserAccountActionType ActionType { get; set; }
        public DateTime Created { get; set; }
    }
}
