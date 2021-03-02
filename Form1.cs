using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
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
            for(int i = 0; i < 10; i++)
            {
                person[i] = new Person();
                person[i].color = colors[i];
            }
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
        static int index = 0;
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
                
                //length = pos_file_top.Length;
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
            uiDataGridView1.ColumnHeadersHeight = Convert.ToInt32(uiDataGridView1.ColumnHeadersHeight * newy);

            uiDataGridView1.RowHeadersWidth = Convert.ToInt32(uiDataGridView1.RowHeadersWidth * newx);
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
        Color[] colors = {Color.Black, Color.Red, Color.Pink, Color.Orange, Color.Yellow, Color.Green, Color.Blue, Color.Purple, Color.Gold, Color.Brown };
        string[] lines;
        class Person
        {
            public float[] top_xmin { get; set; }
            public float[] top_ymin { get; set; }
            public float[] top_xlen { get; set; }
            public float[] top_ylen { get; set; }
            public float[] hor1_xmin { get; set; }
            public float[] hor1_ymin { get; set; }
            public float[] hor1_xlen { get; set; }
            public float[] hor1_ylen { get; set; }
            public float[] hor2_xmin { get; set; }
            public float[] hor2_ymin { get; set; }
            public float[] hor2_xlen { get; set; }
            public float[] hor2_ylen { get; set; }
            public float[] hor3_xmin { get; set; }
            public float[] hor3_ymin { get; set; }
            public float[] hor3_xlen { get; set; }
            public float[] hor3_ylen { get; set; }
            public Color color { get; set; }

            //加个构造函数初始化

            public Person()
            {
                 top_xmin = new float[1800];
                 top_ymin = new float[1800];
                 top_xlen = new float[1800];
                 top_ylen = new float[1800];
                 hor1_xmin = new float[1800];
                 hor1_ymin = new float[1800];
                 hor1_xlen = new float[1800];
                 hor1_ylen = new float[1800];
                 hor2_xmin = new float[1800];
                 hor2_ymin = new float[1800];
                 hor2_xlen = new float[1800];
                 hor2_ylen = new float[1800];
                 hor3_xmin = new float[1800];
                 hor3_ymin = new float[1800];
                 hor3_xlen = new float[1800];
                 hor3_ylen = new float[1800];
                 color = new Color();
            }

        }

        Person[] person = new Person[10];
        int person_sum; // 总人数

        private void timer_Tick(object sender, EventArgs e)
        {
            int cur_person; // 循环变量
            int cnt_person; // 场景人数统计
            Mat image1 = new Mat(image_file_top[index], ImreadModes.AnyColor);
            buffimage[1] = image1.ToBitmap();
            // TopView
            Graphics g1 = Graphics.FromImage(buffimage[1]);
            cur_person = 0;
            cnt_person = 0;
            while(cur_person < person_sum)
            {
                if (person[cur_person].top_xmin[index] == 0.0f)
                {
                    ++cur_person;
                    continue;
                }
                ++cnt_person;
                g1.DrawRectangle(new Pen(person[cur_person].color, 10), person[cur_person].top_xmin[index], person[cur_person].top_ymin[index], person[cur_person].top_xlen[index], person[cur_person].top_xlen[index]);
                g1.DrawString(cur_person.ToString(), new Font("Arial", 80), new SolidBrush(person[cur_person].color), person[cur_person].top_xmin[index] - 80, person[cur_person].top_ymin[index] - 80);
                ++cur_person;
            }
            this.uiDataGridView1.Rows[0].Cells[1].Value = cnt_person.ToString();
            image1.Release();

            Mat image2 = null;
            Graphics g2 = null;
            buffimage[2] = null;
            if (mode >= 3)
            {
                image2 = new Mat(image_file_hor1[index], ImreadModes.AnyColor);
                buffimage[2] = image2.ToBitmap();
                g2 = Graphics.FromImage(buffimage[2]);
                cur_person = 0;
                cnt_person = 0;
                while (cur_person < person_sum)
                {
                    if (person[cur_person].hor1_xmin[index] == 0.0f)
                    {
                        ++cur_person;
                        continue;
                    }
                    ++cnt_person;
                    g2.DrawRectangle(new Pen(person[cur_person].color, 10), person[cur_person].hor1_xmin[index], person[cur_person].hor1_ymin[index], person[cur_person].hor1_xlen[index], person[cur_person].hor1_ylen[index]);
                    g2.DrawString(cur_person.ToString(), new Font("Arial", 80), new SolidBrush(person[cur_person].color), person[cur_person].hor1_xmin[index] - 80, person[cur_person].hor1_ymin[index] - 80);
                    ++cur_person;
                }
                this.uiDataGridView1.Rows[1].Cells[1].Value = cnt_person.ToString();
                image2.Release();
            }
            Mat image3 = null;
            Graphics g3 = null;
            buffimage[3] = null;
            if (mode == 4)
            {
                image3 = new Mat(image_file_hor2[index], ImreadModes.AnyColor);
                buffimage[3] = image3.ToBitmap();
                g3 = Graphics.FromImage(buffimage[3]);
                cur_person = 0;
                cnt_person = 0;
                while (cur_person < person_sum)
                {
                    if (person[cur_person].hor2_xmin[index] == 0.0f)
                    {
                        ++cur_person;
                        continue;
                    }
                    ++cnt_person;
                    g3.DrawRectangle(new Pen(person[cur_person].color, 10), person[cur_person].hor2_xmin[index], person[cur_person].hor2_ymin[index], person[cur_person].hor2_xlen[index], person[cur_person].hor2_ylen[index]);
                    g3.DrawString(cur_person.ToString(), new Font("Arial", 80), new SolidBrush(person[cur_person].color), person[cur_person].hor2_xmin[index] - 80, person[cur_person].hor2_ymin[index] - 80);
                    ++cur_person;
                }
                this.uiDataGridView1.Rows[2].Cells[1].Value = cnt_person.ToString();
                image3.Release();
            }

            Mat image4 = null;
            if (mode == 2)
            {
                image4 = new Mat(image_file_hor1[index], ImreadModes.AnyColor);
                
            }
            else if (mode == 3)
            {
                image4 = new Mat(image_file_hor2[index], ImreadModes.AnyColor);
                
            }
            else if (mode == 4)
            {
                image4 = new Mat(image_file_hor3[index], ImreadModes.AnyColor);
                
            }
            buffimage[4] = image4.ToBitmap();
            Graphics g4 = Graphics.FromImage(buffimage[4]);
            if(mode == 2)
            {
                cur_person = 0;
                cnt_person = 0;
                while (cur_person < person_sum)
                {
                    if (person[cur_person].hor1_xmin[index] == 0.0f)
                    {
                        ++cur_person;
                        continue;
                    }
                    ++cnt_person;
                    g4.DrawRectangle(new Pen(person[cur_person].color, 10), person[cur_person].hor1_xmin[index], person[cur_person].hor1_ymin[index], person[cur_person].hor1_xlen[index], person[cur_person].hor1_ylen[index]);
                    g4.DrawString(cur_person.ToString(), new Font("Arial", 80), new SolidBrush(person[cur_person].color), person[cur_person].hor1_xmin[index] - 80, person[cur_person].hor1_ymin[index] - 80);
                    ++cur_person;
                }
                this.uiDataGridView1.Rows[1].Cells[1].Value = cnt_person.ToString();
            }
            else if(mode == 3)
            {
                cur_person = 0;
                cnt_person = 0;
                while (cur_person < person_sum)
                {
                    if (person[cur_person].hor2_xmin[index] == 0.0f)
                    {
                        ++cur_person;
                        continue;
                    }
                    ++cnt_person;
                    g4.DrawRectangle(new Pen(person[cur_person].color, 10), person[cur_person].hor2_xmin[index], person[cur_person].hor2_ymin[index], person[cur_person].hor2_xlen[index], person[cur_person].hor2_ylen[index]);
                    g4.DrawString(cur_person.ToString(), new Font("Arial", 80), new SolidBrush(person[cur_person].color), person[cur_person].hor2_xmin[index] - 80, person[cur_person].hor2_ymin[index] - 80);
                    ++cur_person;
                }
                this.uiDataGridView1.Rows[2].Cells[1].Value = cnt_person.ToString();
            }
            else if(mode == 4)
            {
                cur_person = 0;
                cnt_person = 0;
                while (cur_person < person_sum)
                {
                    if (person[cur_person].hor3_xmin[index] == 0.0f)
                    {
                        ++cur_person;
                        continue;
                    }
                    ++cnt_person;
                    g4.DrawRectangle(new Pen(person[cur_person].color, 10), person[cur_person].hor3_xmin[index], person[cur_person].hor3_ymin[index], person[cur_person].hor3_xlen[index], person[cur_person].hor3_ylen[index]);
                    g4.DrawString(cur_person.ToString(), new Font("Arial", 80), new SolidBrush(person[cur_person].color), person[cur_person].hor3_xmin[index] - 80, person[cur_person].hor3_ymin[index] - 80);
                    ++cur_person;
                }
                this.uiDataGridView1.Rows[3].Cells[1].Value = cnt_person.ToString();
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
            string url = "http://kurox.cn:5715/get_file/test.jpg";
            WebRequest wRequest = WebRequest.Create(url);
            wRequest.Method = "GET";
            wRequest.ContentType = "text/html;charset=UTF-8";
            WebResponse wResponse = wRequest.GetResponse();
            Stream stream = wResponse.GetResponseStream();
            StreamReader reader = new StreamReader(stream, System.Text.Encoding.Default);
            string str = reader.ReadToEnd(); //url返回的值
            reader.Close();
            wResponse.Close();
            MessageBox.Show(str);
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

        private void Button2_Click_1(object sender, EventArgs e)
        {

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
            person_sum = 0;
            for(int i = 0; i < 10; i++)
            {
                for(int j = 0; j < 1500; j++)
                {
                    person[i].top_xmin[j] = person[i].top_ymin[j] = person[i].top_xlen[j] = person[i].top_ylen[j] = 0.0f;
                    person[i].hor1_xmin[j] = person[i].hor1_ymin[j] = person[i].hor1_xlen[j] = person[i].hor1_ylen[j] = 0.0f;
                    person[i].hor2_xmin[j] = person[i].hor2_ymin[j] = person[i].hor2_xlen[j] = person[i].hor2_ylen[j] = 0.0f;
                    person[i].hor3_xmin[j] = person[i].hor3_ymin[j] = person[i].hor3_xlen[j] = person[i].hor3_ylen[j] = 0.0f;
                }
            }
            mode = 4;
            X = this.Width;//获取窗体的宽度
            Y = this.Height;//获取窗体的高度
            if (folderdia.ShowDialog() == DialogResult.OK)
            {
                string text = folderdia.SelectedPath;
                foldername = text;
                int cur_line;
                int cur_frame;
                int cur_person;
                /* 重置表格 */
                this.uiDataGridView1.Rows.Clear();
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
                this.uiDataGridView1.Rows.Add(new System.String[] {
                "TopView",
                "",
                "" });
                lines = System.IO.File.ReadAllLines(pos_file_top[0]);
                cur_line = 0;
                cur_frame = 0;
                cur_person = 0;
                while(cur_line < lines.Length)
                {
                    string[] words = lines[cur_line].Split(',');
                    if (Int32.Parse(words[0]) < cur_frame)
                    {
                        cur_person++;
                    }
                    cur_frame = Int32.Parse(words[0]) - 1;
                    person[cur_person].top_xmin[cur_frame] = float.Parse(words[2]);
                    person[cur_person].top_ymin[cur_frame] = float.Parse(words[3]);
                    person[cur_person].top_xlen[cur_frame] = float.Parse(words[4]);
                    person[cur_person].top_ylen[cur_frame] = float.Parse(words[5]);
                    cur_line++;
                }
                cur_person++;
                if (cur_person > person_sum)
                {
                    person_sum = cur_person;
                }

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
                this.uiDataGridView1.Rows.Add(new System.String[] {
                "HorizontalView1",
                "",
                "" });
                lines = System.IO.File.ReadAllLines(pos_file_hor1[0]);
                cur_line = 0;
                cur_frame = 0;
                cur_person = 0;
                while (cur_line < lines.Length)
                {
                    string[] words = lines[cur_line].Split(',');
                    if (Int32.Parse(words[0]) < cur_frame)
                    {
                        cur_person++;
                    }
                    cur_frame = Int32.Parse(words[0]) - 1;
                    person[cur_person].hor1_xmin[cur_frame] = float.Parse(words[2]);
                    person[cur_person].hor1_ymin[cur_frame] = float.Parse(words[3]);
                    person[cur_person].hor1_xlen[cur_frame] = float.Parse(words[4]);
                    person[cur_person].hor1_ylen[cur_frame] = float.Parse(words[5]);
                    cur_line++;
                }
                cur_person++;
                if (cur_person > person_sum)
                {
                    person_sum = cur_person;
                }

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
                    pictureBox1.Width = Convert.ToInt32(268 * (X / 920));
                    pictureBox1.Height = Convert.ToInt32(177 * (Y / 551));
                    pictureBox4.Width = Convert.ToInt32(268 * (X / 920));
                    pictureBox4.Height = Convert.ToInt32(177 * (Y / 551));
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
                    this.uiDataGridView1.Rows.Add(new System.String[] {
                    "HorizontalView2",
                    "",
                    "" });
                    lines = System.IO.File.ReadAllLines(pos_file_hor2[0]);
                    cur_line = 0;
                    cur_frame = 0;
                    cur_person = 0;
                    while (cur_line < lines.Length)
                    {
                        string[] words = lines[cur_line].Split(',');
                        if (Int32.Parse(words[0]) < cur_frame)
                        {
                            cur_person++;
                        }
                        cur_frame = Int32.Parse(words[0]) - 1;
                        person[cur_person].hor2_xmin[cur_frame] = float.Parse(words[2]);
                        person[cur_person].hor2_ymin[cur_frame] = float.Parse(words[3]);
                        person[cur_person].hor2_xlen[cur_frame] = float.Parse(words[4]);
                        person[cur_person].hor2_ylen[cur_frame] = float.Parse(words[5]);
                        cur_line++;
                    }
                    cur_person++;
                    if (cur_person > person_sum)
                    {
                        person_sum = cur_person;
                    }
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
                        pictureBox1.Width = Convert.ToInt32(245 * (X / 920));
                        pictureBox1.Height = Convert.ToInt32(169 * (Y / 551));
                        pictureBox4.Width = Convert.ToInt32(245 * (X / 920));
                        pictureBox4.Height = Convert.ToInt32(169 * (Y / 551));
                        pictureBox1.Location = new System.Drawing.Point(146, 87);
                        label1.Location = new System.Drawing.Point(146, 87);
                        pictureBox4.Location = new System.Drawing.Point(270, 259);
                        label4.Location = new System.Drawing.Point(266, 259);
                        label4.Text = "HorizontalView2";
                    }
                    else
                    {
                        mode = 4;
                        pictureBox2.Visible = true;
                        label2.Visible = true;
                        pictureBox3.Visible = true;
                        label3.Visible = true;
                        pictureBox1.Width = Convert.ToInt32(245 * (X / 920));
                        pictureBox1.Height = Convert.ToInt32(169 * (Y / 551));
                        pictureBox4.Width = Convert.ToInt32(245 * (X / 920));
                        pictureBox4.Height = Convert.ToInt32(169 * (Y / 551));
                        pictureBox1.Location = new System.Drawing.Point(Convert.ToInt32(12 * (X / 920)), Convert.ToInt32(74 * (Y / 551)));
                        label1.Location = new System.Drawing.Point(Convert.ToInt32(12 * (X / 920)), Convert.ToInt32(74 * (Y / 551)));
                        pictureBox4.Location = new System.Drawing.Point(Convert.ToInt32(266 * (X / 920)), Convert.ToInt32(259 * (Y / 551)));
                        label4.Location = new System.Drawing.Point(Convert.ToInt32(266 * (X / 920)), Convert.ToInt32(259 * (Y / 551)));
                        label4.Text = "HorizontalView3";
                        image_file_hor3 = System.IO.Directory.GetFiles(f4[0], "*.jpg");
                        //pos4 = System.IO.Directory.GetDirectories(f4[0], "*xml");
                        pos_file_hor3 = System.IO.Directory.GetFiles(f4[0], "*.txt");
                        this.uiDataGridView1.Rows.Add(new System.String[] {
                        "HorizontalView3",
                        "",
                        "" });
                        lines = System.IO.File.ReadAllLines(pos_file_hor3[0]);
                        cur_line = 0;
                        cur_frame = 0;
                        cur_person = 0;
                        while (cur_line < lines.Length)
                        {
                            string[] words = lines[cur_line].Split(',');
                            if (Int32.Parse(words[0]) < cur_frame)
                            {
                                cur_person++;
                            }
                            cur_frame = Int32.Parse(words[0]) - 1;
                            person[cur_person].hor3_xmin[cur_frame] = float.Parse(words[2]);
                            person[cur_person].hor3_ymin[cur_frame] = float.Parse(words[3]);
                            person[cur_person].hor3_xlen[cur_frame] = float.Parse(words[4]);
                            person[cur_person].hor3_ylen[cur_frame] = float.Parse(words[5]);
                            cur_line++;
                        }
                        cur_person++;
                        if (cur_person > person_sum)
                        {
                            person_sum = cur_person;
                        }
                    }
                }
            }
            setTag(this);
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




