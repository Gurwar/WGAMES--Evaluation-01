using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movie
{
    public Movie(string _title, string _year, string _imdbLink, string _imageLink)
    {
        title = _title;
        year = _year;
        imdbLink = _imdbLink;
        imageLink = _imageLink;
    }

    public string title;
    public string year;
    public string imdbLink;
    public string imageLink;
}

public class TextToList : MonoBehaviour
{
    [SerializeField]
    GameObject MoviePrefab;
    [SerializeField]
    Transform MovieParent;
    [SerializeField]
    float yOffset;
    [SerializeField]
    List<string> Titles = new List<string>();
    [SerializeField]
    List<string> Years = new List<string>();
    [SerializeField]
    List<string> IMDBLinks = new List<string>();
    [SerializeField]
    List<string> imageLinks = new List<string>();
    List<Movie> Movies = new List<Movie>();
    string data;
    string nameSearched;

    
    public void SetData(string d, string n)
    {
        data = d;
        nameSearched = n;
    }

    public void OrganizeMovies()
    {

        char splitter = '"';
        string[] a = data.Split(splitter);
        for (int i =0; i < a.Length; i++)
        {
            int indent = imageLinks.Count;

            if (i == 5 + (20 * indent))
            {
                if (a[i].ToLower().Contains(nameSearched))
                Titles.Add(a[i]);
            }
            else if (i == 9 + (20 * indent))
            {
                if (char.IsNumber(a[i], 0))
                Years.Add(a[i]);
            }
            else if (i == 13 + (20 * indent))
            {
                if (a[i].Contains("tt"))
                IMDBLinks.Add(a[i]);
            }
            else if (i == 21 + (20 * indent))
            {
                if (a[i].Contains("https"))
                    imageLinks.Add(a[i]);
            }
        }
        Debug.Log(Titles.Count);
        for (int i =0; i < Titles.Count; i++)
        {
            string title = "";
            string year = "";
            string ImdbLink = "";
            string imageLink = "";

            if (i < Titles.Count )
            title = Titles[i];
            if (i < Years.Count)
                year = Years[i];
            if (i < IMDBLinks.Count)
                ImdbLink = IMDBLinks[i];
            if (i < imageLinks.Count)
                imageLink = imageLinks[i];
            Movie tempMovie = new Movie(title, year, ImdbLink, imageLink);
            Movies.Add(tempMovie);
        }
    }

    public void DrawUI()
    {
        for (int i = 0; i < Movies.Count; i++)
        {
            GameObject tempMovie = (GameObject)Instantiate(MoviePrefab, MovieParent);
            tempMovie.transform.localPosition = new Vector3(0, 5000 + (-i * yOffset), 0);
            tempMovie.GetComponent<MovieManager>().SetMovieTitle(Movies[i].title + " " + Movies[i].year);
            tempMovie.GetComponent<MovieManager>().SetImageURL(Movies[i].imageLink);
            tempMovie.GetComponent<MovieManager>().SetIMDBLink(Movies[i].imdbLink);

        }
    }
}
