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
        Features,
        Position,
        Lcn,
        FreeCAMode,
        Type,
        Pcr,
        Pmt,
        Sid,
        Tsid,
        Nid,
        Onid,
        BouquetList,
        Video,
        Audio,
        AudioLanguage

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

        public bool Enable
        {
            get;
            set;
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
                //skip disabled items
                if (!enumurator.Current.Enable)
                {
                    moreitems = enumurator.MoveNext();
                    continue;
                }

                // loop through all filter conditions of unique attribute type. Conditions for unique attribute type ared OR'ed.
                // Groups of unique attribute type are AND'ed.
                FilterAttributeType filterattributetype = enumurator.Current.filterAttributeType;
                match = FilterMatch(s, enumurator.Current);

                while ((moreitems = enumurator.MoveNext()) && filterattributetype == enumurator.Current.filterAttributeType)
                {
                    // if already match continue
                    if (match) continue;

                    //skip disabled items
                    if (!enumurator.Current.Enable)
                    {
                        //moreitems = enumurator.MoveNext();
                        continue;
                    }
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
                    return FilterMatchString(f.filterRelationType,s.Name, f.Value);
                case FilterAttributeType.Provider:
                    return FilterMatchString(f.filterRelationType,s.Provider, f.Value);
                case FilterAttributeType.NetworkName:
                    return FilterMatchString(f.filterRelationType,s.NetworkName, f.Value);
                case FilterAttributeType.CASystemID:
                    return FilterMatchIntList(f.filterRelationType, s.CaSystemIdList, f.Value);
                case FilterAttributeType.Features:
                    return /*FilterMatchFeatures(s,f)*/FilterMatchStringList(s.FeatureList,f);
                case FilterAttributeType.Position:
                    return FilterMatchString(f.filterRelationType, s.Position, f.Value);
                case FilterAttributeType.Lcn:
                    return FilterMatchInt(f.filterRelationType, s.Lcn, f.Value);
                case FilterAttributeType.FreeCAMode:
                    return FilterMatchInt(f.filterRelationType, s.FreeCaMode ? 1 : 0, f.Value);
                case FilterAttributeType.Type:
                    return FilterMatchInt(f.filterRelationType, s.Type, f.Value);
                case FilterAttributeType.Pcr:
                    return FilterMatchInt(f.filterRelationType, s.Pcr, f.Value);
                case FilterAttributeType.Pmt:
                    return FilterMatchInt(f.filterRelationType, s.Pmt, f.Value);
                case FilterAttributeType.Sid:
                    return FilterMatchInt(f.filterRelationType, s.Sid, f.Value);
                case FilterAttributeType.Tsid:
                    return FilterMatchInt(f.filterRelationType, s.Tsid, f.Value);
                case FilterAttributeType.Nid:
                    return FilterMatchInt(f.filterRelationType, s.Nid, f.Value);
                case FilterAttributeType.Onid:
                    return FilterMatchInt(f.filterRelationType, s.Onid, f.Value);
                case FilterAttributeType.BouquetList:
                case FilterAttributeType.Audio:
                    return FilterMatchIntList(f.filterRelationType, s.AudioPidList, f.Value);
                case FilterAttributeType.Video:
                    return FilterMatchIntList(f.filterRelationType, s.VideoPidList, f.Value);
                case FilterAttributeType.AudioLanguage:
                    return FilterMatchStringList(s.AudioLanguageList,f);
                default:
                    return false; // this should not happen
            }
        }

        private bool FilterMatchIntList(FilterRelationType r,SortedSet<int> servicevalue,string filtervalue)
        {
            if (r == FilterRelationType.Contains)
            {
                // each element of filter value is matched against all values in service.
                // If just one elment fails the whole condition fails (AND)
                string[] list = filtervalue.Split(new char[1] { ',' });
                SortedSet<int> intlist = new SortedSet<int>();
                foreach (var item in list)
                {
                    try
                    {
                        intlist.Add(item.StartsWith("0x") ? Convert.ToInt32(item.Substring(2), 16) : Convert.ToInt32(item));
                    }
                    catch (Exception)
                    {
                        //on failure no match
                        return false;
                    }
                }

                foreach (var elememt in intlist)
                {
                    if(!servicevalue.Contains(elememt)) return false;
                }
                return true; // if we reached here all elements matched


            }
            else if (r == FilterRelationType.InRange)
            {
                string[] list = filtervalue.Split(new char[2] { ',','-' });
                
                //only two values are allowed, start and end
                if (list.Length != 2) return false;
                int v1 = list[0].StartsWith("0x") ? Convert.ToInt32(list[0].Substring(2), 16) : Convert.ToInt32(list[0]);
                int v2 = list[1].StartsWith("0x") ? Convert.ToInt32(list[1].Substring(2), 16) : Convert.ToInt32(list[1]);

                // if 
                foreach (var item in servicevalue)
                {
                    if (item >= v1 && item <= v2) return true;
                }
                return false; // if we reached here none of the elements matched
            }
            else
            {
                return false;
            }
        }

        private bool FilterMatchInt(FilterRelationType r,int servicevalue, string filtervalue)
        {
            int value;
            try
            {
                value = Int32.Parse(filtervalue);
            }
            catch (Exception)
            {
                //on failure no match
                return false;
            }
            switch (r)
            {
                case FilterRelationType.Is:
                    return servicevalue == value;
                case FilterRelationType.IsNot:
                    return servicevalue != value;
                case FilterRelationType.LessThan:
                    return servicevalue < value;
                case FilterRelationType.MoreThan:
                    return servicevalue > value;
                default:
                    return false;
            }
        }

        private bool FilterMatchString(FilterRelationType r, string servicevalue, string filtervalue)
        {
            switch (r)
            {
                case FilterRelationType.Is:
                    return servicevalue.CompareTo(filtervalue) == 0;
                case FilterRelationType.IsNot:
                    return servicevalue.CompareTo(filtervalue) != 0;
                case FilterRelationType.LessThan:
                    return servicevalue.CompareTo(filtervalue) < 0;
                case FilterRelationType.MoreThan:
                    return servicevalue.CompareTo(filtervalue) > 0;
                case FilterRelationType.BeginsWith:
                    return servicevalue.StartsWith(filtervalue);
                case FilterRelationType.EndsWith:
                    return servicevalue.EndsWith(filtervalue);
                case FilterRelationType.Contains:
                    return servicevalue.Contains(filtervalue);
                case FilterRelationType.Excludes:
                    return !servicevalue.Contains(filtervalue);
                default:
                    return false; // no match for unsupported relation 
            }
        }

        private bool FilterMatchStringList(string value, FilterCondition f)
        {
            // each element of filter value is matched against all values in service.
            // If just one elment fails the whole condition fails (AND)
            string[] featurelist = f.Value.Split(new char[1] { ',' });

            foreach (var element in featurelist)
            {
                switch (f.filterRelationType)
                {
                    case FilterRelationType.Contains:
                        if (!value.Contains(element)) return false;
                        break;
                    default:
                        return false; // no match for unsupported relation 
                }
            }
            return true; // if we reached here all elements matched
        }

        private bool FilterMatchStringList(SortedSet<string> s,FilterCondition f)
        {
            // each element of filter value is matched against all values in service.
            // If just one elment fails the whole condition fails (AND)
            string[] list = f.Value.Split(new char[1] { ',' });

            foreach (var element in list)
            {
                switch (f.filterRelationType)
                {
                    case FilterRelationType.Contains:
                        if (!s.Contains(element)) return false;
                        break;
                    default:
                        return false; // no match for unsupported relation 
                }
            }
            return true; // if we reached here all elements matched
        }

    }
}