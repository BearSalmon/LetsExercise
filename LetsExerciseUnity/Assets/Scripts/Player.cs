using System.Collections.Generic;
using SQLite4Unity3d;
using UnityEngine;

public class Player
{

    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }
    public string Name { get; set; }
    public int Age { get; set; }
    public string Weight { get; set; } // array of int 
    public int Height { get; set; }
    public string Gender { get; set; }
    public string PreferPart { get; set; }
    public string Hair { get; set; }
    public string Body { get; set; }
    public string Cloth { get; set; }
    public string LastLogin { get; set; }

    public override string ToString()
    {
        return string.Format("[Player: Id={0}, Name={1},  Age={2}, Weight={3}, Height={4}, Gender={5}, PreferPart={6}, Hair={7}, Body={8}, Cloth={9}, LastLogin={10}]",
            Id, Name, Age , Weight,Height,Gender,PreferPart,Hair,Body,Cloth,LastLogin);
    }


}
