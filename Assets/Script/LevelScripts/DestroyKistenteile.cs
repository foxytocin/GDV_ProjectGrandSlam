using UnityEngine;
using System.Collections;

public class DestroyKistenteile : MonoBehaviour {

    //Use this for initialization
    void Start()
    {
        FindObjectOfType<AudioManager>().playSound("destroyed_box");
        Destroy(gameObject, 1f);
    }
}