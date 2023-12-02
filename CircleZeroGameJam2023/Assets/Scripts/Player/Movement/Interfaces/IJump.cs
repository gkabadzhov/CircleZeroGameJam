using System;

namespace OTBG.Gameplay.Player.Movement.Interfaces
{
    public interface IJump
    {
        int Priority { get; }
        void Initialise(PlayerJump playerJump);
        bool CanJump();
        void OnJump(Action<bool> OnPauseMovement);
        void OnJumpRelease();
        void OnGroundedCheck(bool isGrounded);
    }
}