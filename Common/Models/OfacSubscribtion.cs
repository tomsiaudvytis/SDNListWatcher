using System.Xml.Serialization;
using System.Collections.Generic;

// Auto generated
namespace Common.Models
{
    [XmlRoot(ElementName = "link", Namespace = "http://www.w3.org/2005/Atom")]
    public class Link
    {
        [XmlAttribute(AttributeName = "href", Namespace = "")]
        public string Href;

        [XmlAttribute(AttributeName = "rel", Namespace = "")]
        public string Rel;

        [XmlAttribute(AttributeName = "type", Namespace = "")]
        public string Type;
    }

    [XmlRoot(ElementName = "guid", Namespace = "")]
    public class Guid
    {
        [XmlAttribute(AttributeName = "isPermaLink", Namespace = "")]
        public string IsPermaLink;
    }

    [XmlRoot(ElementName = "item", Namespace = "")]
    public class Item
    {
        [XmlElement(ElementName = "title", Namespace = "")]
        public string Title;

        [XmlElement(ElementName = "description", Namespace = "")]
        public string Description;

        [XmlElement(ElementName = "pubDate", Namespace = "")]
        public string PubDate;

        [XmlElement(ElementName = "link", Namespace = "")]
        public string Link;

        [XmlElement(ElementName = "guid", Namespace = "")]
        public Guid Guid;
    }

    [XmlRoot(ElementName = "channel", Namespace = "")]
    public class Channel
    {
        [XmlElement(ElementName = "pubDate", Namespace = "")]
        public string PubDate;

        [XmlElement(ElementName = "link", Namespace = "http://www.w3.org/2005/Atom")]
        public Link Link;

        [XmlElement(ElementName = "title", Namespace = "")]
        public string Title;

        [XmlElement(ElementName = "description", Namespace = "")]
        public string Description;

        [XmlElement(ElementName = "link", Namespace = "")]
        public string Link2;

        [XmlElement(ElementName = "ttl", Namespace = "")]
        public string Ttl;

        [XmlElement(ElementName = "language", Namespace = "")]
        public string Language;

        [XmlElement(ElementName = "webMaster", Namespace = "")]
        public string WebMaster;

        [XmlElement(ElementName = "item", Namespace = "")]
        public List<Item> Items;
    }

    [XmlRoot(ElementName = "rss", Namespace = "")]
    public class OfacFeedSubscription
    {
        [XmlElement(ElementName = "channel", Namespace = "")]
        public Channel Channel;

        [XmlAttribute(AttributeName = "version", Namespace = "")]
        public string Version;

        [XmlAttribute(AttributeName = "xsi", Namespace = "http://www.w3.org/2000/xmlns/")]
        public string Xsi;

        [XmlAttribute(AttributeName = "ofac", Namespace = "http://www.w3.org/2000/xmlns/")]
        public string Ofac;

        [XmlAttribute(AttributeName = "atom", Namespace = "http://www.w3.org/2000/xmlns/")]
        public string Atom;
    }
}