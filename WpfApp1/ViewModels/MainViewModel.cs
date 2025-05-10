using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Linq;

namespace WpfApp1.ViewModels
{
  public partial  class MainViewModel:ObservableObject
    {
#if false
        //[ObservableProperty]
        //private string userName;
        [ObservableProperty]
        private string? name;
        //public string? Name
        //{
        //    get => name;
        //    set
        //    {
        //        if (!EqualityComparer<string?>.Default.Equals(name, value))
        //        {
        //            string? oldValue = name;
        //            OnNameChanging(value);
        //            OnNameChanging(oldValue, value);
        //            OnPropertyChanging();
        //            name = value;
        //            OnNameChanged(value);
        //            OnNameChanged(oldValue, value);
        //            OnPropertyChanged();
        //        }
        //    }
        //}

        //partial void OnNameChanging(string? value);
        //partial void OnNameChanged(string? value);

        //partial void OnNameChanging(string? oldValue, string? newValue);
        //partial void OnNameChanged(string? oldValue, string? newValue);
        partial void OnNameChanging(string? value)
        {
            Console.WriteLine($"Name is about to change to {value}");
        }

        partial void OnNameChanged(string? value)
        {
            Console.WriteLine($"Name has changed to {value}");
        }
#endif
        [ObservableProperty]
        private string? fullName;
        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(FullName))]
        private string? name;
        [RelayCommand]
        private void saveUser()
        {
            //MessageBox.Show("Save");
            Name= "Save";
        }
    }
   
}
