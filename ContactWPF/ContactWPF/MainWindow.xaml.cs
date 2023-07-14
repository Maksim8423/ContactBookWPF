using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ContactWPF
{
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        private ObservableCollection<Contact> contact;

        public ObservableCollection<Contact> Contact
        {
            get { return contact; }
            set { PropertyChangeMethod(out contact, value); }
        }

        string filePath = "Contacts.json";

        private Contact? selectedContact;
        public Contact? SelectedContact
        {
            get => selectedContact;
            set
            {
                this.PropertyChangeMethod(out selectedContact, value);
            }
        }

        public MainWindow()
        {
            InitializeComponent();

            this.DataContext = this;

            var loadedContact = this.LoadUsers();
            this.Contact = new ObservableCollection<Contact>(loadedContact);
            Contact = new ObservableCollection<Contact>(loadedContact);
            ContactListBox.ItemsSource = Contact;
        }

        protected void PropertyChangeMethod<T>(out T field, T value, [CallerMemberName] string propName = "")
        {
            field = value;

            if (this.PropertyChanged != null)
            {
                this.PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propName));
            }
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);

            this.SaveUsers();
        }

        public IEnumerable<Contact> LoadUsers()
        {
            string contactJson = File.ReadAllText(filePath);
            var contact = JsonSerializer.Deserialize<IEnumerable<Contact>>(contactJson);
            return contact;
        }

        public void SaveUsers()
        {
            string contactJson = JsonSerializer.Serialize(this.Contact);
            File.WriteAllText(filePath, contactJson);
        }

        public void AddContact_Click(object sender, RoutedEventArgs e)
        {
            ContactAdd contactAdd = new ContactAdd(Contact);
            contactAdd.ShowDialog();
        }
        public void DeleteContact_Click(object sender, RoutedEventArgs e)
        {
            Contact.RemoveAt(ContactListBox.SelectedIndex);
        }

        public void EditContact_Click(object sender, RoutedEventArgs e)
        {
            ContactEdit contactEdit = new ContactEdit(SelectedContact);
            contactEdit.ShowDialog();
        }
    }
}
