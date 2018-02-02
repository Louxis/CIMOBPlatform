using BackOfficeWPF.Dialogs;
using CIMOBProject.Models;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace BackOfficeWPF {
    /// <summary>
    /// This class represents the functionalitys of the screen related to the bilateral protocols.
    /// This class contains an instance of applicationDbContext in order to access the data base.
    /// </summary>
    public partial class BilateralProtocolScreen : UserControl {

        private ApplicationDbContext _db = new ApplicationDbContext();
        private ObservableCollection<BilateralProtocol> protocols = new ObservableCollection<BilateralProtocol>();

        public BilateralProtocolScreen()
        {
            InitializeComponent();
        }

        private void ButtonFirst_Click(object sender, RoutedEventArgs e) {
            bilateralGrd.Items.MoveCurrentToFirst();
        }

        private void ButtonPrevious_Click(object sender, RoutedEventArgs e) {
            bilateralGrd.Items.MoveCurrentToPrevious();
        }

        private void ButtonNext_Click(object sender, RoutedEventArgs e) {
            bilateralGrd.Items.MoveCurrentToNext();
        }

        private void ButtonLast_Click(object sender, RoutedEventArgs e) {
            bilateralGrd.Items.MoveCurrentToLast();
        }

        private void ButtonAdd_Click(object sender, RoutedEventArgs e) {
            BilateralDialog bilateralDialog = new BilateralDialog() { Title = "Adicionar Acordo" };
            if (bilateralDialog.ShowDialog() == true) {
                protocols.Add(DbContextHelper.AddBilateral(_db, bilateralDialog.BilateralProtocol));
                bilateralGrd.SelectedIndex = bilateralGrd.Items.Count - 1;
            }
        }

        private void ButtonEdit_Click(object sender, RoutedEventArgs e) {
            BilateralProtocol bilateralProtocolEdit = bilateralGrd.SelectedItem as BilateralProtocol;
            if (bilateralProtocolEdit == null)
                return;
            BilateralDialog bilateralDialog = new BilateralDialog(new BilateralProtocol(bilateralProtocolEdit)) { Title = "Editar Acordo" };
            if (bilateralDialog.ShowDialog() == true) {
                DbContextHelper.EditBilateral(_db, bilateralDialog.BilateralProtocol);
                bilateralGrd.Items.Refresh();
            }
        }

        private void ButtonRemove_Click(object sender, RoutedEventArgs e) {
            BilateralProtocol bilateralDisable = bilateralGrd.SelectedItem as BilateralProtocol;
            if (MessageBox.Show("Deseja remover este acordo da lista de escolhas ? (S / N)", "Deseja remover este acordo da lista de escolhas? (S/N)", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes) {
                _db.BilateralProtocols.Where(b => b.Id == bilateralDisable.Id).First().OpenSlots = 99;
                _db.SaveChanges();
                bilateralDisable.OpenSlots = 99;
            }
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e) {
            _db.BilateralProtocols.Load();
            protocols = _db.BilateralProtocols.Local;
            bilateralGrd.ItemsSource = protocols;
            bilateralGrd.SelectedIndex = 0;
            bilateralGrd.IsSynchronizedWithCurrentItem = true;
        }
    }
}
