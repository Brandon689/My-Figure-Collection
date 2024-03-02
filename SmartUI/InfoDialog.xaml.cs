using System;
using System.Windows;
using System.Windows.Input;

namespace SmartUI
{
    public partial class InfoDialog : Window
    {
        internal InfoViewModel ViewModel { get; }

        public InfoDialog(InfoViewModel info)
        {
            InitializeComponent();
            ViewModel = info;//(InfoViewModel)this.DataContext;
            ;
            this.DataContext = ViewModel;
        }
        protected override Size MeasureOverride(Size availableSize)
        {
            portrait.Height = availableSize.Height; //- 35;
            return base.MeasureOverride(availableSize);
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                Close();
            }
            base.OnKeyDown(e);
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Console.WriteLine(ViewModel.Fig);
            Console.WriteLine(ViewModel.Fig.MFC);
            ;
            Clipboard.SetText(ViewModel.Fig.MFC.MFCId.ToString());
        }
    }
}
