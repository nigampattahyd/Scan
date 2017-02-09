namespace BHSScannerApp
{
    partial class SettingsInstaller
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
            this.selectSource = new System.Windows.Forms.Button();
            this.autoRotateCheckBox = new System.Windows.Forms.CheckBox();
            this.useAdfCheckBox = new System.Windows.Forms.CheckBox();
            this.autoDetectBorderCheckBox = new System.Windows.Forms.CheckBox();
            this.useUICheckBox = new System.Windows.Forms.CheckBox();
            this.useDuplexCheckBox = new System.Windows.Forms.CheckBox();
            this.blackAndWhiteCheckBox = new System.Windows.Forms.CheckBox();
            this.showProgressIndicatorUICheckBox = new System.Windows.Forms.CheckBox();
            this.checkBoxArea = new System.Windows.Forms.CheckBox();
            this.saveButton = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // selectSource
            // 
            this.selectSource.Location = new System.Drawing.Point(16, 19);
            this.selectSource.Name = "selectSource";
            this.selectSource.Size = new System.Drawing.Size(116, 48);
            this.selectSource.TabIndex = 2;
            this.selectSource.Text = "Click for available scanners";
            this.selectSource.UseVisualStyleBackColor = true;
            this.selectSource.Click += new System.EventHandler(this.selectSource_Click);
            // 
            // autoRotateCheckBox
            // 
            this.autoRotateCheckBox.AutoSize = true;
            this.autoRotateCheckBox.Location = new System.Drawing.Point(201, 53);
            this.autoRotateCheckBox.Name = "autoRotateCheckBox";
            this.autoRotateCheckBox.Size = new System.Drawing.Size(83, 17);
            this.autoRotateCheckBox.TabIndex = 26;
            this.autoRotateCheckBox.Text = "Auto Rotate";
            this.autoRotateCheckBox.UseVisualStyleBackColor = true;
            // 
            // useAdfCheckBox
            // 
            this.useAdfCheckBox.AutoSize = true;
            this.useAdfCheckBox.Location = new System.Drawing.Point(12, 30);
            this.useAdfCheckBox.Name = "useAdfCheckBox";
            this.useAdfCheckBox.Size = new System.Drawing.Size(69, 17);
            this.useAdfCheckBox.TabIndex = 19;
            this.useAdfCheckBox.Text = "Use ADF";
            this.useAdfCheckBox.UseVisualStyleBackColor = true;
            // 
            // autoDetectBorderCheckBox
            // 
            this.autoDetectBorderCheckBox.AutoSize = true;
            this.autoDetectBorderCheckBox.Location = new System.Drawing.Point(201, 30);
            this.autoDetectBorderCheckBox.Name = "autoDetectBorderCheckBox";
            this.autoDetectBorderCheckBox.Size = new System.Drawing.Size(117, 17);
            this.autoDetectBorderCheckBox.TabIndex = 25;
            this.autoDetectBorderCheckBox.Text = "Auto Detect Border";
            this.autoDetectBorderCheckBox.UseVisualStyleBackColor = true;
            // 
            // useUICheckBox
            // 
            this.useUICheckBox.AutoSize = true;
            this.useUICheckBox.Location = new System.Drawing.Point(332, 30);
            this.useUICheckBox.Name = "useUICheckBox";
            this.useUICheckBox.Size = new System.Drawing.Size(59, 17);
            this.useUICheckBox.TabIndex = 20;
            this.useUICheckBox.Text = "Use UI";
            this.useUICheckBox.UseVisualStyleBackColor = true;
            // 
            // useDuplexCheckBox
            // 
            this.useDuplexCheckBox.AutoSize = true;
            this.useDuplexCheckBox.Location = new System.Drawing.Point(12, 53);
            this.useDuplexCheckBox.Name = "useDuplexCheckBox";
            this.useDuplexCheckBox.Size = new System.Drawing.Size(81, 17);
            this.useDuplexCheckBox.TabIndex = 24;
            this.useDuplexCheckBox.Text = "Use Duplex";
            this.useDuplexCheckBox.UseVisualStyleBackColor = true;
            // 
            // blackAndWhiteCheckBox
            // 
            this.blackAndWhiteCheckBox.AutoSize = true;
            this.blackAndWhiteCheckBox.Location = new System.Drawing.Point(108, 30);
            this.blackAndWhiteCheckBox.Name = "blackAndWhiteCheckBox";
            this.blackAndWhiteCheckBox.Size = new System.Drawing.Size(56, 17);
            this.blackAndWhiteCheckBox.TabIndex = 21;
            this.blackAndWhiteCheckBox.Text = "B && W";
            this.blackAndWhiteCheckBox.UseVisualStyleBackColor = true;
            // 
            // showProgressIndicatorUICheckBox
            // 
            this.showProgressIndicatorUICheckBox.AutoSize = true;
            this.showProgressIndicatorUICheckBox.Location = new System.Drawing.Point(332, 53);
            this.showProgressIndicatorUICheckBox.Name = "showProgressIndicatorUICheckBox";
            this.showProgressIndicatorUICheckBox.Size = new System.Drawing.Size(97, 17);
            this.showProgressIndicatorUICheckBox.TabIndex = 23;
            this.showProgressIndicatorUICheckBox.Text = "Show Progress";
            this.showProgressIndicatorUICheckBox.UseVisualStyleBackColor = true;
            // 
            // checkBoxArea
            // 
            this.checkBoxArea.AutoSize = true;
            this.checkBoxArea.Location = new System.Drawing.Point(108, 53);
            this.checkBoxArea.Name = "checkBoxArea";
            this.checkBoxArea.Size = new System.Drawing.Size(73, 17);
            this.checkBoxArea.TabIndex = 22;
            this.checkBoxArea.Text = "Grab area";
            this.checkBoxArea.UseVisualStyleBackColor = true;
            // 
            // saveButton
            // 
            this.saveButton.Location = new System.Drawing.Point(359, 463);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(89, 25);
            this.saveButton.TabIndex = 27;
            this.saveButton.Text = "Next";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.White;
            this.panel2.BackgroundImage = global::BHSScannerApp.Properties.Resources.logo1;
            this.panel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(460, 76);
            this.panel2.TabIndex = 29;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(145, 37);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(136, 13);
            this.label1.TabIndex = 27;
            this.label1.Text = "Select scanner and options";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.blackAndWhiteCheckBox);
            this.groupBox1.Controls.Add(this.useDuplexCheckBox);
            this.groupBox1.Controls.Add(this.useUICheckBox);
            this.groupBox1.Controls.Add(this.autoDetectBorderCheckBox);
            this.groupBox1.Controls.Add(this.autoRotateCheckBox);
            this.groupBox1.Controls.Add(this.showProgressIndicatorUICheckBox);
            this.groupBox1.Controls.Add(this.checkBoxArea);
            this.groupBox1.Controls.Add(this.useAdfCheckBox);
            this.groupBox1.Location = new System.Drawing.Point(4, 217);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(446, 100);
            this.groupBox1.TabIndex = 28;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Select Scanner Options";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.selectSource);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Location = new System.Drawing.Point(4, 82);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(444, 100);
            this.groupBox2.TabIndex = 30;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Select Scanner";
            // 
            // groupBox3
            // 
            this.groupBox3.Location = new System.Drawing.Point(4, 335);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(444, 86);
            this.groupBox3.TabIndex = 31;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Shortcut Settings";
            // 
            // SettingsInstaller
            // 
            this.AcceptButton = this.saveButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(460, 500);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.saveButton);
            this.Name = "SettingsInstaller";
            this.Text = "BHS Scan Client";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button selectSource;
        private System.Windows.Forms.CheckBox autoRotateCheckBox;
        private System.Windows.Forms.CheckBox useAdfCheckBox;
        private System.Windows.Forms.CheckBox autoDetectBorderCheckBox;
        private System.Windows.Forms.CheckBox useUICheckBox;
        private System.Windows.Forms.CheckBox useDuplexCheckBox;
        private System.Windows.Forms.CheckBox blackAndWhiteCheckBox;
        private System.Windows.Forms.CheckBox showProgressIndicatorUICheckBox;
        private System.Windows.Forms.CheckBox checkBoxArea;
        private System.Windows.Forms.Button saveButton;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
    }
}