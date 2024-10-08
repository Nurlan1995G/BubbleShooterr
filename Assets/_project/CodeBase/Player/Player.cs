using System;
using System.Collections;
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
        private Camera _camera;
        private Ball _currentBall;
        
        private float _shootingForce;
        private bool _isCharging;
        private bool _hatShot;

        public void Construct(BallManager ballManager)
        {
            _ballManager = ballManager;

            _camera = Camera.main;
        }

        private void Update()
        {
            if(!_hatShot)
                AimAndShoot();
        }

        private void AimAndShoot()
        {
            if (_currentBall == null)
            {
                _currentBall = _ballManager.GetRandomBall();

                if (_currentBall != null)
                {
                    _currentBall.transform.position = _shootingPoint.position;
                    _currentBall.gameObject.SetActive(true);
                }
            }

            Vector3 mousePosition = _camera.ScreenToWorldPoint(Input.mousePosition);
            Vector2 aimDirection = new Vector2(mousePosition.x - transform.position.x, mousePosition.y - transform.position.y);
            aimDirection.Normalize();

            DrawAimingLine(aimDirection);

            if (Input.GetMouseButton(1))
                _isCharging = true;

            if (_isCharging && Input.GetMouseButtonUp(1))
            {
                ShootBall(aimDirection);
                _isCharging = false;
            }
        }

        private void ShootBall(Vector2 direction)
        {
            if (_currentBall != null)
            {
                float force = Mathf.Lerp(_minShootingForce, _maxShootingForce, Time.time);
                _currentBall.MoveBall(direction, force);
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
