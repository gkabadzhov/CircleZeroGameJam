using OTBG.Gameplay.Player.Combat;
using OTBG.Gameplay.Player.Interfaces;
using UnityEngine;

[RequireComponent(typeof(HealthController))]
public class AbilityGiver : MonoBehaviour
{
    AbilityTypes abilityType;
    HealthController healthController;

    private void Awake()
    {
        healthController = GetComponent<HealthController>();
        healthController.OnDeath += OnDeath;
    }

    public void OnDeath()
    {
        FindFirstObjectByType<PlayerPassiveManager>().PickupAbility(abilityType);
    }
}


