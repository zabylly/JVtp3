using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class EndGameReport : MonoBehaviour
{
    TMP_Text score;
    TMP_Text message;
    //initialisation du message de scene de fin
    void Start()
    {
        GetComponentInChildren<UnityEngine.UI.Button>().onClick.AddListener(Restart);
        score = GameObject.Find("Text_Score").GetComponent<TMP_Text>();
        message = GameObject.Find("Text_Message").GetComponent<TMP_Text>();

        message.text = PlayerPrefs.GetString("message");
        score.text = PlayerPrefs.GetInt("score").ToString() + " points";
    }
    //boutton pour recommencer
    public static void Restart()
    {
        SceneManager.LoadScene("SampleScene");
    }
}
