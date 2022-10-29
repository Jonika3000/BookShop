﻿using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using EkzamenEF.Models;
using EkzamenEF.Helpers;
using Microsoft.EntityFrameworkCore;

namespace EkzamenEF.Pages
{
    /// <summary>
    /// Логика взаимодействия для AllBooks.xaml
    /// </summary>
    public partial class AllBooks : Page
    {
        string search;
        List<Book> books;
        public AllBooks()
        {
            InitializeComponent();
            AllSet();
        }
        public AllBooks(string search, bool isGenre)
        {
            InitializeComponent();
            this.search = search;
            if (isGenre)
            {
                GenreSet();
            }
            else
            {
                NameSet();
            }
        }
        private void AllSet()
        {
            WrapPanelBooks.Children.Clear();
            using (ApplicationContext db = new ApplicationContext())
            {
                db.books.Load();
                foreach (var b in db.books)
                {
                    WrapPanelBooks.Children.Add(CreateBook(b));
                }
            }
        }
        private void NameSet()
        {
            WrapPanelBooks.Children.Clear();
            using (ApplicationContext db = new ApplicationContext())
            {
                db.books.Load();
                books = db.books.Where(b => b.title.Contains(search)).ToList();
            }
            if(books.Count > 0)
            {
                foreach (var b in books)
                {
                    WrapPanelBooks.Children.Add(CreateBook(b));
                }
            }
           else
            {
                MessageBox.Show("Nothing found","Info",MessageBoxButton.OK,MessageBoxImage.Information);
                AllSet();
            }
        }
        private void GenreSet()
        {
            WrapPanelBooks.Children.Clear();
            using (ApplicationContext db = new ApplicationContext())
            {
                db.books.Load();
                books = db.books.Where(b => b.genre == search).ToList();
            }
            foreach(var b in books)
            {
                WrapPanelBooks.Children.Add(CreateBook(b));
            }
        }
        private Button CreateBook(Book book )
        {
            var b = new Button();
            b.Style = Resources["BookButton"] as Style;
            var stack = new StackPanel();
            var img = new Image();
            var text = new TextBlock();

            img.Source = ImageConverter.ConvertByteArrayToBitmapImage(book.photo).Source;
            img.Height = 150;
            img.Stretch = System.Windows.Media.Stretch.Uniform;

            text.FontSize = 15;
            text.FontWeight = FontWeights.Regular;
            text.HorizontalAlignment = HorizontalAlignment.Center;
            text.Text = book.title;

            stack.Children.Add(img);
            stack.Children.Add(text);
            b.Content = stack;

            return b;
        }  
    }
}
