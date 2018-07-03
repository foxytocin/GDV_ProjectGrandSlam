using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OffScreen : MonoBehaviour {

    private void OnTriggerEnter(Collider other)
    {

        switch (other.gameObject.tag)
        {
            case "Wand":
                Destroy(other.gameObject);
                break;

            case "Kiste":
                Destroy(other.gameObject);
                break;

            case "Item":
                Destroy(other.gameObject);
                break;

            case "OffScreen":
                Destroy(other.gameObject);
                break;

            default:
                break;
        }
    }
}
