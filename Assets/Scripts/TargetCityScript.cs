using UnityEngine;
using System.Collections;

public class TargetCityScript : MonoBehaviour
{

    public Globals.GoodTypeEnum desiredGood;
    GameObject moneyFab;
    AudioClip cashClip;
    AudioSource finisherAudio;

    public int crashCountMin = 1;
    public int crashCountMax = 8;

    int crashCount;
    int curCrashCount;

    // Use this for initialization
    void Start()
    {
        curCrashCount = 0;
        crashCount = Random.Range(crashCountMin, crashCountMax);

        moneyFab = Resources.Load("Prefabs/MoneySign") as GameObject;
        cashClip = Resources.Load("Audio/cash") as AudioClip;

        finisherAudio = gameObject.AddComponent<AudioSource>();
        finisherAudio.rolloffMode = AudioRolloffMode.Linear;
        finisherAudio.spread = 360;
        finisherAudio.loop = false;
        finisherAudio.clip = cashClip;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.name == "CaravanPrefab(Clone)")
        {
            //Debug.Log(Time.time + " caravan arrived " + col.gameObject.GetComponent<CaravanState>().goodType + " " + desiredGood);
            if (desiredGood == col.gameObject.GetComponent<CaravanState>().goodType)
            {
                Globals.gameState.money += col.gameObject.GetComponent<CaravanState>().value;
                //GetComponent<CityGrowth> ().LevelUp (false);
                GameObject money = Instantiate(moneyFab);
                money.transform.position = gameObject.transform.position;
                money.AddComponent<TimedObject>().lifeTime = 2.5f;
                money.AddComponent<Rise>().moveSpeed = 2;

                Utility.PlaySFX(finisherAudio, 1, false);
                curCrashCount++;
                if (curCrashCount >= crashCount)
                {
                    curCrashCount = 0;
                    crashCount = Random.Range(crashCountMin, crashCountMax);

                    GameObject camGameObject = Camera.main.gameObject;
                    EconomySimulator econSim = camGameObject.GetComponent<EconomySimulator>();
                    econSim.UpdateDesiredGood(gameObject);
                }
            }

            Destroy(col.gameObject);
        }
    }
}

