using UnityEngine;
using UnityEngine.AI;

namespace WinterUniverse
{
    public class NPCController : Character
    {
        public ALIFEMode ALIFEMode;
        public NPCActionData CurrentAction;
        public NPCState CurrentState;
        public IdleState IdleState;
        public ChaseState ChaseState;
        public CombatPhase CombatPhase;
        public AttackState AttackState;
        public float ActionRecoveryTimer;

        [HideInInspector] public NavMeshAgent Agent;
        [HideInInspector] public NPCDetectionModule NPCDetectionModule;
        [HideInInspector] public NPCLocomotionModule NPCLocomotionModule;
        [HideInInspector] public Vector3 RootPosition;
        [HideInInspector] public bool ReachedDestination;

        protected override void Awake()
        {
            base.Awake();
            Agent = GetComponentInChildren<NavMeshAgent>();
            NPCDetectionModule = GetComponent<NPCDetectionModule>();
            NPCLocomotionModule = GetComponent<NPCLocomotionModule>();
            IdleState = Instantiate(IdleState);
            ChaseState = Instantiate(ChaseState);
            CombatPhase = Instantiate(CombatPhase);
            CombatPhase.ResetPhase();
            CombatPhase.InstantiateStates();
            AttackState = Instantiate(AttackState);
            CurrentState = IdleState;
            Agent.updateRotation = false;
        }

        public override void CreateCharacter(CharacterSaveData data)
        {
            base.CreateCharacter(data);
            Agent.height = AnimatorModule.Height;
            Agent.radius = AnimatorModule.Radius;
        }

        protected override void FixedUpdate()
        {
            base.FixedUpdate();
            if (!Spawned)
            {
                return;
            }
            if (ActionRecoveryTimer > 0f && !IsPerfomingAction)
            {
                ActionRecoveryTimer -= Time.fixedDeltaTime;
            }
            if (ALIFEMode == ALIFEMode.Simple)
            {
                // other stuff
                if (PlayerInputManager.StaticInstance.Player != null && Vector3.Distance(transform.position, PlayerInputManager.StaticInstance.Player.transform.position) < 250f)
                {
                    ALIFEMode = ALIFEMode.Advanced;
                    // set values aka rendering/animation to advanced
                }
            }
            else if (ALIFEMode == ALIFEMode.Advanced)
            {
                // other stuff
                if (PlayerInputManager.StaticInstance.Player == null || Vector3.Distance(transform.position, PlayerInputManager.StaticInstance.Player.transform.position) > 250f)
                {
                    ALIFEMode = ALIFEMode.Simple;
                    // set values aka rendering/animation to simple
                }
            }
            ProcessStateMachine();
        }

        private void ProcessStateMachine()
        {
            if (!IsPerfomingAction && CombatPhase.IsReadyToChangePhase(StatModule.HealthPercent))
            {
                AnimatorModule.PlayActionAnimation("Phase Change", true);
            }
            NPCState nextState = CurrentState?.Tick(this);
            if (nextState != null)
            {
                CurrentState = nextState;
            }
            Agent.transform.localPosition = Vector3.zero;
            //Agent.transform.localRotation = Quaternion.identity;
            //IsMoving = Agent.enabled && Agent.remainingDistance > Agent.stoppingDistance;
            if (Agent.enabled && Agent.hasPath && Agent.remainingDistance > Agent.stoppingDistance)
            {
                ReachedDestination = false;
                if (IsPerfomingAction)
                {
                    StopMovement();// TODO ???
                }
            }
            else
            {
                ReachedDestination = true;
            }
        }

        public void StopMovement()
        {
            ReachedDestination = true;
            Agent.ResetPath();
        }
    }
}