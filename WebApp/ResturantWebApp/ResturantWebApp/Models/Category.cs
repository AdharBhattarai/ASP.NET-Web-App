using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;



namespace ResturantWebApp.Models
{
    public class Category
    {

        private int _ID;
        private string _Name;
        private string _Description;

        private List<Menu> _Menus = null;

        [ValidateNever]
        public List<Menu> Menus {
            get {
                if (_Menus == null)
                {
                    _Menus = DAL.MenuGet(this);
                }
                return _Menus;
            }
        }

        public int ID {
            get { return _ID; }
            set { _ID = value; }
        }

        [StringLength(60)]
        [Required(ErrorMessage = "Please, provide a {0}.")]
        public string Name {
            get { return _Name; }
            set { _Name = value; }
        }
        [StringLength(200)]
        [Required(ErrorMessage = "Please, provide a {0}.")]
        public string Description {
            get { return _Description; }
            set { _Description = value; }
        }


    }
}
