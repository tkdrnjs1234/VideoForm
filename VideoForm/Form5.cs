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

namespace VideoForm
{
    public partial class Form5 : Form
    {   // DB 연결 문자열
        string connStr = "Server=localhost;" + "Database=KSG;" + "User Id=sa;" + "Password=std001;" + "TrustServerCertificate=True;";
        public Form5()
        {
            InitializeComponent();
        }

        private void Form5_Load(object sender, EventArgs e)// 정보조회 폼 로드 시 장르 목록과 고객신분 목록을 초기화한다.
        {
            장르조회();
            // 고객신분 조회조건 설정
            comboBox2.Items.Add("전체");
            comboBox2.Items.Add("성인");
            comboBox2.Items.Add("청소년");

            comboBox2.SelectedIndex = 0;
        }

        private void 장르조회()// 장르정보 테이블 조회 후 장르 콤보박스에 출력한다
        {
            SqlConnection conn =
                new SqlConnection(connStr);

            SqlCommand cmd =     // 장르조회 저장프로시저 호출
                new SqlCommand(
                    "장르조회",
                    conn);

            cmd.CommandType =
                CommandType.StoredProcedure;

            SqlDataAdapter da =  // 조회결과 저장
                new SqlDataAdapter(cmd);

            DataTable dt =
                new DataTable();

            da.Fill(dt);

            DataRow row = // 전체 조회용 항목 추가
                dt.NewRow();

            row["장르코드"] = DBNull.Value;
            row["장르명"] = "전체";

            dt.Rows.InsertAt(row, 0);

            comboBox1.DataSource = dt;// 콤보박스 바인딩

            comboBox1.DisplayMember =
                "장르명";

            comboBox1.ValueMember =
                "장르코드";
        }
        private void 비디오대여순위조회()// 비디오 대여 순위 조회,장르별 조건 검색 가능
        {
            SqlConnection conn =
                new SqlConnection(connStr);

            SqlCommand cmd =     // 비디오대여순위조회 저장프로시저 호출
                new SqlCommand(
                    "비디오대여순위조회",
                    conn);

            cmd.CommandType =
                CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue( // 선택한 장르코드 전달
                "@장르코드",

                comboBox1.SelectedValue == DBNull.Value
                ? (object)DBNull.Value
                : comboBox1.SelectedValue);

            SqlDataAdapter da = // 조회결과 저장
                new SqlDataAdapter(cmd);

            DataTable dt =
                new DataTable();

            da.Fill(dt);

            dataGridView1.DataSource = dt;// 조회결과 출력
        }
        private void 대여중비디오조회()// 현재 대여중인 비디오 조회,장르별 조건 검색 가능
        {
            SqlConnection conn =
                new SqlConnection(connStr);

            SqlCommand cmd = // 대여중비디오조회 저장프로시저 호출
                new SqlCommand(
                    "대여중비디오조회",
                    conn);

            cmd.CommandType =
                CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue(  // 선택한 장르코드 전달
                "@장르코드",

                comboBox1.SelectedValue == DBNull.Value
                ? (object)DBNull.Value
                : comboBox1.SelectedValue);

            SqlDataAdapter da =   // 조회결과 저장
                new SqlDataAdapter(cmd);

            DataTable dt =
                new DataTable();

            da.Fill(dt);

            dataGridView1.DataSource = dt;// 조회결과 출력
        }
        private void 고객대여순위조회()// 고객 대여 순위 조회,고객신분별 조건 검색 가능    
        {
            SqlConnection conn =
                new SqlConnection(connStr);

            SqlCommand cmd =// 고객대여순위조회 저장프로시저 호출
                new SqlCommand(
                    "고객대여순위조회",
                    conn);

            cmd.CommandType =
                CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue( // 선택한 고객신분 전달
                "@고객신분",

                comboBox2.Text == "전체"
                ? (object)DBNull.Value
                : comboBox2.Text);

            SqlDataAdapter da =  // 조회결과 저장
                new SqlDataAdapter(cmd);

            DataTable dt =
                new DataTable();

            da.Fill(dt);

            dataGridView1.DataSource = dt;   // 조회결과 출력
        }

        private void button1_Click(object sender, EventArgs e)// 선택한 조회조건에 따라 해당 정보를 조회한다.
        {
            if (radioButton1.Checked)  // 비디오 대여 순위 조회
            {
                비디오대여순위조회();
            }
            else if (radioButton2.Checked)// 대여중인 비디오 조회
            {
                대여중비디오조회();
            }
            else if (radioButton3.Checked) // 고객 대여 순위 조회
            {
                고객대여순위조회();
            }
        }

        private void button2_Click(object sender, EventArgs e)// 정보조회 폼 종료
        {
            this.Close();
        }
    }
}
