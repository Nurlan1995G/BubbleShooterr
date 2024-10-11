using UnityEngine;

namespace Assets._project.CodeBase
{
    public class PlayerInput
    {
        public bool IsChargingShot()
        {
            return Input.GetMouseButton(1);
        }

        public bool IsShotReleased()
        {
            return Input.GetMouseButtonUp(1);
        }
    }
}
