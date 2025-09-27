using POS.Commands;
using POS.DataBase;
using POS.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace POS.ViewModels
{
    internal class ProductViewModel : BaseViewModel
    {
        private readonly GenericRepository<Product> _productRepository;
        private ObservableCollection<Product> _products = [];
        private Product _product;
        private Product _SelectedProduct;

        public ICommand AddCommand { get; }
        public ICommand DeleteCommand { get; }
        public ICommand UpdateCommand { get; }

        public ProductViewModel()
        {
            _productRepository = new GenericRepository<Product>();
            _product = new Product();
            LoadProducts();
            AddCommand = new RelayCommand(AddExecute, AddCanExecute);
            DeleteCommand = new RelayCommand(DeleteExecute, DeleteCanExecute);
            UpdateCommand = new RelayCommand(UpdateExecute, UpdateCanExecute);
        }

        public Product Product
        {
            get => _product;
            set
            {
                if (_product != value)
                {
                    _product = value;
                    OnPropertyChanged(nameof(Product));
                }
            }
        }

        public Product SelectedProduct
        {
            get => _SelectedProduct;
            set
            {
                if (_SelectedProduct != value)
                {
                    _SelectedProduct = value;
                    OnPropertyChanged(nameof(_SelectedProduct));

                    if(_SelectedProduct != null)
                    {
                        Product = new Product
                        {
                            Id = _SelectedProduct.Id,
                            Code = _SelectedProduct.Code,
                            Description = _SelectedProduct.Description,
                            CostPrice = _SelectedProduct.CostPrice,
                            SalePrice = _SelectedProduct.SalePrice,
                            Stock = _SelectedProduct.Stock
                        };
                    }
                    else
                    {
                        Product = new Product();
                    }
                }
            }
        }

        public ObservableCollection<Product> Products
        {
            get => _products;
            set
            {
                if (_products != value)
                {
                    _products = value;
                    OnPropertyChanged(nameof(Products));
                }
            }
        }

        private void AddExecute(object obj)
        {
            _productRepository.Add(Product);
            LoadProducts();
            Product = new Product();
        }
        private bool AddCanExecute(object obj)
        {
            return true;
        }
        private void DeleteExecute(object obj)
        {
            _productRepository.Delete(Product);
            LoadProducts();
            Product = new Product();
        }
        private bool DeleteCanExecute(object obj)
        {
            return true;
        }
        private void UpdateExecute(object obj)
        {
            _productRepository.Update(Product);
            LoadProducts();
            Product = new Product();
        }
        private bool UpdateCanExecute(object obj)
        {
            return true;
        }
        
        private void LoadProducts()
        {
            _products = _productRepository.Get();
            OnPropertyChanged(nameof(Products));
        }
    }
}
