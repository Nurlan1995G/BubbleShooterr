using UnityEngine;

namespace Assets._project.CodeBase.Interface
{
    public interface IBallMovement
    {
        void Activate();
        void Diactivate();
        void MoveBall(Vector3 direction);
        void SetPosition(Vector3 position);
        void StoppingMoving();
    }
}
