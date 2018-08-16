using UnityEngine;
using System.Collections;

public class DestroyKistenteile : MonoBehaviour {

    // Use this for initialization
    void Start()
    {
        StartCoroutine(destroyKistenteile());
    }

    private IEnumerator destroyKistenteile()
    {
        yield return new WaitForSeconds(1f);
        gameObject.SetActive(false);
    }
}