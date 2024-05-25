using System.Collections.Generic;
using SQLite4Unity3d;
using UnityEngine;

public class Record
{

    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }
    public string Name { get; set; }
    public string Date { get; set; }
    public string Parts { get; set; }
    public int Duration { get; set; }
    public string Mood { get; set; }

    public override string ToString()
    {
        return string.Format("[Player: Id={0}, Name={1},  ",
            Id, Name);
    }


}
