public class ClusterPointSlider : PointSliderBaseClass
{
    protected override ref int GetPoint()
    {
        return ref targetingSystem.clusterTargetPoint;
    }
}