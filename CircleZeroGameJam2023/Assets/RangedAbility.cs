public class RangedAbility : PassiveAbility
{
    MissileAttack _missileAttack;

    public override void Activate()
    {
        base.Activate();
        _missileAttack = FindFirstObjectByType<MissileAttack>(UnityEngine.FindObjectsInactive.Include);
        _missileAttack._abilityLevel = 2;
    }

    public override void OnLevelUp()
    {
        _missileAttack._abilityLevel = 3;
    }

    public override void Deactivate()
    {
        base.Deactivate();
        _missileAttack._abilityLevel = 1;
    }
}


