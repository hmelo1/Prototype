using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lightbug.CharacterControllerPro.Demo
{
    [RequireComponent(typeof(Rigidbody))]
    public class OrbitPlanet : MonoBehaviour
    {
        public Transform planet;
        public float orbitDistance = 20f;
        public float speed = 10f;

        new Rigidbody rigidbody;
        Vector3 rotationAxis;

        private void Awake()
        {
            if (planet == null)
            { 
                enabled = false;
                return;
            }

            rigidbody = GetComponent<Rigidbody>();

            Vector3 dir = (transform.position - planet.position).normalized;
            //transform.position = planet.position + dir * orbitDistance;
            transform.rotation = Quaternion.LookRotation(transform.forward, dir);

            rotationAxis = transform.right;
        }

        void FixedUpdate()
        {
            Vector3 targetPosition = Orbit();
            Vector3 targetVelocity = (targetPosition - transform.position) / Time.deltaTime;
            rigidbody.velocity = Vector3.MoveTowards(rigidbody.velocity, targetVelocity, 1f);

            Vector3 dir = (transform.position - planet.position).normalized;
            Vector3 newForward = Vector3.Cross(transform.right, dir).normalized;
            Quaternion targetRotation = Quaternion.LookRotation(targetVelocity, dir);

            //targetRotation
            rigidbody.MoveRotation(Quaternion.LookRotation(newForward, dir));
        }
     
        public Vector3 Orbit()
        {
            Vector3 orbitReference = transform.position - planet.position;
            Quaternion deltaRotation = Quaternion.AngleAxis(speed * Time.deltaTime, rotationAxis);
            orbitReference = deltaRotation * orbitReference;

            return planet.position + orbitReference;
        }
    }

}
