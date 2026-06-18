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
    public partial class Form7 : Form
    {   // DB 연결 문자열
        string connStr = "Server=localhost;" + "Database=KSG;" + "User Id=sa;" + "Password=std001;" + "TrustServerCertificate=True;";
        public Form7()
        {
            InitializeComponent();
        }

        private void Form7_Load(object sender, EventArgs e)// 환경설정 폼 로드 시 현재 설정값을 조회한다
        {
            환경설정조회();
        }
        private void 환경설정조회()// 신프로 및 구프로 설정정보 조회
        {
            SqlConnection conn =
                new SqlConnection(connStr);

            SqlCommand cmd =  // 환경설정조회 저장프로시저 호출
                new SqlCommand(
                    "환경설정조회",
                    conn);

            cmd.CommandType =
                CommandType.StoredProcedure;

            SqlDataAdapter da =    // 조회결과 저장
                new SqlDataAdapter(cmd);

            DataTable dt =
                new DataTable();

            da.Fill(dt);
            // 조회된 설정값을 화면에 출력
            foreach (DataRow row in dt.Rows)
            {   // 신프로 설정 출력
                if (row["프로구분"].ToString()
                    == "신프로")
                {
                    textBox1.Text =
                        row["전환기간"].ToString();

                    textBox2.Text =
                        row["대여료"].ToString(); 

                    textBox3.Text =
                        row["대여기간"].ToString();

                    textBox4.Text =
                        row["연체료"].ToString();
                }
                else // 구프로 설정 출력
                {
                    textBox5.Text =
                        row["대여기간"].ToString();

                    textBox6.Text =
                        row["연체료"].ToString(); 

                    textBox7.Text =
                        row["대여료"].ToString();
                }
            }
        }

        private void button2_Click(object sender, EventArgs e) // 환경설정 수정,신프로 및 구프로 설정을 저장한다.
        {
            SqlConnection conn =
       new SqlConnection(connStr);

            conn.Open();

            // 신프로

            SqlCommand cmd1 = // 신프로 설정 수정
                new SqlCommand(
                    "환경설정수정",
                    conn);

            cmd1.CommandType =
                CommandType.StoredProcedure;

            cmd1.Parameters.AddWithValue(  // 프로구분 전달
                "@프로구분",
                "신프로");

            cmd1.Parameters.AddWithValue( // 신프로 대여료 전달
                "@대여료", 
                Convert.ToInt32(textBox2.Text));

            cmd1.Parameters.AddWithValue(// 신프로 대여기간 전달
                "@대여기간",
                Convert.ToInt32(textBox3.Text));

            cmd1.Parameters.AddWithValue(// 신프로 연체료 전달
                "@연체료",
                Convert.ToInt32(textBox4.Text));

            cmd1.Parameters.AddWithValue(// 신프로 전환기간 전달
                "@전환기간",
                Convert.ToInt32(textBox1.Text));

            cmd1.ExecuteNonQuery();     // 신프로 설정 저장

            

            SqlCommand cmd2 =  // 구프로 설정 수정
                new SqlCommand(
                    "환경설정수정",
                    conn);

            cmd2.CommandType =
                CommandType.StoredProcedure;

            cmd2.Parameters.AddWithValue(  // 프로구분 전달
                "@프로구분",
                "구프로");

            cmd2.Parameters.AddWithValue(// 구프로 대여기간 전달
                "@대여기간",
                Convert.ToInt32(textBox5.Text));

            cmd2.Parameters.AddWithValue(// 구프로 연체료 전달
                "@연체료",
                Convert.ToInt32(textBox6.Text));

            cmd2.Parameters.AddWithValue( // 구프로 대여료 전달
                "@대여료", 
                Convert.ToInt32(textBox7.Text));

            cmd2.Parameters.AddWithValue(// 구프로는 전환기간이 없으므로 NULL 전달
                "@전환기간",
                DBNull.Value);

            cmd2.ExecuteNonQuery();// 구프로 설정 저장

            conn.Close();

            MessageBox.Show(
                "수정 완료");
        }

        private void button3_Click(object sender, EventArgs e)// 환경설정 폼 종료
        {
            this.Close();
        }
    }
}
