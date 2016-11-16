using UnityEngine;
using System.Collections;
using com.tinylabproductions.TLPLib.Extensions;
using com.tinylabproductions.TLPLib.Functional;

public class Spawner : MonoBehaviour
{
    // Groups
    public GameObject[] groups;

    public void Initialize(GameObject[] groups) {
        this.groups = groups;
    }

    public GameObject spawnNext(MatchController controller, IScore score) {

        int i = Random.Range(
            0, groups.Length
            );

        var group = (GameObject)Instantiate(
            groups[i],
            transform.position,
            Quaternion.identity
            );

        group.GetComponent<Group>()
            .Initialize(
                controller, 
                score
                );

        return group;
    }
}