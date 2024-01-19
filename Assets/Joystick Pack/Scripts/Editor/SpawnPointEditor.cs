using Gameplay.MatchLogic.SpawnLogic.SpawnPointLogic;
using UnityEditor;
using UnityEngine;

namespace Joystick_Pack.Scripts.Editor
{
    [CustomEditor(typeof(SpawnPoint))]
    public class SpawnPointEditor : UnityEditor.Editor
    {
        [DrawGizmo(GizmoType.Active | GizmoType.Pickable | GizmoType.NonSelected)]
        public static void RenderCustomGizmo(SpawnPoint point, GizmoType gizmoType)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawSphere(point.transform.position, 0.5f);
        }
    }
}