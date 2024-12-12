using OrderCreator.ViewModel;

namespace OrderCreator.View
{
    internal class ConsoleView
    {
        private readonly MenuView _menu;
        private readonly ShowHistoryView _showHistory;
        private readonly CreateOrderView _createOrder;

        private readonly UniversalViewModel _viewModel;

        public ConsoleView(UniversalViewModel viewModel)
        {
            _viewModel = viewModel;

            _menu = new MenuView(
                onNewOrder: () => _createOrder.DrawCreateOrder(),
                onShowHistory: () => _showHistory.DrawOrderHistory(),
                onExit: this.onExit
            );

            _showHistory = new ShowHistoryView(
                onReturn: () => _menu.DrawMenu(),
                getHistory: _viewModel.GetHistory
            );

            _createOrder = new CreateOrderView(
                onReturn: () => _menu.DrawMenu(),
                getProducts: _viewModel.GetProducts,
                saveOrder: _viewModel.SaveOrder,
                applyDiscounts: _viewModel.ApplyDiscounts,
                getAllDiscounts: _viewModel.GetAllSpecialOffers
            );
        }

        private void onExit()
        {
            Environment.Exit(0);
        }

        public void StartUI()
        {
            _menu.DrawMenu();
        }


    }
}
