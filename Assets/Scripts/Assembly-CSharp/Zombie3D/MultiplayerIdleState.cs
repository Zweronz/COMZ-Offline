using UnityEngine;

namespace Zombie3D
{
	public class MultiplayerIdleState : PlayerState
	{
		public MultiplayerIdleState()
		{
			state_type = PlayerStateType.Idle;
		}

		public override void EnterState(Player player)
		{
			player.StopFire();
		}

		public override void DoStateLogic(Player player, float deltaTime)
		{
			player.Move(deltaTime);
			player.ResetSawAnimation();
			player.Animate("Idle01" + player.WeaponNameEnd, WrapMode.Loop);
		}

		public override void ExitState(Player player)
		{
		}
	}
}
