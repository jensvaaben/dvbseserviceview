namespace dvbseserviceview
{
    partial class FilterForm
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
            this.listViewFilterCondition = new System.Windows.Forms.ListView();
            this.Attribute = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Relation = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Value = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.buttonOk = new System.Windows.Forms.Button();
            this.comboBoxAttribute = new System.Windows.Forms.ComboBox();
            this.comboBoxCondition = new System.Windows.Forms.ComboBox();
            this.comboBoxValue = new System.Windows.Forms.ComboBox();
            this.buttonAdd = new System.Windows.Forms.Button();
            this.buttonRemove = new System.Windows.Forms.Button();
            this.buttonReset = new System.Windows.Forms.Button();
            this.radioButtonInclude = new System.Windows.Forms.RadioButton();
            this.radioButtonExclude = new System.Windows.Forms.RadioButton();
            this.SuspendLayout();
            // 
            // listViewFilterCondition
            // 
            this.listViewFilterCondition.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.Attribute,
            this.Relation,
            this.Value});
            this.listViewFilterCondition.FullRowSelect = true;
            this.listViewFilterCondition.Location = new System.Drawing.Point(31, 108);
            this.listViewFilterCondition.Name = "listViewFilterCondition";
            this.listViewFilterCondition.Size = new System.Drawing.Size(545, 97);
            this.listViewFilterCondition.TabIndex = 0;
            this.listViewFilterCondition.UseCompatibleStateImageBehavior = false;
            this.listViewFilterCondition.View = System.Windows.Forms.View.Details;
            // 
            // Attribute
            // 
            this.Attribute.Text = "Attribute";
            // 
            // Relation
            // 
            this.Relation.Text = "Relation";
            // 
            // Value
            // 
            this.Value.Text = "Value";
            // 
            // buttonOk
            // 
            this.buttonOk.Location = new System.Drawing.Point(31, 227);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(75, 23);
            this.buttonOk.TabIndex = 1;
            this.buttonOk.Text = "&OK";
            this.buttonOk.UseVisualStyleBackColor = true;
            this.buttonOk.Click += new System.EventHandler(this.buttonOk_Click);
            // 
            // comboBoxAttribute
            // 
            this.comboBoxAttribute.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxAttribute.FormattingEnabled = true;
            this.comboBoxAttribute.Items.AddRange(new object[] {
            "Name",
            "Provider",
            "NetworkName",
            "CASystemID",
            "Features"});
            this.comboBoxAttribute.Location = new System.Drawing.Point(31, 38);
            this.comboBoxAttribute.Name = "comboBoxAttribute";
            this.comboBoxAttribute.Size = new System.Drawing.Size(121, 21);
            this.comboBoxAttribute.TabIndex = 2;
            // 
            // comboBoxCondition
            // 
            this.comboBoxCondition.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxCondition.FormattingEnabled = true;
            this.comboBoxCondition.Items.AddRange(new object[] {
            "Is",
            "IsNot",
            "LessThan",
            "MoreThan",
            "BeginsWith",
            "EndsWith",
            "Contains",
            "Excludes",
            "InRange"});
            this.comboBoxCondition.Location = new System.Drawing.Point(175, 38);
            this.comboBoxCondition.Name = "comboBoxCondition";
            this.comboBoxCondition.Size = new System.Drawing.Size(121, 21);
            this.comboBoxCondition.TabIndex = 3;
            // 
            // comboBoxValue
            // 
            this.comboBoxValue.FormattingEnabled = true;
            this.comboBoxValue.Location = new System.Drawing.Point(316, 38);
            this.comboBoxValue.Name = "comboBoxValue";
            this.comboBoxValue.Size = new System.Drawing.Size(121, 21);
            this.comboBoxValue.TabIndex = 4;
            // 
            // buttonAdd
            // 
            this.buttonAdd.Location = new System.Drawing.Point(361, 79);
            this.buttonAdd.Name = "buttonAdd";
            this.buttonAdd.Size = new System.Drawing.Size(75, 23);
            this.buttonAdd.TabIndex = 5;
            this.buttonAdd.Text = "&Add";
            this.buttonAdd.UseVisualStyleBackColor = true;
            this.buttonAdd.Click += new System.EventHandler(this.buttonAdd_Click);
            // 
            // buttonRemove
            // 
            this.buttonRemove.Location = new System.Drawing.Point(458, 79);
            this.buttonRemove.Name = "buttonRemove";
            this.buttonRemove.Size = new System.Drawing.Size(75, 23);
            this.buttonRemove.TabIndex = 6;
            this.buttonRemove.Text = "&Remove";
            this.buttonRemove.UseVisualStyleBackColor = true;
            this.buttonRemove.Click += new System.EventHandler(this.buttonRemove_Click);
            // 
            // buttonReset
            // 
            this.buttonReset.Location = new System.Drawing.Point(31, 79);
            this.buttonReset.Name = "buttonReset";
            this.buttonReset.Size = new System.Drawing.Size(75, 23);
            this.buttonReset.TabIndex = 7;
            this.buttonReset.Text = "Reset";
            this.buttonReset.UseVisualStyleBackColor = true;
            this.buttonReset.Click += new System.EventHandler(this.buttonReset_Click);
            // 
            // radioButtonInclude
            // 
            this.radioButtonInclude.AutoSize = true;
            this.radioButtonInclude.Location = new System.Drawing.Point(175, 65);
            this.radioButtonInclude.Name = "radioButtonInclude";
            this.radioButtonInclude.Size = new System.Drawing.Size(60, 17);
            this.radioButtonInclude.TabIndex = 8;
            this.radioButtonInclude.TabStop = true;
            this.radioButtonInclude.Text = "&Include";
            this.radioButtonInclude.UseVisualStyleBackColor = true;
            this.radioButtonInclude.CheckedChanged += new System.EventHandler(this.radioButtonInclude_CheckedChanged);
            // 
            // radioButtonExclude
            // 
            this.radioButtonExclude.AutoSize = true;
            this.radioButtonExclude.Location = new System.Drawing.Point(175, 84);
            this.radioButtonExclude.Name = "radioButtonExclude";
            this.radioButtonExclude.Size = new System.Drawing.Size(63, 17);
            this.radioButtonExclude.TabIndex = 9;
            this.radioButtonExclude.TabStop = true;
            this.radioButtonExclude.Text = "&Exclude";
            this.radioButtonExclude.UseVisualStyleBackColor = true;
            // 
            // FilterForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(638, 262);
            this.Controls.Add(this.radioButtonExclude);
            this.Controls.Add(this.radioButtonInclude);
            this.Controls.Add(this.buttonReset);
            this.Controls.Add(this.buttonRemove);
            this.Controls.Add(this.buttonAdd);
            this.Controls.Add(this.comboBoxValue);
            this.Controls.Add(this.comboBoxCondition);
            this.Controls.Add(this.comboBoxAttribute);
            this.Controls.Add(this.buttonOk);
            this.Controls.Add(this.listViewFilterCondition);
            this.Name = "FilterForm";
            this.Text = "Filter";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FilterForm_FormClosed);
            this.Load += new System.EventHandler(this.FilterForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView listViewFilterCondition;
        private System.Windows.Forms.Button buttonOk;
        private System.Windows.Forms.ComboBox comboBoxAttribute;
        private System.Windows.Forms.ComboBox comboBoxCondition;
        private System.Windows.Forms.ComboBox comboBoxValue;
        private System.Windows.Forms.Button buttonAdd;
        private System.Windows.Forms.Button buttonRemove;
        private System.Windows.Forms.Button buttonReset;
        private System.Windows.Forms.RadioButton radioButtonInclude;
        private System.Windows.Forms.RadioButton radioButtonExclude;
        private System.Windows.Forms.ColumnHeader Attribute;
        private System.Windows.Forms.ColumnHeader Relation;
        private System.Windows.Forms.ColumnHeader Value;
    }
}