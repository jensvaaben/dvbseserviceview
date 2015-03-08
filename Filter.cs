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

        public void ApplyFilter(List<Service> listin, List<Service> listout)
        {
            listout.Clear();
            foreach (var item in listin)
            {
                if (FilterMatch(item, this.FilterConditionSet))
                {
                    if (!this.Exclude)
                    {
                        listout.Add(item);
                    }
                }
                else
                {
                    if (this.Exclude)
                    {
                        listout.Add(item);
                    }
                }

            }
        }

        private bool FilterMatch(Service s, SortedSet<FilterCondition> f)
        {
            SortedSet<FilterCondition>.Enumerator enumurator = f.GetEnumerator();
            bool moreitems = enumurator.MoveNext();
            bool match = false;

            while (moreitems)
            {
                // loop through all filter conditions of unique attribute type. Conditions for unique attribute type ared OR'ed.
                // Groups of unique attribute type are AND'ed.
                FilterAttributeType filterattributetype = enumurator.Current.filterAttributeType;
                match = FilterMatch(s, enumurator.Current);
                while ((moreitems = enumurator.MoveNext()) && filterattributetype == enumurator.Current.filterAttributeType && !match)
                {
                    match = FilterMatch(s, enumurator.Current);
                }
                // if not at least match for unique attribute type retrurn false. Otherwise proceed with next attribute type
                if (!match) return false;
            };

            // if we reached here all groups of unique attribute type AND'ed matched.
            return true;
        }

        private bool FilterMatch(Service s, FilterCondition f)
        {
            switch (f.filterAttributeType)
            {
                case FilterAttributeType.Name:
                    return FilterMatchName(s,f);
                case FilterAttributeType.Provider:
                    return FilterMatchProvider(s,f);
                case FilterAttributeType.NetworkName:
                    return FilterMatchNetworkName(s,f);
                case FilterAttributeType.CASystemID:
                    return FilterMatchCASystemID(s,f);
                case FilterAttributeType.Features:
                    return FilterMatchFeatures(s,f);
                default:
                    return false; // this should not happen
            }
        }

        private bool FilterMatchName(Service s, FilterCondition f)
        {
            switch (f.filterRelationType)
            {
                case FilterRelationType.Is:
                    return s.Name.CompareTo(f.Value)==0;
                case FilterRelationType.IsNot:
                    return s.Name.CompareTo(f.Value) != 0;
                case FilterRelationType.LessThan:
                    return s.Name.CompareTo(f.Value) < 0;
                case FilterRelationType.MoreThan:
                    return s.Name.CompareTo(f.Value) > 0;
                case FilterRelationType.BeginsWith:
                    return s.Name.StartsWith(f.Value);
                case FilterRelationType.EndsWith:
                    return s.Name.EndsWith(f.Value);
                case FilterRelationType.Contains:
                    return s.Name.Contains(f.Value);
                case FilterRelationType.Excludes:
                    return !s.Name.Contains(f.Value);
                default:
                    return false; // no match for unsupported relation 
            }
        }

        private bool FilterMatchProvider(Service s, FilterCondition f)
        {
            switch (f.filterRelationType)
            {
                case FilterRelationType.Is:
                    return s.Provider.CompareTo(f.Value) == 0;
                case FilterRelationType.IsNot:
                    return s.Provider.CompareTo(f.Value) != 0;
                case FilterRelationType.LessThan:
                    return s.Provider.CompareTo(f.Value) < 0;
                case FilterRelationType.MoreThan:
                    return s.Provider.CompareTo(f.Value) > 0;
                case FilterRelationType.BeginsWith:
                    return s.Provider.StartsWith(f.Value);
                case FilterRelationType.EndsWith:
                    return s.Provider.EndsWith(f.Value);
                case FilterRelationType.Contains:
                    return s.Provider.Contains(f.Value);
                case FilterRelationType.Excludes:
                    return !s.Provider.Contains(f.Value);
                default:
                    return false; // no match for unsupported relation 
            }
        }

        private bool FilterMatchNetworkName(Service s, FilterCondition f)
        {
            switch (f.filterRelationType)
            {
                case FilterRelationType.Is:
                    return s.NetworkName.CompareTo(f.Value) == 0;
                case FilterRelationType.IsNot:
                    return s.NetworkName.CompareTo(f.Value) != 0;
                case FilterRelationType.LessThan:
                    return s.NetworkName.CompareTo(f.Value) < 0;
                case FilterRelationType.MoreThan:
                    return s.NetworkName.CompareTo(f.Value) > 0;
                case FilterRelationType.BeginsWith:
                    return s.NetworkName.StartsWith(f.Value);
                case FilterRelationType.EndsWith:
                    return s.NetworkName.EndsWith(f.Value);
                case FilterRelationType.Contains:
                    return s.NetworkName.Contains(f.Value);
                case FilterRelationType.Excludes:
                    return !s.NetworkName.Contains(f.Value);
                default:
                    return false; // no match for unsupported relation 
            }
        }

        private bool FilterMatchCASystemID(Service s, FilterCondition f)
        {
            return true;
        }

        private bool FilterMatchFeatures(Service s, FilterCondition f)
        {
            switch (f.filterRelationType)
            {
                case FilterRelationType.Contains:
                    return s.FeatureList.Contains(f.Value);
                default:
                    return false; // no match for unsupported relation 
            }
        }
    }
}