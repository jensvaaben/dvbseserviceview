using System;

namespace dvbseserviceview
{
    internal enum FilterAttributeType
    {
        None,
        Name,
        Provider,
        NetworkName,
        CASystemID,
        Features
    }

    internal enum FilterRelationType
    {
        None,
        Is,
        IsNot,
        LessThan,
        MoreThan,
        BeginsWith,
        EndsWith,
        Contains,
        Excludes,
        InRange
    }

    internal class FilterCondition
    {
        private FilterAttributeType filterattributetype = FilterAttributeType.None;
        private FilterRelationType filterrelationtype = FilterRelationType.None;
        private string value = "";

        public FilterAttributeType filterAttributeType
        {
            get
            {
                return this.filterattributetype;
            }
            set
            {
                this.filterattributetype = value;
            }
        }

        public FilterRelationType filterRelationType
        {
            get
            {
                return this.filterrelationtype;
            }
            set
            {
                this.filterrelationtype = value;
            }
        }

        public string Value
        {
            get
            {
                return this.value;
            }
            set
            {
                this.value = value;
            }
        }
    }
}