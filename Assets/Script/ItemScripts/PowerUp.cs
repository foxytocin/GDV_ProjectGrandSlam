using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour {

    //Schreibt Item Fähigkeit gewünschtem Player zu
    public void PlayerItem(int id)
    {
        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<Light>().enabled = false;

        int RandomValue = (int)(Random.Range(0, 2f));
        {

            switch (RandomValue)
            {
                case 0:

                    Item1(id);
                    break;

                case 1:
                    Item2(id);
                    break;
            }
        }
    }

    public void Item1(int id)
    {
        Debug.Log("Player" + id + " hat Item 1");
    }

    public void Item2(int id)
    {
        Debug.Log("Player" + id + " hat Item 2");
    }
























    void Update()
    {
        //Drehung des Items
        transform.eulerAngles += new Vector3(0f, 80f * Time.deltaTime, 0f);


    }
}
		
