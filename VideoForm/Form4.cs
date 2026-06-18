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

    public partial class Form4 : Form
    {   // DB 연결 문자열
        string connStr = "Server=localhost;" + "Database=KSG;" + "User Id=sa;" + "Password=std001;" + "TrustServerCertificate=True;";
        public Form4()
        {
            InitializeComponent();
        }

        private void Form4_Load(object sender, EventArgs e)// 고객관리 폼 로드 시 전체 고객 목록을 조회한다.
        {
            고객전체조회();
        }
        private void 고객전체조회()   // 고객 전체 목록 조회
        {
            SqlConnection conn =
                new SqlConnection(connStr);

            SqlCommand cmd =  // 고객전체조회 저장프로시저 호출
                new SqlCommand(
                    "고객전체조회",
                    conn);

            cmd.CommandType =
                CommandType.StoredProcedure;

            SqlDataAdapter da =  // 조회 결과 저장
                new SqlDataAdapter(cmd);

            DataTable dt =
                new DataTable();

            da.Fill(dt);

            dataGridView1.DataSource = dt; // 그리드뷰에 고객목록 출력

            textBox10.Text =    // 전체 고객 수 출력
                dt.Rows.Count.ToString();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)// 선택한 고객 정보를 입력영역에 출력한다
        {
            if (e.RowIndex < 0)  // 헤더 클릭 방지 (데이터 행이 아닌 헤더 클릭 시 오류방지)
                return;

            DataGridViewRow row =
                dataGridView1.Rows[e.RowIndex];

            textBox1.Text = // 고객 정보 출력
                row.Cells["고객번호"].Value.ToString();

            textBox2.Text =
                row.Cells["고객명"].Value.ToString();

            textBox3.Text =
                row.Cells["주민등록번호"].Value.ToString();

            textBox4.Text =
                row.Cells["고객신분"].Value.ToString();

            textBox5.Text =
                row.Cells["성별"].Value.ToString();

            textBox6.Text =
                row.Cells["전화번호"].Value.ToString();

            textBox7.Text =
                row.Cells["휴대폰"].Value.ToString();

            textBox8.Text =
                row.Cells["우편번호"].Value.ToString();

            textBox9.Text =
                row.Cells["주소"].Value.ToString();
        }

        private void button1_Click(object sender, EventArgs e)// 신규 고객 등록
        {
            SqlConnection conn =
       new SqlConnection(connStr);

            SqlCommand cmd =    // 고객추가 저장프로시저 호출
                new SqlCommand(
                    "고객추가",
                    conn);

            cmd.CommandType =
                CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue(  // 고객 정보 전달
                "@고객번호",
                Convert.ToInt32(textBox1.Text));

            cmd.Parameters.AddWithValue(
                "@고객명",
                textBox2.Text);

            cmd.Parameters.AddWithValue(
                "@주민등록번호",
                textBox3.Text);

            cmd.Parameters.AddWithValue(
                "@고객신분",
                textBox4.Text);

            cmd.Parameters.AddWithValue(
                "@성별",
                textBox5.Text);

            cmd.Parameters.AddWithValue(
                "@전화번호",
                textBox6.Text);

            cmd.Parameters.AddWithValue(
                "@휴대폰",
                textBox7.Text);

            cmd.Parameters.AddWithValue(
                "@우편번호",
                textBox8.Text);

            cmd.Parameters.AddWithValue(
                "@주소",
                textBox9.Text);

            conn.Open();

            cmd.ExecuteNonQuery(); // 고객 등록 실행

            conn.Close();

            MessageBox.Show(
                "고객 등록 완료");

            고객전체조회(); // 목록 새로고침
            초기화();// 입력창 초기화
        }

        private void button2_Click(object sender, EventArgs e)// 선택한 고객 정보 수정
        {
            SqlConnection conn =
       new SqlConnection(connStr);

            SqlCommand cmd =     // 고객수정 저장프로시저 호출
                new SqlCommand(
                    "고객수정",
                    conn);

            cmd.CommandType =
                CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue(    // 수정할 고객 정보 전달
                "@고객번호",
                Convert.ToInt32(textBox1.Text));

            cmd.Parameters.AddWithValue(
                "@고객명",
                textBox2.Text);

            cmd.Parameters.AddWithValue(
                "@주민등록번호",
                textBox3.Text);

            cmd.Parameters.AddWithValue(
                "@고객신분",
                textBox4.Text);

            cmd.Parameters.AddWithValue(
                "@성별",
                textBox5.Text);

            cmd.Parameters.AddWithValue(
                "@전화번호",
                textBox6.Text);

            cmd.Parameters.AddWithValue(
                "@휴대폰",
                textBox7.Text);

            cmd.Parameters.AddWithValue(
                "@우편번호",
                textBox8.Text);

            cmd.Parameters.AddWithValue(
                "@주소",
                textBox9.Text);

            conn.Open();

            cmd.ExecuteNonQuery();   // 수정 실행

            conn.Close();

            MessageBox.Show(
                "수정 완료");

            고객전체조회();  // 목록 새로고침
        }

        private void button3_Click(object sender, EventArgs e)// 선택한 고객 삭제
        {
            SqlConnection conn =
       new SqlConnection(connStr);

            SqlCommand cmd =  // 고객삭제 저장프로시저 호출 
                new SqlCommand(
                    "고객삭제",
                    conn);

            cmd.CommandType =
                CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue(   // 삭제할 고객번호 전달
                "@고객번호",
                Convert.ToInt32(textBox1.Text));

            conn.Open();

            cmd.ExecuteNonQuery(); // 삭제 실행

            conn.Close();

            MessageBox.Show(
                "삭제 완료");

            고객전체조회();   // 목록 새로고침
            초기화(); // 입력창 초기화
        }
        private void 초기화()  // 고객 정보 입력영역 초기화
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox5.Clear();
            textBox6.Clear();
            textBox7.Clear();
            textBox8.Clear();
            textBox9.Clear();
        }

        private void button4_Click(object sender, EventArgs e)// 초기화 버튼
        {
            초기화();
        }

        private void button6_Click(object sender, EventArgs e)// 고객관리 폼 종료
        {
            this.Close();
        }
    }
}
