using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroScene : MonoBehaviour
{

    [SerializeField] private Utils utils;
    [SerializeField] private GameObject text_output;

    [SerializeField] private AudioClip[] intro_clips_english;
    [SerializeField] private AudioClip [] intro_clips_spanish;

    [SerializeField] private bool english = true;
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

        if (with_voice)
        {
            if (english)
            {
                GetComponent<AudioSource>().volume = 0.75f;
                StartCoroutine(utils.PlayAudioWhenShowFileContent(GetComponent<AudioSource>(), intro_clips_english, Application.dataPath + "/StreamingAssets/Dialogs/intro_english.txt", 2, 1, 2));
            }
            else
            {
                GetComponent<AudioSource>().volume = 1;
                StartCoroutine(utils.PlayAudioWhenShowFileContent(GetComponent<AudioSource>(), intro_clips_spanish, Application.dataPath + "/StreamingAssets/Dialogs/intro_spanish.txt", 2, 1, 2));
            }
        }
        if (english) yield return StartCoroutine(utils.ShowFileContent(text_output.GetComponent<TextMeshProUGUI>(), Application.dataPath + "/StreamingAssets/Dialogs/intro_english.txt", 2, 1, 2));
        else yield return StartCoroutine(utils.ShowFileContent(text_output.GetComponent<TextMeshProUGUI>(), Application.dataPath + "/StreamingAssets/Dialogs/intro_spanish.txt", 2, 1, 2));


        yield return new WaitForSeconds(1.0f);

        SceneManager.LoadScene("RoomTest");


    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
