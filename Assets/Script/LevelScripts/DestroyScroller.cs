using UnityEngine;

public class DestroyScroller : MonoBehaviour
{
    private int oldDummy;
    public int dummyPos;
    public LevelGenerator LevelGenerator;
    public CameraMovement camMove;
    public GameObject dummy;
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
        dummy = new GameObject("DestroyScroller");
        dummy.transform.position = new Vector3(15f, 0, -10f);
    }

    public void restartDestroyScroller()
    {
        gameStatePlay = false;
        oldDummy = -1;
        dummy.transform.position = new Vector3(15f, 0, -10f);
        dummyPos = (int)dummy.transform.position.z;
    }

    // Update is called once per frame
    void Update()
    {
        target = camMove.centerPoint;
<<<<<<< HEAD
        dummyPos = (int) dummy.transform.position.z;
        //Erstes if nur zu testzwecken für cameraorbit
        if(!rules.resultScreen.activeSelf)
        {
            if (gameStatePlay)
                moveDummy(target);
        }
=======
        dummyPos = (int)dummy.transform.position.z;

        if(gameStatePlay)
            moveDummy(target);
>>>>>>> Andi

        //Prüft ob die Camera genau EINE Zeile weitergescrollt ist um die cleanLine() für genau diese 1 Zeile aufzurufen.
        if (dummyPos > oldDummy)
        {
            oldDummy = dummyPos;
            levelGenerator.cleanLine(dummyPos);
        }
    }

    private void moveDummy(Vector3 target)
    {
        Vector3 pos = Vector3.Lerp(dummy.transform.position, new Vector3(15f, 0f, target.z + 3f), 0.1f * Time.deltaTime);
        dummy.transform.position = pos;
    }
}