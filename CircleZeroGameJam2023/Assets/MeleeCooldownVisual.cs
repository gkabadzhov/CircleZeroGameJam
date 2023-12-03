using OTBG.Utilities.Data;
using UnityEngine;
using UnityEngine.UI;
//Dashing
public class MeleeCooldownVisual : MonoBehaviour
{
    public Image _cooldownImage;

    private void Awake()
    {
        PlayerDash.OnDashCooldownChanged += PlayerDash_OnDashCooldownChanged;
    }
    private void OnDestroy()
    {
        PlayerDash.OnDashCooldownChanged -= PlayerDash_OnDashCooldownChanged;
    }

    private void PlayerDash_OnDashCooldownChanged(ValueChange obj)
    {
        _cooldownImage.fillAmount = obj.GetPercentage();
    }
}
