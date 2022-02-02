using System;
using MatchList.Domain.Entities;
using MatchList.Domain.Matches.Enums;

namespace MatchList.Domain.Matches
{
    public class Match : EntityBase<int>, IAggregateRoot
    {
        public long EventId { get; set; }

        public EventType EventType { get; set; }
        public string    Country   { get; set; }
        public string    League    { get; set; }
        public string    HomeTeam  { get; set; }
        public string    AwayTeam  { get; set; }
        public DateTime  EventTime { get; set; }

        public void Update(EventType eventType, string country, string league, string homeTeam, string awayTeam, DateTime eventTime)
        {
            EventType = eventType;
            Country   = country;
            League    = league;
            HomeTeam  = homeTeam;
            AwayTeam  = awayTeam;
            EventTime = eventTime;
        }
    }
}