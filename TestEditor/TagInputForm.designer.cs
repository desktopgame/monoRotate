namespace TestEditor
{
	partial class TagInputForm
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
			this.cancelButton = new System.Windows.Forms.Button();
			this.okButton = new System.Windows.Forms.Button();
			this.xLayoutCheckBox = new System.Windows.Forms.CheckBox();
			this.yLayoutCheckBox = new System.Windows.Forms.CheckBox();
			this.genLabel = new System.Windows.Forms.Label();
			this.textBox = new System.Windows.Forms.TextBox();
			this.SuspendLayout();
			// 
			// cancelButton
			// 
			this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.cancelButton.Location = new System.Drawing.Point(281, 98);
			this.cancelButton.Name = "cancelButton";
			this.cancelButton.Size = new System.Drawing.Size(93, 35);
			this.cancelButton.TabIndex = 2;
			this.cancelButton.Text = "取り消し";
			this.cancelButton.UseVisualStyleBackColor = true;
			// 
			// okButton
			// 
			this.okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.okButton.Location = new System.Drawing.Point(380, 98);
			this.okButton.Name = "okButton";
			this.okButton.Size = new System.Drawing.Size(93, 35);
			this.okButton.TabIndex = 3;
			this.okButton.Text = "了解";
			this.okButton.UseVisualStyleBackColor = true;
			// 
			// xLayoutCheckBox
			// 
			this.xLayoutCheckBox.AutoSize = true;
			this.xLayoutCheckBox.Location = new System.Drawing.Point(12, 12);
			this.xLayoutCheckBox.Name = "xLayoutCheckBox";
			this.xLayoutCheckBox.Size = new System.Drawing.Size(150, 22);
			this.xLayoutCheckBox.TabIndex = 4;
			this.xLayoutCheckBox.Text = "X軸方向の整列";
			this.xLayoutCheckBox.UseVisualStyleBackColor = true;
			this.xLayoutCheckBox.CheckedChanged += new System.EventHandler(this.xLayoutCheckBox_CheckedChanged);
			// 
			// yLayoutCheckBox
			// 
			this.yLayoutCheckBox.AutoSize = true;
			this.yLayoutCheckBox.Location = new System.Drawing.Point(12, 41);
			this.yLayoutCheckBox.Name = "yLayoutCheckBox";
			this.yLayoutCheckBox.Size = new System.Drawing.Size(150, 22);
			this.yLayoutCheckBox.TabIndex = 5;
			this.yLayoutCheckBox.Text = "Y軸方向の整列";
			this.yLayoutCheckBox.UseVisualStyleBackColor = true;
			this.yLayoutCheckBox.CheckedChanged += new System.EventHandler(this.yLayoutCheckBox_CheckedChanged);
			// 
			// genLabel
			// 
			this.genLabel.AutoSize = true;
			this.genLabel.Location = new System.Drawing.Point(12, 70);
			this.genLabel.Name = "genLabel";
			this.genLabel.Size = new System.Drawing.Size(116, 18);
			this.genLabel.TabIndex = 6;
			this.genLabel.Text = "生成されるキー";
			// 
			// textBox
			// 
			this.textBox.Location = new System.Drawing.Point(156, 67);
			this.textBox.Name = "textBox";
			this.textBox.ReadOnly = true;
			this.textBox.Size = new System.Drawing.Size(317, 25);
			this.textBox.TabIndex = 7;
			this.textBox.Text = "Layout";
			// 
			// TagInputForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 18F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(485, 145);
			this.Controls.Add(this.textBox);
			this.Controls.Add(this.genLabel);
			this.Controls.Add(this.yLayoutCheckBox);
			this.Controls.Add(this.xLayoutCheckBox);
			this.Controls.Add(this.okButton);
			this.Controls.Add(this.cancelButton);
			this.Name = "TagInputForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "TagInputForm";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion
		private System.Windows.Forms.Button cancelButton;
		private System.Windows.Forms.Button okButton;
		private System.Windows.Forms.CheckBox xLayoutCheckBox;
		private System.Windows.Forms.CheckBox yLayoutCheckBox;
		private System.Windows.Forms.Label genLabel;
		private System.Windows.Forms.TextBox textBox;
	}
}