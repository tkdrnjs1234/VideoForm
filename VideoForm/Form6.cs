using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ClosedXML.Excel;

namespace VideoForm
{
    public partial class Form6 : Form
    {   // DB 연결 문자열
        string connStr = "Server=localhost;" + "Database=KSG;" + "User Id=sa;" + "Password=std001;" + "TrustServerCertificate=True;";
        public Form6()
        {
            InitializeComponent();
        }

        private void Form6_Load(object sender, EventArgs e)// 엑셀 출력 폼 로드 시 장르 목록을 조회한다.
        {
            장르조회();
        }
        private void 장르조회()// 장르정보를 조회하여 장르 콤보박스에 출력한다.
        {
            SqlConnection conn =
                new SqlConnection(connStr);

            SqlCommand cmd =  // 장르조회 저장프로시저 호출
                new SqlCommand(
                    "장르조회",
                    conn);

            cmd.CommandType =
                CommandType.StoredProcedure;

            SqlDataAdapter da = // 조회 결과 저장
                new SqlDataAdapter(cmd);

            DataTable dt =
                new DataTable();

            da.Fill(dt);

            DataRow row =  // 전체 조회용 항목 추가
                dt.NewRow();

            row["장르코드"] = DBNull.Value;
            row["장르명"] = "전체";

            dt.Rows.InsertAt(row, 0);

            comboBox1.DataSource = dt; // 콤보박스 바인딩 (DataTable을 연결하는 것)

            comboBox1.DisplayMember =   //사용자에게 보이는 값은 장르명 컬럼
                "장르명";

            comboBox1.ValueMember = //실제로 저장할 값은 장르코드
                "장르코드";
        }

        private void button1_Click(object sender, EventArgs e) // 비디오 목록을 조회하여 엑셀 파일로 저장한다.
        {
            DataTable dt = // 비디오 목록 조회
                비디오목록가져오기();

            
            ExcelExport(dt);// 엑셀 파일 생성
        }
        private DataTable 비디오목록가져오기()// 장르별 비디오 목록 조회
        {
            SqlConnection conn =
                new SqlConnection(connStr);

            SqlCommand cmd =    // 비디오목록조회 저장프로시저 호출
                new SqlCommand(
                    "비디오목록조회",
                    conn);

            cmd.CommandType =
                CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue(    // 선택한 장르코드 전달
                "@장르코드",

                comboBox1.SelectedValue == DBNull.Value
                ? (object)DBNull.Value
                : comboBox1.SelectedValue);

            SqlDataAdapter da =  // 조회 결과 저장
                new SqlDataAdapter(cmd);

            DataTable dt =
                new DataTable();

            da.Fill(dt);

            return dt;
        }

        private void button2_Click(object sender, EventArgs e) // 대여 현황을 조회하여 엑셀 파일로 저장한다.
        {
            DataTable dt =  // 대여 현황 조회
                 대여현황가져오기();

            ExcelExport(dt);// 엑셀 파일 생성
        }
        private DataTable 대여현황가져오기() // 장르별 대여 현황 조회
        {
            SqlConnection conn =
         new SqlConnection(connStr);

            SqlCommand cmd = // 대여현황조회 저장프로시저 호출
                new SqlCommand(
                    "대여현황조회",
                    conn);

            cmd.CommandType =
                CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue(    // 선택한 장르코드 전달
                "@장르코드",

                comboBox1.SelectedValue == DBNull.Value
                ? (object)DBNull.Value
                : comboBox1.SelectedValue);

            SqlDataAdapter da =  // 조회 결과 저장
                new SqlDataAdapter(cmd);

            DataTable dt =
                new DataTable();

            da.Fill(dt);

            return dt;
        }
        private void ExcelExport(DataTable dt) // 조회된 데이터를 엑셀 파일로 저장
        {
            try
            {
                SaveFileDialog sfd =    // 저장할 파일명 선택
                    new SaveFileDialog();

                sfd.Filter =
                    "Excel File|*.xlsx";

                if (sfd.ShowDialog()
                    != DialogResult.OK)
                    return;

                XLWorkbook wb =     // 엑셀 파일 생성
                    new XLWorkbook();

                wb.Worksheets.Add( // DataTable 데이터를 시트에 추가
                    dt,
                    "목록");

                wb.SaveAs( // 엑셀 파일 저장
                    sfd.FileName);

                MessageBox.Show(
                    "엑셀 저장 완료");
            }
            catch (Exception ex)
            {   // 오류 발생 시 메시지 출력
                MessageBox.Show(
                    ex.ToString());
            }
        }
    }
}
