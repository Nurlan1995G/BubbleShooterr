using UnityEngine;

public class BoxHTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent(out BoxH boxH))
        {
            Debug.Log("BoxH");
        }
    }
}
