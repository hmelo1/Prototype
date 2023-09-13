using System.Collections.Generic;
using UnityEngine;

namespace Lightbug.Utilities
{

    /// <summary>
    /// This component represents a capsule collider in a 2D world.
    /// </summary>
    public class BoxColliderComponent2D : ColliderComponent2D
    {
        BoxCollider2D boxCollider = null;

        public override Vector3 Size
        {
            get
            {
                return boxCollider.size;
            }
            set
            {
                boxCollider.size = value;
            }
        }

        public override Vector3 BoundsSize => boxCollider.bounds.size;

        public override Vector3 Offset
        {
            get => boxCollider.offset;
            set => boxCollider.offset = value;
        }

        protected override int InternalOverlapBody(Vector3 position, Quaternion rotation, float bottomOffset, Collider2D[] unfilteredResults, List<Collider2D> filteredResults, OverlapFilterDelegate2D filter)
        {
            Vector3 bottom = position + rotation * Vector3.up * (Size.x / 2f + bottomOffset);

            return PhysicsUtilities.OverlapBox(
                bottom,
                Size,
                rotation,
                unfilteredResults,
                filteredResults,
                filter
            );
        }

        protected override void Awake()
        {

            boxCollider = gameObject.GetOrAddComponent<BoxCollider2D>(true);
            collider = boxCollider;

            base.Awake();

        }


    }



}
