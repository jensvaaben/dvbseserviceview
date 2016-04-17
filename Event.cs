using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dvbseserviceview
{
    internal class Event
    {
        int id = -1;
        int versionnumber = -1;
        int tableid = -1;
        int originalnetworkid = -1;
        int transportstreamid = -1;
        int serviceid = -1;
        DateTime starttime = DateTime.Now;
        DateTime endtime = DateTime.Now;
        string eventname = "";
        string eventtext = "";
        string extendedeventtext = "";

        public int Id
        {
            get
            {
                return this.id;
            }
            set
            {
                this.id = value;
            }
        }
        public int VersionNumber
        {
            get
            {
                return this.versionnumber;
            }
            set
            {
                this.versionnumber = value;
            }
        }
        public int TableId
        {
            get
            {
                return this.tableid;
            }
            set
            {
                this.tableid = value;
            }
        }
        public int Onid
        {
            get
            {
                return this.originalnetworkid;
            }
            set
            {
                this.originalnetworkid = value;
            }
        }
        public int Tsid
        {
            get
            {
                return this.transportstreamid;
            }
            set
            {
                this.transportstreamid = value;
            }
        }
        public int Sid
        {
            get
            {
                return this.serviceid;
            }
            set
            {
                this.serviceid = value;
            }
        }
        public DateTime StartTime
        {
            get
            {
                return this.starttime;
            }
            set
            {
                this.starttime = value;
            }
        }
        public DateTime EndTime
        {
            get
            {
                return this.endtime;
            }
            set
            {
                this.endtime = value;
            }
        }
        public string Name
        {
            get
            {
                return this.eventname;
            }
            set
            {
                this.eventname = value;
            }
        }
        public string Text
        {
            get
            {
                return this.eventtext;
            }
            set
            {
                this.eventtext = value;
            }
        }
        public string ExtendedText
        {
            get
            {
                return this.extendedeventtext;
            }
            set
            {
                this.extendedeventtext = value;
            }
        }
    }
}
