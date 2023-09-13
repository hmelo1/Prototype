using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lightbug.Utilities
{

    /// <summary>
    /// An implementation of a ColliderComponent for 2D colliders.
    /// </summary>
    public abstract class ColliderComponent2D : ColliderComponent
    {
        protected new Collider2D collider = null;

        public RaycastHit2D[] UnfilteredHits { get; protected set; } = new RaycastHit2D[20];
        public List<RaycastHit2D> FilteredHits { get; protected set; } = new List<RaycastHit2D>(10);

        public Collider2D[] UnfilteredOverlaps { get; protected set; } = new Collider2D[20];
        public List<Collider2D> FilteredOverlaps { get; protected set; } = new List<Collider2D>(10);
        protected CollisionFilterDelegate2D internalHitFilter;
        protected OverlapFilterDelegate2D internalOverlapFilter;

        public PhysicsMaterial2D Material
        {
            get
            {
                return collider.sharedMaterial;
            }
            set
            {
                collider.sharedMaterial = value;
            }
        }

        protected abstract int InternalOverlapBody(Vector3 position, Quaternion rotation, float bottomOffset, Collider2D[] unfilteredResults, List<Collider2D> filteredResults, OverlapFilterDelegate2D filter);

        public sealed override int OverlapBody(Vector3 position, Quaternion rotation, float bottomOffset)
        {
            return InternalOverlapBody(position, rotation, bottomOffset, UnfilteredOverlaps, FilteredOverlaps, internalOverlapFilter);
        }

        public override bool ComputePenetration(ref Vector3 position, ref Quaternion rotation, PenetrationDelegate Action)
        {
            int overlaps = OverlapBody(position, rotation, BottomOffset);

            if (overlaps == 0)
                return false;

            for (int i = 0; i < overlaps; i++)
            {
                Collider2D otherCollider = FilteredOverlaps[i];
                if (otherCollider == collider)
                    continue;

                if (otherCollider.isTrigger)
                    continue;

                ColliderDistance2D colliderDistance2D = Physics2D.Distance(collider, otherCollider);      
                if (!colliderDistance2D.isOverlapped)   // only negative distances (overlaps) are allowed
                    continue;
                
                Action?.Invoke(ref position, ref rotation, otherCollider.transform, -colliderDistance2D.normal, colliderDistance2D.distance);
            }

            return true;
        }
                
        protected bool InternalHitFilter(RaycastHit2D raycastHit)
        {
            if (raycastHit.collider == collider)
                return false;

            if (raycastHit.collider.isTrigger)
                return false;

            return true;
        }

        protected bool InternalOverlapFilter(Collider2D collider)
        {
            if (collider == this.collider)
                return false;

            if (collider.isTrigger)
                return false;

            return true;
        }

        protected override void Awake()
        {
            base.Awake();

            PhysicsMaterial2D material = new PhysicsMaterial2D("Frictionless 2D");
            material.friction = 0f;
            material.bounciness = 0f;

            collider.sharedMaterial = material;
            this.collider.hideFlags = HideFlags.NotEditable;

            internalHitFilter = InternalHitFilter;
            internalOverlapFilter = InternalOverlapFilter;
        }

        protected override void OnEnable()
        {
            collider.enabled = true;
        }

        protected override void OnDisable()
        {
            collider.enabled = false;
        }
    }

}
