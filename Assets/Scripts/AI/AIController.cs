using Lean.Pool;
using UnityEngine;
using UnityEngine.AI;

namespace WinterUniverse
{
    public class AIController : MonoBehaviour
    {
        private PawnController _pawn;

        private ALIFEMode _ALIFEMode;

        private NavMeshAgent _agent;
        private AIDetectionModule _aiDetectionModule;
        private bool _reachedDestination;

        public PawnController Pawn => _pawn;
        public NavMeshAgent Agent => _agent;
        public AIDetectionModule AIDetectionModule => _aiDetectionModule;
        public bool ReachedDestination => _reachedDestination;
        public ALIFEMode ALIFEMode => _ALIFEMode;

        public void Initialize()
        {
            _pawn = LeanPool.Spawn(GameManager.StaticInstance.ObjectManager.HumanPrefab).GetComponent<PawnController>();
            _agent = GetComponent<NavMeshAgent>();
            _aiDetectionModule = GetComponent<AIDetectionModule>();
            _agent.updateRotation = false;
            _agent.height = _pawn.PawnAnimator.Height;
            _agent.radius = _pawn.PawnAnimator.Radius;
        }

        public void OnUpdate()
        {
            _pawn.IsRunning = false;
            _pawn.IsFiring = false;
            _pawn.IsAiming = false;
            _pawn.MoveDirection = _agent.desiredVelocity;
            if (_pawn.PawnCombat.CurrentTarget != null && _pawn.PawnCombat.CurrentTargetIsVisible())
            {
                _pawn.LookDirection = (_pawn.PawnCombat.CurrentTarget.transform.position - transform.position).normalized;
                _pawn.IsFiring = true;
                _pawn.IsAiming = true;
            }
            else if (!_reachedDestination)
            {
                _pawn.LookDirection = _agent.desiredVelocity;
                _pawn.IsRunning = true;
            }
            else
            {
                _pawn.LookDirection = transform.forward;
            }
            CheckALIFE();
            ProcessStateMachine();
            transform.SetPositionAndRotation(_pawn.transform.position, _pawn.transform.rotation);
            _pawn.OnUpdate();
        }

        private void CheckALIFE()
        {
            if (_ALIFEMode == ALIFEMode.Simple)
            {
                // other stuff
                if (GameManager.StaticInstance.PlayerManager != null && Vector3.Distance(transform.position, GameManager.StaticInstance.PlayerManager.transform.position) < 250f)
                {
                    _ALIFEMode = ALIFEMode.Advanced;
                    // set values aka rendering/animation to advanced
                }
            }
            else if (_ALIFEMode == ALIFEMode.Advanced)
            {
                // other stuff
                if (GameManager.StaticInstance.PlayerManager == null || Vector3.Distance(transform.position, GameManager.StaticInstance.PlayerManager.transform.position) > 250f)
                {
                    _ALIFEMode = ALIFEMode.Simple;
                    // set values aka rendering/animation to simple
                }
            }
        }

        private void ProcessStateMachine()
        {
            if (_agent.hasPath && _agent.remainingDistance > _agent.stoppingDistance)
            {
                _reachedDestination = false;
                if (_pawn.IsPerfomingAction)
                {
                    StopMovement();
                }
            }
            else if (!_reachedDestination)
            {
                StopMovement();
            }
        }

        public void StopMovement()
        {
            _reachedDestination = true;
            _agent.ResetPath();
        }
    }
}