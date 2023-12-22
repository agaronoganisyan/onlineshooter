using UnityEngine;

namespace HelpersLogic
{
    public struct BallisticFunctions
    {
        public static float GetForce(Vector3 startPos, Vector3 targetPos, float angle)
        {
            Vector3 vector3 = (targetPos - startPos);
            vector3.y = 0.0f;
            float num = targetPos.y - startPos.y;
            float magnitude = vector3.magnitude;
            float y = Physics.gravity.y;
            float f = Mathf.Tan(angle * ((float) Mathf.PI / 180f));
            return Mathf.Sqrt((float) (-((double) Mathf.Pow(f, 2f) * (double) y * (double) Mathf.Pow(magnitude, 2f)) + -((double) y * (double) Mathf.Pow(magnitude, 2f))) / (float) ((double) f * (double) magnitude * 2.0 - (double) num * 2.0));
        }
    }
}