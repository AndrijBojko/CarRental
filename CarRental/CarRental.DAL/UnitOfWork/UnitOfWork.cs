using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarRental.Entities;
using CarRental.DAL.Context;
using CarRental.DAL.Repositories;
using CarRental.DAL.Repositories.Abstract;

namespace CarRental.DAL.UnitOfWork
{
    public class UnitOfWork :IDisposable
    {
        private readonly CarRentalContext _context = new CarRentalContext();

        private CarRepository _carRepository;
        private CustomerRepository _customerRepository;
        private ManagerRepository _managerRepository;
        private OrderRepository _orderRepository;

        public ICarRepository CarRepository => _carRepository ?? (_carRepository = new CarRepository(_context));

        public ICustomerRepository CustomerRepository => _customerRepository ?? (_customerRepository = new CustomerRepository(_context));

        public IManagerRepository ManagerRepository => _managerRepository ?? (_managerRepository = new ManagerRepository(_context));

        public IOrderRepository OrderRepository => _orderRepository ?? (_orderRepository = new OrderRepository(_context));


        public void Save()
        {
            _context.SaveChanges();
        }

        private bool _disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
