using System.Collections.Generic;
using SQLite4Unity3d;
using UnityEngine;

public class Pose
{

    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }
    public string Name { get; set; }
    public string Path { get; set; }
    public int CheckPoint { get; set; }
    public string Part { get; set; }
    public string Description { get; set; }

    public override string ToString()
    {
        return string.Format("[Pose: Id={0}, Name={1}, Path={2}, CheckPoint={3}, Part={4}, Description={5}",
            Id, Name , Path , CheckPoint , Part , Description);
    }


}
