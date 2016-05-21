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
        public enum NetworkType
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

        /*struct EitTreeNode
        {
            public TreeNode root;
        }*/

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

        class MuxKey
        {
            private string position = ""; //DVB-S
            private Int64 frequency = -1; // DVB-S/T/C
            private string polarity = ""; // DVB-S

            public string Position
            {
                get
                {
                    return this.position;
                }
                set
                {
                    this.position = value;
                }
            }
            public Int64 Frequency
            {
                get
                {
                    return this.frequency;
                }
                set
                {
                    this.frequency = value;
                }
            }
            public string Polarity
            {
                get
                {
                    return this.polarity;
                }
                set
                {
                    this.polarity = value;
                }
            }

        }

        class MuxSKeyComparer : Comparer<MuxKey>
        {
            public override int Compare(MuxKey k1, MuxKey k2)
            {
                if (k1.Position != k2.Position) return k1.Position.CompareTo(k2.Position);
                else if (k1.Frequency != k2.Frequency) return k1.Frequency.CompareTo(k2.Frequency);
                else return k1.Polarity.CompareTo(k2.Polarity);
            }
        }

        class MuxTCKeyComparer : Comparer<MuxKey>
        {
            public override int Compare(MuxKey k1, MuxKey k2)
            {
                return k1.Frequency.CompareTo(k2.Frequency);
            }
        }


        class ServiceDiffKey
        {
            private string position = "";
            private Int64 frequency = -1;
            private string polarity = "";
            private int sid = -1;
            private int tsid = -1;
            private int nid = -1;

            public string Position
            {
                get
                {
                    return this.position;
                }
                set
                {
                    this.position = value;
                }
            }
            public string Polarity
            {
                get
                {
                    return this.polarity;
                }
                set
                {
                    this.polarity = value;
                }
            }
            public Int64 Frequency
            {
                get
                {
                    return this.frequency;
                }
                set
                {
                    this.frequency = value;
                }
            }
            public int Sid
            {
                get
                {
                    return this.sid;
                }
                set
                {
                    this.sid = value;
                }
            }
            public int Tsid
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
            public int Nid
            {
                get
                {
                    return this.nid;
                }
                set
                {
                    this.nid = value;
                }
            }
        }

        class ServiceDvbSComparer : Comparer<ServiceDiffKey>
        {
            public override int Compare(ServiceDiffKey x, ServiceDiffKey y)
            {
                if (x.Position != y.Position) return x.Position.CompareTo(y.Position);
                else if (x.Frequency != y.Frequency) return x.Frequency.CompareTo(y.Frequency);
                else if (x.Polarity != y.Polarity) return x.Polarity.CompareTo(y.Polarity);
                else if (x.Nid != y.Nid) return x.Nid.CompareTo(y.Nid);
                else if (x.Tsid != y.Tsid) return x.Tsid.CompareTo(y.Tsid);
                else return x.Sid.CompareTo(y.Sid);
            }
        }

        class ServiceDvbCTComparer : Comparer<ServiceDiffKey>
        {
            public override int Compare(ServiceDiffKey x, ServiceDiffKey y)
            {
                if (x.Frequency != y.Frequency) return x.Frequency.CompareTo(y.Frequency);
                else if (x.Nid != y.Nid) return x.Nid.CompareTo(y.Nid);
                else if (x.Tsid != y.Tsid) return x.Tsid.CompareTo(y.Tsid);
                else return x.Sid.CompareTo(y.Sid);
            }
        }

        struct ChanngedService
        {
            public Service s1;
            public Service s2;
        }

        private FilterContext filterContext = new FilterContext();
        private SortedDictionary<ServiceKey, List<Event>> eventidx = new SortedDictionary<ServiceKey, List<Event>>(new ServiceKeyComparer());
        private TreeNode eitroot = new TreeNode();
        private List<Service> servicelist = new List<Service>();
        private List<Service> servicelistfiltered = new List<Service>();
        private int[] dvbscolumn = new int[] { 70, 100, 100, 100, 100, 100, 100, 100, 100, 100, 100, 100, 100, 100, 100, 100, 100, 100, 100, 100 };
        private int[] dvbtcolumn = new int[] { 70, 100, 100, 100, 100, 100, 100, 100, 100, 100, 100, 100, 100, 100, 100, 100, 100, 100, 100 };
        private int[] dvbccolumn = new int[] { 70, 100, 100, 100, 100, 100, 100, 100, 100, 100, 100, 100, 100, 100, 100, 100, 100, 100, 100 };
        private int[] eitcolumn = new int[] { 50, 50, 50, 50, 50, 50, 150, 150, 150, 150, 150 };
        private SortedDictionary<string, List<Service>> provideridx = new SortedDictionary<string, List<Service>>();
        private SortedDictionary<string, SortedDictionary<int, List<Service>>> networknameidx = new SortedDictionary<string, SortedDictionary<int, List<Service>>>();
        private SortedDictionary<int, SortedDictionary<int, List<Service>>> networkidx = new SortedDictionary<int, SortedDictionary<int, List<Service>>>();
        private SortedDictionary<int, SortedDictionary<int, List<Service>>> originalnetworkidx = new SortedDictionary<int, SortedDictionary<int, List<Service>>>();
        private SortedDictionary<BouquetKey, List<Service>> bouquetidx = new SortedDictionary<BouquetKey, List<Service>>(new BouquetKeyComparer());
        private SortedDictionary<string, SortedDictionary<FrequencyKey, List<Service>>> satnameidx = new SortedDictionary<string, SortedDictionary<FrequencyKey, List<Service>>>();
        private Dictionary<char, List<Service>> alphabeticidx = new Dictionary<char, List<Service>>();
        private List<Service> allalphabeticidx = new List<Service>();
        private TreeNode root = null;
        private TreeNode position = null;
        private TreeNode network = null;
        private TreeNode original_network = null;
        private TreeNode network_name = null;
        private TreeNode provider = null;
        private TreeNode bouquet = null;
        private TreeNode alphabetic = null;
        private SortedDictionary<ServiceKey, string> serviceidx = new SortedDictionary<ServiceKey, string>(new ServiceKeyComparer());
        private List<Event> eventlist = new System.Collections.Generic.List<Event>();
        private List<Event> activeeventlist = null;
        private string servicedifferentialfile1 = "";
        private string servicedifferentialfile2 = "";
        private NetworkType servicediffnetworktype = NetworkType.DVBS;
        private SortedSet<MuxKey> muxdiff1 = null;
        private SortedSet<MuxKey> muxdiff2 = null;
        private SortedSet<MuxKey> muxonlylist1 = null;
        private SortedSet<MuxKey> muxonlylist2 = null;
        private SortedSet<MuxKey> muxbothlists = null;
        private SortedDictionary<ServiceDiffKey, Service> servicediff1 = null;
        private SortedDictionary<ServiceDiffKey, Service> servicediff2 = null;
        private SortedDictionary<ServiceDiffKey, Service> serviceonlylist1 = null;
        private SortedDictionary<ServiceDiffKey, Service> serviceonlylist2 = null;
        private SortedDictionary<ServiceDiffKey, Service> servicediffunchanged = null;
        private SortedDictionary<ServiceDiffKey, ChanngedService> servicediffchanged = null;
        private List<Service> servicedifflist = null;
        private ServiceDiffSettings DiffSettings = new ServiceDiffSettings();
        private ColumnSettings columnSettings = new ColumnSettings();
 
        public Form1()
        {
            InitializeComponent();

            ImageList imageListSmall = new ImageList();
            imageListSmall.Images.Add(Properties.Resources.video);
            imageListSmall.Images.Add(Properties.Resources.audioser);
            imageListSmall.Images.Add(Properties.Resources.dataserv);
            this.listViewService.SmallImageList = imageListSmall;
            this.listViewServiceDiff.SmallImageList = imageListSmall;

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

            this.DiffSettings.Load();
            this.columnSettings.Load();

            // intialize filter condition
            this.filterContext.Exclude = Properties.Settings.Default.FilterOptionExclude;

            this.servicedifferentialfile1 = Properties.Settings.Default.servicedifferentialfile1;
            this.servicedifferentialfile2 = Properties.Settings.Default.servicedifferentialfile2;
            string v = Properties.Settings.Default.servicedifferentialnetworktype;
            if (v == "DVBS")
            {
                this.servicediffnetworktype = NetworkType.DVBS;
            }
            else if (v == "DVBT")
            {
                this.servicediffnetworktype = NetworkType.DVBT;
            }
            else if (v == "DVBC")
            {
                this.servicediffnetworktype = NetworkType.DVBC;
            }
            else
            {
                this.servicediffnetworktype = NetworkType.DVBS;
            }
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
                this.tabControl1.SelectedIndex = 0;
            }
        }

        private void LoadServiceFile(string file, NetworkType networktype)
        {
            // save column state
            if (this.listViewService.Columns.Count > 0)
            {
                if (oldnetworktype == NetworkType.DVBS)
                {
                    for (int n = 0; n < this.listViewService.Columns.Count; n++)
                    {
                        dvbscolumn[n] = this.listViewService.Columns[n].Width;
                    }
                }
                else if (oldnetworktype == NetworkType.DVBT)
                {
                    for (int n = 0; n < this.listViewService.Columns.Count; n++)
                    {
                        dvbtcolumn[n] = this.listViewService.Columns[n].Width;
                    }
                }
                else if (oldnetworktype == NetworkType.DVBC)
                {
                    for (int n = 0; n < this.listViewService.Columns.Count; n++)
                    {
                        dvbccolumn[n] = this.listViewService.Columns[n].Width;
                    }
                }
            }

            this.treeViewService.Nodes.Clear();
            this.listViewService.Clear();
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
                AddDvbColoumns(this.listViewService, networktype, this.columnSettings);
                CreateTree();
                BuildIdx();
                UpdateTreeView();
                UpdateEITServiceNames();
                this.treeViewService.SelectedNode = this.root;
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

            this.root = this.treeViewService.Nodes.Add("DVB Services");
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

        private void AddDvbColoumns(ListView listview, NetworkType networktype, ColumnSettings columnsettings)
        {
            listview.Columns.Add("No.", 30, HorizontalAlignment.Left);
            if (columnsettings.Name) listview.Columns.Add("Name", 100, HorizontalAlignment.Left);
            if (columnsettings.Provider) listview.Columns.Add("Provider", 100, HorizontalAlignment.Left);
            if (columnsettings.Frequency) listview.Columns.Add("Frequency", 100, HorizontalAlignment.Left);

            if (networktype == NetworkType.DVBS)
            {
                if (columnsettings.Position) listview.Columns.Add("Position", 100, HorizontalAlignment.Left);
            }

            if (columnsettings.Network) listview.Columns.Add("Network", 100, HorizontalAlignment.Left);
            if (columnsettings.Sid) listview.Columns.Add("SID", 100, HorizontalAlignment.Left);
            if (columnsettings.Tsid) listview.Columns.Add("TSID", 100, HorizontalAlignment.Left);
            if (columnsettings.Nid) listview.Columns.Add("NID", 100, HorizontalAlignment.Left);
            if (columnsettings.Onid) listview.Columns.Add("ONID", 100, HorizontalAlignment.Left);
            if (columnsettings.Video) listview.Columns.Add("Video", 100, HorizontalAlignment.Left);
            if (columnsettings.Audio) listview.Columns.Add("Audio", 100, HorizontalAlignment.Left);
            if (columnsettings.Pmt) listview.Columns.Add("PMT", 100, HorizontalAlignment.Left);
            if (columnsettings.Pcr) listview.Columns.Add("PCR", 100, HorizontalAlignment.Left);
            if (columnsettings.Type) listview.Columns.Add("Type", 100, HorizontalAlignment.Left);
            if (columnsettings.FreeCaMode) listview.Columns.Add("free_CA_mode", 100, HorizontalAlignment.Left);
            if (columnsettings.CaSystemId) listview.Columns.Add("CA_system_ID", 100, HorizontalAlignment.Left);
            if (columnsettings.Lcn) listview.Columns.Add("lcn", 100, HorizontalAlignment.Left);
            if (columnsettings.Bouquet) listview.Columns.Add("bouquet", 100, HorizontalAlignment.Left);
            if (columnsettings.Features) listview.Columns.Add("features", 100, HorizontalAlignment.Left);

            //adjust column width
            //if (networktype == NetworkType.DVBS)
            //{
            //    for (int n = 0; n < this.dvbscolumn.Count(); n++)
            //    {
            //        listview.Columns[n].Width = this.dvbscolumn[n];
            //    }
            //}
            //else if (networktype == NetworkType.DVBT)
            //{
            //    for (int n = 0; n < this.dvbtcolumn.Count(); n++)
            //    {
            //        listview.Columns[n].Width = this.dvbtcolumn[n];
            //    }
            //}
            //else if (networktype == NetworkType.DVBC)
            //{
            //    for (int n = 0; n < this.dvbccolumn.Count(); n++)
            //    {
            //        listview.Columns[n].Width = this.dvbccolumn[n];
            //    }
            //}
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
            if (this.columnSettings.Name) i.SubItems.Add(service.Name);
            if (this.columnSettings.Provider) i.SubItems.Add(service.Provider);

            if (networktype == NetworkType.DVBS)
            {
                if (this.columnSettings.Frequency) i.SubItems.Add(GetTunerString(service.DvbSTuner));
                if (this.columnSettings.Position) i.SubItems.Add(service.DvbSTuner.Position);
            }
            else  //DVB-T or DVB-C
            {
                if (this.columnSettings.Frequency) i.SubItems.Add(GetTunerString(service.DvbCTTuner));
            }

            AddDvbValues(service, i);
            this.listViewService.Items.Add(i);
        }

        private void AddDvbValues(Service service, ListViewItem i)
        {
            if (this.columnSettings.Network) i.SubItems.Add(service.NetworkName);
            if (this.columnSettings.Sid) i.SubItems.Add(Convert.ToString(service.Sid));
            if (this.columnSettings.Tsid) i.SubItems.Add(Convert.ToString(service.Tsid));
            if (this.columnSettings.Nid) i.SubItems.Add(Convert.ToString(service.Nid));
            if (this.columnSettings.Onid) i.SubItems.Add(Convert.ToString(service.Onid));
            if (this.columnSettings.Video) i.SubItems.Add(service.VideoPidListString); //VPID
            if (this.columnSettings.Audio) i.SubItems.Add(service.AudioPidListString); //APID
            if (this.columnSettings.Pmt) i.SubItems.Add(Convert.ToString(service.Pmt));
            if (this.columnSettings.Pcr) i.SubItems.Add(Convert.ToString(service.Pcr));
            if (this.columnSettings.Type) i.SubItems.Add(Convert.ToString(service.Type));
            if (this.columnSettings.FreeCaMode) i.SubItems.Add(service.FreeCaMode ? "1" : "0");
            if (this.columnSettings.CaSystemId) i.SubItems.Add(service.CaSystemIdListString); //CA_system_ID
            if (this.columnSettings.Lcn) i.SubItems.Add(service.Lcn!=-1 ? Convert.ToString(service.Lcn) : "n/a");
            if (this.columnSettings.Bouquet) i.SubItems.Add(service.BouquetListString); //bouquet
            if (this.columnSettings.Features) i.SubItems.Add(service.FeatureList); //features
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
                this.tabControl1.SelectedIndex = 0;
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
                this.tabControl1.SelectedIndex = 0;
            }
        }

        private void BuildIdx()
        {
            // clear all 
            this.provideridx.Clear();
            this.networknameidx.Clear();
            this.networkidx.Clear();
            this.originalnetworkidx.Clear();
            this.bouquetidx.Clear();
            this.satnameidx.Clear();
            this.alphabeticidx.Clear();
            this.allalphabeticidx.Clear();
            this.serviceidx.Clear();

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
            //service name ny onid/tsid/sid
            BuildServiceNameIdx(this.servicelist, this.serviceidx);
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

        private void BuildServiceNameIdx(List<Service> servicelist, SortedDictionary<ServiceKey, string> idx)
        {
            foreach(Service s in servicelist)
            {
                ServiceKey key = new ServiceKey();
                key.onid = s.Onid;
                key.tsid = s.Tsid;
                key.sid = s.Sid;
                if (!idx.Keys.Contains(key))
                {
                    idx.Add(key, s.Name);
                }
            }
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

        private void treeViewService_AfterSelect(object sender, TreeViewEventArgs e)
        {
            TreeNode selected = this.treeViewService.SelectedNode;
            if (selected != null)
            {
                this.listViewService.Items.Clear();
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
            if (this.listViewService.Columns.Count > 0)
            {
                if (networktype == NetworkType.DVBS)
                {
                    for (int n = 0; n < this.listViewService.Columns.Count; n++)
                    {
                        dvbscolumn[n] = this.listViewService.Columns[n].Width;
                    }
                }
                else if (networktype == NetworkType.DVBT)
                {
                    for (int n = 0; n < this.listViewService.Columns.Count; n++)
                    {
                        dvbtcolumn[n] = this.listViewService.Columns[n].Width;
                    }
                }
                else if (networktype == NetworkType.DVBC)
                {
                    for (int n = 0; n < this.listViewService.Columns.Count; n++)
                    {
                        dvbccolumn[n] = this.listViewService.Columns[n].Width;
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
            Properties.Settings.Default.servicedifferentialfile1 = this.servicedifferentialfile1;
            Properties.Settings.Default.servicedifferentialfile2 = this.servicedifferentialfile2;

            if (this.servicediffnetworktype == NetworkType.DVBS)
            {
                Properties.Settings.Default.servicedifferentialnetworktype = "DVBS";
            }
            else if (this.servicediffnetworktype == NetworkType.DVBT)
            {
                Properties.Settings.Default.servicedifferentialnetworktype = "DVBT";
            }
            else if (this.servicediffnetworktype == NetworkType.DVBC)
            {
                Properties.Settings.Default.servicedifferentialnetworktype = "DVBC";
            }

            this.DiffSettings.Save();
            this.columnSettings.Save();

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
            this.treeViewService.Nodes.Clear();
            this.listViewService.Items.Clear();
            CreateTree();
            BuildIdx();
            UpdateTreeView();
            this.treeViewService.SelectedNode = this.root;
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
                this.tabControl1.SelectedIndex = 1;
            }
        }

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
                UpdateEITServiceNames();
                this.activeeventlist = this.eventlist;
                this.treeViewEIT.SelectedNode = this.eitroot;
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
                e.StartTime = starttime.ToLocalTime();
            }
            if(DateTime.TryParse(l.Attributes["endtime"].Value, out endtime))
            {
                e.EndTime = endtime.ToLocalTime();
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

        private void CreateEITTree()
        {
            this.eitroot = this.treeViewEIT.Nodes.Add("All");
            this.eitroot.Tag = this.eventlist;
            foreach (ServiceKey key in this.eventidx.Keys)
            {
                string s = string.Format("{0}/{1}/{2}", key.onid, key.tsid, key.sid);
                TreeNode item = new TreeNode(s);
                item.Tag = this.eventidx[key];
                this.eitroot.Nodes.Add(item);
            }
            this.eitroot.Expand();
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

        private void UpdateEITServiceNames()
        {
            foreach(TreeNode node in this.eitroot.Nodes)
            {
                ServiceKey key = new ServiceKey();
                List<Event> v = (List<Event>)node.Tag;
                key.onid = v[0].Onid;
                key.tsid = v[0].Tsid;
                key.sid = v[0].Sid;
                if(this.serviceidx.Keys.Contains(key))
                {
                    node.Text = this.serviceidx[key];
                }
                else
                {
                    node.Text = string.Format("{0}/{1}/{2}", key.onid, key.tsid, key.sid);
                }
            }
        }

        private void compareServicesFilesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ServiceDiffForm dlg = new ServiceDiffForm();
            dlg.File1 = this.servicedifferentialfile1;
            dlg.File2 = this.servicedifferentialfile2;
            dlg.networkType = this.servicediffnetworktype;
            DialogResult res = dlg.ShowDialog();
            if (res == DialogResult.OK)
            {
                this.servicedifferentialfile1 = dlg.File1;
                this.servicedifferentialfile2 = dlg.File2;
                this.servicediffnetworktype = dlg.networkType;
                ProcessDiff();
            }
        }

        private void ProcessDiff()
        {
            // clear any items in UI
            // load and index files
            LoadDiffFile(this.servicedifferentialfile1, ref this.muxdiff1, ref this.servicediff1, this.servicediffnetworktype);
            LoadDiffFile(this.servicedifferentialfile2, ref this.muxdiff2, ref this.servicediff2, this.servicediffnetworktype);
            CreateDiffReports();
            this.tabControl1.SelectedIndex = 2;
        }

        private void CreateDiffReports()
        {
            // create comparison reports
            if(this.muxdiff1!=null && this.muxdiff2!=null)
            {
                CompareMuxDiff(this.muxdiff1, this.muxdiff2, ref this.muxonlylist1, ref this.muxonlylist2, ref this.muxbothlists, this.servicediffnetworktype);
                CompareServiceDiff(this.servicediff1, this.servicediff2, ref this.serviceonlylist1, ref this.serviceonlylist2, ref this.servicediffchanged, ref this.servicediffunchanged, this.servicediffnetworktype);
                // update UI
                UpdateMuxDiffUi();
                UpdateServiceDiffUi();
            }
        }

        private void LoadDiffFile(string file, ref SortedSet<MuxKey> muxlist, ref SortedDictionary<ServiceDiffKey, Service> servicelist, NetworkType networktype)
        {
            if(networktype==NetworkType.DVBS)
            {
                muxlist = new SortedSet<MuxKey>(new MuxSKeyComparer());
                servicelist = new SortedDictionary<ServiceDiffKey, Service>(new ServiceDvbSComparer());
                LoadDiffFileS(file, muxlist, servicelist);
            }
            else if (networktype == NetworkType.DVBT || networktype == NetworkType.DVBC)
            {
                muxlist = new SortedSet<MuxKey>(new MuxTCKeyComparer());
                servicelist = new SortedDictionary<ServiceDiffKey, Service>(new ServiceDvbSComparer());
                LoadDiffFileCT(file, muxlist, servicelist);
            }
        }

        private void LoadDiffFileS(string file, SortedSet<MuxKey> muxlist, SortedDictionary<ServiceDiffKey, Service> servicelist)
        {
            using (System.IO.Stream f = new FileStream(file, FileMode.Open))
            {
                XmlReader reader = new XmlTextReader(f);
                XmlDocument doc = new XmlDocument();
                doc.Load(reader);
                foreach (var service in doc["services"].ChildNodes)
                {
                    Service s = new Service();
                    ExtractService((XmlNode)service, s, NetworkType.DVBS);

                    MuxKey muxkey = new MuxKey();
                    muxkey.Position = s.Position;
                    muxkey.Frequency = s.DvbSTuner.Frequency;
                    muxkey.Polarity = s.DvbSTuner.Polarity;

                    if (!muxlist.Contains(muxkey))
                    {
                        muxlist.Add(muxkey);
                    }

                    ServiceDiffKey key = new ServiceDiffKey();
                    key.Position = s.Position;
                    key.Frequency = s.DvbSTuner.Frequency;
                    key.Polarity = s.DvbSTuner.Polarity;
                    key.Nid = s.Nid;
                    key.Tsid = s.Tsid;
                    key.Sid = s.Sid;

                    if (!servicelist.Keys.Contains(key))
                    {
                        servicelist.Add(key,s);
                    }
                }
            }
        }

        private void LoadDiffFileCT(string file, SortedSet<MuxKey> muxlist, SortedDictionary<ServiceDiffKey, Service> servicelist)
        {
            using (System.IO.Stream f = new FileStream(file, FileMode.Open))
            {
                XmlReader reader = new XmlTextReader(f);
                XmlDocument doc = new XmlDocument();
                doc.Load(reader);
                foreach (var service in doc["services"].ChildNodes)
                {
                    Service s = new Service();
                    ExtractService((XmlNode)service, s, NetworkType.DVBT);

                    MuxKey muxkey = new MuxKey();
                    muxkey.Frequency = s.DvbCTTuner.Frequency;

                    if (!muxlist.Contains(muxkey))
                    {
                        muxlist.Add(muxkey);
                    }

                    ServiceDiffKey key = new ServiceDiffKey();
                    key.Position = s.Position;
                    key.Frequency = s.DvbSTuner.Frequency;
                    key.Polarity = s.DvbSTuner.Polarity;
                    key.Nid = s.Nid;
                    key.Tsid = s.Tsid;
                    key.Sid = s.Sid;

                    if (!servicelist.Keys.Contains(key))
                    {
                        servicelist.Add(key, s);
                    }
                }
            }
        }

        private void CompareMuxDiff(SortedSet<MuxKey> list1, SortedSet<MuxKey> list2, ref SortedSet<MuxKey> onlylist1, ref SortedSet<MuxKey> onlylist2, ref SortedSet<MuxKey> common, NetworkType networktype)
        {
            if (networktype == NetworkType.DVBS)
            {
                CompareMuxDiffS(list1, list2, ref onlylist1, ref onlylist2, ref common);
            }
            else if (networktype == NetworkType.DVBT || networktype == NetworkType.DVBT)
            {
                CompareMuxDiffCT(list1, list2, ref onlylist1, ref onlylist2, ref common);
            }
        }

        private void CompareMuxDiffS(SortedSet<MuxKey> list1, SortedSet<MuxKey> list2, ref SortedSet<MuxKey> onlylist1, ref SortedSet<MuxKey> onlylist2, ref SortedSet<MuxKey> common)
        {
            onlylist1 = new SortedSet<MuxKey>(new MuxSKeyComparer());
            onlylist2 = new SortedSet<MuxKey>(new MuxSKeyComparer());
            common = new SortedSet<MuxKey>(new MuxSKeyComparer());
            foreach (MuxKey k in list1)
            {
                if (list2.Contains(k))
                {
                    common.Add(k);
                }
                else
                {
                    onlylist1.Add(k);
                }
            }

            foreach (MuxKey k in list2)
            {
                if (!list1.Contains(k))
                {
                    onlylist2.Add(k);
                }
            }
        }

        private void CompareMuxDiffCT(SortedSet<MuxKey> list1, SortedSet<MuxKey> list2, ref SortedSet<MuxKey> onlylist1, ref SortedSet<MuxKey> onlylist2, ref SortedSet<MuxKey> common)
        {
            onlylist1 = new SortedSet<MuxKey>(new MuxTCKeyComparer());
            onlylist2 = new SortedSet<MuxKey>(new MuxTCKeyComparer());
            common = new SortedSet<MuxKey>(new MuxTCKeyComparer());
            foreach (MuxKey k in list1)
            {
                if(list2.Contains(k))
                {
                    common.Add(k);
                }
                else
                {
                    onlylist1.Add(k);
                }
            }

            foreach (MuxKey k in list2)
            {
                if (!list1.Contains(k))
                {
                    onlylist2.Add(k);
                }
            }
        }

        private void UpdateMuxDiffUi()
        {
            if (this.servicediffnetworktype == NetworkType.DVBS)
            {
                UpdateDiffUIS();
            }
            else if (this.servicediffnetworktype == NetworkType.DVBT || this.servicediffnetworktype == NetworkType.DVBC)
            {
                UpdateDiffUICT();
            }
        }

        private void UpdateDiffUIS()
        {
            this.treeViewMuxDiff.Nodes.Clear();
            TreeNode only1 = this.treeViewMuxDiff.Nodes.Add(string.Format("Only in {0}", this.servicedifferentialfile1));
            TreeNode only2 = this.treeViewMuxDiff.Nodes.Add(string.Format("Only in {0}", this.servicedifferentialfile2));
            TreeNode common = this.treeViewMuxDiff.Nodes.Add("common");

            foreach (MuxKey k in this.muxonlylist1)
            {
                only1.Nodes.Add(string.Format("'{0}' {1}{2}", k.Position, k.Frequency, k.Polarity));
            }

            foreach (MuxKey k in this.muxonlylist2)
            {
                only2.Nodes.Add(string.Format("'{0}' {1}{2}", k.Position, k.Frequency, k.Polarity));
            }

            foreach (MuxKey k in this.muxbothlists)
            {
                common.Nodes.Add(string.Format("'{0}' {1}{2}", k.Position, k.Frequency, k.Polarity));
            }
        }

        private void UpdateDiffUICT()
        {
            this.treeViewMuxDiff.Nodes.Clear();
            TreeNode only1 = this.treeViewMuxDiff.Nodes.Add(string.Format("Only in {0}", this.servicedifferentialfile1));
            TreeNode only2 = this.treeViewMuxDiff.Nodes.Add(string.Format("Only in {0}", this.servicedifferentialfile2));
            TreeNode common = this.treeViewMuxDiff.Nodes.Add("common");

            foreach (MuxKey k in this.muxonlylist1)
            {
                only1.Nodes.Add(string.Format("{0}", k.Frequency));
            }

            foreach (MuxKey k in this.muxonlylist2)
            {
                only2.Nodes.Add(string.Format("{0}", k.Frequency));
            }

            foreach (MuxKey k in this.muxbothlists)
            {
                common.Nodes.Add(string.Format("{0}", k.Frequency));
            }
        }

        private void CompareServiceDiff(SortedDictionary<ServiceDiffKey, Service> list1, SortedDictionary<ServiceDiffKey, Service> list2, ref SortedDictionary<ServiceDiffKey, Service> only1, ref SortedDictionary<ServiceDiffKey, Service> only2, ref SortedDictionary<ServiceDiffKey, ChanngedService> changed, ref SortedDictionary<ServiceDiffKey, Service> unchanged, NetworkType networktype)
        {
            if(networktype == NetworkType.DVBS)
            {
                CompareServiceDiffS(list1, list2, ref only1, ref only2, ref changed, ref unchanged);
            }
            else if (networktype == NetworkType.DVBT || networktype == NetworkType.DVBC)
            {
                CompareServiceDiffCT(list1, list2, ref only1, ref only2, ref changed, ref unchanged);
            }
        }

        private void CompareServiceDiffS(SortedDictionary<ServiceDiffKey, Service> list1, SortedDictionary<ServiceDiffKey, Service> list2, ref SortedDictionary<ServiceDiffKey, Service> only1, ref SortedDictionary<ServiceDiffKey, Service> only2, ref SortedDictionary<ServiceDiffKey, ChanngedService> changed, ref SortedDictionary<ServiceDiffKey, Service> unchanged)
        {
            only1 = new SortedDictionary<ServiceDiffKey, Service>(new ServiceDvbSComparer());
            only2 = new SortedDictionary<ServiceDiffKey, Service>(new ServiceDvbSComparer());
            changed = new SortedDictionary<ServiceDiffKey, ChanngedService>(new ServiceDvbSComparer());
            unchanged = new SortedDictionary<ServiceDiffKey, Service>(new ServiceDvbSComparer());

            foreach(ServiceDiffKey key in list1.Keys)
            {
                if(this.DiffSettings.OnlyCompareCommonMux)
                {
                    //Check if service is in a common MUX and if not skip it.
                    MuxKey muxkey = new MuxKey();
                    muxkey.Frequency = list1[key].DvbSTuner.Frequency;
                    muxkey.Position = list1[key].Position;
                    muxkey.Polarity = list1[key].DvbSTuner.Polarity;

                    if (!this.muxbothlists.Contains(muxkey))
                        continue;
                }

                if (!list2.Keys.Contains(key))
                {
                    only1.Add(key,list1[key]);
                }
                else
                {
                    if (IsServiceEqual(list1[key],list2[key]))
                    {
                        unchanged.Add(key, list1[key]);
                    }
                    else
                    {
                        ChanngedService channgedservice;
                        channgedservice.s1 = list1[key];
                        channgedservice.s2 = list2[key];
                        changed.Add(key, channgedservice);
                    }
                }
            }

            foreach (ServiceDiffKey key in list2.Keys)
            {
                if (this.DiffSettings.OnlyCompareCommonMux)
                {
                    //Check if service is in a common MUX and if not skip it.
                    MuxKey muxkey = new MuxKey();
                    muxkey.Frequency = list2[key].DvbSTuner.Frequency;
                    muxkey.Position = list2[key].Position;
                    muxkey.Polarity = list2[key].DvbSTuner.Polarity;

                    if (!this.muxbothlists.Contains(muxkey))
                        continue;
                }

                if (!list1.Keys.Contains(key))
                {
                    only2.Add(key, list2[key]);
                }
            }
        }

        private void CompareServiceDiffCT(SortedDictionary<ServiceDiffKey, Service> list1, SortedDictionary<ServiceDiffKey, Service> list2, ref SortedDictionary<ServiceDiffKey, Service> only1, ref SortedDictionary<ServiceDiffKey, Service> only2, ref SortedDictionary<ServiceDiffKey, ChanngedService> changed, ref SortedDictionary<ServiceDiffKey, Service> unchanged)
        {
            only1 = new SortedDictionary<ServiceDiffKey, Service>(new ServiceDvbCTComparer());
            only2 = new SortedDictionary<ServiceDiffKey, Service>(new ServiceDvbCTComparer());
            changed = new SortedDictionary<ServiceDiffKey, ChanngedService>(new ServiceDvbCTComparer());
            unchanged = new SortedDictionary<ServiceDiffKey, Service>(new ServiceDvbCTComparer());

            foreach (ServiceDiffKey key in list1.Keys)
            {
                if (this.DiffSettings.OnlyCompareCommonMux)
                {
                    //Check if service is in a common MUX and if not skip it.
                    MuxKey muxkey = new MuxKey();
                    muxkey.Frequency = list1[key].DvbCTTuner.Frequency;

                    if (!this.muxbothlists.Contains(muxkey))
                        continue;
                }

                if (!list2.Keys.Contains(key))
                {
                    only1.Add(key, list1[key]);
                }
                else
                {
                    if (IsServiceEqual(list1[key], list2[key]))
                    {
                        unchanged.Add(key, list1[key]);
                    }
                    else
                    {
                        ChanngedService channgedservice;
                        channgedservice.s1 = list1[key];
                        channgedservice.s2 = list2[key];
                        changed.Add(key, channgedservice);
                    }
                }
            }

            foreach (ServiceDiffKey key in list2.Keys)
            {
                if (this.DiffSettings.OnlyCompareCommonMux)
                {
                    //Check if service is in a common MUX and if not skip it.
                    MuxKey muxkey = new MuxKey();
                    muxkey.Frequency = list2[key].DvbCTTuner.Frequency;

                    if (!this.muxbothlists.Contains(muxkey))
                        continue;
                }

                if (!list1.Keys.Contains(key))
                {
                    only2.Add(key, list2[key]);
                }
            }
        }

        private bool IsServiceEqual(Service s1, Service s2)
        {
            if (this.DiffSettings.Name && s1.Name != s2.Name)
                return false;
            else if (this.DiffSettings.Provider && s1.Provider != s2.Provider)
                return false;
            else if (this.DiffSettings.Video && s1.VideoPidListString != s2.VideoPidListString)
                return false;
            else if (this.DiffSettings.Audio && s1.AudioPidListString != s2.AudioPidListString)
                return false;
            else if (this.DiffSettings.Bouquet && s1.BouquetListString != s2.BouquetListString)
                return false;
            else if (this.DiffSettings.CaSystemId && s1.CaSystemIdListString != s2.CaSystemIdListString)
                return false;
            else if (this.DiffSettings.Features && s1.FeatureList != s2.FeatureList)
                return false;
            else if (this.DiffSettings.FreeCaMode && s1.FreeCaMode != s2.FreeCaMode)
                return false;
            else if (this.DiffSettings.Lcn && s1.Lcn != s2.Lcn)
                return false;
            else if (this.DiffSettings.Pcr && s1.Pcr != s2.Pcr)
                return false;
            else if (this.DiffSettings.Pmt && s1.Pmt != s2.Pmt)
                return false;
            else if (this.DiffSettings.SericeType && s1.Type != s2.Type)
                return false;
            else if (this.DiffSettings.Onid && s1.Onid != s2.Onid)
                return false;
            else
                return true;
        }

        delegate void UpdateServiceDiffListView();

        struct DiffTreeContext
        {
            public UpdateServiceDiffListView update;
        }

        private void UpdateServiceDiffUi()
        {
            this.servicedifflist = new List<Service>();

            this.treeViewServiceDiff.Nodes.Clear();
            this.listViewServiceDiff.VirtualListSize = 0;
            this.listViewServiceDiff.Columns.Clear();
            DiffTreeContext difftreecontext;

            AddDvbColoumns(this.listViewServiceDiff, this.servicediffnetworktype, this.columnSettings);

            TreeNode only1 = this.treeViewServiceDiff.Nodes.Add(string.Format("Only in {0}", this.servicedifferentialfile1));
            difftreecontext.update = UpdateServiceDiffListViewOnly1;
            only1.Tag = difftreecontext;
            TreeNode only2 = this.treeViewServiceDiff.Nodes.Add(string.Format("Only in {0}", this.servicedifferentialfile2));
            difftreecontext.update = UpdateServiceDiffListViewOnly2;
            only2.Tag = difftreecontext;
            TreeNode changed = this.treeViewServiceDiff.Nodes.Add("changed");
            difftreecontext.update = UpdateServiceDiffListViewChanged;
            changed.Tag = difftreecontext;
            TreeNode unchanged = this.treeViewServiceDiff.Nodes.Add("unchanged");
            difftreecontext.update = UpdateServiceDiffListViewUnchanged;
            unchanged.Tag = difftreecontext;

            treeViewServiceDiff.SelectedNode = only1;
        }

        void UpdateServiceDiffListViewOnly1()
        {
            this.servicedifflist.Clear();
            int idx = 0;
            foreach (ServiceDiffKey k in this.serviceonlylist1.Keys)
            {
                this.serviceonlylist1[k].No = idx++;
                this.servicedifflist.Add(this.serviceonlylist1[k]);
            }
            this.listViewServiceDiff.VirtualListSize = this.servicedifflist.Count();
        }

        void UpdateServiceDiffListViewOnly2()
        {
            this.servicedifflist.Clear();
            int idx = 0;
            foreach (ServiceDiffKey k in this.serviceonlylist2.Keys)
            {
                this.serviceonlylist2[k].No = idx++;
                this.servicedifflist.Add(this.serviceonlylist2[k]);
            }
            this.listViewServiceDiff.VirtualListSize = this.servicedifflist.Count();
        }

        void UpdateServiceDiffListViewChanged()
        {
            this.servicedifflist.Clear();
            int idx = 0;
            foreach (ServiceDiffKey k in this.servicediffchanged.Keys)
            {
                this.servicediffchanged[k].s1.No = idx++;
                this.servicedifflist.Add(this.servicediffchanged[k].s1);
                this.servicediffchanged[k].s2.No = idx++;
                this.servicedifflist.Add(this.servicediffchanged[k].s2);
            }
            this.listViewServiceDiff.VirtualListSize = this.servicedifflist.Count();
        }

        void UpdateServiceDiffListViewUnchanged()
        {
            this.servicedifflist.Clear();
            int idx = 0;
            foreach (ServiceDiffKey k in this.servicediffunchanged.Keys)
            {
                this.servicediffunchanged[k].No = idx++;
                this.servicedifflist.Add(this.servicediffunchanged[k]);
            }
            this.listViewServiceDiff.VirtualListSize = this.servicedifflist.Count();
        }

        private void treeViewServiceDiff_AfterSelect(object sender, TreeViewEventArgs e)
        {
            DiffTreeContext ctx = (DiffTreeContext) e.Node.Tag;
            ctx.update();
        }

        private void listViewServiceDiff_RetrieveVirtualItem(object sender, RetrieveVirtualItemEventArgs e)
        {
            Service s = this.servicedifflist[e.ItemIndex];
            ListViewItem i = new ListViewItem();
            i.ImageIndex = GetServiceTypeImage(s.Type);
            i.Text = Convert.ToString(s.No);

            if (this.columnSettings.Name) i.SubItems.Add(s.Name);
            if (this.columnSettings.Provider) i.SubItems.Add(s.Provider);

            if (this.servicediffnetworktype==NetworkType.DVBS)
            {
                if (this.columnSettings.Frequency) i.SubItems.Add(GetTunerString(s.DvbSTuner));
                if (this.columnSettings.Position) i.SubItems.Add(s.DvbSTuner.Position);
            }
            else //DVB-T or DVB-C
            {
                if (this.columnSettings.Frequency) if (this.columnSettings.Frequency) i.SubItems.Add(GetTunerString(s.DvbCTTuner));
            }

            AddDvbValues(s, i);

            e.Item = i;
        }

        private void comparisonPreferencesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ComparisonPreferencesForm dlg = new ComparisonPreferencesForm();
            dlg.DiffSettings = this.DiffSettings;
            DialogResult r = dlg.ShowDialog();
            if (r == DialogResult.OK)
                CreateDiffReports();
        }

        private void ColumnSelectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ColumnSelectorForm dlg = new ColumnSelectorForm();
            dlg.Settings = this.columnSettings;
            DialogResult r = dlg.ShowDialog();
            if(r == DialogResult.OK)
            {
                // redraw columns
                UpdateServiceDiffUi();
                UpdateServiceList();
            }
        }

       

        private void UpdateServiceList()
        {
            this.listViewService.Clear();
            AddDvbColoumns(this.listViewService, this.networktype, this.columnSettings);
            UpdateServiceListFromTreeView();
        }

        private void UpdateServiceListFromTreeView()
        {
            TreeNode selected = this.treeViewService.SelectedNode;
            if (selected != null)
            {
                this.listViewService.Items.Clear();
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
    }
}
