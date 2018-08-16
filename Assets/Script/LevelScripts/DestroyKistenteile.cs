using UnityEngine;
using System.Collections;

public class DestroyKistenteile : MonoBehaviour {

    //Use this for initialization
    void Start()
    {
        Destroy(gameObject, 1f);
    }

    // Use this for initialization
    // void Start()
    // {
    //     StartCoroutine(destroyKistenteile());
    // }

    // IEnumerator destroyKistenteile()
    // {
    //     yield return new WaitForSeconds(1f);
    //     gameObject.SetActive(false);
    //     StopAllCoroutines();
    // }
}