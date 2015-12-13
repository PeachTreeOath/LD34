using UnityEngine;
using System.Collections;

public class BouncyScaler : MonoBehaviour {
	
	public float targetScale;
	public float scaleSpeed;
	public float epsilon;
	
	Vector3 startScale;
	Vector3 destScale;
	
	Vector3[] scales;
	
	int scaleDir;
	int tarIdx;
	
	Vector3 one;
	float totalTime;
	
	// Use this for initialization
	void Start () {
		startScale = transform.localScale;
		destScale = startScale * targetScale;
		one = startScale.normalized;
		scaleDir = 1;
		scales = new Vector3[2];
		scales[0] = startScale;
		scales[1] = destScale;
		tarIdx = 1;
		totalTime = 0;
	}
	
	// Update is called once per frame
	void Update () {		
		if( Mathf.Abs(Vector3.Distance(transform.localScale, scales[tarIdx])) > epsilon)
		{
			totalTime += Time.deltaTime;
			transform.localScale = Vector3.Lerp(scales[tarIdx^1], scales[tarIdx], totalTime * scaleSpeed);
		}else
		{
			if(tarIdx == 0)
			{
				//Destroy(this);
			}
			transform.localScale = scales[tarIdx];
			scaleDir = -scaleDir;
			tarIdx = tarIdx ^ 1;
			totalTime = 0;
		}
	}
}
