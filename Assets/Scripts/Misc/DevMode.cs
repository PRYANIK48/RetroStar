using UnityEngine;

public class DevMode : MonoBehaviour
{
    [Header("Developer Mode")]
    [SerializeField] private bool isDev = false;
    [SerializeField] private Transform player;
    [SerializeField] private Transform[] rooms;

    private int roomID = 0;

    private void Update()
    {
        if (!isDev || rooms == null || rooms.Length == 0 || player == null) return;

        if (Input.GetKeyDown(KeyCode.PageUp))
        {
            ChangeRoom(+1);
        }
        else if (Input.GetKeyDown(KeyCode.PageDown))
        {
            ChangeRoom(-1);
        }
        else if (Input.GetKeyDown(KeyCode.End))
        {
            TeleportToRoom();
            Debug.Log($"[DevMode] Player manually reset to room {roomID} at position {player.position}");
        }
    }

    private void ChangeRoom(int direction)
    {
        int newID = Mathf.Clamp(roomID + direction, 0, rooms.Length - 1);
        if (newID != roomID)
        {
            roomID = newID;
            TeleportToRoom();
            Debug.Log($"[DevMode] Moved to room {roomID}");
        }
    }

    private void TeleportToRoom()
    {
        if (rooms[roomID] != null)
            player.position = rooms[roomID].position;
    }
}
