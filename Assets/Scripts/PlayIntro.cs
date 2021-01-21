using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//https://stackoverflow.com/questions/48382042/is-it-possible-to-add-animated-intro-to-splash-screen-in-unity
public class PlayIntro : MonoBehaviour
{
    private const string Movie = "Logo_Intro.mov";

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(streamVideo(Movie));
    }

    private IEnumerator streamVideo(string video)
    {
        Handheld.PlayFullScreenMovie(video, new Color(0, 37/255f, 254/255f), FullScreenMovieControlMode.Hidden, FullScreenMovieScalingMode.Fill);
        yield return new WaitForEndOfFrame();
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainScene");
    }
}
