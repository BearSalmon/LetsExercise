using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoseSetService
{
    DataBase dataBase;
    public PoseSetService(DataBase db)
    {
        dataBase = db;
    }

    public void CreatePoseSetTable()
    {
        dataBase.GetPoseSetConnection().DropTable<PoseSet>();
        dataBase.GetPoseSetConnection().CreateTable<PoseSet>();
    }

    public int AddPoseSet(PoseSet poseSet)
    {
        return dataBase.GetPoseSetConnection().Insert(poseSet);
    }

    public IEnumerable<PoseSet> GetPoseSets()
    {
        return dataBase.GetPoseSetConnection().Table<PoseSet>();
    }

    public PoseSet GetPoseSetByID(int id)
    {
        return dataBase.GetPoseSetConnection().Table<PoseSet>().Where(x => x.Id == id).FirstOrDefault();
    }

    public PoseSet GetPoseSetByName(string name)
    {
        return dataBase.GetPoseSetConnection().Table<PoseSet>().Where(x => x.PoseSetName == name).FirstOrDefault();
    }

    public int UpdatePoseSet(PoseSet poseSet)
    {
        return dataBase.GetPoseSetConnection().Update(poseSet);
    }

    public int CountPoseSets()
    {
        return dataBase.GetPoseSetConnection().Table<PoseSet>().Count();
    }
}
