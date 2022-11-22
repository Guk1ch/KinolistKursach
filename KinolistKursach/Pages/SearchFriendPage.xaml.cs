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

namespace KinolistKursach.Pages
{
    /// <summary>
    /// Логика взаимодействия для SearchFriendPage.xaml
    /// </summary>
    public partial class SearchFriendPage : Page
    {
        public List<User> friends { get; set; }
        public SearchFriendPage()
        {
            InitializeComponent();
            friends = BdConnection.connection.User.Where(x => x.ID != AuthorisPage.user.ID).ToList();
            DataContext = this;
        }

        private void lv_friendColl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(lv_friendColl.SelectedItem != null)
            {
                var select = lv_friendColl.SelectedItem as User;
                NavigationService.Navigate(new FriendPage(select.ID));
            }
        }

        private void tb_searchFriend_SelectionChanged(object sender, RoutedEventArgs e)
        {
            List<User> users = BdConnection.connection.User.Where(x => x.Nickname.Contains(tb_searchFriend.Text.Trim())).ToList();
            lv_friendColl.ItemsSource = users;
        }

        private void BtnChat_Click(object sender, RoutedEventArgs e)
        {
            var senderButton = sender as Button;
            if (senderButton != null)
            {
                var user = senderButton.DataContext as User;
                var folow = BdConnection.connection.Follow.FirstOrDefault(x => x.ID_Follower_User == AuthorisPage.user.ID && x.ID_Following_User == user.ID);
                if (folow == null)
                {
                    Follow follow = new Follow();
                    follow.ID_Follower_User = AuthorisPage.user.ID;
                    follow.ID_Following_User = user.ID;
                    follow.Date_follow = DateTime.Now;
                    Follow follow1 = new Follow();
                    follow1.ID_Follower_User = user.ID;
                    follow1.ID_Following_User = AuthorisPage.user.ID;
                    follow1.Date_follow = DateTime.Now;
                    BdConnection.connection.Follow.Add(follow);
                    BdConnection.connection.Follow.Add(follow1);
                    BdConnection.connection.SaveChanges();
                    MessageBox.Show("Вы успешно подписались");
                }
                {
                    MessageBox.Show("Вы уже подписаны");
                }
            }
        }
    }
}
