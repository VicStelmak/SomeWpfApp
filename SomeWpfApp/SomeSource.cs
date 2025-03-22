using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Threading;

namespace SomeWpfApp
{
    internal class SomeSource
    {
        private string _output;
        private readonly Dispatcher _dispatcher = Application.Current.Dispatcher;

        internal string GetData()
        {
            _output = ""; 

            var frame = new DispatcherFrame(); 

            _dispatcher.BeginInvoke(new Action(() =>
            {
                var someWindow = new SomeWindow();

                someWindow.Closed += (input, eventArgs) =>
                {
                    _output = someWindow.UserInput;

                    if (Application.Current.MainWindow?.DataContext is SomeViewModel viewModel)
                    {
                        var property = viewModel.GetType().GetProperty("Data");
                        property?.SetValue(viewModel, _output);

                        var textBlockToUpdate = "TextBlock2";
                        var mainWindow = Application.Current.MainWindow;
                        var manualBinding = BindingOperations.GetBindingExpression(mainWindow.FindName(textBlockToUpdate) as TextBlock, TextBlock.TextProperty);
                        manualBinding?.UpdateTarget();
                    }

                    frame.Continue = false; 
                };

                someWindow.Show();
            }));

            Dispatcher.PushFrame(frame); 

            return _output ?? ""; 
        }
    }
}
