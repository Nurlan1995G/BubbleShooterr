namespace Assets._project.CodeBase.Interface
{
    public interface IBallPosition
    {
        TypeBallColor TypeBallColor { get; }

        Point GetCurrentPoint();
        void RemoveFromCurrentPoint();
        void SetCurrentPoint(Point point);
    }
}
