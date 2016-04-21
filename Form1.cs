using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.IO;
using System.Diagnostics;
using System.Configuration;

namespace dvbseserviceview
{
    
    public partial class Form1 : Form
    {
        enum NetworkType
        {
            UNKNOWN,
            DVBS,
            DVBT,
            DVBC
        }

        enum TreeViewNodeType
        {
            ServicesRoot,
            PositionsRoot,
            OneMuxByPosition,
            Position,
            NetworksRoot,
            Network,
            ProvidersRoot,
            Provider,
            AlphabeticRoot,
            Letter,
            TransportStreamNid,
            TransportStreamOnid,
            TransportStreamNetworkname,
            NetoworkNamesRoot,
            NetworkName,
            OriginalNetworkId,
            OriginalNetworkIdRoot,
            Bouquet
        }

        struct BouquetKey
        {
            public int id;
            public string name;
        }

        class BouquetKeyComparer : Comparer<BouquetKey>
        {
            public override int Compare(BouquetKey k1, BouquetKey k2)
            {
                return k1.id.CompareTo(k2.id);
            }
        }

        struct FrequencyKey
        {
            public int frequency;
            public string polarity;
        }

        class FrequencyKeyComparer : Comparer<FrequencyKey>
        {
            public override int Compare(FrequencyKey k1, FrequencyKey k2)
            {
                if (k1.frequency != k2.frequency) return k1.frequency.CompareTo(k2.frequency);
                else return k1.polarity.CompareTo(k2.polarity);
            }
        }

        class TreeViewContext
        {
            TreeViewNodeType nodetype = TreeViewNodeType.ServicesRoot;
            char letter = '\0';
            int tsid = -1;
            string name = ""; // General purpose string value. Depends on context.
            string string1 = "";
            int int1 = -1;

            public int Int1
            {
                get
                {
                    return this.int1;
                }
                set
                {
                    this.int1 = value;
                }
            }
            public string String1
            {
                get
                {
                    return this.string1;
                }
                set
                {
                    this.string1 = value;
                }
            }
            public string Name
            {
                get
                {
                    return this.name;
                }
                set
                {
                    this.name = value;
                }
            }
            public int TsId
            {
                get
                {
                    return this.tsid;
                }
                set
                {
                    this.tsid = value;
                }
            }
            public TreeViewNodeType NodeType
            {
                get
                {
                    return this.nodetype;
                }
                set
                {
                    this.nodetype = value;
                }
            }
            public char Letter
            {
                get
                {
                    return this.letter;
                }
                set
                {
                    this.letter = value;
                }
            }
        }

        FilterContext filterContext = new FilterContext();

        public Form1()
        {
            InitializeComponent();

            ImageList imageListSmall = new ImageList();
            imageListSmall.Images.Add(Properties.Resources.video);
            imageListSmall.Images.Add(Properties.Resources.audioser);
            imageListSmall.Images.Add(Properties.Resources.dataserv);
            this.listView1.SmallImageList = imageListSmall;

            LoadFilterXML();

            //initialize column header width
            // no validation of array sizes.
            if (Properties.Settings.Default.DVBSColumnHeaderWidth.Count() > 0)
            {
                string[] columnwidth = Properties.Settings.Default.DVBSColumnHeaderWidth.Split(new char[1] { ',' });
                for (int n = 0; n < columnwidth.Count(); n++)
                {
                    this.dvbscolumn[n] = Convert.ToInt32(columnwidth[n]);
                }
            }
            if (Properties.Settings.Default.DVBTColumnHeaderWidth.Count() > 0)
            {
                string[] columnwidth = Properties.Settings.Default.DVBTColumnHeaderWidth.Split(new char[1] { ',' });
                for (int n = 0; n < columnwidth.Count(); n++)
                {
                    this.dvbtcolumn[n] = Convert.ToInt32(columnwidth[n]);
                }
            }
            if (Properties.Settings.Default.DVBCColumnHeaderWidth.Count() > 0)
            {
                string[] columnwidth = Properties.Settings.Default.DVBCColumnHeaderWidth.Split(new char[1] { ',' });
                for (int n = 0; n < columnwidth.Count(); n++)
                {
                    this.dvbccolumn[n] = Convert.ToInt32(columnwidth[n]);
                }
            }
            if (Properties.Settings.Default.EITColumnHeaderWidth.Count() > 0)
            {
                string[] columnwidth = Properties.Settings.Default.EITColumnHeaderWidth.Split(new char[1] { ',' });
                for (int n = 0; n < columnwidth.Count(); n++)
                {
                    this.eitcolumn[n] = Convert.ToInt32(columnwidth[n]);
                }
            }

            if(this.eitcolumn.Count()==this.listViewEIT.Columns.Count)
            {
                for (int n = 0; n < eitcolumn.Count(); n++)
                {
                    this.listViewEIT.Columns[n].Width = this.eitcolumn[n];
                }
            }

            // intialize filter condition
            this.filterContext.Exclude = Properties.Settings.Default.FilterOptionExclude;

        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.FileName = Properties.Settings.Default.ServiceFileDVBS;
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                this.oldnetworktype = this.networktype;
                this.networktype = NetworkType.DVBS;
                Properties.Settings.Default.ServiceFileDVBS = dlg.FileName;
                Properties.Settings.Default.Save();
                LoadServiceFile(dlg.FileName,NetworkType.DVBS);
            }
        }

        List<Service> servicelist = new List<Service>();
        List<Service> servicelistfiltered = new List<Service>();

        private int[] dvbscolumn = new int[] { 70, 100, 100, 100, 100, 100, 100, 100, 100, 100, 100, 100, 100, 100, 100, 100, 100, 100, 100, 100 };
        private int[] dvbtcolumn = new int[] { 70, 100, 100, 100, 100, 100, 100, 100, 100, 100, 100, 100, 100, 100, 100, 100, 100, 100, 100 };
        private int[] dvbccolumn = new int[] { 70, 100, 100, 100, 100, 100, 100, 100, 100, 100, 100, 100, 100, 100, 100, 100, 100, 100, 100 };
        private int[] eitcolumn = new int[] {50, 50, 50, 50, 50, 50, 150, 150, 150, 150, 150 };

        private void LoadServiceFile(string file, NetworkType networktype)
        {
            // save column state
            if (this.listView1.Columns.Count > 0)
            {
                if (oldnetworktype == NetworkType.DVBS)
                {
                    for (int n = 0; n < this.listView1.Columns.Count; n++)
                    {
                        dvbscolumn[n] = this.listView1.Columns[n].Width;
                    }
                }
                else if (oldnetworktype == NetworkType.DVBT)
                {
                    for (int n = 0; n < this.listView1.Columns.Count; n++)
                    {
                        dvbtcolumn[n] = this.listView1.Columns[n].Width;
                    }
                }
                else if (oldnetworktype == NetworkType.DVBC)
                {
                    for (int n = 0; n < this.listView1.Columns.Count; n++)
                    {
                        dvbccolumn[n] = this.listView1.Columns[n].Width;
                    }
                }
            }

            this.treeView1.Nodes.Clear();
            this.listView1.Clear();
            this.servicelist.Clear();

            using (System.IO.Stream f = new FileStream(file, FileMode.Open))
            {
                XmlReader reader = new XmlTextReader(f);
                XmlDocument doc = new XmlDocument();
                doc.Load(reader);

                this.servicelist = new System.Collections.Generic.List<Service>();
                int number = 0;
                foreach (var service in doc["services"].ChildNodes)
                {
                    Service s = new Service();
                    s.No = number++;
                    ExtractService((XmlNode)service, s, networktype);
                    servicelist.Add(s);
                }
                this.filterContext.ApplyFilter(this.servicelist, this.servicelistfiltered);
                AddDvbColoumns(networktype);
                CreateTree();
                BuildIdx();
                UpdateTreeView();
                this.treeView1.SelectedNode = this.root;
            }
        }

        private void ExtractService(XmlNode l, Service s, NetworkType network)
        {
            if (network == NetworkType.DVBS)
            {
                s.Position = l.Attributes["position"].Value;
                s.DvbSTuner.RollOff = Convert.ToInt32(l.Attributes["roll_off"].Value);
                s.DvbSTuner.ModulationType = Convert.ToInt32(l.Attributes["modulation_type"].Value);
                s.DvbSTuner.ModulationSystem = Convert.ToInt32(l.Attributes["modulation_system"].Value);
                s.DvbSTuner.Fec = l.Attributes["fec"].Value;
                s.DvbSTuner.Symbolrate = Convert.ToInt32(l.Attributes["symbolrate"].Value);
                s.DvbSTuner.Polarity = l.Attributes["polarity"].Value;
                s.DvbSTuner.Frequency = Convert.ToInt32(l.Attributes["frequency"].Value);
                s.DvbSTuner.Position = l.Attributes["position"].Value;
            }
            else //DVB-C or DVB-T
            {
                s.DvbCTTuner.Frequency = Convert.ToInt32(l.Attributes["frequency"].Value);
            }

            s.Name = l.Attributes["name"].Value;
            s.FreeCaMode = l.Attributes["free_ca_mode"].Value == "1";
            s.Type = Convert.ToInt32(l.Attributes["type"].Value);
            s.Pcr = Convert.ToInt32(l.Attributes["pcr"].Value);
            s.Pmt = Convert.ToInt32(l.Attributes["pmt"].Value);
            s.Sid = Convert.ToInt32(l.Attributes["sid"].Value);
            s.Tsid = Convert.ToInt32(l.Attributes["tsid"].Value);
            s.Nid = Convert.ToInt32(l.Attributes["nid"].Value);
            s.Onid = Convert.ToInt32(l.Attributes["onid"].Value);
            s.Provider = l.Attributes["provider"].Value;
            s.NetworkName = l.Attributes["network_name"].Value; ;
            s.MuxId = Convert.ToInt32(l.Attributes["muxid"].Value);

            foreach (var item in l["ca_list"].ChildNodes)
            {
                CA ca = new CA();
                ExtractCA((XmlNode)item, ca);
                s.CAList.Add(ca);
            }

            foreach (var item in l["streams"])
            {
                Stream stream = new Stream();
                ExtractStream((XmlNode)item, stream);
                s.Streams.Add(stream);
            }

            foreach (var item in l["bouquet_list"])
            {
                Bouquet bouquet = new Bouquet();
                ExctractBouquet((XmlNode)item, bouquet);
                s.BouquetList.Add(bouquet);
            }
            s.Update();
        }

        private void ExtractCA(XmlNode l, CA ca)
        {
            ca.Pid = Convert.ToInt32(l.Attributes["CA_PID"].Value);
            ca.SystemId = Convert.ToInt32(l.Attributes["CA_system_ID"].Value);

            if (l.Attributes["private_bytes"].Value != string.Empty)
            {
                ca.PrivateBytes = ExtractBytes(l.Attributes["private_bytes"].Value);
            }
        }

        private byte[] ExtractBytes(string s)
        {
            if (s.Length % 2 != 0) throw new Exception("hex string must be even number of bytes");
            byte[] val = new byte[s.Length / 2];
            for (int n = 0; n < s.Length / 2; n++)
            {
                string hexbyte = s.Substring(n * 2, 2);
                val[n] = byte.Parse(hexbyte, System.Globalization.NumberStyles.HexNumber);
            }

            return val;
        }

        private void ExtractStream(XmlNode l, Stream s)
        {
            s.Type = Convert.ToInt32(l.Attributes["type"].Value);
            s.Type2 = l.Attributes["type2"].Value;
            s.Pid = Convert.ToInt32(l.Attributes["pid"].Value);
            if(l.Attributes["language"]!=null)
            {
                s.Language = l.Attributes["language"].Value;
            }

            if (l.Attributes["application_name"] != null)
            {
                s.ApplicationName = l.Attributes["application_name"].Value;
            }

            foreach (var item in l["ca_list"].ChildNodes)
            {
                CA ca = new CA();
                ExtractCA((XmlNode)item, ca);
                s.CAList.Add(ca);
            }
        }

        private void ExctractBouquet(XmlNode l, Bouquet b)
        {
            b.Id = Convert.ToInt32(l.Attributes["id"].Value);
            b.Name = l.Attributes["name"].Value;
        }

        TreeNode root = null;
        TreeNode position = null;
        TreeNode network = null;
        TreeNode original_network = null;
        TreeNode network_name = null;
        TreeNode provider = null;
        TreeNode bouquet = null;
        TreeNode alphabetic = null;

        private void CreateTree()
        {
            this.root = null;
            this.position = null;
            this.network = null;
            this.original_network = null;
            this.network_name = null;
            this.provider = null;
            this.bouquet = null;
            this.alphabetic = null;
            TreeViewContext context = null;

            this.root = this.treeView1.Nodes.Add("DVB Services");
            context = new TreeViewContext();
            context.NodeType = TreeViewNodeType.ServicesRoot;
            this.root.Tag = context;

            if (this.networktype == NetworkType.DVBS)
            {
                this.position = root.Nodes.Add("By Position");
                context = new TreeViewContext();
                context.NodeType = TreeViewNodeType.PositionsRoot;
                this.position.Tag = context;
            }

            this.network = root.Nodes.Add("By Network ID");
            context = new TreeViewContext();
            context.NodeType = TreeViewNodeType.NetworksRoot;
            this.network.Tag = context;

            this.original_network = root.Nodes.Add("By Original Network ID");
            context = new TreeViewContext();
            context.NodeType = TreeViewNodeType.OriginalNetworkIdRoot;
            this.original_network.Tag = context;

            this.network_name = root.Nodes.Add("By Network Name");
            context = new TreeViewContext();
            context.NodeType = TreeViewNodeType.NetoworkNamesRoot;
            this.network_name.Tag = context;

            this.provider = root.Nodes.Add("By Provider");
            context = new TreeViewContext();
            context.NodeType = TreeViewNodeType.ProvidersRoot;
            this.provider.Tag = context;

            this.bouquet = root.Nodes.Add("By Bouquet");
            context = new TreeViewContext();
            context.NodeType = TreeViewNodeType.NetoworkNamesRoot;
            this.bouquet.Tag = context;

            this.alphabetic = root.Nodes.Add("Alphabetic");
            context = new TreeViewContext();
            context.NodeType = TreeViewNodeType.AlphabeticRoot;
            this.alphabetic.Tag = context;

            // add letters
            for (char n = 'A'; n <= 'Z'; n++)
            {
                TreeNode tmp = alphabetic.Nodes.Add(Convert.ToString(n));
                context = new TreeViewContext();
                context.NodeType = TreeViewNodeType.Letter;
                context.Letter = n;
                tmp.Tag = context;
            }
            // add digits
            for (char n = '0'; n <= '9'; n++)
            {
                TreeNode tmp = alphabetic.Nodes.Add(Convert.ToString(n));
                context = new TreeViewContext();
                context.NodeType = TreeViewNodeType.Letter;
                context.Letter = n;
                tmp.Tag = context;
            }
            // add ohter
            TreeNode tmp1 = alphabetic.Nodes.Add("Other");
            context = new TreeViewContext();
            context.NodeType = TreeViewNodeType.Letter;
            context.Letter = '%';
            tmp1.Tag = context;

        }

        private void AddDvbColoumns(NetworkType networktype)
        {
            if (networktype == NetworkType.DVBS)
            {
                this.listView1.Columns.Add("No.", 30, HorizontalAlignment.Left);
                this.listView1.Columns.Add("Name", 100, HorizontalAlignment.Left);
                this.listView1.Columns.Add("Provider", 100, HorizontalAlignment.Left);
                this.listView1.Columns.Add("Frequency", 100, HorizontalAlignment.Left);
                this.listView1.Columns.Add("Position", 100, HorizontalAlignment.Left);
            }
            else //DVB-T or DVB-C
            {
                this.listView1.Columns.Add("No.", 30, HorizontalAlignment.Left);
                this.listView1.Columns.Add("Name", 100, HorizontalAlignment.Left);
                this.listView1.Columns.Add("Provider", 100, HorizontalAlignment.Left);
                this.listView1.Columns.Add("Frequency", 100, HorizontalAlignment.Left);
            }

            this.listView1.Columns.Add("Network", 100, HorizontalAlignment.Left);
            this.listView1.Columns.Add("SID", 100, HorizontalAlignment.Left);
            this.listView1.Columns.Add("TSID", 100, HorizontalAlignment.Left);
            this.listView1.Columns.Add("NID", 100, HorizontalAlignment.Left);
            this.listView1.Columns.Add("ONID", 100, HorizontalAlignment.Left);
            this.listView1.Columns.Add("Video", 100, HorizontalAlignment.Left);
            this.listView1.Columns.Add("Audio", 100, HorizontalAlignment.Left);
            this.listView1.Columns.Add("PMT", 100, HorizontalAlignment.Left);
            this.listView1.Columns.Add("PCR", 100, HorizontalAlignment.Left);
            this.listView1.Columns.Add("Type", 100, HorizontalAlignment.Left);
            this.listView1.Columns.Add("free_CA_mode", 100, HorizontalAlignment.Left);
            this.listView1.Columns.Add("CA_system_ID", 100, HorizontalAlignment.Left);
            this.listView1.Columns.Add("lcn", 100, HorizontalAlignment.Left);
            this.listView1.Columns.Add("bouquet", 100, HorizontalAlignment.Left);
            this.listView1.Columns.Add("features", 100, HorizontalAlignment.Left);

            //adjust column width
            if (networktype == NetworkType.DVBS)
            {
                for (int n = 0; n < this.dvbscolumn.Count(); n++)
                {
                    this.listView1.Columns[n].Width = this.dvbscolumn[n];
                }
            }
            else if (networktype == NetworkType.DVBT)
            {
                for (int n = 0; n < this.dvbtcolumn.Count(); n++)
                {
                    this.listView1.Columns[n].Width = this.dvbtcolumn[n];
                }
            }
            else if (networktype == NetworkType.DVBC)
            {
                for (int n = 0; n < this.dvbccolumn.Count(); n++)
                {
                    this.listView1.Columns[n].Width = this.dvbccolumn[n];
                }
            }

        }

        private void UpdateList(List<Service> l, NetworkType networktype)
        {
            foreach (var service in l)
            {
                AddService(service, networktype);
            }
        }

        private void AddService(Service service, NetworkType networktype)
        {
            ListViewItem i = new ListViewItem();
            i.ImageIndex = GetServiceTypeImage(service.Type);
            i.Text = Convert.ToString(service.No);
            i.SubItems.Add(service.Name);
            i.SubItems.Add(service.Provider);

            if (networktype == NetworkType.DVBS)
            {
                i.SubItems.Add(GetTunerString(service.DvbSTuner));
                i.SubItems.Add(service.DvbSTuner.Position);
            }
            else  //DVB-T or DVB-C
            {
                i.SubItems.Add(GetTunerString(service.DvbCTTuner));
            }

            AddDvbValues(service, i);
            this.listView1.Items.Add(i);
        }

        private void AddDvbValues(Service service, ListViewItem i)
        {
            i.SubItems.Add(service.NetworkName);
            i.SubItems.Add(Convert.ToString(service.Sid));
            i.SubItems.Add(Convert.ToString(service.Tsid));
            i.SubItems.Add(Convert.ToString(service.Nid));
            i.SubItems.Add(Convert.ToString(service.Onid));
            i.SubItems.Add(service.VideoPidListString); //VPID
            i.SubItems.Add(service.AudioPidListString); //APID
            i.SubItems.Add(Convert.ToString(service.Pmt));
            i.SubItems.Add(Convert.ToString(service.Pcr));
            i.SubItems.Add(Convert.ToString(service.Type));
            i.SubItems.Add(service.FreeCaMode ? "1" : "0");
            i.SubItems.Add(service.CaSystemIdListString); //CA_system_ID
            i.SubItems.Add(service.Lcn!=-1 ? Convert.ToString(service.Lcn) : "n/a");
            i.SubItems.Add(service.BouquetListString); //bouquet
            i.SubItems.Add(service.FeatureList); //features
        }

        private int GetServiceTypeImage(int type)
        {
            switch (type)
            {
                case 0x01: // digital television service (see note 1)
                case 0x04: // NVOD reference service (see note 1)
                case 0x05: // NVOD time-shifted service (see note 1)
                case 0x11: // MPEG-2 HD digital television service
                case 0x16: // advanced codec SD digital television service
                case 0x17: // advanced codec SD NVOD time-shifted service
                case 0x18: // advanced codec SD NVOD reference service
                case 0x19: // advanced codec HD digital television service
                case 0x1a: // advanced codec HD NVOD time-shifted service
                case 0x1b: // advanced codec HD NVOD reference service
                case 0x1c: // advanced codec frame compatible plano-stereoscopic HD digital television service (see note 3)
                case 0x1d: // advanced codec frame compatible plano-stereoscopic HD NVOD time-shifted service (see note 3)
                case 0x1e: // advanced codec frame compatible plano-stereoscopic HD NVOD reference service (see note 3)
                    return 0;
                case 0x02: // digital radio sound service (see note 2)
                case 0x07: // FM radio service
                case 0x0a: // advanced codec digital radio sound service
                    return 1;
                default:
                    return 2;
            }
        }

        NetworkType networktype = NetworkType.UNKNOWN;
        NetworkType oldnetworktype = NetworkType.UNKNOWN;

        private void openDVBCToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.FileName = Properties.Settings.Default.ServiceFileDVBC;
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                this.oldnetworktype = this.networktype;
                this.networktype = NetworkType.DVBC;
                Properties.Settings.Default.ServiceFileDVBC = dlg.FileName;
                Properties.Settings.Default.Save();
                LoadServiceFile(dlg.FileName, NetworkType.DVBC);
            }
        }

        private void openDVBTToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.FileName = Properties.Settings.Default.ServiceFileDVBT;
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                this.oldnetworktype = this.networktype;
                this.networktype = NetworkType.DVBT;
                Properties.Settings.Default.ServiceFileDVBT = dlg.FileName;
                Properties.Settings.Default.Save();
                LoadServiceFile(dlg.FileName, NetworkType.DVBT);
            }
        }

        SortedDictionary<string, List<Service>> provideridx = new SortedDictionary<string, List<Service>>();
        SortedDictionary<string, SortedDictionary<int, List<Service>>> networknameidx = new SortedDictionary<string, SortedDictionary<int, List<Service>>>();
        SortedDictionary<int, SortedDictionary<int, List<Service>>> networkidx = new SortedDictionary<int, SortedDictionary<int, List<Service>>>();
        SortedDictionary<int, SortedDictionary<int, List<Service>>> originalnetworkidx = new SortedDictionary<int, SortedDictionary<int, List<Service>>>();
        SortedDictionary<BouquetKey, List<Service>> bouquetidx = new SortedDictionary<BouquetKey, List<Service>>(new BouquetKeyComparer());
        SortedDictionary<string, SortedDictionary<FrequencyKey, List<Service>>> satnameidx = new SortedDictionary<string, SortedDictionary<FrequencyKey, List<Service>>>();
        Dictionary<char, List<Service>> alphabeticidx = new Dictionary<char,List<Service>>();
        List<Service> allalphabeticidx = new List<Service>();

        private void BuildIdx()
        {
            // clear all 
            provideridx.Clear();
            networknameidx.Clear();
            networkidx.Clear();
            originalnetworkidx.Clear();
            bouquetidx.Clear();
            satnameidx.Clear();
            alphabeticidx.Clear();
            allalphabeticidx.Clear();

            //provideridx
            BuildProviderIdx(this.servicelistfiltered, this.provideridx);
            //nidtsidx
            BuildNetworkIdx(this.servicelistfiltered, this.networkidx);
            //onidtsidx
            BuildOriginalNetworkIdx(this.servicelistfiltered, this.originalnetworkidx);
            //networknameidx
            BuildNetworkNameIdx(this.servicelistfiltered, this.networknameidx);
            //satnameidx
            BuildSatNameIdx(this.servicelistfiltered, this.satnameidx);
            //alphabeticidx
            BuildAlphabeticIdx(this.servicelistfiltered, this.alphabeticidx);
            //allalphabeticidx
            BuildAllAlphabetic(this.servicelistfiltered, this.allalphabeticidx);
            //bouquetidx
            BuildBouquetIdx(this.servicelistfiltered, this.bouquetidx);
        }

        private void BuildProviderIdx(List<Service> s,SortedDictionary<string, List<Service>> idx)
        {
            foreach (var service in s)
            {
                if(!idx.Keys.Contains(service.Provider))
                {
                    List<Service> servicelist = new List<Service>();
                    idx.Add(service.Provider, servicelist);
                }
                idx[service.Provider].Add(service);
            }
        }

        private void BuildNetworkNameIdx(List<Service> s, SortedDictionary<string, SortedDictionary<int, List<Service>>> idx)
        {
            foreach (var service in s)
            {
                if(!idx.Keys.Contains(service.NetworkName))
                {
                    SortedDictionary<int, List<Service>> network = new SortedDictionary<int, List<Service>>();
                    idx.Add(service.NetworkName, network);
                }
                if (!idx[service.NetworkName].Keys.Contains(service.Tsid))
                {
                    List<Service> servicelist = new List<Service>();
                    idx[service.NetworkName].Add(service.Tsid, servicelist);
                }
                idx[service.NetworkName][service.Tsid].Add(service);
            }
        }

        private void BuildNetworkIdx(List<Service> s, SortedDictionary<int, SortedDictionary<int, List<Service>>> idx)
        {
            foreach (var service in s)
            {
                if(!idx.Keys.Contains(service.Nid))
                {
                    SortedDictionary<int, List<Service>> network = new SortedDictionary<int, List<Service>>();
                    idx.Add(service.Nid, network);
                }
                if (!idx[service.Nid].Keys.Contains(service.Tsid))
                {
                    List<Service> servicelist = new List<Service>();
                    idx[service.Nid].Add(service.Tsid, servicelist);
                }
                idx[service.Nid][service.Tsid].Add(service);
            }
        }

        private void BuildOriginalNetworkIdx(List<Service> s, SortedDictionary<int, SortedDictionary<int, List<Service>>> idx)
        {
            foreach (var service in s)
            {
                if (!idx.Keys.Contains(service.Onid))
                {
                    SortedDictionary<int, List<Service>> originalnetwork = new SortedDictionary<int, List<Service>>();
                    idx.Add(service.Onid, originalnetwork);
                }
                if (!idx[service.Onid].Keys.Contains(service.Tsid))
                {
                    List<Service> servicelist = new List<Service>();
                    idx[service.Onid].Add(service.Tsid, servicelist);
                }
                idx[service.Onid][service.Tsid].Add(service);
            }
        }

        private void BuildBouquetIdx(List<Service> s, SortedDictionary<BouquetKey, List<Service>> idx)
        {
            foreach (var service in s)
            {
                foreach (var bouquet in service.BouquetList)
                {
                    BouquetKey key = new BouquetKey();
                    key.id = bouquet.Id;
                    key.name = bouquet.Name;
                    if (!idx.Keys.Contains(key))
                    {
                        List<Service> servicelist = new List<Service>();
                        idx.Add(key, servicelist);
                    }
                    idx[key].Add(service);
                }
            }
        }

        private void BuildSatNameIdx(List<Service> s, SortedDictionary<string, SortedDictionary<FrequencyKey, List<Service>>> idx)
        {
            foreach (var service in s)
            {
                if(!idx.Keys.Contains(service.Position))
                {
                    SortedDictionary<FrequencyKey, List<Service>> position = new SortedDictionary<FrequencyKey, List<Service>>(new FrequencyKeyComparer());
                    idx.Add(service.Position, position);
                }
                FrequencyKey key = new FrequencyKey();
                key.frequency = service.DvbSTuner.Frequency;
                key.polarity = service.DvbSTuner.Polarity;
                if (!idx[service.Position].Keys.Contains(key))
                {
                    List<Service> servicelist = new List<Service>();
                    idx[service.Position].Add(key, servicelist);
                }
                idx[service.Position][key].Add(service);
            }
        }

        private void BuildAlphabeticIdx(List<Service> s, Dictionary<char, List<Service>> idx)
        {
            foreach (var service in s)
            {
                char key;
                if (service.Name.Length > 0)
                {
                    key = service.Name[0];
                    key = char.ToUpper(key);
                    if (!((key >= 'A' && key <= 'Z') || (key >= '0' && key <= '9')))
                    {
                        key = '%';
                    }
                }
                else
                {
                    key = '%';
                }
                if (!idx.Keys.Contains(key))
                {
                    List<Service> servicelist = new List<Service>();
                    idx.Add(key, servicelist);
                }
                idx[key].Add(service);
            }

            foreach (var key in idx.Keys)
            {
                idx[key].Sort(CompareServiceByName);
            }
        }

        private static int CompareServiceByName(Service s1, Service s2)
        {
            return s1.Name.CompareTo(s2.Name);
        }

        private void BuildAllAlphabetic(List<Service> s, List<Service> idx)
        {
            foreach (var service in s)
            {
                idx.Add(service);
            }
            idx.Sort(CompareServiceByName);
        }

        private void UpdateTreeView()
        {
            //provider
            foreach (var provider in this.provideridx.Keys)
            {
                TreeNode node = this.provider.Nodes.Add(provider);
                TreeViewContext treeviewcontext = new TreeViewContext();
                treeviewcontext.NodeType = TreeViewNodeType.Provider;
                treeviewcontext.Name = provider;
                node.Tag = treeviewcontext;
            }
            //networkname
            foreach (var networkname in this.networknameidx.Keys)
            {
                TreeNode node = this.network_name.Nodes.Add(networkname);
                TreeViewContext treeviewcontext = new TreeViewContext();
                treeviewcontext.NodeType = TreeViewNodeType.NetworkName;
                treeviewcontext.Name = networkname;
                node.Tag = treeviewcontext;
                UpdateTsView(node, networkname, TreeViewNodeType.TransportStreamNetworkname, this.networknameidx[networkname]);
            }
            //network
            foreach (var network in this.networkidx.Keys)
            {
                TreeNode node = this.network.Nodes.Add(string.Format("0x{0:x} ({0})",network));
                TreeViewContext treeviewcontext = new TreeViewContext();
                treeviewcontext.NodeType = TreeViewNodeType.Network;
                treeviewcontext.Int1 = network;
                node.Tag = treeviewcontext;
                UpdateTsView(node, network, TreeViewNodeType.TransportStreamNid, this.networkidx[network]);
            }
            //original network
            foreach (var originalnetwork in this.originalnetworkidx.Keys)
            {
                TreeNode node = this.original_network.Nodes.Add(string.Format("0x{0:x} ({0})", originalnetwork));
                TreeViewContext treeviewcontext = new TreeViewContext();
                treeviewcontext.NodeType = TreeViewNodeType.OriginalNetworkId;
                treeviewcontext.Int1 = originalnetwork;
                node.Tag = treeviewcontext;
                UpdateTsView(node, originalnetwork, TreeViewNodeType.TransportStreamOnid, this.originalnetworkidx[originalnetwork]);
            }
            //bouquet
            foreach (var bouquet in this.bouquetidx.Keys)
            {
                TreeNode node = this.bouquet.Nodes.Add(string.Format("0x{0:x} ({0}) - {1}",bouquet.id,bouquet.name));
                TreeViewContext treeviewcontext = new TreeViewContext();
                treeviewcontext.NodeType = TreeViewNodeType.Bouquet;
                treeviewcontext.Int1 = bouquet.id;
                treeviewcontext.Name = bouquet.name;
                node.Tag = treeviewcontext;
            }
            //satname
            if (this.networktype == NetworkType.DVBS)
            {
                foreach (var position in this.satnameidx.Keys)
                {
                    TreeNode node = this.position.Nodes.Add(position);
                    TreeViewContext treeviewcontext = new TreeViewContext();
                    treeviewcontext.NodeType = TreeViewNodeType.Position;
                    treeviewcontext.Name = position;
                    node.Tag = treeviewcontext;
                    UpdateTsView(node, position, TreeViewNodeType.OneMuxByPosition, this.satnameidx[position]);
                }
            }
            //alphabetic
            foreach (var letter in this.alphabeticidx.Keys)
            {
                //nothing to do here
            }
        }

        private void UpdateTsView(TreeNode parent,string name, TreeViewNodeType type,SortedDictionary<int, List<Service>> ts)
        {
            foreach(var tsitem in ts.Keys)
            {
                TreeNode node = null;
                if (this.networktype == NetworkType.DVBC || this.networktype == NetworkType.DVBT)
                {
                    node = parent.Nodes.Add(string.Format("0x{0:x} ({0}) - {1}", tsitem, ts[tsitem].First().DvbCTTuner.Frequency));
                }
                else if (this.networktype == NetworkType.DVBS)
                {
                    node = parent.Nodes.Add(string.Format("0x{0:x} ({0}) - {1}", tsitem, ts[tsitem].First().DvbSTuner.Frequency));
                }
                TreeViewContext treeviewcontext = new TreeViewContext();
                treeviewcontext.NodeType = type;
                treeviewcontext.Name = name;
                treeviewcontext.TsId = ts[tsitem].First().Tsid;
                node.Tag = treeviewcontext;
            }
        }

        private void UpdateTsView(TreeNode parent, int id, TreeViewNodeType type, SortedDictionary<int, List<Service>> ts)
        {
            foreach (var tsitem in ts.Keys)
            {
                TreeNode node = null;
                if (this.networktype == NetworkType.DVBC || this.networktype == NetworkType.DVBT)
                {
                    node = parent.Nodes.Add(string.Format("0x{0:x} ({0}) - {1}", tsitem, ts[tsitem].First().DvbCTTuner.Frequency));
                }
                else if (this.networktype == NetworkType.DVBS)
                {
                    node = parent.Nodes.Add(string.Format("0x{0:x} ({0}) - {1}", tsitem, ts[tsitem].First().DvbSTuner.Frequency));
                }
                TreeViewContext treeviewcontext = new TreeViewContext();
                treeviewcontext.NodeType = type;
                treeviewcontext.Int1 = id;
                treeviewcontext.TsId = ts[tsitem].First().Tsid;
                node.Tag = treeviewcontext;
            }
        }

        private void UpdateTsView(TreeNode parent, string position, TreeViewNodeType type, SortedDictionary<FrequencyKey, List<Service>> ts)
        {
            foreach (var tsitem in ts.Keys)
            {
                TreeNode node = parent.Nodes.Add(GetTunerString(ts[tsitem].First().DvbSTuner));
                TreeViewContext treeviewcontext = new TreeViewContext();
                treeviewcontext.NodeType = type;
                treeviewcontext.Name = position;
                treeviewcontext.Int1 = tsitem.frequency;
                treeviewcontext.String1 = tsitem.polarity;
                node.Tag = treeviewcontext;
            }
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            TreeNode selected = this.treeView1.SelectedNode;
            if (selected != null)
            {
                this.listView1.Items.Clear();
                TreeViewContext context = (TreeViewContext)selected.Tag;
                if (context.NodeType == TreeViewNodeType.ServicesRoot)
                {
                    UpdateList(this.servicelistfiltered, this.networktype);
                }
                else if (context.NodeType == TreeViewNodeType.Provider)
                {
                    UpdateList(this.provideridx[context.Name], this.networktype);
                }
                else if (context.NodeType == TreeViewNodeType.NetworkName)
                {
                    foreach (var tsitem in this.networknameidx[context.Name].Keys)
                    {
                        foreach (var service in this.networknameidx[context.Name][tsitem])
                        {
                            AddService(service, this.networktype);
                        }
                    }
                }
                else if (context.NodeType == TreeViewNodeType.TransportStreamNetworkname)
                {
                    UpdateList(this.networknameidx[context.Name][context.TsId], this.networktype);
                }
                else if (context.NodeType == TreeViewNodeType.Network)
                {
                    foreach (var tsitem in this.networkidx[context.Int1].Keys)
                    {
                        foreach (var service in this.networkidx[context.Int1][tsitem])
                        {
                            AddService(service, this.networktype);
                        }
                    }
                }
                else if (context.NodeType == TreeViewNodeType.TransportStreamNid)
                {
                    UpdateList(this.networkidx[context.Int1][context.TsId], this.networktype);
                }
                else if (context.NodeType == TreeViewNodeType.OriginalNetworkId)
                {
                    foreach (var tsitem in this.originalnetworkidx[context.Int1].Keys)
                    {
                        foreach (var service in this.originalnetworkidx[context.Int1][tsitem])
                        {
                            AddService(service, this.networktype);
                        }
                    }
                }
                else if (context.NodeType == TreeViewNodeType.TransportStreamOnid)
                {
                    UpdateList(this.originalnetworkidx[context.Int1][context.TsId], this.networktype);
                }
                else if (context.NodeType == TreeViewNodeType.Bouquet)
                {
                    BouquetKey b = new BouquetKey();
                    b.id = context.Int1;
                    b.name = context.Name;
                    UpdateList(this.bouquetidx[b], this.networktype);
                }
                else if (context.NodeType == TreeViewNodeType.Position)
                {
                    foreach (var tsitem in this.satnameidx[context.Name].Keys)
                    {
                        foreach (var service in this.satnameidx[context.Name][tsitem])
                        {
                            AddService(service, this.networktype);
                        }
                    }
                }
                else if (context.NodeType == TreeViewNodeType.OneMuxByPosition)
                {
                    FrequencyKey key = new FrequencyKey();
                    key.frequency = context.Int1;
                    key.polarity = context.String1;
                    UpdateList(this.satnameidx[context.Name][key], this.networktype);
                }
                else if (context.NodeType == TreeViewNodeType.AlphabeticRoot)
                {
                    UpdateList(this.allalphabeticidx, this.networktype);
                }
                else if (context.NodeType == TreeViewNodeType.Letter)
                {
                    if (this.alphabeticidx.Keys.Contains(context.Letter))
                    {
                        UpdateList(this.alphabeticidx[context.Letter], this.networktype);
                    }
                }
            }
        }

        private string GetTunerString(DvbCTTuner tuner)
        {
            return Convert.ToString(tuner.Frequency);
        }

        private string GetTunerString(DvbSTuner tuner)
        {
            string str = string.Format("{0}{1} {2} {3}",tuner.Frequency,tuner.Polarity,tuner.Fec,tuner.Symbolrate);

            if (tuner.ModulationSystem == 1)
            {
                str += " S2";
            }

            return str;
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (AboutBox1 box = new AboutBox1())
            {
                box.ShowDialog(this);
            }
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            SaveFilterXML();

            //save column width state
            if (this.listView1.Columns.Count > 0)
            {
                if (networktype == NetworkType.DVBS)
                {
                    for (int n = 0; n < this.listView1.Columns.Count; n++)
                    {
                        dvbscolumn[n] = this.listView1.Columns[n].Width;
                    }
                }
                else if (networktype == NetworkType.DVBT)
                {
                    for (int n = 0; n < this.listView1.Columns.Count; n++)
                    {
                        dvbtcolumn[n] = this.listView1.Columns[n].Width;
                    }
                }
                else if (networktype == NetworkType.DVBC)
                {
                    for (int n = 0; n < this.listView1.Columns.Count; n++)
                    {
                        dvbccolumn[n] = this.listView1.Columns[n].Width;
                    }
                }
            }

            if(this.listViewEIT.Columns.Count==this.eitcolumn.Count())
            {
                for (int n = 0; n < this.listViewEIT.Columns.Count; n++)
                {
                    eitcolumn[n] = this.listViewEIT.Columns[n].Width;
                }
            }

            string str = "";
            for (int n = 0; n < this.dvbscolumn.Count(); n++)
            {
                if (n != 0) str += ",";
                str += Convert.ToString(this.dvbscolumn[n]);
            }
            Properties.Settings.Default.DVBSColumnHeaderWidth = str;

            str = "";
            for (int n = 0; n < this.dvbtcolumn.Count(); n++)
            {
                if (n != 0) str += ",";
                str += Convert.ToString(this.dvbtcolumn[n]);
            }
            Properties.Settings.Default.DVBTColumnHeaderWidth = str;

            str = "";
            for (int n = 0; n < this.dvbccolumn.Count(); n++)
            {
                if (n != 0) str += ",";
                str += Convert.ToString(this.dvbccolumn[n]);
            }
            Properties.Settings.Default.DVBCColumnHeaderWidth = str;

            str = "";
            for (int n = 0; n < this.eitcolumn.Count(); n++)
            {
                if (n != 0) str += ",";
                str += Convert.ToString(this.eitcolumn[n]);
            }
            Properties.Settings.Default.EITColumnHeaderWidth = str;

            Properties.Settings.Default.FilterOptionExclude = this.filterContext.Exclude;

            Properties.Settings.Default.Save();
        }

        private void filterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FilterForm filter = new FilterForm();
            filter.filterContext = this.filterContext;
            filter.ShowDialog();
            this.filterContext.ApplyFilter(this.servicelist, this.servicelistfiltered);
            RefreshView();
        }

        private void RefreshView()
        {
            this.treeView1.Nodes.Clear();
            this.listView1.Items.Clear();
            CreateTree();
            BuildIdx();
            UpdateTreeView();
            this.treeView1.SelectedNode = this.root;
        }

        private void LoadFilterXML()
        {
            string settingspath = GetSettingsPath();
            settingspath += "\\filter.xml";

            try
            {
                using (System.IO.Stream f = new FileStream(settingspath,FileMode.Open))
                {
                    XmlReader reader = new XmlTextReader(f);
                    XmlDocument doc = new XmlDocument();
                    doc.Load(reader);

                    foreach (var item in doc["filterconditions"])
                    {
                        FilterCondition filtercondition = new FilterCondition();

                        filtercondition.Enable = Convert.ToBoolean(((XmlNode)item).Attributes["enable"].Value);
                        filtercondition.filterAttributeType = (FilterAttributeType)System.Enum.Parse(typeof(FilterAttributeType), ((XmlNode)item).Attributes["attribute"].Value);
                        filtercondition.filterRelationType = (FilterRelationType)System.Enum.Parse(typeof(FilterRelationType), ((XmlNode)item).Attributes["relation"].Value);
                        filtercondition.Value = ((XmlNode)item).Attributes["value"].Value;

                        this.filterContext.FilterConditionSet.Add(filtercondition);
                    }
                }
            }
            catch (Exception e)
            {
                // this is excepted first time application is run
                Trace.WriteLine(string.Format("error loading filterXML: {0}",e.Message));
            }
        }

        private void SaveFilterXML()
        {
            try
            {
                string settingspath = GetSettingsPath();
                settingspath += "\\filter.xml";

                XmlDocument doc = new XmlDocument();
                XmlElement element = doc.CreateElement("filterconditions");

                foreach (var item in this.filterContext.FilterConditionSet)
                {
                    XmlNode s = doc.CreateElement("filtercondition");
                    XmlAttribute attribute = null;

                    attribute = doc.CreateAttribute("enable");
                    attribute.Value = Convert.ToString(item.Enable);
                    s.Attributes.Append(attribute);

                    attribute = doc.CreateAttribute("attribute");
                    attribute.Value = Convert.ToString(item.filterAttributeType);
                    s.Attributes.Append(attribute);

                    attribute = doc.CreateAttribute("relation");
                    attribute.Value = Convert.ToString(item.filterRelationType);
                    s.Attributes.Append(attribute);


                    attribute = doc.CreateAttribute("value");
                    attribute.Value = item.Value;
                    s.Attributes.Append(attribute);

                    element.AppendChild(s);
                }
                doc.AppendChild(element);
                doc.Save(settingspath);
            }
            catch (Exception e)
            {
                Trace.WriteLine(string.Format("error loading SaveFilterXML: {0}", e.Message));
            }
        }

        private string GetSettingsPath()
        {
            try
            {
                var UserConfig = ConfigurationManager.OpenExeConfiguration( ConfigurationUserLevel.PerUserRoamingAndLocal);
                return System.IO.Path.GetDirectoryName(UserConfig.FilePath);
            }
            catch (ConfigurationException e)
            {
                Trace.WriteLine(string.Format("error retrieving settings folder: {0}", e.Message));
                return e.Filename;
            }
        }

        private void openEITToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.FileName = Properties.Settings.Default.EITFile;
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                Properties.Settings.Default.EITFile = dlg.FileName;
                Properties.Settings.Default.Save();
                LoadEITFile(dlg.FileName);
            }
        }

        List<Event> eventlist = new System.Collections.Generic.List<Event>();
        List<Event> activeeventlist = null;

        private void LoadEITFile(string file)
        {
            //this.listViewEIT.Clear();
            this.listViewEIT.VirtualListSize = 0;

            using (System.IO.Stream f = new FileStream(file, FileMode.Open))
            {
                XmlReader reader = new XmlTextReader(f);
                XmlDocument doc = new XmlDocument();
                doc.Load(reader);

                this.eventlist = new System.Collections.Generic.List<Event>();

                foreach (var _event in doc["eit"].ChildNodes)
                {
                    Event e = new Event();
                    ExtractEvent((XmlNode)_event, e);
                    this.eventlist.Add(e);
                }
                BuildEventIdx();
                CreateEITTree();
                this.activeeventlist = this.eventlist;
                this.listViewEIT.VirtualListSize = this.activeeventlist.Count();
            }
        }

        void ExtractEvent(XmlNode l, Event e)
        {
            DateTime starttime, endtime;
            e.Id = Convert.ToInt32(l.Attributes["eventid"].Value);
            e.VersionNumber = Convert.ToInt32(l.Attributes["versionnumber"].Value);
            e.TableId = Convert.ToInt32(l.Attributes["tableid"].Value);
            e.Onid = Convert.ToInt32(l.Attributes["originalnetworkid"].Value);
            e.Tsid = Convert.ToInt32(l.Attributes["transportstreamid"].Value);
            e.Sid = Convert.ToInt32(l.Attributes["serviceid"].Value);
            if(DateTime.TryParse(l.Attributes["starttime"].Value, out starttime))
            {
                e.StartTime = starttime;
            }
            if(DateTime.TryParse(l.Attributes["endtime"].Value, out endtime))
            {
                e.EndTime = endtime;
            }
            e.Name = l.Attributes["eventname"].Value;
            e.Text = l.Attributes["eventtext"].Value;
            e.ExtendedText = l.Attributes["extendedeventtext"].Value;
        }
        
        void AddEvent(Event e)
        {
            ListViewItem i = new ListViewItem();
            i.Text = Convert.ToString(e.Id);
            i.SubItems.Add(Convert.ToString(e.VersionNumber));
            i.SubItems.Add(Convert.ToString(e.TableId));
            i.SubItems.Add(Convert.ToString(e.Onid));
            i.SubItems.Add(Convert.ToString(e.Tsid));
            i.SubItems.Add(Convert.ToString(e.Sid));
            i.SubItems.Add(Convert.ToString(e.StartTime));
            i.SubItems.Add(Convert.ToString(e.EndTime));
            i.SubItems.Add(e.Name);
            i.SubItems.Add(e.Text);
            i.SubItems.Add(e.ExtendedText);
            this.listViewEIT.Items.Add(i);
        }

        private void listViewEIT_RetrieveVirtualItem(object sender, RetrieveVirtualItemEventArgs e)
        {
            Event _e = this.activeeventlist[e.ItemIndex];
            ListViewItem i = new ListViewItem();
            i.Text = Convert.ToString(_e.Id);
            i.SubItems.Add(Convert.ToString(_e.VersionNumber));
            i.SubItems.Add(Convert.ToString(_e.TableId));
            i.SubItems.Add(Convert.ToString(_e.Onid));
            i.SubItems.Add(Convert.ToString(_e.Tsid));
            i.SubItems.Add(Convert.ToString(_e.Sid));
            i.SubItems.Add(Convert.ToString(_e.StartTime));
            i.SubItems.Add(Convert.ToString(_e.EndTime));
            i.SubItems.Add(_e.Name);
            i.SubItems.Add(_e.Text);
            i.SubItems.Add(_e.ExtendedText);
            e.Item = i;
        }

        struct ServiceKey
        {
            public int onid;
            public int tsid;
            public int sid;
        }

        class ServiceKeyComparer : Comparer<ServiceKey>
        {
            public override int Compare(ServiceKey k1, ServiceKey k2)
            {
                if (k1.onid != k2.onid) return k1.onid.CompareTo(k2.onid);
                else if (k1.tsid != k2.tsid) return k1.tsid.CompareTo(k2.tsid);
                else return k1.sid.CompareTo(k2.sid);
            }
        }

        private SortedDictionary<ServiceKey, List<Event>> eventidx = new SortedDictionary<ServiceKey, List<Event>>(new ServiceKeyComparer());

        private void BuildEventIdx()
        {
            this.eventidx.Clear();
            foreach(Event e in this.eventlist)
            {
                ServiceKey key = new ServiceKey();
                key.onid = e.Onid;
                key.tsid = e.Tsid;
                key.sid = e.Sid;

                if(!this.eventidx.Keys.Contains(key))
                {
                    List<Event> list = new List<Event>();
                    this.eventidx.Add(key, list);
                }
                this.eventidx[key].Add(e);
            }
        }

        struct EitTreeNode
        {
            public TreeNode root;
        }


        EitTreeNode eittreenode = new EitTreeNode();

        private void CreateEITTree()
        {
            this.eittreenode.root = this.treeViewEIT.Nodes.Add("All");
            this.eittreenode.root.Tag = this.eventlist;
            foreach (ServiceKey key in this.eventidx.Keys)
            {
                string s = string.Format("{0}/{1}/{2}", key.onid, key.tsid, key.sid);
                TreeNode item = new TreeNode(s);
                item.Tag = this.eventidx[key];
                this.eittreenode.root.Nodes.Add(item);
            }
        }

        private void treeViewEIT_AfterSelect(object sender, TreeViewEventArgs e)
        {
            TreeNode selected = this.treeViewEIT.SelectedNode;
            if (selected != null)
            {
                this.listViewEIT.VirtualListSize = 0;
                this.activeeventlist = (List<Event>) selected.Tag;
                this.listViewEIT.VirtualListSize = activeeventlist.Count();
            }
        }
    }
}
