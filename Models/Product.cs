using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Models
{
    internal class Product
    {
        private int _Id;
        private int _Code;
        private string _Description;
        private decimal _CostPrice;
        private decimal _SalePrice;
        private int _Stock;

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

        public int Code
        {
            get => _Code;
            set
            {
                if (_Code != value)
                {
                    _Code = value;
                }
            }
        }

        public string Description
        {
            get => _Description;
            set
            {
                if (_Description != value)
                {
                    _Description = value;
                }
            }
        }



        public decimal CostPrice
        {
             get => _CostPrice;
            set
            {
                if (_CostPrice != value)
                {
                    _CostPrice = value;
                }
            }
        }

        public decimal SalePrice
        {
            get => _SalePrice;
            set
            {
                if (_SalePrice != value)
                {
                    _SalePrice = value;
                }
            }
        }

        public int Stock
        {
            get => _Stock;
            set
            {
                if (_Stock != value)
                {
                    _Stock = value;
                }
            }
        }
    }
}
