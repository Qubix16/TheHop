using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformGenerator : MonoBehaviour
{
    public static PlatformGenerator platformGenerator;
    public List<PlatformSet> setsPrefabs = new List<PlatformSet>();
    private List<PlatformSet> setsOfPlatforms = new List<PlatformSet>();
    [SerializeField] private Transform startPoint;
    [SerializeField] private Transform deathPoint;
    void Awake()
    {
        platformGenerator = this;
        AddNewSet();
        AddNewSet();
        AddNewSet();
        AddNewSet();
        AddNewSet();
    }

    private void AddNewSet()
    {
        PlatformSet platformSet;
        int randomIndex = Random.Range(0, setsPrefabs.Count);
        platformSet = (PlatformSet)Instantiate(setsPrefabs[randomIndex]);
        platformSet.transform.SetParent(this.transform, false);
        if (setsOfPlatforms.Count < 1)
        {
            platformSet.startPoint.position = startPoint.position;
        }
        else
        {
            platformSet.startPoint.position = new Vector2(setsOfPlatforms[setsOfPlatforms.Count - 1].exitPoint.position.x, setsOfPlatforms[setsOfPlatforms.Count - 1].exitPoint.position.y);
        }
        setsOfPlatforms.Add(platformSet);
    }

    public void RemoveOldestSet()
    {
        if (setsOfPlatforms.Count > 1)
        {
            PlatformSet oldestPiece = setsOfPlatforms[0];
            setsOfPlatforms.RemoveAt(0);
            Destroy(oldestPiece.gameObject);
        }
    }
    void FixedUpdate()
    {
        if (GameController.gameController.GetGameState() != GameState.GS_GAME_OVER)
        {
            if (setsOfPlatforms[0].startPoint.position.y < deathPoint.position.y - 15)
            {
                AddNewSet();
                RemoveOldestSet();
            }
        }

    }
}
