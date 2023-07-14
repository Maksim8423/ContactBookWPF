using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ContactWPF
{
    public partial class ContactAdd : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        private readonly ObservableCollection<Contact>? contacts;

        private string firstname;
        private string? lastname;
        private string phonenumber;


        public string Firstname
        {
            get { return this.firstname; }
            set
            {
                this.PropertyChangeMethod(out firstname, value);
            }
        }
        public string Lastname
        {
            get { return this.lastname; }
            set
            {
                this.PropertyChangeMethod(out lastname, value);
            }
        }
        public string Phonenumber
        {
            get { return this.phonenumber; }
            set
            {
                this.PropertyChangeMethod(out phonenumber, value);
            }
        }

        public ContactAdd()
        {
            InitializeComponent();
            
            this.DataContext = this;
        }
        public ContactAdd(ObservableCollection<Contact>? contacts) : this()
        {
            this.contacts = contacts;
        }

        protected void PropertyChangeMethod<T>(out T field, T value, [CallerMemberName] string propName = "")
        {
            field = value;

            if (this.PropertyChanged != null)
            {
                this.PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propName));
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            bool ErrorFlag = false;
            while (!ErrorFlag)
            {
                try
                {
                    if (string.IsNullOrWhiteSpace(this.FirstNameTextBox.Text))
                    {
                        ErrorFlag = true;
                        throw new ArgumentNullException(message: "Empty contact", new ArgumentNullException());
                    }
                    foreach(var symbol in this.PhoneNumberTextBox.Text)
                    {
                        if (symbol == '+')
                            continue;
                        if (!char.IsDigit(symbol))
                        {
                            ErrorFlag = true;
                            throw new ArgumentException(message: "Incorrect number");
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());

                }
                if (!ErrorFlag)
                {
                    this.contacts?.Add(new Contact()
                    {
                        FirstName = this.Firstname,
                        SecondName = this.Lastname,
                        PhoneNumber = this.Phonenumber
                    });
                    this.Close();
                    break;
                }
            }


        }

        

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Show();
            this.Close();
        }
    }
}
