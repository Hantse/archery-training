using Shared.Contracts.Enums;
using Shared.Contracts.Response;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Service.API.Mocks
{
    public static class SessionMocks
    {
        public static SessionHistoryItem[] GetUserSessionHistoryMocks()
        {
            var sessions = new List<SessionHistoryItem>();

            sessions.Add(new SessionHistoryItem()
            {
                Id = Guid.NewGuid(),
                Date = DateTime.UtcNow.AddDays(-14),
                Duration = TimeSpan.FromMinutes(75),
                Set = 4,
                Status = SessionStatus.FINISH,
                Total = 347,
                TotalArrow = 40,
                Type = SessionType.SOLO_TRAINING
            });

            sessions.Add(new SessionHistoryItem()
            {
                Id = Guid.NewGuid(),
                Date = DateTime.UtcNow.AddDays(-12),
                Duration = TimeSpan.FromMinutes(45),
                Set = 2,
                Status = SessionStatus.FINISH,
                Total = 207,
                TotalArrow = 30,
                Performance = Performance.VERY_LOWER,
                Type = SessionType.SOLO_TRAINING
            });

            sessions.Add(new SessionHistoryItem()
            {
                Id = Guid.NewGuid(),
                Date = DateTime.UtcNow.AddDays(-7),
                Duration = TimeSpan.FromMinutes(85),
                Set = 7,
                Status = SessionStatus.FINISH,
                Total = 578,
                TotalArrow = 70,
                Performance = Performance.VERY_UPPER,
                Type = SessionType.SOLO_TRAINING
            });

            sessions.Add(new SessionHistoryItem()
            {
                Id = Guid.NewGuid(),
                Date = DateTime.UtcNow.AddDays(-2),
                Duration = TimeSpan.FromMinutes(35),
                Set = 3,
                Status = SessionStatus.FINISH,
                Total = 148,
                TotalArrow = 18,
                Performance = Performance.EQUAL,
                Type = SessionType.DUO_TRAINING
            });

            sessions.Add(new SessionHistoryItem()
            {
                Id = Guid.NewGuid(),
                Date = DateTime.UtcNow,
                Set = 4,
                Status = SessionStatus.IN_PROGRESS,
                Total = 248,
                TotalArrow = 32,
                Performance = Performance.LOWER,
                Type = SessionType.SOLO_TRAINING
            });

            return sessions.OrderBy(s => s.Date).ToArray();
        }
    }
}
