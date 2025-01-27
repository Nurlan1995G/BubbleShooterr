﻿using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] private bool _isMoving = false;
    [field: SerializeField] public string ColorBall { get; private set; }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out Ball ball))
        {
            if (_isMoving && ball != null)
                ResolveCollision(ball);
        }
    }

    private void ResolveCollision(Ball otherBall)
    {
        if (ColorBall == otherBall.ColorBall)
            CheckForMatch(otherBall);
        else
            BounceBack();
    }

    private void CheckForMatch(Ball otherBall)
    {
        List<Ball> matchingBalls = new List<Ball>();
        FindMatchingBalls(this, ref matchingBalls);

        if (matchingBalls.Count > 2)
        {
            foreach (Ball ball in matchingBalls)
                Destroy(ball.gameObject);  
        }
    }

    private void FindMatchingBalls(Ball currentBall, ref List<Ball> matchingBalls)
    {
        if (!matchingBalls.Contains(currentBall))
        {
            matchingBalls.Add(currentBall);
            Collider2D[] nearbyBalls = Physics2D.OverlapCircleAll(currentBall.transform.position, 1.0f);

            foreach (var collider in nearbyBalls)
            {
                Ball nextBall = collider.GetComponent<Ball>();
                
                if (nextBall != null && nextBall.ColorBall == currentBall.ColorBall)
                    FindMatchingBalls(nextBall, ref matchingBalls);
            }
        }
    }

    private void BounceBack() =>
        transform.position += new Vector3(0, 0.2f, 0); 
}
