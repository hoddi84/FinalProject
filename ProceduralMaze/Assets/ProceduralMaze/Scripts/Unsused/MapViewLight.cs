using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/*
 * Gives the ability of having the mapView with
 * more lighting, and here we do not render the
 * additional lighting to the player's camera. 
 * 
 * Attach this script to cameras that should not
 * display the MapViewLight.
 */
public class MapViewLight : MonoBehaviour
{

    public Light light;

    void Start()
    {
        light.enabled = true;
    }

    void OnPreCull()
    {
        light.enabled = false;
    }

    void OnPostRender()
    {
        light.enabled = true;
    }
}