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
using System.IO;
using Core.DataBase;
using Core.Functions;


namespace KinolistKursach.Pages
{
    /// <summary>
    /// Логика взаимодействия для FriendCollectionPage.xaml
    /// </summary>
    public partial class FriendCollectionPage : Page
    {
        public static ObservableCollection<Collection> collectionsFriend { get; set; }
        public static ObservableCollection<User> users { get; set; }
        public static User profil { get; set; }
        public FriendCollectionPage()
        {
            InitializeComponent();
            //ObservableCollection<User> allUser = FriendFunction.GetUsers();
            //CbSearchFriend.ItemsSource = allUser;
            //CbSearchFriend.DisplayMemberPath = "Nickname";
            profil = AuthorisPage.user;
            this.DataContext = this;
        }
        private void CbSearchFriendSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CbSearchFriend.SelectedItem != null)
            {
                LvFriendColl.Visibility = Visibility.Visible;
                ImgPhoto.Visibility = Visibility.Visible;
                TbFriendName.Visibility = Visibility.Visible;
                BtnFollow.Visibility = Visibility.Visible;
                ObservableCollection<Collection> friendCollections = CollectionFunction.GetFriendCollection((CbSearchFriend.SelectedItem as User).ID);
                User frienduser = CbSearchFriend.SelectedItem as User;
                BitmapImage bd = ToBitmapImage(frienduser.Photo);
                ImgPhoto.Source = bd;
                TbFriendName.Text = frienduser.Nickname;
                LvFriendColl.ItemsSource = friendCollections;
            }
        }
        private void LvFriendCollSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Collection collection = LvFriendColl.SelectedItem as Collection;
            if (collection != null)
            {
                //NavigationService.Navigate(new FilmInCollectionFriendsPage(collection));
            }
        }
        public static BitmapImage ToBitmapImage(byte[] data)
        {
            if (data != null)
            {
                using (MemoryStream ms = new MemoryStream(data))
                {

                    BitmapImage img = new BitmapImage();
                    img.BeginInit();
                    img.CacheOption = BitmapCacheOption.OnLoad;
                    img.StreamSource = ms;
                    img.EndInit();

                    if (img.CanFreeze)
                    {
                        img.Freeze();
                    }
                    return img;
                }
            }
            else
            {
                return null;
            }
        }
        private void BtnFollowClick(object sender, RoutedEventArgs e)
        {
            var follower = new Follow();
            User frienduser = CbSearchFriend.SelectedItem as User;
            follower.ID_Following_User = frienduser.ID;
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
