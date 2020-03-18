using System;
using System.Collections.Generic;

namespace WhiteboardService.DataTypes
{
    public class BoardUpdate
    {
        public DateTime Timestamp { get; /*private*/ set; } = DateTime.Now;
        public string BoardId { get; /*private*/ set; }
        public string User { get; /*private*/ set; }
        public int UpdateType { get; /*private*/ set; } // Add, Delete
        public List<MoveInfo> Moves { get; /*private*/ set; } = new List<MoveInfo>();
        public string Color { get; /*private*/ set; }
        public string LineType { get; /*private*/ set; }
    }
}