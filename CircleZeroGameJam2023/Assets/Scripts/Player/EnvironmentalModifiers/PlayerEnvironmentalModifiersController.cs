using OTBG.Gameplay.EnvironmentalModifiers.Data;
using OTBG.Gameplay.EnvironmentalModifiers.Interfaces;
using OTBG.Gameplay.EnvironmentalModifiers.Logic;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace OTBG.Gameplay.Player.Effects.EnvironmentalModifiers
{
    public class PlayerEnvironmentalModifiersController : MonoBehaviour
	{
		private List<IEnvironmentModifiable> _environmentModifiables = new List<IEnvironmentModifiable>();

		private void Awake()
		{
			_environmentModifiables = GetComponentsInChildren<IEnvironmentModifiable>().ToList();
		}

        private void Start()
        {
			ClearModifier();
        }

        public void SetModifier(string modifierId)
		{
			_environmentModifiables.ForEach(m => m.ApplyModifier(EnvironmentModifiersManager.Instance.GetModifier(modifierId)));
		}

		public void ClearModifier()
		{
			_environmentModifiables.ForEach(m => m.ApplyModifier(EnvironmentModifiersManager.Instance.GetDefaultModifier()));
		}
	}
}

