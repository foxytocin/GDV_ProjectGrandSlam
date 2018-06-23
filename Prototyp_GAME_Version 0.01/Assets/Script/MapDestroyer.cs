using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapDestroyer : MonoBehaviour {

    public WorldScript World;

    //Wird nach Zeitablauf der Bombe durch die BombeScript aufgerufen und emfängt die Position der Bombe.
    public void Explode(int x, int z)
    {
        //Controlliert benachbarte Zellen auf Bomben, Kisten und Wände
        //ExplodeCell(x, z);
        ExplodeCell(x + 1, z);
        ExplodeCell(x, z + 1);
        ExplodeCell(x - 1, z);
        ExplodeCell(x, z - 1);
    }

    void ExplodeCell(int x, int z) {
        
        //Speichert das GameObject was an der uebergebenen Coordinate gefunden wird um dann zu prüfen was es ist
        if (World.WorldArray[x, z] != null)
        {
            Debug.Log("In der Celle ist eine " + World.WorldArray[x, z].name);
            if (World.WorldArray[x, z].name == "Bombe")
            {
                Debug.Log("Weiter Bombe gefunden. Kettenreaktion sollte ausgelöst werden");
                //Hier fehlt die Kettenreaktion von Bomben.
            }

            if (World.WorldArray[x, z].name == "Kiste")
            {
                Destroy(World.WorldArray[x, z]);
            }

            if (World.WorldArray[x, z].name.Contains("Item"))
            {
                Destroy(World.WorldArray[x, z]);
            }

            if (World.WorldArray[x, z].name == "Wand")
            {
       
            }
        }

    }


}
