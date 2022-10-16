
using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Firebase.Firestore;
using Firebase.Extensions;
using System.Linq;

public class QuizManager : MonoBehaviour
{
   public List<QuestionsAndAnswers> QnA;
   public GameObject[] options;
   public int currentQuestion;

   public Text QuestionTxt;
   public Text ScoreTxt;
   public Text currScore;

   int totalQuestions= 0;
   public int score = 0;

   public GameObject Quizpanel;
   public GameObject GoPanel;

   public Dictionary<string, object> questionBank;

//    public QuestionDAO questionDAO;

   private void Start()
   {
    totalQuestions = QnA.Count;
    currScore.text = score + "";
    GoPanel.SetActive(false);
    dataFetch();
    
    generateQuestion();

   }

   public void retry()
   {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
   }

    public void dataFetch(){
    FirebaseFirestore db = FirebaseFirestore.DefaultInstance;
    UnityEngine.Debug.Log("Connection established");
    Query questionQuery = db.Collection("QnA/Testing/Sections/Functional Testing/difficulty/Easy/Questions");
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

    void GameOver()
   {
    Quizpanel.SetActive(false);
    GoPanel.SetActive(true);
    ScoreTxt.text = score + "/" + totalQuestions;


   }

   public void correct()
   {
        score += 1;
        QnA.RemoveAt(currentQuestion);
        currScore.text = score + "";
        generateQuestion();

   }

   public void wrong()
   {
        QnA.RemoveAt(currentQuestion);
        generateQuestion();
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
        GameOver();
    }
    




   }
}
