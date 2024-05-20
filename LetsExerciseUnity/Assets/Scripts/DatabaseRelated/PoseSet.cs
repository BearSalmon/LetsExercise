using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SQLite4Unity3d;

public class PoseSet
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }
    public string PoseSetName { get; set; }
    public string Part { get; set; }
    public int NumberOfGesture { get; set; }
    public string TrainPoseSet { get; set; }
    public float Calories { get; set; }

}
