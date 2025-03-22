using System;
using System.Windows;

namespace SomeWpfApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private int _counter = 0;

        public MainWindow()
        {
            InitializeComponent();
            
            DataContext = new SomeViewModel();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            _counter++;

            TextBlock1.Text = _counter.ToString();
        }

        protected override void OnClosed(EventArgs arguments)
        {
            base.OnClosed(arguments);

            Application.Current.Shutdown();
        }
    }
}
