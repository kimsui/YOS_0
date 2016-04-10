using FirstFloor.ModernUI.Windows.Controls;
using System.Windows;
using System.Windows.Controls;
using System;
using System.Windows.Input;

using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;
using System.Data;

using System.Collections.Generic;

namespace FirstFloor.ModernUI.App.YOS_Pages.Course
{
    public partial class Ongoing : UserControl
    {
        private string strOraConn = "Data Source=ORCL;User Id=bitsoft;Password=bitsoft_";
        private DataSet LECTURE_DS = new DataSet("LECTURE_DS");
        private DataSet LECTUREO_DS = new DataSet("LECTUREO_DS");
        private DataSet PARTNERS_DS = new DataSet("PARTNERS_DS");
        private DataSet FEE_DS = new DataSet("FEE_DS");
        private DataSet CATEGORY_DS = new DataSet("CATEGORY_DS");

        private OracleCommandBuilder oraBuilder;
        private OracleCommandBuilder oraBuilder2;
        private OracleCommandBuilder oraBuilder3;
        private OracleCommandBuilder oraBuilder4;
        private OracleDataAdapter Adpt;
        private OracleDataAdapter Adpt2;
        private OracleDataAdapter Adpt3;
        private OracleDataAdapter Adpt4;

        private OracleCommandBuilder oraBuilder5; 
        private OracleCommandBuilder oraBuilder6;
        private OracleCommandBuilder oraBuilder7;
        private OracleCommandBuilder oraBuilder8;
        private OracleDataAdapter Adpt5;
        private OracleDataAdapter Adpt6;
        private OracleDataAdapter Adpt7;
        private OracleDataAdapter Adpt8;

        private Dictionary<string, int> LECTUREL = new Dictionary<string, int>();
        private Dictionary<string, string> LECTUREID = new Dictionary<string, string>();
        private Dictionary<string, string> PL = new Dictionary<string, string>();
        private Dictionary<string, string> PLR = new Dictionary<string, string>();
        private Dictionary<string, int> PLlinker = new Dictionary<string, int>();

        string selectname = "";
        string selectid = "";

        public object SelectedItem { get; set; }


        public Ongoing()
        {
            InitializeComponent();

            #region 데이터 가져오기 및 DataGrid에 추가

            //SELECT * FROM LECTURE

            wUpdate();

            #endregion
        }
        
        private void wUpdate()
        {
            try
            {
                DGLEC.ItemsSource = null;
                CBOXLEC.ItemsSource = null;

                DataTable d1 = LECTURE_DS.Tables["LECTURE_dt"];
                DataTable d2 = LECTUREO_DS.Tables["LECTUREO_dt"];
                DataTable d3 = PARTNERS_DS.Tables["PARTNERS_dt"];
                d1.Clear();
                d2.Clear();
                d3.Clear();

                cbb_CategoryOfPurpose.ItemsSource = null;
                cbb_CategoryOfInstitution.ItemsSource = null;
                cbb_CategoryOfTarget.ItemsSource = null;
                cbb_CateroryOfSubject.ItemsSource = null;

                DataTable d4 = CATEGORY_DS.Tables["PurposeCATEGORY_dt"];
                DataTable d5 = CATEGORY_DS.Tables["InstitutionCATEGORY_dt"];
                DataTable d6 = CATEGORY_DS.Tables["TargetCATEGORY_dt"];
                DataTable d7 = CATEGORY_DS.Tables["SubjectCATEGORY_dt"];

                d4.Clear();
                d5.Clear();
                d6.Clear();
                d7.Clear();
            }
            catch
            {

            }

            Adpt2 = new OracleDataAdapter("SELECT * FROM PARTNERS", strOraConn);
            oraBuilder2 = new OracleCommandBuilder(Adpt2);
            Adpt2.Fill(PARTNERS_DS, "PARTNERS_dt");
            DataTable PARTNERS_dt = PARTNERS_DS.Tables["PARTNERS_dt"];

            PL.Clear();
            PLR.Clear();
            PLlinker.Clear();
            for (int i = 0; i < PARTNERS_dt.Rows.Count; i++)
            {
                PL.Add(PARTNERS_dt.Rows[i].ItemArray[0].ToString(), PARTNERS_dt.Rows[i].ItemArray[1].ToString());
                PLR.Add(PARTNERS_dt.Rows[i].ItemArray[1].ToString(), PARTNERS_dt.Rows[i].ItemArray[0].ToString());
                PLlinker.Add(PARTNERS_dt.Rows[i].ItemArray[0].ToString(), i);
            }

            Adpt3 = new OracleDataAdapter("SELECT * FROM LECTURE", strOraConn);
            oraBuilder3 = new OracleCommandBuilder(Adpt3);
            Adpt3.Fill(LECTUREO_DS, "LECTUREO_dt");
            DataTable LECTUREO_dt = LECTUREO_DS.Tables["LECTUREO_dt"];

            LECTUREL.Clear();
            LECTUREID.Clear();
            for (int i = 0; i < LECTUREO_dt.Rows.Count; i++)
            {
                LECTUREL.Add(LECTUREO_dt.Rows[i].ItemArray[1].ToString(), i);
                LECTUREID.Add(LECTUREO_dt.Rows[i].ItemArray[1].ToString(), LECTUREO_dt.Rows[i].ItemArray[0].ToString());
            }

            Adpt = new OracleDataAdapter("SELECT l.LECTURENAME, l.PURPOSECATEGORY, l.INSTITUTIONCATEGORY, l.TARGETCATEGORY, l.SUBJECTCATEGORY, p1.ID \"NC1\", p1.NAME \"N1\", p2.ID \"NC2\", p2.NAME \"N2\", p3.ID \"NC3\", p3.NAME \"N3\",l.LECPLACE, l.STARTDATE, l.CLOSEDATE, l.LECTURETIME, l.LECTUREFEE, l.COMPLETERATE FROM LECTURE l, PARTNERS p1, PARTNERS p2, PARTNERS p3 WHERE l.PROJMANAGER = p1.ID AND l.RECOMMENDER = p2.ID AND l.LECTURER = p3.ID AND NOT(l.COMPLETERATE=10)", strOraConn);
            oraBuilder = new OracleCommandBuilder(Adpt);
            Adpt.Fill(LECTURE_DS, "LECTURE_dt");
            DataTable PERSON_dt = LECTURE_DS.Tables["LECTURE_dt"];

            DGLEC.ItemsSource = LECTURE_DS.Tables["LECTURE_dt"].DefaultView;
            CBOXLEC.ItemsSource = LECTURE_DS.Tables["LECTURE_dt"].DefaultView;
            DGLEC.CanUserAddRows = false;

            Adpt5 = new OracleDataAdapter("SELECT * FROM PurposeCATEGORY", strOraConn);
            Adpt6 = new OracleDataAdapter("SELECT * FROM InstitutionCATEGORY", strOraConn);
            Adpt7 = new OracleDataAdapter("SELECT * FROM TargetCATEGORY", strOraConn);
            Adpt8 = new OracleDataAdapter("SELECT * FROM SubjectCATEGORY", strOraConn);

            DataTable PurposeCATEGORY_dt = CATEGORY_DS.Tables["PurposeCATEGORY_dt"];
            DataTable InstitutionCATEGORY_dt = CATEGORY_DS.Tables["InstitutionCATEGORY_dt"];
            DataTable TargetCATEGORY_dt = CATEGORY_DS.Tables["TargetCATEGORY_dt"];
            DataTable SubjectCATEGORY_dt = CATEGORY_DS.Tables["SubjectCATEGORY_dt"];

            oraBuilder5 = new OracleCommandBuilder(Adpt5);
            oraBuilder6 = new OracleCommandBuilder(Adpt6);
            oraBuilder7 = new OracleCommandBuilder(Adpt7);
            oraBuilder8 = new OracleCommandBuilder(Adpt8);

            Adpt5.Fill(CATEGORY_DS, "PurposeCATEGORY_dt");
            Adpt6.Fill(CATEGORY_DS, "InstitutionCATEGORY_dt");
            Adpt7.Fill(CATEGORY_DS, "TargetCATEGORY_dt");
            Adpt8.Fill(CATEGORY_DS, "SubjectCATEGORY_dt");

            cbb_CategoryOfPurpose.ItemsSource = CATEGORY_DS.Tables["PurposeCATEGORY_dt"].DefaultView;
            cbb_CategoryOfInstitution.ItemsSource = CATEGORY_DS.Tables["InstitutionCATEGORY_dt"].DefaultView;
            cbb_CategoryOfTarget.ItemsSource = CATEGORY_DS.Tables["TargetCATEGORY_dt"].DefaultView;
            cbb_CateroryOfSubject.ItemsSource = CATEGORY_DS.Tables["SubjectCATEGORY_dt"].DefaultView;

        }

        private void CommonDialog_Click(object sender, RoutedEventArgs e)
        {
			var wnd = new Windows.Controls.Page();

			wnd.Style = (Style)App.Current.Resources["BlankWindow"];
			wnd.Title = "비용 등록 및 수정";
			wnd.Width = 300;
			wnd.Height = 480;
			wnd.WindowStartupLocation = WindowStartupLocation.CenterScreen;

            YOS_Content.AddCost wndCost = new YOS_Content.AddCost(tbx_Course.Text, selectid);
			wndCost.lblCourse.Content = tbx_Course.Text;

			wnd.Content = wndCost;
			
			wnd.Show();
		}

		private void btn_Registration_Click(object sender, RoutedEventArgs e)
        {
            DataTable LECTURE_dt = LECTURE_DS.Tables["LECTURE_dt"];
            DataTable LECTUREO_dt = LECTUREO_DS.Tables["LECTUREO_dt"];
            DataTable PARTNERS_dt = PARTNERS_DS.Tables["PARTNERS_dt"];

            if (DGLEC.SelectedIndex == -1 && tbx_Course.Text != "")
            {
                bool ec = false;
                if (PLR.ContainsKey(tbx_PMName.Text) == false)
                {
                    ec = true;
                    MessageBox.Show("파트너 테이블에서 " + tbx_PMName.Text + "을 찾을 수 없습니다");

                }

                if (PLR.ContainsKey(tbx_RecommanderName.Text) == false)
                {
                    ec = true;
                    MessageBox.Show("파트너 테이블에서 " + tbx_RecommanderName.Text + "을 찾을 수 없습니다");
                }

                if (PLR.ContainsKey(tbx_LecturerName.Text) == false)
                {
                    ec = true;
                    MessageBox.Show("파트너 테이블에서 " + tbx_LecturerName.Text + "을 찾을 수 없습니다");
                }

                if (ec == false)
                {
                    if (txtCompleteRatio.Text == "")
                        txtCompleteRatio.Text = "0";

                    /*
                    주의사항 데이터 추가시 무결성 제약조건 발생할 수 있음
                    데이터 4개 추가시 1,2,3,4의 아이디를 갖게되고 이중 3번을 제거한 후
                    데이터 1개 추가시 1,2,4의 아이디에 새로운 아이디 4를 추가하게 되어
                    4가 두번나오므로 Primary Key 제약조건에 위배되 추가시 오류가 발생할 수 있음

                    해결방법
                    데이터베이스 아이디를 직접입력이 아닌 Auto Increasement 옵션을 이용해 중복없이 자동생성되게 해야함
                    *데이터베이스를 처음부터 다시만들어야 함
                    */

                    LECTUREO_dt.Rows.Add(LECTUREO_dt.Rows.Count + 1, tbx_Course.Text, cbb_CategoryOfPurpose.Text, cbb_CategoryOfInstitution.Text, cbb_CategoryOfTarget.Text, cbb_CateroryOfSubject.Text, Convert.ToInt32(PLR[tbx_PMName.Text]), Convert.ToInt32(PLR[tbx_RecommanderName.Text]), Convert.ToInt32(PLR[tbx_LecturerName.Text]), tbx_Place.Text, dp_StartDate.SelectedDate, dp_EndDate.SelectedDate, Convert.ToInt32(tbx_Time.Text), Convert.ToInt32(tbx_TotalMoney.Text), Convert.ToInt32(txtCompleteRatio.Text));

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
            }

            string Record = "";
            foreach (DataRow R in LECTURE_dt.Rows)
            {
                bool rowdeleted = false;
                switch (R.RowState)
                {
                    case DataRowState.Added:
                        Record = string.Format("추가: {0}", Convert.ToString(R["LECTURENAME"]));
                        MessageBox.Show($"데이터가 추가되었습니다. {Record}");
                        break;

                    case DataRowState.Deleted:
                        Record = string.Format("삭제: {0}", Convert.ToString(R["LECTURENAME", DataRowVersion.Original]));
                        //LECTUREO_dt.Rows.RemoveAt(LECTUREL[R.ItemArray[0].ToString()]);
                        MessageBox.Show($"데이터가 삭제되었습니다. {Record}");
                        rowdeleted = true;
                        break;
                }

                if (rowdeleted == false)
                {
                    bool rowupdated = false;
                    foreach (DataColumn C in LECTURE_dt.Columns)
                    {
                        if (!R[C, DataRowVersion.Original].Equals(R[C, DataRowVersion.Current]))
                        {
                            rowupdated = true;

                            Record = string.Format("수정: {0}", Convert.ToString(R["LECTURENAME"]));
                            //MessageBox.Show($"데이터가 수정되었습니다. {Record}");
                        }
                    }

                    if (rowupdated == true)
                    {
                        bool ec = false;
                        if (PLR.ContainsKey(R.ItemArray[6].ToString()) == false)
                        {
                            ec = true;
                            MessageBox.Show("파트너 테이블에서 " + R.ItemArray[6].ToString() + "을 찾을 수 없습니다");

                        }

                        if (PLR.ContainsKey(R.ItemArray[8].ToString()) == false)
                        {
                            ec = true;
                            MessageBox.Show("파트너 테이블에서 " + R.ItemArray[8].ToString() + "을 찾을 수 없습니다");
                        }

                        if (PLR.ContainsKey(R.ItemArray[10].ToString()) == false)
                        {
                            ec = true;
                            MessageBox.Show("파트너 테이블에서 " + R.ItemArray[10].ToString() + "을 찾을 수 없습니다");
                        }

                        if (ec == false)
                        {
                            LECTUREO_dt.Rows[LECTUREL[selectname]][1] = R.ItemArray[0];
                            LECTUREO_dt.Rows[LECTUREL[selectname]][2] = R.ItemArray[1];
                            LECTUREO_dt.Rows[LECTUREL[selectname]][3] = R.ItemArray[2];
                            LECTUREO_dt.Rows[LECTUREL[selectname]][4] = R.ItemArray[3];
                            LECTUREO_dt.Rows[LECTUREL[selectname]][5] = R.ItemArray[4];

                            //LECTUREO_dt.Rows[LECTUREL[selectname]][6] = R.ItemArray[5];
                            //LECTUREO_dt.Rows[LECTUREL[selectname]][7] = R.ItemArray[7];
                            //LECTUREO_dt.Rows[LECTUREL[selectname]][8] = R.ItemArray[9];

                            LECTUREO_dt.Rows[LECTUREL[selectname]][6] = Convert.ToInt32(PLR[R.ItemArray[6].ToString()]);
                            LECTUREO_dt.Rows[LECTUREL[selectname]][7] = Convert.ToInt32(PLR[R.ItemArray[8].ToString()]);
                            LECTUREO_dt.Rows[LECTUREL[selectname]][8] = Convert.ToInt32(PLR[R.ItemArray[10].ToString()]);

                            LECTUREO_dt.Rows[LECTUREL[selectname]][9] = R.ItemArray[11];
                            LECTUREO_dt.Rows[LECTUREL[selectname]][10] = R.ItemArray[12];

                            LECTUREO_dt.Rows[LECTUREL[selectname]][11] = R.ItemArray[13];
                            LECTUREO_dt.Rows[LECTUREL[selectname]][12] = R.ItemArray[14];
                            LECTUREO_dt.Rows[LECTUREL[selectname]][13] = R.ItemArray[15];
                            LECTUREO_dt.Rows[LECTUREL[selectname]][14] = R.ItemArray[16];


                            //LECTUREO_dt.Rows[LECTUREL[R.ItemArray[0].ToString()]][1] = R.ItemArray[0];
                            //LECTUREO_dt.Rows[LECTUREL[R.ItemArray[0].ToString()]][2] = R.ItemArray[1];
                            //LECTUREO_dt.Rows[LECTUREL[R.ItemArray[0].ToString()]][3] = R.ItemArray[2];
                            //LECTUREO_dt.Rows[LECTUREL[R.ItemArray[0].ToString()]][4] = R.ItemArray[3];
                            //LECTUREO_dt.Rows[LECTUREL[R.ItemArray[0].ToString()]][5] = R.ItemArray[4];

                            ////LECTUREO_dt.Rows[LECTUREL[R.ItemArray[0].ToString()]][6] = R.ItemArray[5];
                            ////LECTUREO_dt.Rows[LECTUREL[R.ItemArray[0].ToString()]][7] = R.ItemArray[7];
                            ////LECTUREO_dt.Rows[LECTUREL[R.ItemArray[0].ToString()]][8] = R.ItemArray[9];

                            //LECTUREO_dt.Rows[LECTUREL[R.ItemArray[0].ToString()]][6] = Convert.ToInt32(PLR[R.ItemArray[6].ToString()]);
                            //LECTUREO_dt.Rows[LECTUREL[R.ItemArray[0].ToString()]][7] = Convert.ToInt32(PLR[R.ItemArray[8].ToString()]);
                            //LECTUREO_dt.Rows[LECTUREL[R.ItemArray[0].ToString()]][8] = Convert.ToInt32(PLR[R.ItemArray[10].ToString()]);

                            //LECTUREO_dt.Rows[LECTUREL[R.ItemArray[0].ToString()]][9] = R.ItemArray[11];
                            //LECTUREO_dt.Rows[LECTUREL[R.ItemArray[0].ToString()]][10] = R.ItemArray[12];

                            //LECTUREO_dt.Rows[LECTUREL[R.ItemArray[0].ToString()]][11] = R.ItemArray[13];
                            //LECTUREO_dt.Rows[LECTUREL[R.ItemArray[0].ToString()]][12] = R.ItemArray[14];
                            //LECTUREO_dt.Rows[LECTUREL[R.ItemArray[0].ToString()]][13] = R.ItemArray[15];
                            //LECTUREO_dt.Rows[LECTUREL[R.ItemArray[0].ToString()]][14] = R.ItemArray[16];


                            //PARTNERS_dt.Rows[PLlinker[R.ItemArray[5].ToString()]][1] = PLR[R.ItemArray[6].ToString()];
                            //PARTNERS_dt.Rows[PLlinker[R.ItemArray[7].ToString()]][1] = PLR[R.ItemArray[8].ToString()];
                            //PARTNERS_dt.Rows[PLlinker[R.ItemArray[9].ToString()]][1] = PLR[R.ItemArray[10].ToString()];
                        }
                    }

                }
            }

            //try
            //{
            Adpt2.Update(PARTNERS_DS, "PARTNERS_dt");
            Adpt3.Update(LECTUREO_DS, "LECTUREO_dt");
            PARTNERS_dt.AcceptChanges();
            LECTUREO_dt.AcceptChanges();

            //PARTNERS_dt.Clear();
            //LECTUREO_dt.Clear();
            //LECTURE_dt.Clear();

            //wUpdate();
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show("에러 발생: " + ex.ToString());
            //}

            try
            {
                LECTURE_dt.AcceptChanges();
            }
            catch
            {

            }
            //MessageBox.Show($"데이터가 등록되었습니다. {Record}");
            //= MessageBox.Show(string.Format("데이터가 등록되었습니다.{0}", Record));

            PARTNERS_dt.Clear();
            LECTUREO_dt.Clear();
            LECTURE_dt.Clear();

            wUpdate();

            MessageBox.Show("데이터가 업데이트 되었습니다");
        }

        private void CBOXLEC_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DGLEC.SelectedIndex = CBOXLEC.SelectedIndex;
        }

        private void DGLEC_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            btn_Registration.Content = "업데이트";

            DataTable LECTURE_dt = LECTURE_DS.Tables["LECTURE_dt"];

            if (DGLEC.SelectedIndex != -1)
            {
                selectname = LECTURE_dt.Rows[DGLEC.SelectedIndex].ItemArray[0].ToString();
                selectid = LECTUREID[selectname].ToString();
                
                btn_OpenCostResistrationWindow.IsEnabled = true;
            }

            Expense_Calculation();

            
            //tbN1.Text = PL[LECTURE_dt.Rows[DGLEC.SelectedIndex].ItemArray[5].ToString()];
            //tbN2.Text = PL[LECTURE_dt.Rows[DGLEC.SelectedIndex].ItemArray[7].ToString()];
        }

        public void StackPannel_control_init()
        {
            tbx_Course.Text = null;
            cbb_CategoryOfPurpose.Text = null;
            cbb_CategoryOfInstitution.Text = null;
            cbb_CategoryOfTarget.Text = null;
            cbb_CateroryOfSubject.Text = null;
            tbx_PMName.Text = null;
            tbx_RecommanderName.Text = null;
            tbx_LecturerName.Text = null;
            tbx_Place.Text = null;
            dp_StartDate.Text = null;
            dp_EndDate.Text = null;
            tbx_Time.Text = null;
            tbx_TotalMoney.Text = null;
            txtCompleteRatio.Text = null;
        }

        private void btn_Init_Click(object sender, RoutedEventArgs e)
        {
            DGLEC.SelectedIndex = -1;
            StackPannel_control_init();
            btn_Registration.Content = "등록";
            btn_OpenCostResistrationWindow.IsEnabled = false;
            lb_NetProfit_Value.Content = "";
        }

        private void tbN1_TextChanged(object sender, TextChangedEventArgs e)
        {
            DataTable LECTURE_dt = LECTURE_DS.Tables["LECTURE_dt"];
            DataTable PARTNERS_dt = PARTNERS_DS.Tables["PARTNERS_dt"];

            if (DGLEC.SelectedIndex != -1)
            {
                PARTNERS_dt.Rows[PLlinker[LECTURE_dt.Rows[DGLEC.SelectedIndex].ItemArray[5].ToString()]].ItemArray[1] = tbN1.Text;
            }
        }

        private void tbN2_TextChanged(object sender, TextChangedEventArgs e)
        {
            DataTable LECTURE_dt = LECTURE_DS.Tables["LECTURE_dt"];
            DataTable PARTNERS_dt = PARTNERS_DS.Tables["PARTNERS_dt"];

            if (DGLEC.SelectedIndex != -1)
            {
                PARTNERS_dt.Rows[PLlinker[LECTURE_dt.Rows[DGLEC.SelectedIndex].ItemArray[7].ToString()]].ItemArray[1] = tbN2.Text;
            }
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            wUpdate();
            StackPannel_control_init();
            btn_OpenCostResistrationWindow.IsEnabled = false;
            lb_NetProfit_Value.Content = "";
        }

        private void btn_Cancel_Click(object sender, RoutedEventArgs e)
        {
            DataTable LECTURE_dt = LECTURE_DS.Tables["LECTURE_dt"];
            DataTable LECTUREO_dt = LECTUREO_DS.Tables["LECTUREO_dt"];

            LECTUREO_dt.Rows[LECTUREL[LECTURE_dt.Rows[DGLEC.SelectedIndex].ItemArray[0].ToString()]].Delete();
            LECTURE_dt.Rows[DGLEC.SelectedIndex].Delete();

            //LECTUREO_dt.Rows.RemoveAt(LECTUREL[LECTURE_dt.Rows[DGLEC.SelectedIndex].ItemArray[0].ToString()]);
            //LECTURE_dt.Rows.RemoveAt(DGLEC.SelectedIndex);
            
            btn_Registration_Click(null, null);
        }

        public void Expense_Calculation()
        {
            if (DGLEC.SelectedIndex != -1)
            {
                DataTable FEE_dt = FEE_DS.Tables["FEE_dt"];
            DataTable LECTURE_dt = LECTURE_DS.Tables["LECTURE_dt"];
                try
                {
                    FEE_dt.Clear();
                }
                catch
                {

                }

                string s = selectid;
                if (s != "")
                    s = " where LECTUREID=" + s; ;
                Adpt4 = new OracleDataAdapter("SELECT FOODEXPENSES+RENTALFEE+TEXTBOOK+TALK+CONJECTUREWORDCARD+STICKER+POSTCARD+PICTURECARD_A+PICTURECARD_B+CARDPOCKET+PROTECT+OTHERMATERIALS+ETC SUM FROM FEE" + s, strOraConn);
                oraBuilder4 = new OracleCommandBuilder(Adpt4);
                Adpt4.Fill(FEE_DS, "FEE_dt");
                FEE_dt = FEE_DS.Tables["FEE_dt"];

                if (FEE_dt.Rows.Count != 0)
                    lb_NetProfit_Value.Content = FEE_dt.Rows[0].ItemArray[0].ToString();
                else
                    lb_NetProfit_Value.Content = "등록안됨";
            }
        }
    }
}
