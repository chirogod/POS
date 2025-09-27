using POS.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace POS.ViewModels
{
    internal class MainWindowViewModel : BaseViewModel
    {
        private BaseViewModel _currentViewModel;
        public BaseViewModel CurrentViewModel
        {
            get => _currentViewModel;
            set
            {
                _currentViewModel = value;
                OnPropertyChanged(nameof(CurrentViewModel));
            }
        }

        // Comandos de navegación
        public ICommand NavigateToDashBoardCommand { get; }
        public ICommand NavigateToClientsCommand { get; }
        public ICommand NavigateToProductsCommand { get; }
        public ICommand NavigateToSalesCommand { get; }

        public MainWindowViewModel()
        {
            // Inicializar los comandos
            NavigateToClientsCommand = new RelayCommand(o => CurrentViewModel = new ClientViewModel());
            NavigateToProductsCommand = new RelayCommand(o => CurrentViewModel = new ProductViewModel());
            NavigateToSalesCommand = new RelayCommand(o => CurrentViewModel = new SaleViewModel());
            NavigateToDashBoardCommand = new RelayCommand(o => CurrentViewModel = new DashBoardViewModel());

            // Vista inicial
            CurrentViewModel = new ClientViewModel();
        }
    }
}
