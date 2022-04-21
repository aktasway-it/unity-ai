using System.Collections.Generic;
using UnityEngine;

namespace Helpers
{
    public static class PhysicsExtensions
    {
        public static List<RaycastHit> ConeCast(Vector3 origin, float distance, Vector3 direction, float angle, LayerMask layerMask)
        {
            RaycastHit[] raycastHits = Physics.SphereCastAll(origin, distance, direction, distance, layerMask);
            List<RaycastHit> coneCastHits = new List<RaycastHit>();
            for (int i = 0; i < raycastHits.Length; i++)
            {
                Vector3 hitDirection = (raycastHits[i].point - origin).normalized;
                float hitAngle = Vector3.Angle(direction, hitDirection);
                if (hitAngle < angle)
                    coneCastHits.Add(raycastHits[i]);
            }
                    
            return coneCastHits;
        }
    }
}
