using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHandler : MonoBehaviour
{
    public static int score; //The score of the game
    public int round; //The round that the game is

    public static int correctid; //the index of the correct answer (0,1,2,3) in the board. Corresponds to a quarter
    string correctname; //The name of the correct answer

    public static bool waiting; //True-> we are waiting after an object click
    public static bool finished; //True-> the game is finished and waiting for the decision of the player whether to play again or quit category
    public static bool playing; //True-> When the game has started (finished state is also true here)

    GameObject canvashandler; //The object of the CanvasHandler
    GameObject audiohandler; //The object of the AudioHandler
    
   
    List<GameObject> objlist = new List<GameObject> { }; //The list of GameObjects. Containts the 10 possible 3D Objects (10 animals, 10 instruments, 10 monuments)
    public static List<GameObject> selectedlist = new List<GameObject> { }; //The list of selected GameObjects
    
    //Method is executed when the Scene is starting. Initialization
    void Start()
    {
        score = 0; //Initialize score and round
        round = 0;
        
        playing = false; //Not playing 
        waiting = false; //Not waiting 
        finished = false; //Not finished

        canvashandler = GameObject.Find("CanvasHandler"); //Find the 2 Objects in the Unity Hierarchy
        audiohandler = GameObject.Find("AudioHandler");

        audiohandler.SendMessage("playsound", 3); //Play the initial screen sound        
    }

    //Method is executed when a board is found and the game is not finished
    public void trackingfound(string board)
    {
        if (objlist.Count == 0) //If the object list is empty. It means that we shave scanned for the first time a board, so we need to fill in the objects list and start the game
        {
            audiohandler.SendMessage("stopsound"); //Stop the sound of initial screen
            
            //Find which board it is
            if (board == "AnimalsBoardTarget") //Animals Board
            {
                GameObject[] objarray = GameObject.FindGameObjectsWithTag("animal"); //Find all the objects with the Tag "animal"
                foreach (GameObject o in objarray)
                {
                    objlist.Add(o); //Add the object in the ObjectsList
                    o.transform.GetComponent<Renderer>().enabled = false; //Make then disappear, Renderer off
                    canvashandler.SendMessage("setcategory", 1); //Set the category to Animals
                }
            }
            else if (board == "InstrumentsTarget") //Instruments Board
            {
                GameObject[] objarray = GameObject.FindGameObjectsWithTag("instrument"); //Find all the objects with the Tag "instrument"
                foreach (GameObject o in objarray) 
                {
                    objlist.Add(o); //Add the object to the ObjectsList
                    o.transform.GetComponent<Renderer>().enabled = false; //Renderer off
                    canvashandler.SendMessage("setcategory", 2); //Set the category to Instruments
                }
            }
            else if (board == "MonumentsTarget") //Monuments Board
            {
                GameObject[] objarray = GameObject.FindGameObjectsWithTag("monument"); //Find all objects with tag monument
                foreach (GameObject o in objarray)
                {
                    objlist.Add(o); //Add the object to the ObjectsList
                    o.transform.GetComponent<Renderer>().enabled = false; //Renderer off
                    canvashandler.SendMessage("setcategory", 3); //Set the category to Monuments
                }
            }
        }
        
        if (playing == false) //If the game has not started yet or the user quited, set all renderers off
        {
            foreach (GameObject o in objlist)
            {         
                o.transform.GetComponent<Renderer>().enabled = false;
                foreach (Renderer r in o.GetComponentsInChildren<Renderer>()) r.enabled = false;

            }   
            newround(); //Start a new round (it is a new Game)
        }
        else //If the game has already started
        {
            foreach (GameObject o in objlist) //If the game has started, set all renderers to off
            {
                
                o.transform.GetComponent<Renderer>().enabled = false;
                foreach (Renderer r in o.GetComponentsInChildren<Renderer>()) r.enabled = false;

            }
            if (selectedlist.Count != 0) //If the game is playing and there is a round going on, make all active objects (the 4 objects that are selected to play) renderers on
            {
                for (int i = 0; i < 4; i++)
                {
                    selectedlist[i].transform.GetComponent<Renderer>().enabled = true;
                    foreach (Renderer r in selectedlist[i].GetComponentsInChildren<Renderer>()) r.enabled = true;
                }
            }

        }       
    }

    //Method is executed when there is a new round. It is triggered whether from the tracking of a image (for round one) or from the next round button
    public void newround()
    {
        round++; //Increment the number of round     
        if (round <= 10)
        {
            canvashandler.SendMessage("setround", "Round: " + round.ToString()); //Trigger the canvas to show the new round
            if (round == 1)
            {
                playing = true; //We set the game started
                //Enable or disable the buttons and texts we want when the game is starting by triggering canvas
                //On->ScoreText,CategoryText,Roundtext,AnswerText,RestartCategoryButton,PlaySoundButton,QuitCategoryButton
                //Off->StartText
                canvashandler.SendMessage("enablescoretext");
                canvashandler.SendMessage("enableroundtext");
                canvashandler.SendMessage("enablecategorytext");
                canvashandler.SendMessage("enableanswertext");
                canvashandler.SendMessage("disablestarttext");
                canvashandler.SendMessage("enablerestartcategorybutton");
                canvashandler.SendMessage("enablequitcategorybutton");
                canvashandler.SendMessage("enableplaysoundbutton");
                canvashandler.SendMessage("setscore", "Score: " + score.ToString()); //Trigger canvas to set the score

            }
            else //If the round is not 1
            {
                canvashandler.SendMessage("enableplaysoundbutton"); //Enable PlaySound button and disable NextRound button
                canvashandler.SendMessage("disablenextroundbutton");
            }
            showobjects(); //Execute the showobjects method
        }
    }

    //Method is executed when we want to play again the sound of the correct object
    public void playsound()
    {
        if (selectedlist.Count != 0) selectedlist[correctid].GetComponent<AudioSource>().Play();
    }

    //Method is executed when an object is clicked
    IEnumerator objectclicked(string clickedname)
    {
        selectedlist[correctid].GetComponent<AudioSource>().Stop(); //Stop the sound the of the correct object
        canvashandler.SendMessage("disablequitcategorybutton"); //Disable the buttons of quit category, restart category and play sound
        canvashandler.SendMessage("disablerestartcategorybutton");
        canvashandler.SendMessage("disableplaysoundbutton");
        if (clickedname==correctname) //If the object clicked is the correct 
        {
            audiohandler.SendMessage("playsound", 1); //Play the soundeffect "correct"
            canvashandler.SendMessage("setanswer", 1); //Show in cavnas that the answer is the correct
            score = score + 10; //Increment score by 10
            
        }
        else //If the object clicked is not the correct 
        {
            audiohandler.SendMessage("playsound", 2); //Play the soundeffect "wrong"
            canvashandler.SendMessage("setanswer", 2); //Show in canvas that the answer was wrong, and which was the correct answer
        }
        
        yield return new WaitForSeconds(1); //Wait for 1 Second
        canvashandler.SendMessage("setscore", "Score: " + score.ToString()); //Set the new score
        //Set renderers off for the 4 active (selected) objects and transform them to (0,0,0) 
        for (int i = 0; i < 4; i++)
        {
            selectedlist[i].transform.GetComponent<Renderer>().enabled = false;
            foreach (Renderer r in selectedlist[i].GetComponentsInChildren<Renderer>()) r.enabled = false;
            selectedlist[i].transform.localPosition = new Vector3((float)0f, (float)0, (float)0f);
        }
        selectedlist.Clear(); //Clear the list of active objects
        waiting = true; //enable waiting
        yield return new WaitForSeconds(2); //wait for 2 seconds

        //If the round is less than 10, the game should continue. Clear the answertext and enable Buttons of next round, quit category and restart category
        if (round < 10) {
            canvashandler.SendMessage("setanswer", 3);
            canvashandler.SendMessage("enablenextroundbutton");
            canvashandler.SendMessage("enablequitcategorybutton");
            canvashandler.SendMessage("enablerestartcategorybutton");
        }
        else //If the round is 10, it means and the game should finish
        {
            audiohandler.SendMessage("playsound", 4); //Play finish sound
            canvashandler.SendMessage("setanswer", 3); //Clear answer text
            canvashandler.SendMessage("setscore", "Final Score: " + score.ToString()); //Set the final socre
            canvashandler.SendMessage("setround", "Game Finished"); //Set the round text to "Game Finished"
            canvashandler.SendMessage("disableplaysoundbutton"); //Disable the buttons of playsound, nextround, restartcategory,quitcategory, answer text and enable quit and playagain button
            canvashandler.SendMessage("disablenextroundbutton");
            canvashandler.SendMessage("disableanswertext");
            canvashandler.SendMessage("disablerestartcategorybutton");
            canvashandler.SendMessage("disablequitcategorybutton");
            canvashandler.SendMessage("enablequitbutton");
            canvashandler.SendMessage("enableplayagainbutton");
            finished = true;
        }
        waiting = false; //Set waiting to false
    }

    //Method is executed when we want to show 4 new active (selected) objects
    public void showobjects()
    {
        int randIndex; // Random index of the List
        for (int i = 0; i < 4; i++)
        {
            randIndex = (int)Random.Range(0, 10 - i); //Range (0, NUMBER OF 3D OBJECTS - i) - Choose a random between [0,9]
            
            selectedlist.Add(objlist[randIndex]); //Add it to the active objects
            objlist[randIndex].SendMessage("transformObj", i); //Trigger the ObjectHandler to locate it correctly in a quarter of the board
            objlist.RemoveAt(randIndex); //Remove it from the objects list

        }

        for (int i = 0; i < 4; i++) //Add again the deleted objects in the ObjectsList
        {
            objlist.Add(selectedlist[i]);
        }

        correctid = (int)Random.Range(0,4); //Pick randomly one of the four selected objects as the correct
        selectedlist[correctid].GetComponent<AudioSource>().Play(); //Play the sound of the correct object
        correctname = selectedlist[correctid].name; //Set the name of the correct object
    }

    //Method is executed when we want to play again the same category or restart the category
    public void restart()
    {
        if (finished == true) //If the game is in the finished state (after round 10)
        {
            finished = false; //Set finished to false
            canvashandler.SendMessage("disableplayagainbutton"); //Disable button of play again and quit
            canvashandler.SendMessage("disablequitbutton");
        }
        if (selectedlist.Count != 0) //If the are active objects (the game is playing)
        {
            selectedlist[correctid].GetComponent<AudioSource>().Stop(); //Stop the sound of the correct object
            for (int i = 0; i < 4; i++)
            {
                selectedlist[i].transform.GetComponent<Renderer>().enabled = false; //Set renderers off for all active objects
                foreach (Renderer r in selectedlist[i].GetComponentsInChildren<Renderer>()) r.enabled = false;
                selectedlist[i].transform.localPosition = new Vector3((float)0f, (float)0, (float)0f); //Transform them to (0,0,0)
            }
            selectedlist.Clear(); //Clear the selected (active) list
        }
        canvashandler.SendMessage("disablenextroundbutton"); //Disable the next round button
        score = 0; //Set score and round to 0
        round = 0;
        newround(); //Start again a new round from 0
    }

    //Method is executed when we want to quit a category
    public void quit()
    {
        if (selectedlist.Count != 0) //If we are playing
        {
            selectedlist[correctid].GetComponent<AudioSource>().Stop(); //Stop the sound of the correct object
            for (int i = 0; i < 4; i++)
            {             
                selectedlist[i].transform.GetComponent<Renderer>().enabled = false; //Set renderers off for all active objects
                foreach (Renderer r in selectedlist[i].GetComponentsInChildren<Renderer>()) r.enabled = false; 
                selectedlist[i].transform.localPosition = new Vector3((float)0f, (float)0, (float)0f); //Transform them to (0,0,0)
            }
            selectedlist.Clear();
        }
        score = 0; //Set score and round to 0
        round = 0;
        //Disable all buttons and texts
        //Enable only the start text of the initial screen
        canvashandler.SendMessage("disablenextroundbutton");
        canvashandler.SendMessage("disablescoretext");
        canvashandler.SendMessage("disableroundtext");
        canvashandler.SendMessage("disablecategorytext");
        canvashandler.SendMessage("disablequitbutton");
        canvashandler.SendMessage("disablerestartcategorybutton");
        canvashandler.SendMessage("disablequitcategorybutton");
        canvashandler.SendMessage("disableplayagainbutton");
        canvashandler.SendMessage("disableplaysoundbutton");
        canvashandler.SendMessage("disableanswertext");
        canvashandler.SendMessage("enablestarttext");
        canvashandler.SendMessage("disableanswertext");
        playing = false; //Set playing to false
        if (finished == true)  finished = false; //if the game was in the finished state, set it off
        objlist.Clear(); //Clear all ObjectsList
        audiohandler.SendMessage("playsound", 3); //Play the sound of the initial screen
    }
}
