using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Firebase.Firestore;
using Firebase.Extensions;
using Firebase.Auth;
using System.Linq;

/****************************************************//**
 *  ... Quiz Manager- fetches data for the quiz page UI
 ******************************************************/

public class QuizManager : MonoBehaviour
{
   public int maxHealth = 100; /**< Maximum Health of player/monster character */
   public int currentPlayerHealth; /**< Current Health of the Player */
   public int currentMonsterHealth; /**< Current Health of the Monster */
   public HealthBar playerHealthBar; /**< HealthBar object for Player */
   public HealthBar monsterHealthBar; /**< HealthBar object for Monster */

   public int userScore; /** User's Score */

   public List<QuestionsAndAnswers> QnA; /**< List of Questions and Answers */
   public GameObject[] options; /**< An array of GameObjects for the options */
   public int currentQuestion; /**< The current question number */

   public Text QuestionTxt; /**< Text of the Question */
   public Text ScoreTxt; /**< Text that displays the score */
   public Text currScore; /**< Current socre of the player */
   public Text countdownText; /**< Text that displays time remaining */
   public Text ResultText; /**< Text displaying result (win/loss) */

   float currentTime = 0f; /**< Current Time remaining */
   float startingTime = 30f; /**< Initializing starting time to 30 seconds */
   bool flag=true; 
   bool quiz_started = false; /**< Whether the quiz has begun */


   int totalQuestions= 0; /**< Total questions */
   public int score = 0; /**< Score */
   public int win_game; /**< Wherher tou won or lost the game */

   public GameObject Quizpanel; /**< Game Object for the Quiz Panel with Questions*/
   public GameObject GoPanel; /**< Game Object for the Game Over Panel */
   public GameObject StartPanel; /**< Game Object for the Start Dialog Panel*/
   public GameObject Timer; /**< Game Object for the Countdown Timer */

   public Dictionary<string, object> questionBank; /**< Dictionary containing the Questions*/

//    public QuestionDAO questionDAO;

   private void Start()
   {

    currentPlayerHealth = maxHealth;
    currentMonsterHealth = maxHealth;
    playerHealthBar.SetMaxHealth(maxHealth);
    monsterHealthBar.SetMaxHealth(maxHealth);

    currentTime = startingTime;

    
    string userProfileLevel = userDAO.getUserProgressLevel();
    UnityEngine.Debug.Log(userProfileLevel);
    dataFetch(userProfileLevel);

    totalQuestions = QnA.Count;
    currScore.text = score + "";
    Quizpanel.SetActive(false);
    GoPanel.SetActive(false);
    generateQuestion();

   } //!< Function that runs when the scene is loaded.


    void Update()
    {
        if(quiz_started == true)
        {
        currentTime -= 1 * Time.deltaTime;
        countdownText.text = currentTime.ToString("0");

        if(currentTime <= 10)
        {
            countdownText.color = Color.red;
        }

        if(currentTime <= 0){
            GameOver('T');
        }
            
        }
        
        
    }

    public void startquiz()
    {
        StartPanel.SetActive(false);
        Quizpanel.SetActive(true);
        QnA.RemoveAt(currentQuestion);
        generateQuestion();
        quiz_started = true;


    }

   public void retry()
   {
        string userProfileLevel = userDAO.getUserProgressLevel();
        dataFetch(userProfileLevel);
        generateQuestion();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
   }

    public void backButton()
    {
        SceneManager.LoadScene("World");
    }

    public async void dataFetch(string userProfileLevel)
    {
     FirebaseFirestore db = FirebaseFirestore.DefaultInstance;
     UnityEngine.Debug.Log("Connection established");
        Query questionQuery = db.Collection($"QnA/Testing/Sections/Functional Testing/difficulty/{userProfileLevel}/Questions");
        //Subsequently 'Easy' should be made into a variable
        questionQuery.GetSnapshotAsync().ContinueWithOnMainThread(task => {
        QuerySnapshot questionQuery = task.Result;
        foreach (DocumentSnapshot documentSnapshot in questionQuery.Documents) {
            QuestionsAndAnswers questionsAndAnswers = new QuestionsAndAnswers();
            UnityEngine.Debug.Log(System.String.Format("Document data for {0} document:", documentSnapshot.Id));
            // var test=documentSnapshot.ToDictionary()["Answers"] as object[];
            // UnityEngine.Debug.Log(test);
            Dictionary<string, object> question = documentSnapshot.ToDictionary();
            // UnityEngine.Debug.Log(question["Answers"] as List<object>);
            // UnityEngine.Debug.Log("here;"+ questionsAndAnswers.Answers[0]);
            foreach (KeyValuePair<string, object> pair in question) {   
                if (pair.Key.Equals("CorrectAnswer")){
                    UnityEngine.Debug.Log("Correct Answer If");
                    questionsAndAnswers.CorrectAnswer=System.Convert.ToInt32(pair.Value);
                    UnityEngine.Debug.Log(questionsAndAnswers.CorrectAnswer);
                }
                else if(pair.Key.Equals("Answers")){
                    UnityEngine.Debug.Log("Answers If");
                    questionsAndAnswers.Answers=((IEnumerable)pair.Value).Cast<string>().Select(x => x.ToString()).ToArray(); 
                    UnityEngine.Debug.Log(questionsAndAnswers.Answers[0]);
                    UnityEngine.Debug.Log(questionsAndAnswers.Answers[1]);
        
                    }
        
                else if (pair.Key.Equals("Question")){
                    UnityEngine.Debug.Log("Question If");
                    questionsAndAnswers.Question=pair.Value.ToString();
                }
            UnityEngine.Debug.Log(System.String.Format("{0}: {1}", pair.Key, pair.Value));
            }
                QnA.Add(questionsAndAnswers);
                //questionBank=question;
            };
        
        }
    );
        
        
    }

    void GameOver(char Result)
   {
    Timer.SetActive(false);
    Quizpanel.SetActive(false);
    StartPanel.SetActive(false);
    GoPanel.SetActive(true);
    ScoreTxt.text = score.ToString();
    

    if(Result == 'W')
    {
        ResultText.text = "You Won. Good job!";
        while (flag){
            flag=false;
            QuestionService.storyModeUpdate(Result);
            }
        
    }

    else if(Result == 'T')
    {
        ResultText.text = "You ran out of time!";
        while (flag){
            flag=false;
            QuestionService.storyModeUpdate(Result);
        }
    }

    else if(Result == 'L')
    {
        ResultText.text = "You Lost. Try Again!";
        while (flag){
            flag=false;
            QuestionService.storyModeUpdate(Result);
        }
    }


   }

   public void correct()
   {
        score += 1;
        QnA.RemoveAt(currentQuestion);
        currScore.text = score + "";
        TakeDamage(20, 'M');
        generateQuestion();

   }

   public void wrong()
   {
        
        QnA.RemoveAt(currentQuestion);
        TakeDamage(20, 'P');
        generateQuestion();
   }


   void TakeDamage(int damage, char character)
   {
    if (character == 'M')
    {
        currentMonsterHealth -= damage;
        monsterHealthBar.SetHealth(currentMonsterHealth);

    }

    else if (character == 'P')
    {
        currentPlayerHealth -= damage;
        playerHealthBar.SetHealth(currentPlayerHealth);

    }

    if(currentMonsterHealth == 0 )
    {
        win_game = 1;
        GameOver('W');
    }

    else if(currentPlayerHealth == 0 )
    {
        win_game = 0;
        GameOver('L');
    }
   }



   void SetAnswers()
   {
    for (int i = 0; i < options.Length; i++)
    {
        options[i].GetComponent<AnswersScript>().isCorrect = false;
        options[i].transform.GetChild(0).GetComponent<Text>().text = QnA[currentQuestion].Answers[i];

        if(QnA[currentQuestion].CorrectAnswer == i+1)
        {
            options[i].GetComponent<AnswersScript>().isCorrect = true;
        }
    }
   }

   void generateQuestion()
   {
    if(QnA.Count > 0)
    {
        currentQuestion = Random.Range(0, QnA.Count);
        QuestionTxt.text = QnA[currentQuestion].Question;
        SetAnswers();
    }

    else
    {
        UnityEngine.Debug.Log("Out of Questions");
        GameOver('W');
    }
    
    




   }
}
