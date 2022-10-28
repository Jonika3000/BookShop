using EkzamenEF.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace EkzamenEF.Pages
{
    /// <summary>
    /// Логика взаимодействия для DeleteAuthor.xaml
    /// </summary>
    public partial class DeleteAuthor : Page
    {
        public DeleteAuthor()
        {
            InitializeComponent();
            SetComboBoxItems();
        }
        private void SetComboBoxItems()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var a = db.authors.Select(a => a.name).ToList();
                CBBook.ItemsSource = a;
            }
        }

        private void ButtonDelete_Click(object sender, RoutedEventArgs e)
        {
            if (CBBook.SelectedItem != null)
            {
                using (ApplicationContext db = new ApplicationContext())
                {
                    db.authors.Load();
                    db.authors.Remove(
                        db.authors.FirstOrDefault(b => b.name == CBBook.SelectedItem.ToString()));
                    db.SaveChanges();
                    CBBook.SelectedItem = null;
                    SetComboBoxItems();
                }
            }
        }
    }
}
