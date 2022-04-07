using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UrunlerApp.Model
{
    public class Urun:INotifyPropertyChanged
    {
        private int _id;
        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        private string _urunadi;
        public string Urunadi
        {
            get { return _urunadi; }
            set { _urunadi = value; }
        }

        private float _urunfiyat;
        public float Urunfiyat
        {
            get { return _urunfiyat; }
            set { _urunfiyat = value; }
        }

        private int _urunmiktar;
        public int Urunmiktar
        {
            get { return _urunmiktar; }
            set { _urunmiktar = value; }
        }

        private string _urunresim;
        public string Urunresim
        {
            get { return _urunresim; }
            set { _urunresim = value; PropertyDegişti(new PropertyChangedEventArgs("Urunresim"));}
        }

        public event PropertyChangedEventHandler PropertyChanged;


        public void PropertyDegişti(PropertyChangedEventArgs e)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, e);
            }
        }
    }
}
