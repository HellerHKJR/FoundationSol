using System.Windows.Input;

namespace WinFormWPFTest.ViewModel
{
    internal class DelegateCommand : ICommand
    {
        private object minus;

        public DelegateCommand(object minus)
        {
            this.minus = minus;
        }
    }
}