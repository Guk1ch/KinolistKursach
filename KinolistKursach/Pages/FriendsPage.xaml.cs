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
using Core.DataBase;
using Core.Functions;
using System.Collections.ObjectModel;

namespace KinolistKursach.Pages
{
	/// <summary>
	/// Interaction logic for FriendsPage.xaml
	/// </summary>
	public partial class FriendsPage : Page
	{
		public static ObservableCollection<Follow> follow { get; set; }
		public int IDCollection;
		public Collection updateCollection { get; set; }
        public List<Follow> follows { get; set; }
		public FriendsPage()
		{
			InitializeComponent();
            follows = BdConnection.connection.Follow.Where(x => x.ID_Follower_User == AuthorisPage.user.ID).ToList();
            DataContext = this;
		}

        private void BtnChatClick(object sender, RoutedEventArgs e)
        {

        }
        private void TextBlockMouseDown(object sender, MouseButtonEventArgs e)
        {
            NavigationService.Navigate(new ProfilPage(AuthorisPage.user));
        }

        private void TextBlockMouseEnter(object sender, MouseEventArgs e)
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

        private void LvFriendsSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int selected = (int)(LvFriends.SelectedItem as Follow).ID_Following_User;
            NavigationService.Navigate(new FriendPage(selected));
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

    }
}
