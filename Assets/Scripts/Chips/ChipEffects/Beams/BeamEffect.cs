using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeamEffect : MonoBehaviour
{
    public virtual void FireBeam(Transform userTransform, int damage)
    {
        // Fire a raycast from the user's transform position
        RaycastHit hit;
        if (Physics.Raycast(userTransform.position, userTransform.forward, out hit))
        {
            hit.collider.gameObject.GetComponent<Entity>().HandleDamageTaken(damage);
        }
    }
}
