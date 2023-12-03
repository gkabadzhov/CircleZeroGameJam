using UnityEngine;

public class PassiveAbility : MonoBehaviour
{
    public AbilityTypes abilityType;
    public string levelOneName;
    public string levelTwoName;

    public Sprite levelOneIcon;
    public Sprite levelTwoIcon;

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

    public Sprite GetLevelSprite() => currentLevel == 1 ? levelOneIcon : levelTwoIcon;
}


