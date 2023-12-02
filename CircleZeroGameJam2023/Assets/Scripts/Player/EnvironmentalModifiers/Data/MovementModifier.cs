using Sirenix.OdinInspector;
using UnityEngine;

namespace OTBG.Gameplay.EnvironmentalModifiers.Data
{
    [System.Serializable]
	public class MovementModifier
	{
		public string modifierId;

		[FoldoutGroup("Movement")]
		public float acceleration = 40f;
		[FoldoutGroup("Movement")]
		public float deceleration = 0.75f;
		[FoldoutGroup("Movement")]
		public float resistance = 1f;

		[FoldoutGroup("Gravity")]
		public float gravity = 4f;
		[FoldoutGroup("Gravity")]
		public float maxFallSpeed = 20f;

		[FoldoutGroup("Jumping")]
		public bool canInfinitelyJump = false;
		[FoldoutGroup("Jumping")]
		public float groundJumpHeight = 16f;
		[FoldoutGroup("Jumping")]
		public float doubleJumpHeight = 12f;
		[FoldoutGroup("Jumping")]
		public Vector2 wallJumpForce = new Vector2(10f, 12f);

	}

}
