using UnityEngine;

namespace Zombie3D
{
	public class Hunter : Enemy
	{
		public static EnemyState JUMP_STATE = new JumpState();

		public static EnemyState LOOKAROUND_STATE = new LookAroundState();

		protected Collider handCollider;

		protected Vector3 targetPosition;

		protected Collider collider;

		protected Vector3[] p = new Vector3[4];

		protected bool startAttacking;

		protected float rushingInterval;

		protected float rushingSpeed;

		protected float rushingDamage;

		protected float rushingAttackDamage;

		protected float lastRushingTime;

		protected string rndRushingAnimationName;

		protected Vector3 rushingTarget;

		protected bool rushingCollided;

		protected bool rushingAttacked;

		public Vector3 speed;

		protected Collider leftHandCollider;

		protected bool jumpended;

		protected float lookAroundStartTime;

		public bool JumpEnded
		{
			get
			{
				return jumpended;
			}
		}

		public override void Init(GameObject gObject)
		{
			m_tip_height = 2.5f;
			base.Init(gObject);
			handCollider = enemyTransform.Find("Bip01/Bip01 Pelvis/Bip01 Spine/Bip01 Spine1/Bip01 Neck/Bip01 R Clavicle/Bip01 R UpperArm/Bip01 R Forearm/Bip01 R Hand").GetComponent<Collider>();
			leftHandCollider = enemyTransform.Find("Bip01/Bip01 Pelvis/Bip01 Spine/Bip01 Spine1/Bip01 Neck/Bip01 L Clavicle/Bip01 L UpperArm/Bip01 L Forearm/Bip01 L Hand").GetComponent<Collider>();
			collider = enemyTransform.GetComponent<Collider>();
			controller = enemyTransform.GetComponent<Collider>() as CharacterController;
			lastTarget = Vector3.zero;
			mAttributes.attackRange = 3f;
			mAttributes.AttackSpeed = 10f;
			lastRushingTime = -99f;
			startAttacking = false;
			rushingCollided = false;
			rushingAttacked = false;
			mAttributes.detectionRange = 120f;
			MonsterConfig monsterConfig = gConfig.GetMonsterConfig("Hunter");
			rushingInterval = monsterConfig.rushInterval;
			rushingSpeed = monsterConfig.rushSpeed;
			rushingDamage = monsterConfig.rushDamage * gameScene.GetEnemyAttributesComputingFactor("damage");
			rushingAttackDamage = monsterConfig.rushAttackDamage * gameScene.GetEnemyAttributesComputingFactor("damage");
			if (base.IsElite)
			{
				mAttributes.Hp *= 1.5f;
			}
			animation["Run"].speed = 1.5f;
			animation["JumpStart01"].wrapMode = WrapMode.ClampForever;
			animation["JumpIdle01"].wrapMode = WrapMode.Loop;
			animation["JumpEnd01"].wrapMode = WrapMode.ClampForever;
		}

		public override void OnAttack()
		{
			base.OnAttack();
			Animate("Attack01", WrapMode.ClampForever);
			attacked = false;
			lastAttackTime = Time.time;
		}

		public override void CheckHit()
		{
			if (CouldMakeNextAttack() || (!attacked && IsAnimationPlayedPercentage("Attack01", 0.6f)))
			{
				Collider collider = player.Collider;
				if (collider != null && handCollider.bounds.Intersects(collider.bounds))
				{
					player.OnHit(mAttributes.AttackDamage);
					if (CouldMakeNextAttack())
					{
						lastAttackTime = Time.time;
					}
					else if (!attacked && IsAnimationPlayedPercentage("Attack01", 0.6f))
					{
						attacked = true;
					}
				}
			}
			base.CheckHit();
		}

		public bool Jump(float deltaTime)
		{
			if ((Time.time - lastRushingTime > 0.5f && enemyTransform.position.y <= 10000.3f) || Time.time - lastRushingTime > 4f)
			{
				CheckHit();
			}
			else
			{
				speed += Physics.gravity * deltaTime;
				controller.Move(speed * deltaTime);
			}
			if ((Time.time - lastRushingTime > 0.5f && enemyTransform.position.y <= 10001.699f) || Time.time - lastRushingTime > 2f || controller.isGrounded)
			{
				if (!jumpended)
				{
					animation.CrossFade("JumpEnd01");
					jumpended = true;
				}
				if (IsAnimationPlayedPercentage("JumpEnd01", 1f))
				{
					return true;
				}
			}
			return false;
		}

		public override void DoLogic(float deltaTime)
		{
			base.DoLogic(deltaTime);
			if (state == Enemy.DEAD_STATE)
			{
				speed = Physics.gravity * 10f;
				controller.Move(speed * deltaTime);
			}
		}

		public bool ReadyForJump()
		{
			if (Time.time - lastRushingTime > 5.5f && (enemyTransform.position - player.GetTransform().position).sqrMagnitude > 64f && (enemyTransform.position - player.GetTransform().position).sqrMagnitude < 225f)
			{
				return true;
			}
			return false;
		}

		public void StartJump()
		{
			lastRushingTime = Time.time;
			Vector3 vector = new Vector3(enemyTransform.position.x, 10000.1f, enemyTransform.position.z);
			float num = 0f;
			Vector3 vector2 = new Vector3(player.GetTransform().position.x, 10000.1f, player.GetTransform().position.z) - vector;
			float magnitude = vector2.magnitude;
			float num2 = 10f;
			float num3 = magnitude / num2;
			float num4 = (num - 0.5f * Physics.gravity.y * num3 * num3) / num3;
			speed = Vector3.up * num4 + vector2.normalized * num2;
			animation.CrossFade("JumpStart01");
			audio.PlayAudio("Special");
			jumpended = false;
		}

		public bool LookAroundTimOut()
		{
			if (Time.time - lookAroundStartTime > 2f)
			{
				return true;
			}
			return false;
		}

		public override void DoMove(float deltaTime)
		{
			Vector3 vector = enemyTransform.TransformDirection(Vector3.forward);
			vector += Physics.gravity * 0.2f;
			controller.Move(vector * mAttributes.MoveSpeed * deltaTime);
			audio.PlayAudio("Walk");
		}

		public override EnemyState EnterSpecialState(float deltaTime)
		{
			EnemyState result = null;
			lastTarget = player.GetTransform().position;
			if (Time.time - lastRushingTime > 5f && Time.time - lookAroundStartTime > 10f)
			{
				int num = Random.Range(0, 100);
				if (num < 10)
				{
					result = new LookAroundState();
					lookAroundStartTime = Time.time;
				}
				else if (ReadyForJump())
				{
					StartJump();
					result = new JumpState();
				}
			}
			return result;
		}
	}
}
