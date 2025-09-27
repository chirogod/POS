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
    internal class ClientViewModel : BaseViewModel
    {
        private readonly GenericRepository<Client> _clientRepository;
        private ObservableCollection<Client> _clients = [];
        private Client _client;
        

        public ICommand AddCommand { get; }
        public ICommand UpdateCommand { get; }
        public ICommand DeleteCommand { get; }


        public ClientViewModel()
        {
            _clientRepository = new GenericRepository<Client>();
            _client = new Client();
            LoadClients();
            AddCommand = new RelayCommand(AddExecute, AddCanExecute);
            DeleteCommand = new RelayCommand(DeleteExecute, DeleteCanExecute);
            UpdateCommand = new RelayCommand(UpdateExecute, UpdateCanExecute);
        }



        public Client Client
        {
            get => _client;
            set
            {
                if (_client != value)
                {
                    _client = value;
                    OnPropertyChanged(nameof(Client));
                }
            }
        }
        private Client _SelectedClient;
        public Client SelectedClient
        {
            get => _SelectedClient;
            set {
                if(_SelectedClient != value){
                    _SelectedClient = value;
                    OnPropertyChanged(nameof(SelectedClient));
                    if (_SelectedClient != null)
                    {
                        Client = new Client
                        {
                            Id = _SelectedClient.Id,
                            Dni = _SelectedClient.Dni,
                            Name = _SelectedClient.Name,
                            LastName = _SelectedClient.LastName,
                            Email = _SelectedClient.Email,
                            Phone = _SelectedClient.Phone,
                            Address = _SelectedClient.Address
                        };
                    }
                    else
                    {
                        Client = new Client();
                    }
                } 
            }
        }

        public ObservableCollection<Client> Clients
        {
            get => _clients;
            set
            {
                if(_clients != value)
                {
                    _clients = value;
                    OnPropertyChanged(nameof(Clients));
                }
            }
        }

        private bool AddCanExecute(object obj)
        {
            return true;
        }

        private void AddExecute(object obj)
        {
            _clientRepository.Add(Client);
            LoadClients();
            Client = new Client();
        }

        private void DeleteExecute(object obj)
        {
            _clientRepository.Delete(Client);
            LoadClients();
            Client = new Client();
        }

        private bool DeleteCanExecute(object obj)
        {
            return SelectedClient != null;
        }

        private bool UpdateCanExecute(object obj)
        {
            return SelectedClient != null;
        }

        private void UpdateExecute(object obj)
        {
            _clientRepository.Update(Client);
            LoadClients();
            Client = new Client();
        }

        private void LoadClients()
        {
            _clients = _clientRepository.Get();
            OnPropertyChanged(nameof(Clients));
        }
    }
}
