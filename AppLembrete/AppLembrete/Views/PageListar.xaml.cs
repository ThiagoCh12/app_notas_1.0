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
    public partial class PageListar : ContentPage
    {
        public PageListar()
        {
            InitializeComponent();
            AtualizaLista();
        }
        public void AtualizaLista()
        {
            String titulo = "";
            if(txtNota.Text != null) titulo = txtNota.Text;
            ServiceDBNotas dbNotas = new ServiceDBNotas(App.DbPath);
            if (swFavorito.IsToggled)
            {
                ListaNotas.ItemsSource = dbNotas.Localizar(titulo, true);
            }
            else
            {
                ListaNotas.ItemsSource = dbNotas.Localizar(titulo);
            }
        }

        private void swFavorito_Toggled(object sender, ToggledEventArgs e)
        {
            AtualizaLista();
        }

        private void btLocalizar_Clicked(object sender, EventArgs e)
        {
            AtualizaLista();
        }

        private void ListaNotas_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            ModelNotas nota = (ModelNotas)ListaNotas.SelectedItem;
            MasterDetailPage p = (MasterDetailPage)Application.Current.MainPage;
            p.Detail = new NavigationPage(new PageCadastrar(nota));
        }
    }
}