using UnityEngine;

public class ColliderScript : MonoBehaviour {

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.name == "Wand")
        {
            Destroy(col.gameObject);
        }
    }
}
