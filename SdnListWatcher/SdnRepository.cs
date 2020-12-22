using System;
using Common.Interfaces;
using Common.Models;

namespace SdnListWatcher
{
    public class SdnRepository : ISdnRepository
    {
        private readonly ISdnStorage _sdnStorage;
        private DateTime _lastUpdateDate;

        public SdnRepository(ISdnStorage sdnStorage)
        {
            // Mock last update timestamp
            _lastUpdateDate = DateTime.Now.AddDays(-5);

            _sdnStorage = sdnStorage;
        }

        public DateTime GetLastUpdateDateTime() => _lastUpdateDate;
        public void SetLastUpdateDateTime(DateTime dateTime) => _lastUpdateDate = dateTime;
        public SdnEntry GetSdnByLastName(string lastName) => _sdnStorage.GetSdnByLastName(lastName);
        public void Add(DateTime dateTime, SdnEntry sdnEntry) => _sdnStorage.AddSdn(dateTime, sdnEntry);
        public void AddMany(SdnList items) => _sdnStorage.StoreSdn(items);
    }
}