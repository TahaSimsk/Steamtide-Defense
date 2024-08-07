public class HighestHpPointSlider : PointSliderBaseClass
{
    protected override ref int GetPoint()
    {
        return ref targetingSystem.highestHpTargetPoint;
    }
}
