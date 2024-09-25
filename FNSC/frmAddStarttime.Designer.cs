namespace FNSC{
    partial class frmAddStarttime
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
            numStarttime = new System.Windows.Forms.NumericUpDown();
            btnSaveStarttime = new System.Windows.Forms.Button();
            label1 = new System.Windows.Forms.Label();
            txtCode = new System.Windows.Forms.TextBox();
            label2 = new System.Windows.Forms.Label();
            btnCancel = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)numStarttime).BeginInit();
            SuspendLayout();
            // 
            // numStarttime
            // 
            numStarttime.Location = new System.Drawing.Point(86, 35);
            numStarttime.Name = "numStarttime";
            numStarttime.Size = new System.Drawing.Size(120, 23);
            numStarttime.TabIndex = 0;
            numStarttime.KeyDown += numStarttime_KeyDown;
            // 
            // btnSaveStarttime
            // 
            btnSaveStarttime.Location = new System.Drawing.Point(12, 64);
            btnSaveStarttime.Name = "btnSaveStarttime";
            btnSaveStarttime.Size = new System.Drawing.Size(75, 23);
            btnSaveStarttime.TabIndex = 1;
            btnSaveStarttime.Text = "Save";
            btnSaveStarttime.UseVisualStyleBackColor = true;
            btnSaveStarttime.Click += btnSaveStarttime_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(12, 37);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(55, 15);
            label1.TabIndex = 2;
            label1.Text = "Starttime";
            // 
            // txtCode
            // 
            txtCode.Location = new System.Drawing.Point(86, 6);
            txtCode.Name = "txtCode";
            txtCode.Size = new System.Drawing.Size(120, 23);
            txtCode.TabIndex = 3;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new System.Drawing.Point(12, 9);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(35, 15);
            label2.TabIndex = 4;
            label2.Text = "Code";
            // 
            // btnCancel
            // 
            btnCancel.Location = new System.Drawing.Point(130, 64);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new System.Drawing.Size(75, 23);
            btnCancel.TabIndex = 5;
            btnCancel.Text = "Cancel";
            btnCancel.UseVisualStyleBackColor = true;
            btnCancel.Click += btnCancel_Click;
            // 
            // frmAddStarttime
            // 
            AcceptButton = btnSaveStarttime;
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            CancelButton = btnCancel;
            ClientSize = new System.Drawing.Size(217, 95);
            Controls.Add(btnCancel);
            Controls.Add(label2);
            Controls.Add(txtCode);
            Controls.Add(label1);
            Controls.Add(btnSaveStarttime);
            Controls.Add(numStarttime);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            Name = "frmAddStarttime";
            Text = "Modify starttime";
            ((System.ComponentModel.ISupportInitialize)numStarttime).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.NumericUpDown numStarttime;
        private System.Windows.Forms.Button btnSaveStarttime;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtCode;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnCancel;
    }
}