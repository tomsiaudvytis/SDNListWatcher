using System;
using System.Collections.Generic;
using Common.Interfaces;
using Common.Models;

namespace SdnListWatcher
{
    public class MockDatabase : IMockDatabase
    {
        private DateTime _lastUpdateDate;
        private readonly Dictionary<string, SdnEntry> _sdnEntries = new Dictionary<string, SdnEntry>();

        private readonly Dictionary<DateTime, List<SdnEntry>> _changeHistory =
            new Dictionary<DateTime, List<SdnEntry>>();

        public MockDatabase()
        {
            _lastUpdateDate = DateTime.Now.AddDays(-3);
        }

        public DateTime GetLastUpdateDateTime() => _lastUpdateDate;
        public void SetLastUpdateDateTime(DateTime dateTime) => _lastUpdateDate = dateTime;
        public SdnEntry GetEntryByLastName(string lastName) => _sdnEntries[lastName];

        public void AddSdnRecordToHistory(DateTime dateTime, SdnEntry sdnEntry)
        {
            if (_changeHistory.ContainsKey(dateTime))
            {
                var existingItems = _changeHistory[dateTime];
                existingItems.Add(sdnEntry);
            }
            else
            {
                _changeHistory[dateTime] = new List<SdnEntry>()
                {
                    sdnEntry
                };
            }
        }

        public void UpdateExistingSdnItems(SdnList items)
        {
            foreach (var entry in items.SdnEntry)
            {
                _sdnEntries[entry.LastName] = entry;
            }
        }
    }
}