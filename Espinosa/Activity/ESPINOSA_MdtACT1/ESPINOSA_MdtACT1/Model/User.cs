using System;
using System.Collections.Generic;
using System.Text;

namespace ESPINOSA_MdtACT1.Model
{
    public class User //create users table Db
    {
        public int UserId { get; set; } //primary key column
        public required string Name { get; set; }


    }
}
