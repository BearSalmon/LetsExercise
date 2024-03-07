using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DBUtils : MonoBehaviour
{
    PlayerService playerService;

    public string nowPlayer;

    // Start is called before the first frame update
    void Start()
    {
        playerService = new PlayerService();
        playerService.CreatePlayerTable();
        TestAddPlayers();
    }

    private void ToConsole(IEnumerable<Player> players)
    {
        foreach (var player in players)
        {
            ToConsole(player.ToString());
        }
    }

    private void ToConsole(string msg)
    {
        Debug.Log(msg);
    }

    // for test only
    public void TestAddPlayers()
    {
        DateTime currentTime = DateTime.Now;
        string currentTimeString = currentTime.ToString("yyyy-MM-dd HH:mm:ss");

        Player player = new Player
        {
            Name = "Player1",
            Gender = "Girl",
            Hair = "000000FF",
            Body = "FFDBC6FF",
            Cloth = "CAEE8AFF",
            LastLogin = currentTimeString,
            DominantHand = "Left"
        };
        int pk = playerService.AddPlayer(player);
        Debug.Log("Primary key = " + pk);

        player = new Player
        {
            Name = "Player2",
            Gender = "Boy",
            Hair = "000000FF",
            Body = "FFDBC6FF",
            Cloth = "CAEE8AFF",
            LastLogin = currentTimeString,
            DominantHand = "Left"
        };
        pk = playerService.AddPlayer(player);
        Debug.Log("Primary key = " + pk);

        player = new Player
        {
            Name = "Player3",
            Gender = "Boy",
            Hair = "000000FF",
            Body = "FFDBC6FF",
            Cloth = "CAEE8AFF",
            LastLogin = currentTimeString,
            DominantHand = "Left"
        };
        pk = playerService.AddPlayer(player);
        Debug.Log("Primary key = " + pk);
    }

    public void AddPlayer()
    {
        int number = playerService.CountPlayers();
        Debug.Log(number);
        number += 1;
        nowPlayer = "Player"+number.ToString();

        Player player = new Player
        {
            Name = "Player"+number.ToString(),
        };
        int pk = playerService.AddPlayer(player);
        Debug.Log("Primary key = " + pk);
        
    }

    public IEnumerable<Player> GetPlayers()
    {
        var players = playerService.GetPlayers();
        ToConsole(players);
        return players;
    }

    public Player GetPlayerByName(string name)
    {
        return playerService.GetPlayerByName(name);
    }

    public void UpdatePlayer(Player player)
    {
        //Debug.Log(player.Gender);
        playerService.UpdatePlayer(player);
    }

    public int CountPlayers()
    {
        return playerService.CountPlayers();
    }
    
}
