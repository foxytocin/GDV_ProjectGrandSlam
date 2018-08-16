using UnityEngine;
using System.Collections;

public class DestroyKistenteile : MonoBehaviour {

    // Use this for initialization
    void Start()
    {
        StartCoroutine(destroyKistenteile());
    }

    IEnumerator destroyKistenteile()
    {
        yield return new WaitForSeconds(1f);
        this.gameObject.SetActive(false);
        StopAllCoroutines();
    }
}