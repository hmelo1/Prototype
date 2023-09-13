using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "Scriptable Objects/Beam")]
public class ChipData_Beam : ChipData
{
    private BeamEffect beamEffect;

    public override void Initialize(GameObject obj)
    {
        beamEffect = obj.GetComponent<BeamEffect>();
    }

    public override void TriggerAbility(Transform userTransform)
    {
        beamEffect.FireBeam(userTransform, this.Damage);
    }
}
