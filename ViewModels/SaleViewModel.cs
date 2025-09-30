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
    internal class SaleViewModel : BaseViewModel
    {
        private readonly GenericRepository<Sale> _saleRepository;
        private readonly GenericRepository<Product> _productRepository;
        private readonly GenericRepository<Client> _clientRepository;
        private Sale _Sale;
        private ObservableCollection<Sale> _Sales;
        private ObservableCollection<Product> _Products;
        private ObservableCollection<Client> _Clients;
        private ObservableCollection<SaleItem> _SaleItems;

        private int _SaleItemQuantity = 1;
        private int _SaleItemDiscount = 0;

        private Client _SelectedClient;
        private Product _SelectedProduct;
        private SaleItem _SelectedSaleItem;

        public ICommand AddProductCommand { get; }
        public ICommand RemoveProductCommand { get; }
        public ICommand SaveSaleCommand { get; }

        private string _searchClientQuery;

        private ObservableCollection<Client> _filteredClients;

        private string _searchProductQuery;

        private ObservableCollection<Product> _filteredProducts;

        public SaleViewModel()
        {
            _saleRepository = new GenericRepository<Sale>();
            _productRepository = new GenericRepository<Product>();
            _clientRepository = new GenericRepository<Client>();
            _Sale = new Sale();
            _Sales = [];
            _Products = [];
            _Clients = [];
            _SaleItems = [];

            Task.Run(async () => await LoadSalesAsync());
            Task.Run(async () => await LoadProductsAsync());
            Task.Run(async () => await LoadClientsAsync());

            AddProductCommand = new RelayCommand(AddProductExecute, AddProductCanExecute);
            RemoveProductCommand = new RelayCommand(RemoveProductExecute, RemoveProductCanExecute);
            SaveSaleCommand = new AsyncCommand(SaveSaleExecuteAsync, SaveSaleCanExecute);
        }
        public Sale Sale
        {
            get => _Sale;
            set
            {
                if (_Sale != value)
                {
                    _Sale = value;
                    OnPropertyChanged(nameof(Sale));
                }
            }
        }
        public SaleItem SelectedItem
        {
            get => _SelectedSaleItem;
            set
            {
                _SelectedSaleItem = value;
                OnPropertyChanged(nameof(SelectedItem));
            }
        }
        public Client SelectedClient
        {
            get => _SelectedClient;
            set
            {
                _SelectedClient = value;
                OnPropertyChanged(nameof(SelectedClient));
            }
        }

        public Product SelectedProduct
        {
            get => _SelectedProduct;
            set
            {
                _SelectedProduct = value;
                OnPropertyChanged(nameof(SelectedProduct));
            }
        }

        public ObservableCollection<Client> Clients
        {
            get => _Clients;
            set
            {
                if (_Clients != value)
                {
                    _Clients = value;
                    OnPropertyChanged(nameof(Clients));
                }
            }
        }
        public ObservableCollection<Product> Products
        {
            get => _Products;
            set
            {
                if (_Products != value)
                {
                    _Products = value;
                    OnPropertyChanged(nameof(Products));
                }
            }
        }

        public ObservableCollection<Sale> Sales
        {
            get => _Sales;
            set
            {
                if (_Sales != value)
                {
                    _Sales = value;
                    OnPropertyChanged(nameof(Sales));
                }
            }
        }

        public ObservableCollection<SaleItem> SaleItems
        {
            get => _SaleItems;
            set
            {
                if (_SaleItems != value)
                {
                    _SaleItems = value;
                    OnPropertyChanged(nameof(SaleItems));
                }
            }
        }

        public int SaleItemQuantity
        {
            get => _SaleItemQuantity;
            set
            {
                if (_SaleItemQuantity != value)
                {
                    _SaleItemQuantity = value;
                    OnPropertyChanged(nameof(SaleItemQuantity));
                }
            }
        }

        public int SaleItemDiscount

        {
            get => _SaleItemDiscount;
            set
            {

               if (_SaleItemDiscount != value)
               {
                    _SaleItemDiscount = value;
                    OnPropertyChanged(nameof(SaleItemDiscount));
               }
            }
        }

        private void AddProductExecute(object x)
        {
            if (SelectedProduct != null && SelectedProduct.Stock > 0 && SelectedProduct.Stock >= SaleItemQuantity)
            {
                var existingItem = _SaleItems.FirstOrDefault(item => item.ProductId == SelectedProduct.Id);
                if (existingItem != null)
                {
                    existingItem.Quantity += SaleItemQuantity;
                    OnPropertyChanged(nameof(SaleItems));
                }
                else
                {
                    var newSaleItem = new SaleItem
                    {
                        ProductId = SelectedProduct.Id,
                        Quantity = SaleItemQuantity,
                        SalePrice = SelectedProduct.SalePrice,
                        Product = SelectedProduct,
                        Discount = SaleItemDiscount
                    };
                    _SaleItems.Add(newSaleItem);
                }
                _Sale.SubTotal = _SaleItems.Sum(item => item.Total);
                _Sale.Total = _Sale.SubTotal - _Sale.Discount;
                OnPropertyChanged(nameof(Sale));

                SaleItemQuantity = 1;
                SaleItemDiscount = 0;

            }
        }

        private bool AddProductCanExecute(object x)
        {
            return SelectedProduct != null && SelectedProduct.Stock > 0 && SaleItemQuantity > 0;
        }

        private void RemoveProductExecute(object x)
        {
            if (SelectedItem != null)
            {
                _SaleItems.Remove(SelectedItem);
                _Sale.SubTotal = _SaleItems.Sum(item => item.Total);
                OnPropertyChanged(nameof(Sale));
            }
        }

        private bool RemoveProductCanExecute(object x)
        {
            return SelectedItem != null;
        }

        private async Task SaveSaleExecuteAsync(object x)
        {
            if (Sale.Total > 0 && SelectedClient != null)
            {
                _Sale.ClientId = SelectedClient.Id;
                _Sale.DateTime = DateTime.Now;
                _Sale.SubTotal = Sale.SubTotal;
                _Sale.Discount = Sale.Discount;
                _Sale.Total = Sale.SubTotal - (Sale.SubTotal * Sale.Discount / 100);
                _Sale.Observations = Sale.Observations;

                await _saleRepository.AddAsync(_Sale);

                foreach (var item in _Sale.SaleItems)
                {
                    var productInStock = _Products.FirstOrDefault(p => p.Id == item.ProductId);
                    if (productInStock != null)
                    {
                        productInStock.Stock -= (int)(decimal)item.Quantity;
                        await _productRepository.UpdateAsync(productInStock);
                    }
                }

                await LoadSalesAsync();
                _SaleItems.Clear();
                SelectedClient = null;
                Sale = new Sale();
                OnPropertyChanged(nameof(Sale));
                OnPropertyChanged(nameof(SaleItems));
            }
        }
        private bool SaveSaleCanExecute(object x)
        {
            return true;
        }
        public string SearchClientQuery
        {
            get => _searchClientQuery;
            set
            {
                if (_searchClientQuery != value)
                {
                    _searchClientQuery = value;
                    OnPropertyChanged(nameof(SearchClientQuery));
                    FilterClients();
                }
            }
        }

        public ObservableCollection<Client> FilteredClients
        {
            get => _filteredClients;
            set
            {
                _filteredClients = value;
                OnPropertyChanged(nameof(FilteredClients));
            }
        }

        private void FilterClients()
        {
            if (string.IsNullOrWhiteSpace(SearchClientQuery))
            {
                FilteredClients = new ObservableCollection<Client>(_Clients);
            }
            else
            {
                var filteredList = _Clients.Where(c =>
                  c.Dni.ToString().Contains(SearchClientQuery, StringComparison.OrdinalIgnoreCase) ||
                  c.Name.Contains(SearchClientQuery, StringComparison.OrdinalIgnoreCase) ||
                  c.Email.Contains(SearchClientQuery, StringComparison.OrdinalIgnoreCase) ||
                  c.Phone.Contains(SearchClientQuery, StringComparison.OrdinalIgnoreCase));

                FilteredClients = new ObservableCollection<Client>(filteredList);
            }
        }

        public string SearchProductQuery
        {
            get => _searchProductQuery;
            set
            {
                if (_searchProductQuery != value)
                {
                    _searchProductQuery = value;
                    OnPropertyChanged(nameof(SearchProductQuery));
                    FilterProducts();
                }
            }
        }

        public ObservableCollection<Product> FilteredProducts
        {
            get => _filteredProducts;
            set
            {
                _filteredProducts = value;
                OnPropertyChanged(nameof(FilteredProducts));
            }
        }
        private void FilterProducts()
        {
            if (string.IsNullOrWhiteSpace(SearchProductQuery))
            {
                FilteredProducts = new ObservableCollection<Product>(_Products);
            }

            else
            {
                var filteredList = _Products.Where(c =>
                    c.Code.ToString().Contains(SearchProductQuery, StringComparison.OrdinalIgnoreCase) ||
                    c.Description.Contains(SearchProductQuery, StringComparison.OrdinalIgnoreCase));

                FilteredProducts = new ObservableCollection<Product>(filteredList);
            }
        }
        private async Task LoadSalesAsync()
        {
            _Sales = await _saleRepository.GetAsync();
            OnPropertyChanged(nameof(Sales));
        }
        private async Task LoadClientsAsync()
        {
            _Clients = await _clientRepository.GetAsync();
            OnPropertyChanged(nameof(Clients));
            FilterClients();
        }

        private async Task LoadProductsAsync()
        {
            _Products = await _productRepository.GetAsync();
            OnPropertyChanged(nameof(Products));
            FilterProducts();
        }
    }
}