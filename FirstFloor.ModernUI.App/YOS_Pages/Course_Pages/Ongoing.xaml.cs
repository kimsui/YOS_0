using FirstFloor.ModernUI.Windows.Controls;
using System.Windows;
using System.Windows.Controls;
using System;
using System.Windows.Input;

using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;
using System.Data;

namespace FirstFloor.ModernUI.App.YOS_Pages.Course_Pages
{

    public partial class Ongoing : UserControl
    {
        private string strOraConn = "Data Source=orcl;User Id=scott;Password=tiger";
        //private OracleConnection Con = new OracleConnection();
        private DataSet LECTURE_DS = new DataSet("LECTURE_DS");
        private OracleCommandBuilder oraBuilder;
        private OracleDataAdapter Adpt;
        public object SelectedItem { get; set; }


        public Ongoing()
        {
            InitializeComponent();

            #region 데이터 가져오기 및 DataGrid에 추가

            Adpt = new OracleDataAdapter("SELECT * FROM LECTURE", strOraConn);

            DataTable PERSON_dt = LECTURE_DS.Tables["LECTURE_dt"];

            oraBuilder = new OracleCommandBuilder(Adpt);

            Adpt.Fill(LECTURE_DS, "LECTURE_dt");
            DGLEC.ItemsSource = LECTURE_DS.Tables["LECTURE_dt"].DefaultView;
            CBOXLEC.ItemsSource = LECTURE_DS.Tables["LECTURE_dt"].DefaultView;
            DGLEC.CanUserAddRows = false;

            #endregion
        }

        private void CommonDialog_Click(object sender, RoutedEventArgs e)
        {
            var dlg = new ModernDialog
            {
                Title = "Common dialog",
                Content = new YOS_Content.AddCost()
            };
            dlg.Buttons = new Button[] { dlg.OkButton, dlg.CancelButton };

            dlg.ShowDialog();
        }

        private void btn_Registration_Click(object sender, RoutedEventArgs e)
        {
            DataTable LECTURE_dt = LECTURE_DS.Tables["LECTURE_dt"];


            if (DGLEC.SelectedIndex == -1)
            {

                LECTURE_dt.Rows.Add(tbx_Course.Text, cbb_CategoryOfPurpose.Text, cbb_CategoryOfInstitution.Text, cbb_CategoryOfTarget.Text, cbb_CateroryOfSubject.Text, tbx_PMName.Text, tbx_RecommanderName.Text, tbx_Place.Text, dp_StartDate.Text, dp_EndDate.Text, tbx_Time.Text, tbx_TotalMoney.Text);

                try
                {
                    Adpt.Update(LECTURE_DS, "LECTURE_dt");
                    MessageBox.Show("추가가 완료되었습니다.");
                }
                catch (Exception ex)
                {
                    LECTURE_dt.Rows.RemoveAt(LECTURE_dt.Rows.Count - 1);
                    MessageBox.Show("에러가 발생해 추가가 되지 않았습니다\n 에러메세지: " + ex.ToString());
                }
            }

            string Record = "";
            foreach (DataRow R in LECTURE_dt.Rows)
            {
                switch (R.RowState)
                {
                    case DataRowState.Added:
                        Record = string.Format("추가: {0}", Convert.ToString(R["LECTURENAME"]));
                        MessageBox.Show($"데이터가 추가되었습니다. {Record}");
                        break;

                    case DataRowState.Modified:
                        Record = string.Format("수정: {0}", Convert.ToString(R["LECTURENAME"]));
                        MessageBox.Show($"데이터가 수정되었습니다. {Record}");
                        break;
                }
            }

            try
            {
                Adpt.Update(LECTURE_DS, "LECTURE_dt");
            }
            catch (Exception ex)
            {
                MessageBox.Show("에러 발생: " + ex.ToString());
            }


            //MessageBox.Show($"데이터가 등록되었습니다. {Record}");
            //= MessageBox.Show(string.Format("데이터가 등록되었습니다.{0}", Record));
        
        }

        private void CBOXLEC_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DGLEC.SelectedIndex = CBOXLEC.SelectedIndex;
        }
    }
}
