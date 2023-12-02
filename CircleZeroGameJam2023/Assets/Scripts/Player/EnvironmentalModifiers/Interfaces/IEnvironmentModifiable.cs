using OTBG.Gameplay.EnvironmentalModifiers.Data;

namespace OTBG.Gameplay.EnvironmentalModifiers.Interfaces
{
    public interface IEnvironmentModifiable
	{
		public void ApplyModifier(MovementModifier modifier);
	}
}
