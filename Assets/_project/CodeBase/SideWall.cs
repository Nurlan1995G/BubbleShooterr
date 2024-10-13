using Assets._project.CodeBase.Interface;
using UnityEngine;

namespace Assets._project.CodeBase
{
    public class SideWall : MonoBehaviour
    {
        private Vector2 _speedBall;

        public void SetBallSpeed(IBallControll currentBall)
        {
            _speedBall = currentBall.GetBallSpeed();
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.TryGetComponent(out IBallControll ball))
            {
                Vector2 normal = collision.contacts[0].normal;

                Vector2 reflectedVelocity = Vector2.Reflect(_speedBall, normal);

                ball.MoveBall(reflectedVelocity);
            }
        }
    }
}
