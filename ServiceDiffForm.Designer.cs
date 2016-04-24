namespace dvbseserviceview
{
    partial class ServiceDiffForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxFile1 = new System.Windows.Forms.TextBox();
            this.textBoxFile2 = new System.Windows.Forms.TextBox();
            this.buttonBrowse1 = new System.Windows.Forms.Button();
            this.buttonBrowse2 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(27, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(39, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "File #1";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(30, 67);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(39, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "File #2";
            // 
            // textBoxFile1
            // 
            this.textBoxFile1.Location = new System.Drawing.Point(90, 29);
            this.textBoxFile1.Name = "textBoxFile1";
            this.textBoxFile1.Size = new System.Drawing.Size(239, 20);
            this.textBoxFile1.TabIndex = 2;
            // 
            // textBoxFile2
            // 
            this.textBoxFile2.Location = new System.Drawing.Point(90, 67);
            this.textBoxFile2.Name = "textBoxFile2";
            this.textBoxFile2.Size = new System.Drawing.Size(239, 20);
            this.textBoxFile2.TabIndex = 3;
            // 
            // buttonBrowse1
            // 
            this.buttonBrowse1.Location = new System.Drawing.Point(383, 29);
            this.buttonBrowse1.Name = "buttonBrowse1";
            this.buttonBrowse1.Size = new System.Drawing.Size(75, 23);
            this.buttonBrowse1.TabIndex = 4;
            this.buttonBrowse1.Text = "Browse...";
            this.buttonBrowse1.UseVisualStyleBackColor = true;
            // 
            // buttonBrowse2
            // 
            this.buttonBrowse2.Location = new System.Drawing.Point(383, 67);
            this.buttonBrowse2.Name = "buttonBrowse2";
            this.buttonBrowse2.Size = new System.Drawing.Size(75, 23);
            this.buttonBrowse2.TabIndex = 5;
            this.buttonBrowse2.Text = "Browse...";
            this.buttonBrowse2.UseVisualStyleBackColor = true;
            // 
            // ServiceDiffForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(550, 196);
            this.Controls.Add(this.buttonBrowse2);
            this.Controls.Add(this.buttonBrowse1);
            this.Controls.Add(this.textBoxFile2);
            this.Controls.Add(this.textBoxFile1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "ServiceDiffForm";
            this.Text = "ServiceDiffForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxFile1;
        private System.Windows.Forms.TextBox textBoxFile2;
        private System.Windows.Forms.Button buttonBrowse1;
        private System.Windows.Forms.Button buttonBrowse2;
    }
}