public class SlowestPointSlider : PointSliderBaseClass
{
    protected override ref int GetPoint()
    {
        return ref targetingSystem.slowestTargetPoint;
    }
}
