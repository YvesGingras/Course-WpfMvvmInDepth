﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xaml;
using ZzaDesktop.Customers;
using ZzaDesktop.OrderPrep;
using ZzaDesktop.Orders;

namespace ZzaDesktop
{
    public class MainWindowViewModel :BindableBase
    {
        private CustomerListViewModel _customerListViewModel = new CustomerListViewModel();
        private OrderViewModel _orderViewModel = new OrderViewModel();
        private OrderPrepViewModel _orderPrepViewModel = new OrderPrepViewModel();

        private BindableBase _currentViewModel;

        public BindableBase CurrentViewModel {
            get { return _currentViewModel; }
            set { SetProperty(ref _currentViewModel,value);}
        }

        public RelayCommand<string> NavCommand { get; private set; }

        public MainWindowViewModel() {
            NavCommand = new RelayCommand<string>(OnNav);
        }

        private void OnNav(string destination) {
            switch (destination) {
                case "orderPrep":
                    CurrentViewModel = _orderPrepViewModel;
                    break;
                default:
                    CurrentViewModel = _customerListViewModel;
                    break;
            }
        }


    }
}