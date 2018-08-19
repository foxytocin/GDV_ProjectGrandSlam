using UnityEngine;

public class CameraScroller : MonoBehaviour
{
    public int rowPosition;
    private int altePosition;
    private int oldDummy;
    private int dummyPos;
    public LevelGenerator LevelGenerator;
    public CameraMovement camMove;

    // Use this for initialization
    void Start()
    {
        altePosition = -1;
        oldDummy = -1;

        camMove = GameObject.Find("HorizontalAxis").GetComponent<CameraMovement>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.Translate(0, 0, LevelGenerator.LevelSpeed * Time.deltaTime);
        rowPosition = (int)transform.position.z;
        dummyPos = (int)camMove.dummy.transform.position.z;

        //Prüft ob die Camera genau EINE Zeile weitergescrollt ist um die createWorld() für genau diese 1 Zeile aufzurufen.
        if (dummyPos > oldDummy)
        //if (rowPosition > altePosition)
        {
            altePosition = rowPosition;
            oldDummy = dummyPos;

            //LevelGenerator.tiefeLevelStartBasis ist ein definierter Startwert, der x Zeilen des Levels direkt zum Start erstellt. 
            //LevelGenerator.createWorld(rowPosition + LevelGenerator.tiefeLevelStartBasis);
            //LevelGenerator.createWorld(((Mathf.RoundToInt(camMove.dummy.transform.position.z))+ 15 + LevelGenerator.tiefeLevelStartBasis));
            LevelGenerator.createWorld((dummyPos + 8 + LevelGenerator.tiefeLevelStartBasis));
        }
    }
}