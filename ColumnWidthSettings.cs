using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dvbseserviceview
{
    class ColumnWidthSettings
    {
        private int number = 70;
        private int name = 100;
        private int provider = 100;
        private int frequency = 100;
        private int position = 100;
        private int network = 100;
        private int sid = 100;
        private int tsid = 100;
        private int nid = 100;
        private int onid = 100;
        private int video = 100;
        private int audio = 100;
        private int pmt = 100;
        private int pcr = 100;
        private int type = 100;
        private int free_ca_mode = 100;
        private int ca_system_id = 100;
        private int lcn = 100;
        private int bouquet = 100;
        private int features = 100;

        public int Number
        {
            get
            {
                return this.number;
            }
            set
            {
                this.number = value;
            }
        }
        public int Name
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
        public int Provider
        {
            get
            {
                return this.provider;
            }
            set
            {
                this.provider = value;
            }
        }
        public int Frequency
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
        public int Position
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
        public int Network
        {
            get
            {
                return this.network;
            }
            set
            {
                this.network = value;
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
        public int Onid
        {
            get
            {
                return this.onid;
            }
            set
            {
                this.onid = value;
            }
        }
        public int Video
        {
            get
            {
                return this.video;
            }
            set
            {
                this.video = value;
            }
        }
        public int Audio
        {
            get
            {
                return this.audio;
            }
            set
            {
                this.audio = value;
            }
        }
        public int Pmt
        {
            get
            {
                return this.pmt;
            }
            set
            {
                this.pmt = value;
            }
        }
        public int Pcr
        {
            get
            {
                return this.pcr;
            }
            set
            {
                this.pcr = value;
            }
        }
        public int Type
        {
            get
            {
                return this.type;
            }
            set
            {
                this.type = value;
            }
        }
        public int FreeCaMode
        {
            get
            {
                return this.free_ca_mode;
            }
            set
            {
                this.free_ca_mode = value;
            }
        }
        public int CaSystemId
        {
            get
            {
                return this.ca_system_id;
            }
            set
            {
                this.ca_system_id = value;
            }
        }
        public int Lcn
        {
            get
            {
                return this.lcn;
            }
            set
            {
                this.lcn = value;
            }
        }
        public int Bouquet
        {
            get
            {
                return this.bouquet;
            }
            set
            {
                this.bouquet = value;
            }
        }
        public int Features
        {
            get
            {
                return this.features;
            }
            set
            {
                this.features = value;
            }
        }

        public void Load()
        {
            this.number = Properties.Settings.Default.columnWidthNumber;
            this.name = Properties.Settings.Default.columnWidthName;
            this.provider = Properties.Settings.Default.columnWidthProvider;
            this.frequency = Properties.Settings.Default.columnWidthFrequency;
            this.position = Properties.Settings.Default.columnWidthPosition;
            this.network = Properties.Settings.Default.columnWidthNetwork;
            this.sid = Properties.Settings.Default.columnWidthSid;
            this.tsid = Properties.Settings.Default.columnWidthTsid;
            this.nid = Properties.Settings.Default.columnWidthNid;
            this.onid = Properties.Settings.Default.columnWidthOnid;
            this.video = Properties.Settings.Default.columnWidthVideo;
            this.audio = Properties.Settings.Default.columnWidthAudio;
            this.pmt = Properties.Settings.Default.columnWidthPmt;
            this.pcr = Properties.Settings.Default.columnWidthPcr;
            this.type = Properties.Settings.Default.columnWidthType;
            this.free_ca_mode = Properties.Settings.Default.columnWidthFreeCaMode;
            this.ca_system_id = Properties.Settings.Default.columnWidthCaSystemId;
            this.lcn = Properties.Settings.Default.columnWidthLcn;
            this.bouquet = Properties.Settings.Default.columnWidthBouquet;
            this.features = Properties.Settings.Default.columnWidthFeatures;
        }

        public void Save()
        {
            Properties.Settings.Default.columnWidthNumber = this.number;
            Properties.Settings.Default.columnWidthName = this.name;
            Properties.Settings.Default.columnWidthProvider = this.provider;
            Properties.Settings.Default.columnWidthFrequency = this.frequency;
            Properties.Settings.Default.columnWidthPosition = this.position;
            Properties.Settings.Default.columnWidthNetwork = this.network;
            Properties.Settings.Default.columnWidthSid = this.sid;
            Properties.Settings.Default.columnWidthTsid = this.tsid;
            Properties.Settings.Default.columnWidthNid = this.nid;
            Properties.Settings.Default.columnWidthOnid = this.onid;
            Properties.Settings.Default.columnWidthVideo = this.video;
            Properties.Settings.Default.columnWidthAudio = this.audio;
            Properties.Settings.Default.columnWidthPmt = this.pmt;
            Properties.Settings.Default.columnWidthPcr = this.pcr;
            Properties.Settings.Default.columnWidthType = this.type;
            Properties.Settings.Default.columnWidthFreeCaMode = this.free_ca_mode;
            Properties.Settings.Default.columnWidthCaSystemId = this.ca_system_id;
            Properties.Settings.Default.columnWidthLcn = this.lcn;
            Properties.Settings.Default.columnWidthBouquet = this.bouquet;
            Properties.Settings.Default.columnWidthFeatures = this.features;
        }

    }
}
