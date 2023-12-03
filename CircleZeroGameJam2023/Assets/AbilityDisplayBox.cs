using OTBG.Gameplay.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityDisplayBox : MonoBehaviour
{
    public AbilityTypes abilityTypes;

    public Image _iconImage;
    public Sprite _defaultIcon;
    private void Awake()
    {
        PlayerPassiveManager.OnAbilityUpdated += PlayerPassiveManager_OnAbilityUpdated;
    }

    private void OnDestroy()
    {
        PlayerPassiveManager.OnAbilityUpdated -= PlayerPassiveManager_OnAbilityUpdated;
    }

    private void Start()
    {
        PlayerPassiveManager_OnAbilityUpdated(null);
    }

    private void PlayerPassiveManager_OnAbilityUpdated(PassiveAbility obj)
    {
        if (obj == null || obj.abilityType != abilityTypes)
        {
            _iconImage.sprite = _defaultIcon;
            return;
        }

        _iconImage.sprite = obj.GetLevelSprite();

    }

    public void Initialise(PassiveAbility ability)
    {
        _iconImage.sprite = ability.GetLevelSprite();

    }
}
