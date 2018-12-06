//using UnityEngine;
//using System.Collections;
//using Facebook.Unity;
//using UnityEngine.UI;
//using GestureRecognizer;

//public class FaceBookManager : MonoBehaviour
//{

//    //Use this for initialization
    
//    LevelController level;
//    public string userText;
//    void Awake()
//    {
//        if (!FB.IsInitialized)
//        {
//            FB.Init();
//        }
//        else
//        {
//            FB.ActivateApp();
//        }
//    }
//    public void Login()
//    {
//        FB.LogInWithReadPermissions(callback: OnLogin);

//    }

//    private void OnLogin(ILoginResult result)
//    {
//        if (FB.IsLoggedIn)
//        {
//            AccessToken token = AccessToken.CurrentAccessToken;
//            userText = token.UserId;
//            SharePost(userText);
//        }
//    }
//    private void SharePost(string user)
//    {
//        FB.ShareLink(
//            contentTitle: "Score!" + level.scoreText,
//            contentURL: new System.Uri("https://www.facebook.com/OneArmy113/")
//            //contentDescription: user.ToString() + " reached bla bla balba bla bla bla  " + Highscore.HighscoreCount.ToString()
//            );
//    }
//}