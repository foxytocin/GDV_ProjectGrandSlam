using UnityEngine;

public class DestroyScroller : MonoBehaviour
{
    private int oldDummy;
    private int dummyPos;
    public LevelGenerator LevelGenerator;
    public CameraMovement camMove;
    public GameObject dummy;
    private Vector3 target;
    private LevelGenerator levelGenerator;

    // Use this for initialization
    void Start()
    {
        oldDummy = -1;
        dummy = new GameObject("dummy");
        dummy.transform.position = new Vector3(15, 0, -10f);

        // camMove = GameObject.Find("HorizontalAxis").GetComponent<CameraMovement>();
        // levelGenerator = GameObject.Find("LevelGenerator").GetComponent<LevelGenerator>();
        camMove = FindObjectOfType<CameraMovement>();
        levelGenerator = FindObjectOfType<LevelGenerator>();
    }

    // Update is called once per frame
    void Update()
    {
        target = camMove.centerPoint;
        dummyPos = (int) dummy.transform.position.z;

        moveDummy(target);

        //Prüft ob die Camera genau EINE Zeile weitergescrollt ist um die cleanLine() für genau diese 1 Zeile aufzurufen.
        if (dummyPos > oldDummy)
        {
            oldDummy = dummyPos;
            levelGenerator.cleanLine(dummyPos);
            //LevelGenerator.createWorld(((Mathf.RoundToInt(camMove.dummy.transform.position.z))+ 15 + LevelGenerator.tiefeLevelStartBasis));
            //LevelGenerator.createWorld((dummyPos + 8 + LevelGenerator.tiefeLevelStartBasis));
        }
    }

    private void moveDummy(Vector3 target)
    {
        Vector3 pos = Vector3.Lerp(dummy.transform.position, new Vector3(15f, 0f, target.z + 3f), 0.05f * Time.deltaTime);
        dummy.transform.position = pos;            
    }
}