using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroScene : MonoBehaviour
{

    [SerializeField] private Utils utils;
    [SerializeField] private GameObject text_output;

    [SerializeField] private AudioClip [] intro_clips;

    [SerializeField] private bool with_voice = true;

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(GameObject.Find("KeepOnLoadGameObject"));
        StartCoroutine(IntroTextController());
    }

    private IEnumerator IntroTextController()
    {

        yield return new WaitForSeconds(1.0f);

        if(with_voice) StartCoroutine(utils.PlayAudioWhenShowFileContent(GetComponent<AudioSource>(), intro_clips, "Assets/Dialogs/intro.txt", 2, 1, 2));
        yield return StartCoroutine(utils.ShowFileContent(text_output.GetComponent<TextMeshProUGUI>(), "Assets/Dialogs/intro.txt", 2, 1, 2));
        

        yield return new WaitForSeconds(1.0f);

        SceneManager.LoadScene("RoomTest");


    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
