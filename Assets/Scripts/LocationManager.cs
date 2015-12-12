using UnityEngine;
using System.Collections;

//Determines valid locations to put things
public class LocationManager : MonoBehaviour {

    public Vector3 getPersonSpawnLoc() {
        //TODO make this fancy
        return new Vector3(Random.Range(-5, 5), Random.Range(-5, 5), 0);
    }
}