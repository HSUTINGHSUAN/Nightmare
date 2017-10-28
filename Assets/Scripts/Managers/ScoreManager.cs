using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreManager : MonoBehaviour
{
    public static int score;//靜態變數 //可以直接用 ScoreManager.score //只能有一個變數實體 //遊戲裡面只能有一個score


    Text text;


    void Awake ()
    {
        text = GetComponent <Text> ();
        score = 0;
    }


    void Update ()
    {
        text.text = "Score: " + score;//只要有人改值，就會改變內容
    }
}
