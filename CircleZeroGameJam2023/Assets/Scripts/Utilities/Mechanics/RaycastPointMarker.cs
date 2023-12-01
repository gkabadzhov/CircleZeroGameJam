using OTBG.Utilities.Attributes;
using OTBG.Utilities.General;
using Sirenix.OdinInspector;
using UnityEngine;

namespace OTBG.Utlity.Gameplay
{
    public class RaycastPointMarker : MonoBehaviour
    {
        [FoldoutGroup("Raycasting"), SerializeField, Vector3Visualiser]
        private Vector3 _direction;
        [FoldoutGroup("Raycasting"), SerializeField]
        private LayerMask _targetLayers;
        [FoldoutGroup("Raycasting"), SerializeField]
        private float _length;

        [FoldoutGroup("References"), SerializeField]
        private GameObject _prefabToVisualise;
        [FoldoutGroup("References"), SerializeField, ReadOnly]
        private GameObject _spawnedPrefab;

        public void Initialise()
        {
            Vector2Utils.GetPointPosition(transform.position, _direction, _length, _targetLayers, OnPointFound);
        }

        public void Deinitialise()
        {
            Destroy(_spawnedPrefab);
        }

        public void OnPointFound(Vector3 position)
        {
            _spawnedPrefab = Instantiate(_prefabToVisualise, position, Quaternion.identity, transform);
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawRay(transform.position, _direction * _length);
        }

    }
}