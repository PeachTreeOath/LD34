using UnityEngine;
using System.Collections;

//Model data for resources
public class GResource : ScriptableObject {

	public Globals.GoodTypeEnum goodType;
    public int prodRate;
    public int prodCount;
    public StatBar statBar;
    public string resName;

    public void init(Globals.GoodTypeEnum type, int initProdCount, int initProdRate) {
        prodRate = initProdRate;
        prodCount = initProdCount;
        goodType = type;
        resName = goodType.ToString();
    }
}
