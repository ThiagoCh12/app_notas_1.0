using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using AppLembrete.Models;
using AppLembrete.Services;

namespace AppLembrete.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageCadastrar : ContentPage
    {
        public PageCadastrar()
        {
            InitializeComponent();
        }

        public PageCadastrar(ModelNotas nota)
        {
            InitializeComponent();
            btSalvar.Text = "Alterar";
            txtCodigo.IsVisible = true;
            btExcluir.IsVisible = true;
            txtCodigo.Text = nota.Id.ToString();
            txtTitulo.Text = nota.Titulo;
            txtDados.Text = nota.Dados;
            swFavorito.IsToggled = nota.Favorito;
        }

        private void btSalvar_Clicked(object sender, EventArgs e)
        {
            try
            {
                ModelNotas notas = new ModelNotas();
                notas.Titulo = txtTitulo.Text;
                notas.Dados = txtDados.Text;
                notas.Favorito = swFavorito.IsToggled;
                ServiceDBNotas dbNotas = new ServiceDBNotas(App.DbPath);
                if(btSalvar.Text == "Inserir")
                {
                    dbNotas.Inserir(notas);
                    DisplayAlert("Resultado", dbNotas.StatusMessage, "ok");
                }
                else
                {
                    notas.Id = Convert.ToInt32(txtCodigo.Text);
                    dbNotas.Alterar(notas);
                    DisplayAlert("Nota alterada com sucesso", dbNotas.StatusMessage, "OK");

                }
                MasterDetailPage p = (MasterDetailPage)Application.Current.MainPage;
                p.Detail = new NavigationPage(new PageHome());
            }
            catch (Exception ex)
            {

                DisplayAlert("Erro", ex.Message, "OK");
            }
        }

        private async void btExcluir_Clicked(object sender, EventArgs e)
        {
            var resp = await DisplayAlert("Excluir nota", "Deseja excluir a nota selecionada?", "Sim", "Não");
            if(resp == true)
            {
                ServiceDBNotas dbNotas = new ServiceDBNotas(App.DbPath);
                int id = Convert.ToInt32(txtCodigo.Text);
                dbNotas.Excluir(id);
                DisplayAlert("Nota excluida com sucesso", dbNotas.StatusMessage, "ok");
                MasterDetailPage p = (MasterDetailPage)Application.Current.MainPage;
                p.Detail = new NavigationPage(new PageHome());
            }
        }

        private void btCancelar_Clicked(object sender, EventArgs e)
        {
            MasterDetailPage p = (MasterDetailPage)Application.Current.MainPage;
            p.Detail = new NavigationPage(new PageHome());
        }
    }
}