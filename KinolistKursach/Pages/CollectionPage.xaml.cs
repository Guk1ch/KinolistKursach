using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Core;
using Core.DataBase;
using Core.Functions;

namespace KinolistKursach.Pages
{
	/// <summary>
	/// Interaction logic for CollectionPage.xaml
	/// </summary>
	public partial class CollectionPage : Page
	{
		public static ObservableCollection<Film_Collection> filmsToFill { get; set; }
		public int IDCollection;
		public Collection updateCollection { get; set; }
		public CollectionPage(Collection collection)
		{
			InitializeComponent();
            updateCollection = collection;
            if (collection.Name == "Избранное" || collection.Name == "Просмотрено")
            {
                ImgRedaction.Visibility = Visibility.Hidden;
                ImgDelete.Visibility = Visibility.Hidden;
            }
            filmsToFill = CollectionFunction.GetFilmInCollection(collection.ID);
            if (filmsToFill.Count == 0)
            {
                TbIsEmpty.Visibility = Visibility.Visible;
            }
            else
            {
                TbIsEmpty.Visibility = Visibility.Hidden;
            }
            if (collection.Inkognito == true)
            {
                ImgInckognito.Source = new BitmapImage(new Uri("/img/locked.png", UriKind.Relative));
            }
            IDCollection = collection.ID;
            this.DataContext = this;
        }
        private void TbBackMouseDown(object sender, MouseButtonEventArgs e)
        {
            NavigationService.Navigate(new CollectionListPage());
        }

        private void TbBackMouseEnter(object sender, MouseEventArgs e)
        {
            TbBack.Foreground = new SolidColorBrush(Colors.White);
        }

        private void TbBackMouseLeave(object sender, MouseEventArgs e)
        {
            TbBack.Foreground = new SolidColorBrush(Colors.Black);
        }

        private void ImgRedactionMouseDown(object sender, MouseButtonEventArgs e)
        {
			EditCollectionWindow editCollection = new EditCollectionWindow(updateCollection.ID);
			editCollection.ShowDialog();
		}

        private void LvFilmSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Film selected = (LvFilm.SelectedItem as Film_Collection).Film;
            NavigationService.Navigate(new FilmPage(selected));
        }

        private void ImgDeleteMouseDown(object sender, MouseButtonEventArgs e)
        {
			DeleteWindow deleteWindow = new DeleteWindow();
			if (deleteWindow.ShowDialog() == true)
			{
				CollectionFunction.DeletedCollection(IDCollection);
				NavigationService.Navigate(new CollectionListPage());
			}
		}

        private void BtnDelClick(object sender, RoutedEventArgs e)
        {
            var senderButton = sender as Button;
            var film = senderButton.DataContext as Film_Collection;
            Collection delFilmColl = BdConnection.connection.Collection.Where(a => a.ID == film.ID_Collection).FirstOrDefault();

            if (delFilmColl.Name != "Просмотрено")
            {
                CollectionFunction.DeletedFilmInCollection(IDCollection, Convert.ToInt32(film.ID_Film));
                UpdateFilm();
            }
            else
            {
                MessageBox.Show("Просмотрено не доступно для редактирования");
            }
        }

        public void UpdateFilm()
        {
            if (updateCollection.Name == "Избранное" || updateCollection.Name == "Просмотрено")
            {
                ImgRedaction.Visibility = Visibility.Hidden;
                ImgDelete.Visibility = Visibility.Hidden;
            }
            filmsToFill = CollectionFunction.GetFilmInCollection(updateCollection.ID);
            if (filmsToFill.Count == 0)
            {
                TbIsEmpty.Visibility = Visibility.Visible;
            }
            else
            {
                TbIsEmpty.Visibility = Visibility.Hidden;
            }

            LvFilm.ItemsSource = filmsToFill;
        }

        private void ImgInckognitoMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (updateCollection.Inkognito != true)
            {
                CollectionFunction.InkognitoCollection(IDCollection);
                ImgInckognito.Source = new BitmapImage(new Uri("/img/locked.png", UriKind.Relative));
            }
            else
            {
                CollectionFunction.NotInkognitoCollection(IDCollection);
                ImgInckognito.Source = new BitmapImage(new Uri("/img/open.png", UriKind.Relative));
            }
        }
    }
}
