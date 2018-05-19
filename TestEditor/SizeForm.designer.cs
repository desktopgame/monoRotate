namespace TestEditor
{
	partial class SizeForm
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
			this.widthLabel = new System.Windows.Forms.Label();
			this.heightLabel = new System.Windows.Forms.Label();
			this.widthNumericUpdown = new System.Windows.Forms.NumericUpDown();
			this.heightNumericUpdown = new System.Windows.Forms.NumericUpDown();
			this.cancelButton = new System.Windows.Forms.Button();
			this.okButton = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.widthNumericUpdown)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.heightNumericUpdown)).BeginInit();
			this.SuspendLayout();
			// 
			// widthLabel
			// 
			this.widthLabel.AutoSize = true;
			this.widthLabel.Location = new System.Drawing.Point(13, 13);
			this.widthLabel.Name = "widthLabel";
			this.widthLabel.Size = new System.Drawing.Size(44, 18);
			this.widthLabel.TabIndex = 0;
			this.widthLabel.Text = "横幅";
			// 
			// heightLabel
			// 
			this.heightLabel.AutoSize = true;
			this.heightLabel.Location = new System.Drawing.Point(13, 52);
			this.heightLabel.Name = "heightLabel";
			this.heightLabel.Size = new System.Drawing.Size(44, 18);
			this.heightLabel.TabIndex = 1;
			this.heightLabel.Text = "縦幅";
			// 
			// widthNumericUpdown
			// 
			this.widthNumericUpdown.Increment = new decimal(new int[] {
            32,
            0,
            0,
            0});
			this.widthNumericUpdown.Location = new System.Drawing.Point(63, 11);
			this.widthNumericUpdown.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
			this.widthNumericUpdown.Minimum = new decimal(new int[] {
            1280,
            0,
            0,
            0});
			this.widthNumericUpdown.Name = "widthNumericUpdown";
			this.widthNumericUpdown.Size = new System.Drawing.Size(502, 25);
			this.widthNumericUpdown.TabIndex = 2;
			this.widthNumericUpdown.Value = new decimal(new int[] {
            1280,
            0,
            0,
            0});
			// 
			// heightNumericUpdown
			// 
			this.heightNumericUpdown.Increment = new decimal(new int[] {
            32,
            0,
            0,
            0});
			this.heightNumericUpdown.Location = new System.Drawing.Point(63, 52);
			this.heightNumericUpdown.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
			this.heightNumericUpdown.Minimum = new decimal(new int[] {
            720,
            0,
            0,
            0});
			this.heightNumericUpdown.Name = "heightNumericUpdown";
			this.heightNumericUpdown.Size = new System.Drawing.Size(502, 25);
			this.heightNumericUpdown.TabIndex = 3;
			this.heightNumericUpdown.Value = new decimal(new int[] {
            720,
            0,
            0,
            0});
			// 
			// cancelButton
			// 
			this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.cancelButton.Location = new System.Drawing.Point(354, 101);
			this.cancelButton.Name = "cancelButton";
			this.cancelButton.Size = new System.Drawing.Size(98, 37);
			this.cancelButton.TabIndex = 4;
			this.cancelButton.Text = "取り消し";
			this.cancelButton.UseVisualStyleBackColor = true;
			// 
			// okButton
			// 
			this.okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.okButton.Location = new System.Drawing.Point(467, 101);
			this.okButton.Name = "okButton";
			this.okButton.Size = new System.Drawing.Size(98, 37);
			this.okButton.TabIndex = 5;
			this.okButton.Text = "了解";
			this.okButton.UseVisualStyleBackColor = true;
			// 
			// SizeForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 18F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(577, 150);
			this.Controls.Add(this.okButton);
			this.Controls.Add(this.cancelButton);
			this.Controls.Add(this.heightNumericUpdown);
			this.Controls.Add(this.widthNumericUpdown);
			this.Controls.Add(this.heightLabel);
			this.Controls.Add(this.widthLabel);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Name = "SizeForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "SizeForm";
			((System.ComponentModel.ISupportInitialize)(this.widthNumericUpdown)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.heightNumericUpdown)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label widthLabel;
		private System.Windows.Forms.Label heightLabel;
		private System.Windows.Forms.NumericUpDown widthNumericUpdown;
		private System.Windows.Forms.NumericUpDown heightNumericUpdown;
		private System.Windows.Forms.Button cancelButton;
		private System.Windows.Forms.Button okButton;
	}
}