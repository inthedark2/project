using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Helpers
{
    public enum UserStatus
    {
        Success=0,
        DublicateEmail=1,
        NotFoundUser=2
    }
}
