using System.Collections.Generic;

namespace ConferenceDTO
{
    public class SessionResponse : Session
    {
        public IList<Speaker> Speakers { get; set; } = new List<Speaker>();

        public Track Track { get; set; }
    }
}