using UnityEngine;

namespace Assets._project.CodeBase
{
    public class PlayerInput
    {
        private Vector2 _aimDirection;
        private Player _player;
        private Camera _camera;

        private Vector2 _pullStartPoint;
        private bool _isPulling;

        public Vector2 AimDirection => _aimDirection;
        public float PullDistance { get; private set; }  

        public PlayerInput(Player player)
        {
            _player = player;
            _camera = Camera.main;
        }

        public void Update()
        {
            GetAimDirection();
            CalculatePull();

            if (IsChargingShot())
                StartPull();
        }

        private Vector2 GetAimDirection()
        {
            Vector3 mousePosition = _camera.ScreenToWorldPoint(Input.mousePosition);
            _aimDirection = new Vector2(mousePosition.x - _player.transform.position.x, mousePosition.y - _player.transform.position.y);
            return _aimDirection.normalized;
        }

        public bool IsChargingShot() => 
            Input.GetMouseButton(1);

        public bool IsShotReleased() => 
            Input.GetMouseButtonUp(1);

        public void StartPull()
        {
            _pullStartPoint = _camera.ScreenToWorldPoint(Input.mousePosition);
            _isPulling = true;
        }

        private void CalculatePull()
        {
            if (_isPulling && IsChargingShot())
            {
                Vector2 currentPoint = _camera.ScreenToWorldPoint(Input.mousePosition);
                PullDistance = Vector2.Distance(_pullStartPoint, currentPoint);  
            }
            else if (IsShotReleased())
            {
                _isPulling = false;  
            }
        }
    }
}
