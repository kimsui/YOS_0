using FirstFloor.ModernUI.Windows.Controls;
using System.Windows;
using System.Windows.Controls;
using System;
using System.Windows.Input;

using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;
using System.Data;
using System.Collections.ObjectModel;
using System.Collections.Generic;

namespace FirstFloor.ModernUI.App.YOS_Pages.Course.Complete_Pages
{

    public partial class Whole : UserControl
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
        private Dictionary<string, string> PL = new Dictionary<string, string>();
        private Dictionary<string, string> PLR = new Dictionary<string, string>();
        private Dictionary<string, int> PLlinker = new Dictionary<string, int>();

        string selectid = "";

        public object SelectedItem { get; set; }


        public Whole()
        {
            InitializeComponent();

            wUpdate();
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

            Adpt3 = new OracleDataAdapter("SELECT * FROM LECTURE WHERE COMPLETERATE=10", strOraConn);
            oraBuilder3 = new OracleCommandBuilder(Adpt3);
            Adpt3.Fill(LECTUREO_DS, "LECTUREO_dt");
            DataTable LECTUREO_dt = LECTUREO_DS.Tables["LECTUREO_dt"];

            LECTUREL.Clear();
            for (int i = 0; i < LECTUREO_dt.Rows.Count; i++)
            {
                LECTUREL.Add(LECTUREO_dt.Rows[i].ItemArray[1].ToString(), i);
            }

            Adpt = new OracleDataAdapter("SELECT l.LECTURENAME, l.PURPOSECATEGORY, l.INSTITUTIONCATEGORY, l.TARGETCATEGORY, l.SUBJECTCATEGORY, p1.ID \"NC1\", p1.NAME \"N1\", p2.ID \"NC2\", p2.NAME \"N2\", p3.ID \"NC3\", p3.NAME \"N3\",l.LECPLACE, l.STARTDATE, l.CLOSEDATE, l.LECTURETIME, l.LECTUREFEE, l.COMPLETERATE FROM LECTURE l, PARTNERS p1, PARTNERS p2, PARTNERS p3 WHERE l.PROJMANAGER = p1.ID AND l.RECOMMENDER = p2.ID AND l.LECTURER = p3.ID AND COMPLETERATE=10", strOraConn);
            //Adpt = new OracleDataAdapter("SELECT * FROM LECTURE", strOraConn);
            //DataTable PERSON_dt = LECTURE_DS.Tables["LECTURE_dt"];
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

            oraBuilder5 = new OracleCommandBuilder(Adpt4);
            oraBuilder6= new OracleCommandBuilder(Adpt5);
            oraBuilder7 = new OracleCommandBuilder(Adpt6);
            oraBuilder8 = new OracleCommandBuilder(Adpt7);

            Adpt5.Fill(CATEGORY_DS, "PurposeCATEGORY_dt");
            Adpt6.Fill(CATEGORY_DS, "InstitutionCATEGORY_dt");
            Adpt7.Fill(CATEGORY_DS, "TargetCATEGORY_dt");
            Adpt8.Fill(CATEGORY_DS, "SubjectCATEGORY_dt");

            cbb_CategoryOfPurpose.ItemsSource = CATEGORY_DS.Tables["PurposeCATEGORY_dt"].DefaultView;
            cbb_CategoryOfInstitution.ItemsSource = CATEGORY_DS.Tables["InstitutionCATEGORY_dt"].DefaultView;
            cbb_CategoryOfTarget.ItemsSource = CATEGORY_DS.Tables["TargetCATEGORY_dt"].DefaultView;
            cbb_CateroryOfSubject.ItemsSource = CATEGORY_DS.Tables["SubjectCATEGORY_dt"].DefaultView;

        }

        //private void CommonDialog_Click(object sender, RoutedEventArgs e)
        //{
        //    var dlg = new ModernDialog
        //    {
        //        Title = "Common dialog",
        //        Content = new YOS_Content.AddCost("")
        //    };
        //    dlg.Buttons = new Button[] { dlg.OkButton, dlg.CancelButton };

        //    dlg.ShowDialog();
        //}

        private void btn_Update_Click(object sender, RoutedEventArgs e)
        {
            DataTable LECTURE_dt = LECTURE_DS.Tables["LECTURE_dt"];
            DataTable LECTUREO_dt = LECTUREO_DS.Tables["LECTUREO_dt"];
            DataTable PARTNERS_dt = PARTNERS_DS.Tables["PARTNERS_dt"];

            string Record = "";
            foreach (DataRow R in LECTURE_dt.Rows)
            {
                bool rowdeleted = false;
                switch (R.RowState)
                {

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
                            LECTUREO_dt.Rows[LECTUREL[R.ItemArray[0].ToString()]][1] = R.ItemArray[0];
                            LECTUREO_dt.Rows[LECTUREL[R.ItemArray[0].ToString()]][2] = R.ItemArray[1];
                            LECTUREO_dt.Rows[LECTUREL[R.ItemArray[0].ToString()]][3] = R.ItemArray[2];
                            LECTUREO_dt.Rows[LECTUREL[R.ItemArray[0].ToString()]][4] = R.ItemArray[3];
                            LECTUREO_dt.Rows[LECTUREL[R.ItemArray[0].ToString()]][5] = R.ItemArray[4];

                            //LECTUREO_dt.Rows[LECTUREL[R.ItemArray[0].ToString()]][6] = R.ItemArray[5];
                            //LECTUREO_dt.Rows[LECTUREL[R.ItemArray[0].ToString()]][7] = R.ItemArray[7];
                            //LECTUREO_dt.Rows[LECTUREL[R.ItemArray[0].ToString()]][8] = R.ItemArray[9];

                            LECTUREO_dt.Rows[LECTUREL[R.ItemArray[0].ToString()]][6] = Convert.ToInt32(PLR[R.ItemArray[6].ToString()]);
                            LECTUREO_dt.Rows[LECTUREL[R.ItemArray[0].ToString()]][7] = Convert.ToInt32(PLR[R.ItemArray[8].ToString()]);
                            LECTUREO_dt.Rows[LECTUREL[R.ItemArray[0].ToString()]][8] = Convert.ToInt32(PLR[R.ItemArray[10].ToString()]);

                            LECTUREO_dt.Rows[LECTUREL[R.ItemArray[0].ToString()]][9] = R.ItemArray[11];
                            LECTUREO_dt.Rows[LECTUREL[R.ItemArray[0].ToString()]][10] = R.ItemArray[12];

                            LECTUREO_dt.Rows[LECTUREL[R.ItemArray[0].ToString()]][11] = R.ItemArray[13];
                            LECTUREO_dt.Rows[LECTUREL[R.ItemArray[0].ToString()]][12] = R.ItemArray[14];
                            LECTUREO_dt.Rows[LECTUREL[R.ItemArray[0].ToString()]][13] = R.ItemArray[15];
                            LECTUREO_dt.Rows[LECTUREL[R.ItemArray[0].ToString()]][14] = R.ItemArray[16];


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
            //MessageBox.Show($"데이터가 등록되었습니다. {Record}");
            //= MessageBox.Show(string.Format("데이터가 등록되었습니다.{0}", Record));
        }

        private void CBOXLEC_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DGLEC.SelectedIndex = CBOXLEC.SelectedIndex;
        }

        private void StartDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            searchDate();
        }

        private void EndDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            searchDate();

            if (EndDate.SelectedDate <= StartDate.SelectedDate)
            {
                MessageBox.Show("시작일보다 빠름! 시작일 이후로 다시 선택해 주십시오.");
                EndDate.SelectedDate = null;
            }
        }

        private void searchDate()
        {
            string sd = String.Format("{0:yy-MM-dd}", StartDate.SelectedDate);
            string ed = String.Format("{0:yy-MM-dd}", EndDate.SelectedDate);

            string dynamicquery = "";

            if (sd != "" && ed != "")
            {

            }

            if (sd != "")
            {
                dynamicquery += "CLOSEDATE >= to_date('" + sd + "', 'yy-mm-dd')";
            }

            if (ed != "")
            {
                if (dynamicquery != "")
                    dynamicquery += " and ";
                else
                    dynamicquery += " ";

                dynamicquery += "STARTDATE <= to_date('" + ed + "', 'yy-mm-dd')";
            }

            if (dynamicquery != "")
                dynamicquery = " where l.PROJMANAGER = p1.ID and l.RECOMMENDER = p2.ID and l.LECTURER = p3.ID and COMPLETERATE=10 and " + dynamicquery;
            Adpt = new OracleDataAdapter("SELECT l.LECTURENAME, l.PURPOSECATEGORY, l.INSTITUTIONCATEGORY, l.TARGETCATEGORY, l.SUBJECTCATEGORY, p1.ID \"NC1\", p1.NAME \"N1\", p2.ID \"NC2\", p2.NAME \"N2\", p3.ID \"NC3\", p3.NAME \"N3\",l.LECPLACE, l.STARTDATE, l.CLOSEDATE, l.LECTURETIME, l.LECTUREFEE, l.COMPLETERATE FROM LECTURE l, PARTNERS p1, PARTNERS p2, PARTNERS p3" + dynamicquery, strOraConn);
            DataTable LECTURE_dt = LECTURE_DS.Tables["LECTURE_dt"];

            DGLEC.ItemsSource = null;
            CBOXLEC.ItemsSource = null;

            LECTURE_dt.Clear();

            oraBuilder = new OracleCommandBuilder(Adpt);

            Adpt.Fill(LECTURE_DS, "LECTURE_dt");
            DGLEC.ItemsSource = LECTURE_DS.Tables["LECTURE_dt"].DefaultView;
            CBOXLEC.ItemsSource = LECTURE_DS.Tables["LECTURE_dt"].DefaultView;
            DGLEC.CanUserAddRows = false;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            wUpdate();
        }

        private void btn_change_Click(object sender, RoutedEventArgs e)
        {
            //이 버튼을 클릭하면 선택한 강좌의 완료율이 10 -> 0 으로 변경되어야 함.

            DataTable LECTURE_dt = LECTURE_DS.Tables["LECTURE_dt"];

            LECTURE_dt.Rows[DGLEC.SelectedIndex][16] = 0;
            MessageBox.Show($"{LECTURE_dt.Rows[DGLEC.SelectedIndex][0]} 강좌를 진행 중 강좌로 이동합니다.");
            btn_Update_Click(null, null);


        }

        private void btn_Cancel_Click(object sender, RoutedEventArgs e)
        {
            DataTable LECTURE_dt = LECTURE_DS.Tables["LECTURE_dt"];
            DataTable LECTUREO_dt = LECTUREO_DS.Tables["LECTUREO_dt"];

            LECTUREO_dt.Rows[LECTUREL[LECTURE_dt.Rows[DGLEC.SelectedIndex].ItemArray[0].ToString()]].Delete();
            LECTURE_dt.Rows[DGLEC.SelectedIndex].Delete();

            //LECTUREO_dt.Rows.RemoveAt(LECTUREL[LECTURE_dt.Rows[DGLEC.SelectedIndex].ItemArray[0].ToString()]);
            //LECTURE_dt.Rows.RemoveAt(DGLEC.SelectedIndex);

            btn_Update_Click(null, null);
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

                lb_NetProfit_Value2.Content = FEE_dt.Rows[0].ItemArray[0].ToString();
            }
        }

        private void DGLEC_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Expense_Calculation();

        }
    }
}
