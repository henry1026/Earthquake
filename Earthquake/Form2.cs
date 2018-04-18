using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Earthquake
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
            this.TransparencyKey = BackColor;
            this.Width = 445;
            this.Height = 364;
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            //this.BackgroundImage = Properties.Resources.info;
            string name=Form1.Current+"//earth/Num/"+(Form1.j+1)+".png";
            Image bg=Image.FromFile(name);
            this.BackgroundImage = bg;
        }
    }
}
