using System;
using System.Collections.Generic;
using Common.Models;

namespace Common.Interfaces
{
    public interface ISdnRepository
    {
        DateTime GetLastUpdateDateTime();
        void SetLastUpdateDateTime(DateTime dateTime);
        void AddMany(SdnList sdnList);
        void Add(DateTime dateTime, SdnEntry sdnEntry);
        SdnEntry GetSdnByLastName(string lastName);
    }
}