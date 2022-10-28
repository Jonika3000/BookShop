using EkzamenEF.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace EkzamenEF.Pages
{
    /// <summary>
    /// Логика взаимодействия для DeleteBook.xaml
    /// </summary>
    public partial class DeleteBook : Page
    {
        public DeleteBook()
        {
            InitializeComponent();
            SetComboBoxItems();
        }
        private void SetComboBoxItems()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var a = db.books.Select(a => a.title).ToList();
                CBBook.ItemsSource = a;
            }
        }
        private void ButtonDelete_Click(object sender, RoutedEventArgs e)
        {
            if(CBBook.SelectedItem != null)
            {
                using (ApplicationContext db = new ApplicationContext())
                {
                    db.books.Load();
                    db.books.Remove(
                        db.books.FirstOrDefault(b => b.title == CBBook.SelectedItem.ToString()));
                    db.SaveChanges();
                    CBBook.SelectedItem = null;
                    SetComboBoxItems();
                }
            }
        }
    }
}
