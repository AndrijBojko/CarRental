using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarRental.Entities;

namespace CarRental.DAL.Repositories.Abstract
{
    public interface IManagerRepository
    {
        Manager GetManagerByLogin(string login, string password);
    }

}
