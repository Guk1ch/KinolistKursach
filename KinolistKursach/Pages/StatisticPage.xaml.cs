using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
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
using Excel = Microsoft.Office.Interop.Excel;
using Core.DataBase;
using Core.Functions;

namespace KinolistKursach.Pages
{
	/// <summary>
	/// Interaction logic for StatisticPage.xaml
	/// </summary>
	public partial class StatisticPage : Page
	{
		public static User profil { get; set; }
		public static User frienfUser { get; set; }
		public StatisticPage(User user)
		{
			InitializeComponent();
			profil = user;
			TbViewedCount.Text = TbViewedCount.Text + StatisticFunction.CountViewedFilm(user.ID);
			TbViewedTime.Text = TbViewedTime.Text + StatisticFunction.CountTimeViewedFilm(user.ID) + " мин.";
			this.DataContext = this;
		}
		private void BtnSaveClick(object sender, RoutedEventArgs e)
		{
			Exportcs export = new Exportcs();
			export.nikcname = profil.Nickname;
			export.countViewedFilm = (StatisticFunction.CountViewedFilm(profil.ID).ToString());
			export.countTimeViewedFilm = (StatisticFunction.CountTimeViewedFilm(profil.ID).ToString());
			var application = new Excel.Application();

			Excel.Workbook workbook = application.Workbooks.Add(Type.Missing);
			Excel.Worksheet worksheet = application.Worksheets.Item[1];
			int rowIndex = 2;
			worksheet.Name = $"Статистика";
			worksheet.Columns.AutoFit();
			worksheet.Rows.AutoFit();
			worksheet.Cells[1][1] = "Никнейм";
			worksheet.Cells[2][1] = "Количество просмотренных фильмов";
			worksheet.Cells[3][1] = "Всего времени за просмотром";


			worksheet.Cells[1][rowIndex] = export.nikcname;
			worksheet.Cells[2][rowIndex] = export.countViewedFilm;
			worksheet.Cells[3][rowIndex] = export.countTimeViewedFilm;

			application.Visible = true;




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


	
	}
}
