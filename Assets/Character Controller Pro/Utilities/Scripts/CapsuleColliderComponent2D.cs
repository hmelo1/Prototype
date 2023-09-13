using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lightbug.Utilities
{

    /// <summary>
    /// This component represents a capsule collider in a 2D world.
    /// </summary>
    public class CapsuleColliderComponent2D : ColliderComponent2D
    {
        CapsuleCollider2D capsuleCollider = null;

        public override Vector3 Size
        {
            get
            {
                return capsuleCollider.size;
            }
            set
            {
                capsuleCollider.size = value;
            }
        }

        public override Vector3 BoundsSize => capsuleCollider.bounds.size;


        public override Vector3 Offset
        {
            get
            {
                return capsuleCollider.offset;
            }
            set
            {
                capsuleCollider.offset = value;
            }
        }

        protected override int InternalOverlapBody(Vector3 position, Quaternion rotation, float bottomOffset, Collider2D[] unfilteredResults, List<Collider2D> filteredResults, OverlapFilterDelegate2D filter)
        {
            Vector3 bottom = position + rotation * Vector3.up * (Size.x / 2f + bottomOffset);
            Vector3 top = position + rotation * Vector3.up * (Size.y + bottomOffset - Size.x / 2f);

            return PhysicsUtilities.OverlapCapsule(
                bottom,
                top,
                Size.x / 2f,
                unfilteredResults,
                filteredResults,
                filter
            );
        }


        protected override void Awake()
        {

            capsuleCollider = gameObject.GetOrAddComponent<CapsuleCollider2D>();
            collider = capsuleCollider;

            base.Awake();

        }


    }



}
