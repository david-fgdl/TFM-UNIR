using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroScene : MonoBehaviour
{

    private AudioUtils _audioUtils = new AudioUtils();
    private FileUtils _fileUtils = new FileUtils();
    [SerializeField] private GameObject _textOutput;

    [SerializeField] private AudioClip[] _introClipEnglish;
    [SerializeField] private AudioClip [] _introClipSpanish;

    [SerializeField] private bool _isEnglish = true;
    [SerializeField] private bool _hasVoice = true;

    private void Awake()
    {
        Application.targetFrameRate = 60;
    }

    // Start is called before the first frame update
    private void Start()
    {
        DontDestroyOnLoad(GameObject.Find("KeepOnLoadGameObject"));
        StartCoroutine(IntroTextController());
    }

    private IEnumerator IntroTextController()
    {

        yield return new WaitForSeconds(1.0f);

        if (_hasVoice)
        {
            if (_isEnglish)
            {
                GetComponent<AudioSource>().volume = 0.25f;
                StartCoroutine(_audioUtils.PlayAudioWhenShowFileContent(GetComponent<AudioSource>(), _introClipEnglish, Application.dataPath + "/StreamingAssets/Dialogs/intro_english.txt", 2, 1, 2));
            }
            else
            {
                GetComponent<AudioSource>().volume = 1;
                StartCoroutine(_audioUtils.PlayAudioWhenShowFileContent(GetComponent<AudioSource>(), _introClipSpanish, Application.dataPath + "/StreamingAssets/Dialogs/intro_spanish.txt", 2, 1, 2));
            }
        }
        // if (_isEnglish) yield return StartCoroutine(TextViewUtils.Instance.ShowFileContent(_textOutput.GetComponent<TextMeshProUGUI>(), Application.dataPath + "/StreamingAssets/Dialogs/intro_english.txt", 2, 1, 2));
        // else yield return StartCoroutine(TextViewUtils.Instance.ShowFileContent(_textOutput.GetComponent<TextMeshProUGUI>(), Application.dataPath + "/StreamingAssets/Dialogs/intro_spanish.txt", 2, 1, 2));


        yield return new WaitForSeconds(1.0f);

        SceneManager.LoadScene("RoomTest");


    }
}
