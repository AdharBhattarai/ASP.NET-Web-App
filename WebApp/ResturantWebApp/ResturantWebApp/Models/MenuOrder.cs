using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ResturantWebApp.Models
{
    public class MenuOrder
    {

        #region Private Variables

        private int _ID;
        private int _Quantity;
        private decimal _ItemPrice;
        private decimal _TotalPrice;
        private string _Comment;
        private int _OrderStatusID;
        private OrderStatus _OrderStatus;
        private int _MenuID;
        private Menu _Menu;

        #endregion

        #region  Properties

        public OrderStatus OrderStatus {
            get {
                if (_OrderStatus == null)
                {
                    _OrderStatus = DAL.GetOrderByID(_OrderStatusID);
                }
                return _OrderStatus;
            }
            set {
                _OrderStatus = value;
                if (value == null)
                {
                    _OrderStatusID = -1;
                }
                else
                {
                    _OrderStatusID = value.ID;
                }
            }
        }

        public int OrderStatusID {
            get { return _OrderStatusID; }
            set { _OrderStatusID = value; }
        }

        public int ID {
            get { return _ID; }
            set { _ID = value; }
        }

        public int Quantity {
            get { return _Quantity; }
            set { _Quantity = value; }
        }

        public decimal ItemPrice {
            get { return _ItemPrice; }
            set { _ItemPrice = value; }
        }


        public decimal TotalPrice {
            get { return _TotalPrice; }
            set { _TotalPrice = value; }
        }

        public string Comment {
            get { return _Comment; }
            set { _Comment = value; }
        }

        public int MenuID {
            get { return _MenuID; }
            set { _MenuID = value; }
        }

        public Menu Menu {
            get {
                if (_Menu == null)
                {
                    _Menu = DAL.MenuGet(_MenuID);
                }
                return _Menu;
            }
            set {
                _Menu = value;
                if (value == null)
                {
                    _MenuID = -1;
                }
                else
                {
                    _MenuID = value.ID;
                }
            }
        }

        #endregion





    }
}
