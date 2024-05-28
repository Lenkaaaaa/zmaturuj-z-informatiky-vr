using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SocketCheck : MonoBehaviour
{
    public GameObject[] requiredObjects; 
    public GameObject[] sockets; 


    private bool allObjectsPlaced = false;

    void Update()
    {
        if (!allObjectsPlaced)
        {
            CheckObjectPlacement();
        }
    }

    void CheckObjectPlacement()
    {
        foreach (GameObject obj in requiredObjects)
        {

            string objTag = obj.tag;

            foreach (GameObject socket in sockets)
            {
                string socketTag = socket.tag;
                if (objTag == socketTag)
                {
                    continue;
                }
                else
                {
                    Debug.LogError("zle umiestneny");
                    return;
                }
            }
        }
        
        allObjectsPlaced = true;
    }
}
