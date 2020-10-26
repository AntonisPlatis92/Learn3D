using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasHandler : MonoBehaviour
{
    //The objects that we need in the canvas
    GameObject gamehandler;
    GameObject nextroundbutton;
    GameObject playsoundbutton;
    GameObject roundtext;
    GameObject scoretext;
    GameObject categorytext;
    GameObject answertext;
    GameObject starttext;
    GameObject quitcategorybutton;
    GameObject restartcategorybutton;
    GameObject playagainbutton;
    GameObject quitbutton;

    //Method is executed when the app is starting
    void Start()
    {
        //First, locate all the objects in the hierarchy of Unity
        gamehandler = GameObject.Find("GameHandler");
        nextroundbutton = GameObject.Find("NextRoundButton");
        playsoundbutton = GameObject.Find("PlaySoundButton");
        roundtext = GameObject.Find("RoundText");
        scoretext = GameObject.Find("ScoreText");
        answertext = GameObject.Find("AnswerText");
        starttext = GameObject.Find("StartText");
        quitbutton = GameObject.Find("QuitButton");
        playagainbutton = GameObject.Find("PlayAgainButton");
        quitcategorybutton = GameObject.Find("QuitCategoryButton");
        restartcategorybutton = GameObject.Find("RestartCategoryButton");
        categorytext = GameObject.Find("CategoryText");
        scoretext.GetComponent<Text>().text = ""; //Set blank text to Score
        nextroundbutton.SetActive(false); //Disable all the buttons
        playsoundbutton.SetActive(false);
        quitbutton.SetActive(false);
        playagainbutton.SetActive(false);
        quitcategorybutton.SetActive(false);
        restartcategorybutton.SetActive(false);
        disablequitbutton();
    }

    //Method to put a text in the answer text
    public void setanswer(int i) //1 = correct , 2 = wrong , 3 = blank
    {
        if (i == 1) {
            answertext.GetComponent<Text>().text = "Correct! +10 " + "\n\rIt was the " + GameHandler.selectedlist[GameHandler.correctid].name;
            answertext.GetComponent<Text>().color = Color.green; //Set text color to green
        }
        else if (i == 2){
            answertext.GetComponent<Text>().text = "Wrong! :(" + "\n\rIt was the " + GameHandler.selectedlist[GameHandler.correctid].name; 
            answertext.GetComponent<Text>().color = Color.red; //Set text color to red
        }
        else if (i == 3)
        {
            answertext.GetComponent<Text>().text = "";
        }
        
    }

    //Method to put a text to score text
    public void setscore(string score)
    {
        scoretext.GetComponent<Text>().text = score; //Set the text
        //If the score is final, mention the number of correct answers
        if (score.Contains("Final"))
        {
            scoretext.GetComponent<Text>().text = score + "\r\nYou got " + (GameHandler.score / 10) + "/10 Correct!";
        }
    }

    //Method to put a text in round text
    public void setround(string round)
    {
        roundtext.GetComponent<Text>().text = round;
    }

    //Method to put a text the category text
    public void setcategory (int i)
    {
        if (i == 1)
        {
            categorytext.GetComponent<Text>().text = "Category: Animals"; //Animal Category
        }
        if (i == 2)
        {
            categorytext.GetComponent<Text>().text = "Category: Instruments"; //Instruments Category
        }
        if (i == 3)
        {
            categorytext.GetComponent<Text>().text = "Category: Monuments"; //Monuments Category
        }
    }

    //Method to enable Next Round Button
    public void enablenextroundbutton()
    {
        nextroundbutton.SetActive(true);
    }

    //Method to disable Next Round Button
    public void disablenextroundbutton()
    {
        nextroundbutton.SetActive(false);
    }

    //Method to enable Play Sound Button
    public void enableplaysoundbutton()
    {
        playsoundbutton.SetActive(true);
    }

    //Method to disable Play Sound Button
    public void disableplaysoundbutton()
    {
        playsoundbutton.SetActive(false);
    }

    //Method to enable Start Text
    public void enablestarttext()
    {
        starttext.SetActive(true);
    }

    //Method to disable Start Text
    public void disablestarttext()
    {
        starttext.SetActive(false);
    }

    //Method to enable Play Again Button
    public void enableplayagainbutton()
    {
        playagainbutton.SetActive(true);
    }

    //Method to disable Play Again Button
    public void disableplayagainbutton()
    {
        playagainbutton.SetActive(false);
    }

    //Method to enable Quit Button
    public void enablequitbutton()
    {
        quitbutton.SetActive(true);
    }

    //Method to disable Quit Button
    public void disablequitbutton()
    {
        quitbutton.SetActive(false);
    }

    //Method to enable Quit Category Button
    public void enablequitcategorybutton()
    {
        quitcategorybutton.SetActive(true);
    }

    //Method to disable Quit Category Button
    public void disablequitcategorybutton()
    {
        quitcategorybutton.SetActive(false);
    }

    //Method to enable Restart Category Button
    public void enablerestartcategorybutton()
    {
        restartcategorybutton.SetActive(true);
    }

    //Method to disable Restart Category Button
    public void disablerestartcategorybutton()
    {
        restartcategorybutton.SetActive(false);
    }

    //Method to enable Score Text
    public void enablescoretext()
    {
        scoretext.SetActive(true);
    }

    //Method to disable Score Text
    public void disablescoretext()
    {
        scoretext.SetActive(false);
    }

    //Method to enable Round Text
    public void enableroundtext()
    {
        roundtext.SetActive(true);
    }

    //Method to disable Round Text
    public void disableroundtext()
    {
        roundtext.SetActive(false);
    }

    //Method to enable Answer Text
    public void enableanswertext()
    {
        answertext.SetActive(true);
    }

    //Method to disable Answer Text
    public void disableanswertext()
    {
        answertext.SetActive(false);
    }

    //Method to enable Category Text
    public void enablecategorytext()
    {
        categorytext.SetActive(true);
    }

    //Method to disable Category Text
    public void disablecategorytext()
    {
        categorytext.SetActive(false);
    }

    //Method that is executed when the Play Sound button is pressed. It triggers the Play Sound method of GameHandler
    public void playsound()
    {
        gamehandler.SendMessage("playsound");
    }

    //Method that is executed when the Next Round button is pressed. It enables the play sound button and triggers the new round method of GameHandler
    public void nextround()
    {
        enableplaysoundbutton();
        gamehandler.SendMessage("newround");
    }

    //Method that is executed when Restart Category Button and Play Again Button are pressed. It triggers the restart method of GameHandler
    public void restart()
    {
        if (DefaultTrackableEventHandler.tracking == true) gamehandler.SendMessage("restart");
        
    }

    //Method that is executed when Quit Button and Quit Category Button are pressed. It triggers the quit method of GameHandler
    public void quit()
    {
        gamehandler.SendMessage("quit");
    }

    
}
