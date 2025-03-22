using System;
using System.Threading.Tasks;
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

            StartUiResponsivenessTestAsync();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            _counter++;

            TextBlock1.Text = _counter.ToString();
        }

        private async void StartUiResponsivenessTestAsync()
        {
            int count = 0;

            while (true) 
            { 
                TextBlock3.Text = $"Проверка интерфейса {count++}.";
                await Task.Delay(500);
            }
        }

        protected override void OnClosed(EventArgs arguments)
        {
            base.OnClosed(arguments);

            Application.Current.Shutdown();
        }
    }
}
