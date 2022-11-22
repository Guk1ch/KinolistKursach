using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Collections.ObjectModel;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Core.DataBase;
using Core.Functions;

namespace KinolistKursach.Pages
{
	/// <summary>
	/// Interaction logic for FriendPage.xaml
	/// </summary>
	public partial class FriendPage : Page
	{
		public static ObservableCollection<Collection> collections { get; set; }
		public static User profil { get; set; }
		public FriendPage(int user)
		{
			InitializeComponent();
			profil = BdConnection.connection.User.FirstOrDefault(x => x.ID == user);
			collections = CollectionFunction.GetFriendCollection(profil.ID);
			LvUserColl.ItemsSource = collections;
			var follower = BdConnection.connection.Follow.Where(z => z.ID_Follower_User == AuthorisPage.user.ID && z.ID_Following_User == profil.ID).FirstOrDefault();

			if (follower == null)
			{
				BtnFollow.Visibility = Visibility.Visible;
			}

			if (profil.ID == AuthorisPage.user.ID)
			{
				BtnFollow.Visibility = Visibility.Hidden;
			}

			this.DataContext = this;
		}

        private void BtnWriteClick(object sender, RoutedEventArgs e)
        {

        }
		private void LvUserCollSelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			Collection collection = LvUserColl.SelectedItem as Collection;
			if (collection != null)
			{
				//NavigationService.Navigate(new FilmInCollectionFriendsPage(collection));
			}
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
		private void BtnFollowClick(object sender, RoutedEventArgs e)
		{
			var follower = new Follow();
			follower.ID_Following_User = profil.ID;
			follower.ID_Follower_User = AuthorisPage.user.ID;
			follower.Date_follow = DateTime.Now;
			var isFolvr = BdConnection.connection.Follow.Where(a => a.ID_Following_User == follower.ID_Following_User && a.ID_Follower_User == follower.ID_Follower_User).Count();
			if (isFolvr == 0)
			{
				BdConnection.connection.Follow.Add(follower);
				BdConnection.connection.SaveChanges();
				MessageBox.Show("Вы успешно подписались");

			}
			else
			{
				MessageBox.Show("Вы уже подписаны на этого пользователя", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
			}
		}
	}
}

