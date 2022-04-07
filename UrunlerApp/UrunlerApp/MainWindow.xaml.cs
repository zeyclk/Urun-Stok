using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
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
using UrunlerApp.Model;

namespace UrunlerApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        UrunlerProvider db;
        private void btn_getir_Click(object sender, RoutedEventArgs e)
        {
            db = new UrunlerProvider();
            dgrid.ItemsSource = db.GetUrun();
        }

        Urun secilenUrun;
        private void dgrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            secilenUrun = (Urun)dgrid.SelectedItem;
            layoutGrid.DataContext = secilenUrun;
        }

        private void btn_sil_Click(object sender, RoutedEventArgs e)
        {
            db.DeleteUrun(secilenUrun);

            db = new UrunlerProvider();
            dgrid.ItemsSource = db.GetUrun();
            dgrid.SelectedIndex = 0;
        }

        private void btn_güncelle_Click(object sender, RoutedEventArgs e)
        {
            db.UpdateUrun(secilenUrun);

            db = new UrunlerProvider();
            dgrid.ItemsSource = db.GetUrun();
            dgrid.SelectedIndex = 0;
        }

        private void btn_yeni_Click(object sender, RoutedEventArgs e)
        {
            secilenUrun = new Urun();
            layoutGrid.DataContext = secilenUrun;
        }

        private void resim_ac_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Title = "Resim Seç";
            dialog.Filter = "JPG Dosyası|*.jpg;*.jpeg;*.png;";
            if (dialog.ShowDialog() == true)
            {
                secilenUrun.Urunresim = dialog.FileName;
            }
        }

        private void btn_ekle_Click(object sender, RoutedEventArgs e)
        {
            db.InsertUrun(secilenUrun);

            db = new UrunlerProvider();
            dgrid.ItemsSource = db.GetUrun();
            dgrid.SelectedIndex = 0;
        }

        



       
    }
}
