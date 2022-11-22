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
using System.Windows.Shapes;
using Core.Functions;
using Core.DataBase;
using System.Collections.ObjectModel;

namespace KinolistKursach.Pages
{
	/// <summary>
	/// Interaction logic for SellectionCollectionWindow.xaml
	/// </summary>
	public partial class SellectionCollectionWindow : Window
	{
		public int FilmId;
		public SellectionCollectionWindow(int FilmID)
		{
			InitializeComponent();
			FilmId = FilmID;
			ObservableCollection<Collection> collection = CollectionFunction.GetCollection(AuthorisPage.user.ID);
			ObservableCollection<Collection> trueCollection = new ObservableCollection<Collection>();
			foreach (var i in collection)
			{
				if (i.Name != "Просмотрено")
				{
					trueCollection.Add(i);
				}
			}
			CbCollection.ItemsSource = trueCollection;
			CbCollection.DisplayMemberPath = "Name";
		}
		private void BtnAddClick(object sender, RoutedEventArgs e)
		{
			if (CbCollection.SelectedIndex >= 0)
			{
				var FilmColl = new Film_Collection();
				var select = CbCollection.SelectedItem as Collection;
				FilmColl.ID_Collection = select.ID;
				FilmColl.ID_Film = FilmId;
				var isColl = BdConnection.connection.Film_Collection.Where(a => a.ID_Collection == select.ID && a.ID_Film == FilmId).Count();
				if (isColl == 0)
				{
					BdConnection.connection.Film_Collection.Add(FilmColl);
					BdConnection.connection.SaveChanges();
					this.DialogResult = true;
				}
				else
				{
					MessageBox.Show("Фильм уже добавлен в коллекцию", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
				}
			}
		}
		private void ImgPlusMouseDown(object sender, MouseButtonEventArgs e)
		{
			NewCollectionWindow newCollection = new NewCollectionWindow(AuthorisPage.user.ID);
			newCollection.ShowDialog();
			UpdateCollection();
		}
		public void UpdateCollection()
		{
			ObservableCollection<Collection> collection = CollectionFunction.GetCollection(AuthorisPage.user.ID);
			ObservableCollection<Collection> trueCollection = new ObservableCollection<Collection>();
			foreach (var i in collection)
			{
				if (i.Name != "Просмотрено")
				{
					trueCollection.Add(i);
				}
			}
			CbCollection.ItemsSource = trueCollection;
			CbCollection.DisplayMemberPath = "Name";
		}

	}
}
