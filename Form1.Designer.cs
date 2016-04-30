namespace dvbseserviceview
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openDVBCToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openDVBTToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.filterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.openEITToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.compareServicesFilesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.listView1 = new System.Windows.Forms.ListView();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.splitContainerEIT = new System.Windows.Forms.SplitContainer();
            this.treeViewEIT = new System.Windows.Forms.TreeView();
            this.listViewEIT = new System.Windows.Forms.ListView();
            this.columnHeaderEventId = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderVersionNumber = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderTableId = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderOriginalNetworkId = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderTransportStreamId = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderServiceId = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderStartTime = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderEndTime = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderEventName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderEventText = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderExtendedEventText = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tabPageMuxDiff = new System.Windows.Forms.TabPage();
            this.treeViewMuxDiff = new System.Windows.Forms.TreeView();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerEIT)).BeginInit();
            this.splitContainerEIT.Panel1.SuspendLayout();
            this.splitContainerEIT.Panel2.SuspendLayout();
            this.splitContainerEIT.SuspendLayout();
            this.tabPageMuxDiff.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.aboutToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(782, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem,
            this.openDVBCToolStripMenuItem,
            this.openDVBTToolStripMenuItem,
            this.filterToolStripMenuItem,
            this.toolStripSeparator1,
            this.openEITToolStripMenuItem,
            this.toolStripSeparator2,
            this.compareServicesFilesToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(200, 22);
            this.openToolStripMenuItem.Text = "Open DVB-S";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // openDVBCToolStripMenuItem
            // 
            this.openDVBCToolStripMenuItem.Name = "openDVBCToolStripMenuItem";
            this.openDVBCToolStripMenuItem.Size = new System.Drawing.Size(200, 22);
            this.openDVBCToolStripMenuItem.Text = "Open DVB-C";
            this.openDVBCToolStripMenuItem.Click += new System.EventHandler(this.openDVBCToolStripMenuItem_Click);
            // 
            // openDVBTToolStripMenuItem
            // 
            this.openDVBTToolStripMenuItem.Name = "openDVBTToolStripMenuItem";
            this.openDVBTToolStripMenuItem.Size = new System.Drawing.Size(200, 22);
            this.openDVBTToolStripMenuItem.Text = "Open DVB-T";
            this.openDVBTToolStripMenuItem.Click += new System.EventHandler(this.openDVBTToolStripMenuItem_Click);
            // 
            // filterToolStripMenuItem
            // 
            this.filterToolStripMenuItem.Name = "filterToolStripMenuItem";
            this.filterToolStripMenuItem.Size = new System.Drawing.Size(200, 22);
            this.filterToolStripMenuItem.Text = "Filter...";
            this.filterToolStripMenuItem.Click += new System.EventHandler(this.filterToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(197, 6);
            // 
            // openEITToolStripMenuItem
            // 
            this.openEITToolStripMenuItem.Name = "openEITToolStripMenuItem";
            this.openEITToolStripMenuItem.Size = new System.Drawing.Size(200, 22);
            this.openEITToolStripMenuItem.Text = "Open EIT";
            this.openEITToolStripMenuItem.Click += new System.EventHandler(this.openEITToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(197, 6);
            // 
            // compareServicesFilesToolStripMenuItem
            // 
            this.compareServicesFilesToolStripMenuItem.Name = "compareServicesFilesToolStripMenuItem";
            this.compareServicesFilesToolStripMenuItem.Size = new System.Drawing.Size(200, 22);
            this.compareServicesFilesToolStripMenuItem.Text = "Compare services files...";
            this.compareServicesFilesToolStripMenuItem.Click += new System.EventHandler(this.compareServicesFilesToolStripMenuItem_Click);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(52, 20);
            this.aboutToolStripMenuItem.Text = "About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(3, 3);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.treeView1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.listView1);
            this.splitContainer1.Size = new System.Drawing.Size(768, 593);
            this.splitContainer1.SplitterDistance = 254;
            this.splitContainer1.TabIndex = 1;
            // 
            // treeView1
            // 
            this.treeView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView1.Location = new System.Drawing.Point(0, 0);
            this.treeView1.Name = "treeView1";
            this.treeView1.Size = new System.Drawing.Size(254, 593);
            this.treeView1.TabIndex = 0;
            this.treeView1.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView1_AfterSelect);
            // 
            // listView1
            // 
            this.listView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listView1.FullRowSelect = true;
            this.listView1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.listView1.Location = new System.Drawing.Point(0, 0);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(510, 593);
            this.listView1.TabIndex = 0;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPageMuxDiff);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 24);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(782, 625);
            this.tabControl1.TabIndex = 2;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.splitContainer1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(774, 599);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Service";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.splitContainerEIT);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Size = new System.Drawing.Size(774, 599);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "EIT";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // splitContainerEIT
            // 
            this.splitContainerEIT.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerEIT.Location = new System.Drawing.Point(0, 0);
            this.splitContainerEIT.Name = "splitContainerEIT";
            // 
            // splitContainerEIT.Panel1
            // 
            this.splitContainerEIT.Panel1.Controls.Add(this.treeViewEIT);
            // 
            // splitContainerEIT.Panel2
            // 
            this.splitContainerEIT.Panel2.Controls.Add(this.listViewEIT);
            this.splitContainerEIT.Size = new System.Drawing.Size(774, 599);
            this.splitContainerEIT.SplitterDistance = 309;
            this.splitContainerEIT.TabIndex = 1;
            // 
            // treeViewEIT
            // 
            this.treeViewEIT.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeViewEIT.Location = new System.Drawing.Point(0, 0);
            this.treeViewEIT.Name = "treeViewEIT";
            this.treeViewEIT.Size = new System.Drawing.Size(309, 599);
            this.treeViewEIT.TabIndex = 0;
            this.treeViewEIT.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeViewEIT_AfterSelect);
            // 
            // listViewEIT
            // 
            this.listViewEIT.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeaderEventId,
            this.columnHeaderVersionNumber,
            this.columnHeaderTableId,
            this.columnHeaderOriginalNetworkId,
            this.columnHeaderTransportStreamId,
            this.columnHeaderServiceId,
            this.columnHeaderStartTime,
            this.columnHeaderEndTime,
            this.columnHeaderEventName,
            this.columnHeaderEventText,
            this.columnHeaderExtendedEventText});
            this.listViewEIT.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listViewEIT.Location = new System.Drawing.Point(0, 0);
            this.listViewEIT.Name = "listViewEIT";
            this.listViewEIT.Size = new System.Drawing.Size(461, 599);
            this.listViewEIT.TabIndex = 0;
            this.listViewEIT.UseCompatibleStateImageBehavior = false;
            this.listViewEIT.View = System.Windows.Forms.View.Details;
            this.listViewEIT.VirtualMode = true;
            this.listViewEIT.RetrieveVirtualItem += new System.Windows.Forms.RetrieveVirtualItemEventHandler(this.listViewEIT_RetrieveVirtualItem);
            // 
            // columnHeaderEventId
            // 
            this.columnHeaderEventId.Text = "eventid";
            // 
            // columnHeaderVersionNumber
            // 
            this.columnHeaderVersionNumber.Text = "versionnumber";
            // 
            // columnHeaderTableId
            // 
            this.columnHeaderTableId.Text = "tableid";
            // 
            // columnHeaderOriginalNetworkId
            // 
            this.columnHeaderOriginalNetworkId.Text = "onid";
            // 
            // columnHeaderTransportStreamId
            // 
            this.columnHeaderTransportStreamId.Text = "tsid";
            // 
            // columnHeaderServiceId
            // 
            this.columnHeaderServiceId.Text = "sid";
            // 
            // columnHeaderStartTime
            // 
            this.columnHeaderStartTime.Text = "starttime";
            // 
            // columnHeaderEndTime
            // 
            this.columnHeaderEndTime.Text = "endtime";
            // 
            // columnHeaderEventName
            // 
            this.columnHeaderEventName.Text = "eventname";
            // 
            // columnHeaderEventText
            // 
            this.columnHeaderEventText.Text = "eventtext";
            // 
            // columnHeaderExtendedEventText
            // 
            this.columnHeaderExtendedEventText.Text = "extendedeventtext";
            // 
            // tabPageMuxDiff
            // 
            this.tabPageMuxDiff.Controls.Add(this.treeViewMuxDiff);
            this.tabPageMuxDiff.Location = new System.Drawing.Point(4, 22);
            this.tabPageMuxDiff.Name = "tabPageMuxDiff";
            this.tabPageMuxDiff.Size = new System.Drawing.Size(774, 599);
            this.tabPageMuxDiff.TabIndex = 2;
            this.tabPageMuxDiff.Text = "MUX Diff";
            this.tabPageMuxDiff.UseVisualStyleBackColor = true;
            // 
            // treeViewMuxDiff
            // 
            this.treeViewMuxDiff.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeViewMuxDiff.Location = new System.Drawing.Point(0, 0);
            this.treeViewMuxDiff.Name = "treeViewMuxDiff";
            this.treeViewMuxDiff.Size = new System.Drawing.Size(774, 599);
            this.treeViewMuxDiff.TabIndex = 0;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(782, 649);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "DVBStreamExplorer Service Viewer";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.splitContainerEIT.Panel1.ResumeLayout(false);
            this.splitContainerEIT.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerEIT)).EndInit();
            this.splitContainerEIT.ResumeLayout(false);
            this.tabPageMuxDiff.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ToolStripMenuItem openDVBCToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openDVBTToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.ToolStripMenuItem filterToolStripMenuItem;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.ListView listViewEIT;
        private System.Windows.Forms.ToolStripMenuItem openEITToolStripMenuItem;
        private System.Windows.Forms.ColumnHeader columnHeaderEventId;
        private System.Windows.Forms.ColumnHeader columnHeaderVersionNumber;
        private System.Windows.Forms.ColumnHeader columnHeaderTableId;
        private System.Windows.Forms.ColumnHeader columnHeaderOriginalNetworkId;
        private System.Windows.Forms.ColumnHeader columnHeaderTransportStreamId;
        private System.Windows.Forms.ColumnHeader columnHeaderServiceId;
        private System.Windows.Forms.ColumnHeader columnHeaderStartTime;
        private System.Windows.Forms.ColumnHeader columnHeaderEndTime;
        private System.Windows.Forms.ColumnHeader columnHeaderEventName;
        private System.Windows.Forms.ColumnHeader columnHeaderEventText;
        private System.Windows.Forms.ColumnHeader columnHeaderExtendedEventText;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.SplitContainer splitContainerEIT;
        private System.Windows.Forms.TreeView treeViewEIT;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem compareServicesFilesToolStripMenuItem;
        private System.Windows.Forms.TabPage tabPageMuxDiff;
        private System.Windows.Forms.TreeView treeViewMuxDiff;
    }
}

