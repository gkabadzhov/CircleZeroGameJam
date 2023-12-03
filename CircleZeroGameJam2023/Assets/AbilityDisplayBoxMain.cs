using UnityEngine;
using UnityEngine.InputSystem.HID;
using UnityEngine.UI;

public class AbilityDisplayBoxMain : MonoBehaviour
{
    public Image _iconImage;
    public Sprite _defaultIcon;
    public GameObject hud;
    private void Awake()
    {
        PlayerPassiveManager.OnAbilityUpdated += PlayerPassiveManager_OnAbilityUpdated;
    }

    private void OnDestroy()
    {
        PlayerPassiveManager.OnAbilityUpdated -= PlayerPassiveManager_OnAbilityUpdated;
    }

    private void PlayerPassiveManager_OnAbilityUpdated(PassiveAbility obj)
    {
        if (obj == null)
        {
            hud.SetActive(false);
            _iconImage.sprite = _defaultIcon;
            return;
        }

            hud.SetActive(true);
        _iconImage.sprite = obj.GetLevelSprite();

    }
}
