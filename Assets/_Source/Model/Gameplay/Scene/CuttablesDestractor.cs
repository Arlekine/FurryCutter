using UnitySpriteCutter;

public class CuttablesDestractor : TypedTrigger<Cuttable>
{
    protected override void OnEnterTriggered(Cuttable other)
    {
        Destroy(other.gameObject);
    }
}
