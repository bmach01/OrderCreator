using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderCreator.View
{
    internal class InputHandler
    {
        private readonly ConsoleView _consoleView;

        public InputHandler()
        {
            _consoleView = new ConsoleView();
        }

        public InputHandler(ConsoleView consoleView)
        {
            _consoleView = consoleView;
        }

        private void menuFlow()
        {

        }

        private void editOrderFlow()
        {

        }
    }
}
