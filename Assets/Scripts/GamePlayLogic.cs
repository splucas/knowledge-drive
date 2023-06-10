using System.Collections;
using UnityEngine;

public class GamePlayLogic : MonoBehaviour
{

    public MatchData ItemData; // Scriptable Object that contains the "match data"

    // Locations to spawn/move the Matchables
    public Transform[] SpawnPoints = new Transform[3];

    public GameObject MatchablePrefab;

    MatchObject[] MatchObjects = new MatchObject[3];

    public PlayerLogic playerLogic;

    AnswersManager answerManager;

    float killDelay;

    public GameObject StartPanel;
    public RestartController restartController;

    public FadeInOutCallback fader;

    public LaneLineMover LineMover;

    string lastTopic = "";

    // move items down the screen?
    bool shouldDrop = false;
    public float NormalSpeed = 1.5f;
    public float HighSpeed = 3.5f;
    float currentSpeed;

    bool resetQd = false;

    //Spawn the inital match objects
    void InitMatchables()
    {
        for(int cnt = 0; cnt < MatchObjects.Length; cnt ++)
        {
            GameObject spawnedObject = Instantiate(MatchablePrefab);
            MatchObjects[cnt] = spawnedObject.GetComponent<MatchObject>();

            spawnedObject.transform.position = SpawnPoints[cnt].position;

        }
    }

    private void Awake()
    {
        LineMover.gameObject.SetActive(false);
        playerLogic.OnCollision += HandlePlayerCollide;
        playerLogic.GetComponent<PlayerInputHandler>().OnDropFast += HandleDropImmediate;

        answerManager = GetComponent<AnswersManager>();
    }

    // Start is called before the first frame update
    void Start()
    {
        // Make sure these are running at start
        StartPanel.SetActive(true);
        restartController.gameObject.SetActive(false);

    }

    public void StartLevel()
    {
        LineMover.gameObject.SetActive(true);
        answerManager.Reset();
        fader.FadeInOut(
            // Fade In Callback
            () => {
                StartPanel.SetActive(false);
                restartController.gameObject.SetActive(false);
            },

            // FadeOut Callback
            () => {
                InitMatchables();
                ResetMatchables();
                shouldDrop = true;
            }
            );

        
    }
    

    // Update is called once per frame
    void Update()
    {
        if(shouldDrop)
        {

            float lasty=0;
            for(int cnt = 0; cnt < MatchObjects.Length; cnt ++)
            {
                Transform matchObjTrans = MatchObjects[cnt].transform;
                Vector3 pos = matchObjTrans.position;
                pos.y -= currentSpeed * Time.deltaTime;
                matchObjTrans.position = pos;
                lasty = pos.y;
            }

            if(lasty < -7 && !resetQd)
            {
                resetQd = true;
                StartCoroutine(ResetPlayBoard());
            }

        }
    }

    // REset the Matchable Locations and update their display text
    void ResetMatchables()
    {
        LineMover.GoFast(false);
        currentSpeed = NormalSpeed;
        MatchItem[] matchDataItems = new MatchItem[3];

        ItemData.GetRandomMatchables( matchDataItems, lastTopic);
        for (int cnt = 0; cnt < matchDataItems.Length; cnt++)
        {
            MatchItem item = matchDataItems[cnt];
            MatchObjects[cnt].SetMatchItemData(item);
            if (item.MatchThis == true)
            {
                playerLogic.SetMatchItemData(item);
                lastTopic = item.Topic;
            }    

            MatchObjects[cnt].transform.position = SpawnPoints[cnt].position;
        }
    }


    void HandleDropImmediate()
    {
        LineMover.GoFast(true);
        currentSpeed = HighSpeed;
    }

    void HandlePlayerCollide(GameObject collideWith)
    {
        
        MatchObject matchObject = collideWith.GetComponent<MatchObject>();
        MatchItem playerMatchData = playerLogic.MatchItemData;
        if(matchObject)
        {
            MatchItem matchData = matchObject.MatchItemData;
            // Match Success!
            if (playerMatchData.Topic == matchData.Topic)
            {
                answerManager.HandleCorrectAnswer(collideWith.transform.position);
                killDelay = 1.6f;
            }
            // Match Failure!
            else
            {
                answerManager.HandleWrongAnswer();
                killDelay = 0.4f;
            }    
        }

        StartCoroutine(ResetPlayBoard());
        
    }


    IEnumerator ResetPlayBoard()
    {
        


        yield return new WaitForSeconds(0.1f);

        if (answerManager.AllDone)
        {
            StartCoroutine(ShowRestart());
            shouldDrop = false;
        }
        else
        {
            ResetMatchables();
            yield return new WaitForSeconds(killDelay);

        }

        resetQd = false;


        


    }

    IEnumerator ShowRestart()
    {
        yield return new WaitForSeconds(0.25f);
        foreach (MatchObject mo in MatchObjects)
            Destroy(mo.gameObject);
        restartController.ShowScore(answerManager.CorrectCount, answerManager.WrongCount);

        fader.FadeInOut( () => 
            { 
                restartController.gameObject.SetActive(true);
                LineMover.gameObject.SetActive(false);
            },
                         () => {}
                   );


    }


}
