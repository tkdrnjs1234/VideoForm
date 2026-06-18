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
using System.Xml.Linq;
using static System.Runtime.CompilerServices.RuntimeHelpers;

namespace VideoForm
{
    public partial class Form2 : Form
    {
        // DB 연결 문자열
        string connStr = "Server=localhost;" + "Database=KSG;" + "User Id=sa;" + "Password=std001;" + "TrustServerCertificate=True;";
        
        int selectedRentNo = 0;  // 현재 선택된 대여번호 저장,비디오 회수 및 수정 시 사용
        public Form2()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)  // 고객 검색 버튼                                                        
        {                                                       // 고객 정보를 조회하고 현재 대여목록을 출력한다
            SqlConnection conn =                               
       new SqlConnection(connStr);                            

            SqlCommand cmd =
                new SqlCommand("고객검색", conn);   // 고객검색 저장프로시저 호출

            cmd.CommandType =
                CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue(            // 고객명 검색조건 설정
                "@고객명",                             
                string.IsNullOrWhiteSpace(textBox1.Text)
                 ? (object)DBNull.Value
                 : textBox1.Text);

            cmd.Parameters.AddWithValue(            // 고객번호 검색조건 설정
                "@고객번호",
                string.IsNullOrWhiteSpace(textBox2.Text)
                ? (object)DBNull.Value
                : Convert.ToInt32(textBox2.Text));

            cmd.Parameters.AddWithValue(            // 전화번호 검색조건 설정
                "@전화번호",
                string.IsNullOrWhiteSpace(textBox3.Text)
                ? (object)DBNull.Value
                : textBox3.Text);

            cmd.Parameters.AddWithValue(            // 휴대폰번호 검색조건 설정
                "@휴대폰",
                string.IsNullOrWhiteSpace(textBox4.Text)
                ? (object)DBNull.Value
                : textBox4.Text);

            SqlDataAdapter da =                     // 검색결과 저장
                new SqlDataAdapter(cmd);

            DataTable dt =
                new DataTable();

            da.Fill(dt);

            if (dt.Rows.Count > 0)               // 검색된 고객정보 화면 출력
            {
                textBox5.Text =
                    dt.Rows[0]["고객번호"].ToString();

                textBox6.Text =
                    dt.Rows[0]["고객명"].ToString();

                textBox7.Text =
                    dt.Rows[0]["주민등록번호"].ToString();

                textBox8.Text =
                    dt.Rows[0]["고객신분"].ToString();

                textBox9.Text =
                    dt.Rows[0]["전화번호"].ToString();

                textBox10.Text =
                    dt.Rows[0]["휴대폰"].ToString();

                textBox11.Text =
                    dt.Rows[0]["우편번호"].ToString();

                textBox12.Text =
                    dt.Rows[0]["주소"].ToString();
            }
            else
            {
                MessageBox.Show("검색 결과가 없습니다.");
            }

            SqlCommand cmd2 =
                new SqlCommand("현재대여목록조회", conn);         // 현재 대여중인 비디오 조회

            cmd2.CommandType =
                CommandType.StoredProcedure;

            cmd2.Parameters.AddWithValue(
                "@고객번호",
                textBox5.Text); // 고객번호 출력칸

            SqlDataAdapter da2 =
                new SqlDataAdapter(cmd2);

            DataTable dt2 =
                new DataTable();

            da2.Fill(dt2);

            dataGridView1.DataSource = dt2;
            if (dataGridView1.Columns["대여번호"] != null)
            {
                dataGridView1.Columns["대여번호"]
                    .Visible = false;
            }
            총정보조회();
        }

        private void 비디오조회()                // 비디오코드 또는 제목으로                                         
        {                                        // 비디오 정보를 검색한다.   
            SqlConnection conn =
                new SqlConnection(connStr);

            SqlCommand cmd =
                new SqlCommand("비디오검색", conn);  // 비디오검색 저장프로시저 호출

            cmd.CommandType =
                CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue(
                "@비디오코드",
                string.IsNullOrWhiteSpace(textBox13.Text)
                ? (object)DBNull.Value
                : Convert.ToInt32(textBox13.Text));

            cmd.Parameters.AddWithValue(
                "@제목",
                string.IsNullOrWhiteSpace(textBox14.Text)
                ? (object)DBNull.Value
                : textBox14.Text);

            SqlDataAdapter da =
                new SqlDataAdapter(cmd);

            DataTable dt =
                new DataTable();

            da.Fill(dt);

            if (dt.Rows.Count == 0)
            {
                MessageBox.Show("비디오가 없습니다.");
                return;
            }

            textBox13.Text =
                dt.Rows[0]["비디오코드"].ToString();  // 조회된 비디오 정보 출력

            textBox14.Text =
                dt.Rows[0]["제목"].ToString();

            대여조건조회();
        }

        private void 대여조건조회()           // 신프로/구프로 판정                                    
        {                                     // 대여료, 연체료, 반납예정일 계산
            SqlConnection conn =
                new SqlConnection(connStr);

            SqlCommand cmd =
                new SqlCommand("대여조건조회", conn);  // 대여조건조회 저장프로시저 호출

            cmd.CommandType =
                CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue(
                "@비디오코드",
                Convert.ToInt32(textBox13.Text));

            SqlDataAdapter da =
                new SqlDataAdapter(cmd);

            DataTable dt =
                new DataTable();

            da.Fill(dt);

            if (dt.Rows.Count > 0)
            {
                textBox15.Text =
                    dt.Rows[0]["대여료"].ToString();   // 대여료 출력

                textBox16.Text =
                    dt.Rows[0]["연체료"].ToString();   // 연체료 출력

                int rentPeriod =
                    Convert.ToInt32(
                    dt.Rows[0]["대여기간"]);

                textBox17.Text =
                    dateTimePicker1.Value             // 반납예정일 자동 계산
                    .AddDays(rentPeriod)
                    .ToShortDateString();
            }
        }

        private void 현재대여목록조회()             // 고객의 현재 대여중인 비디오 조회
        {
            SqlConnection conn =
       new SqlConnection(connStr);

            SqlCommand cmd =                        // 현재대여목록조회 저장프로시저 호출
                new SqlCommand(
                    "현재대여목록조회",
                    conn);

            cmd.CommandType =
                CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue(
                "@고객번호",
                Convert.ToInt32(textBox5.Text));

            SqlDataAdapter da =
                new SqlDataAdapter(cmd);

            DataTable dt =
                new DataTable();

            da.Fill(dt);

            dataGridView1.DataSource = dt;          // 조회결과를 그리드뷰에 출력

            // 대여번호 숨기기
            dataGridView1.Columns["대여번호"]       // 내부키인 대여번호 숨김
                .Visible = false;

            총정보조회();     // 총 대여정보 계산
        }

        private void button3_Click(object sender, EventArgs e)          // 비디오 대여 등록
        {
            SqlConnection conn =
       new SqlConnection(connStr);

            SqlCommand cmd =
                new SqlCommand("대여등록", conn);           // 대여등록 저장프로시저 호출

            cmd.CommandType =
                CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue(                   // 고객번호 전달
                "@고객번호",
                Convert.ToInt32(textBox5.Text));

            cmd.Parameters.AddWithValue(                   // 비디오코드 전달
                "@비디오코드",
                Convert.ToInt32(textBox13.Text));

            cmd.Parameters.AddWithValue(                   // 대여일 전달
                "@대여일",
                dateTimePicker1.Value.Date);

            conn.Open();

            cmd.ExecuteNonQuery();

            conn.Close();

            MessageBox.Show("대여 등록 완료");

            현재대여목록조회();    // 등록 완료 후 목록 갱신
        }
        private void 총정보조회()        // 총 대여개수, 총 대여료                                    
        {                                // 총 연체료 계산   
            SqlConnection conn =
                new SqlConnection(connStr);

            conn.Open();

           
            SqlCommand cmd1 =          // 총 대여 개수 조회
                new SqlCommand(
                    "총대여개수조회",
                    conn);

            cmd1.CommandType =
                CommandType.StoredProcedure;

            cmd1.Parameters.AddWithValue(
                "@고객번호",
                Convert.ToInt32(textBox5.Text));

            textBox18.Text =
                cmd1.ExecuteScalar().ToString();

            
            SqlCommand cmd2 =           // 총 대여료 조회
                new SqlCommand(
                    "총대여료조회",
                    conn);

            cmd2.CommandType =
                CommandType.StoredProcedure;

            cmd2.Parameters.AddWithValue(
                "@고객번호",
                Convert.ToInt32(textBox5.Text));

            textBox19.Text =
                cmd2.ExecuteScalar().ToString();

                                    
            SqlCommand cmd3 =           // 총 연체료 조회
                new SqlCommand(
                    "총연체료조회",
                    conn);

            cmd3.CommandType =
                CommandType.StoredProcedure;

            cmd3.Parameters.AddWithValue(
                "@고객번호",
                Convert.ToInt32(textBox5.Text));

            textBox20.Text =
                cmd3.ExecuteScalar().ToString();

            conn.Close();
        }
        private void textBox13_KeyDown_1(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                비디오조회();
            }
        }

        private void textBox14_KeyDown_1(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                비디오조회();
            }
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(textBox13.Text))
            {
                대여조건조회();
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)    // 선택한 대여번호 저장
        {                                                                                   // 회수 기능에서 사용
            if (e.RowIndex >= 0)
            {
                selectedRentNo =            // 선택한 대여번호 저장
                    Convert.ToInt32(
                        dataGridView1.Rows[e.RowIndex]
                        .Cells["대여번호"]
                        .Value);
            }
        }

        private void button5_Click(object sender, EventArgs e)  // 비디오 반납 처리
        {
            if (selectedRentNo == 0) // 비디오 선택 여부 확인
            {
                MessageBox.Show(
                    "회수할 비디오를 선택하세요.");
                return;
            }

            DialogResult result =       // 반납 여부 확인
                MessageBox.Show(
                    "반납 처리하시겠습니까?",
                    "확인",
                    MessageBoxButtons.YesNo);

            if (result == DialogResult.No)    
            {
                return;
            }

            SqlConnection conn =
                new SqlConnection(connStr);

            SqlCommand cmd =        // 비디오회수 저장프로시저 호출
                new SqlCommand(
                    "비디오회수",
                    conn);

            cmd.CommandType =
                CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue(        // 선택된 대여번호 전달
                "@대여번호",
                selectedRentNo);

            conn.Open();

            cmd.ExecuteNonQuery();  // 반납 처리 실행

            conn.Close();

            MessageBox.Show(
                "비디오 회수 완료");

            현재대여목록조회();     // 반납 후 목록 새로고침
        }
    }
        
}
