using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

/// <summary>
/// Classifies the nine current game mechanics available in the game, to be used as indices in the save file
/// </summary>
public enum GameMechanic
{
    Health = 0,
    Movement = 1,
    Rotation = 2,
    Jumping = 3,
    Combat = 4,
    Dialogue = 5,
    Unlockables = 6,
    TimeFlow = 7,
    Interactibles = 8,
}

/// <summary>
/// Manages the save system
/// </summary>
public class SaveManagerController : MonoBehaviour
{
    [SerializeField] private string filepath = "mechanics.sav";

    private byte[] mechanics;

    // Start is called before the first frame update
    void Start()
    {
        mechanics = new byte[9];

        if (!File.Exists(filepath))
        {
            Reset();
            Save();
        }

        using var fr = File.OpenRead(filepath);
        fr.Read(mechanics, 0, 9);
    }

    /// <summary>
    /// Resets all the game mechanics to their default values
    /// Remember to save!
    /// </summary>
    public void Reset()
    {
        mechanics = new byte[9];
        mechanics[(uint)GameMechanic.Health] = 1;
        mechanics[(uint)GameMechanic.Movement] = 1;
        mechanics[(uint)GameMechanic.Rotation] = 1;
    }

    /// <summary>
    /// Toggles the value of a given mechanic.
    /// Remember to save!
    /// </summary>
    /// <param name="gameMechanic"></param>
    public void Toggle(GameMechanic gameMechanic)
    {
        var mechanic = (uint)gameMechanic;
        var value = mechanics[mechanic];

        mechanics[mechanic] = (byte)(value == 0 ? 1 : 0);
    }

    /// <summary>
    /// Sets the value of a given mechanic.
    /// Remember to save!
    /// </summary>
    /// <param name="gameMechanic"></param>
    /// <param name="value"></param>
    public void Set(GameMechanic gameMechanic, bool value)
    {
        mechanics[(uint)gameMechanic] = (byte)(value ? 1 : 0);
    }

    /// <summary>
    /// Gets the value of a given mechanic.
    /// </summary>
    /// <param name="gameMechanic"></param>
    /// <returns></returns>
    public bool Get(GameMechanic gameMechanic)
    {
        return mechanics[(uint)gameMechanic] != 0;
    }

    /// <summary>
    /// Writes the current settings to the file
    /// </summary>
    public void Save()
    {
        using var fw = File.OpenWrite(filepath);
        fw.Write(mechanics, 0, 9);
    }
}
