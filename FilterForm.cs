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

            //populate dropdown list contols
            this.comboBoxAttribute.Items.Add(Properties.Resources.FilterAttributeTypeName);
            this.comboBoxAttribute.Items.Add(Properties.Resources.FilterAttributeTypeProvider);
            this.comboBoxAttribute.Items.Add(Properties.Resources.FilterAttributeTypeNetworkName);
            this.comboBoxAttribute.Items.Add(Properties.Resources.FilterAttributeTypeCASystemID);
            this.comboBoxAttribute.Items.Add(Properties.Resources.FilterAttributeTypeFeatures);
            this.comboBoxAttribute.Items.Add(Properties.Resources.FilterAttributeTypePosition);
            this.comboBoxAttribute.Items.Add(Properties.Resources.FilterAttributeTypeLcn);
            this.comboBoxAttribute.Items.Add(Properties.Resources.FilterAttributeTypeFreeCAMode);
            this.comboBoxAttribute.Items.Add(Properties.Resources.FilterAttributeTypeType);
            this.comboBoxAttribute.Items.Add(Properties.Resources.FilterAttributeTypePcr);
            this.comboBoxAttribute.Items.Add(Properties.Resources.FilterAttributeTypePmt);
            this.comboBoxAttribute.Items.Add(Properties.Resources.FilterAttributeTypeSid);
            this.comboBoxAttribute.Items.Add(Properties.Resources.FilterAttributeTypeTsid);
            this.comboBoxAttribute.Items.Add(Properties.Resources.FilterAttributeTypeNid);
            this.comboBoxAttribute.Items.Add(Properties.Resources.FilterAttributeTypeOnid);
            this.comboBoxAttribute.Items.Add(Properties.Resources.FilterAttributeTypeBouquetList);
            this.comboBoxAttribute.Items.Add(Properties.Resources.FilterAttributeTypeVideo);
            this.comboBoxAttribute.Items.Add(Properties.Resources.FilterAttributeTypeAudio);

            this.comboBoxCondition.Items.Add(Properties.Resources.FilterRelationTypeIs);
            this.comboBoxCondition.Items.Add(Properties.Resources.FilterRelationTypeIsNot);
            this.comboBoxCondition.Items.Add(Properties.Resources.FilterRelationTypeLessThan);
            this.comboBoxCondition.Items.Add(Properties.Resources.FilterRelationTypeMoreThan);
            this.comboBoxCondition.Items.Add(Properties.Resources.FilterRelationTypeBeginsWith);
            this.comboBoxCondition.Items.Add(Properties.Resources.FilterRelationTypeEndsWith);
            this.comboBoxCondition.Items.Add(Properties.Resources.FilterRelationTypeContains);
            this.comboBoxCondition.Items.Add(Properties.Resources.FilterRelationTypeExcludes);
            this.comboBoxCondition.Items.Add(Properties.Resources.FilterRelationTypeInRange);
            
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

            if ((string)this.comboBoxAttribute.SelectedItem == Properties.Resources.FilterAttributeTypeName)
            {
                filtercondition.filterAttributeType = FilterAttributeType.Name;
            }
            else if ((string)this.comboBoxAttribute.SelectedItem == Properties.Resources.FilterAttributeTypeProvider)
            {
                filtercondition.filterAttributeType = FilterAttributeType.Provider;
            }
            else if ((string)this.comboBoxAttribute.SelectedItem == Properties.Resources.FilterAttributeTypeNetworkName)
            {
                filtercondition.filterAttributeType = FilterAttributeType.NetworkName;
            }
            else if ((string)this.comboBoxAttribute.SelectedItem == Properties.Resources.FilterAttributeTypeCASystemID)
            {
                filtercondition.filterAttributeType = FilterAttributeType.CASystemID;
            }
            else if ((string)this.comboBoxAttribute.SelectedItem == Properties.Resources.FilterAttributeTypeFeatures)
            {
                filtercondition.filterAttributeType = FilterAttributeType.Features;
            }

            if ((string)this.comboBoxCondition.SelectedItem == Properties.Resources.FilterRelationTypeIs)
            {
                filtercondition.filterRelationType = FilterRelationType.Is;
            }
            else if ((string)this.comboBoxCondition.SelectedItem == Properties.Resources.FilterRelationTypeIsNot)
            {
                filtercondition.filterRelationType = FilterRelationType.IsNot;
            }
            else if ((string)this.comboBoxCondition.SelectedItem == Properties.Resources.FilterRelationTypeLessThan)
            {
                filtercondition.filterRelationType = FilterRelationType.LessThan;
            }
            else if ((string)this.comboBoxCondition.SelectedItem == Properties.Resources.FilterRelationTypeMoreThan)
            {
                filtercondition.filterRelationType = FilterRelationType.MoreThan;
            }
            else if ((string)this.comboBoxCondition.SelectedItem == Properties.Resources.FilterRelationTypeBeginsWith)
            {
                filtercondition.filterRelationType = FilterRelationType.BeginsWith;
            }
            else if ((string)this.comboBoxCondition.SelectedItem == Properties.Resources.FilterRelationTypeEndsWith)
            {
                filtercondition.filterRelationType = FilterRelationType.EndsWith;
            }
            else if ((string)this.comboBoxCondition.SelectedItem == Properties.Resources.FilterRelationTypeContains)
            {
                filtercondition.filterRelationType = FilterRelationType.Contains;
            }
            else if ((string)this.comboBoxCondition.SelectedItem == Properties.Resources.FilterRelationTypeExcludes)
            {
                filtercondition.filterRelationType = FilterRelationType.Excludes;
            }
            else if ((string)this.comboBoxCondition.SelectedItem == Properties.Resources.FilterRelationTypeInRange)
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
            if (t == FilterAttributeType.Name) return Properties.Resources.FilterAttributeTypeName;
            else if (t == FilterAttributeType.Provider) return Properties.Resources.FilterAttributeTypeProvider;
            else if (t == FilterAttributeType.NetworkName) return Properties.Resources.FilterAttributeTypeNetworkName;
            else if (t == FilterAttributeType.CASystemID) return Properties.Resources.FilterAttributeTypeCASystemID;
            else if (t == FilterAttributeType.Features) return Properties.Resources.FilterAttributeTypeFeatures;
            else return Properties.Resources.FilterAttributeTypeNone; // this should not happen
        }

        private string ConvertFilterRelationTypeToString(FilterRelationType t)
        {
            if (t == FilterRelationType.Is) return Properties.Resources.FilterRelationTypeIs;
            else if (t == FilterRelationType.IsNot) return Properties.Resources.FilterRelationTypeIsNot;
            else if (t == FilterRelationType.LessThan) return Properties.Resources.FilterRelationTypeLessThan;
            else if (t == FilterRelationType.MoreThan) return Properties.Resources.FilterRelationTypeMoreThan;
            else if (t == FilterRelationType.BeginsWith) return Properties.Resources.FilterRelationTypeBeginsWith;
            else if (t == FilterRelationType.EndsWith) return Properties.Resources.FilterRelationTypeEndsWith;
            else if (t == FilterRelationType.Contains) return Properties.Resources.FilterRelationTypeContains;
            else if (t == FilterRelationType.Excludes) return Properties.Resources.FilterRelationTypeExcludes;
            else if (t == FilterRelationType.InRange) return Properties.Resources.FilterRelationTypeInRange;
            else return Properties.Resources.FilterRelationTypeNone; // this should not happen
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

        private void comboBoxAttribute_SelectedIndexChanged(object sender, EventArgs e)
        {
            //TODO: populate relation dropdown list in context of selected attribute
            Trace.WriteLine("comboBoxAttribute_SelectedIndexChanged");
            this.comboBoxCondition.Items.Clear();
            switch(FilterAttributeTypeFromString((string)this.comboBoxAttribute.SelectedItem))
            {
                case FilterAttributeType.Name:
                case FilterAttributeType.Provider:
                case FilterAttributeType.NetworkName:
                case FilterAttributeType.Position:
                    this.comboBoxCondition.Items.Add(Properties.Resources.FilterRelationTypeIs);
                    this.comboBoxCondition.Items.Add(Properties.Resources.FilterRelationTypeIsNot);
                    this.comboBoxCondition.Items.Add(Properties.Resources.FilterRelationTypeLessThan);
                    this.comboBoxCondition.Items.Add(Properties.Resources.FilterRelationTypeMoreThan);
                    this.comboBoxCondition.Items.Add(Properties.Resources.FilterRelationTypeBeginsWith);
                    this.comboBoxCondition.Items.Add(Properties.Resources.FilterRelationTypeEndsWith);
                    this.comboBoxCondition.Items.Add(Properties.Resources.FilterRelationTypeContains);
                    this.comboBoxCondition.Items.Add(Properties.Resources.FilterRelationTypeExcludes);
                    break;
                case FilterAttributeType.CASystemID:
                    this.comboBoxCondition.Items.Add(Properties.Resources.FilterRelationTypeIs);
                    this.comboBoxCondition.Items.Add(Properties.Resources.FilterRelationTypeIsNot);
                    this.comboBoxCondition.Items.Add(Properties.Resources.FilterRelationTypeLessThan);
                    this.comboBoxCondition.Items.Add(Properties.Resources.FilterRelationTypeMoreThan);
                    this.comboBoxCondition.Items.Add(Properties.Resources.FilterRelationTypeBeginsWith);
                    this.comboBoxCondition.Items.Add(Properties.Resources.FilterRelationTypeEndsWith);
                    this.comboBoxCondition.Items.Add(Properties.Resources.FilterRelationTypeContains);
                    this.comboBoxCondition.Items.Add(Properties.Resources.FilterRelationTypeExcludes);
                    this.comboBoxCondition.Items.Add(Properties.Resources.FilterRelationTypeInRange);
                    break;
                case FilterAttributeType.Features:
                    this.comboBoxCondition.Items.Add(Properties.Resources.FilterRelationTypeContains);
                    break;
                default:
                    this.comboBoxCondition.Items.Add(Properties.Resources.FilterRelationTypeIs);
                    this.comboBoxCondition.Items.Add(Properties.Resources.FilterRelationTypeIsNot);
                    this.comboBoxCondition.Items.Add(Properties.Resources.FilterRelationTypeLessThan);
                    this.comboBoxCondition.Items.Add(Properties.Resources.FilterRelationTypeMoreThan);
                    this.comboBoxCondition.Items.Add(Properties.Resources.FilterRelationTypeBeginsWith);
                    this.comboBoxCondition.Items.Add(Properties.Resources.FilterRelationTypeEndsWith);
                    this.comboBoxCondition.Items.Add(Properties.Resources.FilterRelationTypeContains);
                    this.comboBoxCondition.Items.Add(Properties.Resources.FilterRelationTypeExcludes);
                    this.comboBoxCondition.Items.Add(Properties.Resources.FilterRelationTypeInRange);
                    break;
            }
            this.comboBoxCondition.SelectedIndex = 0;
        }

        private FilterAttributeType FilterAttributeTypeFromString(string s)
        {
            if (s == Properties.Resources.FilterAttributeTypeName)
            {
                return FilterAttributeType.Name;
            }
            else if (s == Properties.Resources.FilterAttributeTypeProvider)
            {
                return FilterAttributeType.Provider;
            }
            else if (s == Properties.Resources.FilterAttributeTypeNetworkName)
            {
                return FilterAttributeType.NetworkName;
            }
            else if (s == Properties.Resources.FilterAttributeTypeCASystemID)
            {
                return FilterAttributeType.CASystemID;
            }
            else if (s == Properties.Resources.FilterAttributeTypeFeatures)
            {
                return FilterAttributeType.Features;
            }
            else if (s == Properties.Resources.FilterAttributeTypePosition)
            {
                return FilterAttributeType.Position;
            }
            else if (s == Properties.Resources.FilterAttributeTypeLcn)
            {
                return FilterAttributeType.Lcn;
            }
            else if (s == Properties.Resources.FilterAttributeTypeFreeCAMode)
            {
                return FilterAttributeType.FreeCAMode;
            }
            else if (s == Properties.Resources.FilterAttributeTypeType)
            {
                return FilterAttributeType.Type;
            }
            else if (s == Properties.Resources.FilterAttributeTypePcr)
            {
                return FilterAttributeType.Pcr;
            }
            else if (s == Properties.Resources.FilterAttributeTypePmt)
            {
                return FilterAttributeType.Pmt;
            }
            else if (s == Properties.Resources.FilterAttributeTypeSid)
            {
                return FilterAttributeType.Sid;
            }
            else if (s == Properties.Resources.FilterAttributeTypeTsid)
            {
                return FilterAttributeType.Tsid;
            }
            else if (s == Properties.Resources.FilterAttributeTypeNid)
            {
                return FilterAttributeType.Nid;
            }
            else if (s == Properties.Resources.FilterAttributeTypeOnid)
            {
                return FilterAttributeType.Onid;
            }
            else if (s == Properties.Resources.FilterAttributeTypeBouquetList)
            {
                return FilterAttributeType.BouquetList;
            }
            else if (s == Properties.Resources.FilterAttributeTypeVideo)
            {
                return FilterAttributeType.Video;
            }
            else if (s == Properties.Resources.FilterAttributeTypeAudio)
            {
                return FilterAttributeType.Audio;
            }
            else
            {
                return FilterAttributeType.None;
            }
        }
    }
}
