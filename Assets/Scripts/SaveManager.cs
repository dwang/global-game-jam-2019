﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class SaveManager : MonoBehaviour, IService
{
    public SaveState State { get; private set; }

    public void Awake()
    {
        Load();
        
        ServiceLocator.Instance.AddService(this);
    }

    public void ResetSave()
    {
        State = new SaveState();
        SetDefaultValues();
        Save();
    }

    private void SetDefaultValues()
    {
        State.highscores = new int[5];
    }

    public void Save()
    {
        BinaryFormatter binaryFormatter = Utils.GetBinaryFormatterWithSurrogates();

        FileStream fs = new FileStream(Application.persistentDataPath + "/Save.dat", FileMode.OpenOrCreate);

        binaryFormatter.Serialize(fs, State);

        fs.Close();
    }

    //Load save state and deserializes it
    public void Load()
    {
        // Open the file containing the data that you want to deserialize.
        if (!File.Exists(Application.persistentDataPath + "/Save.dat"))
            ResetSave();
        else
        {
            FileStream fs = new FileStream(Application.persistentDataPath + "/Save.dat", FileMode.Open);
            try
            {
                BinaryFormatter binaryFormatter = Utils.GetBinaryFormatterWithSurrogates();

                // Deserialize the hashtable from the file and 
                // assign the reference to the local variable.
                State = binaryFormatter.Deserialize(fs) as SaveState;
            }
            catch (SerializationException e)
            {
                Debug.Log("Failed to deserialize. Reason: " + e.Message);
                throw;
            }
            finally
            {
                fs.Close();
            }
        }
    }

    public void AddHighScore(int newScore)
    {
        int location = -1;
        for (int i = 0; i < State.highscores.Length; i++)
            if (newScore > State.highscores[i])
            {
                location = i;
                break;
            }
        if (location > -1)
        {
            int temp = State.highscores[location];
            for (int i = State.highscores.Length - 1; i > location; i--)
                State.highscores[i] = State.highscores[i - 1];
            State.highscores[location] = newScore;
        }
    }
}
