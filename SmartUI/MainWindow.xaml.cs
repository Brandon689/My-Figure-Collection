using System.Windows;
using System.Windows.Controls;

namespace SmartUI
{
    public partial class MainWindow : Window
    {
        internal MainWindowViewModel ViewModel { get; }
        public MainWindow()
        {
            InitializeComponent();
            ViewModel = (MainWindowViewModel)this.DataContext;
            Closing += MainWindow_Closing;
        }

        private void MainWindow_Closing(object? sender, System.ComponentModel.CancelEventArgs e)
        {
            ViewModel.Closing();
        }
    }
}
