public class FastestHpPointSlider : PointSliderBaseClass
{
    protected override ref int GetPoint()
    {
        return ref targetingSystem.fastestTargetPoint;
    }
}
