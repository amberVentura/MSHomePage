using System.Windows.Forms;

namespace NewUIDesignOfMiniSystem
{
    partial class MainPage
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainPage));
            this.panel1 = new System.Windows.Forms.Panel();
            this.buttonBillings = new System.Windows.Forms.Button();
            this.buttonRecords = new System.Windows.Forms.Button();
            this.buttonAppointment = new System.Windows.Forms.Button();
            this.buttonHomepage = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.panel1.Controls.Add(this.label8);
            this.panel1.Controls.Add(this.label17);
            this.panel1.Controls.Add(this.buttonBillings);
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Controls.Add(this.buttonRecords);
            this.panel1.Controls.Add(this.buttonAppointment);
            this.panel1.Controls.Add(this.buttonHomepage);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1010, 113);
            this.panel1.TabIndex = 38;
            // 
            // buttonBillings
            // 
            this.buttonBillings.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.buttonBillings.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.buttonBillings.FlatAppearance.BorderSize = 0;
            this.buttonBillings.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonBillings.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonBillings.ForeColor = System.Drawing.SystemColors.ControlText;
            this.buttonBillings.Image = ((System.Drawing.Image)(resources.GetObject("buttonBillings.Image")));
            this.buttonBillings.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonBillings.Location = new System.Drawing.Point(612, 45);
            this.buttonBillings.Name = "buttonBillings";
            this.buttonBillings.Size = new System.Drawing.Size(194, 68);
            this.buttonBillings.TabIndex = 3;
            this.buttonBillings.Text = "Billings";
            this.buttonBillings.UseVisualStyleBackColor = false;
            this.buttonBillings.Click += new System.EventHandler(this.buttonBillings_Click);
            // 
            // buttonRecords
            // 
            this.buttonRecords.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.buttonRecords.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.buttonRecords.FlatAppearance.BorderSize = 0;
            this.buttonRecords.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonRecords.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonRecords.ForeColor = System.Drawing.SystemColors.ControlText;
            this.buttonRecords.Image = ((System.Drawing.Image)(resources.GetObject("buttonRecords.Image")));
            this.buttonRecords.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonRecords.Location = new System.Drawing.Point(412, 45);
            this.buttonRecords.Name = "buttonRecords";
            this.buttonRecords.Size = new System.Drawing.Size(194, 68);
            this.buttonRecords.TabIndex = 2;
            this.buttonRecords.Text = "Records";
            this.buttonRecords.UseVisualStyleBackColor = false;
            this.buttonRecords.Click += new System.EventHandler(this.buttonRecords_Click);
            // 
            // buttonAppointment
            // 
            this.buttonAppointment.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.buttonAppointment.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.buttonAppointment.FlatAppearance.BorderSize = 0;
            this.buttonAppointment.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonAppointment.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonAppointment.ForeColor = System.Drawing.SystemColors.ControlText;
            this.buttonAppointment.Image = ((System.Drawing.Image)(resources.GetObject("buttonAppointment.Image")));
            this.buttonAppointment.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonAppointment.Location = new System.Drawing.Point(212, 45);
            this.buttonAppointment.Name = "buttonAppointment";
            this.buttonAppointment.Size = new System.Drawing.Size(194, 68);
            this.buttonAppointment.TabIndex = 1;
            this.buttonAppointment.Text = "Appointment";
            this.buttonAppointment.UseVisualStyleBackColor = false;
            this.buttonAppointment.Click += new System.EventHandler(this.buttonAppointment_Click);
            // 
            // buttonHomepage
            // 
            this.buttonHomepage.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.buttonHomepage.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.buttonHomepage.FlatAppearance.BorderSize = 0;
            this.buttonHomepage.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonHomepage.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonHomepage.ForeColor = System.Drawing.SystemColors.ControlText;
            this.buttonHomepage.Image = ((System.Drawing.Image)(resources.GetObject("buttonHomepage.Image")));
            this.buttonHomepage.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonHomepage.Location = new System.Drawing.Point(12, 45);
            this.buttonHomepage.Name = "buttonHomepage";
            this.buttonHomepage.Size = new System.Drawing.Size(194, 68);
            this.buttonHomepage.TabIndex = 0;
            this.buttonHomepage.Text = "HomePage";
            this.buttonHomepage.UseVisualStyleBackColor = false;
            this.buttonHomepage.Click += new System.EventHandler(this.buttonHomepage_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.label8.Font = new System.Drawing.Font("Arial Rounded MT Bold", 18F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.ForestGreen;
            this.label8.Location = new System.Drawing.Point(46, 9);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(66, 28);
            this.label8.TabIndex = 41;
            this.label8.Text = "Dent";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.label17.Font = new System.Drawing.Font("Arial Rounded MT Bold", 18F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label17.ForeColor = System.Drawing.Color.Aqua;
            this.label17.Location = new System.Drawing.Point(102, 9);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(53, 28);
            this.label17.TabIndex = 40;
            this.label17.Text = "Pro";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(4, 6);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(36, 31);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 39;
            this.pictureBox1.TabStop = false;
            // 
            // MainPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1010, 719);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MainPage";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button buttonHomepage;
        private Button buttonAppointment;
        private Button buttonBillings;
        private Button buttonRecords;
        private Label label8;
        private Label label17;
        private PictureBox pictureBox1;
    }
}