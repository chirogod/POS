using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Models
{
    internal class Client
    {
        private int _Id;
        private int _Dni;
        private string _Name;
        private string _LastName;
        private string _Phone;
        private string _Address;
        private string _Email;

        public int Id
        {
            get => _Id;
            set
            {
                if (_Id != value)
                {
                    _Id = value;
                }
            }
        }

        public int Dni
        {
            get => _Dni;
            set
            {
                if (_Dni != value)
                {
                    _Dni = value;
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

        public string Phone
        {
            get => _Phone;
            set
            {
                if (_Phone != value)
                {
                    _Phone = value;
                }
            }
        }

        public string Address
        {
            get => _Address;
            set
            {
                if (_Address != value)
                {
                    _Address = value;
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
    }
}
