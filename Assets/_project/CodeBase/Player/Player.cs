using Assets._project.CodeBase.Interface;
using Assets._project.CodeBase.Sounds;
using Assets._project.Config;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Assets._project.CodeBase
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private LineRenderer _lineRenderer;
        [SerializeField] private Transform _nextBallPosition;
        [SerializeField] private Transform _shootPosition;
        [SerializeField] private TextMeshProUGUI _textCountBall;
        [SerializeField] private ParticleSystem _particleEffect;
        [SerializeField] private TextMeshProUGUI _scoreText;

        private PlayerData _playerData;
        private BallManager _ballManager;
        private PlayerInput _input;
        private List<SideWall> _sideWalls;
        private IBallControll _currentBall;
        private IBallControll _nextBall;

        private bool _hasShot;
        private int _remainingBalls;
        private int _score;

        public void Construct(PlayerData playerData, BallManager ballManager, PlayerInput input, List<SideWall> sideWalls)
        {
            _playerData = playerData;
            _ballManager = ballManager;
            _input = input;
            _sideWalls = sideWalls;

            _remainingBalls = _playerData.TotalBall; 
            UpdateBallCountUI(); 
            InitializeBalls();
        }

        private void InitializeBalls()
        {
            if (_remainingBalls > 0)
            {
                _currentBall = _ballManager.GetRandomBall();

                if (_currentBall != null)
                {
                    _currentBall.SetPosition(transform.position);
                    _currentBall.Activate();
                }

                PrepareNextBall();
            }
        }

        private void Update()
        {
            _input.Update();

            if (!_hasShot && _remainingBalls > 0)
                AimAndShoot();
        }

        public void AddScore(int score)
        {
            _score += score;
            UpdateScoreTextUI();
        }

        private void AimAndShoot()
        {
            if (_currentBall == null)
                PrepareBallForShooting();

            Vector2 aimDirection = _input.AimDirection;

            DrawAimingLine(aimDirection);

            if (_input.IsShotReleased())
                ShootBall(aimDirection);
        }

        private void PrepareBallForShooting()
        {
            if (_nextBall != null)
            {
                _currentBall = _nextBall;
                _currentBall.SetPosition(transform.position);
                _currentBall.Activate();
                PrepareNextBall();
            }
        }

        private void PrepareNextBall()
        {
            if (_remainingBalls > 1)
            {
                _nextBall = _ballManager.GetRandomBall();
                _particleEffect.Play();

                if (_nextBall != null)
                {
                    _nextBall.SetPosition(_nextBallPosition.position);
                    _nextBall.Activate(); 
                }
            }
            else
            {
                _nextBall = null;
                SoundHandler.Instance.PlayLose();
            }
        }

        private void ShootBall(Vector2 direction)
        {
            if (_currentBall != null)
            {
                float force = CalculateShootingForce();
                _currentBall.MoveBall(direction * force);

                UpdateSideWallsWithBallSpeed();
                _remainingBalls--; 
                UpdateBallCountUI(); 
                _hasShot = true;
                StartCoroutine(DelayNextBall());
            }
        }

        private float CalculateShootingForce()
        {
            float pullDistance = Mathf.Clamp(_input.PullDistance, 0, _playerData.MaxLineLength);
            return Mathf.Lerp(_playerData.MinShootingForce, _playerData.MaxShootingForce, pullDistance / _playerData.MaxLineLength);
        }

        private void UpdateSideWallsWithBallSpeed()
        {
            foreach (SideWall sideWall in _sideWalls)
                sideWall.SetBallSpeed(_currentBall);
        }

        private void DrawAimingLine(Vector2 aimDirection)
        {
            _lineRenderer.enabled = true;
            _lineRenderer.positionCount = 2;
            _lineRenderer.SetPosition(0, _shootPosition.position);

            RaycastHit2D hit = Physics2D.Raycast(_shootPosition.position, aimDirection, 10);
            Vector3 endPosition = _shootPosition.position + (Vector3)aimDirection * 10;

            if (hit.collider != null)
            {
                if (hit.collider.TryGetComponent(out SideWall sideWall))
                {
                    Vector2 reflectedDirection = Vector2.Reflect(aimDirection, hit.normal);

                    RaycastHit2D secondHit = Physics2D.Raycast(hit.point, reflectedDirection, 10);

                    _lineRenderer.positionCount = 3;
                    _lineRenderer.SetPosition(1, hit.point);

                    if (secondHit.collider != null && secondHit.collider.TryGetComponent(out IBallControll ballComponent))
                        endPosition = secondHit.point;
                    else
                        endPosition = (Vector3)hit.point + (Vector3)reflectedDirection * 20;

                    _lineRenderer.SetPosition(2, endPosition);
                }
                else if (hit.collider.TryGetComponent(out IBallControll ball))
                {
                    endPosition = hit.point;
                    _lineRenderer.SetPosition(1, endPosition);
                }
            }
            else
                _lineRenderer.SetPosition(1, endPosition);
        }


        private IEnumerator DelayNextBall()
        {
            yield return new WaitForSeconds(0.5f);
            _currentBall = null;
            _hasShot = false;

            if (_remainingBalls > 0)
                PrepareBallForShooting();
        }

        private void UpdateBallCountUI() =>
            _textCountBall.text = _remainingBalls.ToString();

        private void UpdateScoreTextUI() =>
            _scoreText.text = _score.ToString();
    }
}
