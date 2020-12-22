using System;
using Common.Models;

namespace Common.Interfaces
{
    public interface ISdnStorage
    {
        SdnEntry GetSdnByLastName(string lastName);
        void AddSdn(in DateTime dateTime, SdnEntry sdnEntry);
        void StoreSdn(SdnList items);
    }
}