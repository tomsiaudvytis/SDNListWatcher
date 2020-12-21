using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Common.Models
{
    [XmlRoot(ElementName = "publshInformation", Namespace = "http://tempuri.org/sdnList.xsd")]
    public class PublshInformation
    {
        [XmlElement(ElementName = "Publish_Date", Namespace = "http://tempuri.org/sdnList.xsd")]
        public string PublishDate { get; set; }

        [XmlElement(ElementName = "Record_Count", Namespace = "http://tempuri.org/sdnList.xsd")]
        public string RecordCount { get; set; }
    }

    [XmlRoot(ElementName = "programList", Namespace = "http://tempuri.org/sdnList.xsd")]
    public class ProgramList
    {
        [XmlElement(ElementName = "program", Namespace = "http://tempuri.org/sdnList.xsd")]
        public string Program { get; set; }
    }

    [XmlRoot(ElementName = "aka", Namespace = "http://tempuri.org/sdnList.xsd")]
    public class Aka
    {
        [XmlElement(ElementName = "uid", Namespace = "http://tempuri.org/sdnList.xsd")]
        public string Uid { get; set; }

        [XmlElement(ElementName = "type", Namespace = "http://tempuri.org/sdnList.xsd")]
        public string Type { get; set; }

        [XmlElement(ElementName = "category", Namespace = "http://tempuri.org/sdnList.xsd")]
        public string Category { get; set; }

        [XmlElement(ElementName = "lastName", Namespace = "http://tempuri.org/sdnList.xsd")]
        public string LastName { get; set; }
    }

    [XmlRoot(ElementName = "akaList", Namespace = "http://tempuri.org/sdnList.xsd")]
    public class AkaList
    {
        [XmlElement(ElementName = "aka", Namespace = "http://tempuri.org/sdnList.xsd")]
        public Aka Aka { get; set; }
    }

    [XmlRoot(ElementName = "address", Namespace = "http://tempuri.org/sdnList.xsd")]
    public class Address
    {
        [XmlElement(ElementName = "uid", Namespace = "http://tempuri.org/sdnList.xsd")]
        public string Uid { get; set; }

        [XmlElement(ElementName = "city", Namespace = "http://tempuri.org/sdnList.xsd")]
        public string City { get; set; }

        [XmlElement(ElementName = "country", Namespace = "http://tempuri.org/sdnList.xsd")]
        public string Country { get; set; }
    }

    [XmlRoot(ElementName = "addressList", Namespace = "http://tempuri.org/sdnList.xsd")]
    public class AddressList
    {
        [XmlElement(ElementName = "address", Namespace = "http://tempuri.org/sdnList.xsd")]
        public Address Address { get; set; }
    }

    [XmlRoot(ElementName = "sdnEntry", Namespace = "http://tempuri.org/sdnList.xsd")]
    public class SdnEntry
    {
        [XmlElement(ElementName = "uid", Namespace = "http://tempuri.org/sdnList.xsd")]
        public string Uid { get; set; }

        [XmlElement(ElementName = "lastName", Namespace = "http://tempuri.org/sdnList.xsd")]
        public string LastName { get; set; }
        
        [XmlElement(ElementName = "firstName", Namespace = "http://tempuri.org/sdnList.xsd")]
        public string FirstName { get; set; }

        [XmlElement(ElementName = "sdnType", Namespace = "http://tempuri.org/sdnList.xsd")]
        public string SdnType { get; set; }

        [XmlElement(ElementName = "programList", Namespace = "http://tempuri.org/sdnList.xsd")]
        public ProgramList ProgramList { get; set; }

        [XmlElement(ElementName = "akaList", Namespace = "http://tempuri.org/sdnList.xsd")]
        public AkaList AkaList { get; set; }

        [XmlElement(ElementName = "addressList", Namespace = "http://tempuri.org/sdnList.xsd")]
        public AddressList AddressList { get; set; }

        public SdnChangeType? SdnChangeType { get; set; }
    }

    [XmlRoot(ElementName = "address", Namespace = "http://tempuri.org/sdnList.xsd")]
    public class Address2
    {
        [XmlElement(ElementName = "uid", Namespace = "http://tempuri.org/sdnList.xsd")]
        public string Uid { get; set; }

        [XmlElement(ElementName = "address1", Namespace = "http://tempuri.org/sdnList.xsd")]
        public string Address1 { get; set; }

        [XmlElement(ElementName = "city", Namespace = "http://tempuri.org/sdnList.xsd")]
        public string City { get; set; }

        [XmlElement(ElementName = "postalCode", Namespace = "http://tempuri.org/sdnList.xsd")]
        public string PostalCode { get; set; }

        [XmlElement(ElementName = "country", Namespace = "http://tempuri.org/sdnList.xsd")]
        public string Country { get; set; }
    }

    [XmlRoot(ElementName = "addressList", Namespace = "http://tempuri.org/sdnList.xsd")]
    public class AddressList2
    {
        [XmlElement(ElementName = "address", Namespace = "http://tempuri.org/sdnList.xsd")]
        public Address2 Address2 { get; set; }
    }

    [XmlRoot(ElementName = "sdnEntry", Namespace = "http://tempuri.org/sdnList.xsd")]
    public class SdnEntry2
    {
        [XmlElement(ElementName = "uid", Namespace = "http://tempuri.org/sdnList.xsd")]
        public string Uid { get; set; }

        [XmlElement(ElementName = "lastName", Namespace = "http://tempuri.org/sdnList.xsd")]
        public string LastName { get; set; }

        [XmlElement(ElementName = "sdnType", Namespace = "http://tempuri.org/sdnList.xsd")]
        public string SdnType { get; set; }

        [XmlElement(ElementName = "programList", Namespace = "http://tempuri.org/sdnList.xsd")]
        public ProgramList ProgramList { get; set; }

        [XmlElement(ElementName = "akaList", Namespace = "http://tempuri.org/sdnList.xsd")]
        public AkaList AkaList { get; set; }

        [XmlElement(ElementName = "addressList", Namespace = "http://tempuri.org/sdnList.xsd")]
        public AddressList2 AddressList2 { get; set; }
    }

    [XmlRoot(ElementName = "sdnList", Namespace = "http://tempuri.org/sdnList.xsd")]
    public class SdnList
    {
        [XmlElement(ElementName = "publshInformation", Namespace = "http://tempuri.org/sdnList.xsd")]
        public PublshInformation PublshInformation { get; set; }

        [XmlElement(ElementName = "sdnEntry", Namespace = "http://tempuri.org/sdnList.xsd")]
        public List<SdnEntry> SdnEntry { get; set; }

        [XmlAttribute(AttributeName = "xsi", Namespace = "http://www.w3.org/2000/xmlns/")]
        public String Xsi { get; set; }

        [XmlAttribute(AttributeName = "xmlns", Namespace = "")]
        public String Xmlns { get; set; }
    }
}