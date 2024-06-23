using UnityEngine;

public class BossRoomTrigger : MonoBehaviour
{
    public MusicTrackSO bossMusicTrack;  // Assign this in the Inspector

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))  // Assuming your player has the tag "Player"
        {
            MusicManager.Instance.EnterBossRoom(bossMusicTrack);
            Debug.Log("Boss Room Entered, Music Changed");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))  // Assuming your player has the tag "Player"
        {
            MusicManager.Instance.ExitBossRoom();
            Debug.Log("Boss Room Exited, Music Reverted");
        }
    }
}
