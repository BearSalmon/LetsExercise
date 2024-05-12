using SQLite4Unity3d;
using UnityEngine;
using System.IO;
#if !UNITY_EDITOR
using System.Collections;
using System.IO;
#endif
using System.Collections.Generic;

public class DataBase
{
    private SQLiteConnection _userConnection;
    private SQLiteConnection _poseConnection;
    private SQLiteConnection _poseSetConnection;

    public DataBase()
    {
        InitializeConnections();
    }

    private void InitializeConnections()
    {
        string userDatabaseName = "User.db";
        string poseDatabaseName = "Pose.db";
        string poseSetDatabaseName = "PoseSet.db";

#if UNITY_EDITOR
        string userDbPath = string.Format(@"Assets/StreamingAssets/{0}", userDatabaseName);
        string poseDbPath = string.Format(@"Assets/StreamingAssets/{0}", poseDatabaseName);
        string poseSetDbPath = string.Format(@"Assets/StreamingAssets/{0}", poseSetDatabaseName);
       

#else
        // comment under this (bug)
        // Check if files exist in Application.persistentDataPath
        string userDbPath = Application.streamingAssetsPath + "/" + userDatabaseName;
        string poseDbPath = Application.streamingAssetsPath + "/" + poseDatabaseName;
        string poseSetDbPath = Application.streamingAssetsPath + "/" + poseSetDatabaseName;


        if (!File.Exists(userDbPath))
        {
            //CopyDatabaseFromStreamingAssets(userDatabaseName, userDbPath);
            Debug.Log(userDbPath);
        }

        if (!File.Exists(poseDbPath))
        {
            //CopyDatabaseFromStreamingAssets(poseDatabaseName, poseDbPath);
            Debug.Log(poseDbPath);
        }

        if (!File.Exists(poseSetDbPath))
        {
            //CopyDatabaseFromStreamingAssets(poseDatabaseName, poseSetDbPath);
            Debug.Log(poseSetDbPath);
        }
#endif

        _userConnection = new SQLiteConnection(userDbPath, SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.Create);
        _poseConnection = new SQLiteConnection(poseDbPath, SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.Create);
        _poseSetConnection = new SQLiteConnection(poseSetDbPath, SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.Create);
        

        // end of comment (bug)
    }

    public SQLiteConnection GetUserConnection()
    {
        return _userConnection;
    }

    public SQLiteConnection GetPoseConnection()
    {
        return _poseConnection;
    }

    public SQLiteConnection GetPoseSetConnection()
    {
        return _poseSetConnection;
    }
}
