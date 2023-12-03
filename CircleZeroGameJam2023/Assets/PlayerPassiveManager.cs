using JetBrains.Annotations;
using Sirenix.OdinInspector;
using Sirenix.Utilities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerPassiveManager : MonoBehaviour
{
    public static event Action<PassiveAbility> OnAbilityUpdated;
    public static event Action<float> OnAbilityTimerUpdated;

    public PassiveAbility _activeAbility;

    public List<PassiveAbility> allAbilities = new List<PassiveAbility>();

    public float maxAbilityTime;
    private Coroutine abilityTimerCoroutine;
    [SerializeField]
    private float debugTimer;
    private void Start()
    {
        OnAbilityUpdated?.Invoke(null);
    }

    [Button]
    public void PickupAbility(AbilityTypes abilityType)
    {
        CheckAndCleanup(abilityType);

        if(_activeAbility != null)
        {
            _activeAbility.Levelup();
            TriggerTimer();
            return;
        }


        if(_activeAbility == null)
        {
            PassiveAbility ability = CreateAbility(abilityType);

            _activeAbility = ability;
            _activeAbility.Activate();
            OnAbilityUpdated?.Invoke(ability);
            TriggerTimer();
            return;
        }
    }

    public void CheckAndCleanup(AbilityTypes abilityType)
    {
        if (_activeAbility == null)
            return;

        if (abilityType == _activeAbility.abilityType)
            return;

        _activeAbility.Deactivate();
        Destroy(_activeAbility);
        _activeAbility = null;
    }

    public PassiveAbility CreateAbility(AbilityTypes abilityType)
    {
        PassiveAbility ability = allAbilities.First(a => a.abilityType == abilityType);
        return Instantiate(ability, transform);
    } 

    public void TriggerTimer()
    {
        if(abilityTimerCoroutine != null)
        {
            StopCoroutine(abilityTimerCoroutine);
            abilityTimerCoroutine = null;
        }

        abilityTimerCoroutine = StartCoroutine(WaitToRemoveAbility());
    }

    public IEnumerator WaitToRemoveAbility()
    {
        yield return AbilityCooldown();
        _activeAbility.Deactivate();
        Destroy(_activeAbility);
        _activeAbility = null;
        abilityTimerCoroutine = null;
        OnAbilityUpdated?.Invoke(_activeAbility);
    }

    public IEnumerator AbilityCooldown()
    {
        float timer = maxAbilityTime;
        while(timer > 0)
        {
            OnAbilityTimerUpdated?.Invoke(timer);
            timer -= Time.deltaTime;
            debugTimer = timer;
            yield return null;
        }
    }
}

public enum AbilityTypes
{
    Movement,
    Combat,
    Shooting
}


