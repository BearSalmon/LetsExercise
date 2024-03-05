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

    public override string ToString()
    {
        return string.Format("[Person: Id={0}, Name={1},  Surname={2}, Age={3}]", Id, Name, Age , Height);
    }


}
