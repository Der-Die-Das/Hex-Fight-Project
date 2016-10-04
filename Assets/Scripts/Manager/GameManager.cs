using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

    private static GameManager instance;

    private GameManager()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    void Awake()
    {
        
    }


	// Use this for initialization
	void Start () {
        DontDestroyOnLoad(instance);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
