using System;
using System.Collections.Generic;
using Common.Models;

namespace Common.Interfaces
{
    public interface IMockDatabase
    {
        DateTime GetLastUpdateDateTime();
        void SetLastUpdateDateTime(DateTime dateTime);
        void UpdateExistingSdnItems(SdnList sdnList);
        void AddSdnRecordToHistory(DateTime dateTime, SdnEntry sdnEntry);
        SdnEntry GetEntryByLastName(string lastName);
    }
}