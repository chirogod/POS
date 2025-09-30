using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace POS.Models
{
    class User
    {
        private int _Id;
        private string _Name;
        private string _LastName;
        private string _Telephone;
        private string _Email;
        private string _Username;
        private string _Password; 

        public int Id
        {
            get => _Id;
            set
            {
                if(_Id != value)
                {
                    _Id = value;
                }
            }
        }

        public string Name
        {
            get => _Name;
            set
            {
                if (_Name != value)
                {
                    _Name = value;
                }
            }
        }

        public string LastName
        {
            get => _LastName;
            set
            {
                if (_LastName != value)
                {
                    _LastName = value;
                }
            }
        }

        public string Telephone
        {
            get => _Telephone;
            set
            {
                if (_Telephone != value)
                {
                    _Telephone = value;
                }
            }
        }

        public string Email
        {
            get => _Email;
            set
            {
                if (_Email != value)
                {
                    _Email = value;
                }
            }
        }

        public string Username
        {
            get => _Username;
            set
            {
                if (_Username != value)
                {
                    _Username = value;
                }
            }
        }

        public string Password
        {
            get => _Password;
            set
            {
                if (_Password != value)
                {
                    _Password = value;
                }
            }
        }
    }
}
