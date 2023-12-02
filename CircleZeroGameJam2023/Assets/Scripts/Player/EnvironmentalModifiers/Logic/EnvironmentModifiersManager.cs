using OTBG.Gameplay.EnvironmentalModifiers.Data;
using OTBG.Utilities.General;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace OTBG.Gameplay.EnvironmentalModifiers.Logic
{
    public class EnvironmentModifiersManager : Singleton<EnvironmentModifiersManager>
	{
		[SerializeField] private List<MovementModifier> _movementModifiers = new List<MovementModifier>();

		public MovementModifier GetModifier(string id)
		{
			MovementModifier modifier = _movementModifiers.FirstOrDefault(m => m.modifierId == id);
			if (modifier == null)
			{
				Debug.LogError($"No Modifier with ID {id}");
				return _movementModifiers.First();
			}
			return modifier;
		}

		public MovementModifier GetDefaultModifier()
		{
			return _movementModifiers.First();
		}
		public List<MovementModifier> GetModifiersList() => _movementModifiers;
	}
}
