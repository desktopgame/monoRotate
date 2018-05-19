namespace TestEditor
{
	partial class Form1
	{
		/// <summary>
		/// 必要なデザイナー変数です。
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// 使用中のリソースをすべてクリーンアップします。
		/// </summary>
		/// <param name="disposing">マネージ リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
		protected override void Dispose(bool disposing)
		{
			if(disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows フォーム デザイナーで生成されたコード

		/// <summary>
		/// デザイナー サポートに必要なメソッドです。このメソッドの内容を
		/// コード エディターで変更しないでください。
		/// </summary>
		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
			this.menuStrip = new System.Windows.Forms.MenuStrip();
			this.fileMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.newMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.openMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.saveAsMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.saveMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.editMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.cutMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.pasteMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.copyMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.undoMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.redoMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this.sizeEditMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolPictureSplit = new System.Windows.Forms.SplitContainer();
			this.listTableSplit = new System.Windows.Forms.SplitContainer();
			this.objectList = new System.Windows.Forms.ListBox();
			this.dataGridView = new System.Windows.Forms.DataGridView();
			this.keyColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.valueColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.pictureFormSplit = new System.Windows.Forms.SplitContainer();
			this.pictureBox = new System.Windows.Forms.PictureBox();
			this.formN = new System.Windows.Forms.Panel();
			this.layerDepthLabel = new System.Windows.Forms.Label();
			this.layerComboBox = new System.Windows.Forms.ComboBox();
			this.formC = new System.Windows.Forms.Panel();
			this.selectGroupBox = new System.Windows.Forms.GroupBox();
			this.snapButton = new System.Windows.Forms.Button();
			this.tagSelectButton = new System.Windows.Forms.Button();
			this.tagClearButton = new System.Windows.Forms.Button();
			this.editAllButton = new System.Windows.Forms.Button();
			this.frameButton = new System.Windows.Forms.Button();
			this.gridButton = new System.Windows.Forms.Button();
			this.removeButton = new System.Windows.Forms.Button();
			this.maxYFlattenButton = new System.Windows.Forms.Button();
			this.avgYFlattenButton = new System.Windows.Forms.Button();
			this.minYFlattenButton = new System.Windows.Forms.Button();
			this.minXFlattenButton = new System.Windows.Forms.Button();
			this.maxXFlattenButton = new System.Windows.Forms.Button();
			this.avgXFlattenButton = new System.Windows.Forms.Button();
			this.groupBox = new System.Windows.Forms.GroupBox();
			this.selectRadioButton = new System.Windows.Forms.RadioButton();
			this.drawRadioButton = new System.Windows.Forms.RadioButton();
			this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
			this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
			this.menuStrip.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.toolPictureSplit)).BeginInit();
			this.toolPictureSplit.Panel1.SuspendLayout();
			this.toolPictureSplit.Panel2.SuspendLayout();
			this.toolPictureSplit.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.listTableSplit)).BeginInit();
			this.listTableSplit.Panel1.SuspendLayout();
			this.listTableSplit.Panel2.SuspendLayout();
			this.listTableSplit.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureFormSplit)).BeginInit();
			this.pictureFormSplit.Panel1.SuspendLayout();
			this.pictureFormSplit.Panel2.SuspendLayout();
			this.pictureFormSplit.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
			this.formN.SuspendLayout();
			this.formC.SuspendLayout();
			this.selectGroupBox.SuspendLayout();
			this.groupBox.SuspendLayout();
			this.SuspendLayout();
			// 
			// menuStrip
			// 
			this.menuStrip.ImageScalingSize = new System.Drawing.Size(24, 24);
			this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileMenu,
            this.editMenu});
			this.menuStrip.Location = new System.Drawing.Point(0, 0);
			this.menuStrip.Name = "menuStrip";
			this.menuStrip.Size = new System.Drawing.Size(1251, 33);
			this.menuStrip.TabIndex = 0;
			this.menuStrip.Text = "menuStrip1";
			// 
			// fileMenu
			// 
			this.fileMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newMenuItem,
            this.openMenuItem,
            this.saveAsMenuItem,
            this.saveMenuItem});
			this.fileMenu.Name = "fileMenu";
			this.fileMenu.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F)));
			this.fileMenu.Size = new System.Drawing.Size(94, 29);
			this.fileMenu.Text = "ファイル(F)";
			// 
			// newMenuItem
			// 
			this.newMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("newMenuItem.Image")));
			this.newMenuItem.Name = "newMenuItem";
			this.newMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
			this.newMenuItem.Size = new System.Drawing.Size(312, 30);
			this.newMenuItem.Text = "新規作成(N)";
			this.newMenuItem.Click += new System.EventHandler(this.newMenuItem_Click);
			// 
			// openMenuItem
			// 
			this.openMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("openMenuItem.Image")));
			this.openMenuItem.Name = "openMenuItem";
			this.openMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
			this.openMenuItem.Size = new System.Drawing.Size(312, 30);
			this.openMenuItem.Text = "開く(O)";
			this.openMenuItem.Click += new System.EventHandler(this.openMenuItem_Click);
			// 
			// saveAsMenuItem
			// 
			this.saveAsMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("saveAsMenuItem.Image")));
			this.saveAsMenuItem.Name = "saveAsMenuItem";
			this.saveAsMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.A)));
			this.saveAsMenuItem.Size = new System.Drawing.Size(312, 30);
			this.saveAsMenuItem.Text = "名前を付けて保存(A)";
			this.saveAsMenuItem.Click += new System.EventHandler(this.saveAsMenuItem_Click);
			// 
			// saveMenuItem
			// 
			this.saveMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("saveMenuItem.Image")));
			this.saveMenuItem.Name = "saveMenuItem";
			this.saveMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.A)));
			this.saveMenuItem.Size = new System.Drawing.Size(312, 30);
			this.saveMenuItem.Text = "上書き保存(S)";
			this.saveMenuItem.Click += new System.EventHandler(this.saveMenuItem_Click);
			// 
			// editMenu
			// 
			this.editMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cutMenuItem,
            this.pasteMenuItem,
            this.copyMenuItem,
            this.toolStripSeparator1,
            this.undoMenuItem,
            this.redoMenuItem,
            this.toolStripSeparator2,
            this.sizeEditMenuItem});
			this.editMenu.Name = "editMenu";
			this.editMenu.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.E)));
			this.editMenu.Size = new System.Drawing.Size(79, 29);
			this.editMenu.Text = "編集(E)";
			// 
			// cutMenuItem
			// 
			this.cutMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("cutMenuItem.Image")));
			this.cutMenuItem.Name = "cutMenuItem";
			this.cutMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.X)));
			this.cutMenuItem.Size = new System.Drawing.Size(255, 30);
			this.cutMenuItem.Text = "切り取り(T)";
			this.cutMenuItem.Click += new System.EventHandler(this.cutMenuItem_Click);
			// 
			// pasteMenuItem
			// 
			this.pasteMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("pasteMenuItem.Image")));
			this.pasteMenuItem.Name = "pasteMenuItem";
			this.pasteMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.V)));
			this.pasteMenuItem.Size = new System.Drawing.Size(255, 30);
			this.pasteMenuItem.Text = "貼り付け(P)";
			this.pasteMenuItem.Click += new System.EventHandler(this.pasteMenuItem_Click);
			// 
			// copyMenuItem
			// 
			this.copyMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("copyMenuItem.Image")));
			this.copyMenuItem.Name = "copyMenuItem";
			this.copyMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
			this.copyMenuItem.Size = new System.Drawing.Size(255, 30);
			this.copyMenuItem.Text = "コピー(C)";
			this.copyMenuItem.Click += new System.EventHandler(this.copyMenuItem_Click);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(252, 6);
			// 
			// undoMenuItem
			// 
			this.undoMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("undoMenuItem.Image")));
			this.undoMenuItem.Name = "undoMenuItem";
			this.undoMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Z)));
			this.undoMenuItem.Size = new System.Drawing.Size(255, 30);
			this.undoMenuItem.Text = "前へ(U)";
			this.undoMenuItem.Click += new System.EventHandler(this.undoMenuItem_Click);
			// 
			// redoMenuItem
			// 
			this.redoMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("redoMenuItem.Image")));
			this.redoMenuItem.Name = "redoMenuItem";
			this.redoMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Y)));
			this.redoMenuItem.Size = new System.Drawing.Size(255, 30);
			this.redoMenuItem.Text = "次へ(R)";
			this.redoMenuItem.Click += new System.EventHandler(this.redoMenuItem_Click);
			// 
			// toolStripSeparator2
			// 
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new System.Drawing.Size(252, 6);
			// 
			// sizeEditMenuItem
			// 
			this.sizeEditMenuItem.Name = "sizeEditMenuItem";
			this.sizeEditMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
			this.sizeEditMenuItem.Size = new System.Drawing.Size(255, 30);
			this.sizeEditMenuItem.Text = "サイズ変更(S)";
			this.sizeEditMenuItem.Click += new System.EventHandler(this.sizeEditMenuItem_Click);
			// 
			// toolPictureSplit
			// 
			this.toolPictureSplit.Dock = System.Windows.Forms.DockStyle.Fill;
			this.toolPictureSplit.Location = new System.Drawing.Point(0, 33);
			this.toolPictureSplit.Name = "toolPictureSplit";
			// 
			// toolPictureSplit.Panel1
			// 
			this.toolPictureSplit.Panel1.AutoScroll = true;
			this.toolPictureSplit.Panel1.Controls.Add(this.listTableSplit);
			// 
			// toolPictureSplit.Panel2
			// 
			this.toolPictureSplit.Panel2.AutoScroll = true;
			this.toolPictureSplit.Panel2.Controls.Add(this.pictureFormSplit);
			this.toolPictureSplit.Size = new System.Drawing.Size(1251, 870);
			this.toolPictureSplit.SplitterDistance = 416;
			this.toolPictureSplit.TabIndex = 1;
			// 
			// listTableSplit
			// 
			this.listTableSplit.Dock = System.Windows.Forms.DockStyle.Fill;
			this.listTableSplit.Location = new System.Drawing.Point(0, 0);
			this.listTableSplit.Name = "listTableSplit";
			this.listTableSplit.Orientation = System.Windows.Forms.Orientation.Horizontal;
			// 
			// listTableSplit.Panel1
			// 
			this.listTableSplit.Panel1.AutoScroll = true;
			this.listTableSplit.Panel1.Controls.Add(this.objectList);
			// 
			// listTableSplit.Panel2
			// 
			this.listTableSplit.Panel2.AutoScroll = true;
			this.listTableSplit.Panel2.Controls.Add(this.dataGridView);
			this.listTableSplit.Size = new System.Drawing.Size(416, 870);
			this.listTableSplit.SplitterDistance = 413;
			this.listTableSplit.TabIndex = 0;
			// 
			// objectList
			// 
			this.objectList.Dock = System.Windows.Forms.DockStyle.Fill;
			this.objectList.FormattingEnabled = true;
			this.objectList.ItemHeight = 18;
			this.objectList.Location = new System.Drawing.Point(0, 0);
			this.objectList.Name = "objectList";
			this.objectList.Size = new System.Drawing.Size(416, 413);
			this.objectList.TabIndex = 0;
			// 
			// dataGridView
			// 
			this.dataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
			this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.keyColumn,
            this.valueColumn});
			this.dataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
			this.dataGridView.Location = new System.Drawing.Point(0, 0);
			this.dataGridView.Name = "dataGridView";
			this.dataGridView.RowTemplate.Height = 27;
			this.dataGridView.Size = new System.Drawing.Size(416, 453);
			this.dataGridView.TabIndex = 0;
			// 
			// keyColumn
			// 
			this.keyColumn.HeaderText = "キー";
			this.keyColumn.Name = "keyColumn";
			// 
			// valueColumn
			// 
			this.valueColumn.HeaderText = "値";
			this.valueColumn.Name = "valueColumn";
			// 
			// pictureFormSplit
			// 
			this.pictureFormSplit.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pictureFormSplit.Location = new System.Drawing.Point(0, 0);
			this.pictureFormSplit.Name = "pictureFormSplit";
			this.pictureFormSplit.Orientation = System.Windows.Forms.Orientation.Horizontal;
			// 
			// pictureFormSplit.Panel1
			// 
			this.pictureFormSplit.Panel1.AutoScroll = true;
			this.pictureFormSplit.Panel1.Controls.Add(this.pictureBox);
			// 
			// pictureFormSplit.Panel2
			// 
			this.pictureFormSplit.Panel2.AutoScroll = true;
			this.pictureFormSplit.Panel2.Controls.Add(this.formN);
			this.pictureFormSplit.Panel2.Controls.Add(this.formC);
			this.pictureFormSplit.Panel2MinSize = 200;
			this.pictureFormSplit.Size = new System.Drawing.Size(831, 870);
			this.pictureFormSplit.SplitterDistance = 327;
			this.pictureFormSplit.TabIndex = 0;
			// 
			// pictureBox
			// 
			this.pictureBox.Location = new System.Drawing.Point(0, 0);
			this.pictureBox.Name = "pictureBox";
			this.pictureBox.Size = new System.Drawing.Size(1280, 720);
			this.pictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
			this.pictureBox.TabIndex = 0;
			this.pictureBox.TabStop = false;
			// 
			// formN
			// 
			this.formN.AutoSize = true;
			this.formN.Controls.Add(this.layerDepthLabel);
			this.formN.Controls.Add(this.layerComboBox);
			this.formN.Location = new System.Drawing.Point(21, 14);
			this.formN.Name = "formN";
			this.formN.Size = new System.Drawing.Size(810, 76);
			this.formN.TabIndex = 1;
			// 
			// layerDepthLabel
			// 
			this.layerDepthLabel.AutoSize = true;
			this.layerDepthLabel.Location = new System.Drawing.Point(17, 21);
			this.layerDepthLabel.Name = "layerDepthLabel";
			this.layerDepthLabel.Size = new System.Drawing.Size(65, 18);
			this.layerDepthLabel.TabIndex = 0;
			this.layerDepthLabel.Text = "レイヤー";
			// 
			// layerComboBox
			// 
			this.layerComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.layerComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.layerComboBox.FormattingEnabled = true;
			this.layerComboBox.Items.AddRange(new object[] {
            "0.0",
            "0.1",
            "0.2",
            "0.3",
            "0.4",
            "0.5",
            "0.6",
            "0.7",
            "0.8",
            "0.9",
            "1.0"});
			this.layerComboBox.Location = new System.Drawing.Point(98, 18);
			this.layerComboBox.Name = "layerComboBox";
			this.layerComboBox.Size = new System.Drawing.Size(700, 26);
			this.layerComboBox.TabIndex = 1;
			// 
			// formC
			// 
			this.formC.Controls.Add(this.selectGroupBox);
			this.formC.Controls.Add(this.groupBox);
			this.formC.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.formC.Location = new System.Drawing.Point(0, 201);
			this.formC.Name = "formC";
			this.formC.Size = new System.Drawing.Size(831, 338);
			this.formC.TabIndex = 7;
			// 
			// selectGroupBox
			// 
			this.selectGroupBox.Controls.Add(this.snapButton);
			this.selectGroupBox.Controls.Add(this.tagSelectButton);
			this.selectGroupBox.Controls.Add(this.tagClearButton);
			this.selectGroupBox.Controls.Add(this.editAllButton);
			this.selectGroupBox.Controls.Add(this.frameButton);
			this.selectGroupBox.Controls.Add(this.gridButton);
			this.selectGroupBox.Controls.Add(this.removeButton);
			this.selectGroupBox.Controls.Add(this.maxYFlattenButton);
			this.selectGroupBox.Controls.Add(this.avgYFlattenButton);
			this.selectGroupBox.Controls.Add(this.minYFlattenButton);
			this.selectGroupBox.Controls.Add(this.minXFlattenButton);
			this.selectGroupBox.Controls.Add(this.maxXFlattenButton);
			this.selectGroupBox.Controls.Add(this.avgXFlattenButton);
			this.selectGroupBox.Dock = System.Windows.Forms.DockStyle.Fill;
			this.selectGroupBox.Location = new System.Drawing.Point(0, 100);
			this.selectGroupBox.Name = "selectGroupBox";
			this.selectGroupBox.Size = new System.Drawing.Size(831, 238);
			this.selectGroupBox.TabIndex = 6;
			this.selectGroupBox.TabStop = false;
			this.selectGroupBox.Text = "クイックアクション";
			// 
			// snapButton
			// 
			this.snapButton.Enabled = false;
			this.snapButton.Location = new System.Drawing.Point(21, 179);
			this.snapButton.Name = "snapButton";
			this.snapButton.Size = new System.Drawing.Size(156, 37);
			this.snapButton.TabIndex = 14;
			this.snapButton.Text = "スナップ";
			this.snapButton.UseVisualStyleBackColor = true;
			this.snapButton.Click += new System.EventHandler(this.snapButton_Click);
			// 
			// tagSelectButton
			// 
			this.tagSelectButton.Enabled = false;
			this.tagSelectButton.Location = new System.Drawing.Point(566, 127);
			this.tagSelectButton.Name = "tagSelectButton";
			this.tagSelectButton.Size = new System.Drawing.Size(156, 37);
			this.tagSelectButton.TabIndex = 13;
			this.tagSelectButton.Text = "オブジェクトの整列";
			this.tagSelectButton.UseVisualStyleBackColor = true;
			this.tagSelectButton.Click += new System.EventHandler(this.tagSelectButton_Click);
			// 
			// tagClearButton
			// 
			this.tagClearButton.Enabled = false;
			this.tagClearButton.Location = new System.Drawing.Point(387, 127);
			this.tagClearButton.Name = "tagClearButton";
			this.tagClearButton.Size = new System.Drawing.Size(156, 37);
			this.tagClearButton.TabIndex = 12;
			this.tagClearButton.Text = "タグのクリア";
			this.tagClearButton.UseVisualStyleBackColor = true;
			this.tagClearButton.Click += new System.EventHandler(this.tagClearButton_Click);
			// 
			// editAllButton
			// 
			this.editAllButton.Location = new System.Drawing.Point(204, 127);
			this.editAllButton.Name = "editAllButton";
			this.editAllButton.Size = new System.Drawing.Size(156, 37);
			this.editAllButton.TabIndex = 11;
			this.editAllButton.Text = "キーの一括操作";
			this.editAllButton.UseVisualStyleBackColor = true;
			this.editAllButton.Click += new System.EventHandler(this.editAllButton_Click);
			// 
			// frameButton
			// 
			this.frameButton.Location = new System.Drawing.Point(21, 127);
			this.frameButton.Name = "frameButton";
			this.frameButton.Size = new System.Drawing.Size(156, 37);
			this.frameButton.TabIndex = 2;
			this.frameButton.Text = "フレーム";
			this.frameButton.UseVisualStyleBackColor = true;
			this.frameButton.Click += new System.EventHandler(this.frameButton_Click);
			// 
			// gridButton
			// 
			this.gridButton.Location = new System.Drawing.Point(566, 76);
			this.gridButton.Name = "gridButton";
			this.gridButton.Size = new System.Drawing.Size(156, 37);
			this.gridButton.TabIndex = 10;
			this.gridButton.Text = "グリッド";
			this.gridButton.UseVisualStyleBackColor = true;
			this.gridButton.Click += new System.EventHandler(this.gridButton_Click);
			// 
			// removeButton
			// 
			this.removeButton.Location = new System.Drawing.Point(566, 25);
			this.removeButton.Name = "removeButton";
			this.removeButton.Size = new System.Drawing.Size(156, 36);
			this.removeButton.TabIndex = 9;
			this.removeButton.Text = "削除";
			this.removeButton.UseVisualStyleBackColor = true;
			this.removeButton.Click += new System.EventHandler(this.removeButton_Click);
			// 
			// maxYFlattenButton
			// 
			this.maxYFlattenButton.Location = new System.Drawing.Point(387, 76);
			this.maxYFlattenButton.Name = "maxYFlattenButton";
			this.maxYFlattenButton.Size = new System.Drawing.Size(156, 36);
			this.maxYFlattenButton.TabIndex = 8;
			this.maxYFlattenButton.Text = "最大フラットY";
			this.maxYFlattenButton.UseVisualStyleBackColor = true;
			this.maxYFlattenButton.Click += new System.EventHandler(this.maxYFlattenButton_Click);
			// 
			// avgYFlattenButton
			// 
			this.avgYFlattenButton.Location = new System.Drawing.Point(204, 76);
			this.avgYFlattenButton.Name = "avgYFlattenButton";
			this.avgYFlattenButton.Size = new System.Drawing.Size(156, 36);
			this.avgYFlattenButton.TabIndex = 7;
			this.avgYFlattenButton.Text = "平均フラットY";
			this.avgYFlattenButton.UseVisualStyleBackColor = true;
			this.avgYFlattenButton.Click += new System.EventHandler(this.avgYFlattenButton_Click);
			// 
			// minYFlattenButton
			// 
			this.minYFlattenButton.Location = new System.Drawing.Point(21, 76);
			this.minYFlattenButton.Name = "minYFlattenButton";
			this.minYFlattenButton.Size = new System.Drawing.Size(156, 36);
			this.minYFlattenButton.TabIndex = 6;
			this.minYFlattenButton.Text = "最小フラットY";
			this.minYFlattenButton.UseVisualStyleBackColor = true;
			this.minYFlattenButton.Click += new System.EventHandler(this.minYFlattenButton_Click);
			// 
			// minXFlattenButton
			// 
			this.minXFlattenButton.Location = new System.Drawing.Point(21, 24);
			this.minXFlattenButton.Name = "minXFlattenButton";
			this.minXFlattenButton.Size = new System.Drawing.Size(156, 37);
			this.minXFlattenButton.TabIndex = 3;
			this.minXFlattenButton.Text = "最小フラットX";
			this.minXFlattenButton.UseVisualStyleBackColor = true;
			this.minXFlattenButton.Click += new System.EventHandler(this.minXFlattenButton_Click);
			// 
			// maxXFlattenButton
			// 
			this.maxXFlattenButton.Location = new System.Drawing.Point(387, 24);
			this.maxXFlattenButton.Name = "maxXFlattenButton";
			this.maxXFlattenButton.Size = new System.Drawing.Size(156, 37);
			this.maxXFlattenButton.TabIndex = 5;
			this.maxXFlattenButton.Text = "最大フラットX";
			this.maxXFlattenButton.UseVisualStyleBackColor = true;
			this.maxXFlattenButton.Click += new System.EventHandler(this.maxXFlattenButton_Click);
			// 
			// avgXFlattenButton
			// 
			this.avgXFlattenButton.Location = new System.Drawing.Point(204, 24);
			this.avgXFlattenButton.Name = "avgXFlattenButton";
			this.avgXFlattenButton.Size = new System.Drawing.Size(156, 37);
			this.avgXFlattenButton.TabIndex = 4;
			this.avgXFlattenButton.Text = "平均フラットX";
			this.avgXFlattenButton.UseVisualStyleBackColor = true;
			this.avgXFlattenButton.Click += new System.EventHandler(this.avgXFlattenButton_Click);
			// 
			// groupBox
			// 
			this.groupBox.Controls.Add(this.selectRadioButton);
			this.groupBox.Controls.Add(this.drawRadioButton);
			this.groupBox.Dock = System.Windows.Forms.DockStyle.Top;
			this.groupBox.Location = new System.Drawing.Point(0, 0);
			this.groupBox.Name = "groupBox";
			this.groupBox.Size = new System.Drawing.Size(831, 100);
			this.groupBox.TabIndex = 2;
			this.groupBox.TabStop = false;
			this.groupBox.Text = "編集モード";
			// 
			// selectRadioButton
			// 
			this.selectRadioButton.AutoSize = true;
			this.selectRadioButton.Location = new System.Drawing.Point(6, 53);
			this.selectRadioButton.Name = "selectRadioButton";
			this.selectRadioButton.Size = new System.Drawing.Size(69, 22);
			this.selectRadioButton.TabIndex = 1;
			this.selectRadioButton.TabStop = true;
			this.selectRadioButton.Text = "選択";
			this.selectRadioButton.UseVisualStyleBackColor = true;
			this.selectRadioButton.CheckedChanged += new System.EventHandler(this.selectRadioButton_CheckedChanged);
			// 
			// drawRadioButton
			// 
			this.drawRadioButton.AutoSize = true;
			this.drawRadioButton.Checked = true;
			this.drawRadioButton.Location = new System.Drawing.Point(6, 24);
			this.drawRadioButton.Name = "drawRadioButton";
			this.drawRadioButton.Size = new System.Drawing.Size(69, 22);
			this.drawRadioButton.TabIndex = 0;
			this.drawRadioButton.TabStop = true;
			this.drawRadioButton.Text = "描画";
			this.drawRadioButton.UseVisualStyleBackColor = true;
			this.drawRadioButton.CheckedChanged += new System.EventHandler(this.drawRadioButton_CheckedChanged);
			// 
			// openFileDialog
			// 
			this.openFileDialog.FileName = "無題.text";
			this.openFileDialog.RestoreDirectory = true;
			this.openFileDialog.ShowHelp = true;
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 18F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1251, 903);
			this.Controls.Add(this.toolPictureSplit);
			this.Controls.Add(this.menuStrip);
			this.MainMenuStrip = this.menuStrip;
			this.Name = "Form1";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "TestEditor";
			this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
			this.menuStrip.ResumeLayout(false);
			this.menuStrip.PerformLayout();
			this.toolPictureSplit.Panel1.ResumeLayout(false);
			this.toolPictureSplit.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.toolPictureSplit)).EndInit();
			this.toolPictureSplit.ResumeLayout(false);
			this.listTableSplit.Panel1.ResumeLayout(false);
			this.listTableSplit.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.listTableSplit)).EndInit();
			this.listTableSplit.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
			this.pictureFormSplit.Panel1.ResumeLayout(false);
			this.pictureFormSplit.Panel1.PerformLayout();
			this.pictureFormSplit.Panel2.ResumeLayout(false);
			this.pictureFormSplit.Panel2.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictureFormSplit)).EndInit();
			this.pictureFormSplit.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
			this.formN.ResumeLayout(false);
			this.formN.PerformLayout();
			this.formC.ResumeLayout(false);
			this.selectGroupBox.ResumeLayout(false);
			this.groupBox.ResumeLayout(false);
			this.groupBox.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.MenuStrip menuStrip;
		private System.Windows.Forms.ToolStripMenuItem fileMenu;
		private System.Windows.Forms.SplitContainer toolPictureSplit;
		private System.Windows.Forms.SplitContainer listTableSplit;
		private System.Windows.Forms.ListBox objectList;
		private System.Windows.Forms.DataGridView dataGridView;
		private System.Windows.Forms.DataGridViewTextBoxColumn keyColumn;
		private System.Windows.Forms.DataGridViewTextBoxColumn valueColumn;
		private System.Windows.Forms.ToolStripMenuItem editMenu;
		private System.Windows.Forms.OpenFileDialog openFileDialog;
		private System.Windows.Forms.SaveFileDialog saveFileDialog;
		private System.Windows.Forms.SplitContainer pictureFormSplit;
		private System.Windows.Forms.PictureBox pictureBox;
		private System.Windows.Forms.Label layerDepthLabel;
		private System.Windows.Forms.ComboBox layerComboBox;
		private System.Windows.Forms.GroupBox groupBox;
		private System.Windows.Forms.RadioButton drawRadioButton;
		private System.Windows.Forms.RadioButton selectRadioButton;
		private System.Windows.Forms.Button minXFlattenButton;
		private System.Windows.Forms.Button avgXFlattenButton;
		private System.Windows.Forms.Button maxXFlattenButton;
		private System.Windows.Forms.GroupBox selectGroupBox;
		private System.Windows.Forms.Button minYFlattenButton;
		private System.Windows.Forms.Button avgYFlattenButton;
		private System.Windows.Forms.Button maxYFlattenButton;
		private System.Windows.Forms.Button removeButton;
		private System.Windows.Forms.Panel formC;
		private System.Windows.Forms.Panel formN;
		private System.Windows.Forms.Button gridButton;
		private System.Windows.Forms.Button frameButton;
		private System.Windows.Forms.ToolStripMenuItem newMenuItem;
		private System.Windows.Forms.ToolStripMenuItem openMenuItem;
		private System.Windows.Forms.ToolStripMenuItem saveAsMenuItem;
		private System.Windows.Forms.ToolStripMenuItem saveMenuItem;
		private System.Windows.Forms.ToolStripMenuItem cutMenuItem;
		private System.Windows.Forms.ToolStripMenuItem pasteMenuItem;
		private System.Windows.Forms.ToolStripMenuItem copyMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripMenuItem undoMenuItem;
		private System.Windows.Forms.ToolStripMenuItem redoMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
		private System.Windows.Forms.ToolStripMenuItem sizeEditMenuItem;
		private System.Windows.Forms.Button editAllButton;
		private System.Windows.Forms.Button tagClearButton;
		private System.Windows.Forms.Button tagSelectButton;
		private System.Windows.Forms.Button snapButton;
	}
}

