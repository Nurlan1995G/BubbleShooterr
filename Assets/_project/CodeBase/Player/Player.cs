using UnityEngine;

namespace Assets._project.CodeBase
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private Transform _shootingPoint;
        [SerializeField] private float _maxShootingForce = 15f;
        [SerializeField] private float _minShootingForce = 5f;
        [SerializeField] private LineRenderer _lineRenderer;
        [SerializeField] private float _maxLineLength = 5f;  // Максимальная длина луча

        private BallManager _ballManager;
        private Ball _currentBall;
        private float _shootingForce;
        private bool _isCharging;

        public void Construct(BallManager ballManager)
        {
            _ballManager = ballManager;
        }

        private void Update()
        {
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

                    Rigidbody2D rb = _currentBall.GetComponent<Rigidbody2D>();

                    if (rb != null)
                    {
                        rb.velocity = Vector2.zero;
                        rb.angularVelocity = 0f;
                    }
                }
            }

            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 aimDirection = new Vector2(mousePosition.x - transform.position.x, mousePosition.y - transform.position.y);
            aimDirection.Normalize();

            // Отображаем луч, показывающий направление выстрела
            DrawAimingLine(aimDirection);

            // Начинаем заряжать "пружину" при нажатии правой кнопки мыши
            if (Input.GetMouseButton(1))
            {
                _isCharging = true;
                _shootingForce = Mathf.Lerp(_minShootingForce, _maxShootingForce, Time.time);
            }

            // Выстрел при отпускании правой кнопки
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
                Rigidbody2D rb = _currentBall.GetComponent<Rigidbody2D>();
                if (rb != null)
                {
                    rb.velocity = Vector2.zero;
                    rb.AddForce(direction * _shootingForce, ForceMode2D.Impulse);
                }

                _currentBall = null;  // Шар выпущен, нужно получить новый
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
    }
}
