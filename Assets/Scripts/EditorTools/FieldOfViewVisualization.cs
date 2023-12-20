using UnityEditor;
using UnityEngine;

namespace EditorTools
{
    public class FieldOfViewVisualization: MonoBehaviour
    {
        [SerializeField, Range(0,360)] private float _angle;
        [SerializeField] private float _radius;
        private void OnDrawGizmos()
        {
            Handles.color = new Color(1, 0, 0, 0.35f);
            Handles.DrawSolidArc(
                transform.position,
                transform.up,
                Quaternion.AngleAxis(-_angle/2, transform.up) * transform.forward,
                _angle,
                _radius);
            
        }
    }
}