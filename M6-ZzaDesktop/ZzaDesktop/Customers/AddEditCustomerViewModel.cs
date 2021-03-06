﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zza.Data;
using ZzaDesktop.Services;

namespace ZzaDesktop.Customers
{
    public class AddEditCustomerViewModel :BindableBase
    {
        private bool _editMode;
        private ICustomersRepository _repo;
        private SimpleEditableCustomer _customer;
        private Customer _editingCustomer;

        public AddEditCustomerViewModel(ICustomersRepository repo) {
            _repo = repo;
            CancelCommand = new RelayCommand(OnCancel);
            SaveCommand = new RelayCommand(OnSave,CanSave);
        }

        public event Action Done = delegate { };

        public bool EditMode {
            get => _editMode;
            set => SetProperty(ref _editMode, value);
        }
        public SimpleEditableCustomer Customer {
            get => _customer;
            set => SetProperty(ref _customer,value);
        }
        public RelayCommand CancelCommand { get; private set; }
        public RelayCommand SaveCommand { get; private set; }   

        public void SetCustomer(Customer cust) {
            _editingCustomer = cust;
            if (Customer != null) Customer.ErrorsChanged -= RaiseCanExecuteChanged;
            Customer = new SimpleEditableCustomer();
            Customer.ErrorsChanged += RaiseCanExecuteChanged;
            CopyCustomer(cust, Customer);
        }

        private void RaiseCanExecuteChanged(object sender, EventArgs e) {
            SaveCommand.RaiseCanExecuteChanged();
        }

        private void CopyCustomer(Customer source, SimpleEditableCustomer target)  {
            target.Id = source.Id;
            if (!EditMode) return;
            target.FirstName = source.FirstName;
            target.LastName = source.LastName;
            target.Phone = source.Phone;
            target.Email = source.Email;
        }

        private bool CanSave() {
            return !Customer.HasErrors;
        }
        
        private async void OnSave() {
            UpdateCustomer(Customer, _editingCustomer);
            if (EditMode)
                await _repo.UpdateCustomerAsync(_editingCustomer);
            else
                await _repo.AddCustomerAsync(_editingCustomer);
            Done();
        }

        private void UpdateCustomer(SimpleEditableCustomer source, Customer target) {
            target.FirstName = source.FirstName;
            target.LastName = source.LastName;
            target.Phone = source.Phone;
            target.Email = source.Email;
        }


        private void OnCancel() {
            Done();
        }
    }
}
