using System.Windows.Controls;
using System.Windows.Threading;
#region ODP.NET @ CONNECTIONSTRING namespace 추가
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;
using System.Windows;
using System.Data;
using System;
using System.IO;
#endregion


namespace FirstFloor.ModernUI.App.YOS_Pages.Reference_Pages
{
    public partial class TeachingMaterialPrice : UserControl
    {
        //EDUTOOL

        //#region 비연결기반 객체들 준비
        //private DataSet EDUCATION_SUPPORT_TOOL_DS = new DataSet("EDUCATION_SUPPORT_TOOL_DS");

        //private OracleCommandBuilder oraBuilder_EDUTOOL;

        //private OracleDataAdapter oraDA_EDUTOOL;

		private string connStr = "User Id=scott;Password=tiger;Data Source=orcl";

        //static DataTable DT;
        //#endregion
        public DataTable DT_1 = new DataTable();
        // static DataSet DS = new DataSet("XMLTABLE_DS");
        public DataSet DS = new DataSet();
        public DataTable CloneDT = new DataTable();
        public StringWriter stream = new StringWriter();

        public Dispatcher UIDispatcher = Application.Current.Dispatcher;
        
        public TeachingMaterialPrice()
        {
            InitializeComponent();
            UIDispatcher.Invoke(new Action(() => CSampleClient.Program.SrvrConn()));
            UIDispatcher.Invoke(new Action(() => CSampleClient.Program.SelectTable("EDUCATION_SUPPORT_TOOL")));

            #region 데이터 가져오기 및 DataGrid에 추가
            oraDA_EDUTOOL = new OracleDataAdapter("SELECT * FROM EDUCATION_SUPPORT_TOOL ", connStr);

            oraBuilder_EDUTOOL = new OracleCommandBuilder(oraDA_EDUTOOL);

            oraDA_EDUTOOL.Fill(EDUCATION_SUPPORT_TOOL_DS, "EDUCATION_SUPPORT_TOOL");

            DataTable DT = EDUCATION_SUPPORT_TOOL_DS.Tables["EDUCATION_SUPPORT_TOOL"];
            EDUTOOL_DG1.ItemsSource = DT.DefaultView;
            #endregion
        }
        //internal static void WaitForPriority(DispatcherPriority priority)
        //{
        //DispatcherFrame frame = new DispatcherFrame();
        //DispatcherOperation dispatcherOperation =
        //    Dispatcher.CurrentDispatcher.BeginInvoke(priority,
        //        new DispatcherOperationCallback(ExitFrameOperation), frame);
        //Dispatcher.PushFrame(frame);
        //if (dispatcherOperation.Status != DispatcherOperationStatus.Completed)
        //{
        //    dispatcherOperation.Abort();
        //}
        //}

        //private static object ExitFrameOperation(object obj)
        //{
        //    ((DispatcherFrame)obj).Continue = false;
        //    return null;
        //}
        #region 추가 button click event
        private void btn_Insert_Click(object sender, RoutedEventArgs e)
        {
            if ((string)btn_Insert.Content == "확인")
            {
                try
                {
                    //oraDA_EDUTOOL.Update(EDUCATION_SUPPORT_TOOL_DS, "EDUCATION_SUPPORT_TOOL"); //서버
                    //CloneDT = new DataTable();
                    // CloneDT.Clear();
                    UIDispatcher.Invoke(new Action(() => CSampleClient.Program.SrvrConn()));
                    UIDispatcher.Invoke(new Action(() => DT_1 = YOS.CAccessDB.getdt()));
                    

                    CloneDT = DT_1.Copy();
                    CloneDT.TableName = "XMLTABLE";
                    DS.Tables.Clear();
                    DS.Tables.Add(CloneDT);             
                                       
                    DS.WriteXml(stream, XmlWriteMode.WriteSchema);
                    CSampleClient.Program.UpdateTable(stream.ToString());

                    MessageBox.Show("추가 성공");
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
                //DataGridRow row = (DataGridRow)EDUTOOL_DG1.ItemContainerGenerator.ContainerFromIndex(EDUTOOL_DG1.SelectedIndex);
                //string expression = "EDUCATIONTOOLNAME = '" + ((TextBlock)(EDUTOOL_DG1.Columns[0].GetCellContent(row).Parent as DataGridCell).Content).Text + "'";
                //DataRow[] DeleteRow = EDUCATION_SUPPORT_TOOL_DS.Tables["EDUCATION_SUPPORT_TOOL"].Select(expression);

                //EDUCATION_SUPPORT_TOOL_DS.Tables["EDUCATION_SUPPORT_TOOL"].Rows[EDUTOOL_DG1.SelectedIndex].Delete();
                ////oraDA_EDUTOOL.Update(EDUCATION_SUPPORT_TOOL_DS, "EDUCATION_SUPPORT_TOOL");

                //CSampleClient.Program.SrvrConn();
                //CloneDT = DT.Copy();
                //CloneDT.TableName = "XMLTABLE";
                //DS.Tables.Add(CloneDT);
                //DS.WriteXml(stream, XmlWriteMode.WriteSchema);

                //CSampleClient.Program.SendMessage2(stream.ToString());

                MessageBox.Show("삭제 성공");
            }
            catch (Exception ex)
            {
                MessageBox.Show("오류 : " + ex.ToString());
            }
        }
        #endregion
        public void EDUTOOL_DG1_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void EDUTOOL_DG1_LayoutUpdated(object sender, EventArgs e)
        {
            UIDispatcher.Invoke(new Action(() => DT_1 = YOS.CAccessDB.getdt()));
            UIDispatcher.Invoke(new Action(() => EDUTOOL_DG1.ItemsSource = DT_1.DefaultView));//수신
        }
    }
}
