using ContactsApp.Classes;
using SQLite;
using System.Windows;

namespace ContactsApp
{
    public partial class DetailsContactWindow : Window
    {
        Contact contact;
        public DetailsContactWindow(Contact contact)
        {
            InitializeComponent();
            this.contact = contact;

            nameTextBox.Text = contact.Name;
            emailTextBox.Text = contact.Email;
            phoneTextBox.Text = contact.Phone;
        }

        private void HandleUpdate(object sender, RoutedEventArgs e)
        {
            contact.Name = nameTextBox.Text;
            contact.Email = emailTextBox.Text;
            contact.Phone  = phoneTextBox.Text;
            using (SQLiteConnection connection = new SQLiteConnection(App.databasePath))
            {
                connection.CreateTable<Contact>();
                connection.Update(contact);
            }

            this.Close();
        }

        private void HandleDelete(object sender, RoutedEventArgs e)
        {
            using (SQLiteConnection connection = new SQLiteConnection(App.databasePath))
            {
                connection.CreateTable<Contact>();
                connection.Delete(contact);
            }

            this.Close();
        }
    }
}
