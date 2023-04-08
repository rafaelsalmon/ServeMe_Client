using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using SirvaMe.Models;

namespace SirvaMe.ViewModels
{
    /// <summary>
    /// Services List
    /// </summary>
    public class ServicosListaVM : BaseVM
    {
        private ObservableCollection<ServicosDisponiveis> _servicosDisponiveis;

        public ObservableCollection<ServicosDisponiveis> ServicosDisponiveis
        {
            get { return _servicosDisponiveis; }
            set
            {
                _servicosDisponiveis = value;
                RaisePropertyChanged("ServicosDisponiveis");
            }
        }

        public ServicosListaVM(int prestadorId)
        {
            try
            {
                ServicosDisponiveis = new ObservableCollection<ServicosDisponiveis>();
                CarregaServicosDisponiveis();
            }
            catch (Exception ex)
            {
                IsEmpty = true;
            }
        }

        private void CarregaServicosDisponiveis()
        {
            try
            {
                ServicosDisponiveis.Add(new ServicosDisponiveis { Id = 1, NomeServico = "Troca de Lâmpada", TipoPrestador = "Eletricista", DataServico = DateTime.Now, ServicoConfirmadoCliente = true });
                ServicosDisponiveis.Add(new ServicosDisponiveis { Id = 2, NomeServico = "Vazamento na Pia", TipoPrestador = "Encanador", DataServico = DateTime.Now, ServicoConfirmadoCliente = true });
                ServicosDisponiveis.Add(new ServicosDisponiveis { Id = 3, NomeServico = "Troca de Fiação", TipoPrestador = "Eletricista", DataServico = DateTime.Now, ServicoConfirmadoCliente = true });
                ServicosDisponiveis.Add(new ServicosDisponiveis { Id = 4, NomeServico = "Válvula Hidra com Vazamento", TipoPrestador = "Encanador", DataServico = DateTime.Now, ServicoConfirmadoCliente = true });
            }
            catch (Exception ex)
            {

            }
        }

        //private void CarregarServicos(int prestadorId)
        //{
        //    Task.Run(async () =>
        //    {
        //        try
        //        {
        //            IsBusy = true;

        //            var api = new UsuarioApi();
        //            var servicos = await api.GetServicosNaApiAsync();

        //            if (servicos != null)
        //            {
        //                var servicosPrestador = await api.GetServicosPrestadorNaApiAsync(prestadorId);

        //                foreach (var item in servicos)
        //                {
        //                    var selecionado = (servicosPrestador != null && servicosPrestador.Count(ss => ss.Id == item.Id) > 0);

        //                    this.Servicos.Add(new Servicos
        //                    {
        //                        Id = item.Id,
        //                        Nome = item.Nome,
        //                        LinkFoto = item.LinkFoto,
        //                        Checado = selecionado
        //                    });
        //                }
        //            }
        //        }
        //        catch (Exception e)
        //        {
        //            IsEmpty = true;
        //        }
        //        IsBusy = false;
        //    });
        //}
    }
}
