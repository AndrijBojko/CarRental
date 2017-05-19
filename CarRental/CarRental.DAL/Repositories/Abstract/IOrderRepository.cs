using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarRental.Entities;
using CarRental.Entities.HelpClass;

namespace CarRental.DAL.Repositories.Abstract
{
    public interface IOrderRepository
    {
        void AddOrder(int managerId, int carId, int customerId, DateTime startDateTime, DateTime finishDateTime);
        void AddOrder(Order newOrder);
        void UpdateOrderFinishById(int id, int managerId,  DateTime finishDateTime);
        void UpdateOrder(Order orderToUpdate);
        Order GetOrderById(int orderId);
        void RemoveFinishedOrders();
        List<ActiveOrder> GetAllActiveOrders();

    }
}
