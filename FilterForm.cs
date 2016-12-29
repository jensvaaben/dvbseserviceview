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
            this.comboBoxAttribute.Items.Add(Properties.Resources.FilterAttributeTypeVideoType);
            this.comboBoxAttribute.Items.Add(Properties.Resources.FilterAttributeTypeAudio);
            this.comboBoxAttribute.Items.Add(Properties.Resources.FilterAttributeTypeAudioLanguage);
            this.comboBoxAttribute.Items.Add(Properties.Resources.FilterAttributeTypeAudioType);
            this.comboBoxAttribute.Items.Add(Properties.Resources.FilterAttributeTypeData);
            this.comboBoxAttribute.Items.Add(Properties.Resources.FilterAttributeTypeDataLanguage);
            this.comboBoxAttribute.Items.Add(Properties.Resources.FilterAttributeTypeDataType);

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
            filtercondition.filterAttributeType = FilterAttributeTypeFromString((string)this.comboBoxAttribute.SelectedItem);
            filtercondition.filterRelationType = FilterRelationTypeFromString((string)this.comboBoxCondition.SelectedItem);
            filtercondition.Value = this.textBoxValue.Text;
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
            else if (t == FilterAttributeType.Position) return Properties.Resources.FilterAttributeTypePosition;
            else if (t == FilterAttributeType.Lcn) return Properties.Resources.FilterAttributeTypeLcn;
            else if (t == FilterAttributeType.FreeCAMode) return Properties.Resources.FilterAttributeTypeFreeCAMode;
            else if (t == FilterAttributeType.Type) return Properties.Resources.FilterAttributeTypeType;
            else if (t == FilterAttributeType.Pcr) return Properties.Resources.FilterAttributeTypePcr;
            else if (t == FilterAttributeType.Pmt) return Properties.Resources.FilterAttributeTypePmt;
            else if (t == FilterAttributeType.Sid) return Properties.Resources.FilterAttributeTypeSid;
            else if (t == FilterAttributeType.Tsid) return Properties.Resources.FilterAttributeTypeTsid;
            else if (t == FilterAttributeType.Nid) return Properties.Resources.FilterAttributeTypeNid;
            else if (t == FilterAttributeType.Onid) return Properties.Resources.FilterAttributeTypeOnid;
            else if (t == FilterAttributeType.BouquetList) return Properties.Resources.FilterAttributeTypeBouquetList;
            else if (t == FilterAttributeType.Video) return Properties.Resources.FilterAttributeTypeVideo;
            else if (t == FilterAttributeType.Audio) return Properties.Resources.FilterAttributeTypeAudio;
            else if (t == FilterAttributeType.AudioLanguage) return Properties.Resources.FilterAttributeTypeAudioLanguage;
            else if (t == FilterAttributeType.AudioType) return Properties.Resources.FilterAttributeTypeAudioType;
            else if (t == FilterAttributeType.VideoType) return Properties.Resources.FilterAttributeTypeVideoType;
            else if (t == FilterAttributeType.Data) return Properties.Resources.FilterAttributeTypeData;
            else if (t == FilterAttributeType.DataLanguage) return Properties.Resources.FilterAttributeTypeDataLanguage;
            else if (t == FilterAttributeType.DataType) return Properties.Resources.FilterAttributeTypeDataType;
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
                case FilterAttributeType.Name: // single string fields
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
                case FilterAttributeType.CASystemID: //  comma separated list of integers 
                case FilterAttributeType.Video: // this is in most if not all cases just a single integer. But there could theoretically be more than one video stream 
                case FilterAttributeType.Audio: // Audio list also contain language. But for now we will only consider PID part.
                case FilterAttributeType.Data: // Data list also contain language. But for now we will only consider PID part.
                    this.comboBoxCondition.Items.Add(Properties.Resources.FilterRelationTypeContains);
                    this.comboBoxCondition.Items.Add(Properties.Resources.FilterRelationTypeInRange);
                    break;
                case FilterAttributeType.Features: //  comma separated list of strings 
                case FilterAttributeType.AudioLanguage:
                case FilterAttributeType.DataLanguage:
                    this.comboBoxCondition.Items.Add(Properties.Resources.FilterRelationTypeContains);
                    break;
                case FilterAttributeType.Lcn: // single integer fields
                case FilterAttributeType.FreeCAMode: // this is really a boolean
                case FilterAttributeType.Type:
                case FilterAttributeType.Pcr:
                case FilterAttributeType.Pmt:
                case FilterAttributeType.Sid:
                case FilterAttributeType.Tsid:
                case FilterAttributeType.Nid:
                case FilterAttributeType.Onid:
                    this.comboBoxCondition.Items.Add(Properties.Resources.FilterRelationTypeIs);
                    this.comboBoxCondition.Items.Add(Properties.Resources.FilterRelationTypeIsNot);
                    this.comboBoxCondition.Items.Add(Properties.Resources.FilterRelationTypeLessThan);
                    this.comboBoxCondition.Items.Add(Properties.Resources.FilterRelationTypeMoreThan);
                    break;
                case FilterAttributeType.AudioType:
                case FilterAttributeType.VideoType:
                case FilterAttributeType.DataType:
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
            else if (s == Properties.Resources.FilterAttributeTypeAudioLanguage)
            {
                return FilterAttributeType.AudioLanguage;
            }
            else if (s == Properties.Resources.FilterAttributeTypeAudioType)
            {
                return FilterAttributeType.AudioType;
            }
            else if (s == Properties.Resources.FilterAttributeTypeVideoType)
            {
                return FilterAttributeType.VideoType;
            }
            else if (s == Properties.Resources.FilterAttributeTypeData)
            {
                return FilterAttributeType.Data;
            }
            else if (s == Properties.Resources.FilterAttributeTypeDataLanguage)
            {
                return FilterAttributeType.DataLanguage;
            }
            else if (s == Properties.Resources.FilterAttributeTypeDataType)
            {
                return FilterAttributeType.DataType;
            }
            else
            {
                return FilterAttributeType.None;
            }
        }

        private FilterRelationType FilterRelationTypeFromString(string s)
        {
            if (s == Properties.Resources.FilterRelationTypeIs)
            {
               return FilterRelationType.Is;
            }
            else if (s == Properties.Resources.FilterRelationTypeIsNot)
            {
                return FilterRelationType.IsNot;
            }
            else if (s == Properties.Resources.FilterRelationTypeLessThan)
            {
                return FilterRelationType.LessThan;
            }
            else if (s == Properties.Resources.FilterRelationTypeMoreThan)
            {
                return FilterRelationType.MoreThan;
            }
            else if (s == Properties.Resources.FilterRelationTypeBeginsWith)
            {
                return FilterRelationType.BeginsWith;
            }
            else if (s == Properties.Resources.FilterRelationTypeEndsWith)
            {
                return FilterRelationType.EndsWith;
            }
            else if (s == Properties.Resources.FilterRelationTypeContains)
            {
                return FilterRelationType.Contains;
            }
            else if (s == Properties.Resources.FilterRelationTypeExcludes)
            {
                return FilterRelationType.Excludes;
            }
            else if (s == Properties.Resources.FilterRelationTypeInRange)
            {
                return FilterRelationType.InRange;
            }
            else
            {
                return FilterRelationType.None; // This should not happen
            }
        }
    }
}
