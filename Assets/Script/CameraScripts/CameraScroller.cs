using UnityEngine;

public class CameraScroller : MonoBehaviour
{
    public int rowPosition;
    private int altePosition;
    public float scrollSpeed;
    private LevelGenerator LevelGenerator;
    private GameManager GameManager;


    // Use this for initialization
    void Awake()
    {
        LevelGenerator = FindObjectOfType<LevelGenerator>();
        GameManager = FindObjectOfType<GameManager>();
        scrollSpeed = 0.5f;
        altePosition = -1;
    }

    public void restartCameraScroller()
    {
        altePosition = -1;
        rowPosition = -26;
        scrollSpeed = 0.5f;
        transform.localPosition = new Vector3(0f, 0f, -26f);
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if(GameManager.gameStatePlay)
        {
            transform.Translate(0f, 0f, scrollSpeed * Time.deltaTime);
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

}