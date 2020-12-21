using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicSelector : MonoBehaviour
{
    public AudioSource track1;
    public AudioSource track2;
    public AudioSource track3;
    public int selector = 1;
    public AudioSource currentSong;
    public AudioSource crunch;
    public AudioSource woosh;
    void Start()
    {
        SelectNextTrack();
    }
    // Update is called once per frame
    void Update()
    {
        //if left control pressed, go to next song
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            SelectNextTrack();
        }

        //play next song if no track is playing
        if (track1.isPlaying == false && track2.isPlaying == false && track3.isPlaying == false)
        {
            SelectNextTrack();
        }
    }

    public void startPlayingWoosh()
    {
        if (woosh.isPlaying == false)
        {
            woosh.Play();
        }
    }
    public void stopWoosh()
    {
        woosh.Stop();
    }
    public void playCrunch()
    {
        crunch.Play();
    }

    //choose the next track to play
    private void SelectNextTrack()
    {
        StopCurrentTrack();

        if(selector == 1)
        {
            track1.Play();
            currentSong = track1;
            selector++;
        }
        else if (selector == 2)
        {
            track2.Play();
            currentSong = track2;
            selector++;
        }
        else if (selector == 3)
        {
            track3.Play();
            currentSong = track3;
            selector = 1;
            //set selector back to first track
        }
    }
    //stop the playing track
    private void StopCurrentTrack()
    {
            track1.Stop();
            track2.Stop();
            track3.Stop();
    }
}
