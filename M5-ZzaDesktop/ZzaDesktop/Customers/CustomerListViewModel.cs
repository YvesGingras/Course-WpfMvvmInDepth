﻿using System;
using System.Collections.ObjectModel;
using Zza.Data;
using ZzaDesktop.Services;

namespace ZzaDesktop.Customers
{
    public class CustomerListViewModel :BindableBase
    {
        private ICustomersRepository _repo = new CustomersRepository();
        private ObservableCollection<Customer> _customers;

        // <summary>To handled by the parent ViewModel, the <see cref="MainWindowViewModel"/>.</summary>
        public event Action<Guid> PlaceOrderRequested = delegate { };
        public event Action<Customer> AddCustomerRequested = delegate { };
         public event Action<Customer> EditCustomerRequested = delegate { };

        //public event Action<Guid>  PlaceOrderRequested = delegate { };
        //public event Action<Guid> PlaceOrderRequested = delegate { };
        public ObservableCollection<Customer> Customers {
            get { return _customers; }
            set { SetProperty(ref _customers,value); }
        }

        public RelayCommand<Customer> PlaceOrderCommand { get;}
        public RelayCommand AddCustomerCommand { get; }
        public RelayCommand<Customer> EditCustomerCommand { get; }


        public CustomerListViewModel() {
            PlaceOrderCommand = new RelayCommand<Customer>(OnPlaceOrder);
            AddCustomerCommand = new RelayCommand(OnAddCustomer);
            EditCustomerCommand = new RelayCommand<Customer>(OnEditCustomer);    
        }

        private void OnEditCustomer(Customer cust) {
            EditCustomerRequested(cust);    
        }

        private void OnAddCustomer() {
            AddCustomerRequested(new Customer{Id = Guid.NewGuid()});
        }

        private void OnPlaceOrder(Customer customer) {
            PlaceOrderRequested(customer.Id);
        }

        /// <summary>Triggered by the 'Loaded' event of the <see cref="CustomerListView"/>.
        /// It then populates the view the list of customers.</summary>
        public async void LoadCustomers() { 
            Customers = new ObservableCollection<Customer>(await _repo.GetCustomersAsync());
        }

    }
}

  