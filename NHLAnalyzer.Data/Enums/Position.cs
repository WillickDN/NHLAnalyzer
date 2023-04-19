namespace NHLAnalyzer.Data.Enums
{
    public enum Position
    {
        None = 0,
        Center = 1,
        LeftWing = 2,
        RightWing = 4,
        Defense = 8,
        Goalie = 16
    }

    public static class PositionHelper
    {
        public static Position GetPositionFromString(string positionString)
        {
            switch (positionString)
            {
                case "C":
                    return Position.Center;
                case "LW":
                    return Position.LeftWing;
                case "RW":
                    return Position.RightWing;
                case "D":
                    return Position.Defense;
                case "G":
                    return Position.Goalie;
                default:
                    return Position.None;
            }
        }
    }
}
