using System;
using System.Collections.Generic;

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

    internal class FilterConditionComparer : Comparer<FilterCondition>
    {
        public override int Compare(FilterCondition x, FilterCondition y)
        {
            if (x.filterAttributeType != y.filterAttributeType) return x.filterAttributeType.CompareTo( y.filterAttributeType) ;
            else if (x.filterRelationType != y.filterRelationType) return x.filterRelationType.CompareTo(y.filterRelationType);
            else return x.Value.CompareTo(y.Value);
        }
    }

    internal class FilterContext
    {
        public FilterContext()
        {
            FilterConditionSet = new SortedSet<FilterCondition>(new FilterConditionComparer());
        }

        public bool Exclude
        {
            get;
            set;
        }

        public SortedSet<FilterCondition> FilterConditionSet
        {
            get;
            set;
        }
    }
}