namespace TalAssist
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            button1 = new Button();
            label1 = new Label();
            comboBox1 = new ComboBox();
            timeBox1 = new TextBox();
            repeatCheckBox = new CheckBox();
            timeBox2 = new TextBox();
            timeBox3 = new TextBox();
            repeatBox1 = new TextBox();
            repeatBox2 = new TextBox();
            repeatBox3 = new TextBox();
            keyComboBox1 = new ComboBox();
            keyComboBox2 = new ComboBox();
            keyComboBox3 = new ComboBox();
            recordButton = new Button();
            btnPlayMacro = new Button();
            SuspendLayout();
            // 
            // button1
            // 
            button1.Location = new Point(638, 392);
            button1.Name = "button1";
            button1.Size = new Size(150, 46);
            button1.TabIndex = 0;
            button1.Text = "Start";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click_1;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(25, 76);
            label1.Name = "label1";
            label1.Size = new Size(103, 32);
            label1.TabIndex = 1;
            label1.Text = "Stopped";
            // 
            // comboBox1
            // 
            comboBox1.FormattingEnabled = true;
            comboBox1.Location = new Point(25, 12);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(242, 40);
            comboBox1.TabIndex = 3;
            comboBox1.Text = "Select Window";
            // 
            // timeBox1
            // 
            timeBox1.Location = new Point(306, 121);
            timeBox1.Name = "timeBox1";
            timeBox1.PlaceholderText = "Time";
            timeBox1.Size = new Size(62, 39);
            timeBox1.TabIndex = 4;
            // 
            // repeatCheckBox
            // 
            repeatCheckBox.AutoSize = true;
            repeatCheckBox.Location = new Point(390, 392);
            repeatCheckBox.Name = "repeatCheckBox";
            repeatCheckBox.Size = new Size(231, 36);
            repeatCheckBox.TabIndex = 5;
            repeatCheckBox.Text = "Repeat Sequence";
            repeatCheckBox.UseVisualStyleBackColor = true;
            // 
            // timeBox2
            // 
            timeBox2.Location = new Point(306, 177);
            timeBox2.Name = "timeBox2";
            timeBox2.PlaceholderText = "Time";
            timeBox2.Size = new Size(62, 39);
            timeBox2.TabIndex = 7;
            // 
            // timeBox3
            // 
            timeBox3.Location = new Point(306, 232);
            timeBox3.Name = "timeBox3";
            timeBox3.PlaceholderText = "Time";
            timeBox3.Size = new Size(62, 39);
            timeBox3.TabIndex = 9;
            // 
            // repeatBox1
            // 
            repeatBox1.Location = new Point(374, 121);
            repeatBox1.Name = "repeatBox1";
            repeatBox1.PlaceholderText = "Repeat amount";
            repeatBox1.Size = new Size(209, 39);
            repeatBox1.TabIndex = 10;
            // 
            // repeatBox2
            // 
            repeatBox2.Location = new Point(374, 177);
            repeatBox2.Name = "repeatBox2";
            repeatBox2.PlaceholderText = "Repeat amount";
            repeatBox2.Size = new Size(209, 39);
            repeatBox2.TabIndex = 11;
            // 
            // repeatBox3
            // 
            repeatBox3.Location = new Point(374, 232);
            repeatBox3.Name = "repeatBox3";
            repeatBox3.PlaceholderText = "Repeat amount";
            repeatBox3.Size = new Size(209, 39);
            repeatBox3.TabIndex = 12;
            // 
            // keyComboBox1
            // 
            keyComboBox1.FormattingEnabled = true;
            keyComboBox1.Location = new Point(25, 121);
            keyComboBox1.Name = "keyComboBox1";
            keyComboBox1.Size = new Size(275, 40);
            keyComboBox1.TabIndex = 13;
            // 
            // keyComboBox2
            // 
            keyComboBox2.FormattingEnabled = true;
            keyComboBox2.Location = new Point(25, 177);
            keyComboBox2.Name = "keyComboBox2";
            keyComboBox2.Size = new Size(275, 40);
            keyComboBox2.TabIndex = 14;
            // 
            // keyComboBox3
            // 
            keyComboBox3.FormattingEnabled = true;
            keyComboBox3.Location = new Point(25, 232);
            keyComboBox3.Name = "keyComboBox3";
            keyComboBox3.Size = new Size(275, 40);
            keyComboBox3.TabIndex = 15;
            // 
            // recordButton
            // 
            recordButton.Location = new Point(12, 392);
            recordButton.Name = "recordButton";
            recordButton.Size = new Size(150, 46);
            recordButton.TabIndex = 16;
            recordButton.Text = "Record";
            recordButton.UseVisualStyleBackColor = true;
            recordButton.Click += recordButton_Click;
            // 
            // btnPlayMacro
            // 
            btnPlayMacro.Location = new Point(184, 392);
            btnPlayMacro.Name = "btnPlayMacro";
            btnPlayMacro.Size = new Size(150, 46);
            btnPlayMacro.TabIndex = 17;
            btnPlayMacro.Text = "Play Macro";
            btnPlayMacro.UseVisualStyleBackColor = true;
            btnPlayMacro.Click += btnPlayMacro_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(13F, 32F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(btnPlayMacro);
            Controls.Add(recordButton);
            Controls.Add(keyComboBox3);
            Controls.Add(keyComboBox2);
            Controls.Add(keyComboBox1);
            Controls.Add(repeatBox3);
            Controls.Add(repeatBox2);
            Controls.Add(repeatBox1);
            Controls.Add(timeBox3);
            Controls.Add(timeBox2);
            Controls.Add(repeatCheckBox);
            Controls.Add(timeBox1);
            Controls.Add(comboBox1);
            Controls.Add(label1);
            Controls.Add(button1);
            Name = "Form1";
            Text = "Form1";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button button1;
        private Label label1;
        private ComboBox comboBox1;
        private TextBox timeBox1;
        private CheckBox repeatCheckBox;
        private TextBox timeBox2;
        private TextBox timeBox3;
        private TextBox repeatBox1;
        private TextBox repeatBox2;
        private TextBox repeatBox3;
        private ComboBox keyComboBox1;
        private ComboBox keyComboBox2;
        private ComboBox keyComboBox3;
        private Button recordButton;
        private Button btnPlayMacro;
    }
}