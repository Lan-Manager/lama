using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lama.Logic.Model
{
    public class Statistique : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public int Index { get; set; }
        public string Key { get; set; }

        private int? _value;
        public int? Value
        {
            get
            {
                return _value;
            }
            set
            {
                if (value != _value)
                {
                    _value = value;
                    NotifyPropertyChanged("Value");
                }
            }
        }


        public Statistique()
        {
            Key = "n/a";
            Value = -1;
            Index = -1;
        }

        public Statistique(string key, int index)
        {
            Key = key;
            Value = 0;
            Index = index;
        }

        public void NotifyPropertyChanged(string nomProp)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nomProp));
        }
    }
}
