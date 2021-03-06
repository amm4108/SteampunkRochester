﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class TestManager : MonoBehaviour {
	
	TwineImporter Twine;
	bool wasClicked;
	List<Interactable> interactables;
	//EmotionManager eM;
	TimeManager tM;
	//Inventory inv;
	public static string currentLevel;
	static string currLvl;
	public static bool finalLoaded = false;
	public GUISkin normal;
	bool disabledCanvas = false;
	// Use this for initialization
	void Start () {
		/*GameObject testInteractable = new GameObject("BlackBox");
		testInteractable.AddComponent<SpriteRenderer> ();
		test = testInteractable.AddComponent<Interactable> ();
		test.SetName ("test", "test");*/
		Twine = GameObject.Find ("TwineImporter").GetComponent<TwineImporter> ();
		currentLevel = transform.parent.gameObject.name;
		//eM = new EmotionManager();
		tM = GameObject.Find("SystemManager").GetComponent<TimeManager>();
		//inv = new Inventory();
		//find interactables
		interactables = new List<Interactable>();
		var gs = GameObject.FindObjectsOfType<GameObject>();
		foreach(GameObject obj in gs)
		{
			var script = obj.GetComponent<Interactable>();
			if(script != null)
			{
				interactables.Add(script);
			}
		}
	}

	void OnGUI()
	{
		Rect r = new Rect(0,0,Screen.width/7.5f, Screen.height/8);
		r.x += r.width;
		r.y += r.height;
		GUI.skin = normal;
		if(!finalLoaded)
		{
			if(Interactable.KEYMASTER == null)
			{
				r = new Rect(Screen.width/1.5f,0,Screen.width/7.5f, Screen.height/8);
				if(GUI.Button(r, "Leave"))
				{
					if(GameObject.Find("DialogueScreen(Clone)"))
					{
						Object.Destroy(GameObject.Find("DialogueScreen(Clone)"));
					}
					//Debug.Log(currentLevel);
					if(currentLevel == "Party_Night")
					{
						currLvl = "";
						//Destory current level
						Object.Destroy(transform.parent.gameObject);
						
						//load editor office
						Instantiate(Resources.Load("Prefabs/locations/Editor_Office_Day") as GameObject);
				
					}
					//otherwise, go back to map screen.
					else{
						Object.Destroy(transform.parent.gameObject);
						MarkerBhv.m_map.SetActive(true);
						MarkerBhv.mapUI.SetActive(true);
						currentLevel = "Map";
					}
				}
			}
			else{
				r.x = Screen.width/2 - r.width/2;
				r.y = r.height/3;
				if(GUI.Button(r,"Leave conversation"))
				{
					GameObject.Find("DialogueScreen(Clone)").GetComponent<DialogueComponent>().OnDialogueCompelete();
				}
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		if(tM.getDay() >= 2 && !finalLoaded)
		{
			Object.Destroy(transform.parent.gameObject);
			Instantiate(Resources.Load("Prefabs/locations/Editor_Office_Final")as GameObject);
			finalLoaded = true;
		}
		if(finalLoaded && !disabledCanvas)
		{
			GameObject.Find("Canvas").SetActive(false);
			disabledCanvas = true;
		}
		//Debug.Log (test.selected);
		/*foreach(Interactable test in interactables)
		{
			if (test.selected) 
			{
				if(!wasClicked)
				{
					if(Input.GetMouseButtonUp(0))
					{
						wasClicked = true;
					}
				}
				else if(Input.GetMouseButtonDown(0)){
					//Debug.Log ("you clicked the left mouse button");

					test.Progress();
				}
			}
		}*/
	}

	/*void OnMouseDown(){
		Debug.Log (test.selected);
		if(test.selected)
		{
			test.Progress();
		}
	}*/
}
