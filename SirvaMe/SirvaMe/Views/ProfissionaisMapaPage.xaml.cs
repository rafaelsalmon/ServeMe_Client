using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using SirvaMe.Models;
using SirvaMe.Services;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace SirvaMe.Views
{
    public partial class ProfissionaisMapaPage : ContentPage
    {
        private readonly AgendamentoInfo _agendamentoInfo;

        public ProfissionaisMapaPage(AgendamentoInfo agendamentoInfo)
        {
            InitializeComponent();

            _agendamentoInfo = agendamentoInfo;
            GeraMapaPelaPosicaoDosPrestadores(agendamentoInfo.PrestadorLocation, agendamentoInfo.Endereco.Latitude, agendamentoInfo.Endereco.Longitude);

            //GeraMapaPelaPosicaoDosPrestadores(agendamentoInfo.TipoServicoId, agendamentoInfo.Endereco.Latitude, agendamentoInfo.Endereco.Longitude);
            //GeraMapaPelaPosicaoDosPrestadores2(2, -26.9114602, -49.0935426);
            //GeraMapaPelaPosicaoDosPrestadores3(agendamentoInfo.PrestadoresListaMapa, -26.9114602, -49.0935426);
        }

        private void GeraMapaPelaPosicaoDosPrestadores(List<PrestadorLocation> prestadoresLocation, double latCliente, double lonCliente)
        {
            try
            {
                var prestadoresLista = prestadoresLocation.Select(item => new CustomPin
                {
                    Pin = new Pin { Position = new Position(item.Latitude, item.Longitude) }
                }).ToList();

                CustomMap.CustomPins = prestadoresLista;
                CustomMap.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(latCliente, lonCliente), Distance.FromMeters(3000)));
            }
            catch (Exception ex)
            {
                DisplayAlert("Profissionais", "Falha ao carregar!", "OK");
            }
        }

        private async void GeraMapaPelaPosicaoDosPrestadores1(int tipoServicoId, double latCliente, double lonCliente)
        {
            try
            {
                var prestadoresLista = new List<CustomPin>();
                var api = new UsuarioApi();
                var prestadores = await api.GetPrestadoresPorTipoServicoIdNaApiAsync(tipoServicoId, latCliente.ToString().Replace(",", "."), lonCliente.ToString().Replace(",", "."));

                #region Mocks

                //var prestadores = new List<PrestadorLocation>
                //{
                //    new PrestadorLocation {Latitude = -26.9147336, Longitude = -49.0799566},
                //    new PrestadorLocation {Latitude = -26.9116147, Longitude = -49.0934704}
                //};

                //var prestadores = new List<PrestadorLocation>
                //{
                //    new PrestadorLocation {Latitude = "-26.9147336", Longitude = "-49.0799566"},
                //    new PrestadorLocation {Latitude = "-26.9116147", Longitude = "-49.0934704"}
                //};

                #endregion

                if (prestadores != null)
                {
                    prestadoresLista.AddRange(prestadores.Select(prestador => new CustomPin
                    {
                        Pin = new Pin
                        {
                            Position = new Position(double.Parse(prestador.Latitude.ToString().Replace(",", "."), new CultureInfo("en-US")),
                                                    double.Parse(prestador.Longitude.ToString().Replace(",", "."), new CultureInfo("en-US")))
                        }
                    }));
                    //prestadoresLista.Add(new CustomPin { Pin = new Pin { Position = new Position(latCliente - 0.0005000, lonCliente - 0.0004000) } });
                    //prestadoresLista.Add(new CustomPin { Pin = new Pin { Position = new Position(latCliente + 0.0008000, lonCliente - 0.0003000) } });
                    CustomMap.CustomPins = prestadoresLista;
                }
                CustomMap.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(latCliente, lonCliente), Distance.FromMeters(200)));
            }
            catch (Exception ex)
            {

            }
        }

        private void GeraMapaPelaPosicaoDosPrestadores2(int tipoServicoId, double latCliente, double lonCliente)
        {
            try
            {
                const double lat = -26.9147336;
                const double lon = -49.0799566;

                var prestadoresLista = new List<CustomPin>
                {
                    new CustomPin
                    {
                        Pin = new Pin
                        {
                            Position = new Position(lat, lon)
                        }
                    },
                    new CustomPin
                    {
                        Pin = new Pin
                        {
                            Position = new Position(-26.9116147, -49.0934704)
                        }
                    }
                };
                CustomMap.CustomPins = prestadoresLista;
                CustomMap.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(latCliente, lonCliente), Distance.FromMeters(200)));
            }
            catch (Exception ex)
            {

            }
        }

        private async void GeraMapaPelaPosicaoAtual()
        {
            try
            {
                //var gpsPosition = await GetPositions();
                ////var gpsPosition = new Position(-26.9114602, -49.0935426);

                //var pin = new CustomPin
                //{
                //    Pin = new Pin
                //    {
                //        Type = PinType.Place,
                //        Position = new Position(gpsPosition.Latitude, gpsPosition.Longitude),
                //        Label = "Usuario",
                //        Address = "João da Silva"
                //    },
                //    Id = "Xamarin",
                //    Url = "http://xamarin.com/about/"
                //};

                //CustomMap.CustomPins = new List<CustomPin> { pin };
                //CustomMap.Pins.Add(pin.Pin);
                //CustomMap.MoveToRegion(MapSpan.FromCenterAndRadius(gpsPosition, Distance.FromMeters(300)));


                //var gpsPosition = await GetPositions();
                //var lat = gpsPosition.Latitude;
                //var lon = gpsPosition.Longitude;

                //MyMap.MoveToRegion(MapSpan.FromCenterAndRadius(gpsPosition, Distance.FromMeters(300)));

                //MyMap.Pins.Add(new Pin { Position = gpsPosition, Label = "Pin 1", Type = PinType.Generic });
                //MyMap.Pins.Add(new Pin { Position = new Position(gpsPosition.Latitude - 0.0005000, gpsPosition.Longitude - 0.0004000), Label = "Pin 2", Type = PinType.Place });
                //MyMap.Pins.Add(new Pin { Position = new Position(gpsPosition.Latitude + 0.0008000, gpsPosition.Longitude - 0.0003000), Label = "Pin 3", Type = PinType.SavedPin });
                //MyMap.Pins.Add(new Pin { Position = new Position(gpsPosition.Latitude - 0.0013000, gpsPosition.Longitude - 0.0002000), Label = "Pin 4", Type = PinType.SearchResult });
            }
            catch (Exception ex)
            {

            }
        }

        private async void ContinuarOnButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new TermosDeUsoPage(_agendamentoInfo));
        }
    }
}
