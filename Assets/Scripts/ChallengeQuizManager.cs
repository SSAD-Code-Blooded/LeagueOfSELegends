using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using Firebase.Firestore;
using Firebase.Extensions;
using System.Linq;
using System.Text;
using TMPro;
using Photon.Pun;
using Photon.Realtime;

public class ChallengeQuizManager : MonoBehaviourPun
{
    private string level = ChallengeRoom.challengeLevel;
    private string section = ChallengeRoom.challengeSection;
    private string world = ChallengeRoom.challengeWorld;

    public int maxHealth = 100;
    public int player1Health;
    public int player2Health;
    public HealthBar player1HealthBar;
    public HealthBar player2HealthBar;

    public List<QuestionsAndAnswers> QnA;
    public GameObject[] options;
    public int currentQuestion;

    public Text QuestionTxt;
    public Text ScoreTxt;
    public Text OpponentScoreTxt;
    public Text currScore;
    public Text countdownText;
    public Text ResultText;

    float currentTime = 0f;
    float startingTime = 30f;

    int totalQuestions= 0;
    public int score = 0;
    public int win_game;

    public GameObject Quizpanel;
    public GameObject GoPanel;
    public GameObject Timer;
    public GameObject Waitingpanel;

    public TextMeshProUGUI player1email; // to show in the UIUX
    public TextMeshProUGUI player2email; // to show in the UIUX

    private string player1emailtext;
    private string player2emailtext;

    private int player1Score = 0;
    private int player2Score = 0;

    private string player1ScoreString;
    private string player2ScoreString;

    private string player1Done;
    private string player2Done;

    PhotonView pv;

    public Dictionary<string, object> questionBank;

    private bool finishUpdate = false;
    

    private void Start()
    {
        // initialise Health. Char already initialised.
        player1Health = maxHealth;
        player2Health = maxHealth;
        player1HealthBar.SetMaxHealth(maxHealth);
        player2HealthBar.SetMaxHealth(maxHealth);
        finishUpdate = true;
    
        currentTime = startingTime;
        
        // get data from firebase
        dataFetch(world,level);

        totalQuestions = QnA.Count; // no idea what is this for

        currScore.text = score + "";

        GoPanel.SetActive(false);
        Waitingpanel.SetActive(false);
        
        // display 1 question and 4 options
        generateQuestion();

        pv = GetComponent<PhotonView>();
        Debug.Log(ChallengeRoom.player1email + " " + ChallengeRoom.player2email);

        ExitGames.Client.Photon.Hashtable GameRoomInfo = new ExitGames.Client.Photon.Hashtable();
        // no need add for player 1 as it will definitely be inside
        GameRoomInfo.Add("player2email", null);
        GameRoomInfo.Add("player1done", false);
        GameRoomInfo.Add("player2done", false);
        GameRoomInfo.Add("player1score", player1Score);
        GameRoomInfo.Add("player2score", player2Score);
        
        if (PhotonNetwork.CurrentRoom.SetCustomProperties(GameRoomInfo))
        {
            Debug.Log("Successfully set Game Room Info");
        }
        else 
        {
            Debug.Log("Failed set Game Room Info");
        }

        if (ChallengeRoom.playerID == 2)
        {
            ExitGames.Client.Photon.Hashtable Roominfo2 = new ExitGames.Client.Photon.Hashtable();
            Roominfo2.Add("player2email", ChallengeRoom.player2email);
            Roominfo2.Add("player1score", player1Score);
            Roominfo2.Add("player2score", player2Score);
            Roominfo2.Add("player1done", false);
            Roominfo2.Add("player2done", false);
            if (PhotonNetwork.CurrentRoom.SetCustomProperties(Roominfo2))
            {
                Debug.Log("Successfully set p2 info");
            }
            else Debug.Log("Failed set Roominfo");
        }        
    }

    void Update()
    {
        dataFetch(world,level);
        // initialise char's email on 
        if (ChallengeRoom.playerID == 1)
        {
            player1email.text = ChallengeRoom.player1email;
            player2email.text = PhotonNetwork.CurrentRoom.CustomProperties["player2email"].ToString();
        }       
        else
        {
            player1email.text = ChallengeRoom.player2email; 
            player2email.text = ChallengeRoom.player1email;
        }
        
        // check if both players are done. if yes, return results for both players
        if ((bool)PhotonNetwork.CurrentRoom.CustomProperties["player1done"] && (bool)PhotonNetwork.CurrentRoom.CustomProperties["player2done"])
        {
            // player 2 won
            if ((int)PhotonNetwork.CurrentRoom.CustomProperties["player1score"] < (int)PhotonNetwork.CurrentRoom.CustomProperties["player2score"])
            {
                if (ChallengeRoom.playerID == 1) // player 1
                {
                    GameOver('L');
                    PhotonNetwork.LeaveRoom();
                    
                }
                else // player 2
                {
                    GameOver('W');
                    // PhotonNetwork.CurrentRoom.CustomProperties["player2email"].ToString(); // add one win to this email
                    Debug.Log(PhotonNetwork.CurrentRoom.CustomProperties["player2email"].ToString() + "won!");
                    if (finishUpdate)
                    {
                        QuestionService.challengeModeUpdate(PhotonNetwork.CurrentRoom.CustomProperties["player2email"].ToString());
                        finishUpdate = false;
                    }
                    
                    PhotonNetwork.LeaveRoom();
                }
            }
            // player 1 won:
            else if ((int)PhotonNetwork.CurrentRoom.CustomProperties["player1score"] > (int)PhotonNetwork.CurrentRoom.CustomProperties["player2score"])
            {
                if (ChallengeRoom.playerID == 2) // player 2
                {
                    GameOver('L');
                    PhotonNetwork.LeaveRoom();
                }
                else // player 1
                {
                    GameOver('W');
                    // ChallengeRoom.player1email; // add one win to this email
                    Debug.Log(ChallengeRoom.player1email + "won!");
                    if (finishUpdate)
                    {
                        QuestionService.challengeModeUpdate(ChallengeRoom.player1email);  
                        finishUpdate = false;
                    }
                       
                    PhotonNetwork.LeaveRoom(); 
                }
            }
            // player 1 and 2 draw
            else if ((int)PhotonNetwork.CurrentRoom.CustomProperties["player1score"] == (int)PhotonNetwork.CurrentRoom.CustomProperties["player2score"])
            {
                GameOver('D'); // for player 1 and 2
                PhotonNetwork.LeaveRoom();
                if (ChallengeRoom.playerID == 2)
                {
                    //ChallengeRoom.player1email; // add one win to this email      
                    //ChallengeRoom.player2email; // add one win to this email
                    Debug.Log("draw!");
                    
                    if (finishUpdate)
                    {
                        QuestionService.challengeModeUpdate(ChallengeRoom.player1email);
                        QuestionService.challengeModeUpdate(ChallengeRoom.player2email);  
                        finishUpdate = false;
                    }
                    
                    
                }
    
            }
            Waitingpanel.SetActive(false);
            GoPanel.SetActive(true);
        }
        // player 1 is done, player 2 is not done
        else if ((bool)PhotonNetwork.CurrentRoom.CustomProperties["player1done"] &&
                !(bool)PhotonNetwork.CurrentRoom.CustomProperties["player2done"])
                {
                    if (ChallengeRoom.playerID == 1)
                    {
                        Waitingpanel.gameObject.SetActive(true);
                    }                    
                }
        // player 2 is done, player 1 is not done
        else if ((bool)PhotonNetwork.CurrentRoom.CustomProperties["player2done"] &&
                !(bool)PhotonNetwork.CurrentRoom.CustomProperties["player1done"])
                {
                    if (ChallengeRoom.playerID == 2)
                    {
                        Waitingpanel.gameObject.SetActive(true);
                    }
                    
                }
    }
    
    public void onClickButtonToBackAfterCompletingChallenge()
    { 
        SceneManager.LoadScene(sceneName:"4.1 Challenge Create Or Join Room Menu");
    }

    public async void dataFetch(string world, string difficulty)
    {
        int count=0;
        FirebaseFirestore db = FirebaseFirestore.DefaultInstance;
        UnityEngine.Debug.Log("Connection established");
        Query questionQuery = db.Collection($"QnA/{world}/Sections/Functional Testing/difficulty/{difficulty}/Questions");
        //Subsequently 'Easy' should be made into a variable
        questionQuery.GetSnapshotAsync().ContinueWithOnMainThread(task => {
        QuerySnapshot questionQuery = task.Result;

        foreach (DocumentSnapshot documentSnapshot in questionQuery.Documents) {
            QuestionsAndAnswers questionsAndAnswers = new QuestionsAndAnswers();
            // UnityEngine.Debug.Log(System.String.Format("Document data for {0} document:", documentSnapshot.Id));
            
            // var test=documentSnapshot.ToDictionary()["Answers"] as object[];
            // UnityEngine.Debug.Log(test);

            Dictionary<string, object> question = documentSnapshot.ToDictionary();

            // UnityEngine.Debug.Log(question["Answers"] as List<object>);
            // UnityEngine.Debug.Log("here;"+ questionsAndAnswers.Answers[0]);

            foreach (KeyValuePair<string, object> pair in question) {   
                if (pair.Key.Equals("CorrectAnswer")){
                    // UnityEngine.Debug.Log("Correct Answer If");
                    questionsAndAnswers.CorrectAnswer=System.Convert.ToInt32(pair.Value);
                    // UnityEngine.Debug.Log(questionsAndAnswers.CorrectAnswer);
                }
                else if(pair.Key.Equals("Answers")){
                    // UnityEngine.Debug.Log("Answers If");
                    questionsAndAnswers.Answers=((IEnumerable)pair.Value).Cast<string>().Select(x => x.ToString()).ToArray(); 
                    // UnityEngine.Debug.Log(questionsAndAnswers.Answers[0]);
                    // UnityEngine.Debug.Log(questionsAndAnswers.Answers[1]);
        
                    }
        
                else if (pair.Key.Equals("Question")){
                    // UnityEngine.Debug.Log("Question If");
                    questionsAndAnswers.Question=pair.Value.ToString();
                }
            UnityEngine.Debug.Log(System.String.Format("{0}: {1}", pair.Key, pair.Value));
            }
                if (count<5){
                    QnA.Add(questionsAndAnswers);
                    count=count+1;
                }
                //questionBank=question;
            };
        
        }
    );
    }

    void GameOver(char Result)
    {    
        ScoreTxt.text = "Your score: " + score.ToString() + "";
        if (ChallengeRoom.playerID == 1)
        {
            OpponentScoreTxt.text = "Opponent score: " + PhotonNetwork.CurrentRoom.CustomProperties["player2score"].ToString() + "";
        }
        else
        {
            OpponentScoreTxt.text = "Opponent score: " + PhotonNetwork.CurrentRoom.CustomProperties["player1score"].ToString() + "";
        }
        
        if(Result == 'W')
        {
            ResultText.text = "VICTORY!";
            ResultText.color= Color.yellow;
        }
        else if(Result == 'L')
        {
            ResultText.text = "DEFEAT!";
            ResultText.color= Color.red;
        }

        else if(Result == 'D')
        {
            ResultText.text = "DRAW!";
            ResultText.color= Color.green;
        }
    }

    public void correct()
    {
        if (ChallengeRoom.playerID == 1)
        {
            player1Score += 1;
            score += 1;
            currScore.text = score + "";
        }
        if (ChallengeRoom.playerID == 2)
        {
            player2Score += 1;
            score += 1;
            currScore.text = score + "";
        }
        
        QnA.RemoveAt(currentQuestion);
        TakeDamage(20, "P2");
        generateQuestion();
    }

    public void wrong()
    {
        QnA.RemoveAt(currentQuestion);
        TakeDamage(20, "P1");
        generateQuestion();
    }


    void TakeDamage(int damage, string player)
    {
        if (player == "P2")
        {
            player2Health -= damage;
            player2HealthBar.SetHealth(player2Health);
        }

        else if (player == "P1")
        {
            player1Health -= damage;
            player1HealthBar.SetHealth(player1Health);

        }

        if(player2Health == 0 ) //comment out after waichung fixed pull of 5 qns for challenge mode
        {
            Quizpanel.SetActive(false);
            Waitingpanel.SetActive(true);
            updateScore();
        }

        else if(player1Health == 0 ) // comment out after waichung fixed pull of 5 qns for challenge mode
        {
            Quizpanel.SetActive(false);
            Waitingpanel.SetActive(true);
            updateScore();
        }
    }

    void SetAnswers()
    {
        for (int i = 0; i < options.Length; i++)
        {
            options[i].GetComponent<ChallengeAnswersScript>().isCorrect = false;
            options[i].transform.GetChild(0).GetComponent<Text>().text = QnA[currentQuestion].Answers[i];

            if(QnA[currentQuestion].CorrectAnswer == i+1)
            {
                options[i].GetComponent<ChallengeAnswersScript>().isCorrect = true;
            }
        }
    }

    void generateQuestion()
    {
        if(QnA.Count > 0)
        {
            currentQuestion = UnityEngine.Random.Range(0, QnA.Count);
            QuestionTxt.text = QnA[currentQuestion].Question;
            SetAnswers();
        }
        else // QnA.Count <= 0 (finished all qns)
        {
            UnityEngine.Debug.Log("Out of Questions");
            Quizpanel.SetActive(false);
            Waitingpanel.SetActive(true);
            updateScore();
        }
    }
    void updateScore()
    {
        ExitGames.Client.Photon.Hashtable LatestRoomInfo = new ExitGames.Client.Photon.Hashtable();
        if (ChallengeRoom.playerID == 1)
        {   
            LatestRoomInfo.Add("player1score", player1Score);
            LatestRoomInfo.Add("player2score", PhotonNetwork.CurrentRoom.CustomProperties["player2score"]);
        }
        else if (ChallengeRoom.playerID == 2)
        {
            LatestRoomInfo.Add("player1score", PhotonNetwork.CurrentRoom.CustomProperties["player1score"]);
            LatestRoomInfo.Add("player2score", player2Score);
        }
        LatestRoomInfo.Add("player1done",  ChallengeRoom.playerID == 1 ? true : (bool)PhotonNetwork.CurrentRoom.CustomProperties["player1done"]);
        LatestRoomInfo.Add("player2done",  ChallengeRoom.playerID == 2 ? true : (bool)PhotonNetwork.CurrentRoom.CustomProperties["player2done"]);
        
        if (PhotonNetwork.CurrentRoom.SetCustomProperties(LatestRoomInfo))
        {
            Debug.Log("Successfully set LatestRoomInfo");
        }
        else 
        {
            Debug.Log("Failed set LatestRoomInfo");
        }
    }
}
