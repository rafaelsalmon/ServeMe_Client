using System;
using SirvaMe.Models;
using SirvaMe.Services;
using Xamarin.Forms;

namespace SirvaMe.Views
{
    public partial class ProfissionalAvaliacaoPage : ContentPage
    {
        private readonly Propostas _propostas;

        public ProfissionalAvaliacaoPage(Propostas proposta)
        {
            InitializeComponent();

            _propostas = proposta;
            BindingContext = proposta;
        }

        private async void EnviarButton_OnClicked(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(ComentarioEditor.Text))
                {
                    await DisplayAlert("Alerta", "É necessário um comentário para a avaliação!", "OK");
                    return;
                }

                if (!await DisplayAlert("Info", "Deseja enviar a avaliação agora?", "Sim", "Não")) return;

                var api = new UsuarioApi();
                var faceBookId = App.Current.UserFacebookID;
                var avatarFacebook = !string.IsNullOrEmpty(faceBookId)
                                        ? $"https://graph.facebook.com/{faceBookId}/picture?type=small"
                                        : "icon_profile2.png";

                var nomes = App.Current.UserName.Split();

                var avaliacao = new Avaliacao
                {
                    ClienteId = App.Current.UserID,
                    ClienteNome = nomes[0],
                    ClienteAvatar = avatarFacebook,
                    PrestadorId = _propostas.Prestador.Id,
                    Nota = App.Rating,
                    Comentario = ComentarioEditor.Text,
                    AgendamentoId = _propostas.AgendamentoId
                };
                await api.PostAvaliaPrestadorNaApiAsync(avaliacao);
                await DisplayAlert("Info", "Avaliação enviada com sucesso!", "OK");

                App.Current.ShowMainPage(new AgendamentosListaPage());
            }
            catch (Exception ex)
            {
                await DisplayAlert("Erro", "Falha ao enviar avaliação!", "OK");
            }
        }

        private void ComentarioEditor_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            var comentario = ComentarioEditor.Text;

            if (comentario.Length > 200)
                ComentarioEditor.Text = comentario.Remove(comentario.Length - 1);
        }
    }
}
