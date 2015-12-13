using UnityEngine;
using System.Collections;

public class TradeGood : MonoBehaviour {

	public Globals.GoodTypeEnum goodType;
    public GameObject spawner { get; set; }

	public void OnDestroy()
    {
        if (spawner != null)
            spawner.SendMessage("onGoodConsumed");
    }
}
