using UnityEngine;

public class ColliderScript : MonoBehaviour {


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "OffScreen")
        {
            Destroy(other.gameObject);
        }
    }
}
