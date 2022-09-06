using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

// CLASE PARA MOSTRAR UN DIALOGO PARA EL JUGADOR
// CADA VEZ QUE ABRA UNA PUERTA O CREA UN OBJETO
public class PlayerDialog : MonoBehaviour
{
    [SerializeField] private GameObject _dialogObject;
    [SerializeField] private TMP_Text _dialogText;
    
    public static PlayerDialog Instance;

    private void Awake()
    {
        if (Instance != null && Instance != this) Destroy(this);
        else Instance = this;
    }

    public void ShowDialog(string msg)
    {
        StartCoroutine(ShowDialogRoutine(msg));
    }


    // MÉTODO PARA MOSTRAR DIALOGO
    private IEnumerator ShowDialogRoutine(string msg)
    {
        _dialogObject.SetActive(true);

        Debug.Log($"{msg}");
        _dialogText.text = $"{msg}";
        

        yield return new WaitForSeconds(1);
        _dialogObject.SetActive(false);
    }



}