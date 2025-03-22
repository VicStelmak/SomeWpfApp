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
            _dispatcher.BeginInvoke(new Action(() => 
            { 
                var someWindow = new SomeWindow();
                someWindow.Closed += (input, eventArgs) =>
                {
                    _output = someWindow.UserInput;
                    
                    if (Application.Current.MainWindow != null)
                    {
                        var currentDataContext = Application.Current.MainWindow.DataContext;
                        var viewModel = (SomeViewModel)currentDataContext;

                        _dispatcher.Invoke(() =>
                        {
                            var mainWindow = Application.Current.MainWindow;
                            var property = viewModel.GetType().GetProperty("Data");

                            if (property != null) property.SetValue(viewModel, _output);

                            var textBlockToUpdate = "TextBlock2";
                            var manualBinding = BindingOperations.GetBindingExpression(mainWindow.FindName(textBlockToUpdate)
                                as TextBlock, TextBlock.TextProperty);

                            manualBinding?.UpdateTarget();
                        });

                        if (currentDataContext != null)
                        {
                            if (Application.Current.Windows.Count == 0) Application.Current.Shutdown();
                        }
                    }
                };

                someWindow.Show();
            }));

            while (_output == null) 
            {
                var currentlyOpenedWindows = Application.Current?.Windows;

                if (currentlyOpenedWindows == null || currentlyOpenedWindows.Count == 0) return "";

                var frame = new DispatcherFrame();
                _dispatcher.BeginInvoke(DispatcherPriority.Background, new DispatcherOperationCallback(LeaveFrame), frame);
                Dispatcher.PushFrame(frame);
            }

            return _output;
        }

        private object LeaveFrame(object frame)
        {
            ((DispatcherFrame) frame).Continue = false; 
            
            return null;
        }
    }
}
