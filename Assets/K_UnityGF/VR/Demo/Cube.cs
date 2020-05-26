using K_UnityGF;

public class Cube : GrabItem
{
    protected override void GrabEnd()
    {
        transform.parent = null;
        _rig.useGravity = true;
        _rig.isKinematic = false;
        _collider.isTrigger = false;
    }
}
