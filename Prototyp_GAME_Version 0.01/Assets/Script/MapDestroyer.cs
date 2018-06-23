using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapDestroyer : MonoBehaviour {

    public WorldScript World;

    //Wird nach Zeitablauf der Bombe durch die BombeScript aufgerufen und emfängt die Position der Bombe.
    public void Explode(int x, int z)
    {
        //Controlliert benachbarte Zellen auf Bomben, Kisten und Wände
        ExplodeCell(x, z);
        ExplodeCell(x + 1, z);
        ExplodeCell(x, z + 1);
        ExplodeCell(x - 1, z);
        ExplodeCell(x, z - 1);
    }

    void ExplodeCell(int x, int z) {

        //Speichert das GameObject was an der uebergebenen Coordinate gefunden wird um dann zu prüfen was es ist.
        GameObject ObjectInCell = World.WorldArray[x, z];
        Debug.Log("In der Celle ist eine " +ObjectInCell.name);

        if(ObjectInCell.name == "Bombe")
        {
            Destroy(ObjectInCell);    
        }

        if (ObjectInCell.name == "Kiste")
        {
            Destroy(ObjectInCell);
        }

        if (ObjectInCell.name == "Wand")
        {
            //return;
        }

    }


}
