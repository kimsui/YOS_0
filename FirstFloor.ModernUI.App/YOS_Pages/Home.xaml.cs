using FirstFloor.ModernUI.App.Content;
using FirstFloor.ModernUI.App.YOS_Pages.Course;
using FirstFloor.ModernUI.App.YOS_Pages.Course.Complete_Pages;
using FirstFloor.ModernUI.Windows.Controls;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FirstFloor.ModernUI.App.YOS_Pages
{
    /// <summary>
    /// Interaction logic for Introduction.xaml
    /// </summary>
    public partial class Home : UserControl
    {
        public Home()
        {
            InitializeComponent();

			using (FileStream fStream = File.Open("NoticeData.xaml", FileMode.Open))
			{
				FlowDocument doc = XamlReader.Load(fStream) as FlowDocument;
				this.lblNotice.Content = doc;
			}
		}

		private void SetCustomMenu(object sender, RoutedEventArgs e)
		{
			MenuItem selectedMenuItem = (MenuItem)sender;
			MenuItem parentMenuItem = (MenuItem)selectedMenuItem.Parent;

			Button btn = ((ContextMenu)parentMenuItem.Parent).PlacementTarget as Button;
			btn.Content = selectedMenuItem.Header;
			btn.Command = NavigationCommands.GoToPage;

			switch ((string)btn.Content)
			{
				case "진행 중 강좌":
					btn.CommandParameter = "/YOS_Pages/Course/Ongoing.xaml";
					break;
				case "전체 완료 강좌":
					btn.CommandParameter = "/YOS_Pages/Course/Complete_Pages/Whole.xaml";
					break;
				case "파트너별 완료 강좌":
					btn.CommandParameter = "/YOS_Pages/Course/Complete_Pages/Patners.xaml";
					break;
				case "역할별 완료 강좌":
					btn.CommandParameter = "/YOS_Pages/Course/Complete_Pages/Role.xaml";
					break;
				case "강좌별 수수료 현황":
					btn.CommandParameter = "/YOS_Pages/Status/CostStatus_Pages/Course.xaml";
					break;
				case "파트너별 수수료 현황":
					btn.CommandParameter = "/YOS_Pages/Status/CostStatus_Pages/Patners.xaml";
					break;
				case "파트너":
					btn.CommandParameter = "/YOS_Pages/Reference/Reference.xaml";
					break;
				case "항목별 요율":
					btn.CommandParameter = "/YOS_Pages/Reference/Reference_Pages/ItemRate.xaml";
					break;
				case "강좌 분류":
					btn.CommandParameter = "/YOS_Pages/Reference/Reference_Pages/CourseCategory.xaml";
					break;
				case "교구 단가":
					btn.CommandParameter = "/YOS_Pages/Reference/Reference_Pages/TeachingMaterialPrice.xaml";
					break;
			}
		}


		private void btnEdit_Click(object sender, RoutedEventArgs e)
		{
			var wnd = new Windows.Controls.Page();

			wnd.Style = (Style)App.Current.Resources["BlankWindow"];
			wnd.Title = "Edit Notice";
			wnd.Width = 550;
			wnd.Height = 350;
			wnd.WindowStartupLocation = WindowStartupLocation.CenterScreen;

			YOS_Content.Notice wndNotice = new YOS_Content.Notice();
			wnd.Content = wndNotice;

			wnd.Show();

		}

		private void UserControl_MouseEnter(object sender, MouseEventArgs e)
		{
			using (FileStream fStream = File.Open("NoticeData.xaml", FileMode.Open))
			{
				FlowDocument doc = XamlReader.Load(fStream) as FlowDocument;
				this.lblNotice.Content = doc;
			}
		}
	}
}
