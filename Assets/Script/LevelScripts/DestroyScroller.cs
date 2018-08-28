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

    void Awake()
    {
        camMove = FindObjectOfType<CameraMovement>();
        levelGenerator = FindObjectOfType<LevelGenerator>();
        gameStatePlay = false;
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
        oldDummy = -1;
        dummy.transform.position = new Vector3(15f, 0, -10f);
        dummyPos = (int) dummy.transform.position.z;
    }

    // Update is called once per frame
    void Update()
    {
        target = camMove.centerPoint;
        dummyPos = (int) dummy.transform.position.z;

        if(gameStatePlay)
            moveDummy(target);

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