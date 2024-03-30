using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DBUtils : MonoBehaviour
{
    UserService userService;
    PoseService poseService;
    PoseSetService poseSetService;
    DataBase dataBase;

    public string nowPlayer;

    public WholeSampleSceneManager wholeSampleSceneManager;

    // Start is called before the first frame update
    void Start()
    {
        dataBase = new DataBase();
        userService = new UserService(dataBase);
        poseService = new PoseService(dataBase);
        poseSetService = new PoseSetService(dataBase);

        // Create tables
        userService.CreateUserTable();
        poseService.CreatePoseTable();
        poseSetService.CreatePoseSetTable();

        // Add test data
        //TestAddUsers();
        TestAddPoses();
        //TestAddPoseSets();
        Debug.Log("Game start!");
    }

    IEnumerator SetupDatabase()
    {
        // Set up the database
        dataBase = new DataBase();
        userService = new UserService(dataBase);
        poseService = new PoseService(dataBase);
        poseSetService = new PoseSetService(dataBase);

        // Create tables
        userService.CreateUserTable();
        poseService.CreatePoseTable();
        poseSetService.CreatePoseSetTable();

        // Add test data
        //TestAddUsers();
        TestAddPoses();
        //TestAddPoseSets();

        yield return null;
    }

    // for test only
    public void TestAddUsers()
    {
        DateTime currentTime = DateTime.Now;
        string currentTimeString = currentTime.ToString("yyyy-MM-dd HH:mm:ss");

        User user = new User
        {
            Name = "User1",
            Gender = "Girl",
            Hair = "000000FF",
            Body = "FFDBC6FF",
            Cloth = "CAEE8AFF",
            LastLogin = currentTimeString,
        };
        int pk = userService.AddUser(user);

        user = new User
        {
            Name = "User2",
            Gender = "Boy",
            Hair = "000000FF",
            Body = "FFDBC6FF",
            Cloth = "CAEE8AFF",
            LastLogin = currentTimeString,
        };
        pk = userService.AddUser(user);

        user = new User
        {
            Name = "User3",
            Gender = "Boy",
            Hair = "000000FF",
            Body = "FFDBC6FF",
            Cloth = "CAEE8AFF",
            LastLogin = currentTimeString,
        };
        pk = userService.AddUser(user);
        
    }

    // for test only
    public void TestAddPoses()
    {

        Pose pose = new Pose
        {
            Name = "arm1",
            Path = "Assets/PoseDataset/arms/arm1.txt",
            CheckPoint = 4,
            Part = "arms",
            Description = ""
        };

        poseService.AddPose(pose);

        pose = new Pose
        {
            Name = "arm2",
            Path = "Assets/PoseDataset/arms/arm2.txt",
            CheckPoint = 4,
            Part = "arms",
            Description = ""

        };

        
        poseService.AddPose(pose);

        pose = new Pose
        {
            Name = "arm3",
            Path = "Assets/PoseDataset/arms/arm3.txt",
            CheckPoint = 4,
            Part = "arms",
            Description = ""

        };


        poseService.AddPose(pose);

        pose = new Pose
        {
            Name = "arm4",
            Path = "Assets/PoseDataset/arms/arm4.txt",
            CheckPoint = 4,
            Part = "arms",
            Description = ""

        };
        poseService.AddPose(pose);


        pose = new Pose
        {
            Name = "arm5",
            Path = "Assets/PoseDataset/arms/arm5.txt",
            CheckPoint = 4,
            Part = "arms",
            Description = ""

        };
        poseService.AddPose(pose);


    }

    public void TestAddPoseSets()
    {

        PoseSet poseSet = new PoseSet
        {
            PoseSetName = "hello",
            Calories = 130f,
            NumberOfGesture = 10,
            Part = "arms abs",
            Duration = 100,

        };
        poseSetService.AddPoseSet(poseSet);

        poseSet = new PoseSet
        {
            PoseSetName = "how to become a cute salmon",
            Calories = 500f,
            NumberOfGesture = 7,
            Part = "body",
            Duration = 70,

        };

        poseSetService.AddPoseSet(poseSet);

        poseSet = new PoseSet
        {
            PoseSetName = "hi hi",
            Calories = 300f,
            NumberOfGesture = 8,
            Part = "body",
            Duration = 200,

        };

        poseSetService.AddPoseSet(poseSet);



    }


    /////////////////////////////////// User API ///////////////////////////////////////////////////////////////////

    private void ToConsole(IEnumerable<User> users)
    {
        foreach (var user in users)
        {
            Debug.Log(user.ToString());
        }
    }

    public void AddUser()
    {
        int number = userService.CountUsers();
        Debug.Log(number);
        number += 1;
        nowPlayer = "User"+number.ToString();

        User user = new User
        {
            Name = "User"+number.ToString(),
        };
        int pk = userService.AddUser(user);
        Debug.Log("Primary key = " + pk);
        
    }

    public IEnumerable<User> GetUsers()
    {
        var users = userService.GetUsers();
        ToConsole(users);
        return users;
    }

    public User GetUserByName(string name)
    {
        return userService.GetUserByName(name);
    }

    public void UpdateUser(User user)
    {
        //Debug.Log(player.Gender);
        userService.UpdateUser(user);
    }

    public int CountUsers()
    {
        return userService.CountUsers();
    }
    /////////////////////////////////// User API ///////////////////////////////////////////////////////////////////



    ////////////////////////////////////// Pose API ////////////////////////////////////////////////////////////////
    
    private void ToConsole2(IEnumerable<Pose> poses)
    {
        foreach (var pose in poses)
        {
            Debug.Log(pose.ToString());
        }
    }

    public IEnumerable<Pose> GetPose()
    {
        var poses = poseService.GetPoses();
        return poses;
    }

    public Pose GetPoseByName(string name)
    {
        return poseService.GetPoseByName(name);
    }

    public void UpdatePose(Pose pose)
    {
        poseService.UpdatePose(pose);
    }

    public int CountPoses()
    {
        return poseService.CountPoses();
    }


    public IEnumerable<Pose> GetPoseByPart(string part)
    {
        IEnumerable<Pose> poses = poseService.GetPoseByPart(part);
        ToConsole2(poses);
        return poses;
    }

    ////////////////////////////////////// Pose API ////////////////////////////////////////////////////////////////


    ////////////////////////////////////// PoseSet API /////////////////////////////////////////////////////////////
    private void ToConsole3(IEnumerable<PoseSet> poseSets)
    {
        foreach (var poseSet in poseSets)
        {
            Debug.Log(poseSet.ToString());
        }
    }

    public IEnumerable<PoseSet> GetPoseSet()
    {
        var poses = poseSetService.GetPoseSets();
        return poses;
    }

    public PoseSet GetPoseSetById(int id)
    {
        return poseSetService.GetPoseSetByID(id);
    }

    public PoseSet GetPoseSetByName(string name)
    {
        return poseSetService.GetPoseSetByName(name);
    }

    public void UpdatePoseSet(PoseSet poseSet)
    {
        poseSetService.UpdatePoseSet(poseSet);
    }

    public int CountPoseSets()
    {
        return poseSetService.CountPoseSets();
    }

    




}
