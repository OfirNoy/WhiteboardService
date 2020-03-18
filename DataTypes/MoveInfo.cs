namespace WhiteboardService.DataTypes
{
    public class MoveInfo
    {
        public Point MoveTo { get; set; }
        public Point LineTo { get; set; }
        public string Color { get; set; }
        public int LineWidth { get; set; }
        public int LineType { get; set; }

    }
}