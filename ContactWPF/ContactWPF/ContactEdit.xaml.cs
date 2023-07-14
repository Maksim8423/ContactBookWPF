using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
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
    /// <summary>
    /// Логика взаимодействия для ContactEdit.xaml
    /// </summary>
    public partial class ContactEdit : Window, INotifyPropertyChanged
    {
        private ObservableCollection<Contact>? contacts;
        public ObservableCollection<Contact> Contacts
        {
            get => contacts;
            set
            {
                contacts = value;
                PropertyChangeMethod(out contacts, value);
            }
        }
        public event PropertyChangedEventHandler? PropertyChanged;

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

        protected void PropertyChangeMethod<T>(out T field, T value, [CallerMemberName] string propName = "")
        {
            field = value;

            if (this.PropertyChanged != null)
            {
                this.PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propName));
            }
        }
        Contact receiveContact;


        public ContactEdit(Contact contact)
        {
            InitializeComponent();
            receiveContact = contact;
            this.FNBOX.Text = this.receiveContact.FirstName;
            this.LNBOX.Text = this.receiveContact.SecondName;
            this.PNBOX.Text = this.receiveContact.PhoneNumber;
        }


        public ContactEdit(ObservableCollection<Contact>? contacts) : this()
        {
            this.contacts = contacts;
        }

        public ContactEdit()
        {

        }


        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            bool ErrorFlag = false;
            while (!ErrorFlag)
            {
                try
                {
                    if (string.IsNullOrWhiteSpace(this.FNBOX.Text))
                    {
                        ErrorFlag = true;
                        throw new ArgumentNullException(message: "Empty contact", new ArgumentNullException());
                    }
                    foreach (var symbol in this.PNBOX.Text)
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
                    this.receiveContact.FirstName = this.FNBOX.Text;
                    this.receiveContact.SecondName = this.LNBOX.Text;
                    this.receiveContact.PhoneNumber = this.PNBOX.Text;
                    this.contacts?.Add(new Contact()
                    {
                        FirstName = this.receiveContact.FirstName,
                        SecondName = this.receiveContact.SecondName,
                        PhoneNumber = this.receiveContact.PhoneNumber
                    });
                    this.Close();
                    break;
                }
               
            }

        }
        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
