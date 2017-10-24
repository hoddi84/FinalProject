using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Testing : MonoBehaviour {

    // Add a menu item named "Do Something" to MyMenu in the menu bar.
    [MenuItem("MyMenu/Do Something")]
    static void DoSomething()
    {
        Debug.Log("Doing Something...");
    }
}
