using System.Windows.Controls;

#region ODP.NET @ CONNECTIONSTRING namespace 추가
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;
using System;
using System.Data;
using System.Windows;
#endregion

namespace FirstFloor.ModernUI.App.YOS_Pages.Reference_Pages
{

    public partial class ItemRate : UserControl
    {
        #region 비연결기반 객체들 준비
        private DataSet ITEMRATE_DS = new DataSet("ITEMRATE_DS");

        private OracleCommandBuilder oraBuilder_ItemRate;

        private OracleDataAdapter oraDA_ItemRate;

        private string connStr = "Data Source=orcl;User Id=scott;Password=tiger";

        #endregion

        static int switchint = 0;

        public ItemRate()
        {
            InitializeComponent();

            #region 데이터 가져오기 및 DataGrid에 추가
            oraDA_ItemRate = new OracleDataAdapter("SELECT * FROM ITEMRATE", connStr);

            oraBuilder_ItemRate = new OracleCommandBuilder(oraDA_ItemRate);

            oraDA_ItemRate.Fill(ITEMRATE_DS, "ITEMRATE");

            DataTable DT = ITEMRATE_DS.Tables["ITEMRATE"];
            ItemRate_DG1.ItemsSource = DT.DefaultView;
            #endregion
        }

        private void Purpose_Checked(object sender, RoutedEventArgs e)
        {
            ItemRate_DG1.Columns[2].Visibility = Visibility.Visible;
            ItemRate_DG1.Columns[3].Visibility = Visibility.Hidden;
        }

        private void Staff_Checked(object sender, RoutedEventArgs e)
        {
            ItemRate_DG1.Columns[2].Visibility = Visibility.Hidden;
            ItemRate_DG1.Columns[3].Visibility = Visibility.Visible;
        }

        private void Whole_Checked(object sender, RoutedEventArgs e)
        {
            if (switchint > 0)
            {
                if (ItemRate_DG1.Columns[2].Visibility == Visibility.Hidden)
                {
                    ItemRate_DG1.Columns[2].Visibility = Visibility.Visible;
                }
                if (ItemRate_DG1.Columns[3].Visibility == Visibility.Hidden)
                {
                    ItemRate_DG1.Columns[3].Visibility = Visibility.Visible;
                }
            }
            switchint++;
        }
    }
}
