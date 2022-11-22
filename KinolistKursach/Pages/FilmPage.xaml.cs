using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
using Core.Functions;
using Core.DataBase;
using Prism.Commands;

namespace KinolistKursach.Pages
{
	/// <summary>
	/// Interaction logic for FilmPage.xaml
	/// </summary>
	public partial class FilmPage : Page
	{
		public Film filmToFill { get; set; }
		private void NavHomeView(object ID)
		{
			if (ID is string destinationurl)
				System.Diagnostics.Process.Start(filmToFill.Film_link);
		}
		public string ExternalURL { get => filmToFill.Film_link; }
		private readonly ICommand navHomeViewCommand;
		public ICommand NavHomeViewCommand
		{
			get { return navHomeViewCommand; }
		}
		public FilmPage(Film film)
		{
			InitializeComponent();
			filmToFill = film;
			navHomeViewCommand = new DelegateCommand<object>(NavHomeView);
			TbDuration.Text = filmToFill.Duration + " мин.";
			if (CollectionFunction.Viewed(AuthorisPage.user.ID, filmToFill.ID))
			{
				CbWatch.IsChecked = true;
			}
			this.DataContext = this;
		}
		private void TbBackMouseDown(object sender, MouseButtonEventArgs e)
		{
			NavigationService.GoBack();
		}

		private void TbBackMouseEnter(object sender, MouseEventArgs e)
		{
			TbBack.Foreground = new SolidColorBrush(Colors.White);
		}

		private void TbBackMouseLeave(object sender, MouseEventArgs e)
		{
			TbBack.Foreground = new SolidColorBrush(Colors.Black);
		}

		private void ImgPlusMouseDown(object sender, MouseButtonEventArgs e)
		{
			SellectionCollectionWindow selectionCollectionWindow = new SellectionCollectionWindow(filmToFill.ID);
			selectionCollectionWindow.ShowDialog();
		}

		private void CbWatchChecked(object sender, RoutedEventArgs e)
		{
			CollectionFunction.ViewedFilm(AuthorisPage.user.ID, filmToFill.ID);
		}

		private void CbWatchUnchecked(object sender, RoutedEventArgs e)
		{
			CollectionFunction.UnviewedFilm(AuthorisPage.user.ID, filmToFill.ID);
		}


	}
}
