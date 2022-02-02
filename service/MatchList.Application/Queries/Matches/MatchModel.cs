using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using System.Xml.Serialization;
using MatchList.Domain.Matches.Enums;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace MatchList.Application.Queries.Matches
{
    [XmlRoot(ElementName = "element")]
    public class MatchModel
    {
        [JsonProperty("Id")]
        public long EventId { get; set; }

        [Newtonsoft.Json.JsonConverter(typeof(StringEnumConverter))]
        public EventType EventType { get; set; }

        public string Country  { get; set; }
        public string League   { get; set; }
        public string HomeTeam { get; set; }
        public string AwayTeam { get; set; }

        [XmlIgnore]
        public DateTime EventTime { get; set; }

        [XmlElement("EventTime")]
        [System.Text.Json.Serialization.JsonIgnore]
        public string EventTimeString
        {
            get => EventTime.ToString("yyyy-MM-dd HH:mm:ss");
            set => EventTime = value is null ? EventTime : DateTime.Parse(value);
        }

        [System.Text.Json.Serialization.JsonIgnore]
        public bool IsProcessed { get; set; }

        public void Update(EventType eventType, string country, string league, string homeTeam, string awayTeam, DateTime eventTime)
        {
            EventType = eventType;
            Country   = country;
            League    = league;
            HomeTeam  = homeTeam;
            AwayTeam  = awayTeam;
            EventTime = eventTime;
        }

        public void SetProcessed()
        {
            IsProcessed = true;
        }
    }

    [XmlRoot(ElementName = "root")]
    public class XmlMatchModel
    {
        [XmlElement(ElementName = "element")]
        public List<MatchModel> MatchModels { get; set; }
    }
}