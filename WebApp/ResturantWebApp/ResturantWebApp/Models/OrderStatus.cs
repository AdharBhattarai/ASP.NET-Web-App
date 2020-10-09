using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ResturantWebApp.Models
{
    public class OrderStatus
    {
        #region Private Varaibles
        private int _ID;
        private DateTime _OrderTime;
        private int _UserID;
        private User _User;
        private List<MenuOrder> _MenuOrders = null;
        #endregion


        #region Public Properties

        public List<MenuOrder> MenuOrders {
            get {
                if (_MenuOrders == null)
                {
                    _MenuOrders = DAL.MenuOrderGet(this);
                }
                return _MenuOrders;
            }
            set {
                if (_MenuOrders != null)
                {
                    foreach (var order in _MenuOrders)
                    {
                        DAL.MenuOrderAdd(order);
                    }
                }
            }
        }

        public User User {
            get {
                if (_User == null)
                {
                    _User = DAL.UserGetByID(_UserID);
                }
                return _User;
            }
            set {
                _User = value;
                if (value == null)
                {
                    _UserID = -1;
                }
                else
                {
                    _UserID = value.ID;
                }
            }
        }

        public int UserID {
            get { return _UserID; }
            set { _UserID = value; }
        }


        public int ID {
            get { return _ID; }
            set { _ID = value; }
        }


        public DateTime OrderTime {
            get { return _OrderTime; }
            set { _OrderTime = value; }
        }

        #endregion
    }
}
