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
    public partial class ServiceDiffForm : Form
    {
        private string file1 = "";
        private string file2 = "";

        public enum NetworkType
        {
            DVBS,
            DVBT,
            DVBC
        }

        NetworkType networktype = NetworkType.DVBS;

        public NetworkType networkType
        {
            get
            {
                return this.networktype;
            }
            set
            {
                this.networktype = value;
            }
        }

        public string File1
        {
            get
            {
                return this.file1;
            }
            set
            {
                this.file1 = value;
            }
        }

        public string File2
        {
            get
            {
                return this.file2;
            }
            set
            {
                this.file2 = value;
            }
        }

        public ServiceDiffForm()
        {
            InitializeComponent();
        }

        private void ServiceDiffForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.file1 = this.textBoxFile1.Text;
            this.file2 = this.textBoxFile2.Text;
            if((string)this.comboBoxNetworkType.SelectedItem=="DVB-S")
            {
                this.networktype = NetworkType.DVBS;
            }
            else if ((string)this.comboBoxNetworkType.SelectedItem == "DVB-T")
            {
                this.networktype = NetworkType.DVBT;
            }
            else if ((string)this.comboBoxNetworkType.SelectedItem == "DVB-C")
            {
                this.networktype = NetworkType.DVBC;
            }
        }

        private void ServiceDiffForm_Load(object sender, EventArgs e)
        {
            this.textBoxFile1.Text = this.file1;
            this.textBoxFile2.Text = this.file2;

            if (this.networktype == NetworkType.DVBS)
            {
                this.comboBoxNetworkType.SelectedItem = "DVB-S";
            }
            else if (this.networktype == NetworkType.DVBT)
            {
                this.comboBoxNetworkType.SelectedItem = "DVB-T";
            }
            else if (this.networktype == NetworkType.DVBC)
            {
                this.comboBoxNetworkType.SelectedItem = "DVB-C";
            }
            else
            {
                this.comboBoxNetworkType.SelectedItem = "DVB-S";
            }
        }

        private void buttonBrowse1_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.FileName = this.textBoxFile1.Text;
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                this.textBoxFile1.Text = dlg.FileName;
            }
        }

        private void buttonBrowse2_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.FileName = this.textBoxFile2.Text;
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                this.textBoxFile2.Text = dlg.FileName;
            }
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
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
