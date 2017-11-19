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


        public ObservableCollection<Customer> Customers {
            get { return _customers; }
            set { SetProperty(ref _customers,value); }
        }

        public RelayCommand<Customer> PlaceOrderCommand { get; private set; }


        public CustomerListViewModel() {
            PlaceOrderCommand = new RelayCommand<Customer>(OnPlaceOrder);    
        }

        private void OnPlaceOrder(Customer customer) {

        }

        public async void LoadCustomers() {
            Customers = new ObservableCollection<Customer>(await _repo.GetCustomersAsync());
        }

    }
}

 