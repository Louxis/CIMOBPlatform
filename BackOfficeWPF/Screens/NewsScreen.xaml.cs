using CIMOBProject.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Text;
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

namespace BackOfficeWPF {
    /// <summary>
    /// This class represents the functionalitys of the screen related to the news.
    /// This class contains an instance of applicationDbContext in order to access the data base.
    /// </summary>
    public partial class NewsScreen : UserControl {

        ApplicationDbContext _db = new ApplicationDbContext();
        
        public NewsScreen()
        {
            InitializeComponent();    
        }

        private void ButtonFirst_Click(object sender, RoutedEventArgs e) {
            newsGrd.Items.MoveCurrentToFirst();
        }

        private void ButtonPrevious_Click(object sender, RoutedEventArgs e) {
            newsGrd.Items.MoveCurrentToPrevious();
        }

        private void ButtonNext_Click(object sender, RoutedEventArgs e) {
            newsGrd.Items.MoveCurrentToNext();
        }

        private void ButtonLast_Click(object sender, RoutedEventArgs e) {
            newsGrd.Items.MoveCurrentToLast();
        }

        private void ButtonRemove_Click(object sender, RoutedEventArgs e) {
            News newsDelete = newsGrd.SelectedItem as News;
            if (newsDelete == null)
                return;

            if (MessageBox.Show("Deseja remover esta Noticia? (S/N)", "Remover esta Noticia?", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes) {
                _db.News.Remove(_db.News.Where(n => n.Id == newsDelete.Id).First());
                _db.SaveChanges();
            }
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e) {
            _db.News.Load();
            newsGrd.ItemsSource = _db.News.Local;
            newsGrd.SelectedIndex = 0;
            newsGrd.IsSynchronizedWithCurrentItem = true;
        }
    }
}
