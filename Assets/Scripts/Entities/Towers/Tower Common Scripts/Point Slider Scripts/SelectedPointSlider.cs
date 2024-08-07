public class SelectedPointSlider : PointSliderBaseClass
{
    protected override ref int GetPoint()
    {
        return ref targetingSystem.selectedTargetPoint;
    }
}
