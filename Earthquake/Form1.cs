using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.IO;
using System.Management;

namespace Earthquake
{
    
    public partial class Form1 : Form
    {
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int x, int y, int cx, int cy, UInt32 uflags);
        public static readonly UInt32 SWP_FRAMECHANGED = 32;
        public static readonly IntPtr HWND_TOPMOST = new IntPtr(-1);
        public Form1()
        {
            InitializeComponent();
            this.Width = 1920;
            this.Height = 1080;
        }
        Bitmap shan;
        Bitmap ball;
        int t=0;
        int g=0;
        int ballg = 0;
        int gx, gy;
        int[] x1 = new int[12]{300, 615, 790, 1079, 946, 510, 709, 735, 790, 1115, 1006, 765};
        int[] y1 = new int[12] { 338, 443, 545, 458, 545, 784, 709, 768, 689, 888, 898 , 769};
        public static string Current = System.Environment.CurrentDirectory;
        string balln;
        string CpuID, DisID;//获取的硬件ID
        DateTime DeadLine;//软件使用期限
        string time = "2013/7/7 20:46:28";//标志
        string time1 = "2013/9/15 22:46:28";//Deadline
        private void Form1_Load(object sender, EventArgs e)
        {
            string name = Current + "//earth/dizhen.jpg";
            Image bg = Image.FromFile(name);
            this.BackgroundImage = bg;
            shan = Properties.Resources.小;
            timer1.Start();
            balln = Current + "//earth/Halo/1.png";
            DeadLine = DateTime.Parse(time1);
            //CpuID = GetHardwareInfo("Win32_Processor", "ProcessorId");
            //DisID = GetHardwareInfo("Win32_PhysicalMedia", "SerialNumber");
            //DisID = GetHardwareInfo();
            timer2.Start();
            //CheckID();
        }
        //获取硬件信息
        public static string GetHardwareInfo()
        {
            ManagementClass searcher = new ManagementClass("WIN32_PhysicalMedia");
            ManagementObjectCollection moc = searcher.GetInstances();
            //硬盘序列号
            string strHardDiskID = "";
            foreach (ManagementObject mo in moc)
            {
                try
                {
                    strHardDiskID += mo["SerialNumber"].ToString().Trim();
                    break;
                }
                catch
                {
                    //有可能是u盘之类的东西
                    MessageBox.Show("未能获取设备ID");
                }
            }
            return strHardDiskID;
        }
        //判断硬件编号与预存编号是否匹配
        public void CheckID()
        {
            int CheckID = 0;
            /*string[] Cpus = new string[10];
            string[] Diss = new string[10];
            for(int i=0;i<6;i++)
            {
                Cpus[i] = ConfigurationManager.AppSettings["CPUID"+i.ToString()];
                Diss[i] = ConfigurationManager.AppSettings["DISID"+i.ToString()];
                if(Cpus[i]==CpuID&&Diss[i]==DisID)
                {
                    CheckID = 1;
                    break;
                }
            }*/
            //遍历所有保存的ID是否与现有硬件ID匹配
            //string Cpus = "BFEBFBFF000206A6";
            string Diss = "2020202057202d444d5759414855333734393333";
            if (Diss == DisID)
            {
                CheckID = 1;
            }
            if (CheckID == 0)
            {
                MessageBox.Show("对不起，您还没有获得使用权限！");
                this.Close();
            }
        }
        //检查软件使用时间是否到期
        public void CheTime()
        {
            StreamReader sr = new StreamReader("Time.dll", true);
            string s = (sr.ReadLine()).Remove(0, 1);
            System.Text.RegularExpressions.CaptureCollection cs =
            System.Text.RegularExpressions.Regex.Match(s, @"([01]{8})+").Groups[1].Captures;
            byte[] datar = new byte[cs.Count];
            for (int i = 0; i < cs.Count; i++)
            {
                datar[i] = Convert.ToByte(cs[i].Value, 2);
            }
            s = Encoding.Unicode.GetString(datar, 0, datar.Length);
            sr.Close();
            if (s == time)
            {
                if (DateTime.Now < DeadLine)
                {
                    /* byte[] data = Encoding.Unicode.GetBytes(DateTime.Now.ToString());
                     StringBuilder result = new StringBuilder(data.Length * 8);

                     foreach (byte b in data)
                     {
                         result.Append(Convert.ToString(b, 2).PadLeft(8, '0'));
                     }
                     BinaryWriter bw = new BinaryWriter(File.Open("Time.dll", FileMode.OpenOrCreate));
                     bw.Write(result.ToString());
                     bw.Close();*/
                }
                else
                {
                    byte[] data = Encoding.Unicode.GetBytes(DeadLine.ToString());
                    StringBuilder result = new StringBuilder(data.Length * 8);

                    foreach (byte b in data)
                    {
                        result.Append(Convert.ToString(b, 2).PadLeft(8, '0'));
                    }
                    BinaryWriter bw = new BinaryWriter(File.Open("Time.dll", FileMode.OpenOrCreate));
                    bw.Write(result.ToString());
                    bw.Close();
                    timer2.Stop();
                    MessageBox.Show("对不起，您的软件已过期！");
                    this.Close();
                }
            }
            else
            {
                timer2.Stop();
                MessageBox.Show("对不起，您的软件已过期！");
                this.Close();
            }
        }
        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            if (t == 1)
            {
                e.Graphics.DrawImage(shan, x1[0], y1[0], 58,58);
                
                e.Graphics.DrawImage(shan, x1[2], y1[2], 58, 58);
                
                e.Graphics.DrawImage(shan, x1[4], y1[4], 58, 58);
                
                e.Graphics.DrawImage(shan, x1[6], y1[6], 58, 58);
                
                e.Graphics.DrawImage(shan, x1[8], y1[8], 58, 58);
                
                e.Graphics.DrawImage(shan, x1[10], y1[10], 58, 58);
                
            }
            else
            {
                e.Graphics.DrawImage(shan, x1[1], y1[1], 58, 58);
                e.Graphics.DrawImage(shan, x1[3], y1[3], 58, 58);
                e.Graphics.DrawImage(shan, x1[5], y1[5], 58, 58);
                e.Graphics.DrawImage(shan, x1[7], y1[7], 58, 58);
                e.Graphics.DrawImage(shan, x1[9], y1[9], 58, 58);
                e.Graphics.DrawImage(shan, x1[11], y1[11], 58, 58);
            }
            if (ballg == 1)
            {
                e.Graphics.DrawImage(ball, gx,gy, 483, 478);
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (t == 1)
            { t = 0; }
            else{t=1;}
            this.Invalidate();
        }
        public static int j;
        Form2 fr2 = null;
        int i = 1;
        private void timer2_Tick(object sender, EventArgs e)
        {
            CheTime();
            string balln = Current + "//earth/Halo/"+i+".png";
            ball = new Bitmap(balln);
            this.Invalidate();
            i++;
            if (i == 7)
            { i = 1; }
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            for (j = 0; j <= 11; j++)
            {
                if (e.X >= x1[j] && e.X <= x1[j] + 58 && e.Y <= y1[j] + 58 && e.Y >= y1[j])
                {
                    if (fr2 == null)
                    {
                        fr2 = new Form2();
                    }
                    else
                    {
                        fr2.Close();
                        fr2 = new Form2();
                    }
                    SetWindowPos(fr2.Handle, HWND_TOPMOST, x1[j] + 29 - 293, y1[j] + 29 - 345, fr2.Width, fr2.Height, SWP_FRAMECHANGED);
                    fr2.Show();
                    ball = new Bitmap(balln);
                    i = 2;
                    if (timer2.Enabled == true)
                    {
                        ballg = 0;
                        timer2.Stop();
                    }
                    gx = x1[j] + 29 - 241;
                    gy = y1[j] + 29 - 239;
                    ballg = 1;
                    timer2.Start();
                    g = 1;
                    break;
                }
                else
                {
                    g = 0;

                }
            }

            if (g == 0)
            {
                if (fr2 != null)
                {
                    fr2.Close();
                }
                ballg = 0;
                timer2.Stop();
            }
        }

    }
}
