using System;
using System.Collections.Generic;
using System.Text;

namespace EspinosaHomeAct.Model
{
    public class User
    {
        public int UserId { get; set; }
        public required string Name { get; set; }
        public required string PhoneNumber { get; set; }


    }
}
