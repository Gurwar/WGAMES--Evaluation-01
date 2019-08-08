using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class API : MonoBehaviour
{
    string URL = "http://www.omdbapi.com/?apikey=6367da97&s=";
    [SerializeField]
    InputField inputField;
    [SerializeField]
    TextToList TexttoList;
    public void PressButton()
    {
        URL += inputField.text;
        StartCoroutine(webRequest());
    }

    IEnumerator webRequest()
    {
        UnityWebRequest www = UnityWebRequest.Get(URL);
        yield return www.SendWebRequest();

        Debug.Log(www.downloadHandler.text);
        TexttoList.SetData(www.downloadHandler.text, inputField.text);
        TexttoList.OrganizeMovies();
        TexttoList.DrawUI();
    }
}
