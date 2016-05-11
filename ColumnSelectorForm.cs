using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace dvbseserviceview
{
    public partial class ColumnSelectorForm : Form
    {
        private ColumnSettings columnsettings = null;
        public ColumnSelectorForm()
        {
            InitializeComponent();
        }

        public ColumnSettings Settings
        {
            get
            {
                return this.columnsettings;
            }
            set
            {
                this.columnsettings = value;
            }
        }

        private void ColumnSelectorForm_Load(object sender, EventArgs e)
        {
            this.checkBoxNo.Checked = this.columnsettings.Number;
            this.checkBoxName.Checked = this.columnsettings.Name;
            this.checkBoxProvider.Checked = this.columnsettings.Provider;
            this.checkBoxFrequency.Checked = this.columnsettings.Frequency;
            this.checkBoxPosition.Checked = this.columnsettings.Position;
            this.checkBoxNetwork.Checked = this.columnsettings.Network;
            this.checkBoxSid.Checked = this.columnsettings.Sid;
            this.checkBoxTsid.Checked = this.columnsettings.Tsid;
            this.checkBoxNid.Checked = this.columnsettings.Nid;
            this.checkBoxOnid.Checked = this.columnsettings.Onid;
            this.checkBoxVideo.Checked = this.columnsettings.Video;
            this.checkBoxAudio.Checked = this.columnsettings.Audio;
            this.checkBoxPmt.Checked = this.columnsettings.Pmt;
            this.checkBoxPcr.Checked = this.columnsettings.Pcr;
            this.checkBoxType.Checked = this.columnsettings.Type;
            this.checkBoxFreeCaMode.Checked = this.columnsettings.FreeCaMode;
            this.checkBoxCaSystemId.Checked = this.columnsettings.CaSystemId;
            this.checkBoxLcn.Checked = this.columnsettings.Lcn;
            this.checkBoxBouquet.Checked = this.columnsettings.Bouquet;
            this.checkBoxFeatures.Checked = this.columnsettings.Features;
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {

            this.columnsettings.Number = this.checkBoxNo.Checked ;
            this.columnsettings.Name = this.checkBoxName.Checked;
            this.columnsettings.Provider = this.checkBoxProvider.Checked;
            this.columnsettings.Frequency = this.checkBoxFrequency.Checked;
            this.columnsettings.Position = this.checkBoxPosition.Checked;
            this.columnsettings.Network = this.checkBoxNetwork.Checked;
            this.columnsettings.Sid = this.checkBoxSid.Checked;
            this.columnsettings.Tsid = this.checkBoxTsid.Checked;
            this.columnsettings.Nid = this.checkBoxNid.Checked;
            this.columnsettings.Onid = this.checkBoxOnid.Checked;
            this.columnsettings.Video = this.checkBoxVideo.Checked;
            this.columnsettings.Audio = this.checkBoxAudio.Checked;
            this.columnsettings.Pmt = this.checkBoxPmt.Checked;
            this.columnsettings.Pcr = this.checkBoxPcr.Checked;
            this.columnsettings.Type = this.checkBoxType.Checked;
            this.columnsettings.FreeCaMode = this.checkBoxFreeCaMode.Checked;
            this.columnsettings.CaSystemId = this.checkBoxCaSystemId.Checked;
            this.columnsettings.Lcn = this.checkBoxLcn.Checked;
            this.columnsettings.Bouquet = this.checkBoxBouquet.Checked;
            this.columnsettings.Features = this.checkBoxFeatures.Checked;
            this.DialogResult = DialogResult.OK;
            Close();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
