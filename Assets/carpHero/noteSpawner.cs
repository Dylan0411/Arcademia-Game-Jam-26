using UnityEngine;
using System.Collections.Generic;

public class NoteSpawner : MonoBehaviour
{
    public GameObject redNoteOBJ;
    public GameObject greenNoteOBJ;
    public GameObject blueNoteOBJ;

    public float noteBuffer = 2.82f; // travel time
    private float songTimer;

    class NoteData
    {
        public float hitTime;
        public string lane;
        public bool spawned;

        public NoteData(float time, string laneName)
        {
            hitTime = time;
            lane = laneName;
            spawned = false;
        }
    }

    List<NoteData> chart = new List<NoteData>();

    void Start()
    {
        // These are HIT TIMES (when player should press)
        chart.Add(new NoteData(5f, "red"));
        chart.Add(new NoteData(6f, "green"));
        chart.Add(new NoteData(7f, "blue"));
        chart.Add(new NoteData(8f, "red"));
        chart.Add(new NoteData(9f, "green"));
        chart.Add(new NoteData(10f, "blue"));
    }

    void Update()
    {
        songTimer += Time.deltaTime;

        foreach (var note in chart)
        {
            float spawnTime = note.hitTime - noteBuffer;

            if (!note.spawned && songTimer >= spawnTime)
            {
                SpawnNote(note.lane);
                note.spawned = true;
            }
        }
    }

    void SpawnNote(string lane)
    {
        switch (lane)
        {
            case "red":
                Instantiate(redNoteOBJ);
                break;
            case "green":
                Instantiate(greenNoteOBJ);
                break;
            case "blue":
                Instantiate(blueNoteOBJ);
                break;
        }
    }
}