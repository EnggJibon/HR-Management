namespace AttendanceTracker
{
    partial class Dashboard
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
            this.btnEntryTime = new System.Windows.Forms.Button();
            this.btnExitTime = new System.Windows.Forms.Button();
            this.Label1 = new System.Windows.Forms.Label();
            this.Label2 = new System.Windows.Forms.Label();
            this.Label3 = new System.Windows.Forms.Label();
            this.lblName = new System.Windows.Forms.Label();
            this.lblDepartment = new System.Windows.Forms.Label();
            this.lblDesignation = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // btnEntryTime
            // 
            this.btnEntryTime.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.btnEntryTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEntryTime.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.btnEntryTime.Location = new System.Drawing.Point(542, 184);
            this.btnEntryTime.Name = "btnEntryTime";
            this.btnEntryTime.Size = new System.Drawing.Size(206, 45);
            this.btnEntryTime.TabIndex = 0;
            this.btnEntryTime.Text = "Entry Time ";
            this.btnEntryTime.UseVisualStyleBackColor = false;
            this.btnEntryTime.Click += new System.EventHandler(this.btnEntryTime_Click);
            // 
            // btnExitTime
            // 
            this.btnExitTime.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.btnExitTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExitTime.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.btnExitTime.Location = new System.Drawing.Point(542, 133);
            this.btnExitTime.Name = "btnExitTime";
            this.btnExitTime.Size = new System.Drawing.Size(206, 45);
            this.btnExitTime.TabIndex = 1;
            this.btnExitTime.Text = "Exit Time ";
            this.btnExitTime.UseVisualStyleBackColor = false;
            this.btnExitTime.Click += new System.EventHandler(this.btnExitTime_Click);
            // 
            // Label1
            // 
            this.Label1.AutoSize = true;
            this.Label1.ForeColor = System.Drawing.Color.MintCream;
            this.Label1.Location = new System.Drawing.Point(44, 25);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(48, 16);
            this.Label1.TabIndex = 2;
            this.Label1.Text = "Name:";
            // 
            // Label2
            // 
            this.Label2.AutoSize = true;
            this.Label2.ForeColor = System.Drawing.Color.MintCream;
            this.Label2.Location = new System.Drawing.Point(11, 52);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(81, 16);
            this.Label2.TabIndex = 3;
            this.Label2.Text = "Department:";
            // 
            // Label3
            // 
            this.Label3.AutoSize = true;
            this.Label3.ForeColor = System.Drawing.Color.MintCream;
            this.Label3.Location = new System.Drawing.Point(9, 79);
            this.Label3.Name = "Label3";
            this.Label3.Size = new System.Drawing.Size(83, 16);
            this.Label3.TabIndex = 4;
            this.Label3.Text = "Designation:";
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.ForeColor = System.Drawing.Color.Purple;
            this.lblName.Location = new System.Drawing.Point(95, 25);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(18, 16);
            this.lblName.TabIndex = 5;
            this.lblName.Text = "D";
            // 
            // lblDepartment
            // 
            this.lblDepartment.AutoSize = true;
            this.lblDepartment.ForeColor = System.Drawing.Color.Purple;
            this.lblDepartment.Location = new System.Drawing.Point(95, 52);
            this.lblDepartment.Name = "lblDepartment";
            this.lblDepartment.Size = new System.Drawing.Size(18, 16);
            this.lblDepartment.TabIndex = 6;
            this.lblDepartment.Text = "D";
            // 
            // lblDesignation
            // 
            this.lblDesignation.AutoSize = true;
            this.lblDesignation.ForeColor = System.Drawing.Color.Purple;
            this.lblDesignation.Location = new System.Drawing.Point(95, 79);
            this.lblDesignation.Name = "lblDesignation";
            this.lblDesignation.Size = new System.Drawing.Size(18, 16);
            this.lblDesignation.TabIndex = 7;
            this.lblDesignation.Text = "D";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.Label1);
            this.groupBox1.Controls.Add(this.lblDesignation);
            this.groupBox1.Controls.Add(this.Label2);
            this.groupBox1.Controls.Add(this.lblDepartment);
            this.groupBox1.Controls.Add(this.Label3);
            this.groupBox1.Controls.Add(this.lblName);
            this.groupBox1.Location = new System.Drawing.Point(414, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(334, 107);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Logged in as";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::AttendanceTracker.Properties.Resources.images_iems_parent_icon_250x250;
            this.pictureBox1.Location = new System.Drawing.Point(12, 2);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(383, 227);
            this.pictureBox1.TabIndex = 9;
            this.pictureBox1.TabStop = false;
            // 
            // Dashboard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.Teal;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.ClientSize = new System.Drawing.Size(760, 238);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnExitTime);
            this.Controls.Add(this.btnEntryTime);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.Teal;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Dashboard";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Dashboard";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnEntryTime;
        private System.Windows.Forms.Button btnExitTime;
        private System.Windows.Forms.Label Label1;
        private System.Windows.Forms.Label Label2;
        private System.Windows.Forms.Label Label3;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Label lblDepartment;
        private System.Windows.Forms.Label lblDesignation;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.PictureBox pictureBox1;


    }
}