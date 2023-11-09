using ContactsApp.Classes;
using SQLite;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace ContactsApp
{
    public partial class MainWindow : Window
    {
        List<Contact> contacts;

        public MainWindow()
        {
            contacts = new List<Contact>();

            InitializeComponent();
            ReadDatabase();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NewContactWindow newContactWindow = new NewContactWindow();
            newContactWindow.ShowDialog();

            ReadDatabase();
        }

        private void ReadDatabase()
        {
            using (SQLiteConnection connection = new SQLiteConnection(App.databasePath))
            {
                connection.CreateTable<Contact>();
                contacts = connection.Table<Contact>().ToList().OrderBy(c => c.Name).ToList();
            }

            if (contacts == null) return;

            ContactsLV.ItemsSource = contacts;
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox searchTB = sender as TextBox;

            var filteredList = contacts.Where(c => c.Name.ToLower().Contains(searchTB.Text.ToLower())).ToList();
            ContactsLV.ItemsSource = filteredList;
        }

        private void HandleOpenContactDetails(object sender, SelectionChangedEventArgs e)
        {
            Contact selectedContact = (Contact) ContactsLV.SelectedItem;
            if(selectedContact == null) return;

            DetailsContactWindow detailsContactWindow = new DetailsContactWindow(selectedContact);
            detailsContactWindow.ShowDialog();
            ReadDatabase();
        }
    }
}
