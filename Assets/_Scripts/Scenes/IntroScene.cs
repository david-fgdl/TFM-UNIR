/* SCRIPT QUE CONTROLA LA ESCENA INTRODUCTORIA */

using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroScene : MonoBehaviour
{

    /* VARIABLES */

    // REFERENCIAS

    [SerializeField] private GameObject _utilsGameObject;

    private AudioUtils _audioUtils;
    private FileUtils _fileUtils;
    private TextViewUtils _textViewUtils;

    [SerializeField] private GameObject _textOutput;

    // ARRAYS DE AUDIO
    [SerializeField] private AudioClip[] _introClipEnglish;
    [SerializeField] private AudioClip [] _introClipSpanish;

    // SELECCION DE IDIOMA
    [SerializeField] private bool _isEnglish = true;
    [SerializeField] private bool _hasVoice = true;

    /* METODOS BASICOS */

    // METODO AWAKE
    private void Awake()
    {

        // OPTIMIZACION DE RENDIMIENTO
        Application.targetFrameRate = 60;

        // RECOGIDA DE REFERENCIAS

        _utilsGameObject = GameObject.Find("KeepOnLoadGameObject");

        _audioUtils = _utilsGameObject.GetComponent<AudioUtils>();
        _fileUtils = _utilsGameObject.GetComponent<FileUtils>();
        _textViewUtils = _utilsGameObject.GetComponent<TextViewUtils>();

    }

    // METODO START
    // Start es un mEtodo llamado antes del primer frame
    private void Start()
    {
        // PROTECCION DE OBJETO A PASAR ENTRE ESCENAR E INICIO DE CONTRLADOR DE ESCENA
        DontDestroyOnLoad(GameObject.Find("KeepOnLoadGameObject"));
        StartCoroutine(IntroTextController());
    }

    /* METODOS DE LA ESCENA */

    // METODO PARA CONTROLAR LA PROGRESION DE LA ESCENA
    private IEnumerator IntroTextController()
    {

        yield return new WaitForSeconds(1.0f);

        // CONTROL DE VOLUMEN Y SALIDA DE AUDIO
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

        // AVANCE DE LA ESCENA
        if (_isEnglish) yield return StartCoroutine(_textViewUtils.ShowFileContent(_textOutput.GetComponent<TextMeshProUGUI>(), Application.dataPath + "/StreamingAssets/Dialogs/intro_english.txt", 2, 1, 2));
        else yield return StartCoroutine(_textViewUtils.ShowFileContent(_textOutput.GetComponent<TextMeshProUGUI>(), Application.dataPath + "/StreamingAssets/Dialogs/intro_spanish.txt", 2, 1, 2));

        yield return new WaitForSeconds(1.0f);

        SceneManager.LoadScene("RoomTest");  // Cuando se acaba la introducciOn, se pasa a la escena del juego


    }
}
