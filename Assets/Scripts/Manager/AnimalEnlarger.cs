using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class AnimalEnlarger : Singleton<AnimalEnlarger> {

    public Phylum[] phyla;
    public GameObject[] animals;
    private string[] enlargePhyla = { "Annelida","asd" };

    private void Init()
    {
        //phyla = new Phylum[2];
    }

    public void EnlargeAnimal(GameObject[] animal)
    {
        // Modify animal scale
        for (int i = 0; i < animals.Length; i++)
        {
            if (ShouldEnlarge(animals[i]))
            {
                animals[i].transform.localScale += new Vector3(1.5f, 1.5f, 1.5f);
            }
        }
    }

    private bool ShouldEnlarge(GameObject animal)
    {
        for(int i =0; i < phyla.Length; i++)
        {
            if (enlargePhyla.Contains(phyla[i].gameObject.name))
            {
                if (phyla[i].animals.Contains(animal.gameObject))
                {
                    return true;
                }
            }
        }
        return false;
    }
}
