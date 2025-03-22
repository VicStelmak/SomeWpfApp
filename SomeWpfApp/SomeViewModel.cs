using System.Windows.Input;

namespace SomeWpfApp
{
    public class SomeViewModel : ViewModelBase
    {
        public string Data { get; set; }

        public ICommand GetDataCommand 
        { 
            get
            {
                return new RelayCommand(() =>
                {
                    var ds = new SomeSource();
                    Data = ds.GetData();
                });
            }
        }
    }
}
