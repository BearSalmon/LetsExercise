using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoseService
{
    DataBase dataBase;
    public PoseService(DataBase db)
    {
        dataBase = db;
    }

    public void CreatePoseTable()
    {
        dataBase.GetPoseConnection().DropTable<Pose>();
        dataBase.GetPoseConnection().CreateTable<Pose>();
    }

    // add new poses
    public int AddPose(Pose pose)
    {
        return dataBase.GetPoseConnection().Insert(pose);
    }

    // get all poses
    public IEnumerable<Pose> GetPoses()
    {
        return dataBase.GetPoseConnection().Table<Pose>();
    }


    // get pose by name
    public Pose GetPoseByName(string name)
    {
        return dataBase.GetPoseConnection().Table<Pose>().Where(x => x.Name == name).FirstOrDefault();
    }

    public IEnumerable<Pose> GetPoseByPart(string part)
    {
        return dataBase.GetPoseConnection().Table<Pose>().Where(x => x.Part.Contains(part));
    }


    // update pose by name
    public int UpdatePose(Pose pose)
    {
        return dataBase.GetPoseConnection().Update(pose);
    }

    // Count pose number
    public int CountPoses()
    {
        return dataBase.GetPoseConnection().Table<Pose>().Count();
    }
}
