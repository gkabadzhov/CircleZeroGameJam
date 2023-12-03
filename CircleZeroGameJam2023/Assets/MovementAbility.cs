using OTBG.Gameplay.Player.Movement;

public class MovementAbility : PassiveAbility
{
    PlayerJump playerJump;

    public override void Activate()
    {
        base.Activate();

        playerJump = FindFirstObjectByType<PlayerJump>(UnityEngine.FindObjectsInactive.Include);
        playerJump.GetComponent<GroundJump>()._isUnlocked = true;

    }

    public override void OnLevelUp()
    {
        base.OnLevelUp();
        if (currentLevel > 2)
            return;

        playerJump.gameObject.GetComponent<AirJump>()._isUnlocked = true;
    }

    public override void Deactivate()
    {
        base.Deactivate();
        if (playerJump.TryGetComponent(out GroundJump groundJump))
            groundJump._isUnlocked = false;
        if (playerJump.TryGetComponent(out AirJump airJump))
            airJump._isUnlocked = false;
    }

    
}


