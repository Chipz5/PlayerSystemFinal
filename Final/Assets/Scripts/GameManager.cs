using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{
    public Text guiText;
    List<IObserver> observers;
    int numberOfObservers;
    public Player player;
    public Achievement achievement;
    
    int enemyNo = 1;
    // Start is called before the first frame update
    void Start()
    {
        addObserver(player);
        addObserver(achievement);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Awake()
    {
        observers = new List<IObserver>();
        numberOfObservers = 0;
    }

    public void newAbility()
    {
        enemyNo++;
        notify(enemyNo);
        

        if(enemyNo == 2)
        {
            StartCoroutine(ShowMessage("New Attack unlocked: Kick (Press E)", 4));
        }
        if(enemyNo == 3)
        {
            StartCoroutine(ShowMessage("New Attack unlocked: Combo (Press Q,E,R)", 4));
        }
        
    }
    
    IEnumerator ShowMessage(string message, float delay)
    {
        guiText.text = message;
        guiText.enabled = true;
        yield return new WaitForSeconds(delay);
        guiText.enabled = false;
    }

    void addObserver(IObserver observer)
    {
        observers.Add(observer);
        numberOfObservers++;
    }

    void removeObserver(IObserver observer)
    {
        observers.Remove(observer);
        numberOfObservers--;
    }

    public void notify(int checkpointNo)
    {
        for (int i = 0; i < numberOfObservers; i++)
        {
            observers[i].OnNotify(checkpointNo);
        }
    }

    ~GameManager()
    {
        for (int i = 0; i < observers.Count; ++i)
        {
            removeObserver(observers[i]);
        }
        observers.Clear();
    }

    internal void doneTutorial()
    {
        SceneManager.LoadScene("Game1");
    }
}
