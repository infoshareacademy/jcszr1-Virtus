using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VirtusFitApi.Models;

namespace VirtusFitApi.DTO
{
    public class UserAccountActionDto
    {
        public string UserId { get; set; }
        public UserAccountActionType ActionType { get; set; }
        public DateTime Created { get; set; }
    }
}
