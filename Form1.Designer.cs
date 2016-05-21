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
            this.comparisonPreferencesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.ColumnSelectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.treeViewService = new System.Windows.Forms.TreeView();
            this.listViewService = new System.Windows.Forms.ListView();
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
            this.tabPageServiceDIff = new System.Windows.Forms.TabPage();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.treeViewServiceDiff = new System.Windows.Forms.TreeView();
            this.listViewServiceDiff = new System.Windows.Forms.ListView();
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
            this.tabPageServiceDIff.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
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
            this.compareServicesFilesToolStripMenuItem,
            this.comparisonPreferencesToolStripMenuItem,
            this.toolStripSeparator3,
            this.ColumnSelectToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(212, 22);
            this.openToolStripMenuItem.Text = "Open DVB-S";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // openDVBCToolStripMenuItem
            // 
            this.openDVBCToolStripMenuItem.Name = "openDVBCToolStripMenuItem";
            this.openDVBCToolStripMenuItem.Size = new System.Drawing.Size(212, 22);
            this.openDVBCToolStripMenuItem.Text = "Open DVB-C";
            this.openDVBCToolStripMenuItem.Click += new System.EventHandler(this.openDVBCToolStripMenuItem_Click);
            // 
            // openDVBTToolStripMenuItem
            // 
            this.openDVBTToolStripMenuItem.Name = "openDVBTToolStripMenuItem";
            this.openDVBTToolStripMenuItem.Size = new System.Drawing.Size(212, 22);
            this.openDVBTToolStripMenuItem.Text = "Open DVB-T";
            this.openDVBTToolStripMenuItem.Click += new System.EventHandler(this.openDVBTToolStripMenuItem_Click);
            // 
            // filterToolStripMenuItem
            // 
            this.filterToolStripMenuItem.Name = "filterToolStripMenuItem";
            this.filterToolStripMenuItem.Size = new System.Drawing.Size(212, 22);
            this.filterToolStripMenuItem.Text = "Filter...";
            this.filterToolStripMenuItem.Click += new System.EventHandler(this.filterToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(209, 6);
            // 
            // openEITToolStripMenuItem
            // 
            this.openEITToolStripMenuItem.Name = "openEITToolStripMenuItem";
            this.openEITToolStripMenuItem.Size = new System.Drawing.Size(212, 22);
            this.openEITToolStripMenuItem.Text = "Open EIT";
            this.openEITToolStripMenuItem.Click += new System.EventHandler(this.openEITToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(209, 6);
            // 
            // compareServicesFilesToolStripMenuItem
            // 
            this.compareServicesFilesToolStripMenuItem.Name = "compareServicesFilesToolStripMenuItem";
            this.compareServicesFilesToolStripMenuItem.Size = new System.Drawing.Size(212, 22);
            this.compareServicesFilesToolStripMenuItem.Text = "Compare services files...";
            this.compareServicesFilesToolStripMenuItem.Click += new System.EventHandler(this.compareServicesFilesToolStripMenuItem_Click);
            // 
            // comparisonPreferencesToolStripMenuItem
            // 
            this.comparisonPreferencesToolStripMenuItem.Name = "comparisonPreferencesToolStripMenuItem";
            this.comparisonPreferencesToolStripMenuItem.Size = new System.Drawing.Size(212, 22);
            this.comparisonPreferencesToolStripMenuItem.Text = "Comparison preferences...";
            this.comparisonPreferencesToolStripMenuItem.Click += new System.EventHandler(this.comparisonPreferencesToolStripMenuItem_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(209, 6);
            // 
            // ColumnSelectToolStripMenuItem
            // 
            this.ColumnSelectToolStripMenuItem.Name = "ColumnSelectToolStripMenuItem";
            this.ColumnSelectToolStripMenuItem.Size = new System.Drawing.Size(212, 22);
            this.ColumnSelectToolStripMenuItem.Text = "Select service columns...";
            this.ColumnSelectToolStripMenuItem.Click += new System.EventHandler(this.ColumnSelectToolStripMenuItem_Click);
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
            this.splitContainer1.Panel1.Controls.Add(this.treeViewService);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.listViewService);
            this.splitContainer1.Size = new System.Drawing.Size(768, 593);
            this.splitContainer1.SplitterDistance = 254;
            this.splitContainer1.TabIndex = 1;
            // 
            // treeViewService
            // 
            this.treeViewService.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeViewService.Location = new System.Drawing.Point(0, 0);
            this.treeViewService.Name = "treeViewService";
            this.treeViewService.Size = new System.Drawing.Size(254, 593);
            this.treeViewService.TabIndex = 0;
            this.treeViewService.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeViewService_AfterSelect);
            // 
            // listViewService
            // 
            this.listViewService.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listViewService.FullRowSelect = true;
            this.listViewService.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.listViewService.Location = new System.Drawing.Point(0, 0);
            this.listViewService.MultiSelect = false;
            this.listViewService.Name = "listViewService";
            this.listViewService.Size = new System.Drawing.Size(510, 593);
            this.listViewService.TabIndex = 0;
            this.listViewService.UseCompatibleStateImageBehavior = false;
            this.listViewService.View = System.Windows.Forms.View.Details;
            this.listViewService.VirtualMode = true;
            this.listViewService.ColumnWidthChanged += new System.Windows.Forms.ColumnWidthChangedEventHandler(this.listViewService_ColumnWidthChanged);
            this.listViewService.RetrieveVirtualItem += new System.Windows.Forms.RetrieveVirtualItemEventHandler(this.listViewService_RetrieveVirtualItem);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPageMuxDiff);
            this.tabControl1.Controls.Add(this.tabPageServiceDIff);
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
            this.listViewEIT.FullRowSelect = true;
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
            // tabPageServiceDIff
            // 
            this.tabPageServiceDIff.Controls.Add(this.splitContainer2);
            this.tabPageServiceDIff.Location = new System.Drawing.Point(4, 22);
            this.tabPageServiceDIff.Name = "tabPageServiceDIff";
            this.tabPageServiceDIff.Size = new System.Drawing.Size(774, 599);
            this.tabPageServiceDIff.TabIndex = 3;
            this.tabPageServiceDIff.Text = "Service Diff";
            this.tabPageServiceDIff.UseVisualStyleBackColor = true;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.treeViewServiceDiff);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.listViewServiceDiff);
            this.splitContainer2.Size = new System.Drawing.Size(774, 599);
            this.splitContainer2.SplitterDistance = 258;
            this.splitContainer2.TabIndex = 0;
            // 
            // treeViewServiceDiff
            // 
            this.treeViewServiceDiff.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeViewServiceDiff.Location = new System.Drawing.Point(0, 0);
            this.treeViewServiceDiff.Name = "treeViewServiceDiff";
            this.treeViewServiceDiff.Size = new System.Drawing.Size(258, 599);
            this.treeViewServiceDiff.TabIndex = 0;
            this.treeViewServiceDiff.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeViewServiceDiff_AfterSelect);
            // 
            // listViewServiceDiff
            // 
            this.listViewServiceDiff.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listViewServiceDiff.FullRowSelect = true;
            this.listViewServiceDiff.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.listViewServiceDiff.Location = new System.Drawing.Point(0, 0);
            this.listViewServiceDiff.Name = "listViewServiceDiff";
            this.listViewServiceDiff.Size = new System.Drawing.Size(512, 599);
            this.listViewServiceDiff.TabIndex = 0;
            this.listViewServiceDiff.UseCompatibleStateImageBehavior = false;
            this.listViewServiceDiff.View = System.Windows.Forms.View.Details;
            this.listViewServiceDiff.VirtualMode = true;
            this.listViewServiceDiff.ColumnWidthChanged += new System.Windows.Forms.ColumnWidthChangedEventHandler(this.listViewServiceDiff_ColumnWidthChanged);
            this.listViewServiceDiff.RetrieveVirtualItem += new System.Windows.Forms.RetrieveVirtualItemEventHandler(this.listViewServiceDiff_RetrieveVirtualItem);
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
            this.tabPageServiceDIff.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TreeView treeViewService;
        private System.Windows.Forms.ListView listViewService;
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
        private System.Windows.Forms.TabPage tabPageServiceDIff;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.TreeView treeViewServiceDiff;
        private System.Windows.Forms.ListView listViewServiceDiff;
        private System.Windows.Forms.ToolStripMenuItem comparisonPreferencesToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem ColumnSelectToolStripMenuItem;
    }
}

