using UnityEngine;

namespace Assets._project.CodeBase
{
    public class Point : MonoBehaviour
    {
        [SerializeField] private bool _isBusy;

        public bool IsBusy => _isBusy;

        public void PlaceBall(Ball ball)
        {
            if (!_isBusy) 
            {
                ball.transform.position = transform.position;
                ball.gameObject.SetActive(true);
                ball.SetCurrentPoint(this);
                _isBusy = true; 
            }
        }

        public void FreeCell()
        {
            _isBusy = false;  
        }
    }
}
