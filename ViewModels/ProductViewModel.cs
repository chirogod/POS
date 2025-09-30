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
            Task.Run(async () => await LoadProductsAsync());

            AddCommand = new AsyncCommand(AddExecuteAsync, AddCanExecute);
            DeleteCommand = new AsyncCommand(DeleteExecuteAsync, DeleteCanExecute);
            UpdateCommand = new AsyncCommand(UpdateExecuteAsync, UpdateCanExecute);
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

        private async Task AddExecuteAsync(object obj)
        {
            await _productRepository.AddAsync(Product);
            await LoadProductsAsync();
            Product = new Product();
        }
        private bool AddCanExecute(object obj)
        {
            return true;
        }

        private async Task DeleteExecuteAsync(object obj)
        {
            await _productRepository.DeleteAsync(Product);
            await LoadProductsAsync();
            Product = new Product();
        }
        private bool DeleteCanExecute(object obj)
        {
            return true;
        }
        private async Task UpdateExecuteAsync(object obj)
        {
            await _productRepository.UpdateAsync(Product);
            await LoadProductsAsync();
            Product = new Product();
        }
        private bool UpdateCanExecute(object obj)
        {
            return true;
        }
        
        private async Task LoadProductsAsync()
        {
            _products = await _productRepository.GetAsync();
            OnPropertyChanged(nameof(Products));
        }
    }
}
