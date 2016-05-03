using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dvbseserviceview
{
    public class ServiceDiffSettings
    {
        private bool name1 = true;
        private bool provider1 = true;
        private bool network1 = true;
        private bool oNID = true;
        private bool video1 = true;
        private bool audio1 = true;
        private bool pMT = true;
        private bool pCR = true;
        private bool sericeType = true;
        private bool free_CA_mode = true;
        private bool cA_system_ID = true;
        private bool lCN = true;
        private bool bouquet1 = true;
        private bool features1 = true;
        private bool onlyCompareCommonMux = false;

        public bool Name
        {
            get
            {
                return name1;
            }

            set
            {
                name1 = value;
            }
        }

        public bool Provider
        {
            get
            {
                return provider1;
            }

            set
            {
                provider1 = value;
            }
        }

        public bool Network
        {
            get
            {
                return network1;
            }

            set
            {
                network1 = value;
            }
        }

        public bool Onid
        {
            get
            {
                return oNID;
            }

            set
            {
                oNID = value;
            }
        }

        public bool Video
        {
            get
            {
                return video1;
            }

            set
            {
                video1 = value;
            }
        }

        public bool Audio
        {
            get
            {
                return audio1;
            }

            set
            {
                audio1 = value;
            }
        }

        public bool Pmt
        {
            get
            {
                return pMT;
            }

            set
            {
                pMT = value;
            }
        }

        public bool Pcr
        {
            get
            {
                return pCR;
            }

            set
            {
                pCR = value;
            }
        }

        public bool SericeType
        {
            get
            {
                return sericeType;
            }

            set
            {
                sericeType = value;
            }
        }

        public bool FreeCaMode
        {
            get
            {
                return free_CA_mode;
            }

            set
            {
                free_CA_mode = value;
            }
        }

        public bool CaSystemId
        {
            get
            {
                return cA_system_ID;
            }

            set
            {
                cA_system_ID = value;
            }
        }

        public bool Lcn
        {
            get
            {
                return lCN;
            }

            set
            {
                lCN = value;
            }
        }

        public bool Bouquet
        {
            get
            {
                return bouquet1;
            }

            set
            {
                bouquet1 = value;
            }
        }

        public bool Features
        {
            get
            {
                return features1;
            }

            set
            {
                features1 = value;
            }
        }

        public bool OnlyCompareCommonMux
        {
            get
            {
                return onlyCompareCommonMux;
            }

            set
            {
                onlyCompareCommonMux = value;
            }
        }

        public void Load()
        {
            this.OnlyCompareCommonMux = Properties.Settings.Default.cmpOnlyCompareCommonMux;
            this.Name = Properties.Settings.Default.cmpName;
            this.Provider = Properties.Settings.Default.cmpProvider;
            this.Network = Properties.Settings.Default.cmpNetwork;
            this.Onid = Properties.Settings.Default.cmpOnid;
            this.Video = Properties.Settings.Default.cmpVideo;
            this.Audio = Properties.Settings.Default.cmpAudio;
            this.Pmt = Properties.Settings.Default.cmpPmt;
            this.Pcr = Properties.Settings.Default.cmpPcr;
            this.SericeType = Properties.Settings.Default.cmpType;
            this.FreeCaMode = Properties.Settings.Default.cmpFreeCaMode;
            this.CaSystemId = Properties.Settings.Default.cmpCaSystemId;
            this.Lcn = Properties.Settings.Default.cmpLcn;
            this.Bouquet = Properties.Settings.Default.cmpBouquet;
            this.Features = Properties.Settings.Default.cmpFeatures;
        }

        public void Save()
        {
            Properties.Settings.Default.cmpOnlyCompareCommonMux = this.OnlyCompareCommonMux;
            Properties.Settings.Default.cmpName = this.Name;
            Properties.Settings.Default.cmpProvider = this.Provider;
            Properties.Settings.Default.cmpNetwork = this.Network;
            Properties.Settings.Default.cmpOnid = this.Onid;
            Properties.Settings.Default.cmpVideo = this.Video;
            Properties.Settings.Default.cmpAudio = this.Audio;
            Properties.Settings.Default.cmpPmt = this.Pmt;
            Properties.Settings.Default.cmpPcr = this.Pcr;
            Properties.Settings.Default.cmpType = this.SericeType;
            Properties.Settings.Default.cmpFreeCaMode = this.FreeCaMode;
            Properties.Settings.Default.cmpCaSystemId = this.CaSystemId;
            Properties.Settings.Default.cmpLcn = this.Lcn;
            Properties.Settings.Default.cmpBouquet = this.Bouquet;
            Properties.Settings.Default.cmpFeatures = this.Features;
        }
    }
}
