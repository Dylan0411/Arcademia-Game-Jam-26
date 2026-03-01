
using UnityEngine;


public class blueNote1 : MonoBehaviour
{

    public GameObject noteStartPos;
    public GameObject noteEndPos;

    public GameObject note;

    public float moveSpeed = 1f;

    public GameObject noteGoal;

    public GameObject playerBoard;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        noteStartPos = GameObject.FindWithTag("blueNoteStartPos1");
        noteEndPos = GameObject.FindWithTag("blueNoteEndPos1");
        noteGoal = GameObject.FindWithTag("blueNoteGoal");

        //note becomes child of notestartpos
        note.transform.SetParent(noteStartPos.transform);

        note.transform.position = noteStartPos.transform.position;
        note.transform.rotation = noteStartPos.transform.rotation;

        //despawn note after 10 seconds;
        Invoke("destroyNote", 10f);

        playerBoard = GameObject.FindWithTag("P1Board");

    }






    // Update is called once per frame
    void Update()
    {
        //constantly move the end position
        note.transform.position = Vector3.MoveTowards(note.transform.position, noteEndPos.transform.position, moveSpeed * Time.deltaTime);



    }





    public void destroyNote()
    {
        Destroy(note);
    }



    //attach this to note prefab (instantiated at times that match the song within another script)

    //if this note collides with led and the led's valid bool is true then success, else fail
    //when this note reaches point b then destroy it
    //have ui slider to show health (starts at 50%, slowly goes up over time with successful notes and drops with failed notes). when reaches 0 player fails
    //at end of song display player scores. --> then automatically close the game (w/ title card - fragments of memory style)



}
