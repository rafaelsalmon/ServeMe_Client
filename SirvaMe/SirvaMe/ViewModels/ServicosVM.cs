using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using SirvaMe.Models;
using SirvaMe.Services;

namespace SirvaMe.ViewModels
{
    /// <summary>
    /// Services
    /// </summary>
    public class ServicosVM : BaseVM
    {
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

        public ServicosVM()
        {
            try
            {
                this.Servicos = new ObservableCollection<Servicos>();
                CarregarServicos();
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
    }
}
