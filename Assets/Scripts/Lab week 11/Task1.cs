using UnityEngine;
using System.Collections.Generic; //for queues and IEnumerator
using System.Collections;
using System.Linq; //for linq

public class Task1: MonoBehaviour
{
    public string[] firstNames = new string[] {
        "James", "Mary", "Robert", "Patricia", "John", "Jennifer", 
        "Michael", "Linda", "David", "Elizabeth", "William", "Barbara", 
        "Richard", "Susan", "Joseph", "Jessica", "Thomas", "Sarah", 
        "Christopher", "Karen", "Charles", "Nancy", "Christopher", 
        "Lisa", "Daniel", "Betty", "Matthew", "Margaret", "Anthony", "Sandra"
    };
    public string[] lastIntials = new string[]
    {
        "A", "B", "C", "D", "E", "F", "G", "H", "I", 
        "J", "K", "L", "M", "N", "O", "P", "Q", "R", 
        "S", "T", "U", "V", "W", "X", "Y", "Z"
    };

    public Queue<string> loginQueue = new Queue<string>();

    public string GetRandomPlayerName(string[] names, string[] intials)
    {
        string randomName = names[getRandom(names)];
        string randomIntial = intials[getRandom(intials)];
        string fullname = randomName + " " + randomIntial;
        return fullname;
    }

    public int getRandom(string[] target)
    {
        return Random.Range(0, target.Length);
    }

    public IEnumerator QueueLeave() //user leaves queue
    {
        while(true)
        {
            if(loginQueue.Count != 0 )
            {
                float randomInterval = Random.Range(1, 10);
                yield return new WaitForSeconds(randomInterval);
                string leaver = loginQueue.Dequeue();
                Debug.Log($"{leaver} is now inside the game.");

            }
        }
    }

    public IEnumerator QueueEnter() //user enters queue
    {
        while(true)
        {
            float randomInterval = Random.Range(1, 10);
            yield return new WaitForSeconds(randomInterval); //fires at a random time between 1 and 10
            loginQueue.Enqueue(GetRandomPlayerName(firstNames,lastIntials));
            Debug.Log($"{loginQueue.Peek()} is trying to login and added to the login queue.");
        }
    }

    public IEnumerator QueueEmpty()
    {
        while(true)        
        {
            if(loginQueue.Count == 0)
            {
                Debug.Log("Login server is idle. No players are waiting.");
                yield return new WaitForSeconds(1f); //fires every second
            }
            else
            {
                yield return new WaitForSeconds(1f);
            }
        }
    }

    void Start()
    {
        int intialWaiters = Random.Range(4,7); //establish amount of waiters, last number is EXCLUSIVE
        for(int i = 0; intialWaiters > i; i++) //create intial queue
        {
            loginQueue.Enqueue(GetRandomPlayerName(firstNames,lastIntials));
        }

        List<string> intialList = loginQueue.ToList(); //convert queue to list
        Debug.Log($"Initial login queue created. There are {intialList.Count} players in the queue:{string.Join(", ", intialList)}"); //prints every item in the list seperated by whats in quotes

        StartCoroutine(QueueEnter());
        StartCoroutine(QueueLeave());
        StartCoroutine(QueueEmpty());
    }
}
