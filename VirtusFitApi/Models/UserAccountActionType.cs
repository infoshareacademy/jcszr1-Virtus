using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VirtusFitApi.Models
{
    public enum UserAccountActionType
    {
        AccountCreated=0,
        SuccessfulLogonAttempt,
        FailedLogonAttempt,
        UserLoggedOut,
        PasswordChanged

    }
}
