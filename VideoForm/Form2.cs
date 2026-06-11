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

        string connStr = "Server=localhost;" + "Database=KSG;" + "User Id=sa;" + "Password=std001;" + "TrustServerCertificate=True;";
        public Form2()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SqlConnection conn =
       new SqlConnection(connStr);

            SqlCommand cmd =
                new SqlCommand("고객검색", conn);

            cmd.CommandType =
                CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue(
                "@고객명",
                string.IsNullOrWhiteSpace(textBox1.Text)
                 ? (object)DBNull.Value
                 : textBox1.Text);

            cmd.Parameters.AddWithValue(
                "@고객번호",
                string.IsNullOrWhiteSpace(textBox2.Text)
                ? (object)DBNull.Value
                : Convert.ToInt32(textBox2.Text));

            cmd.Parameters.AddWithValue(
                "@전화번호",
                string.IsNullOrWhiteSpace(textBox3.Text)
                ? (object)DBNull.Value
                : textBox3.Text);

            cmd.Parameters.AddWithValue(
                "@휴대폰",
                string.IsNullOrWhiteSpace(textBox4.Text)
                ? (object)DBNull.Value
                : textBox4.Text);

            SqlDataAdapter da =
                new SqlDataAdapter(cmd);

            DataTable dt =
                new DataTable();

            da.Fill(dt);

            if (dt.Rows.Count > 0)
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
                new SqlCommand("현재대여목록조회", conn);

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
        }

        private void 비디오조회()
        {
            SqlConnection conn =
                new SqlConnection(connStr);

            SqlCommand cmd =
                new SqlCommand("비디오검색", conn);

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
                dt.Rows[0]["비디오코드"].ToString();

            textBox14.Text =
                dt.Rows[0]["제목"].ToString();

            대여조건조회();
        }

        private void 대여조건조회()
        {
            SqlConnection conn =
                new SqlConnection(connStr);

            SqlCommand cmd =
                new SqlCommand("대여조건조회", conn);

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
                    dt.Rows[0]["대여료"].ToString();

                textBox16.Text =
                    dt.Rows[0]["연체료"].ToString();

                textBox17.Text =
                    DateTime.Now.AddDays(
                        Convert.ToInt32(
                            dt.Rows[0]["대여기간"]))
                    .ToShortDateString();
            }
        }

        private void 현재대여목록조회()
        {
            SqlConnection conn =
                new SqlConnection(connStr);

            SqlCommand cmd =
                new SqlCommand("현재대여목록조회", conn);

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

            dataGridView1.DataSource = dt;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            SqlConnection conn =
       new SqlConnection(connStr);

            SqlCommand cmd =
                new SqlCommand("대여등록", conn);

            cmd.CommandType =
                CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue(
                "@고객번호",
                Convert.ToInt32(textBox5.Text));

            cmd.Parameters.AddWithValue(
                "@비디오코드",
                Convert.ToInt32(textBox13.Text));

            conn.Open();

            cmd.ExecuteNonQuery();

            conn.Close();

            MessageBox.Show("대여 등록 완료");

            현재대여목록조회();
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
    }
        
}
