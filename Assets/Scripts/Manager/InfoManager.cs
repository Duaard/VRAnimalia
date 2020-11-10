using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using VRStandardAssets.Common;
using VRStandardAssets.Utils;

// This script handles all functions with regards
// to the info mode. It will handle showing of
// info and other core functions like spawning.
public class InfoManager : Singleton<InfoManager>
{
    [SerializeField] private Text textName;                         // This is the UI used to show the name of animal
    [SerializeField] private Text textInfo;                         // This is the UI used to show info about the animal
    [SerializeField] private RawImage img;                          // This is the UI used to show the image of the animal
    [SerializeField] private Transform model;                       // This is the UI uses to show the model of the animal
    [SerializeField] private SelectionRadial m_SelectionRadial;     // Used to continue past the outro.
    [SerializeField] private SelectionSlider m_SelectionSlider;     // Used to confirm the user has understood the intro UI.
    [SerializeField] private Reticle m_Reticle;                     // This is turned on and off when it is required and not.
    [SerializeField] private bool isStaticAnimals = false;

    // Initiate spawning and showing of intro
    private IEnumerator Start()
    {
        
        Phylum phylum = SpawnManager.Instance.phyla[0];             // Get the phylum
        AnimalInfo.LoadAnimalInfo(phylum,isStaticAnimals);                          // Loads animal info for phylum
        
        yield return ShowIntro();                                   // Wait for intro to show and hide
        SpawnManager.Instance.SpawnInfoAnimals();                   // Spawn animals for info
    }

    private IEnumerator ShowIntro()
    {
        // Wait for the intro UI to fade in.
        yield return StartCoroutine(UIController.Instance.ShowIntroUI());

        // Show the reticle (since there is now a selection slider) and hide the radial.
        Phylum phylum = SpawnManager.Instance.phyla[0];             // Get the phylum
        AnimalInfo.LoadAnimalInfo(phylum, isStaticAnimals);                          // Loads animal info for phylum
        m_Reticle.Show();
        m_SelectionRadial.Hide();

        // Wait for the selection slider to finish filling.
        yield return StartCoroutine(m_SelectionSlider.WaitForBarToFill());

        // Wait for the intro UI to fade out.
        yield return StartCoroutine(UIController.Instance.HideIntroUI());
    }

    public IEnumerator ShowInfo(string name, string info, Texture image, GameObject animal)
    {
        // Set the Info UI to given params
        textName.text = name;
        textInfo.text = info;
        img.texture = image;

        // Show the model above the info
        GameObject replica = ShowModel(animal);

        // Wait for the info UI to fade in.
        yield return StartCoroutine(UIController.Instance.ShowInfoUI());

        // Show the reticle (since there is now a selection slider) and hide the radial.
        m_Reticle.Show();
        m_SelectionRadial.Hide();

        // Wait for the selection slider to finish filling.
        yield return StartCoroutine(m_SelectionSlider.WaitForBarToFill());

        // Delete animal model
        Destroy(replica);

        // Turn off audio
        if (SoundManager.Instance.audio.isPlaying)
        {
            SoundManager.Instance.StopAudio();
        }
        
        // Wait for the intro UI to fade out.
        yield return StartCoroutine(UIController.Instance.HideInfoUI());
    }

    private GameObject ShowModel(GameObject animal)
    {
        // Check and destroy existing model
        if (model.childCount > 0) {
            Destroy(model.GetChild(0).gameObject);
        }

        // Create model and remove unneeded components
        GameObject replica = Instantiate(animal, model.transform);
        Destroy(replica.GetComponent<Animal>());
        Destroy(replica.GetComponent<Gaze>());
        Destroy(replica.GetComponent<Move>());
        Destroy(replica.GetComponent<Rigidbody>());
        replica.transform.localScale = new Vector3(15, 15, 15);
        replica.transform.SetParent(model.transform);
        replica.transform.localPosition = new Vector3(0, 0, 0);

        return replica;
    }
}
