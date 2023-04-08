using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using SirvaMe.Models;
using SirvaMe.Services;

namespace SirvaMe.ViewModels
{
    /// <summary>
    /// Professionals
    /// </summary>
    public class ProfissionaisVM : INotifyPropertyChanged
    {
        #region Propriedades

        private ObservableCollection<Servicos> _servicos;

        public ObservableCollection<Servicos> Servicos
        {
            get { return _servicos; }
            set
            {
                _servicos = value;
                RaisePropertyChanged("Servicos");
            }
        }

        private bool _isEmpty;

        public bool IsEmpty
        {
            get { return _isEmpty; }
            set
            {
                _isEmpty = value;
                RaisePropertyChanged("IsEmpty");
                RaisePropertyChanged("IsNotEmpty");
            }
        }

        public bool IsNotEmpty
        {
            get { return (!_isEmpty); }
        }

        private bool _isBusy;

        public bool IsBusy
        {
            get { return _isBusy; }
            set
            {
                _isBusy = value;
                RaisePropertyChanged("IsBusy");
            }
        }

        private bool _isError;

        public bool IsError
        {
            get { return _isError; }
            set
            {
                _isError = value;
                RaisePropertyChanged("IsError");
            }
        }

        private void RaisePropertyChanged(string name)
        {
            var pc = PropertyChanged;
            if (pc != null) pc(this, new PropertyChangedEventArgs(name));
        }

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        public ProfissionaisVM()
        {
            try
            {
                this.Servicos = new ObservableCollection<Servicos>();
                CarregarServicos2();
            }
            catch (Exception ex)
            {
                IsEmpty = true;
            }
        }
        
        private void CarregarServicos()
        {
            Task.Run(async () =>
            {
                try
                {
                    IsBusy = true;

                    var api = new ServicosApi();
                    var servicos = await api.GetServicosNaApiAsync();

                    if (servicos != null)
                    {
                        foreach (var servico in servicos)
                        {
                            this.Servicos.Add(servico);
                        }
                    }
                }
                catch (Exception ex)
                {
                    IsEmpty = true;
                }
                IsBusy = false;
            });
        }

        private void CarregarServicos2()
        {
            try
            {
                this.Servicos.Add(new Servicos { Id = 1, Nome = "Eletricista", LinkFoto = "João da Silva", Checado = true });
                this.Servicos.Add(new Servicos { Id = 2, Nome = "Esteticista", LinkFoto = "Pedro Cunha" });
                this.Servicos.Add(new Servicos { Id = 3, Nome = "Marceneiro", LinkFoto = "João da Silva 2", Checado = true });
                this.Servicos.Add(new Servicos { Id = 4, Nome = "Mecânico", LinkFoto = "Pedro Cunha 2" });
                this.Servicos.Add(new Servicos { Id = 5, Nome = "Pedreiro", LinkFoto = "João da Silva 3" });
                this.Servicos.Add(new Servicos { Id = 6, Nome = "Encanador", LinkFoto = "Pedro Cunha 3" });
            }
            catch (Exception ex)
            {

            }
        }
    }
}
