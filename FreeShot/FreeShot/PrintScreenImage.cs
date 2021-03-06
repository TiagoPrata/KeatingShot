﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace FreeShot
{
    public partial class frmPrintedImages : Form
    {
        public frmPrintedImages()
        {
            InitializeComponent();
            this.DoubleBuffered = true;
        }

        private void PrintScreenImage_SizeChanged(object sender, EventArgs e)
        {
            pictureBox1.Size = new Size(Size.Width,Size.Height);
        }

        public void SetNewImage(Bitmap NewImage)
        {
            pictureBox1.Image = NewImage;
        }
    }
}
