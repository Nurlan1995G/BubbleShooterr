using UnityEngine;

namespace Assets._project.CodeBase
{
    public class CameraRotate : MonoBehaviour
    {
        private Vector2 _aimDirection;
        private Player _player;
        private Camera _camera;

        public Vector2 AimDirection => _aimDirection;

        public void Construct(Player player)
        {
            _player = player;
            _camera = Camera.main;
        }

        private void Update()
        {
            _aimDirection = GetAimDirection();
        }

        private Vector2 GetAimDirection()
        {
            Vector3 mousePosition = _camera.ScreenToWorldPoint(Input.mousePosition);
            Vector2 aimDirection = new Vector2(mousePosition.x - _player.transform.position.x, mousePosition.y - _player.transform.position.y);
            return aimDirection.normalized;
        }
    }

}
