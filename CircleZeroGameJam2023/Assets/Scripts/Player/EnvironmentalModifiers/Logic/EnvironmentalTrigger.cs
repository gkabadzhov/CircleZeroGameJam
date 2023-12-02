using OTBG.Gameplay.Player.Effects.EnvironmentalModifiers;
using OTBG.Utilities.General;
using UnityEngine;

namespace OTBG.Gameplay.EnvironmentalModifiers.Logic
{
    public class EnvironmentalTrigger : MonoBehaviour
    {
        [SerializeField] private string _environmentModifierId;
        [SerializeField] private SubscribablePhysics2D _physics2D;

        public string EnvironmentModifierId => _environmentModifierId;

        private void Awake()
        {
            _physics2D = GetComponentInChildren<SubscribablePhysics2D>();

            _physics2D.OnTriggerEnterEvent += OnTriggerEnterEvent;
            _physics2D.OnTriggerExitEvent += OnTriggerExitEvent;
            _physics2D.OnCollisionEnterEvent += OnCollisionEnterEvent;
            _physics2D.OnCollisionExitEvent += OnCollisionExitEvent;
        }     

        private void OnDisable()
        {
            _physics2D.OnTriggerEnterEvent -= OnTriggerEnterEvent;
            _physics2D.OnTriggerExitEvent -= OnTriggerExitEvent;
            _physics2D.OnCollisionEnterEvent -= OnCollisionEnterEvent;
            _physics2D.OnCollisionExitEvent -= OnCollisionExitEvent;
        }

        private void OnTriggerEnterEvent(Collider2D player)
        {
            SetModifier(player.transform);
        }
        
        private void OnTriggerExitEvent(Collider2D player)
        {
            ClearModifier(player.transform);
        }

        private void OnCollisionEnterEvent(Collision2D player)
        {
            SetModifier(player.transform);
        }
        private void OnCollisionExitEvent(Collision2D player)
        {
            ClearModifier(player.transform);
        }

        public void SetModifier(Transform player)
        {
            if (player.TryGetComponent(out PlayerEnvironmentalModifiersController controller))
            {
                controller.SetModifier(_environmentModifierId);
            }
        }

        public void ClearModifier(Transform player)
        {
            if (player.TryGetComponent(out PlayerEnvironmentalModifiersController controller))
            {
                controller.ClearModifier();
            }
        }

    }
}
