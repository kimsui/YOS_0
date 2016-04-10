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

using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;
using System.Data;

namespace FirstFloor.ModernUI.App.YOS_Content
{
    /// <summary>
    /// Interaction logic for LoremIpsum.xaml
    /// </summary>
    public partial class AddCost : UserControl
    {

        private string strOraConn = "Data Source=ORCL;User Id=bitsoft;Password=bitsoft_";
        private DataSet FEE_DS = new DataSet("FEE_dt");
        private OracleCommandBuilder oraBuilder;
        private OracleDataAdapter Adpt;

        string courseo = "";
        string lid = "";

        public AddCost(string lblCourse, string _lid)
        {
            InitializeComponent();

            courseo = lblCourse;
            lid = _lid;

            #region 데이터 가져오기 및 DataGrid에 추가

            Adpt = new OracleDataAdapter("SELECT count(*) FROM FEE WHERE LECTUREID=" + lid, strOraConn);
            oraBuilder = new OracleCommandBuilder(Adpt);
            Adpt.Fill(FEE_DS, "FEE_dt");
            DataTable FEE_dt = FEE_DS.Tables["FEE_dt"];

            if (FEE_dt.Rows[0].ItemArray[0].ToString() == "0")
            {
                FEE_dt.Clear();
                Adpt = new OracleDataAdapter("INSERT INTO FEE(LECTUREID, FOODEXPENSES, RENTALFEE, TEXTBOOK, TALK, CONJECTUREWORDCARD, STICKER, POSTCARD, PICTURECARD_A, PICTURECARD_B, CARDPOCKET, PROTECT, OTHERMATERIALS, ETC) VALUES (" + lid + ", 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0)", strOraConn);
                oraBuilder = new OracleCommandBuilder(Adpt);
                Adpt.Fill(FEE_DS, "FEE_dt");
                FEE_dt = FEE_DS.Tables["FEE_dt"];

            }

            FEE_dt.Clear();
            Adpt = new OracleDataAdapter("SELECT * FROM FEE WHERE LECTUREID=" + lid , strOraConn);
            oraBuilder = new OracleCommandBuilder(Adpt);
            Adpt.Fill(FEE_DS, "FEE_dt");
            FEE_dt = FEE_DS.Tables["FEE_dt"];

            tbx_FoodCosts.Text = FEE_dt.Rows[0].ItemArray[2].ToString();
            tbx_VenueRentalFee.Text = FEE_dt.Rows[0].ItemArray[3].ToString();
            tbx_Textbook.Text = FEE_dt.Rows[0].ItemArray[4].ToString();

            tbx_BoardGame.Text = FEE_dt.Rows[0].ItemArray[5].ToString();
            tbx_AbstractCard.Text = FEE_dt.Rows[0].ItemArray[6].ToString();
            tbx_Sticker.Text = FEE_dt.Rows[0].ItemArray[7].ToString();
            tbx_Postcard.Text = FEE_dt.Rows[0].ItemArray[8].ToString();
            tbx_ImageCardA.Text = FEE_dt.Rows[0].ItemArray[9].ToString();

            tbx_ImageCardB.Text = FEE_dt.Rows[0].ItemArray[10].ToString();
            tbx_Pocket.Text = FEE_dt.Rows[0].ItemArray[11].ToString();
            tbx_CardProtector.Text = FEE_dt.Rows[0].ItemArray[12].ToString();
            tbx_EtcMaterial.Text = FEE_dt.Rows[0].ItemArray[13].ToString();
            tbx_Etc.Text = FEE_dt.Rows[0].ItemArray[14].ToString();

            #endregion

        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            Adpt = new OracleDataAdapter("UPDATE FEE SET FOODEXPENSES=" + tbx_FoodCosts.Text + ", RENTALFEE=" + tbx_VenueRentalFee.Text + ", TEXTBOOK=" + tbx_Textbook.Text + ", TALK=" + tbx_BoardGame.Text + ", CONJECTUREWORDCARD=" + tbx_AbstractCard.Text + ", STICKER=" + tbx_Sticker.Text + ", POSTCARD=" + tbx_Postcard.Text + ", PICTURECARD_A=" + tbx_ImageCardA.Text + ", PICTURECARD_B=" + tbx_ImageCardB.Text + ", CARDPOCKET=" + tbx_Pocket.Text + ", PROTECT=" + tbx_CardProtector.Text + ", OTHERMATERIALS=" + tbx_EtcMaterial.Text + ", ETC=" + tbx_Etc.Text + " WHERE LECTUREID=" + lid, strOraConn);
            oraBuilder = new OracleCommandBuilder(Adpt);
            
            DataTable FEE_dt = FEE_DS.Tables["FEE_dt"];
            Adpt.Fill(FEE_DS, "FEE_dt");

            try
            {
                Adpt.Update(FEE_DS, "FEE_dt");
                MessageBox.Show("데이터 반영이 완료되었습니다.");
            }
            catch (Exception ex)
            {
                FEE_dt.Rows.RemoveAt(FEE_dt.Rows.Count - 1);
                MessageBox.Show("에러가 발생해 추가가 되지 않았습니다\n 에러메세지: " + ex.ToString());
            }
        }

        private void btnInit_Click(object sender, RoutedEventArgs e)
        {
            tbx_FoodCosts.Text = "0";
            tbx_VenueRentalFee.Text = "0";
            tbx_Textbook.Text = "0";

            tbx_BoardGame.Text = "0";
            tbx_AbstractCard.Text = "0";
            tbx_Sticker.Text = "0";
            tbx_Postcard.Text = "0";
            tbx_ImageCardA.Text = "0";
            tbx_ImageCardB.Text = "0";
            tbx_Pocket.Text = "0";
            tbx_CardProtector.Text = "0";
            tbx_EtcMaterial.Text = "0";
            tbx_Etc.Text = "0";


        }
    }
}
