using UnityEngine;

public class CameraScroller : MonoBehaviour
{
    public int rowPosition;
    private int altePosition;
    public LevelGenerator LevelGenerator;

    // Use this for initialization
    void Start()
    {
        altePosition = -1;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(0, 0, LevelGenerator.LevelSpeed * Time.deltaTime);
        rowPosition = (int)transform.position.z;

        //Prüft ob die Camera genau EINE Zeile weitergescrollt ist um die createWorld() für genau diese 1 Zeile aufzurufen.
        if (rowPosition > altePosition)
        {
            altePosition = rowPosition;

            //LevelGenerator.tiefeLevelStartBasis ist ein definierter Startwert, der x Zeilen des Levels direkt zum Start erstellt. 
            LevelGenerator.createWorld(rowPosition + LevelGenerator.tiefeLevelStartBasis);
        }
    }

    
}