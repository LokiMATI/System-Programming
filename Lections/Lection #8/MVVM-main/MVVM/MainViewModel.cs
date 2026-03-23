using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MVVM
{
    public partial class MainViewModel: ViewModelBase
    {
        [ObservableProperty]
        public string searchString = String.Empty;
        
        [ObservableProperty]
        public List<FileInfo> files = new List<FileInfo>();

        public ICommand SearchCommand { get; set; }

        public MainViewModel()
        {
            SearchCommand = new RelayCommand<string>(Search, (x)=>!String.IsNullOrWhiteSpace(x));
        }

        public void Search(string searchString)
        {
            DirectoryInfo directoryInfo = new DirectoryInfo(searchString);

            if (directoryInfo.Exists)
            {
                Files = directoryInfo.GetFiles("",SearchOption.AllDirectories).ToList();
            }
        }
    }
}
