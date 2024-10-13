using UnityEngine;

namespace Assets._project.CodeBase.Interface
{
    public interface IBallControll
    {
        void Activate();
        void Deactivate();
        void MoveBall(Vector3 direction);
        void SetPosition(Vector3 position);
        void StoppingMoving();
        Vector2 GetBallSpeed();
    }
}
