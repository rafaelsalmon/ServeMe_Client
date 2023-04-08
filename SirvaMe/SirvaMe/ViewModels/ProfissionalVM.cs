using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using SirvaMe.Models;
using SirvaMe.Services;

namespace SirvaMe.ViewModels
{
    /// <summary>
    /// Professional
    /// </summary>
    public class ProfissionalVM : BaseVM
    {
        private ObservableCollection<Avaliacao> _avaliacoes;
        public ObservableCollection<Avaliacao> Avaliacoes
        {
            get { return _avaliacoes; }
            set
            {
                _avaliacoes = value;
                RaisePropertyChanged("Avaliacoes");
            }
        }


        public ProfissionalVM(Propostas proposta)
        {
            try
            {
                this.Avaliacoes = new ObservableCollection<Avaliacao>();

                CarregaAvaliacoes(proposta.Prestador.Id);
            }
            catch (Exception ex)
            {
                IsEmpty = true;
            }
        }

        private void CarregaAvaliacoes(int prestadorId)
        {
            Task.Run(async () =>
            {
                try
                {
                    IsBusy = true;

                    var apiUsuario = new UsuarioApi();
                    var avaliacoes = await apiUsuario.GetAvaliacoesPrestadorNaApiAsync(prestadorId);

                    if (avaliacoes != null)
                    {
                        foreach (var avaliacao in avaliacoes)
                        {
                            this.Avaliacoes.Add(avaliacao);
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

        private async void CarregaAvaliacoes2(int prestadorId)
        {
            try
            {
                IsBusy = true;

                var apiUsuario = new UsuarioApi();
                var avaliacoes = await apiUsuario.GetAvaliacoesPrestadorNaApiAsync(prestadorId);

                if (avaliacoes != null && avaliacoes.Count > 0)
                {
                    
                }
                else 
                    IsEmpty = true;
            }
            catch (Exception ex)
            {
                IsEmpty = true;
            }
            IsBusy = false;
        }
    }
}
