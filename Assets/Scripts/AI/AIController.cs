using Lean.Pool;
using UnityEngine;
using UnityEngine.AI;

namespace WinterUniverse
{
    public class AIController : MonoBehaviour
    {
        private PawnController _pawn;

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

        public PawnController Pawn => _pawn;
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

        public void Initialize()
        {
            _pawn = LeanPool.Spawn(GameManager.StaticInstance.WorldData.PawnPrefab).GetComponent<PawnController>();
            _agent = GetComponent<NavMeshAgent>();
            _aiDetectionModule = GetComponent<AIDetectionModule>();
            _idleState = Instantiate(_idleState);
            _chaseState = Instantiate(_chaseState);
            _combatPhase = Instantiate(_combatPhase);
            _combatPhase.ResetPhase();
            _combatPhase.InstantiateStates();
            _attackState = Instantiate(_attackState);
            _currentState = _idleState;
            _agent.updateRotation = false;
            _agent.height = _pawn.PawnAnimator.Height;
            _agent.radius = _pawn.PawnAnimator.Radius;
        }

        public void OnUpdate()
        {
            _pawn.MoveDirection = _agent.desiredVelocity;
            if (_pawn.PawnCombat.CurrentTarget != null && _pawn.PawnCombat.CurrentTargetIsVisible())
            {
                _pawn.LookDirection = (_pawn.PawnCombat.CurrentTarget.transform.position - transform.position).normalized;
            }
            else if (!_reachedDestination)
            {
                _pawn.LookDirection = _agent.desiredVelocity;
            }
            else
            {
                _pawn.LookDirection = Vector3.zero;
            }
            transform.SetPositionAndRotation(_pawn.transform.position, _pawn.transform.rotation);
            //
            if (_actionRecoveryTimer > 0f && !_pawn.IsPerfomingAction)
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
            if (!_pawn.IsPerfomingAction && _combatPhase.IsReadyToChangePhase(_pawn.PawnStats.HealthPercent))
            {
                _pawn.PawnAnimator.PlayActionAnimation("Phase Change", true);
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
                if (_pawn.IsPerfomingAction)
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