namespace OrderCreator.View
{
    internal class MenuView
    {
        private readonly Action goToOrderCreator;
        private readonly Action goToOrderHistory;
        private readonly Action exitApp;

        public MenuView(
            Action onNewOrder,
            Action onShowHistory,
            Action onExit
        )
        {
            goToOrderCreator = onNewOrder;
            goToOrderHistory = onShowHistory;
            exitApp = onExit;
        }

        public void DrawMenu()
        {
            Console.Clear();
            Console.WriteLine(
                "OrderCreator\n==============\n" +

                "N> New Order\n" +
                "H> Show history of the order\n" +
                "X> Exit\n" +

                "\nEnter the instruction character (e.g. X> - enter 'x' or 'X') to maneuver the program.\n"
            );

            while (true)
            {
                switch (Console.ReadKey().Key)
                {
                    case ConsoleKey.N:
                        goToOrderCreator();
                        return;

                    case ConsoleKey.H:
                        goToOrderHistory();
                        return;

                    case ConsoleKey.X:
                        exitApp();
                        return;

                    default: break;
                }
            }
        }
    }
}
