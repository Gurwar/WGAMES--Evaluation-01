using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class MovieManager : MonoBehaviour
{
    [SerializeField]
    Text MovieTitle;
    [SerializeField]
    Image Thumbnail;
    string url = "http://www.omdbapi.com/?apikey=6367da97&i=";
    string dataExtraInfo;
    [SerializeField]
    Text ExtendedInfo;
    bool ExtendedInfoShow;
    public void ToggleExtendedInfo()
    {
        ExtendedInfoShow = !ExtendedInfoShow;

        if (ExtendedInfoShow)
            ExtendedInfo.enabled = true;
        else
            ExtendedInfo.enabled = false;
    }
    public void SetMovieTitle(string s)
    {
        MovieTitle.text = s;
    }

    public void SetImageURL(string url)
    {
        StartCoroutine(GetText(url));
    }

    IEnumerator GetText(string url)
    {
        using (UnityWebRequest uwr = UnityWebRequestTexture.GetTexture(url))
        {
            yield return uwr.SendWebRequest();

            if (uwr.isNetworkError || uwr.isHttpError)
            {
                Debug.Log(uwr.error);
            }
            else
            {
                // Get downloaded asset bundle
                var texture = DownloadHandlerTexture.GetContent(uwr);
                Thumbnail.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
            }
        }
    }

    public void SetIMDBLink(string s)
    {
        url += s;
        StartCoroutine(GetExtraInfo());
    }

    IEnumerator GetExtraInfo()
    {
        UnityWebRequest www = UnityWebRequest.Get(url);
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            dataExtraInfo = www.downloadHandler.text;
            DrawExtraInfo();
        }



    }

    public void DrawExtraInfo()
    {
        char splitter = '"';
        string[] a = dataExtraInfo.Split(splitter);

        ExtendedInfo.text =
              "Rating" + a[11] + " \n"
            + "Runtime: " + a[19] + " \n"
            + "Genres" + a[23] + " \n"
            + "Director: " + a[27] + " \n"
            + "Writers" + a[31] + " \n"
            + "Actors: " + a[35] + " \n"
            + "Plot" + a[39] + " \n";
    }

}
