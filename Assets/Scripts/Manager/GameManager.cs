using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

    private static GameManager instance;

    private GameManager()
    {
        if(instance == null)
        {
            instance = new GameManager();
        }
    }

    void Awake()
    {
        instance = new GameManager();
        DontDestroyOnLoad(instance);
    }


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
