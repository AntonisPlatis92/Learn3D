using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectHandler : MonoBehaviour
{
    GameObject gamehandler; //Object that we will need - only gamehandler

    //Method executed when the app is starting
    void Start()
    {
        gamehandler = GameObject.Find("GameHandler"); //Find the object in the Hierarchy
    }

    //Method executed when a collider of object is clicked
    public void OnMouseDown()
    {
        if (this.GetComponent<Renderer>().isVisible) gamehandler.SendMessage("objectclicked", this.name); //When a Collider is clicked, execute the objectclicked of gamehandler      
    }

    //Method to transform the 4 3D objects in the board
    public void transformObj(int quarter)
    {
        switch (quarter) //One by one they will be located in 1 of the 4 quarters of the board
        {
            
            case (0):
                {
                    this.transform.localPosition = new Vector3((float)0.292f, 0, (float)-0.218f); //Locate the 1st and enable the Renderer in order to appear
                    this.transform.GetComponent<Renderer>().enabled = true;
                    foreach (Renderer r in this.GetComponentsInChildren<Renderer>()) r.enabled = true;
                    break;
                }
            case (1):
                {
                    this.transform.localPosition = new Vector3((float)0.284f, (float) 0, (float)0.194f); //Locate the 2nd and enable the Renderer in order to appear
                    this.transform.GetComponent<Renderer>().enabled = true;
                    foreach (Renderer r in this.GetComponentsInChildren<Renderer>()) r.enabled = true;
                    break;
                }
            case (2):
                {
                    this.transform.localPosition = new Vector3((float)-0.261f, (float)0, (float)0.194f); //Locate the 3rd and enable the Renderer in order to appear
                    this.transform.GetComponent<Renderer>().enabled = true;
                    foreach (Renderer r in this.GetComponentsInChildren<Renderer>()) r.enabled = true;
                    break;
                }
            case (3):
                {
                    this.transform.localPosition = new Vector3((float)-0.261f, (float)0, (float)-0.218f); //Locate the 4th and enable the Renderer in order to appear
                    this.transform.GetComponent<Renderer>().enabled = true;
                    foreach (Renderer r in this.GetComponentsInChildren<Renderer>()) r.enabled = true;
                    break;
                }
            default: break;
        }
        
        

    }
    
}
