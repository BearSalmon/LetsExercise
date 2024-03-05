using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DBUtils : MonoBehaviour
{
    PlayerService playerService;
    // Start is called before the first frame update
    void Start()
    {
        playerService = new PlayerService();
        playerService.CreatePlayerTable();
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


    public void AddPlayer()
    {
        Player player = new Player
        {
            Name = "Player1",
        };
        int pk = playerService.AddPlayer(player);
        Debug.Log("Primary key = " + pk);
        
    }

    public void GetPlayers()
    {
        var players = playerService.GetPlayers();
        ToConsole(players);
    }
    
}
