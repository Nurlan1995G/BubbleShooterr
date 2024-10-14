using Assets._project.CodeBase.Corountine;
using Assets._project.CodeBase.Interface;
using Assets._project.CodeBase.Sounds;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets._project.CodeBase
{
    public class PlayerShoot
    {
        private Player _player;
        private PlayerInput _input;
        private BallManager _ballManager;
        
        private ParticleSystem _effectNextBall;
        
        private List<SideWall> _sideWalls;

        private IBallControll _currentBall;
        private IBallControll _nextBall;
        
        private bool _hasShot;

        public PlayerShoot(Player player, BallManager ballManager, PlayerInput input, List<SideWall> sideWalls, ParticleSystem effectNextBall)
        {
            _player = player;
            _ballManager = ballManager;
            _input = input;
            _sideWalls = sideWalls;
            _effectNextBall = effectNextBall;
        }

        public void AimAndShoot()
        {
            if (_currentBall == null)
                PrepareCurrentBall();

            Vector2 aimDirection = _input.AimDirection;
            DrawAimingLine(aimDirection);

            if (_input.IsShotReleased())
                ShootBall(aimDirection);
        }

        public bool CanShoot() => !_hasShot;

        public void PrepareCurrentBall()
        {
            if (_player.GetRemainingBalls() > 0)
            {
                _currentBall = _ballManager.GetRandomBall();

                if (_currentBall != null)
                {
                    _currentBall.SetPosition(_player.transform.position);
                    _currentBall.Activate();
                    PrepareNextBall();
                }
            }
        }

        private void PrepareNextBall()
        {
            if (_player.GetRemainingBalls() > 1)
            {
                _nextBall = _ballManager.GetRandomBall();
                _effectNextBall.Play();

                if (_nextBall != null)
                {
                    _nextBall.SetPosition(_player.NextBallPosition.position);
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

                _player.DecreaseBallCount();
                _hasShot = true;
                CoroutineRunner.Instance.StartCoroutine(DelayNextBall());
            }
        }

        private float CalculateShootingForce()
        {
            float pullDistance = Mathf.Clamp(_input.PullDistance, 0, _player.PlayerData.MaxLineLength);

            return Mathf.Lerp(_player.PlayerData.MinShootingForce, _player.PlayerData.MaxShootingForce, pullDistance / _player.PlayerData.MaxLineLength);
        }

        private void UpdateSideWallsWithBallSpeed()
        {
            foreach (SideWall sideWall in _sideWalls)
                sideWall.SetBallSpeed(_currentBall);
        }

        private void DrawAimingLine(Vector2 aimDirection)
        {
            _player.LineRenderer.enabled = true;
            _player.LineRenderer.positionCount = 2;
            _player.LineRenderer.SetPosition(0, _player.ShootPosition.position);

            RaycastHit2D hit = Physics2D.Raycast(_player.ShootPosition.position, aimDirection, _player.PlayerData.MaxPullDistance);
            Vector3 endPosition = _player.ShootPosition.position + (Vector3)aimDirection * _player.PlayerData.MaxPullDistance;

            if (hit.collider != null)
            {
                if (hit.collider.TryGetComponent(out SideWall sideWall))
                {
                    Vector2 reflectedDirection = Vector2.Reflect(aimDirection, hit.normal);

                    RaycastHit2D secondHit = Physics2D.Raycast(hit.point, reflectedDirection, _player.PlayerData.MaxPullDistance);

                    _player.LineRenderer.positionCount = 3;
                    _player.LineRenderer.SetPosition(1, hit.point);

                    if (secondHit.collider != null && secondHit.collider.TryGetComponent(out IBallControll ballComponent))
                        endPosition = secondHit.point;
                    else
                        endPosition = (Vector3)hit.point + (Vector3)reflectedDirection * 20;

                    _player.LineRenderer.SetPosition(2, endPosition);
                }
                else if (hit.collider.TryGetComponent(out IBallControll ball))
                {
                    endPosition = hit.point;
                    _player.LineRenderer.SetPosition(1, endPosition);
                }
            }
            else
                _player.LineRenderer.SetPosition(1, endPosition);
        }

        private IEnumerator DelayNextBall()
        {
            yield return new WaitForSeconds(0.5f);
            _currentBall = null;
            _hasShot = false;

            if (_player.GetRemainingBalls() > 0)
                PrepareCurrentBall();
        }
    }
}
