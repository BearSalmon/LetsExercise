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
        TestAddUsers();
        TestAddPoses();
        TestAddPoseSets();
        Debug.Log("Game start!");
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
            Duration = 100,
            Calories = 100,
            Weight = "63,65,",
            Height = 170,
            Recommendation = "1,0,0,0,0,1,0,0,",
            HasUnfinishedPlan = false,
        };
        int pk = userService.AddUser(user);

        user = new User
        {
            Name = "User2",
            Gender = "Boy",
            Hair = "000000FF",
            Body = "FFDBC6FF",
            Cloth = "CAEE8AFF",
            Weight = "63,65,",
            LastLogin = currentTimeString,
            Duration = 100,
            Calories = 100,
            Height = 170,
            Recommendation = "1,0,0,0,0,1,0,0,",
            HasUnfinishedPlan = false,
        };
        pk = userService.AddUser(user);

        user = new User
        {
            Name = "User3",
            Gender = "Boy",
            Hair = "000000FF",
            Body = "FFDBC6FF",
            Cloth = "CAEE8AFF",
            Weight = "63,65,",
            LastLogin = currentTimeString,
            Duration = 100,
            Calories = 100,
            Height = 170,
            Recommendation = "1,0,0,0,0,1,0,0,",
            HasUnfinishedPlan = false,
        };
        pk = userService.AddUser(user);
        
    }

    // for test only
    public void TestAddPoses()
    {

        Pose pose = new Pose
        {
            Name = "arm1",
            Path = "/PoseDataset/arms/arm1.txt",
            CheckPoint = 4,
            Part = "arms",
            Description = ""
        };

        poseService.AddPose(pose);

        pose = new Pose
        {
            Name = "arm2",
            Path = "/PoseDataset/arms/arm2.txt",
            CheckPoint = 4,
            Part = "arms",
            Description = ""

        };

        
        poseService.AddPose(pose);

        pose = new Pose
        {
            Name = "arm3",
            Path = "/PoseDataset/arms/arm3.txt",
            CheckPoint = 4,
            Part = "arms",
            Description = ""

        };


        poseService.AddPose(pose);

        pose = new Pose
        {
            Name = "arm4",
            Path = "/PoseDataset/arms/arm4.txt",
            CheckPoint = 4,
            Part = "arms",
            Description = ""

        };
        poseService.AddPose(pose);


        pose = new Pose
        {
            Name = "arm5",
            Path = "/PoseDataset/arms/arm5.txt",
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
            NumberOfGesture = 5,
            Part = "arms abs",
            TrainPoseSet = "arm1,arm2,arm3,arm4,arm5,",
        };
        poseSetService.AddPoseSet(poseSet);

        poseSet = new PoseSet
        {
            PoseSetName = "how to become a cute salmon",
            Calories = 500f,
            NumberOfGesture = 5,
            Part = "body",
            TrainPoseSet = "arm1,arm2,arm3,arm4,arm5,",
        };

        poseSetService.AddPoseSet(poseSet);

        poseSet = new PoseSet
        {
            PoseSetName = "hi hi",
            Calories = 300f,
            NumberOfGesture = 5,
            Part = "body",
            TrainPoseSet = "arm1,arm2,arm3,arm4,arm5,",
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
            Hair = "000000FF",
            Body = "FFDBC6FF",
            Cloth = "CAEE8AFF",
            Recommendation = "0,0,0,0,0,0,0,0,",
            HasUnfinishedPlan = false
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
