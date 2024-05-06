using System.Collections.Generic;
using SQLite4Unity3d;
using UnityEngine;

public class User
{

    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }
    public string Name { get; set; }
    public int Age { get; set; }
    public string Weight { get; set; } // array of int 
    public int Height { get; set; }
    public string Gender { get; set; }
    public string PreferPart { get; set; }
    public string Level { get; set; }
    public string Hair { get; set; }
    public string Body { get; set; }
    public string Cloth { get; set; }
    public string LastLogin { get; set; }
    public int Duration { get; set; }
    public int Calories { get; set; }
    public string RecommandList { get; set; } // array of string

    public override string ToString()
    {
        return string.Format("[Player: Id={0}, Name={1},  Age={2}, Weight={3}, Height={4}, Gender={5}, PreferPart={6}, Level={7}, Hair={8}, Body={9}, Cloth={10}, LastLogin={11}",
            Id, Name, Age , Weight,Height,Gender,PreferPart,Level,Hair,Body,Cloth,LastLogin);
    }


}
