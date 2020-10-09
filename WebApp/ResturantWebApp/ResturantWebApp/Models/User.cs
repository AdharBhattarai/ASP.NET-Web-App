using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ResturantWebApp.Models
{
    public class User
    {
        private int _ID;
        private string _Email;
        private string _Password;
        private string _Salt;
        private string _FirstName;
        private string _LastName;
        private string _Phone;
        private int _RoleID = 1;
        private Role _Role;

        public int ID {
            get { return _ID; }
            set { _ID = value; }
        }

        [StringLength(42)]
        [Required(ErrorMessage = "Please, provide a {0}.")]
        public string Email {
            get { return _Email; }
            set { _Email = value; }
        }

        [StringLength(42)]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Please, provide a {0}.")]
        public string Password {
            get { return _Password; }
            set { _Password = value; }
        }

        [Display(Name = "First name")]
        [StringLength(42)]
        [Required(ErrorMessage = "Please, provide a {0}.")]
        public string FirstName {
            get { return _FirstName; }
            set { _FirstName = value; }
        }

        [Display(Name = "Last name")]
        [StringLength(42)]
        [Required(ErrorMessage = "Please, provide a {0}.")]
        public string LastName {
            get { return _LastName; }
            set { _LastName = value; }
        }

        public string Salt {
            get { return _Salt; }
            set { _Salt = value; }
        }

        [Display(Name = "Phone Number")]
        [StringLength(15)]
        [Required(ErrorMessage = "Please, provide a {0}.")]
        public string Phone {
            get { return _Phone; }
            set { _Phone = value; }
        }

        public string FullName {
            get {
                return String.Format("{0} {1}",
                    FirstName, LastName);
            }
        }
        public int RoleID {
            get {
                if (_RoleID == null){
                    _RoleID = 1;
                }
                return _RoleID;
            }
            set { _RoleID = value; }
        }

        public Role Role {
            get {
                if (_Role == null)
                {
                    _Role = DAL.RoleGet(_RoleID);
                }
                return _Role;
            }
            set {
                _Role = value;
                if (value == null)
                {
                    _RoleID = -1;
                }
                else
                {
                    _RoleID = value.ID;
                }
            }
        }


    }
}
