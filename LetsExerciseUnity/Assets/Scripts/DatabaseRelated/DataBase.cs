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
        // Check if files exist in Application.persistentDataPath
        string userDbPath = Path.Combine(Application.persistentDataPath, userDatabaseName);
        string poseDbPath = Path.Combine(Application.persistentDataPath, poseDatabaseName);

        if (!File.Exists(userDbPath))
        {
            CopyDatabaseFromStreamingAssets(userDatabaseName, userDbPath);
        }

        if (!File.Exists(poseDbPath))
        {
            CopyDatabaseFromStreamingAssets(poseDatabaseName, poseDbPath);
        }
#endif

        _userConnection = new SQLiteConnection(userDbPath, SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.Create);
        _poseConnection = new SQLiteConnection(poseDbPath, SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.Create);
        _poseSetConnection = new SQLiteConnection(poseSetDbPath, SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.Create);
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
