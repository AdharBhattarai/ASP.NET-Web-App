using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ResturantWebApp.Models
{
    public class Role
    {
        private int _ID;
        private string _Name;
        private bool _CategoryAdd = false;
        private bool _CategoryEdit = false;
        private bool _CategoryDetail = false;
        private bool _CategoryIndex = false;
        private bool _CategoryDelete = false;
        private bool _MenuCreate = false;
        private bool _MenuDelete = false;
        private bool _MenuEdit = false;
        private bool _OrderIndex = false;


        public int ID {
            get { return _ID; }
            set { _ID = value; }
        }

        public string Name {
            get { return _Name; }
            set { _Name = value; }
        }

        public bool CategoryEdit {
            get { return _CategoryEdit; }
            set { _CategoryEdit = value; }
        }

        public bool CategoryDetail {
            get { return _CategoryDetail; }
            set { _CategoryDetail = value; }
        }

        public bool CategoryAdd {
            get { return _CategoryAdd; }
            set { _CategoryAdd = value; }
        }
        public bool CategoryIndex {
            get { return _CategoryIndex; }
            set { _CategoryIndex = value; }
        }

        public bool CategoryDelete {
            get { return _CategoryDelete; }
            set { _CategoryDelete = value; }
        }

        public bool MenuCreate {
            get { return _MenuCreate; }
            set { _MenuCreate = value; }
        }
        public bool MenuEdit {
            get { return _MenuEdit; }
            set { _MenuEdit = value; }
        }
        public bool MenuDelete {
            get { return _MenuDelete; }
            set { _MenuDelete = value; }
        }
        public bool OrderIndex {
            get { return _OrderIndex; }
            set { _OrderIndex = value; }
        }
    }
}
