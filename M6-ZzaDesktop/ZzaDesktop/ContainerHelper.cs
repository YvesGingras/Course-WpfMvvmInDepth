﻿using Unity;
using Unity.Lifetime;
using ZzaDesktop.Services;

namespace ZzaDesktop
{
    public static class ContainerHelper
    {
        private static IUnityContainer _container;

        static ContainerHelper() {
            _container = new UnityContainer();
            _container.RegisterType<ICustomersRepository, CustomersRepository>(
                new ContainerControlledLifetimeManager());
        }

        public static IUnityContainer Container => _container;  
    }
}
