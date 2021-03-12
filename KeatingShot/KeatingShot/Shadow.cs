﻿using System;
using System.Drawing;
using System.Windows.Forms;

namespace KeatingShot
{
    public partial class frmShadow : Form
    {
        public frmShadow()
        {
            InitializeComponent();
            ShowInTaskbar = false;
            //WindowState = FormWindowState.Minimized;
            FormBorderStyle = FormBorderStyle.None;

            ControlResizer resizablePicture = new ControlResizer(this.focusArea);
        }

        public delegate void NewFocusAreaCreatedHandler(object sender);

        public event NewFocusAreaCreatedHandler OnNewFocusAreaCreated;

        private void frmShadow_Load(object sender, EventArgs e)
        {
            
        }

        private void frmShadow_MouseDown(object sender, MouseEventArgs e)
        {
            // Clean cropped rectangle
            var region = new Region(this.ClientRectangle);
            region.Exclude(new Rectangle(0,0,0,0));
            this.Region = region;

            this.focusArea.Visible = false;
            this.focusAreaLoading.Location = new Point(e.X, e.Y);
            this.focusAreaLoading.Size = new Size(1, 1);
            this.focusAreaLoading.Visible = true;
            OnNewFocusAreaCreated?.Invoke(this);
        }

        private void frmShadow_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                focusAreaLoading.Width = e.X - focusAreaLoading.Location.X;
                focusAreaLoading.Height = e.Y - focusAreaLoading.Location.Y;
            }
        }

        private void frmShadow_MouseUp(object sender, MouseEventArgs e)
        {
            this.focusArea.Location = this.focusAreaLoading.Location;
            this.focusArea.Size = this.focusAreaLoading.Size;
            this.focusArea.Visible = true;
            this.focusAreaLoading.Visible = false;

            // Create a hole in the rectangle area below
            var holeLocX = focusArea.Location.X + 3;
            var holeLocY = focusArea.Location.Y + 3;
            var holeSizeW = focusArea.Size.Width - 6;
            var holeSizeH = focusArea.Size.Height - 6;
            var region = new Region(this.ClientRectangle);
            region.Exclude(new Rectangle(holeLocX,holeLocY,holeSizeW,holeSizeH));
            this.Region = region;
        }

        public void HideFocusAreas()
        {
            focusArea.Visible = false;
            focusAreaLoading.Visible = false;
        }
    }
}
