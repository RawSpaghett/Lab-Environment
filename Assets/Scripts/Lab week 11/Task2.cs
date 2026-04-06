using UnityEngine;
using System.Collections.Generic; //for queues and IEnumerator
using System.Linq; //for linq

public class Task2: MonoBehaviour
{
    public List<string> testList = new List<string>();

    public string[] names = new string[] {
        "James", "Mary", "Robert", "Patricia", "John", "Jennifer", 
        "Michael", "Linda", "David", "Elizabeth", "William", "Barbara", 
        "Richard", "Susan", "Joseph", "Jessica", "Thomas", "Sarah", 
        "Christopher", "Karen", "Charles", "Nancy", "Christopher", 
        "Lisa", "Daniel", "Betty", "Matthew", "Margaret", "Anthony", "Sandra"
    };

    public string GetRandomName(string[] names)
    {
        string randomName = names[getRandom(names)];
        return randomName;
    }

    public int getRandom(string[] target)
    {
        return Random.Range(0, target.Length);
    }

    public void DuplicateHandler(List<string> lists)
    {
        HashSet<string> seen = new HashSet<string>();
        HashSet<string> duplicates = new HashSet<string>();

        foreach(string name in lists)
        {
            if(!seen.Add(name))
            {
                duplicates.Add(name);
            }
        }

        if(duplicates.Count == 0)
        {
            Debug.Log("The array has no duplicate names.");
        }
        else
        {
            {
               string duplicateString = string.Join(", ", duplicates);
               Debug.Log($"The array has duplicate names:{duplicateString}");
            }
        }
    }


    void Start()
    {
        for(int i = 0; 15 > i; i++) 
        {
            testList.Add(GetRandomName(names));
        }
        DuplicateHandler(testList);
    }
}
