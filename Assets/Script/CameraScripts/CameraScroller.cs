using UnityEngine;

public class CameraScroller : MonoBehaviour
{
    public int rowPosition;
    private int altePosition;
    public float scrollSpeed;
    public bool demoMode;
    public float fadeInSpeed;
    public float fadeInAcceleration;
    private LevelGenerator LevelGenerator;
    private GameManager GameManager;
    private MenuDemoMode menuDemoMode;


    // Use this for initialization
    void Awake()
    {
        LevelGenerator = FindObjectOfType<LevelGenerator>();
        GameManager = FindObjectOfType<GameManager>();
        menuDemoMode = FindObjectOfType<MenuDemoMode>();
        scrollSpeed = 0.5f;
        altePosition = -1;
        fadeInSpeed = 0.001f;
        fadeInAcceleration = 0.009f;
        demoMode = false;
    }

    public void restartCameraScroller()
    {
        altePosition = -1;
        rowPosition = -26;
        scrollSpeed = 0f;
        transform.localPosition = new Vector3(0f, 0f, -26f);
        demoMode = false;
        fadeInSpeed = 0.001f;
        fadeInAcceleration = 0.009f;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if(GameManager.gameStatePlay || menuDemoMode.demoRunning)
        {
            fadeInSpeed = Mathf.Clamp(fadeInSpeed+ fadeInAcceleration, 0.001f, 1f);
            transform.Translate(0f, 0f, scrollSpeed * Time.deltaTime * fadeInSpeed);
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