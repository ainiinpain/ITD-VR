using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Auth;
using TMPro;
using Firebase.Extensions;
using Firebase.Database;
using System.Linq;
using System;


public class Database : MonoBehaviour
{

    private FirebaseAuth auth;
    private FirebaseApp _app;
    DatabaseReference mDatabaseRef;

    [SerializeField] private TMP_InputField email; // input field for email
    [SerializeField] private TMP_InputField password; // input field for password 
    [SerializeField] private TMP_InputField username; // input field for username 
    /*    public GameObject username;
        public GameObject email;
        public GameObject password;*/

    public GameObject logincanvas;

    private async void Awake()
    {
        var dependancy = await FirebaseApp.CheckAndFixDependenciesAsync();
        if (dependancy == DependencyStatus.Available)
        {
            _app = FirebaseApp.DefaultInstance;
        }
    }

    private DatabaseReference reference; 

    // Start is called before the first frame update
    void Start()
    {
        mDatabaseRef = FirebaseDatabase.DefaultInstance.RootReference;

        auth = FirebaseAuth.DefaultInstance;
        email.onValueChanged.AddListener(handlevaluechange);
        password.onValueChanged.AddListener(handlevaluechange);
        username.onValueChanged.AddListener(handlevaluechange);
    }

    public void handlevaluechange(string A)
    {
        checkstate();

    }
    private void checkstate()
    {
        if (string.IsNullOrEmpty(email.text))
        {
            Debug.Log("Email is invalid");

        }
        if (string.IsNullOrEmpty(password.text))
        {
            Debug.Log("password is empty");

        }
        if (string.IsNullOrEmpty(username.text))
        {
            Debug.Log("username is empty");

        }
    }

    public void registerA()
    {
        StartCoroutine(RegisterB(email.text, password.text, username.text));
    }

    private IEnumerator RegisterB(string email, string password, string username)
    {
        var registertask = auth.CreateUserWithEmailAndPasswordAsync(email, password);
        yield return new WaitUntil(() => registertask.IsCompleted);
    }
    public void logincoroutine() // to login account
    {
        auth = Firebase.Auth.FirebaseAuth.DefaultInstance;


        auth.SignInWithEmailAndPasswordAsync(email.text, password.text).ContinueWithOnMainThread(task => {
            if (task.IsCanceled)
            {
                Debug.LogError("Login was canceled."); //prompt to user on the console that the login got cancelled

                return;
            }
            if (task.IsFaulted)
            {
                Debug.LogError("Login encountered an error: " + task.Exception); // prompt the user that the login have an error 
                return;
            }
            //Firebase.Auth.FirebaseUser user = task.Result;
            auth = Firebase.Auth.FirebaseAuth.DefaultInstance;

            Firebase.Auth.AuthResult result = task.Result; // check if result if successful 
            Debug.LogFormat("User signed in successfully: {0} ({1})", //prompt if can user is able to login in the console
                result.User.DisplayName, result.User.UserId); // show the display name for user and the user ID
            logincanvas.SetActive(false);
                
        });


    }
}
