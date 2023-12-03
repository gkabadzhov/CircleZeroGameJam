using OTBG.Utilities.Data;
using UnityEngine;
using UnityEngine.UI;
//Shooting
public class ShootingCooldownVisual : MonoBehaviour
{
    public Image _cooldownImage;

    private void Awake()
    {
        MissileAttack.OnWeaponTimerUpdated += MissileAttack_OnWeaponTimerUpdated;
    }

    private void OnDestroy()
    {
        MissileAttack.OnWeaponTimerUpdated -= MissileAttack_OnWeaponTimerUpdated;
    }


    private void MissileAttack_OnWeaponTimerUpdated(ValueChange obj)
    {
        _cooldownImage.fillAmount = obj.GetPercentage();
    }
}
