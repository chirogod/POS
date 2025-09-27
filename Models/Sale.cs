using POS.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Models
{
    internal class Sale : BaseViewModel
    {
        private int _Id;
        private DateTime _DateTime;
        private decimal _SubTotal;
        private decimal _Discount;
        private decimal _Total;
        private int _ClientId;
        private string _Observations;
        public virtual ICollection<SaleItem> SaleItems { get; set; }
        public virtual Client Client { get; set; }

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

        public DateTime DateTime
        {
            get => _DateTime;
            set
            {
                if (_DateTime != value)
                {
                    _DateTime = value;
                }
            }
        }

        public decimal SubTotal
        {
            get => _SubTotal;
            set
            {
                if (_SubTotal != value)
                {
                    _SubTotal = value;
                }
            }
        }

        public decimal Discount
        {
            get => _Discount;
            set
            {
                if (_Discount != value)
                {
                    _Discount = value;
                }
            }
        }

        public decimal Total
        {
            get => _Total;
            set
            {
                if (_Total != value)
                {
                    _Total = value;
                }
            }
        }

        public int ClientId
        {
            get => _ClientId;
            set
            {
                if (_ClientId != value)
                {
                    _ClientId = value;
                }
            }
        }

        public string Observations
        {
            get => _Observations;
            set
            {
                if (_Observations != value)
                {
                    _Observations = value;
                }
            }
        }
    }

    internal class SaleItem : BaseViewModel
    {
        private int _Id;
        private int _SaleId;
        private int _ProductId;
        private decimal _SalePrice;
        private decimal _Discount;
        private decimal _Quantity;
        private decimal _Total;

        public virtual Sale Sale { get; set; }
        public virtual Product Product { get; set; }

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

        public int SaleId
        {
            get => _SaleId;
            set
            {
                if (_SaleId != value)
                {
                    _SaleId = value;
                }
            }
        }

        public int ProductId
        {
            get => _ProductId;
            set
            {
                if (_ProductId != value)
                {
                    _ProductId = value;
                }
            }
        }

        public decimal Quantity
        {
            get => _Quantity;
            set
            {
                if (_Quantity != value)
                {
                    _Quantity = value;
                    OnPropertyChanged(nameof(Quantity));
                    OnPropertyChanged(nameof(Total));
                }
            }
        }

        public decimal Discount
        {
            get => _Discount;
            set
            {
                if (_Discount != value)
                {
                    _Discount = value;
                    OnPropertyChanged(nameof(Discount));
                    OnPropertyChanged(nameof(Total));
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

        public decimal Total
        {
            get => _Total;
            set
            {
                if (_Total != value)
                {
                    _Total = value;
                }
            }
        }

    }
}
