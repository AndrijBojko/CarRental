using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarRental.DAL.Repositories.Abstract;
using CarRental.DAL.Context;
using CarRental.Entities;
using CarRental.Entities.HelpClass;

namespace CarRental.DAL.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly CarRentalContext _context;

        public OrderRepository(CarRentalContext context)
        {
            _context = context;
        }

        public void AddOrder(int managerId, int carId, int customerId, DateTime startDateTime, DateTime finishDateTime)
        {
            _context.Orders.Add(new Order()
            {
                ManagerId = managerId,
                CarId = carId,
                CustomerId = customerId,
                StartDateTime = startDateTime,
                FinishDateTime = finishDateTime
            });

            _context.SaveChanges();
        }

        public void AddOrder(Order newOrder)
        {
            if (newOrder == null)
            {
                throw new ArgumentException("Trying to add null reference order entity");
            }
            _context.Orders.Add(newOrder);
            _context.SaveChanges();
        }



        public void UpdateOrderFinishById(int id, int managerId, DateTime finishDateTime)
        {
            var orderToUpdate = _context.Orders.Find(id);

            if (orderToUpdate == null)
            {
                throw new ArgumentException("No order with such id");
            }

            orderToUpdate.ManagerId = managerId;
            orderToUpdate.FinishDateTime = finishDateTime;

            _context.Entry(orderToUpdate).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void UpdateOrder(Order orderToUpdate)
        {
            if (orderToUpdate == null)
            {
                throw new ArgumentException("Trying to update null reference order entity");
            }
            _context.Orders.Attach(orderToUpdate);
            _context.Entry(orderToUpdate).State = EntityState.Modified;
        }

        public Order GetOrderById(int id)
        {
            var order = _context.Orders.Find(id);

            if (order == null)
            {
                throw new ArgumentException("No order with such id");
            }

            return order;
        }


        public List<ActiveOrder> GetAllActiveOrders()
        {
            var activeOrders = from or in _context.Orders
                               join cr in _context.Cars on or.CarId equals cr.Id
                               join cs in _context.Customers on or.CustomerId equals cs.Id
                               join mn in _context.Managers on or.ManagerId equals mn.Id
                               where or.FinishDateTime > DateTime.Now
                               //select or;
                                select new
                                {
                                    MangerLastName = mn.LastName,
                                    CarMake = cr.Make,
                                    CarModel = cr.Model,
                                    CustomerLastName = cs.LastName,
                                    CustomerFirstName = cs.FirstName,
                                    OrderFinishDateTime = or.FinishDateTime
                                };

            List<ActiveOrder> activeOrdersList = new List<ActiveOrder>();

            foreach (var order in activeOrders)
            {
                activeOrdersList.Add(new ActiveOrder()
                {
                    ManagerLName = order.MangerLastName,
                    CarMake = order.CarMake,
                    CarModel = order.CarModel,
                    CustomerFName = order.CustomerFirstName,
                    CustomerLName = order.CustomerLastName,
                    FinishDateTime = order.OrderFinishDateTime
                });
            }


            return activeOrdersList;
        }

        public void RemoveFinishedOrders()
        {
            _context.Orders.RemoveRange(_context.Orders.Where(x => x.FinishDateTime < DateTime.Now));
            _context.SaveChanges();
        }

    }
}
