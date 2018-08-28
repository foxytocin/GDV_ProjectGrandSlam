using UnityEngine;

public class CameraScroller : MonoBehaviour
{
    public int rowPosition;
    public int startPositon;
    private int altePosition;
    public bool gameStatePlay;
    
    public LevelGenerator LevelGenerator;

    // Use this for initialization
    void Start()
    {
        gameStatePlay = false;
        startPositon = (int)transform.position.z;
        altePosition = -1;
    }

    public void restartCameraScroller()
    {
        altePosition = -1;
        rowPosition = startPositon;
        transform.position = Vector3.Lerp(transform.position, new Vector3 (0f, 0f, startPositon), 0.7f);
    }

    // Update is called once per frame
    void Update()
    {
        
        if(gameStatePlay)
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