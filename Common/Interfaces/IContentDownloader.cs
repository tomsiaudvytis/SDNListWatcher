namespace Common.Interfaces
{
    public interface IContentDownloader
    {
        string DownloadOfacSubscriptionPageContent();
        string DownloadUpdatedSdns();
        string DownloadContent(string itemLink);
    }
}