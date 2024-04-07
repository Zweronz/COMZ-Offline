using System.Collections.Generic;
using UnityEngine;

namespace Zombie3D
{
	public class TPSInputController : InputController
	{
		public override void ProcessFireInput(int inputEventType, float distance, float angle, TUIInput data)
		{
			if (base.EnableShootingInput)
			{
				switch (inputEventType)
				{
				case 1:
					GameApp.GetInstance().GetGameScene().GetPlayer()
						.OnFireBegin();
					base.InputInfo.fire = true;
					break;
				case 3:
					base.InputInfo.fire = false;
					break;
				}
			}
		}

		public override void ProcessMoveInput(int inputEventType, float distance, float angle)
		{
			if (base.EnableMoveInput)
			{
				base.InputInfo.moveDirection = new Vector3(distance * Mathf.Cos(angle), 0f, distance * Mathf.Sin(angle));
				base.InputInfo.moveDirection = player.GetTransform().TransformDirection(base.InputInfo.moveDirection);
				base.InputInfo.moveDirection += Physics.gravity * Time.deltaTime * 20f;
				player.SetMoveDirection();
				if (inputEventType == 1 || inputEventType == 2)
				{
					base.InputInfo.IsMoving = true;
				}
				else
				{
					base.InputInfo.IsMoving = false;
				}
			}
		}

		public override void ProcessRotateInput(int inputEventType, TUIInput data)
		{
			if (base.EnableTurningAround)
			{
				switch (inputEventType)
				{
				case 1:
					lastTouchPosition = data.position;
					break;
				case 2:
					cameraRotation.x = (data.position.x - lastTouchPosition.x) * 0.24f;
					cameraRotation.y = (data.position.y - lastTouchPosition.y) * 0.128f;
					lastTouchPosition = data.position;
					break;
				case 3:
					cameraRotation = Vector2.zero;
					break;
				}
			}
		}

		public override void ProcessInput(float deltaTime, InputInfo inputInfo)
		{
			if (base.EnableMoveInput)
			{
				inputInfo.moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
				inputInfo.moveDirection = player.GetTransform().TransformDirection(inputInfo.moveDirection);
				inputInfo.IsMoving = true;
			}
			inputInfo.moveDirection += Physics.gravity * deltaTime * 20f;
			player.SetMoveDirection();
			if (inputInfo.moveDirection.x != 0f || inputInfo.moveDirection.z != 0f)
			{
				inputInfo.IsMoving = true;
			}
			else
			{
				inputInfo.IsMoving = false;
			}
			List<Weapon> battleWeapons = GameApp.GetInstance().GetGameState().GetBattleWeapons();
			for (int i = 1; i <= battleWeapons.Count; i++)
			{
				if (Input.GetButton("Weapon" + i) && player.GetWeapon().Name != battleWeapons[i - 1].Name)
				{
					player.ChangeWeaponAndSendMsg(i - 1);
				}
			}
			if (Input.GetButton("H"))
			{
				player.GetHealed((int)player.MaxHp);
			}
			if (Input.GetButtonDown("K"))
			{
				player.enableHit = !player.enableHit;
			}
			if (Input.GetButtonDown("N"))
			{
				GameObject.Find("ArenaTrigger").GetComponent<ArenaTriggerFromConfigScript>().enabled = false;
				GameApp.GetInstance().GetGameScene().GamePlayingState = PlayingState.GameWin;
				GameApp.GetInstance().GetGameState().DayUp();
				GameApp.GetInstance().Save();
				SceneName.LoadLevel("MainMapTUI");
			}
		}
	}
}
