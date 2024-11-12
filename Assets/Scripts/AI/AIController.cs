using UnityEngine;
using UnityEngine.AI;

namespace WinterUniverse
{
    public class AIController : PawnController
    {
        private ALIFEMode _ALIFEMode;
        private AIActionConfig _currentAction;
        private NPCState _currentState;
        private IdleState _idleState;
        private ChaseState _chaseState;
        private CombatPhase _combatPhase;
        private AttackState _attackState;
        private float _actionRecoveryTimer;

        private NavMeshAgent _agent;
        private AIDetectionModule _aiDetectionModule;
        private Vector3 _rootPosition;
        private bool _reachedDestination;

        public NavMeshAgent Agent => _agent;
        public AIDetectionModule AIDetectionModule => _aiDetectionModule;
        public Vector3 RootPosition => _rootPosition;
        public bool ReachedDestination => _reachedDestination;
        public ALIFEMode ALIFEMode => _ALIFEMode;
        public AIActionConfig CurrentAction => _currentAction;
        public NPCState CurrentState => _currentState;
        public IdleState IdleState => _idleState;
        public ChaseState ChaseState => _chaseState;
        public CombatPhase CombatPhase => _combatPhase;
        public AttackState AttackState => _attackState;
        public float ActionRecoveryTimer => _actionRecoveryTimer;

        public override Vector2 GetMoveInput()
        {
            if (!_reachedDestination)
            {
                return new Vector2(Vector3.Dot(_agent.desiredVelocity, transform.right), Vector3.Dot(_agent.desiredVelocity, transform.forward)).normalized;
            }
            return Vector2.zero;
        }

        public override Vector3 GetLookDirection()
        {
            if (PawnCombat.CurrentTarget != null && PawnCombat.CurrentTargetIsVisible())
            {
                return (PawnCombat.CurrentTarget.transform.position - transform.position).normalized;
            }
            else if (!_reachedDestination)
            {
                return _agent.desiredVelocity;
            }
            else
            {
                return Vector3.zero;
            }
        }

        public override void CreateCharacter(PawnSaveData data)
        {
            _agent = GetComponentInChildren<NavMeshAgent>();
            _aiDetectionModule = GetComponent<AIDetectionModule>();
            _idleState = Instantiate(_idleState);
            _chaseState = Instantiate(_chaseState);
            _combatPhase = Instantiate(_combatPhase);
            _combatPhase.ResetPhase();
            _combatPhase.InstantiateStates();
            _attackState = Instantiate(_attackState);
            _currentState = _idleState;
            _agent.updateRotation = false;
            _agent.height = _pawnAnimator.Height;
            _agent.radius = _pawnAnimator.Radius;
            base.CreateCharacter(data);
        }

        protected override void FixedUpdate()
        {
            base.FixedUpdate();
            if (_actionRecoveryTimer > 0f && !IsPerfomingAction)
            {
                _actionRecoveryTimer -= Time.fixedDeltaTime;
            }
            if (_ALIFEMode == ALIFEMode.Simple)
            {
                // other stuff
                if (GameManager.StaticInstance.Player != null && Vector3.Distance(transform.position, GameManager.StaticInstance.Player.transform.position) < 250f)
                {
                    _ALIFEMode = ALIFEMode.Advanced;
                    // set values aka rendering/animation to advanced
                }
            }
            else if (_ALIFEMode == ALIFEMode.Advanced)
            {
                // other stuff
                if (GameManager.StaticInstance.Player == null || Vector3.Distance(transform.position, GameManager.StaticInstance.Player.transform.position) > 250f)
                {
                    _ALIFEMode = ALIFEMode.Simple;
                    // set values aka rendering/animation to simple
                }
            }
            ProcessStateMachine();
        }

        private void ProcessStateMachine()
        {
            if (!IsPerfomingAction && _combatPhase.IsReadyToChangePhase(PawnStats.HealthPercent))
            {
                PawnAnimator.PlayActionAnimation("Phase Change", true);
            }
            NPCState nextState = _currentState?.Tick(this);
            if (nextState != null)
            {
                _currentState = nextState;
            }
            _agent.transform.localPosition = Vector3.zero;
            //Agent.transform.localRotation = Quaternion.identity;
            //IsMoving = Agent.enabled && Agent.remainingDistance > Agent.stoppingDistance;
            if (_agent.enabled && _agent.hasPath && _agent.remainingDistance > _agent.stoppingDistance)
            {
                _reachedDestination = false;
                if (IsPerfomingAction)
                {
                    StopMovement();// TODO ???
                }
            }
            else
            {
                _reachedDestination = true;
            }
        }

        public void StopMovement()
        {
            _reachedDestination = true;
            _agent.ResetPath();
        }
    }
}