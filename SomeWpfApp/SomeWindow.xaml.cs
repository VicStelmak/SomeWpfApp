using System.Windows;

namespace SomeWpfApp
{
    /// <summary>
    /// Interaction logic for SomeWindow.xaml
    /// </summary>
    public partial class SomeWindow : Window
    {
        public string UserInput {  get; private set; }

        public SomeWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            UserInput = TextBox1.Text;
            Close();
        }
    }
}
