using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Xml.Linq;

using OpenCvSharp;
using OpenCvSharp.Extensions;

namespace VDisplay
{
    public partial class 基于视觉智能的多人场景视频监控平台项目演示 : Form
    {
        public 基于视觉智能的多人场景视频监控平台项目演示()
        {
            InitializeComponent();
            //this.skinEngine1 = new Sunisoft.IrisSkin.SkinEngine(((System.ComponentModel.Component)(this)));
            //this.skinEngine1.SkinFile = Application.StartupPath + "//WarmColor2.ssk";
        }
        System.Windows.Forms.FolderBrowserDialog folderdia = new FolderBrowserDialog();
        static string foldername;
        static string[] image_file_top;
        static string[] image_file_hor1;
        static string[] image_file_hor2;
        static string[] image_file_hor3;
        static string[] pos_file_top, pos_file_hor1, pos_file_hor2, pos_file_hor3;
        string[] lines1;
        string[] lines2;
        string[] lines3;
        string[] lines4;
        static int index = 0, length;
        static int cnt1 = 0;
        static int cnt2 = 0;
        static int cnt3 = 0;
        static int cnt4 = 0;
        Bitmap[] buffimage = new Bitmap[5];
        static bool isopen = false;

        private void videoStart_Click(object sender, EventArgs e)
        {
            isopen = false;
            timer.Stop();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
        int mode = 4;
        string[] pos1, pos2, pos3, pos4;
        int num;
        int itemCnt;
        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            
        }
        private void pictureBox4_Paint(object sender, PaintEventArgs e)
        {
            
        }

        private void pictureBox2_Paint(object sender, PaintEventArgs e)
        {
            
        }

        private void pictureBox3_Paint(object sender, PaintEventArgs e)
        {
            
        }

        private void videoFStart_Click(object sender, EventArgs e)
        {
            isopen = true;
            if (this.timer.Enabled)
            {
                this.timer.Stop();
            }
            else
            {
                lines1 = System.IO.File.ReadAllLines(pos_file_top[0]);
                lines2 = System.IO.File.ReadAllLines(pos_file_hor1[0]);
                if (mode >= 3) lines3 = System.IO.File.ReadAllLines(pos_file_hor2[0]);
                if (mode == 4) lines4 = System.IO.File.ReadAllLines(pos_file_hor3[0]);
                length = pos_file_top.Length;
                this.timer.Start();
            }
        }

        private void timer1Start_Click(object sender, EventArgs e)
        {

        }
        
        float X;
        float Y;
        private void Form1_Load(object sender, EventArgs e) //装载窗口事件, 是窗体启动时触发的事件，一般写程序你把自己要初始化的东东可以放在Form1_Load中
        {
            this.Resize += new EventHandler(Form1_Resize);//窗体调整大小时引发事件
            X = this.Width;//获取窗体的宽度
            Y = this.Height;//获取窗体的高度
            setTag(this);//调用方法
        }

        private void setTag(Control cons)
        {
            //遍历窗体中的控件
            foreach (Control con in cons.Controls)
            {
                con.Tag = con.Width + ":" + con.Height + ":" + con.Left + ":" + con.Top + ":" + con.Font.Size;
                if (con.Controls.Count > 0)
                    setTag(con);
            }
        }
        
        private void setControls(float newx, float newy, Control cons)
        {
            //遍历窗体中的控件，重新设置控件的值
            foreach (Control con in cons.Controls)
            {
                string[] mytag = con.Tag.ToString().Split(new char[] { ':' });//获取控件的Tag属性值，并分割后存储字符串数组
                float a = Convert.ToSingle(mytag[0]) * newx;//根据窗体缩放比例确定控件的值，宽度
                con.Width = (int)a;//宽度
                a = Convert.ToSingle(mytag[1]) * newy;//高度
                con.Height = (int)(a);
                a = Convert.ToSingle(mytag[2]) * newx;//左边距离
                con.Left = (int)(a);
                a = Convert.ToSingle(mytag[3]) * newy;//上边缘距离
                con.Top = (int)(a);
                Single currentSize = Convert.ToSingle(mytag[4]) * newy;//字体大小
                con.Font = new Font(con.Font.Name, currentSize, con.Font.Style, con.Font.Unit);
                if (con.Controls.Count > 0)
                {
                    setControls(newx, newy, con);
                }
            }
        }
        
        void Form1_Resize(object sender, EventArgs e)
        {
            float newx = (this.Width) / X; //窗体宽度缩放比例
            float newy = this.Height / Y;//窗体高度缩放比例
            setControls(newx, newy, this);//随窗体改变控件大小
        }
        
        private void pictureBox4_Click(object sender, EventArgs e)
        {

        }

        private void videoFTimer_Tick(object sender, EventArgs e)
        {
            if (isopen)
            {
                pictureBox1.Refresh();
                pictureBox4.Refresh();//马上刷新
            }
        }

        private void Pic2Timer_Tick(object sender, EventArgs e)
        {
            if (isopen && mode >= 3)
            {
                pictureBox2.Refresh();
            }
        }

        private void Pic3Timer_Tick(object sender, EventArgs e)
        {
            if (isopen && mode == 4)
            {
                pictureBox3.Refresh();
            }
        }

        private void Pic4Timer_Tick(object sender, EventArgs e)
        {
            if (isopen)
            {
                pictureBox4.Refresh();
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void uiComboTreeView1_NodeSelected(object sender, TreeNode node)
        {

        }

        private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void uiDataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void uiDataGridView1_RowStateChanged(object sender, DataGridViewRowStateChangedEventArgs e)//行头显示行号
        {
            //显示在HeaderCell上
            for (int i = 0; i < this.uiDataGridView1.Rows.Count; i++)
            {
                DataGridViewRow r = this.uiDataGridView1.Rows[i];
                r.HeaderCell.Value = string.Format("{0}", i + 1);
            }
            this.uiDataGridView1.Refresh();
        }


        private void uiSymbolButton2_Click(object sender, EventArgs e)
        {

        }

        private void uiSymbolButton3_Click(object sender, EventArgs e)
        {

        }

        private void 开始ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void Line_Control_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void uiLine1_Click(object sender, EventArgs e)
        {

        }

        private void Line_View_Click(object sender, EventArgs e)
        {

        }

        private void uiSymbolButton4_Click(object sender, EventArgs e)
        {

        }

        private void timer_Tick(object sender, EventArgs e)
        {
            int clength = lines1.Length;
            Mat image1 = new Mat(image_file_top[index], ImreadModes.AnyColor);
            buffimage[1] = image1.ToBitmap();
            Graphics g1 = Graphics.FromImage(buffimage[1]);
            int ct = cnt1;
            while (cnt1 < lines1.Length)
            {
                string[] words = lines1[cnt1].Split(',');
                if (words[0] != index.ToString())
                {
                    break;
                }
                else
                {
                    var xmin = float.Parse(words[2]);
                    var ymin = float.Parse(words[3]);
                    var xlen = float.Parse(words[4]);
                    var ylen = float.Parse(words[5]);
                    g1.DrawRectangle(new Pen(Color.Red, 2), xmin, ymin, xlen, ylen);
                    ++cnt1;
                }
            }
            image1.Release();

            Mat image2 = null;
            Graphics g2 = null;
            buffimage[2] = null;
            if (mode >= 3)
            {
                clength = lines2.Length;
                image2 = new Mat(image_file_hor1[index], ImreadModes.AnyColor);
                buffimage[2] = image2.ToBitmap();
                g2 = Graphics.FromImage(buffimage[2]);
                while (cnt2 < clength)
                {
                    string[] words = null;
                    words = lines2[cnt2].Split(',');
                    if (words[0] != index.ToString())
                    {
                        break;
                    }
                    else
                    {
                        var xmin = float.Parse(words[2]);
                        var ymin = float.Parse(words[3]);
                        var xlen = float.Parse(words[4]);
                        var ylen = float.Parse(words[5]);
                        g2.DrawRectangle(new Pen(Color.Red, 2), xmin, ymin, xlen, ylen);
                        ++cnt2;
                    }
                }
                image2.Release();
            }
            Mat image3 = null;
            Graphics g3 = null;
            buffimage[3] = null;
            if (mode == 4)
            {
                clength = lines3.Length;
                image3 = new Mat(image_file_hor2[index], ImreadModes.AnyColor);
                buffimage[3] = image3.ToBitmap();
                g3 = Graphics.FromImage(buffimage[3]);
                while (cnt3 < clength)
                {
                    string[] words = lines3[cnt3].Split(',');
                    if (words[0] != index.ToString())
                    {
                        break;
                    }
                    else
                    {
                        var xmin = float.Parse(words[2]);
                        var ymin = float.Parse(words[3]);
                        var xlen = float.Parse(words[4]);
                        var ylen = float.Parse(words[5]);
                        g3.DrawRectangle(new Pen(Color.Red, 2), xmin, ymin, xlen, ylen);
                        ++cnt3;
                    }
                }
                image3.Release();
            }

            Mat image4 = null;
            if (mode == 2)
            {
                image4 = new Mat(image_file_hor1[index], ImreadModes.AnyColor);
                clength = lines2.Length;
            }
            else if (mode == 3)
            {
                image4 = new Mat(image_file_hor2[index], ImreadModes.AnyColor);
                clength = lines3.Length;
            }
            else if (mode == 4)
            {
                image4 = new Mat(image_file_hor3[index], ImreadModes.AnyColor);
                clength = lines4.Length;
            }
            buffimage[4] = image4.ToBitmap();
            Graphics g4 = Graphics.FromImage(buffimage[4]);
            while (cnt4 < clength)
            {
                string[] words = null;
                if (mode == 2) words = lines2[cnt4].Split(',');
                else if (mode == 3) words = lines3[cnt4].Split(',');
                else words = lines4[cnt4].Split(',');
                if (words[0] != index.ToString())
                {
                    break;
                }
                else
                {
                    var xmin = float.Parse(words[2]);
                    var ymin = float.Parse(words[3]);
                    var xlen = float.Parse(words[4]);
                    var ylen = float.Parse(words[5]);
                    g4.DrawRectangle(new Pen(Color.Red, 2), xmin, ymin, xlen, ylen);
                    ++cnt4;
                }
            }
            image4.Release();
            if (mode == 4)
            {
                pictureBox1.WaitOnLoad = false;
                pictureBox1.Image = buffimage[1];
                pictureBox1.Refresh();
                pictureBox2.WaitOnLoad = false;
                pictureBox2.Image = buffimage[2];
                pictureBox2.Refresh();
                pictureBox3.WaitOnLoad = false;
                pictureBox3.Image = buffimage[3];
                pictureBox3.Refresh();
                pictureBox4.WaitOnLoad = false;
                pictureBox4.Image = buffimage[4];
                pictureBox4.Refresh();
                buffimage[2].Dispose();
                buffimage[3].Dispose();
                image2.Dispose();
                image3.Dispose();
                g2.Dispose();
                g3.Dispose();

            }
            else if (mode == 3)
            {
                pictureBox1.WaitOnLoad = false;
                pictureBox1.Image = buffimage[1];
                pictureBox1.Refresh();
                pictureBox2.WaitOnLoad = false;
                pictureBox2.Image = buffimage[2];
                pictureBox2.Refresh();
                pictureBox4.WaitOnLoad = false;
                pictureBox4.Image = buffimage[4];
                pictureBox4.Refresh();
                buffimage[2].Dispose();
                image2.Dispose();
                g2.Dispose();
            }
            else if (mode == 2)
            {
                pictureBox1.WaitOnLoad = false;
                pictureBox1.Image = buffimage[1];
                pictureBox1.Refresh();
                pictureBox4.WaitOnLoad = false;
                pictureBox4.Image = buffimage[4];
                pictureBox4.Refresh();
            }
            index++;
            if (index == image_file_top.Length)
            {
                index = 0;
                timer.Stop();
            }
            buffimage[1].Dispose();
            buffimage[4].Dispose();
            image1.Dispose();
            image4.Dispose();
            g1.Dispose();
            g4.Dispose();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }


        /*private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (treeView1.SelectedNode.Text == "1俯视+1平视")
            {
                pictureBox2.Visible = false;
                pictureBox3.Visible = false;
                axWindowsMediaPlayer1.Width = 268;
                axWindowsMediaPlayer1.Height = 177;
                pictureBox1.Width = 268;
                pictureBox1.Height = 177;
                pictureBox1.Location = new System.Drawing.Point(226, 287);

            }
            if (treeView1.SelectedNode.Text == "1俯视+3平视")
            {
                pictureBox2.Visible = true;
                pictureBox3.Visible = true;
                axWindowsMediaPlayer1.Width = 364;
                axWindowsMediaPlayer1.Height = 244;
                pictureBox1.Width = 200;
                pictureBox1.Height = 112;
                pictureBox1.Location = new System.Drawing.Point(226, 351);
            }

        }*/

        private void button1_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = null;
            pictureBox2.Image = null;
            pictureBox3.Image = null;
            pictureBox4.Image = null;
            image_file_top = null;
            image_file_hor1 = null;
            image_file_hor2 = null;
            image_file_hor3 = null;
            pos_file_top = null;
            pos_file_hor1 = null;
            pos_file_hor2 = null;
            pos_file_hor3 = null;
            isopen = false;
            index = 0;
            timer.Stop();
        }

        private void 帮助ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }


        private void 选择ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void walking2mpToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void 俯视1平视ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pictureBox2.Visible = false;
            label2.Visible = false;
            pictureBox3.Visible = false;
            label3.Visible = false;
            pictureBox1.Width = 268;
            pictureBox1.Height = 177;
            pictureBox4.Width = 268;
            pictureBox4.Height = 177;
            pictureBox1.Location = new System.Drawing.Point(147, 87);
            label1.Location = new System.Drawing.Point(147, 87);
            pictureBox4.Location = new System.Drawing.Point(147, 280);
            label4.Location = new System.Drawing.Point(147, 280);

        }

        private void 俯视3平视ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pictureBox2.Visible = true;
            pictureBox3.Visible = false;
            pictureBox1.Width = 242;
            pictureBox1.Height = 156;
            pictureBox4.Width = 242;
            pictureBox4.Height = 156;
            pictureBox1.Location = new System.Drawing.Point(146, 87);
            pictureBox4.Location = new System.Drawing.Point(270, 259);
        }

        private void 打开文件ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            mode = 4;
            if (folderdia.ShowDialog() == DialogResult.OK)
            {
                string text = folderdia.SelectedPath;
                foldername = text;
                /* 顶视 */
                string[] f1;
                f1 = System.IO.Directory.GetDirectories(foldername, "*top");
                if (f1 == null)
                {
                    //textBox2.Text = "未找到视频！";
                    return;
                }
                image_file_top = System.IO.Directory.GetFiles(f1[0], "*.jpg");
                pos_file_top = System.IO.Directory.GetFiles(f1[0], "*.txt");

                /* 平视1 */
                string[] f2;
                f2 = System.IO.Directory.GetDirectories(foldername, "*hor1");
                if (f2 == null)
                {
                    //textBox2.Text = "未找到视频！";
                    return;
                }
                image_file_hor1 = System.IO.Directory.GetFiles(f2[0], "*.jpg");
                pos_file_hor1 = System.IO.Directory.GetFiles(f2[0], "*.txt");

                /* 平视2 */
                string[] f3;
                f3 = System.IO.Directory.GetDirectories(foldername, "*hor2");
                if (f3.Length == 0)
                {
                    mode = 2;
                    pictureBox2.Visible = false;
                    label2.Visible = false;
                    pictureBox3.Visible = false;
                    label3.Visible = false;
                    pictureBox1.Width = 268;
                    pictureBox1.Height = 177;
                    pictureBox4.Width = 268;
                    pictureBox4.Height = 177;
                    pictureBox1.Location = new System.Drawing.Point(147, 87);
                    label1.Location = new System.Drawing.Point(147, 87);
                    pictureBox4.Location = new System.Drawing.Point(147, 280);
                    label4.Location = new System.Drawing.Point(147, 280);
                    label4.Text = "HorizontalView1";
                }
                else
                {
                    image_file_hor2 = System.IO.Directory.GetFiles(f3[0], "*.jpg");
                    pos_file_hor2 = System.IO.Directory.GetFiles(f3[0], "*.txt");
                }

                if (mode != 2)
                {
                    /* 平视3 */
                    string[] f4;
                    f4 = System.IO.Directory.GetDirectories(foldername, "*hor3");
                    if (f4.Length == 0)
                    {
                        mode = 3;
                        pictureBox2.Visible = true;
                        label2.Visible = true;
                        pictureBox3.Visible = false;
                        label3.Visible = false;
                        pictureBox1.Width = 245;
                        pictureBox1.Height = 169;
                        pictureBox4.Width = 245;
                        pictureBox4.Height = 169;
                        pictureBox1.Location = new System.Drawing.Point(146, 87);
                        label1.Location = new System.Drawing.Point(146, 87);
                        pictureBox4.Location = new System.Drawing.Point(270, 259);
                        label4.Location = new System.Drawing.Point(270, 259);
                        label4.Text = "HorizontalView2";
                    }
                    else
                    {
                        mode = 4;
                        pictureBox2.Visible = true;
                        label2.Visible = true;
                        pictureBox3.Visible = true;
                        label3.Visible = true;
                        pictureBox1.Width = 245;
                        pictureBox1.Height = 169;
                        pictureBox4.Width = 245;
                        pictureBox4.Height = 169;
                        pictureBox1.Location = new System.Drawing.Point(12, 74);
                        label1.Location = new System.Drawing.Point(12, 74);
                        pictureBox4.Location = new System.Drawing.Point(266, 259);
                        label4.Location = new System.Drawing.Point(266, 259);
                        label4.Text = "HorizontalView3";
                        image_file_hor3 = System.IO.Directory.GetFiles(f4[0], "*.jpg");
                        //pos4 = System.IO.Directory.GetDirectories(f4[0], "*xml");
                        pos_file_hor3 = System.IO.Directory.GetFiles(f4[0], "*.txt");
                    }
                }
            }

        }

        private void 俯视ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pictureBox2.Visible = true;
            pictureBox3.Visible = true;
            pictureBox1.Width = 242;
            pictureBox1.Height = 156;
            pictureBox4.Width = 242;
            pictureBox4.Height = 156;
            pictureBox1.Location = new System.Drawing.Point(22, 87);
            pictureBox4.Location = new System.Drawing.Point(270, 259);
        }

        
    }
}




