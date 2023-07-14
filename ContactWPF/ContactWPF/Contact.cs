using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ContactWPF
{
    public class Contact : INotifyPropertyChanged
    {
        private string? firstName;
        private string? secondName;
        private string? phoneNumber;

        public string? FirstName 
        { 
            get => this.firstName;
            set 
            {
                this.PropertyChangeMethod(out firstName, value);
            }
        }
        public string? SecondName
        {
            get => this.secondName;
            set
            {
                this.PropertyChangeMethod(out secondName, value);
            }
        }
        public string? PhoneNumber
        {
            get => this.phoneNumber;
            set
            {
                this.PropertyChangeMethod(out phoneNumber, value);
            }
        }

        private void PropertyChangeMethod<T>(out T field, T value, [CallerMemberName] string propName = "")
        {
            field = value;

            if (this.PropertyChanged != null)
            {
                this.PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propName));
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        [JsonIgnore]
        public string FullName => $"{FirstName} {SecondName}";

        

    }
}
