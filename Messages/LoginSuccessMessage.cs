using CommunityToolkit.Mvvm.Messaging.Messages;
using project_open.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagementSystem.Messages
{
    public class LoginSuccessMessage(AuthenticationResult result) : ValueChangedMessage<AuthenticationResult>(result);
}