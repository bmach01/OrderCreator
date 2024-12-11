using OrderCreator.Model;
using OrderCreator.Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace OrderCreator.View
{
    internal class CreateOrderView
    {
        private readonly Action returnToMenu;
        private readonly Action<Order> saveOrder;
        private readonly Func<ReadOnlyCollection<Product>> getProducts;
        private readonly Func<Order, Order> applyDiscounts;

        public CreateOrderView(Action onReturn, Action<Order> saveOrder, Func<ReadOnlyCollection<Product>> getProducts, Func<Order, Order> applyDiscounts)
        {
            this.returnToMenu = onReturn;
            this.saveOrder = saveOrder;
            this.getProducts = getProducts;
            this.applyDiscounts = applyDiscounts;
        }

        public void DrawCreateOrder()
        {
            Console.Clear();
            Console.Write("Order title: ");

            string? titleInput = Console.ReadLine();
            Order newOrder;

            if (!string.IsNullOrEmpty(titleInput))
                newOrder = new Order(titleInput);
            else
                newOrder = new Order();

            Console.WriteLine("To add items to your list type number of the desired item.");
            Console.WriteLine("To remove items from your list type '-' and number of the item.");
            Console.WriteLine("If you wish to stop adding items, type 'X' or 'x'.\n");

            ReadOnlyCollection<Product> products = getProducts();
            for (int i = 0; i < products.Count; i++)
            {
                Console.WriteLine("\t" + (i + 1) + "> " + products[i].Name + " - " + string.Format("{0:0.00}", products[i].Price) + "PLN");
            }
            Console.WriteLine("\n\n");

            string? input = "";
            int number = 0;
            while (true)
            {
                input = Console.ReadLine();

                if (input == "X" || input == "y") break;

                try
                {
                    number = int.Parse(input);
                    if (number == 0) throw new Exception("There is no number at 0.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Please choose number from the list.");
                    continue;
                }

                if (number < 0)
                {
                    newOrder.RemoveItem(products[-number - 1]);
                    Console.WriteLine("Removed " + products[-number - 1].Name + " from the order.");
                }
                else
                {
                    newOrder.AddItem(products[number - 1]);
                    Console.WriteLine("Removed " + products[number - 1].Name + " to the order.");
                }

            }

            Console.Clear();
            Console.WriteLine("Checking for discounts...");
            newOrder = applyDiscounts(newOrder);
            Console.WriteLine("Complete!\n");

            Console.WriteLine(newOrder.Name + " | " + newOrder.Id.ToString());
            Console.WriteLine("Net sum: " + newOrder.Sum.ToString() + "PLN");

            Console.WriteLine("Items: ");
            foreach (Product product in newOrder.Items)
            {
                Console.WriteLine("\t" + product.Name + " - " + String.Format("{0:0.00}", product.Price) + "PLN");
            }
            Console.WriteLine("\nApplied discounts:");
            foreach(ISpecialOffer discount in newOrder.AppliedDiscounts)
            {
                Console.WriteLine("\t" + discount.Description);
            }

            bool continueLoop = true;
            while (continueLoop)
            {
                Console.WriteLine("Do you wish to send this order? (Y/N)");
                switch (Console.ReadKey().Key)
                {
                    case ConsoleKey.Y:
                        saveOrder(newOrder);
                        Console.WriteLine("Order sent successfully. Press any key to continue...");
                        Console.ReadKey();
                        continueLoop = false;
                        break;

                    case ConsoleKey.N: break;

                    default: continue;
                }
            }

            returnToMenu();
        }
    }
}
