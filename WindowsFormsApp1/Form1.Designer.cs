namespace WindowsFormsApp1
{
    partial class Form1
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
            this.button1 = new System.Windows.Forms.Button();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.YourNameLabel = new System.Windows.Forms.Label();
            this.HelpLabel = new System.Windows.Forms.Label();
            this.HelpTextBox = new System.Windows.Forms.TextBox();
            this.DrewHelpLabel = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(510, 235);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Akit húztál:";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(355, 397);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "Húz";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // comboBox1
            // 
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(145, 227);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(121, 21);
            this.comboBox1.TabIndex = 2;
            // 
            // YourNameLabel
            // 
            this.YourNameLabel.AutoSize = true;
            this.YourNameLabel.Location = new System.Drawing.Point(77, 235);
            this.YourNameLabel.Name = "YourNameLabel";
            this.YourNameLabel.Size = new System.Drawing.Size(62, 13);
            this.YourNameLabel.TabIndex = 3;
            this.YourNameLabel.Text = "A te neved:";
            // 
            // HelpLabel
            // 
            this.HelpLabel.AutoSize = true;
            this.HelpLabel.Location = new System.Drawing.Point(12, 322);
            this.HelpLabel.Name = "HelpLabel";
            this.HelpLabel.Size = new System.Drawing.Size(286, 13);
            this.HelpLabel.TabIndex = 4;
            this.HelpLabel.Text = "Ide írj be néhány kategóriát amiből örülnél egy ajándéknak:";
            // 
            // HelpTextBox
            // 
            this.HelpTextBox.Location = new System.Drawing.Point(304, 319);
            this.HelpTextBox.Name = "HelpTextBox";
            this.HelpTextBox.Size = new System.Drawing.Size(100, 20);
            this.HelpTextBox.TabIndex = 5;
            // 
            // DrewHelpLabel
            // 
            this.DrewHelpLabel.AutoSize = true;
            this.DrewHelpLabel.Location = new System.Drawing.Point(513, 318);
            this.DrewHelpLabel.Name = "DrewHelpLabel";
            this.DrewHelpLabel.Size = new System.Drawing.Size(0, 13);
            this.DrewHelpLabel.TabIndex = 6;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(575, 157);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(249, 201);
            this.pictureBox1.TabIndex = 7;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Visible = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(836, 592);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.DrewHelpLabel);
            this.Controls.Add(this.HelpTextBox);
            this.Controls.Add(this.HelpLabel);
            this.Controls.Add(this.YourNameLabel);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label YourNameLabel;
        private System.Windows.Forms.Label HelpLabel;
        private System.Windows.Forms.TextBox HelpTextBox;
        private System.Windows.Forms.Label DrewHelpLabel;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}

