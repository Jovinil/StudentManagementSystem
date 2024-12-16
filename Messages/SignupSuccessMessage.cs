using CommunityToolkit.Mvvm.Messaging.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagementSystem.Messages
{
    public class SignupSuccessMessage(bool result) : ValueChangedMessage<bool>(result);
}