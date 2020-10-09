using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ResturantWebApp.Models
{
    public class Menu
    {
        #region Private Variables

        private int _ID;
        private string _Name;
        private decimal _Price;
        private string _Description;
        private int _CategoryID;
        private Category _Category;     
        #endregion

        #region  Properties

        public int ID {
            get { return _ID; }
            set { _ID = value; }
        }

        [StringLength(60)]
        [Required(ErrorMessage = "Please, provide a {0} of the Item.")]
        public string Name {
            get { return _Name; }
            set { _Name = value; }
        }

        // Reference from stack overflow https://stackoverflow.com/questions/19811180/best-data-annotation-for-a-decimal18-2
        [Range(0, 99999999999.99)]
        [Required(ErrorMessage = "Please, provide a {0}.")]
        public decimal Price {
            get { return _Price; }
            set { _Price = value; }
        }

        [StringLength(200)]
        [Required(ErrorMessage = "Please, provide a short {0} of the Item.")]
        public string Description {
            get { return _Description; }
            set { _Description = value; }
        }

        public int CategoryID {
            get { return _CategoryID; }
            set { _CategoryID = value; }
        }
        public Category Category {
            get {
                if (_Category == null)
                {
                    _Category = DAL.CategoryGet(_CategoryID);
                }
                return _Category;
            }
            set {
                _Category = value;
                if (value == null)
                {
                    _CategoryID = -1;
                }
                else
                {
                    _CategoryID = value.ID;
                }
            }
        }
       

        #endregion

    }
}
