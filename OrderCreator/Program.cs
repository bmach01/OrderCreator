using OrderCreator.DAL;
using OrderCreator.View;
using OrderCreator.ViewModel;

namespace OrderCreator
{
    internal class Program
    {
        static void Main(string[] args)
        {
            UniversalViewModel vm = new UniversalViewModel(
                orderRepository: new OrderRepository(),
                productRepository: new ProductRepository(),
                specialOfferRepository: new SpecialOfferRepository()
            );

            ConsoleView consoleView = new ConsoleView(vm);

            consoleView.StartUI();
        }
    }
}