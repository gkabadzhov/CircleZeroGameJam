using OTBG.Gameplay.Player.Combat.Data;
using OTBG.Gameplay.Player.Interfaces;
using OTBG.Interfaces;
using OTBG.Utilities.Data;
using Sirenix.OdinInspector;
using System;
using Unity.VisualScripting;
using UnityEngine;
namespace OTBG.Gameplay.Player.Combat
{
    public class HealthController : MonoBehaviour, IDamageable, IDeathHandler
    {
        public int PLAYER_MAX_HEALTH = 1;

        public event Action<ValueChange> HealthChanged;
        public event Action OnDeath;
        public event Action OnPreDamage;

        [SerializeField]
        private int _health;
        [SerializeField]
        private bool _isInvulnerable = false;

        private void Awake()
        {
            SetHealth(PLAYER_MAX_HEALTH);
        }

        [Button]
        public void TestDamage()
        {
            TakeDamage(new DamageData()
            {
                damage = 1,
            });
        }

        public void TakeDamage(DamageData damageData)
        {
            if (!CanTakeDamage())
            {
                OnPreDamage?.Invoke();
                return;
            }
            Damage(damageData);
        }

        public void Damage(DamageData damageData)
        {
            SetHealth(_health - damageData.damage);
        }

        public void SetHealth(int health)
        {
            _health = Mathf.Clamp(health,0, PLAYER_MAX_HEALTH);
            HealthChanged?.Invoke(new ValueChange(_health,PLAYER_MAX_HEALTH));
            if (_health <= 0) Die();
        }

        private void Die()
        {
            Debug.Log("in die");
            OnDeath?.Invoke();
        }

        public void SetInvulnerability(bool isInvulnerable)
        {
            print("Is invulnerable " + isInvulnerable);
            _isInvulnerable = isInvulnerable;
        }

        private bool CanTakeDamage()
        {
            if (_health <= 0) return false;
            if (_isInvulnerable) return false;
            return true;
        }

        void IDeathHandler.OnDeath()
        {
            
        }

        public void OnRevive()
        {
            SetHealth(PLAYER_MAX_HEALTH);
        }


        public ValueChange GetHealth() => new ValueChange(_health, PLAYER_MAX_HEALTH);

    }
}
