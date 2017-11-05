﻿using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using MVVMCommsDemo.Services;
using Zza.Data;

namespace MVVMCommsDemo.Customers
{
    public class CustomerListViewModel
    {
        private ICustomersRepository _repository = new CustomersRepository();
        private Customer _selectedCustomer;

        //M4-Demo 'Commands for View to ViewModel communication'
        public RelayCommand DeleteCommand { get; }

        public ObservableCollection<Customer> Customers { get; set; }

        public Customer SelectedCustomer {
            get => _selectedCustomer;
            set {
                _selectedCustomer = value;
                DeleteCommand.RaiseCanExecuteChanged();
            }
        }

        public CustomerListViewModel()
        {
            if (DesignerProperties.GetIsInDesignMode(
                new System.Windows.DependencyObject())) return;

            Customers = new ObservableCollection<Customer>( _repository.GetCustomersAsync().Result);
            DeleteCommand = new RelayCommand(OnDelete, CanDelete);
        }

        private bool CanDelete() {
            return SelectedCustomer != null;
        }

        private void OnDelete() {
            Customers.Remove(SelectedCustomer);

        }

       
    }
}
 