using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CarRental.DAL.Context;
using CarRental.DAL.Repositories.Abstract;
using CarRental.Entities;

namespace CarRental.DAL.Repositories
{
    public class ManagerRepository : IManagerRepository
    {
        private readonly CarRentalContext _context;

        public ManagerRepository(CarRentalContext context)
        {
            _context = context;
        }

        public Manager GetManagerByLogin(string login, string password)
        {
            var manager = _context.Managers.FirstOrDefault(m => m.Login == login && m.Password == password);

            return manager;
        }
    }
}
