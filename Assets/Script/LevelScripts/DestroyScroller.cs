using UnityEngine;

public class DestroyScroller : MonoBehaviour
{
    private int oldDummy;
    public int dummyPos;
    public CameraMovement camMove;
    private Vector3 target;
    private LevelGenerator levelGenerator;
    private GameManager gameManager;
    private MenuDemoMode menuDemoMode;

    void Awake()
    {
        camMove = FindObjectOfType<CameraMovement>();
        levelGenerator = FindObjectOfType<LevelGenerator>();
        gameManager = FindObjectOfType<GameManager>();
        menuDemoMode = FindObjectOfType<MenuDemoMode>();
    }

    // Use this for initialization
    void Start()
    {
        oldDummy = -1;
        transform.position = new Vector3(0, 0, -10f);
    }

    public void restartDestroyScroller()
    {
        oldDummy = -1;
        transform.position = new Vector3(0, 0, -10f);
        dummyPos = (int)transform.position.z;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (gameManager.gameStatePlay || menuDemoMode.demoRunning)
        {
            target = camMove.centerPoint;
            dummyPos = (int)transform.position.z;

            Vector3 pos = Vector3.Lerp(transform.position, new Vector3(0f, 0f, target.z + 3f), 0.1f * Time.deltaTime);
            transform.position = pos;

            //Prüft ob die Camera genau EINE Zeile weitergescrollt ist um die cleanLine() für genau diese 1 Zeile aufzurufen.
            if (dummyPos > oldDummy)
            {
                oldDummy = dummyPos;
                levelGenerator.cleanLine(dummyPos);
            }
        }
    }

}