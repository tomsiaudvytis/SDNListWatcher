using System;
using System.Collections.Generic;
using Common.Interfaces;
using Common.Models;

namespace SdnListWatcher
{
    public class SdnStorage : ISdnStorage
    {
        private readonly Dictionary<string, SdnEntry> _sdnEntries = new Dictionary<string, SdnEntry>();

        // Contains all data we need
        private readonly Dictionary<DateTime, List<SdnEntry>> _sdnHistory =
            new Dictionary<DateTime, List<SdnEntry>>();

        public SdnEntry GetSdnByLastName(string lastName)
        {
            return _sdnEntries[lastName];
        }

        public void AddSdn(in DateTime dateTime, SdnEntry sdnEntry)
        {
            if (_sdnHistory.ContainsKey(dateTime))
            {
                var existingItems = _sdnHistory[dateTime];
                existingItems.Add(sdnEntry);
            }
            else
            {
                _sdnHistory[dateTime] = new List<SdnEntry>()
                {
                    sdnEntry
                };
            }
        }

        public void StoreSdn(SdnList items)
        {
            foreach (var entry in items.SdnEntry)
            {
                _sdnEntries[entry.LastName] = entry;
            }
        }
    }
}