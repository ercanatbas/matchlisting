using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace MatchList.Domain.Matches.Enums
{
    public enum EventType
    {
        Unknown                                                                                   = 0,
        [EnumMember(Value = "Soccer")] [XmlEnum(           "Soccer")]            Soccer           = 1,
        [EnumMember(Value = "Basketball")] [XmlEnum(       "Basketball")]        Basketball       = 2,
        [EnumMember(Value = "American Football")] [XmlEnum("American Football")] AmericanFootball = 3
    }
}