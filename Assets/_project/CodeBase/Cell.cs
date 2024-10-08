using System;
using UnityEngine;

namespace Assets._project.CodeBase
{
    public class Cell : MonoBehaviour
    {
        private bool _isBusy;

        public bool IsBusy => _isBusy;

        public void PlaceBall(Ball ball)
        {
            if (!_isBusy) 
            {
                ball.transform.position = transform.position;
                ball.gameObject.SetActive(true);
                _isBusy = true; 
            }
        }

        public void FreeCell()
        {
            _isBusy = false;  
        }
    }
}
