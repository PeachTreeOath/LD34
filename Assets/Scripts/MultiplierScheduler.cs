using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MultiplierScheduler : MonoBehaviour {

	public int maxMults;

	public float minSpawnTime;
	public float maxSpawnTime;

	public float minLifeTime;
	public float maxLifeTime;

	public float minDeathTime;
	public float maxDeathTime;

	GameObject [] multObjs;
	List<float> spawnTimers;
	List<float> spawnTimes;
	List<float> lifeTimers;
	List<float> lifeTimes;
	List<float> deathTimers;
	List<float> deathTimes;
	List<bool> spawned;
	List<bool> dying;
	List<int> indices;

	int spawnCount;

	float [] multChance = {1.5f, 1.5f, 1.5f, 1.5f, 1.5f, 1.5f, 2, 2, 2, 3 };

	// Use this for initialization
	void Start () {
		spawnCount = 0;
		dying = new List<bool>();
		indices = new List<int>();
		spawnTimers = new List<float>();
		spawnTimes = new List<float>();
		deathTimers = new List<float>();
		deathTimes = new List<float>();
		lifeTimers = new List<float>();
		lifeTimes = new List<float>();
		spawned = new List<bool>();

		multObjs = GameObject.FindGameObjectsWithTag("Multiplier");
		for(int i = 0; i < multObjs.Length; i++)
		{
			multObjs[i].GetComponent<Renderer>().enabled = false;
			multObjs[i].GetComponent<Multiplier>().multiplier = 0;
			spawnTimers.Add(Time.time);
			spawnTimes.Add(getSpawnTime());
			deathTimers.Add(0);
			deathTimes.Add(0);
			lifeTimers.Add(0);
			lifeTimes.Add(0);
			spawned.Add(false);
			indices.Add(i);
			dying.Add(false);
		}
	}
	
	// Update is called once per frame
	void Update () {
		indices.Shuffle();
		for(int i = 0; i < spawned.Count; i++)
		{
			if(spawnCount < maxMults)
			{
				if(!spawned[indices[i]] &&
					Time.time - spawnTimers[indices[i]] > spawnTimes[indices[i]])
				{
					spawned[indices[i]] = true;
					spawnCount++;

					multObjs[indices[i]].GetComponent<Renderer>().enabled = true;
					multObjs[indices[i]].GetComponent<Multiplier>().multiplier = multChance[Random.Range(0, multChance.Length)];
					multObjs[indices[i]].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Textures/mult"+((int)multObjs[indices[i]].GetComponent<Multiplier>().multiplier));

					lifeTimers[indices[i]] = Time.time;
					lifeTimes[indices[i]] = getLifeTime();
					dying[indices[i]] = false;

					for(int j = 0; j < spawned.Count; j++)
					{
						if(!spawned[j] && j != indices[i])
						{
							if(Random.Range(0, 100) > 50)
							{
								spawnTimers[j] = Time.time;
								spawnTimes[j] = getSpawnTime();
							}
						}
					}

					Debug.Log(Time.time + " spawned multiplier");
				}
			}

			if(spawned[i])
			{
				if(!dying[i] &&
					Time.time - lifeTimers[i] > lifeTimes[i])
				{
					dying[i] = true;
					deathTimers[i] = Time.time;
					deathTimes[i] = getDeathTime();
					multObjs[i].AddComponent<Blinking>().blinkDelay = .3f;
					Debug.Log(Time.time + " killing multiplier");
				}
				if(dying[i] &&
					Time.time - deathTimers[i] > deathTimes[i])
				{
					spawned[i] = false;
					spawnCount--;
					DestroyImmediate(multObjs[i].GetComponent<Blinking>());
					multObjs[i].GetComponent<Renderer>().enabled = false;
					multObjs[i].GetComponent<Multiplier>().multiplier = 0;
					dying[indices[i]] = false;
					spawnTimers[i] = Time.time;
					spawnTimes[i] = getSpawnTime();
					Debug.Log(Time.time + " killed multiplier");
				}
			}
		}
	}

	float getSpawnTime()
	{
		return Random.Range(minSpawnTime, maxSpawnTime);
	}

	float getLifeTime()
	{
		return Random.Range(minLifeTime, maxLifeTime);
	}

	float getDeathTime()
	{
		return Random.Range(minDeathTime, maxDeathTime);
	}
}
