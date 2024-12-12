using OrderCreator.Model;
using System.Collections.ObjectModel;

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
            Console.Clear();
            Console.WriteLine("Fetching order history...");
            ReadOnlyCollection<Order> orders = getPreviousOrders();
            Console.WriteLine("Complete!\n");

            foreach (Order order in orders)
            {
                Console.WriteLine("========================");
                Console.WriteLine(order.Name + " | " + order.Id.ToString());
                Console.WriteLine("Sent on: " + order.Created.ToString());
                Console.WriteLine("Sum: " + String.Format("{0:0.00}", order.Sum) + "PLN");
                Console.WriteLine("Sum after discounts: " + String.Format("{0:0.00}", order.DiscountedSum) + "PLN");

                Console.WriteLine("Items: ");
                foreach (Product product in order.Items)
                {
                    Console.WriteLine("\t" + product.Name + " - " + String.Format("{0:0.00}", product.Price) + "PLN");
                }
                Console.WriteLine("\nApplied discounts:");
                foreach (ISpecialOffer discount in order.AppliedDiscounts)
                {
                    Console.WriteLine("\t" + discount.Description);
                }
                Console.Write("\n");
            }

            Console.WriteLine("\nPress any key to return...");
            Console.ReadKey();
            returnToMenu();
        }
    }
}
