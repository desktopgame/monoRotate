namespace TestEditor
{
	partial class EditAllForm
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
			if(disposing && (components != null))
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
			this.keyLabel = new System.Windows.Forms.Label();
			this.comboBox = new System.Windows.Forms.ComboBox();
			this.valueLabel = new System.Windows.Forms.Label();
			this.textBox = new System.Windows.Forms.TextBox();
			this.canceButton = new System.Windows.Forms.Button();
			this.okButton = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// keyLabel
			// 
			this.keyLabel.AutoSize = true;
			this.keyLabel.Location = new System.Drawing.Point(12, 16);
			this.keyLabel.Name = "keyLabel";
			this.keyLabel.Size = new System.Drawing.Size(38, 18);
			this.keyLabel.TabIndex = 0;
			this.keyLabel.Text = "キー";
			// 
			// comboBox
			// 
			this.comboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBox.FormattingEnabled = true;
			this.comboBox.Location = new System.Drawing.Point(68, 13);
			this.comboBox.Name = "comboBox";
			this.comboBox.Size = new System.Drawing.Size(481, 26);
			this.comboBox.TabIndex = 1;
			// 
			// valueLabel
			// 
			this.valueLabel.AutoSize = true;
			this.valueLabel.Location = new System.Drawing.Point(13, 53);
			this.valueLabel.Name = "valueLabel";
			this.valueLabel.Size = new System.Drawing.Size(26, 18);
			this.valueLabel.TabIndex = 2;
			this.valueLabel.Text = "値";
			// 
			// textBox
			// 
			this.textBox.Location = new System.Drawing.Point(68, 50);
			this.textBox.Name = "textBox";
			this.textBox.Size = new System.Drawing.Size(481, 25);
			this.textBox.TabIndex = 3;
			// 
			// canceButton
			// 
			this.canceButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.canceButton.Location = new System.Drawing.Point(327, 92);
			this.canceButton.Name = "canceButton";
			this.canceButton.Size = new System.Drawing.Size(108, 32);
			this.canceButton.TabIndex = 4;
			this.canceButton.Text = "取り消し";
			this.canceButton.UseVisualStyleBackColor = true;
			// 
			// okButton
			// 
			this.okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.okButton.Location = new System.Drawing.Point(441, 92);
			this.okButton.Name = "okButton";
			this.okButton.Size = new System.Drawing.Size(108, 32);
			this.okButton.TabIndex = 5;
			this.okButton.Text = "了解";
			this.okButton.UseVisualStyleBackColor = true;
			// 
			// EditAllForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 18F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(561, 136);
			this.Controls.Add(this.okButton);
			this.Controls.Add(this.canceButton);
			this.Controls.Add(this.textBox);
			this.Controls.Add(this.valueLabel);
			this.Controls.Add(this.comboBox);
			this.Controls.Add(this.keyLabel);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Name = "EditAllForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "EditAllForm";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label keyLabel;
		private System.Windows.Forms.ComboBox comboBox;
		private System.Windows.Forms.Label valueLabel;
		private System.Windows.Forms.TextBox textBox;
		private System.Windows.Forms.Button canceButton;
		private System.Windows.Forms.Button okButton;
	}
}