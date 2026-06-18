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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace VideoForm
{
    public partial class Form3 : Form
    {
        // DB 연결 문자열
        string connStr = "Server=localhost;" + "Database=KSG;" + "User Id=sa;" + "Password=std001;" + "TrustServerCertificate=True;";
        public Form3()
        {
            InitializeComponent();

        }

        private void Form3_Load_1(object sender, EventArgs e)// 비디오관리 폼 로드                                                             
        {                                                    // 장르 목록과 비디오 목록을 조회한다.
            장르조회();
            비디오전체조회();
        }

        private void 장르조회()// 장르정보 테이블을 조회하여                           
        {                      // 장르 콤보박스에 출력한다.
            SqlConnection conn =
                new SqlConnection(connStr);

            SqlCommand cmd =
                new SqlCommand("장르조회", conn); // 장르조회 저장프로시저 호출

            cmd.CommandType =
                CommandType.StoredProcedure;

            SqlDataAdapter da =   // 조회 결과 저장
                new SqlDataAdapter(cmd);

            DataTable dt =
                new DataTable();

            da.Fill(dt);

            comboBox1.DataSource = dt;  // 장르 목록을 콤보박스에 바인딩

            comboBox1.DisplayMember =
                "장르명";

            comboBox1.ValueMember =
                "장르코드";
        }

        private void 비디오전체조회()  // 비디오 전체 목록 조회
        {
            SqlConnection conn =
                new SqlConnection(connStr);

            SqlCommand cmd =    // 비디오전체조회 저장프로시저 호출
                new SqlCommand(
                    "비디오전체조회",
                    conn);

            cmd.CommandType =
                CommandType.StoredProcedure;

            SqlDataAdapter da =  // 조회 결과 저장
                new SqlDataAdapter(cmd);

            DataTable dt =
                new DataTable();

            da.Fill(dt);

            dataGridView1.DataSource = dt;  // 그리드뷰에 비디오 목록 출력

            textBox7.Text = // 전체 비디오 개수 출력
                dt.Rows.Count.ToString();
        }

        private void dataGridView1_CellClick_1(// 선택한 비디오 정보를 입력영역에 출력한다.                                               
            object sender,
            DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)  // 헤더 클릭 방지
                return;

            DataGridViewRow row =
                dataGridView1.Rows[e.RowIndex];

            textBox1.Text =      // 비디오 정보 출력
                row.Cells["비디오코드"].Value.ToString();

            comboBox1.SelectedValue =
                row.Cells["장르코드"].Value;

            textBox2.Text =
                row.Cells["제목"].Value.ToString();

            textBox3.Text =
                row.Cells["주연배우"].Value.ToString();

            textBox6.Text =
                row.Cells["감독"].Value.ToString();

            textBox4.Text =
                row.Cells["제작및배급"].Value.ToString();

            dateTimePicker1.Value =
                Convert.ToDateTime(
                    row.Cells["출시일"].Value);
        }

        private void 초기화()// 입력영역 초기화
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox6.Clear();

            comboBox1.SelectedIndex = -1;

            dateTimePicker1.Value =
                DateTime.Today;
        }

        private void button1_Click_1(// 신규 비디오 등록
            object sender,
            EventArgs e)
        {
            SqlConnection conn =
                new SqlConnection(connStr);

            SqlCommand cmd =  // 비디오추가 저장프로시저 호출
                new SqlCommand(
                    "비디오추가",
                    conn);

            cmd.CommandType =
                CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue(      // 비디오 정보 전달
                "@비디오코드",
                Convert.ToInt32(textBox1.Text));

            cmd.Parameters.AddWithValue(
                "@장르코드",
                comboBox1.SelectedValue);

            cmd.Parameters.AddWithValue(
                "@제목",
                textBox2.Text);

            cmd.Parameters.AddWithValue(
                "@주연배우",
                textBox3.Text);

            cmd.Parameters.AddWithValue(
                "@감독",
                textBox6.Text);

            cmd.Parameters.AddWithValue(
                "@제작및배급",
                textBox4.Text);

            cmd.Parameters.AddWithValue(
                "@출시일",
                dateTimePicker1.Value.Date);

            conn.Open();

            cmd.ExecuteNonQuery();// 비디오 등록 실행

            conn.Close();

            MessageBox.Show(
                "추가 완료");

            비디오전체조회();  // 목록 새로고침
            초기화();   // 입력창 초기화
        }

        private void button2_Click_1(// 선택한 비디오 정보 수정
            object sender,
            EventArgs e)
        {
            SqlConnection conn =
                new SqlConnection(connStr);

            SqlCommand cmd =   // 비디오수정 저장프로시저 호출
                new SqlCommand(
                    "비디오수정",
                    conn);

            cmd.CommandType =
                CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue(// 수정할 비디오 정보 전달
                "@비디오코드",
                Convert.ToInt32(textBox1.Text));

            cmd.Parameters.AddWithValue(
                "@장르코드",
                comboBox1.SelectedValue);

            cmd.Parameters.AddWithValue(
                "@제목",
                textBox2.Text);

            cmd.Parameters.AddWithValue(
                "@주연배우",
                textBox3.Text);

            cmd.Parameters.AddWithValue(
                "@감독",
                textBox6.Text);

            cmd.Parameters.AddWithValue(
                "@제작및배급",
                textBox4.Text);

            cmd.Parameters.AddWithValue(
                "@출시일",
                dateTimePicker1.Value.Date);

            conn.Open();

            cmd.ExecuteNonQuery();  // 수정 실행

            conn.Close();

            MessageBox.Show(
                "수정 완료");

            비디오전체조회(); // 목록 새로고침
        }

        private void button3_Click_1(// 선택한 비디오 삭제
            object sender,
            EventArgs e)
        {
            SqlConnection conn =
                new SqlConnection(connStr);

            SqlCommand cmd =        // 비디오삭제 저장프로시저 호출
                new SqlCommand(
                    "비디오삭제",
                    conn);

            cmd.CommandType =
                CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue(    // 삭제할 비디오코드 전달
                "@비디오코드",
                Convert.ToInt32(textBox1.Text));

            conn.Open();

            cmd.ExecuteNonQuery();  // 삭제 실행

            conn.Close();

            MessageBox.Show(
                "삭제 완료");

            비디오전체조회(); // 목록 새로고침
            초기화();// 입력창 초기화
        }

        private void button4_Click_1(// 입력영역 초기화 버튼
            object sender,
            EventArgs e)
        {
            초기화();
        }

        private void button5_Click_1(// 비디오관리 폼 종료
            object sender,
            EventArgs e)
        {
            this.Close();
        }
    }
}