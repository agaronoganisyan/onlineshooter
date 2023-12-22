using UnityEngine;

namespace EditorTools
{
    public class SphereVisualization : MonoBehaviour
    {
        [SerializeField] private float _radius;

        private void OnDrawGizmos()
        {
            Gizmos.DrawWireSphere(transform.position, _radius);
        }
    }
}
