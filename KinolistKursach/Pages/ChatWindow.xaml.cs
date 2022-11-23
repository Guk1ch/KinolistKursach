using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Core.DataBase;
using Core.Functions;


namespace KinolistKursach.Pages
{
    /// <summary>
    /// Логика взаимодействия для ChatWindow.xaml
    /// </summary>
    public partial class ChatWindow : Window
    {
        public List<MessInDialog> MessInDialog { get; set; }
        public User profil { get; set; }
        public ChatWindow(User user)
        {
            InitializeComponent();
            profil = user;
            MessInDialog = BdConnection.connection.MessInDialog.Where(x => x.Follow.ID_Follower_User == AuthorisPage.user.ID && x.Follow.ID_Following_User == user.ID).ToList();
            DataContext = this;

            System.Windows.Threading.DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
            dispatcherTimer.Tick += new EventHandler(timer1_Tick);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 5);
            dispatcherTimer.Start();

        }
        void timer1_Tick(object sender, EventArgs e)
        {
            Update();
        }
        private void BtnSendClick(object sender, RoutedEventArgs e)
        {
            if(TbChat.Text.Trim().Length != 0)
            {
                Message message = new Message();
                message.IdSenderUser = AuthorisPage.user.ID;
                message.TextMessage = TbChat.Text.Trim();
                message.Date = DateTime.Now;
                BdConnection.connection.Message.Add(message);
                MessInDialog messInDialogs = new MessInDialog();
                messInDialogs.IdMessage = message.Id;
                messInDialogs.IdFollow = BdConnection.connection.Follow.FirstOrDefault(x => x.ID_Follower_User == AuthorisPage.user.ID && x.ID_Following_User == profil.ID).ID;
                MessInDialog messInDialog = new MessInDialog();
                messInDialog.IdMessage = message.Id;
                messInDialog.IdFollow = BdConnection.connection.Follow.FirstOrDefault(x => x.ID_Follower_User == profil.ID && x.ID_Following_User == AuthorisPage.user.ID).ID;
                BdConnection.connection.MessInDialog.Add(messInDialogs);
                BdConnection.connection.MessInDialog.Add(messInDialog);
                BdConnection.connection.SaveChanges();
                TbChat.Text = "";
                Update();
            }
        }

        public void Update()
        {
            MessInDialog = BdConnection.connection.MessInDialog.Where(x => x.Follow.ID_Follower_User == AuthorisPage.user.ID && x.Follow.ID_Following_User == profil.ID).ToList();
            LvChat.ItemsSource = MessInDialog;
        }
    }
}
