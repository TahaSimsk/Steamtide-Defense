public class LowestHpPointSlider : PointSliderBaseClass
{
    protected override ref int GetPoint()
    {
        return ref targetingSystem.lowestHpTargetPoint;
    }
}
