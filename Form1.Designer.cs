namespace CS_Example
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
            this.DestPosBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.CmdPosBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.EventLabel = new System.Windows.Forms.Label();
            this.Move = new System.Windows.Forms.Button();
            this.EventInt = new System.Windows.Forms.CheckBox();
            this.VelocityBox = new System.Windows.Forms.TextBox();
            this.AccelBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.Stopbutton = new System.Windows.Forms.Button();
            this.Disablebutton = new System.Windows.Forms.Button();
            this.ActPosBox = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.ExitButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // DestPosBox
            // 
            this.DestPosBox.Location = new System.Drawing.Point(299, 26);
            this.DestPosBox.Name = "DestPosBox";
            this.DestPosBox.Size = new System.Drawing.Size(100, 20);
            this.DestPosBox.TabIndex = 0;
            this.DestPosBox.Text = "0";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.SystemColors.ControlLight;
            this.label1.Location = new System.Drawing.Point(217, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Destination";
            // 
            // CmdPosBox
            // 
            this.CmdPosBox.ForeColor = System.Drawing.SystemColors.WindowText;
            this.CmdPosBox.Location = new System.Drawing.Point(299, 183);
            this.CmdPosBox.Name = "CmdPosBox";
            this.CmdPosBox.ReadOnly = true;
            this.CmdPosBox.Size = new System.Drawing.Size(100, 20);
            this.CmdPosBox.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(187, 186);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(106, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Commanded Position";
            // 
            // EventLabel
            // 
            this.EventLabel.AutoSize = true;
            this.EventLabel.Location = new System.Drawing.Point(187, 248);
            this.EventLabel.Name = "EventLabel";
            this.EventLabel.Size = new System.Drawing.Size(35, 13);
            this.EventLabel.TabIndex = 4;
            this.EventLabel.Text = "label3";
            // 
            // Move
            // 
            this.Move.Location = new System.Drawing.Point(12, 26);
            this.Move.Name = "Move";
            this.Move.Size = new System.Drawing.Size(141, 34);
            this.Move.TabIndex = 5;
            this.Move.Text = "Do Move";
            this.Move.UseVisualStyleBackColor = true;
            this.Move.Click += new System.EventHandler(this.Move_Click);
            // 
            // EventInt
            // 
            this.EventInt.AutoSize = true;
            this.EventInt.Location = new System.Drawing.Point(12, 72);
            this.EventInt.Name = "EventInt";
            this.EventInt.Size = new System.Drawing.Size(169, 17);
            this.EventInt.TabIndex = 6;
            this.EventInt.Text = "Use Magellan Event Interrupts";
            this.EventInt.UseVisualStyleBackColor = true;
            this.EventInt.CheckedChanged += new System.EventHandler(this.EventInt_CheckedChanged);
            // 
            // VelocityBox
            // 
            this.VelocityBox.Location = new System.Drawing.Point(299, 52);
            this.VelocityBox.Name = "VelocityBox";
            this.VelocityBox.Size = new System.Drawing.Size(100, 20);
            this.VelocityBox.TabIndex = 7;
            this.VelocityBox.Text = "0";
            // 
            // AccelBox
            // 
            this.AccelBox.Location = new System.Drawing.Point(299, 78);
            this.AccelBox.Name = "AccelBox";
            this.AccelBox.Size = new System.Drawing.Size(100, 20);
            this.AccelBox.TabIndex = 8;
            this.AccelBox.Text = "0";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(217, 55);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(44, 13);
            this.label3.TabIndex = 9;
            this.label3.Text = "Velocity";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(217, 81);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(66, 13);
            this.label4.TabIndex = 10;
            this.label4.Text = "Acceleration";
            // 
            // Stopbutton
            // 
            this.Stopbutton.Location = new System.Drawing.Point(12, 176);
            this.Stopbutton.Name = "Stopbutton";
            this.Stopbutton.Size = new System.Drawing.Size(125, 23);
            this.Stopbutton.TabIndex = 11;
            this.Stopbutton.Text = "Stop";
            this.Stopbutton.UseVisualStyleBackColor = true;
            this.Stopbutton.Click += new System.EventHandler(this.Stopbutton_Click_1);
            // 
            // Disablebutton
            // 
            this.Disablebutton.Location = new System.Drawing.Point(12, 214);
            this.Disablebutton.Name = "Disablebutton";
            this.Disablebutton.Size = new System.Drawing.Size(125, 23);
            this.Disablebutton.TabIndex = 12;
            this.Disablebutton.Text = "Disable Drive";
            this.Disablebutton.UseVisualStyleBackColor = true;
            this.Disablebutton.Click += new System.EventHandler(this.Disablebutton_Click);
            // 
            // ActPosBox
            // 
            this.ActPosBox.Location = new System.Drawing.Point(299, 214);
            this.ActPosBox.Name = "ActPosBox";
            this.ActPosBox.ReadOnly = true;
            this.ActPosBox.Size = new System.Drawing.Size(100, 20);
            this.ActPosBox.TabIndex = 13;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(187, 214);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(77, 13);
            this.label5.TabIndex = 14;
            this.label5.Text = "Actual Position";
            // 
            // ExitButton
            // 
            this.ExitButton.Location = new System.Drawing.Point(320, 288);
            this.ExitButton.Name = "ExitButton";
            this.ExitButton.Size = new System.Drawing.Size(89, 23);
            this.ExitButton.TabIndex = 15;
            this.ExitButton.Text = "Exit";
            this.ExitButton.UseVisualStyleBackColor = true;
            this.ExitButton.Click += new System.EventHandler(this.ExitButton_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(437, 323);
            this.Controls.Add(this.ExitButton);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.ActPosBox);
            this.Controls.Add(this.Disablebutton);
            this.Controls.Add(this.Stopbutton);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.AccelBox);
            this.Controls.Add(this.VelocityBox);
            this.Controls.Add(this.EventInt);
            this.Controls.Add(this.Move);
            this.Controls.Add(this.EventLabel);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.CmdPosBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.DestPosBox);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox DestPosBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label EventLabel;
        private System.Windows.Forms.Button Move;
        private System.Windows.Forms.CheckBox EventInt;
        private System.Windows.Forms.TextBox VelocityBox;
        private System.Windows.Forms.TextBox AccelBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button Stopbutton;
        private System.Windows.Forms.Button Disablebutton;
        private System.Windows.Forms.TextBox ActPosBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button ExitButton;
        private System.Windows.Forms.TextBox CmdPosBox;
    }
}

