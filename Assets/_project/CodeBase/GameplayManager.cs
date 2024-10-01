using System.Collections.Generic;
using UnityEngine;

public class GameplayManager : MonoBehaviour
{
    [SerializeField] private GameObject _activeBall;
    [SerializeField] private GameObject _nextBall;
    [SerializeField] private Transform _launchPoint;
    [SerializeField] private int _remainingShots = 30;
    [SerializeField] private List<GameObject> _allBalls;
    [SerializeField] private float _fullChargeDistance = 2.0f; 

    private LineRenderer _lineRenderer;
    private bool _isShooting = false;
    private Vector3 _shootDirection;
    private float _shootForce;

    private void Start()
    {
        _lineRenderer = GetComponent<LineRenderer>();
    }

    private void Update()
    {
        HandleInput();
        CheckGameState();
    }

    private void HandleInput()
    {
        if (Input.GetMouseButton(0) && !_isShooting)
        {
            Vector3 dragPosition = GetMouseWorldPosition();
            _shootDirection = (_launchPoint.position - dragPosition).normalized;
            float distance = Vector3.Distance(_launchPoint.position, dragPosition);
            _shootForce = Mathf.Clamp(distance / _fullChargeDistance, 0.1f, 1f);

            DrawTrajectory(_shootDirection, _shootForce);
        }

        if (Input.GetMouseButtonUp(0) && !_isShooting)
        {
            ShootBall(_shootDirection, _shootForce);
        }
    }

    private void ShootBall(Vector3 direction, float force)
    {
        _isShooting = true;
        float speed = force * 10f;  
        StartCoroutine(MoveBall(_activeBall, direction, speed));
    }

    private System.Collections.IEnumerator MoveBall(GameObject ball, Vector3 direction, float speed)
    {
        while (true)
        {
            ball.transform.position += direction * speed * Time.deltaTime;
            yield return null;
        }
    }

    private void DrawTrajectory(Vector3 direction, float force)
    {
        _lineRenderer.positionCount = 2;
        _lineRenderer.SetPosition(0, _launchPoint.position);
        _lineRenderer.SetPosition(1, _launchPoint.position + direction * force * 5f);  
    }

    private void CheckGameState()
    {
        if (CheckWinCondition())
            DropRemainingBalls();

        if (_remainingShots <= 0)
        {
            Debug.Log("Game Over! No more shots.");
        }
    }

    private bool CheckWinCondition()
    {
        int lastRowCount = GetBallsInLastRow();
        return (float)lastRowCount / _allBalls.Count < 0.3f;
    }

    private int GetBallsInLastRow()
    {
        int count = 0;
        foreach (var ball in _allBalls)
        {
            if (ball.transform.position.y < -3f) 
                count++;
        }
        return count;
    }

    private void DropRemainingBalls()
    {
        foreach (var ball in _allBalls)
        {
            Rigidbody2D rb = ball.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.isKinematic = false;
                rb.AddForce(new Vector2(0, -5f), ForceMode2D.Impulse);  
            }
        }
    }

    private Vector3 GetMouseWorldPosition()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = Camera.main.WorldToScreenPoint(_launchPoint.position).z;
        return Camera.main.ScreenToWorldPoint(mousePosition);
    }
}
