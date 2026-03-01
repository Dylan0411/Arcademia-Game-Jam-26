using UnityEngine;
using System.Collections.Generic;

public class NoteSpawner : MonoBehaviour
{
    public GameObject redNoteOBJ;
    public GameObject greenNoteOBJ;
    public GameObject blueNoteOBJ;

    private float songTimer;


    public AudioSource babyShark;
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
        chart.Add(new NoteData(3.8f, "red"));
        chart.Add(new NoteData(4f, "red"));
        chart.Add(new NoteData(4.3f, "green"));
        chart.Add(new NoteData(4.55f, "blue"));
        chart.Add(new NoteData(4.8f, "blue"));
        chart.Add(new NoteData(5.18f, "blue"));
        chart.Add(new NoteData(5.27f, "blue"));
        chart.Add(new NoteData(5.45f, "blue"));
        chart.Add(new NoteData(5.55f, "blue"));

        chart.Add(new NoteData(5.8f, "red"));
        chart.Add(new NoteData(6.05f, "red"));
        chart.Add(new NoteData(6.3f, "green"));
        chart.Add(new NoteData(6.6f, "blue"));
        chart.Add(new NoteData(6.9f, "blue"));
        chart.Add(new NoteData(7.2f, "blue"));
        chart.Add(new NoteData(7.36f, "blue"));
        chart.Add(new NoteData(7.66f, "blue"));
        chart.Add(new NoteData(7.8f, "blue"));

        chart.Add(new NoteData(8.1f, "red"));
        chart.Add(new NoteData(8.4f, "red"));
        chart.Add(new NoteData(8.73f, "green"));
        chart.Add(new NoteData(9.0f, "blue"));
        chart.Add(new NoteData(9.32f, "blue"));
        chart.Add(new NoteData(9.6f, "blue"));
        chart.Add(new NoteData(9.75f, "blue"));
        chart.Add(new NoteData(10.06f, "blue"));
        chart.Add(new NoteData(10.2f, "blue"));

        chart.Add(new NoteData(10.5f, "red"));
        chart.Add(new NoteData(10.8f, "red"));
        chart.Add(new NoteData(11.1f, "green"));
        chart.Add(new NoteData(11.43f, "blue"));
        chart.Add(new NoteData(11.7f, "blue"));
        chart.Add(new NoteData(12.0f, "blue"));
        chart.Add(new NoteData(12.16f, "blue"));
        chart.Add(new NoteData(12.45f, "blue"));
        chart.Add(new NoteData(12.6f, "blue"));

        chart.Add(new NoteData(12.9f, "red"));
        chart.Add(new NoteData(13.2f, "red"));
        chart.Add(new NoteData(13.5f, "green"));
        chart.Add(new NoteData(13.8f, "blue"));
        chart.Add(new NoteData(14.1f, "blue"));
        chart.Add(new NoteData(14.4f, "blue"));
        chart.Add(new NoteData(14.56f, "blue"));
        chart.Add(new NoteData(14.85f, "blue"));
        chart.Add(new NoteData(15f, "blue"));

        chart.Add(new NoteData(15.31f, "red"));
        chart.Add(new NoteData(15.61f, "red"));
        chart.Add(new NoteData(15.9f, "green"));
        chart.Add(new NoteData(16.2f, "blue"));
        chart.Add(new NoteData(16.5f, "blue"));
        chart.Add(new NoteData(16.8f, "blue"));
        chart.Add(new NoteData(16.96f, "blue"));
        chart.Add(new NoteData(17.26f, "blue"));
        chart.Add(new NoteData(17.41f, "blue"));

        chart.Add(new NoteData(17.7f, "red"));
        chart.Add(new NoteData(18.01f, "red"));
        chart.Add(new NoteData(18.33f, "green"));
        chart.Add(new NoteData(18.62f, "blue"));
        chart.Add(new NoteData(18.92f, "blue"));
        chart.Add(new NoteData(19.21f, "blue"));
        chart.Add(new NoteData(19.36f, "blue"));
        chart.Add(new NoteData(19.67f, "blue"));
        chart.Add(new NoteData(19.81f, "blue"));

        chart.Add(new NoteData(20.1f, "red"));
        chart.Add(new NoteData(20.41f, "red"));
        chart.Add(new NoteData(20.71f, "green"));
        chart.Add(new NoteData(21.02f, "blue"));
        chart.Add(new NoteData(21.32f, "blue"));
        chart.Add(new NoteData(21.62f, "blue"));
        chart.Add(new NoteData(21.76f, "blue"));
        chart.Add(new NoteData(22.06f, "blue"));
        chart.Add(new NoteData(22.21f, "blue"));

        chart.Add(new NoteData(22.5f, "red"));
        chart.Add(new NoteData(22.81f, "red"));
        chart.Add(new NoteData(23.19f, "green"));
        chart.Add(new NoteData(23.42f, "blue"));
        chart.Add(new NoteData(23.71f, "blue"));
        chart.Add(new NoteData(24.02f, "blue"));
        chart.Add(new NoteData(24.16f, "blue"));
        chart.Add(new NoteData(24.46f, "blue"));
        chart.Add(new NoteData(24.61f, "blue"));

        chart.Add(new NoteData(24.91f, "red"));
        chart.Add(new NoteData(25.22f, "red"));
        chart.Add(new NoteData(25.51f, "green"));
        chart.Add(new NoteData(25.81f, "blue"));
        chart.Add(new NoteData(26.12f, "blue"));
        chart.Add(new NoteData(26.41f, "blue"));
        chart.Add(new NoteData(26.56f, "blue"));
        chart.Add(new NoteData(26.86f, "blue"));
        chart.Add(new NoteData(27.01f, "blue"));

        chart.Add(new NoteData(27.3f, "red"));
        chart.Add(new NoteData(27.61f, "red"));
        chart.Add(new NoteData(27.91f, "green"));
        chart.Add(new NoteData(28.22f, "blue"));
        chart.Add(new NoteData(28.51f, "blue"));
        chart.Add(new NoteData(28.81f, "blue"));
        chart.Add(new NoteData(28.96f, "blue"));
        chart.Add(new NoteData(29.26f, "blue"));
        chart.Add(new NoteData(29.41f, "blue"));

        chart.Add(new NoteData(29.71f, "red"));
        chart.Add(new NoteData(30.01f, "red"));
        chart.Add(new NoteData(30.3f, "green"));
        chart.Add(new NoteData(30.63f, "blue"));
        chart.Add(new NoteData(30.92f, "blue"));
        chart.Add(new NoteData(31.21f, "blue"));
        chart.Add(new NoteData(31.36f, "blue"));
        chart.Add(new NoteData(31.66f, "blue"));
        chart.Add(new NoteData(31.81f, "blue"));

        chart.Add(new NoteData(32.1f, "red"));
        chart.Add(new NoteData(32.4f, "red"));
        chart.Add(new NoteData(32.72f, "green"));
        chart.Add(new NoteData(33f, "blue"));
        chart.Add(new NoteData(33.31f, "blue"));
        chart.Add(new NoteData(33.61f, "blue"));
        chart.Add(new NoteData(33.76f, "blue"));
        chart.Add(new NoteData(34.07f, "blue"));
        chart.Add(new NoteData(34.21f, "blue"));

        chart.Add(new NoteData(34.5f, "red"));
        chart.Add(new NoteData(34.8f, "red"));
        chart.Add(new NoteData(35.1f, "green"));
        chart.Add(new NoteData(35.41f, "blue"));
        chart.Add(new NoteData(35.72f, "blue"));
        chart.Add(new NoteData(36.01f, "blue"));
        chart.Add(new NoteData(36.16f, "blue"));
        chart.Add(new NoteData(36.45f, "blue"));
        chart.Add(new NoteData(36.61f, "blue"));

        chart.Add(new NoteData(36.91f, "red"));
        chart.Add(new NoteData(37.2f, "red"));
        chart.Add(new NoteData(37.51f, "green"));
        chart.Add(new NoteData(37.82f, "blue"));
        chart.Add(new NoteData(38.12f, "blue"));
        chart.Add(new NoteData(38.41f, "blue"));
        chart.Add(new NoteData(38.56f, "blue"));
        chart.Add(new NoteData(38.86f, "blue"));
        chart.Add(new NoteData(39.01f, "blue"));

        chart.Add(new NoteData(39.31f, "red"));
        chart.Add(new NoteData(39.61f, "red"));
        chart.Add(new NoteData(39.91f, "green"));
        chart.Add(new NoteData(40.21f, "blue"));
        chart.Add(new NoteData(40.51f, "blue"));
        chart.Add(new NoteData(40.81f, "blue"));
        chart.Add(new NoteData(40.96f, "blue"));
        chart.Add(new NoteData(41.26f, "blue"));
        chart.Add(new NoteData(41.41f, "blue"));

        chart.Add(new NoteData(41.7f, "red"));
        chart.Add(new NoteData(42.01f, "red"));
        chart.Add(new NoteData(42.3f, "green"));
        chart.Add(new NoteData(42.61f, "blue"));
        chart.Add(new NoteData(42.9f, "blue"));
        chart.Add(new NoteData(43.2f, "blue"));
        chart.Add(new NoteData(43.36f, "blue"));
        chart.Add(new NoteData(43.66f, "blue"));
        chart.Add(new NoteData(43.8f, "blue"));

        chart.Add(new NoteData(44.11f, "red"));
        chart.Add(new NoteData(44.4f, "red"));
        chart.Add(new NoteData(44.7f, "green"));
        chart.Add(new NoteData(45.01f, "blue"));
        chart.Add(new NoteData(45.32f, "blue"));
        chart.Add(new NoteData(45.6f, "blue"));
        chart.Add(new NoteData(45.76f, "blue"));
        chart.Add(new NoteData(46.05f, "blue"));
        chart.Add(new NoteData(46.2f, "blue"));

        chart.Add(new NoteData(46.51f, "red"));
        chart.Add(new NoteData(46.82f, "red"));
        chart.Add(new NoteData(47.14f, "green"));
        chart.Add(new NoteData(47.43f, "blue"));
        chart.Add(new NoteData(47.7f, "blue"));
        chart.Add(new NoteData(48.01f, "blue"));
        chart.Add(new NoteData(48.15f, "blue"));
        chart.Add(new NoteData(48.46f, "blue"));
        chart.Add(new NoteData(48.6f, "blue"));

        chart.Add(new NoteData(48.9f, "red"));
        chart.Add(new NoteData(49.21f, "red"));
        chart.Add(new NoteData(49.52f, "green"));
        chart.Add(new NoteData(49.8f, "blue"));
        chart.Add(new NoteData(50.12f, "blue"));
        chart.Add(new NoteData(50.4f, "blue"));
        chart.Add(new NoteData(50.55f, "blue"));
        chart.Add(new NoteData(50.86f, "blue"));
        chart.Add(new NoteData(51.0f, "blue"));

        chart.Add(new NoteData(51.3f, "red"));
        chart.Add(new NoteData(51.61f, "red"));
        chart.Add(new NoteData(51.92f, "green"));
        chart.Add(new NoteData(52.21f, "blue"));
        chart.Add(new NoteData(52.51f, "blue"));
        chart.Add(new NoteData(52.8f, "blue"));
        chart.Add(new NoteData(52.95f, "blue"));
        chart.Add(new NoteData(53.25f, "blue"));
        chart.Add(new NoteData(53.4f, "blue"));

        chart.Add(new NoteData(53.71f, "red"));
        chart.Add(new NoteData(54.02f, "red"));
        chart.Add(new NoteData(54.31f, "green"));
        chart.Add(new NoteData(54.61f, "blue"));
        chart.Add(new NoteData(54.91f, "blue"));
        chart.Add(new NoteData(55.21f, "blue"));
        chart.Add(new NoteData(55.36f, "blue"));
        chart.Add(new NoteData(55.67f, "blue"));
        chart.Add(new NoteData(55.8f, "blue"));

        chart.Add(new NoteData(56.1f, "red"));
        chart.Add(new NoteData(56.42f, "red"));
        chart.Add(new NoteData(56.73f, "green"));
        chart.Add(new NoteData(57.01f, "blue"));
        chart.Add(new NoteData(57.32f, "blue"));
        chart.Add(new NoteData(57.61f, "blue"));
        chart.Add(new NoteData(57.75f, "blue"));
        chart.Add(new NoteData(58.07f, "blue"));
        chart.Add(new NoteData(58.21f, "blue"));

        chart.Add(new NoteData(58.5f, "red"));
        chart.Add(new NoteData(58.81f, "red"));
        chart.Add(new NoteData(59.1f, "green"));
        chart.Add(new NoteData(59.4f, "blue"));
        chart.Add(new NoteData(59.72f, "blue"));
        chart.Add(new NoteData(60.0f, "blue"));
        chart.Add(new NoteData(60.17f, "blue"));
        chart.Add(new NoteData(60.46f, "blue"));
        chart.Add(new NoteData(60.61f, "blue"));

        Invoke("playTunes", 3f);
    }

    void Update()
    {
        songTimer += Time.deltaTime;

        foreach (var note in chart)
        {
            if (!note.spawned && songTimer >= note.hitTime)
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



    void playTunes()
    {
        babyShark.Play();
    }
}
