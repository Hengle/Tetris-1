using UnityEngine;
using System.Collections;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Assets.Scripts;
using com.tinylabproductions.TLPLib.Data;
using com.tinylabproductions.TLPLib.Functional;

public class LeaderboardsDataManager {

    public static void Save(Leaderboards data){
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/leaderboards.data");
        bf.Serialize(file, data);
    }

    public static Option<Leaderboards> Load()
    {
        var leadearboards = Option<Leaderboards>.None;

        if (File.Exists(Application.persistentDataPath + "/leaderboards.data"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath 
                + "/leaderboards.data", FileMode.Open);

            Try<Leaderboards> loadedLeaderboard = 
                F.doTry(() => (Leaderboards)bf.Deserialize(file));

            file.Close();

            if (loadedLeaderboard.isSuccess)
                leadearboards = loadedLeaderboard.toOption;
        }

        return leadearboards;
    }
}
