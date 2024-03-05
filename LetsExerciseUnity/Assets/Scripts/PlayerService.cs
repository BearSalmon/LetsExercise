using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerService
{
    DataBase dataBase;
    public PlayerService()
    {
        dataBase = new DataBase();
    }
    public void CreatePlayerTable()
    {
        dataBase.GetConnection().DropTable<Player>();
        dataBase.GetConnection().CreateTable<Player>();
    }
    // add new players 
    public int AddPlayer(Player player)
    {
        return dataBase.GetConnection().Insert(player);
    }

    // get all players
    public IEnumerable<Player> GetPlayers()
    {
        return dataBase.GetConnection().Table<Player>();
    }
}
