using System;

namespace Aidon.Tools.Gollum.ReviewBoard
{
    public class ReviewIdDiscoveredEventArgs : EventArgs
    {
        public string ReviewBoardTicketLink { get; set; }
    }
}