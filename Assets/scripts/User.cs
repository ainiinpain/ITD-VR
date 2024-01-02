using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class User
{
    //attributes
    //JsonUtility will only convert those in public
    public string username;
    public string email;
    public string password;

    //variable to store number of clicks
    /*public int clicks;*/

    //human has ... eyes ears, hair, etc.. 
    //public string hairColor;

    //constructor empty

    //parameterized constructor
    public User(string username1, string email1, string password1)
    {
        this.username = username1;
        this.email = email1;
        this.password = password1;

    }
}
