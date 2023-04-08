using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace SirvaMe.ViewModels
{
    /// <summary>
    /// Radio Button List Group - UI-targeted ViewModel for payment options
    /// </summary>
    public class RadioGroupVM : INotifyPropertyChanged
    {
        public RadioGroupVM()
        {
            listaCredito = new Dictionary<int, string>();
            listaDebito = new Dictionary<int, string>();
            listaDinheiro = new Dictionary<int, string>();
            selectedIndex = -1;
            LoadData();
        }

        private void LoadData()
        {
            ListaCredito.Add(0, "MASTERCARD");
            ListaCredito.Add(1, "VISA");
            ListaCredito.Add(2, "DINNERS CLUB");

            ListaDebito.Add(0, "MASTERCARD MAESTRO");
            ListaDebito.Add(1, "VISA ELECTRON");
            ListaDebito.Add(2, "ELO");

            ListaDinheiro.Add(0, "          ");
        }

        private Dictionary<int, string> listaDinheiro;
        public Dictionary<int, string> ListaDinheiro
        {
            get { return listaDinheiro; }
            set
            {
                listaDinheiro = value;
                NotifyPropertyChanged("ListaDinheiro");
            }
        }
        
        private Dictionary<int, string> listaCredito;
        public Dictionary<int, string> ListaCredito
        {
            get { return listaCredito; }
            set
            {
                listaCredito = value;
                NotifyPropertyChanged("ListaCredito");
            }
        }

        private Dictionary<int, string> listaDebito;
        public Dictionary<int, string> ListaDebito
        {
            get { return listaDebito; }
            set
            {
                listaDebito = value;
                NotifyPropertyChanged("ListaDebito");
            }
        }

        private int selectedIndex;
        public int SelectedIndex
        {
            get { return selectedIndex; }
            set
            {
                if (value == selectedIndex) return;
                selectedIndex = value;
                NotifyPropertyChanged("SelectedIndex");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyPropertyChanged(String propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (null != handler)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}