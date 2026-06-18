using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VideoForm
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)  // 비디오 대여/반납 폼 열기
        {
            Form2 Rental_management = new Form2();
            Rental_management.MdiParent = this;
            Rental_management.Show();
        }

        private void toolStripButton2_Click(object sender, EventArgs e) // 비디오  관리 폼 열기
        {
            Form3 video_information = new Form3();
            video_information.MdiParent = this;
            video_information.Show();
        }

        private void toolStripButton3_Click(object sender, EventArgs e) // 고객 관리 폼 열기
        {
            Form4 Customer_information = new Form4();
            Customer_information.MdiParent = this;
            Customer_information.Show();
        }

        private void toolStripButton4_Click(object sender, EventArgs e)// 정보 조회 및 엑셀 출력 폼 열기     
        {
            Form5 Video_inquiry_management = new Form5();
            Video_inquiry_management.MdiParent = this;
            Video_inquiry_management.Show();

            Form6 Video_Genre_Lookup = new Form6();
            Video_Genre_Lookup.MdiParent = this;
            Video_Genre_Lookup.Show();
        }

        private void toolStripButton5_Click(object sender, EventArgs e)//환경설정 폼 열기
        {
            Form7 Video_rental_settings = new Form7();
            Video_rental_settings.MdiParent= this;
            Video_rental_settings.Show();
        }

        private void 종료ToolStripMenuItem_Click(object sender, EventArgs e)// 프로그램 종료
        {
            DialogResult result = MessageBox.Show(
            "프로그램을 종료하시겠습니까?",
            "종료 확인",
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void 비디오대여ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            toolStripButton1_Click(sender, e);
        
        }

        private void 비디오관리ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            toolStripButton2_Click(sender, e);
        }

        private void 고객관리ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            toolStripButton3_Click(sender, e);
        }

        private void 정보조회ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            toolStripButton4_Click(sender, e);
        }

        private void 환경설정ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            toolStripButton5_Click(sender, e);
        }
    }
}
