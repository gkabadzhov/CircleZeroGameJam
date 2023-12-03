using UnityEngine;
using UnityEngine.UI;
//Jumping
public class MovementCooldownVisual : MonoBehaviour
{
    
    public Image _cooldownImage;

    private void Awake()
    {
        PlayerPassiveManager.OnAbilityUpdated += PlayerPassiveManager_OnAbilityUpdated;    
    }

    private void Start()
    {
        PlayerPassiveManager_OnAbilityUpdated(null);
    }

    private void PlayerPassiveManager_OnAbilityUpdated(PassiveAbility obj)
    {
        if (obj == null || obj.abilityType != AbilityTypes.Movement)
        {
            _cooldownImage.fillAmount = 1;
            return;
        }

        _cooldownImage.fillAmount = 0;

    }

    private void OnDestroy()
    {
        PlayerPassiveManager.OnAbilityUpdated -= PlayerPassiveManager_OnAbilityUpdated;
    }
}