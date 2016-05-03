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
    public partial class ComparisonPreferencesForm : Form
    {
        ServiceDiffSettings diffSettings = null;

        public ServiceDiffSettings DiffSettings
        {
            set
            {
                this.diffSettings = value;
            }
        }

        public ComparisonPreferencesForm()
        {
            InitializeComponent();
        }

        private void ComparisonPreferencesForm_Load(object sender, EventArgs e)
        {
            this.checkBoxCompareCommonMuxOnly.Checked = this.diffSettings.OnlyCompareCommonMux;
            this.checkBoxName.Checked = this.diffSettings.Name;
            this.checkBoxProvider.Checked = this.diffSettings.Provider;
            this.checkBoxNetwork.Checked = this.diffSettings.Network;
            this.checkBoxONid.Checked = this.diffSettings.Onid;
            this.checkBoxVideo.Checked = this.diffSettings.Video;
            this.checkBoxAudio.Checked = this.diffSettings.Audio;
            this.checkBoxPmt.Checked = this.diffSettings.Pmt;
            this.checkBoxPcr.Checked = this.diffSettings.Pcr;
            this.checkBoxType.Checked = this.diffSettings.SericeType;
            this.checkBoxFreeCaMode.Checked = this.diffSettings.FreeCaMode;
            this.checkBoxCaSystemId.Checked = this.diffSettings.CaSystemId;
            this.checkBoxLcn.Checked = this.diffSettings.Lcn;
            this.checkBoxBouquet.Checked = this.diffSettings.Bouquet;
            this.checkBoxFeatures.Checked = this.diffSettings.Features;
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            this.diffSettings.OnlyCompareCommonMux = this.checkBoxCompareCommonMuxOnly.Checked;
            this.diffSettings.Name = this.checkBoxName.Checked;
            this.diffSettings.Provider = this.checkBoxProvider.Checked;
            this.diffSettings.Network = this.checkBoxNetwork.Checked;
            this.diffSettings.Onid = this.checkBoxONid.Checked;
            this.diffSettings.Video = this.checkBoxVideo.Checked;
            this.diffSettings.Audio = this.checkBoxAudio.Checked;
            this.diffSettings.Pmt = this.checkBoxPmt.Checked;
            this.diffSettings.Pcr = this.checkBoxPcr.Checked;
            this.diffSettings.SericeType = this.checkBoxType.Checked;
            this.diffSettings.FreeCaMode = this.checkBoxFreeCaMode.Checked;
            this.diffSettings.CaSystemId = this.checkBoxCaSystemId.Checked;
            this.diffSettings.Lcn = this.checkBoxLcn.Checked;
            this.diffSettings.Bouquet = this.checkBoxBouquet.Checked;
            this.diffSettings.Features = this.checkBoxFeatures.Checked;

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
