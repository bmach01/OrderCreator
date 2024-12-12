using OrderCreator.Model;
using System.Collections.ObjectModel;

namespace OrderCreator.View
{
    internal class CreateOrderView
    {
        private readonly Action returnToMenu;
        private readonly Action<Order> saveOrder;
        private readonly Func<ReadOnlyCollection<Product>> getProducts;
        private readonly Func<Order, Order> applyDiscounts;
        private readonly Func<ReadOnlyCollection<ISpecialOffer>> getAllSpecialOffers;

        public CreateOrderView(
            Action onReturn, 
            Action<Order> saveOrder, 
            Func<ReadOnlyCollection<Product>> getProducts, 
            Func<Order, Order> applyDiscounts,
            Func<ReadOnlyCollection<ISpecialOffer>> getAllDiscounts
        )
        {
            this.returnToMenu = onReturn;
            this.saveOrder = saveOrder;
            this.getProducts = getProducts;
            this.applyDiscounts = applyDiscounts;
            this.getAllSpecialOffers = getAllDiscounts;
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

            Console.WriteLine("\nTo add items to your list type number of the desired item.");
            Console.WriteLine("To remove items from your list type '-' and number of the item.");
            Console.WriteLine("If you wish to stop adding items, type 'X' or 'x'.\n");

            ReadOnlyCollection<Product> products = getProducts();
            for (int i = 0; i < products.Count; i++)
            {
                Console.WriteLine("\t" + (i + 1) + "> " + products[i].Name + " - " + string.Format("{0:0.00}", products[i].Price) + "PLN");
            }
            Console.WriteLine("\n\n");

            Console.WriteLine("Available Special Offers:");
            foreach (ISpecialOffer offer in getAllSpecialOffers())
            {
                Console.WriteLine(offer.Description);
            }

            Console.WriteLine("\n" + new string('=', 50));

            string? input = "";
            int number = 0;
            while (true)
            {
                input = Console.ReadLine();

                if (input == "X" || input == "x") break;

                try
                {
                    number = int.Parse(input);
                    if (number == 0 || Math.Abs(number) > products.Count) throw new Exception("Inocrrect index.");
                }
                catch (Exception)
                {
                    Console.WriteLine("Please choose number from the list.");
                    continue;
                }

                if (number < 0)
                {
                    var item = products[-number - 1];
                    if (!newOrder.HasItem(item))
                    {
                        Console.WriteLine("No such item in the order.");
                        continue;
                    }
                    newOrder.RemoveItem(item);
                    Console.WriteLine("Removed " + item.Name + " from the order.");
                }
                else
                {
                    var item = products[number - 1];
                    newOrder.AddItem(item);
                    Console.WriteLine("Added " + item.Name + " to the order.");
                }

            }

            if (newOrder.Items.Count <= 0)
            {
                Console.WriteLine("Empty orders are not permitted. Press any key to return to the menu...");
                Console.ReadKey();
                returnToMenu();
            }

            Console.Clear();
            Console.WriteLine("Applying viable discounts...\n");
            newOrder = applyDiscounts(newOrder);

            Console.WriteLine("Your order:\n" + new string('-', newOrder.Id.ToString().Length * 2 + 10));
            Console.WriteLine(newOrder.Name + " | " + newOrder.Id.ToString());
            Console.WriteLine("Sum: " + String.Format("{0:0.00}", newOrder.Sum) + "PLN");
            Console.WriteLine("Sum after discounts: " + String.Format("{0:0.00}", newOrder.DiscountedSum) + "PLN");

            Console.WriteLine("Items: ");
            foreach (Product product in newOrder.Items)
            {
                Console.WriteLine("\t" + product.Name + " - " + String.Format("{0:0.00}", product.Price) + "PLN");
            }
            Console.WriteLine("\nApplied discounts:");
            foreach (ISpecialOffer discount in newOrder.AppliedDiscounts)
            {
                Console.WriteLine("\t" + discount.Description);
            }

            bool continueLoop = true;
            while (continueLoop)
            {
                Console.WriteLine("\nDo you wish to send this order? (Y/N)");
                switch (Console.ReadKey().Key)
                {
                    case ConsoleKey.Y:
                        newOrder.SetNewSavedDate();
                        saveOrder(newOrder);
                        Console.WriteLine("\nOrder sent successfully. Press any key to continue...");
                        Console.ReadKey();
                        continueLoop = false;
                        break;

                    case ConsoleKey.N:
                        continueLoop = false;
                        break;

                    default: continue;
                }
            }

            returnToMenu();
        }
    }
}
