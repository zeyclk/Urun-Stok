using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;//veritabanı ile ilgili sınıf tanımlaması, biz yazdık
using System.Data.SqlClient;//Sql işlemleri için
using System.Windows;

namespace UrunlerApp.Model
{
    public class UrunlerProvider
    {
        public ObservableCollection<Urun> urunler_koleksiyonu;

        public UrunlerProvider()
        { 
            urunler_koleksiyonu=new ObservableCollection<Urun>();
        }

        //Data Source kısmında Server isminizi yazmanız gerekiyor!!!
        static string conString = @"Data Source=yourSERVER\SQLEXPRESS;Initial Catalog=dbStok;Integrated Security=True";
        
        //Sql Server başlangıç ekranında *.\SQLEXPRESS uzantısı server isminizdir
        //Integrated Security=True olursa Windows Authentication, Uid ve Passwords varsa false


        SqlConnection baglanti = new SqlConnection(conString); //Sql ile köprü olacak sınıfın tanımı

        public object DialogResult { get; private set; }

        public ObservableCollection<Urun> GetUrun()
        {
            try
            {
                if (baglanti.State == ConnectionState.Closed)

                baglanti.Open();
                String listeleme = "SELECT * from Urunler";
                SqlCommand komut = new SqlCommand(listeleme, baglanti);// sorguları yazacağımız komutların bulunduğu sınıf (sorgu,bağlantı)

                SqlDataReader kayit;//new belirtecine gerek yok, veri okuyucusu
                kayit = komut.ExecuteReader();//karşından gelen verileri kayit isimli nesnenin karşılaması için.

                while (kayit.Read() == true)
                {
                    //veritabanındaki kolon sırasına göre okunur ve değişkene atanır
                    Urun u = new Urun();
                    u.Id = Convert.ToInt32(kayit[0]);
                    u.Urunadi = kayit[1].ToString();
                    u.Urunfiyat = Convert.ToSingle(kayit[2]);
                    u.Urunmiktar = Convert.ToInt32(kayit[3]);
                    u.Urunresim = kayit[4].ToString();
                    urunler_koleksiyonu.Add(u);
                }

                baglanti.Close();//köprü mutlaka kaldırılmalıdır.
            }

            catch (Exception hata) { MessageBox.Show("Ürünler listelenmedi" + hata.Message); }
                
            return urunler_koleksiyonu;//kayıtları koleksiyon içine atamak
        }

        public int InsertUrun(Urun urn)
        {
            int sonuc = 0;

            try
            {
                if(baglanti.State==ConnectionState.Closed)

                baglanti.Open();
                string ekleme = "INSERT INTO Urunler(u_ad,u_fiyat,u_miktar,u_resim) VALUES(@uad,@ufiy,@umik,@ures)";
                SqlCommand komut = new SqlCommand(ekleme, baglanti);

                komut.Parameters.AddWithValue("@uad", urn.Urunadi);
                komut.Parameters.AddWithValue("@ufiy", urn.Urunfiyat);
                komut.Parameters.AddWithValue("@umik", urn.Urunmiktar);
                komut.Parameters.AddWithValue("@ures", urn.Urunresim);

                sonuc= komut.ExecuteNonQuery();
                baglanti.Close();

                urunler_koleksiyonu.Add(urn);
               
            }

            catch(Exception hata) { MessageBox.Show("Ürün ekleme başarısız" + hata.Message); }

            return sonuc;

        }

        public int DeleteUrun(Urun u1)
        {
            int sonuc = 0;

            try
            {
                if (baglanti.State == ConnectionState.Closed)

                    baglanti.Open();
                MessageBoxResult durum = MessageBox.Show(u1.Urunadi + " ürününü silmek istediğinizden emin misiniz?", "Ürün Silme", MessageBoxButton.YesNo,MessageBoxImage.Warning);
                if (durum == MessageBoxResult.Yes)
                {
                    string silme = "DELETE FROM Urunler WHERE u_id= @uid";
                    SqlCommand komut = new SqlCommand(silme, baglanti);
                    komut.Parameters.AddWithValue("uid", u1.Id);
                    sonuc = komut.ExecuteNonQuery();
                    baglanti.Close();
                    urunler_koleksiyonu.Remove(u1);
                }

            }

            catch (Exception hata) { MessageBox.Show("Ürün silme başarısız" + hata.Message); }
            return sonuc;
        }

        public int UpdateUrun(Urun un)
        {
            int sonuc = 0;
            try
            {
                if (baglanti.State == ConnectionState.Closed)

                    baglanti.Open();
                string güncelleme = "UPDATE Urunler SET u_ad=@uad, u_fiyat=@ufiy, u_miktar=@umik, u_resim=@ures WHERE u_id=@uid";
                SqlCommand komut = new SqlCommand(güncelleme, baglanti);

                komut.Parameters.AddWithValue("@uad", un.Urunadi);
                komut.Parameters.AddWithValue("@ufiy", un.Urunfiyat);
                komut.Parameters.AddWithValue("@umik", un.Urunmiktar);
                komut.Parameters.AddWithValue("@ures", un.Urunresim);
                komut.Parameters.AddWithValue("uid", un.Id);
                sonuc = komut.ExecuteNonQuery();
                baglanti.Close();
                urunler_koleksiyonu.Add(un);
             }

            catch (Exception hata) { MessageBox.Show("Ürün güncelleme başarısız" + hata.Message); }

            return sonuc;
        }
    }
}
