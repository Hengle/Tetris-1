using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using com.tinylabproductions.TLPLib.Extensions;
using com.tinylabproductions.TLPLib.Functional;
using Smooth.Collections;

public class Spawner : MonoBehaviour
{
    // Groups
    List<Group> groups;
    public void Initialize(List<Group> groups) {
        this.groups = groups;
    }

    public Group spawnNext(MatchController controller, IScore score)
    {
        var group = Instantiate(groups.Random().get);
        group.transform.position = transform.position;

        group.Initialize(
            controller,
            score);

        return group;
    }
}