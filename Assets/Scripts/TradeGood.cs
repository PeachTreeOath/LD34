using UnityEngine;
using System.Collections;

public class TradeGood : MonoBehaviour {

	public Globals.GoodTypeEnum goodType;
    public GameObject spawner { get; set; }

	void Start()
	{
		string iconName = goodType.ToString().ToLower();
		iconName = iconName.Substring(0, 1).ToUpper() + iconName.Substring(1);
		gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Textures/Trade Goods/"+iconName);
	}

	public void OnDestroy()
    {
        if (spawner != null)
            spawner.SendMessage("onGoodConsumed");
    }
}
