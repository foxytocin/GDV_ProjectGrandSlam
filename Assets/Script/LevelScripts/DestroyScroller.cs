using UnityEngine;

public class DestroyScroller : MonoBehaviour
{
    private int oldDummy;
    public int dummyPos;
    public LevelGenerator LevelGenerator;
    public CameraMovement camMove;
    private Vector3 target;
    private LevelGenerator levelGenerator;
    public bool gameStatePlay;
    private RulesScript rules;

    void Awake()
    {
        camMove = FindObjectOfType<CameraMovement>();
        levelGenerator = FindObjectOfType<LevelGenerator>();
        gameStatePlay = false;
        rules = FindObjectOfType<RulesScript>();
    }

    // Use this for initialization
    void Start()
    {
        oldDummy = -1;
        transform.position = new Vector3(0, 0, -10f);
    }

    public void restartDestroyScroller()
    {
        gameStatePlay = false;
        oldDummy = -1;
        transform.position = new Vector3(0, 0, -10f);
        dummyPos = (int)transform.position.z;
    }

    // Update is called once per frame
    void Update()
    {
        target = camMove.centerPoint;
        dummyPos = (int)transform.position.z;
        
        //Erstes if nur zu testzwecken für cameraorbit
        if(!rules.resultScreen.activeSelf)
        {
            if (gameStatePlay)
                moveDummy(target);
        }

        //Prüft ob die Camera genau EINE Zeile weitergescrollt ist um die cleanLine() für genau diese 1 Zeile aufzurufen.
        if (dummyPos > oldDummy && gameStatePlay)
        {
            oldDummy = dummyPos;
            levelGenerator.cleanLine(dummyPos);
        }
    }

    private void moveDummy(Vector3 target)
    {
        Vector3 pos = Vector3.Lerp(transform.position, new Vector3(0f, 0f, target.z + 3f), 0.1f * Time.deltaTime);
        transform.position = pos;
    }
}