using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerJoinScript : MonoBehaviour
{
    [Header("Player Settings")]

    [Tooltip("Array of player prefabs to instantiate")]
    public GameObject[] players;

    [Tooltip("Spawn positions for each player")]
    public Vector3[] spawnPositions;
    void Start()
    {
        PlayerInput.Instantiate(players[0], controlScheme: "WASD_Scheme", pairWithDevice: Keyboard.current);
        PlayerInput.Instantiate(players[1], controlScheme: "Arrows_Scheme", pairWithDevice: Keyboard.current);
        PlayerInput.Instantiate(players[2], controlScheme: "Mouse_Scheme", pairWithDevice: Mouse.current);
        for (int i = 0; i < PlayerInput.all.Count; i++)
        {
            PlayerInput.all[i].gameObject.transform.position = spawnPositions[i];
        }
    }

#if UNITY_EDITOR
    private void OnValidate()
    {
        if (players.Length != spawnPositions.Length)
        {
            Debug.LogWarning("Players array length and SpawnPositions array length must be the same.");
        }
    }
#endif
}
