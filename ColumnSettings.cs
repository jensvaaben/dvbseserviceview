using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dvbseserviceview
{
    public class ColumnSettings
    {
        private bool number = true;
        private bool name = true;
        private bool provider = true;
        private bool frequency = true;
        private bool position = true;
        private bool network = true;
        private bool sid = true;
        private bool tsid = true;
        private bool nid = true;
        private bool onid = true;
        private bool video = true;
        private bool audio = true;
        private bool pmt = true;
        private bool pcr = true;
        private bool type = true;
        private bool free_ca_mode = true;
        private bool ca_system_id = true;
        private bool lcn = true;
        private bool bouquet = true;
        private bool features = true;

        public bool Number
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
        public bool Name
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
        public bool Provider
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
        public bool Frequency
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
        public bool Position
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
        public bool Network
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
        public bool Sid
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
        public bool Tsid
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
        public bool Nid
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
        public bool Onid
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
        public bool Video
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
        public bool Audio
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
        public bool Pmt
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
        public bool Pcr
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
        public bool Type
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
        public bool FreeCaMode
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
        public bool CaSystemId
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
        public bool Lcn
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
        public bool Bouquet
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
        public bool Features
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
            this.number = Properties.Settings.Default.columnNumber;
            this.name = Properties.Settings.Default.columnName;
            this.provider = Properties.Settings.Default.columnProvider;
            this.frequency = Properties.Settings.Default.columnFrequency;
            this.position = Properties.Settings.Default.columnPosition;
            this.network = Properties.Settings.Default.columnNetwork;
            this.sid = Properties.Settings.Default.columnSid;
            this.tsid = Properties.Settings.Default.columnTsid;
            this.nid = Properties.Settings.Default.columnNid;
            this.onid = Properties.Settings.Default.columnOnid;
            this.video = Properties.Settings.Default.columnVideo;
            this.audio = Properties.Settings.Default.columnAudio;
            this.pmt = Properties.Settings.Default.columnPmt;
            this.pcr = Properties.Settings.Default.columnPcr;
            this.type = Properties.Settings.Default.columnType;
            this.free_ca_mode = Properties.Settings.Default.columnFreeCaMode;
            this.ca_system_id = Properties.Settings.Default.columnCaSystemId;
            this.lcn = Properties.Settings.Default.columnLcn;
            this.bouquet = Properties.Settings.Default.columnBouquet;
            this.features = Properties.Settings.Default.columnFeatures;
        }
        public void Save()
        {
            Properties.Settings.Default.columnNumber = this.number;
            Properties.Settings.Default.columnName = this.name;
            Properties.Settings.Default.columnProvider = this.provider;
            Properties.Settings.Default.columnFrequency = this.frequency;
            Properties.Settings.Default.columnPosition = this.position;
            Properties.Settings.Default.columnNetwork = this.network;
            Properties.Settings.Default.columnSid = this.sid;
            Properties.Settings.Default.columnTsid = this.tsid;
            Properties.Settings.Default.columnNid = this.nid;
            Properties.Settings.Default.columnOnid = this.onid;
            Properties.Settings.Default.columnVideo = this.video;
            Properties.Settings.Default.columnAudio = this.audio;
            Properties.Settings.Default.columnPmt = this.pmt;
            Properties.Settings.Default.columnPcr = this.pcr;
            Properties.Settings.Default.columnType = this.type;
            Properties.Settings.Default.columnFreeCaMode = this.free_ca_mode;
            Properties.Settings.Default.columnCaSystemId = this.ca_system_id;
            Properties.Settings.Default.columnLcn = this.lcn;
            Properties.Settings.Default.columnBouquet = this.bouquet;
            Properties.Settings.Default.columnFeatures = this.features;
        }
    }
}
