using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ResturantWebApp.Models
{
    public class AccountPrivilege
    {
        private int _ID;

        public int ID {
            get { return _ID; }
            set { _ID = value; }
        }

        //FK TO Privilege
        //FK to Account

    }
}
