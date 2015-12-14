using UnityEngine;
using System.Collections;

public class TargetCityScript : MonoBehaviour
{

    public Globals.GoodTypeEnum desiredGood;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.name == "CaravanPrefab(Clone)")
        {

            if (desiredGood == col.gameObject.GetComponent<CaravanState>().goodType)
            {
                Globals.gameState.money += col.gameObject.GetComponent<CaravanState>().value;
                //GetComponent<CityGrowth> ().LevelUp (false);

                GameObject camGameObject = Camera.main.gameObject;
                EconomySimulator econSim = camGameObject.GetComponent<EconomySimulator>();
                econSim.UpdateDesiredGood(gameObject);
            }

            Destroy(col.gameObject);
        }
    }
}
