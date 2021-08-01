using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackEnd.Data;

namespace BackEnd.Infrastructure
{
    public static class EntityExtensions
    {
        public static ConferenceDTO.SpeakerResponse MapSpeakerResponse(this Speaker speaker)
            => new ConferenceDTO.SpeakerResponse
                {
                    Id = speaker.Id,
                    Name = speaker.Name,
                    Bio = speaker.Bio,
                    WebSite = speaker.WebSite,
                    Sessions = speaker.SessionSpeakers?.Select(ss => new ConferenceDTO.Session
                    {
                        Id = ss.SessionId,
                        Title = ss.Session.Title,
                    }).ToList()
                };
        
    }
}
