using System.Windows.Controls;

#region ODP.NET @ CONNECTIONSTRING namespace 추가
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;
using System.Windows;
using System.Data;
using System;
#endregion


namespace FirstFloor.ModernUI.App.YOS_Pages.Reference.Reference_Pages
{
    public partial class TeachingMaterialPrice : UserControl
    {
        //EDUTOOL

        #region 비연결기반 객체들 준비
        private DataSet EDUCATION_SUPPORT_TOOL_DS = new DataSet("EDUCATION_SUPPORT_TOOL_DS");

        private OracleCommandBuilder oraBuilder_EDUTOOL;

        private OracleDataAdapter oraDA_EDUTOOL;

		private string connStr = "Data Source=ORCL;User Id=bitsoft;Password=bitsoft_";

        #endregion

        public TeachingMaterialPrice()
        {
            InitializeComponent();

            #region 데이터 가져오기 및 DataGrid에 추가
            oraDA_EDUTOOL = new OracleDataAdapter("SELECT * FROM EDUCATION_SUPPORT_TOOL ", connStr);

            oraBuilder_EDUTOOL = new OracleCommandBuilder(oraDA_EDUTOOL);

            oraDA_EDUTOOL.Fill(EDUCATION_SUPPORT_TOOL_DS, "EDUCATION_SUPPORT_TOOL");

            DataTable DT = EDUCATION_SUPPORT_TOOL_DS.Tables["EDUCATION_SUPPORT_TOOL"];
            EDUTOOL_DG1.ItemsSource = DT.DefaultView;
            #endregion
        }

        #region 추가 button click event
        private void btn_Insert_Click(object sender, RoutedEventArgs e)
        {
            if ((string)btn_Insert.Content == "확인")
            {
                try
                {
                    oraDA_EDUTOOL.Update(EDUCATION_SUPPORT_TOOL_DS, "EDUCATION_SUPPORT_TOOL");

                    MessageBox.Show("데이터 업데이트 성공");
                    btn_Insert.Content = "추가";
                    EDUTOOL_DG1.IsReadOnly = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("오류 : " + ex.ToString());
                }
            }
            else
            {
                btn_Insert.Content = "확인";
                EDUTOOL_DG1.IsReadOnly = false;
            }
        }
        #endregion

        #region 삭제 button click event
        private void btn_Delete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DataGridRow row = (DataGridRow)EDUTOOL_DG1.ItemContainerGenerator.ContainerFromIndex(EDUTOOL_DG1.SelectedIndex);
                string expression = "EDUCATIONTOOLNAME = '" + ((TextBlock)(EDUTOOL_DG1.Columns[0].GetCellContent(row).Parent as DataGridCell).Content).Text + "'";
                DataRow[] DeleteRow = EDUCATION_SUPPORT_TOOL_DS.Tables["EDUCATION_SUPPORT_TOOL"].Select(expression);

                EDUCATION_SUPPORT_TOOL_DS.Tables["EDUCATION_SUPPORT_TOOL"].Rows[EDUTOOL_DG1.SelectedIndex].Delete();
                oraDA_EDUTOOL.Update(EDUCATION_SUPPORT_TOOL_DS, "EDUCATION_SUPPORT_TOOL");

                MessageBox.Show("삭제 성공");
            }
            catch (Exception ex)
            {
                MessageBox.Show("오류 : " + ex.ToString());
            }
        }
        #endregion
    }
}
