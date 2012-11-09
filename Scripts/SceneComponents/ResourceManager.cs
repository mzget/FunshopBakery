using UnityEngine;
using System.Collections;

public class ResourceManager : ScriptableObject {

    public const string GameEffect_ResourcePath = "GameEffects/";


	// Use this for initialization
    void OnEnable() {
        Debug.Log("Starting... Resource Manager");
    }
}
