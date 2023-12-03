using OTBG.Gameplay.Player.Combat;
using OTBG.Gameplay.Player.Interfaces;
using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace OTBG.Gameplay.Player
{
    [RequireComponent(typeof(HealthController))]
    public class PlayerController : MonoBehaviour
    {
        public static event Action<PlayerController> OnDeath;
        public static event Action<PlayerController> OnInitialised;
        public static event Action<PlayerController> OnDeinitialised;

        private HealthController _healthController;
        private List<SpriteRenderer> _rends = new List<SpriteRenderer>();
        private List<IPlayerInitialisable> _playerInitialisables = new List<IPlayerInitialisable>();
        private List<IDeathHandler> _playerDeathables = new List<IDeathHandler>();

        [Button]
        public void Initialise()
        {
            _rends = GetComponentsInChildren<SpriteRenderer>(true).ToList();

            _playerDeathables = GetComponentsInChildren<IDeathHandler>().ToList();
            _playerInitialisables = GetComponentsInChildren<IPlayerInitialisable>().ToList();
            _playerInitialisables.ForEach(i => i.Initialise());
            
            _healthController = GetComponent<HealthController>();

            SetEvents();
            OnInitialised?.Invoke(this);
        }

        public void SetEvents()
        {
            _healthController.OnDeath += _healthController_OnDeath;
        }
        public void Respawn(Vector3 position)
        {
            transform.position = position;
            _playerDeathables.ForEach(d => d.OnRevive());
        }

        public IEnumerator HandleDeath()
        {
            _playerDeathables.ForEach(d => d.OnDeath());
            yield return new WaitForSeconds(1);
            OnDeath?.Invoke(this);
        }

        private void _healthController_OnDeath()
        {
            StartCoroutine(HandleDeath());
        }

        private void OnDestroy()
        {
            _healthController.OnDeath -= _healthController_OnDeath;
            OnDeinitialised?.Invoke(this);
        }


        public HealthController GetHealthController()
        {
            if (_healthController == null)
                _healthController = GetComponent<HealthController>();
            return _healthController;
        }

        public void PortalToggle(bool inPortal)
        {
            ToggleVisuals(!inPortal);
            ToggleLayers(!inPortal);
        }

        public void ToggleLayers(bool isCollidable)
        {
            gameObject.layer = isCollidable ? LayerMask.NameToLayer("Player") : LayerMask.NameToLayer("NoDetection");
        }

        public void ToggleVisuals(bool isVisible)
        {
            _rends.ForEach(r => r.enabled = isVisible);
        }
    }
}

