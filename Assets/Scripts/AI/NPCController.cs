using UnityEngine;
using UnityEngine.AI;

namespace WinterUniverse
{
    public class NPCController : PawnController
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
        [HideInInspector] public Vector3 RootPosition;
        [HideInInspector] public bool ReachedDestination;

        public override Vector2 GetMoveInput()
        {
            if (!ReachedDestination)
            {
                return new Vector2(Vector3.Dot(Agent.desiredVelocity, transform.right), Vector3.Dot(Agent.desiredVelocity, transform.forward)).normalized;
            }
            return Vector2.zero;
        }

        public override Vector3 GetLookDirection()
        {
            if (PawnCombat.CurrentTarget != null && PawnCombat.CurrentTargetIsVisible())
            {
                return (PawnCombat.CurrentTarget.transform.position - transform.position).normalized;
            }
            else if (!ReachedDestination)
            {
                return Agent.desiredVelocity;
            }
            else
            {
                return Vector3.zero;
            }
        }

        protected override void Awake()
        {
            base.Awake();
            Agent = GetComponentInChildren<NavMeshAgent>();
            NPCDetectionModule = GetComponent<NPCDetectionModule>();
            IdleState = Instantiate(IdleState);
            ChaseState = Instantiate(ChaseState);
            CombatPhase = Instantiate(CombatPhase);
            CombatPhase.ResetPhase();
            CombatPhase.InstantiateStates();
            AttackState = Instantiate(AttackState);
            CurrentState = IdleState;
            Agent.updateRotation = false;
        }

        public override void CreateCharacter(PawnSaveData data)
        {
            base.CreateCharacter(data);
            Agent.height = PawnAnimator.Height;
            Agent.radius = PawnAnimator.Radius;
        }

        protected override void FixedUpdate()
        {
            base.FixedUpdate();
            if (ActionRecoveryTimer > 0f && !IsPerfomingAction)
            {
                ActionRecoveryTimer -= Time.fixedDeltaTime;
            }
            if (ALIFEMode == ALIFEMode.Simple)
            {
                // other stuff
                if (GameManager.StaticInstance.Player != null && Vector3.Distance(transform.position, GameManager.StaticInstance.Player.transform.position) < 250f)
                {
                    ALIFEMode = ALIFEMode.Advanced;
                    // set values aka rendering/animation to advanced
                }
            }
            else if (ALIFEMode == ALIFEMode.Advanced)
            {
                // other stuff
                if (GameManager.StaticInstance.Player == null || Vector3.Distance(transform.position, GameManager.StaticInstance.Player.transform.position) > 250f)
                {
                    ALIFEMode = ALIFEMode.Simple;
                    // set values aka rendering/animation to simple
                }
            }
            ProcessStateMachine();
        }

        private void ProcessStateMachine()
        {
            if (!IsPerfomingAction && CombatPhase.IsReadyToChangePhase(PawnStats.HealthPercent))
            {
                PawnAnimator.PlayActionAnimation("Phase Change", true);
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