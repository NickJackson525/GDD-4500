using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Game_Manager
{
    #region Variables

    public enum Pickup { FAKE_PEDESTRIAN, KITTEN_CANNON, HARPOON, SHIELD }

    #endregion

    #region Singleton

    // create variable for storing singleton that any script can access
    private static Game_Manager instance;

    // create GameManager
    private Game_Manager()
    {

    }

    // Property for Singleton
    public static Game_Manager Instance
    {
        get
        {
            // If the singleton does not exist
            if (instance == null)
            {
                // create and return it
                instance = new Game_Manager();
            }

            // otherwise, just return it
            return instance;
        }
    }

    #endregion
}
