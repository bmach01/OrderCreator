using OrderCreator.Model;
using OrderCreator.Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderCreator.View
{
    internal class ShowHistoryView
    {
        private Action returnToMenu;
        private Func<ReadOnlyCollection<Order>> getPreviousOrders;

        public ShowHistoryView(Action onReturn, Func<ReadOnlyCollection<Order>> getHistory)
        {
            returnToMenu = onReturn;
            getPreviousOrders = getHistory;
        }

        public void DrawOrderHistory()
        {
            Console.WriteLine("Fetching order history...");
            ReadOnlyCollection<Order> orders = getPreviousOrders();
            Console.WriteLine("Complete!\n");

            foreach (Order order in orders)
            {
                Console.WriteLine("========================\n");
                Console.WriteLine(order.Name + " | " + order.Id.ToString());
                Console.WriteLine("Sent on: " + order.Created.ToShortDateString());
                Console.WriteLine("Net sum: " + order.Sum.ToString() + "PLN");

                Console.WriteLine("Items: ");
                foreach(Product product in order.Items)
                {
                    Console.WriteLine("\t" + product.Name + " - " + String.Format("{0:0.00}", product.Price) + "PLN");
                }
                Console.WriteLine("\nApplied discounts:");
                foreach (ISpecialOffer discount in order.AppliedDiscounts)
                {
                    Console.WriteLine("\t" + discount.Description);
                }
            }

            Console.WriteLine("\nTo return press Enter...");
            Console.ReadKey();
            returnToMenu();
        }
    }
}
