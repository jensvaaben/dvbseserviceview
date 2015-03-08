using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace dvbseserviceview
{
    public partial class FilterForm : Form
    {

        public FilterForm()
        {
            InitializeComponent();

            //initialize column header width
            // no validation of array sizes.
            if (Properties.Settings.Default.FilterColumnHeaderWidth.Count() > 0)
            {
                string[] columnwidth = Properties.Settings.Default.FilterColumnHeaderWidth.Split(new char[1] { ',' });
                for (int n = 0; n < columnwidth.Count(); n++)
                {
                    this.listViewFilterCondition.Columns[n].Width = Convert.ToInt32(columnwidth[n]);
                }
            }
        }

        internal FilterContext filterContext = new FilterContext();

        private void FilterForm_Load(object sender, EventArgs e)
        {
            if (this.filterContext.Exclude) this.radioButtonExclude.Checked = true;
            else this.radioButtonInclude.Checked = true;
            this.comboBoxAttribute.SelectedIndex = 0;
            this.comboBoxCondition.SelectedIndex = 0;
            RefreshList();
        }

        private void RefreshList()
        {
            this.listViewFilterCondition.Items.Clear();
            foreach (var item in this.filterContext.FilterConditionSet)
            {
                ListViewItem i = new ListViewItem();
                i.Checked = item.Enable;
                i.Tag = item;
                i.Text = ConvertAttributeTypeToString(item.filterAttributeType);
                i.SubItems.Add(ConvertFilterRelationTypeToString(item.filterRelationType));
                i.SubItems.Add(item.Value);
                this.listViewFilterCondition.Items.Add(i);
            }
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            FilterCondition filtercondition = new FilterCondition();

            if ((string)this.comboBoxAttribute.SelectedItem == "Name")
            {
                filtercondition.filterAttributeType = FilterAttributeType.Name;
            }
            else if ((string)this.comboBoxAttribute.SelectedItem == "Provider")
            {
                filtercondition.filterAttributeType = FilterAttributeType.Provider;
            }
            else if ((string)this.comboBoxAttribute.SelectedItem == "NetworkName")
            {
                filtercondition.filterAttributeType = FilterAttributeType.NetworkName;
            }
            else if ((string)this.comboBoxAttribute.SelectedItem == "CASystemID")
            {
                filtercondition.filterAttributeType = FilterAttributeType.CASystemID;
            }
            else if ((string)this.comboBoxAttribute.SelectedItem == "Features")
            {
                filtercondition.filterAttributeType = FilterAttributeType.Features;
            }

            if ((string)this.comboBoxCondition.SelectedItem == "Is")
            {
                filtercondition.filterRelationType = FilterRelationType.Is;
            }
            else if ((string)this.comboBoxCondition.SelectedItem == "IsNot")
            {
                filtercondition.filterRelationType = FilterRelationType.IsNot;
            }
            else if ((string)this.comboBoxCondition.SelectedItem == "LessThan")
            {
                filtercondition.filterRelationType = FilterRelationType.LessThan;
            }
            else if ((string)this.comboBoxCondition.SelectedItem == "MoreThan")
            {
                filtercondition.filterRelationType = FilterRelationType.MoreThan;
            }
            else if ((string)this.comboBoxCondition.SelectedItem == "BeginsWith")
            {
                filtercondition.filterRelationType = FilterRelationType.BeginsWith;
            }
            else if ((string)this.comboBoxCondition.SelectedItem == "EndsWith")
            {
                filtercondition.filterRelationType = FilterRelationType.EndsWith;
            }
            else if ((string)this.comboBoxCondition.SelectedItem == "Contains")
            {
                filtercondition.filterRelationType = FilterRelationType.Contains;
            }
            else if ((string)this.comboBoxCondition.SelectedItem == "Excludes")
            {
                filtercondition.filterRelationType = FilterRelationType.Excludes;
            }
            else if ((string)this.comboBoxCondition.SelectedItem == "InRange")
            {
                filtercondition.filterRelationType = FilterRelationType.InRange;
            }

            filtercondition.Value = this.comboBoxValue.Text;
            filtercondition.Enable = true;

            this.filterContext.FilterConditionSet.Add(filtercondition);
            RefreshList();
        }

        private void buttonRemove_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem item in this.listViewFilterCondition.SelectedItems)
            {
                this.filterContext.FilterConditionSet.Remove((FilterCondition)item.Tag);
            }
            RefreshList();
        }

        private void buttonReset_Click(object sender, EventArgs e)
        {
            this.filterContext.FilterConditionSet.Clear();
            RefreshList();
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            Close();   
        }

        private string ConvertAttributeTypeToString(FilterAttributeType t)
        {
            if (t == FilterAttributeType.Name) return "Name";
            else if (t == FilterAttributeType.Provider) return "Provider";
            else if (t == FilterAttributeType.NetworkName) return "NetworkName";
            else if (t == FilterAttributeType.CASystemID) return "CASystemID";
            else if (t == FilterAttributeType.Features) return "Features";
            else return "None"; // this should not happen
        }

        private string ConvertFilterRelationTypeToString(FilterRelationType t)
        {
            if (t == FilterRelationType.Is) return "Is";
            else if (t == FilterRelationType.IsNot) return "IsNot";
            else if (t == FilterRelationType.LessThan) return "LessThan";
            else if (t == FilterRelationType.MoreThan) return "MoreThan";
            else if (t == FilterRelationType.BeginsWith) return "BeginsWith";
            else if (t == FilterRelationType.EndsWith) return "EndsWith";
            else if (t == FilterRelationType.Contains) return "Contains";
            else if (t == FilterRelationType.Excludes) return "Excludes";
            else if (t == FilterRelationType.InRange) return "InRange";
            else return "None"; // this should not happen
        }

        private void radioButtonInclude_CheckedChanged(object sender, EventArgs e)
        {
            this.filterContext.Exclude = this.radioButtonExclude.Checked;
        }

        private void FilterForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            //save column width state

            int[] columnwidth = new int[3];


            for (int n = 0; n < this.listViewFilterCondition.Columns.Count; n++)
            {
                columnwidth[n] = this.listViewFilterCondition.Columns[n].Width;
            }

            string str = "";
            for (int n = 0; n < columnwidth.Count(); n++)
            {
                if (n != 0) str += ",";
                str += Convert.ToString(columnwidth[n]);
            }
            Properties.Settings.Default.FilterColumnHeaderWidth = str;
            Properties.Settings.Default.Save();
        }

        private void listViewFilterCondition_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            ((FilterCondition)e.Item.Tag).Enable = e.Item.Checked;
        }
    }
}
