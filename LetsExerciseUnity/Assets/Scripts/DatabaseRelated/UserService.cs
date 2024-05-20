using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserService
{
    DataBase dataBase;
    public UserService(DataBase db)
    {
        dataBase = db;
    }
    public void CreateUserTable()
    {
        dataBase.GetUserConnection().DropTable<User>();
        dataBase.GetUserConnection().CreateTable<User>();
    }
    // add new players 
    public int AddUser(User user)
    {
        return dataBase.GetUserConnection().Insert(user);
    }

    // get all players
    public IEnumerable<User> GetUsers()
    {
        return dataBase.GetUserConnection().Table<User>();
    }


    // get player by name
    public User GetUserByName(string name)
    {
        return dataBase.GetUserConnection().Table<User>().Where(x => x.Name == name).FirstOrDefault();
    }

    // update player by name
    public int UpdateUser(User user)
    {
        return dataBase.GetUserConnection().Update(user);
    }

    // Count player number
    public int CountUsers()
    {
        Debug.Log("hi");
        return dataBase.GetUserConnection().Table<User>().Count();
    }
}
