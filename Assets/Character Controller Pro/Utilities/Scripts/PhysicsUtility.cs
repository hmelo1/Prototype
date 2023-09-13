using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lightbug.Utilities
{
    public delegate bool CollisionFilterDelegate2D(RaycastHit2D raycastHit);
    public delegate bool CollisionFilterDelegate3D(RaycastHit raycastHit);
    public delegate bool OverlapFilterDelegate2D(Collider2D collider);
    public delegate bool OverlapFilterDelegate3D(Collider collider);

    public static class PhysicsUtilities
    {
        #region PhysX

        #region Raycast

        // 3D ──────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────

        public static bool Raycast(out RaycastHit raycastHit, Vector3 origin, Vector3 direction, float distance, float backstepDistance = 0f, CollisionFilterDelegate3D CollisionFilter = null)
        {
            origin -= direction * backstepDistance;
            distance += backstepDistance;

            bool result = Physics.Raycast(origin, direction, out raycastHit, distance);

            if (!result)
                return false;

            if (CollisionFilter != null)
                result = CollisionFilter(raycastHit);

            return result;
        }

        public static int RaycastAll(RaycastHit[] unfilteredHits, List<RaycastHit> filteredHits, Vector3 origin, Vector3 direction, float distance, float backstepDistance = 0f, CollisionFilterDelegate3D CollisionFilter = null)
        {
            origin -= direction * backstepDistance;
            distance += backstepDistance;

            int hits = Physics.RaycastNonAlloc(origin, direction, unfilteredHits, distance);

            if (hits == 0)
                return 0;

            return FilterValidHits(hits, unfilteredHits, filteredHits, backstepDistance, CollisionFilter);
        }

        public static int RaycastAll(out RaycastHit raycastHit, RaycastHit[] unfilteredHits, List<RaycastHit> filteredHits, Vector3 origin, Vector3 direction, float distance, float backstepDistance = 0f, CollisionFilterDelegate3D CollisionFilter = null)
        {
            origin -= direction * backstepDistance;
            distance += backstepDistance;

            int hits = Physics.RaycastNonAlloc(origin, direction, unfilteredHits, distance);

            if (hits == 0)
            {
                raycastHit = new RaycastHit();
                return 0;
            }

            return GetClosestValidHit(hits, out raycastHit, unfilteredHits, filteredHits, backstepDistance, CollisionFilter);
        }
       
        #endregion

        #region SphereCast

        public static bool SphereCast(out RaycastHit raycastHit, Vector3 origin, float radius, Vector3 direction, float distance, float backstepDistance = 0f, CollisionFilterDelegate3D CollisionFilter = null)
        {
            origin -= direction * backstepDistance;
            distance += backstepDistance;

            bool result = Physics.SphereCast(origin, radius, direction, out raycastHit, distance);

            if (!result)
                return false;

            if (CollisionFilter != null)
                result = CollisionFilter(raycastHit);

            return result;
        }

        public static int SphereCastAll(
            RaycastHit[] hitsBuffer,
            List<RaycastHit> filteredHits,
            Vector3 origin,
            float radius,
            Vector3 direction,
            float distance,
            float backstepDistance = 0f,
            CollisionFilterDelegate3D CollisionFilter = null)
        {
            origin -= direction * backstepDistance;
            distance += backstepDistance;

            int hits = Physics.SphereCastNonAlloc(origin, radius, direction, hitsBuffer, distance);

            if (hits == 0)
                return 0;

            return FilterValidHits(hits, hitsBuffer, filteredHits, backstepDistance, CollisionFilter);
        }

        public static int SphereCastAll(
            out RaycastHit raycastHit,
            RaycastHit[] hitsBuffer,
            List<RaycastHit> filteredHits,
            Vector3 origin,
            float radius,
            Vector3 direction,
            float distance,
            float backstepDistance = 0f,
            CollisionFilterDelegate3D CollisionFilter = null)
        {
            origin -= direction * backstepDistance;
            distance += backstepDistance;

            int hits = Physics.SphereCastNonAlloc(origin, radius, direction, hitsBuffer, distance);

            if (hits == 0)
            {
                raycastHit = new RaycastHit();
                return 0;
            }

            return GetClosestValidHit(hits, out raycastHit, hitsBuffer, filteredHits, backstepDistance, CollisionFilter);
        }

        public static int OverlapSphere(Vector3 point, float radius, Collider[] unfilteredResults, List<Collider> filteredResults, OverlapFilterDelegate3D Filter = null)
        {
            int hits = Physics.OverlapSphereNonAlloc(point, radius, unfilteredResults);

            if (hits == 0)
                return 0;

            return FilterValidOverlaps(hits, unfilteredResults, filteredResults, Filter);
        }

        #endregion

        #region CapsuleCast

        public static bool CapsuleCast(out RaycastHit raycastHit, Vector3 bottom, Vector3 top, float radius, Vector3 direction, float distance, float backstepDistance = 0f, CollisionFilterDelegate3D CollisionFilter = null)
        {
            bottom -= direction * backstepDistance;
            top -= direction * backstepDistance;
            distance += backstepDistance;

            bool result = Physics.CapsuleCast(bottom, top, radius, direction, out raycastHit, distance);

            if (!result)
                return false;

            if (CollisionFilter != null)
                result = CollisionFilter(raycastHit);

            return result;
        }

        public static int CapsuleCastAll(
            RaycastHit[] hitsBuffer,
            List<RaycastHit> filteredHits,
            Vector3 bottom,
            Vector3 top,
            float radius,
            Vector3 direction,
            float distance,
            float backstepDistance = 0f,
            CollisionFilterDelegate3D CollisionFilter = null)
        {
            bottom -= direction * backstepDistance;
            top -= direction * backstepDistance;
            distance += backstepDistance;

            int hits = Physics.CapsuleCastNonAlloc(bottom, top, radius, direction, hitsBuffer, distance);

            if (hits == 0)
                return 0;

            return FilterValidHits(hits, hitsBuffer, filteredHits, backstepDistance, CollisionFilter);
        }

        public static int CapsuleCastAll(
            out RaycastHit raycastHit,
            RaycastHit[] hitsBuffer,
            List<RaycastHit> filteredHits,
            Vector3 bottom,
            Vector3 top,
            float radius,
            Vector3 direction,
            float distance,
            float backstepDistance = 0f,
            CollisionFilterDelegate3D CollisionFilter = null)
        {
            bottom -= direction * backstepDistance;
            top -= direction * backstepDistance;
            distance += backstepDistance;

            int hits = Physics.CapsuleCastNonAlloc(bottom, top, radius, direction, hitsBuffer, distance);

            if (hits == 0)
            {
                raycastHit = new RaycastHit();
                return 0;
            }

            return GetClosestValidHit(hits, out raycastHit, hitsBuffer, filteredHits, backstepDistance, CollisionFilter);
        }

        public static int OverlapCapsule(Vector3 bottom, Vector3 top, float radius, Collider[] unfilteredResults, List<Collider> filteredResults, OverlapFilterDelegate3D Filter = null)
        {
            int hits = Physics.OverlapCapsuleNonAlloc(bottom, top, radius, unfilteredResults);

            if (hits == 0)
                return 0;

            return FilterValidOverlaps(hits, unfilteredResults, filteredResults, Filter);
        }

        #endregion

        #region BoxCast

        public static bool BoxCast(out RaycastHit raycastHit, Vector3 center, Vector3 size, Vector3 direction, Quaternion rotation, float distance, float backstepDistance = 0f, CollisionFilterDelegate3D CollisionFilter = null)
        {
            center -= direction * backstepDistance;
            distance += backstepDistance;

            bool result = Physics.BoxCast(center, size / 2f, direction, out raycastHit, rotation, distance);

            if (!result)
                return false;

            if (CollisionFilter != null)
                result = CollisionFilter(raycastHit);

            return result;
        }

        public static int BoxCastAll(RaycastHit[] hitsBuffer, List<RaycastHit> filteredHits, Vector3 center, Vector3 size, Vector3 direction, Quaternion rotation, float distance, float backstepDistance = 0f, CollisionFilterDelegate3D CollisionFilter = null)
        {
            center -= direction * backstepDistance;
            distance += backstepDistance;

            int hits = Physics.BoxCastNonAlloc(center, size / 2f, direction, hitsBuffer, rotation, distance);

            if (hits == 0)
                return 0;

            return FilterValidHits(hits, hitsBuffer, filteredHits, backstepDistance, CollisionFilter);
        }

        public static int BoxCastAll(out RaycastHit raycastHit, RaycastHit[] hitsBuffer, List<RaycastHit> filteredHits, Vector3 center, Vector3 size, Vector3 direction, Quaternion rotation, float distance, float backstepDistance = 0f, CollisionFilterDelegate3D CollisionFilter = null)
        {
            center -= direction * backstepDistance;
            distance += backstepDistance;

            int hits = Physics.BoxCastNonAlloc(center, size / 2f, direction, hitsBuffer, rotation, distance);

            if (hits == 0)
            {
                raycastHit = new RaycastHit();
                return 0;
            }

            return GetClosestValidHit(hits, out raycastHit, hitsBuffer, filteredHits, backstepDistance, CollisionFilter);
        }

        public static int OverlapBox(Vector3 center, Vector3 size, Quaternion rotation, Collider[] unfilteredResults, List<Collider> filteredResults, OverlapFilterDelegate3D Filter = null)
        {
            int hits = Physics.OverlapBoxNonAlloc(center, size / 2f, unfilteredResults, rotation);

            if (hits == 0)
                return 0;

            return FilterValidOverlaps(hits, unfilteredResults, filteredResults, Filter);
        }
        #endregion

        #region HitProcessing

        public static void FilterAndSort(List<RaycastHit> raycastHits, CollisionFilterDelegate3D CollisionFilter)
        {
            for (int i = raycastHits.Count - 1; i > 0; i--)
            {
                RaycastHit raycastHit = raycastHits[i];

                if (CollisionFilter != null)
                {
                    bool validHit = CollisionFilter(raycastHit);
                    if (!validHit)
                    {
                        raycastHits.RemoveAt(i);
                        continue;
                    }
                }

                if (raycastHits[i - 1].distance > raycastHits[i].distance)
                {
                    raycastHits[i] = raycastHits[i - 1];
                    raycastHits[i - 1] = raycastHit;
                }
            }
        }

        public static int FilterValidHits(
            int hits,
            RaycastHit[] hitsBuffer,
            List<RaycastHit> filteredHits,
            float backstepDistance,
            CollisionFilterDelegate3D CollisionFilter)
        {
            filteredHits.Clear();
            for (int i = 0; i < hits; i++)
            {
                RaycastHit raycastHit = hitsBuffer[i];
                raycastHit.distance -= backstepDistance;

                // User-defined filter
                if (CollisionFilter != null)
                {
                    bool validHit = CollisionFilter(raycastHit);
                    if (!validHit)
                        continue;
                }

                filteredHits.Add(raycastHit);
            }

            return filteredHits.Count;
        }

        //public static RaycastHit GetClosestValidHit(List<RaycastHit> filteredHits, CollisionFilterDelegate3D CollisionFilter)
        //{
        //    int validHits = filteredHits.Count;
        //    if (filteredHits == null || validHits == 0)
        //        return new RaycastHit();

        //    float closestDistance = Mathf.Infinity;
        //    int index = -1;
        //    for (int i = 0; i < validHits; i++)
        //    {
        //        RaycastHit currentFilteredHit = filteredHits[i];

        //        if (currentFilteredHit.distance < closestDistance)
        //        {
        //            closestDistance = currentFilteredHit.distance;
        //            index = i;
        //        }
        //    }

        //    if (index != -1)
        //        return filteredHits[index];

        //    return new RaycastHit();
        //}

        public static int GetClosestValidHit(int hits, out RaycastHit raycastHit, RaycastHit[] hitsBuffer, List<RaycastHit> filteredHits, float backstepDistance, CollisionFilterDelegate3D CollisionFilter)
        {
            int validHits = FilterValidHits(hits, hitsBuffer, filteredHits, backstepDistance, CollisionFilter);

            raycastHit = new RaycastHit();

            if (validHits == 0)
                return 0;



            float closestDistance = Mathf.Infinity;
            int index = -1;

            for (int i = 0; i < validHits; i++)
            {
                RaycastHit currentFilteredHit = filteredHits[i];

                if (currentFilteredHit.distance < closestDistance)
                {
                    closestDistance = currentFilteredHit.distance;
                    index = i;
                }

            }

            if (index != -1)
                raycastHit = filteredHits[index];

            return validHits;
        }

        #endregion

        #endregion
                
        #region Box2D

        #region Raycast

        public static bool Raycast(out RaycastHit2D raycastHit, Vector2 origin, Vector2 direction, float distance, float backstepDistance = 0f, CollisionFilterDelegate2D CollisionFilter = null)
        {
            origin -= direction * backstepDistance;
            distance += backstepDistance;

            raycastHit = Physics2D.Raycast(origin, direction, distance);

            if (raycastHit.collider == null)
                return false;

            bool result = true;

            if (CollisionFilter != null)
                result = CollisionFilter(raycastHit);

            return result;
        }

        public static int RaycastAll(RaycastHit2D[] hitsBuffer, List<RaycastHit2D> filteredHits, Vector2 origin, Vector2 direction, float distance, float backstepDistance = 0f, CollisionFilterDelegate2D CollisionFilter = null)
        {
            origin -= direction * backstepDistance;
            distance += backstepDistance;

            int hits = Physics2D.RaycastNonAlloc(origin, direction, hitsBuffer, distance);

            if (hits == 0)
                return 0;

            return FilterValidHits(hits, hitsBuffer, filteredHits, backstepDistance, CollisionFilter);
        }

        public static int RaycastAll(out RaycastHit2D raycastHit, RaycastHit2D[] hitsBuffer, List<RaycastHit2D> filteredHits, Vector2 origin, Vector2 direction, float distance, float backstepDistance = 0f, CollisionFilterDelegate2D CollisionFilter = null)
        {
            origin -= direction * backstepDistance;
            distance += backstepDistance;

            int hits = Physics2D.RaycastNonAlloc(origin, direction, hitsBuffer, distance);

            if (hits == 0)
            {
                raycastHit = new RaycastHit2D();
                return 0;
            }

            return GetClosestValidHit(hits, out raycastHit, hitsBuffer, filteredHits, backstepDistance, CollisionFilter);
        }

        #endregion

        #region CircleCast

        public static bool CircleCast(out RaycastHit2D raycastHit, Vector2 origin, float radius, Vector2 direction, float distance, float backstepDistance = 0f, CollisionFilterDelegate2D CollisionFilter = null)
        {
            origin -= direction * backstepDistance;
            distance += backstepDistance;

            raycastHit = Physics2D.CircleCast(origin, radius, direction, distance);

            bool result = raycastHit.collider != null;

            if (!result)
                return false;

            if (CollisionFilter != null)
                result = CollisionFilter(raycastHit);

            return result;
        }

        public static int CircleCastAll(RaycastHit2D[] hitsBuffer, List<RaycastHit2D> filteredHits, Vector2 origin, float radius, Vector2 direction, float distance, float backstepDistance = 0f, CollisionFilterDelegate2D CollisionFilter = null)
        {
            origin -= direction * backstepDistance;
            distance += backstepDistance;

            int hits = Physics2D.CircleCastNonAlloc(origin, radius, direction, hitsBuffer, distance);

            if (hits == 0)
                return 0;

            return FilterValidHits(hits, hitsBuffer, filteredHits, backstepDistance, CollisionFilter);
        }

        public static int CircleCastAll(out RaycastHit2D raycastHit, RaycastHit2D[] hitsBuffer, List<RaycastHit2D> filteredHits, Vector2 origin, float radius, Vector2 direction, float distance, float backstepDistance = 0f, CollisionFilterDelegate2D CollisionFilter = null)
        {
            origin -= direction * backstepDistance;
            distance += backstepDistance;

            int hits = Physics2D.CircleCastNonAlloc(origin, radius, direction, hitsBuffer, distance);

            if (hits == 0)
            {
                raycastHit = new RaycastHit2D();
                return 0;
            }

            return GetClosestValidHit(hits, out raycastHit, hitsBuffer, filteredHits, backstepDistance, CollisionFilter);
        }


        public static int OverlapCircle(Vector2 point, float radius, Collider2D[] results, List<Collider2D> filteredColliders, OverlapFilterDelegate2D Filter = null)
        {
            int hits = Physics2D.OverlapCircleNonAlloc(point, radius, results);

            if (hits == 0)
                return 0;

            return FilterValidOverlaps(hits, results, filteredColliders, Filter);
        }


        #endregion

        #region CapsuleCast

        public static bool CapsuleCast(out RaycastHit2D raycastHit, Vector2 bottom, Vector2 top, float radius, Vector2 direction, float distance, float backstepDistance = 0f, CollisionFilterDelegate2D CollisionFilter = null)
        {
            bottom -= direction * backstepDistance;
            top -= direction * backstepDistance;
            distance += backstepDistance;

            Vector2 bodyDirection = (top - bottom).normalized;
            Vector2 center = (bottom + top) / 2f;
            Vector2 size = new Vector2(2f * radius, (top - bottom).magnitude + 2 * radius);
            float angle = Vector2.Angle(Vector2.up, bodyDirection);

            raycastHit = Physics2D.CapsuleCast(center, size, CapsuleDirection2D.Vertical, angle, direction, distance);

            bool result = raycastHit.collider == null;

            if (!result)
                return false;

            if (CollisionFilter != null)
                result = CollisionFilter(raycastHit);

            return result;
        }

        public static int CapsuleCastAll(RaycastHit2D[] hitsBuffer, List<RaycastHit2D> filteredHits, Vector2 bottom, Vector2 top, float radius, Vector2 direction, float distance, float backstepDistance = 0f, CollisionFilterDelegate2D CollisionFilter = null)
        {
            bottom -= direction * backstepDistance;
            top -= direction * backstepDistance;
            distance += backstepDistance;

            Vector2 bodyDirection = (top - bottom).normalized;
            Vector2 center = (bottom + top) / 2f;
            Vector2 size = new Vector2(2f * radius, (top - bottom).magnitude + 2 * radius);
            float angle = Vector2.Angle(Vector2.up, bodyDirection);

            int hits = Physics2D.CapsuleCastNonAlloc(center, size, CapsuleDirection2D.Vertical, angle, direction, hitsBuffer, distance);

            if (hits == 0)
                return 0;


            return FilterValidHits(hits, hitsBuffer, filteredHits, backstepDistance, CollisionFilter);
        }

        public static int CapsuleCastAll(out RaycastHit2D raycastHit, RaycastHit2D[] hitsBuffer, List<RaycastHit2D> filteredHits, Vector2 bottom, Vector2 top, float radius, Vector2 direction, float distance, float backstepDistance = 0f, CollisionFilterDelegate2D CollisionFilter = null)
        {
            bottom -= direction * backstepDistance;
            top -= direction * backstepDistance;
            distance += backstepDistance;

            Vector2 bodyDirection = (top - bottom).normalized;
            Vector2 center = (bottom + top) / 2f;
            Vector2 size = new Vector2(2f * radius, (top - bottom).magnitude + 2 * radius);
            float angle = Vector2.Angle(Vector2.up, bodyDirection);

            int hits = Physics2D.CapsuleCastNonAlloc(center, size, CapsuleDirection2D.Vertical, angle, direction, hitsBuffer, distance);

            if (hits == 0)
            {
                raycastHit = new RaycastHit2D();
                return 0;
            }

            return GetClosestValidHit(hits, out raycastHit, hitsBuffer, filteredHits, backstepDistance, CollisionFilter);
        }

        public static int OverlapCapsule(Vector2 bottom, Vector2 top, float radius, Collider2D[] unfilteredResults, List<Collider2D> filteredResults, OverlapFilterDelegate2D Filter = null)
        {
            Vector2 bodyDirection = (top - bottom).normalized;
            Vector2 center = (bottom + top) / 2f;
            Vector2 size = new Vector2(2f * radius, (top - bottom).magnitude + 2f * radius);
            float angle = Vector2.Angle(Vector2.up, bodyDirection);

            int hits = Physics2D.OverlapCapsuleNonAlloc(center, size, CapsuleDirection2D.Vertical, angle, unfilteredResults);
            
            if (hits == 0)
                return 0;

            return FilterValidOverlaps(hits, unfilteredResults, filteredResults, Filter);
        }

        #endregion

        #region BoxCast

        public static bool BoxCast(out RaycastHit2D raycastHit, Vector3 center, Vector3 size, Vector3 direction, Quaternion rotation, float distance, float backstepDistance = 0f, CollisionFilterDelegate2D CollisionFilter = null)
        {
            center -= direction * backstepDistance;
            distance += backstepDistance;
            float angle = rotation.eulerAngles.z;

            raycastHit = Physics2D.BoxCast(center, size, angle, direction, distance);

            bool result = raycastHit.collider != null;

            if (!result)
                return false;

            if (CollisionFilter != null)
                result = CollisionFilter(raycastHit);

            return result;
        }

        public static int BoxCastAll(RaycastHit2D[] hitsBuffer, List<RaycastHit2D> filteredHits, Vector3 center, Vector3 size, Vector3 direction, Quaternion rotation, float distance, float backstepDistance = 0f, CollisionFilterDelegate2D CollisionFilter = null)
        {
            center -= direction * backstepDistance;
            distance += backstepDistance;
            float angle = rotation.eulerAngles.z;

            int hits = Physics2D.BoxCastNonAlloc(center, size, angle, direction, hitsBuffer, distance);

            if (hits == 0)
                return 0;

            return FilterValidHits(hits, hitsBuffer, filteredHits, backstepDistance, CollisionFilter);
        }

        public static int BoxCastAll(out RaycastHit2D raycastHit, RaycastHit2D[] unfilteredResults, List<RaycastHit2D> filteredHits, Vector3 center, Vector3 size, Vector3 direction, Quaternion rotation, float distance, float backstepDistance = 0f, CollisionFilterDelegate2D CollisionFilter = null)
        {
            center -= direction * backstepDistance;
            distance += backstepDistance;
            float angle = rotation.eulerAngles.z;

            int hits = Physics2D.BoxCastNonAlloc(center, size, angle, direction, unfilteredResults, distance);

            if (hits == 0)
            {
                raycastHit = new RaycastHit2D();
                return 0;
            }

            return GetClosestValidHit(hits, out raycastHit, unfilteredResults, filteredHits, backstepDistance, CollisionFilter);
        }

        public static int OverlapBox(Vector2 center, Vector2 size, Quaternion rotation, Collider2D[] unfilteredResults, List<Collider2D> filteredResults, OverlapFilterDelegate2D Filter = null)
        {
            float angle = rotation.eulerAngles.z;
            int hits = Physics2D.OverlapBoxNonAlloc(center, size, angle, unfilteredResults);

            if (hits == 0)
                return 0;

            return FilterValidOverlaps(hits, unfilteredResults, filteredResults, Filter);
        }
        #endregion

        #region HitProcessing

        public static int FilterValidHits(int hits, RaycastHit2D[] hitsBuffer, List<RaycastHit2D> filteredHits, float backstepDistance, CollisionFilterDelegate2D CollisionFilter)
        {
            filteredHits.Clear();

            for (int i = 0; i < hits; i++)
            {
                RaycastHit2D raycastHit = hitsBuffer[i];
                raycastHit.distance -= backstepDistance;

                // User-defined filter
                if (CollisionFilter != null)
                {
                    bool validHit = CollisionFilter(raycastHit);
                    if (!validHit)
                        continue;
                }

                filteredHits.Add(raycastHit);
            }

            return filteredHits.Count;
        }

        public static int FilterValidOverlaps(int hits, Collider2D[] overlapsBuffer, List<Collider2D> filteredOverlaps, OverlapFilterDelegate2D Filter)
        {
            filteredOverlaps.Clear();

            for (int i = 0; i < hits; i++)
            {
                Collider2D collider = overlapsBuffer[i];

                // User-defined filter
                if (Filter != null)
                {
                    bool validHit = Filter(collider);
                    if (!validHit)
                        continue;
                }

                filteredOverlaps.Add(collider);
            }

            return filteredOverlaps.Count;
        }

        public static int FilterValidOverlaps(int hits, Collider[] unfilteredOverlaps, List<Collider> filteredOverlaps, OverlapFilterDelegate3D Filter)
        {
            filteredOverlaps.Clear();

            for (int i = 0; i < hits; i++)
            {
                Collider collider = unfilteredOverlaps[i];

                // User-defined filter
                if (Filter != null)
                {
                    bool validHit = Filter(collider);
                    if (!validHit)
                        continue;

                }

                filteredOverlaps.Add(collider);
            }

            return filteredOverlaps.Count;
        }

        public static int GetClosestValidHit(int hits, out RaycastHit2D raycastHit, RaycastHit2D[] hitsBuffer, List<RaycastHit2D> filteredHits, float backstepDistance, CollisionFilterDelegate2D CollisionFilter)
        {
            // First of all, filter all the hits
            int validHits = FilterValidHits(hits, hitsBuffer, filteredHits, backstepDistance, CollisionFilter);

            raycastHit = new RaycastHit2D();

            if (validHits == 0)
                return 0;


            float closestDistance = Mathf.Infinity;
            int index = -1;

            for (int i = 0; i < validHits; i++)
            {
                RaycastHit2D currentFilteredHit = filteredHits[i];

                if (currentFilteredHit.distance < closestDistance)
                {
                    closestDistance = currentFilteredHit.distance;
                    index = i;
                }

            }

            if (index != -1)
                raycastHit = filteredHits[index];

            return validHits;
        }
        #endregion

        #endregion
    }
}