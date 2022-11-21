using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Collections.ObjectModel;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Core.Functions;
using Core.DataBase;

namespace KinolistKursach.Pages
{
	/// <summary>
	/// Interaction logic for MainPage.xaml
	/// </summary>
	public partial class MainPage : Page
	{
		public static User profil { get; set; }
		public static ObservableCollection<Film> filmList { get; set; }
		public MainPage(User user)
		{
			InitializeComponent();
			profil = user;
			filmList = FilmFunction.GetFilm();
			this.DataContext = this;
		}

		private void BtnProfilClick(object sender, RoutedEventArgs e)
		{
            NavigationService.Navigate(new ProfilPage(profil));
		}
        private void LvFilmsSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var isSelected = LvFilms.SelectedItem as Film;
            if (isSelected != null)
            {
                NavigationService.Navigate(new FilmPage(isSelected));
            }
        }

        private void TbSearchSelectionChanged(object sender, RoutedEventArgs e)
        {
            filmList = FilmFunction.GetFilm();
            if (TbSearch.Text != "")
            {
                filmList = new ObservableCollection<Film>(BdConnection.connection.Film.Where(a => a.Name.Contains(TbSearch.Text)).ToList());
            }

            if (filmList.Count == 0)
            {
                TbIsEmpty.Visibility = Visibility.Visible;
            }
            else
            {
                TbIsEmpty.Visibility = Visibility.Hidden;
            }
            LvFilms.ItemsSource = filmList;
        }
    }
}
