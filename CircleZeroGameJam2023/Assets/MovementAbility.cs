using OTBG.Gameplay.Player.Movement;

public class MovementAbility : PassiveAbility
{
    PlayerJump playerJump;

    public override void Activate()
    {
        base.Activate();

        playerJump = FindFirstObjectByType<PlayerJump>(UnityEngine.FindObjectsInactive.Include);
        playerJump.gameObject.AddComponent<GroundJump>();
        playerJump.ForceUpdate();

    }

    public override void OnLevelUp()
    {
        base.OnLevelUp();
        if (currentLevel > 2)
            return;

        playerJump.gameObject.AddComponent<AirJump>();
        playerJump.ForceUpdate();
    }

    public override void Deactivate()
    {
        base.Deactivate();
        if (playerJump.TryGetComponent(out GroundJump groundJump))
            Destroy(groundJump);
        if (playerJump.TryGetComponent(out AirJump airJump))
            Destroy(airJump);
        playerJump.ForceUpdate();
    }
}


