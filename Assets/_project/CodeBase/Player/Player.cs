using Assets._project.CodeBase.Interface;
using System.Collections;
using TMPro;
using UnityEngine;

namespace Assets._project.CodeBase
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private Transform _shootingPoint;
        [SerializeField] private float _maxShootingForce = 15f;
        [SerializeField] private float _minShootingForce = 5f;
        [SerializeField] private LineRenderer _lineRenderer;
        [SerializeField] private float _maxLineLength = 5f;

        private BallManager _ballManager;
        private PlayerInput _input;
        private CameraRotate _camera;
        private IBallMovement _currentBall;

        private bool _hatShot;

        public void Construct(BallManager ballManager, PlayerInput input, CameraRotate camera)
        {
            _ballManager = ballManager;
            _input = input;
            _camera = camera;
        }

        private void Update()
        {
            if (!_hatShot)
                AimAndShoot();
        }

        private void AimAndShoot()
        {
            if (_currentBall == null)
                PrepareBallForShooting();

            Vector2 aimDirection = _camera.AimDirection;
            DrawAimingLine(aimDirection);

            if (_input.IsShotReleased())
                ShootBall(aimDirection);
        }

        private void PrepareBallForShooting()
        {
            _currentBall = _ballManager.GetRandomBall();

            if (_currentBall != null)
            {
                _currentBall.SetPosition(_shootingPoint.position);
                _currentBall.Activate();
            }
        }

        private void ShootBall(Vector2 direction)
        {
            if (_currentBall != null)
            {
                _currentBall.MoveBall(direction);
                _hatShot = true;
                StartCoroutine(DelayNextBall());
            }
        }

        private void DrawAimingLine(Vector2 aimDirection)
        {
            _lineRenderer.enabled = true;
            _lineRenderer.positionCount = 2;
            _lineRenderer.SetPosition(0, _shootingPoint.position);

            Vector3 endPosition = _shootingPoint.position + (Vector3)aimDirection * _maxLineLength;
            _lineRenderer.SetPosition(1, endPosition);
        }

        private IEnumerator DelayNextBall()
        {
            yield return new WaitForSeconds(0.5f);
            _currentBall = null;
            _hatShot = false;
        }
    }
}
