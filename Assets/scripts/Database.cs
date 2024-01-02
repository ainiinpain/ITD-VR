using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Firebase;
using Firebase.Database;
using Firebase.Auth;
using Firebase.Extensions;
using System.Threading.Tasks;
using System.Text.RegularExpressions;


public class Database : MonoBehaviour
{
    DatabaseReference mDatabaseRef;

    public TMP_InputField username;
    public TMP_InputField email;
    public TMP_InputField password;

    // Start is called before the first frame update
    void Start()
    {
        mDatabaseRef = FirebaseDatabase.DefaultInstance.RootReference;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void WriteNewUser(string username, string email, string password)
    {
        //create a user object
        User demoUser = new User(username, email, password);
        string json = JsonUtility.ToJson(demoUser);

        mDatabaseRef.Child("users").Child(username).SetRawJsonValueAsync(json);
    }

    /// <summary>
    /// sends input data to firebase to create account
    /// </summary>
    public void SendData()
    {
        string username1 = username.text.Trim();
        string email1 = email.text.Trim();
        string password1 = password.text.Trim();

        if (IsValidEmail(email1))
        {
            Debug.Log("Account successfully created");
        }
        else
        {
            Debug.LogError("Invalid email format");
        }

        /*SignUpUser(email1, password1);*/
        WriteNewUser(username1, email1, password1);
    }

    /// <summary>
    /// checks if email format is correct
    /// </summary>
    /// <param name="email"></param>
    /// <returns></returns>
    bool IsValidEmail(string email1)
    {
        string pattern = @"^[\w-]+(\.[\w-]+)*@([\w-]+\.)+[a-zA-Z]{2,7}$";
        return Regex.IsMatch(email1, pattern);
    }
}
