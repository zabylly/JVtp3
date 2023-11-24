using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;

public class ShowTime : Activable
{
    // Start is called before the first frame update
    TextMeshProUGUI messageTimer;
    public int temps = 0;
    private bool playing = true;
    void Start()
    {
        messageTimer = GetComponentInChildren<TextMeshProUGUI>();
        messageTimer.text = FormatTime(0);
        StartCoroutine(UpdateTimer());
    }
    public void Win()
    {
        playing = false;
        StopCoroutine(UpdateTimer());
        PlayerPrefs.SetString("message", "Tu es gagnant, voici ton score");
        PlayerPrefs.SetInt("score", temps * 2);
        SceneManager.LoadScene("EndGameReport");
    }
    public void Lose()
    {
        playing = false;
        StopCoroutine(UpdateTimer());
        PlayerPrefs.SetString("message", "Tu es perdant, voici ton score");
        PlayerPrefs.SetInt("score", temps);
        SceneManager.LoadScene("EndGameReport");
    }
    //Utiliser pour l'affichage du temps dans le jeux
    private IEnumerator UpdateTimer()
    {
        while (playing)
        {
            temps++;
            messageTimer.text = FormatTime(temps);
            yield return new WaitForSeconds(1.0f);
        }
    }
    private string FormatTime(int seconds)
    {
        if (seconds < 60)
            return seconds + " s";
        else
            return seconds/60 + " m " + seconds%60 + " s";
    }

    public override void Activate(bool Activate)
    {
        Win();
    }
}

