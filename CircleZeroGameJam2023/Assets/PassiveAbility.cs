using UnityEngine;

public class PassiveAbility : MonoBehaviour
{
    public AbilityTypes abilityType;
    public string abilityName;
    public int currentLevel = 1;
    public virtual void Activate()
    {

    }

    public void Levelup()
    {
        currentLevel++;
        OnLevelUp();
    }

    public virtual void OnLevelUp()
    {

    }

    public virtual void Deactivate()
    {

    }
}


