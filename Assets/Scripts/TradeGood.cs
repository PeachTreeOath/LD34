using UnityEngine;
using System.Collections;

public class TradeGood : MonoBehaviour {

	public Globals.GoodTypeEnum goodType;
    public GameObject spawner { get; set; }

	Vector3 offset;

	void Start()
	{
		offset = gameObject.transform.position - Globals.playerCity.transform.position;
		string iconName = goodType.ToString().ToLower();
		iconName = iconName.Substring(0, 1).ToUpper() + iconName.Substring(1);
		gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Textures/Trade Goods/"+iconName);
	}

	void Update()
	{
		gameObject.transform.position = Globals.playerCity.transform.position + offset;
	}

	public void OnDestroy()
    {
        if (spawner != null)
            spawner.SendMessage("onGoodConsumed");
    }
}
