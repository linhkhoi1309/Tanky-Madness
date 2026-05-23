using UnityEngine;
using UnityEngine.InputSystem;

public enum TankControlType
{
    Pc,
    Mobile,
    Online
}

[System.Serializable]
public class PlayerSpawnConfig
{
    [SerializeField] private GameObject tankPrefab;
    [SerializeField] private TankControlType controlType;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private Vector3 fallbackSpawnPosition;
    [SerializeField] private InputActionReference moveAction;
    [SerializeField] private InputActionReference fireAction;
    [SerializeField] private InputActionReference aimAction;
    [SerializeField] private InputActionReference pointAction;
    [SerializeField] private Camera mainCamera;

    public GameObject TankPrefab => tankPrefab;
    public TankControlType ControlType => controlType;
    public Vector3 SpawnPosition => spawnPoint != null ? spawnPoint.position : fallbackSpawnPosition;
    public Quaternion SpawnRotation => spawnPoint != null ? spawnPoint.rotation : Quaternion.identity;
    public InputActionReference MoveAction => moveAction;
    public InputActionReference FireAction => fireAction;
    public InputActionReference AimAction => aimAction;
    public InputActionReference PointAction => pointAction;
    public Camera MainCamera => mainCamera;
}

public class PlayerJoinScript : MonoBehaviour
{
    [Header("Player Settings")]
    [SerializeField] private PlayerSpawnConfig[] players;

    private void Start()
    {
        SpawnPlayers();
    }

    public void SpawnPlayers()
    {
        if (players == null) return;

        foreach (PlayerSpawnConfig player in players)
        {
            SpawnPlayer(player);
        }
    }

    public GameObject SpawnPlayer(PlayerSpawnConfig player)
    {
        if (player == null)
        {
            Debug.LogError("Cannot spawn player: player config is missing.", this);
            return null;
        }

        if (player.TankPrefab == null)
        {
            Debug.LogError("Cannot spawn player: tank prefab is not assigned.", this);
            return null;
        }

        GameObject tank = Instantiate(player.TankPrefab, player.SpawnPosition, player.SpawnRotation);
        AddInput(tank, player);
        return tank;
    }

    private void AddInput(GameObject tank, PlayerSpawnConfig player)
    {
        if (tank.GetComponent<ITankInput>() != null)
        {
            Debug.LogWarning($"{tank.name} already has a tank input component. Spawn config input was not added.", tank);
            return;
        }

        switch (player.ControlType)
        {
            case TankControlType.Pc:
                tank.AddComponent<PcTankInput>().Configure(
                    player.MoveAction,
                    player.FireAction,
                    ResolveAimOrigin(tank),
                    player.MainCamera != null ? player.MainCamera : Camera.main);
                break;

            case TankControlType.Mobile:
                tank.AddComponent<MobileTankInput>().Configure(player.MoveAction, player.AimAction);
                break;

            case TankControlType.Online:
                tank.AddComponent<OnlineTankInput>();
                break;

            default:
                Debug.LogError($"Unsupported tank control type {player.ControlType}.", tank);
                break;
        }
    }

    private Transform ResolveAimOrigin(GameObject tank)
    {
        TankAimController aim = tank.GetComponentInChildren<TankAimController>();
        if (aim != null)
        {
            return aim.transform;
        }

        Debug.LogWarning($"{tank.name} has no TankAimController. Tank transform will be used as aim origin.", tank);
        return tank.transform;
    }

#if UNITY_EDITOR
    private void OnValidate()
    {
        if (players == null) return;

        for (int i = 0; i < players.Length; i++)
        {
            ValidatePlayerConfig(players[i], i);
        }
    }

    private void ValidatePlayerConfig(PlayerSpawnConfig player, int index)
    {
        if (player == null)
        {
            Debug.LogWarning($"Player config {index} is missing.", this);
            return;
        }

        GameObject prefab = player.TankPrefab;
        if (prefab == null)
        {
            Debug.LogWarning($"Player config {index} has no tank prefab assigned.", this);
            return;
        }

        if (prefab.GetComponent<TankController>() == null)
        {
            Debug.LogWarning($"{prefab.name} does not have a TankController.", prefab);
        }

        if (prefab.GetComponentInChildren<TankAimController>() == null)
        {
            Debug.LogWarning($"{prefab.name} does not have a TankAimController in its hierarchy.", prefab);
        }

        ValidateInputConfig(player, index);
    }

    private void ValidateInputConfig(PlayerSpawnConfig player, int index)
    {
        switch (player.ControlType)
        {
            case TankControlType.Pc:
                if (player.MoveAction == null) Debug.LogWarning($"Player config {index} has no move action.", this);
                if (player.FireAction == null) Debug.LogWarning($"Player config {index} has no fire action.", this);
                if (player.MainCamera == null) Debug.LogWarning($"Player config {index} has no camera; Camera.main will be used.", this);
                break;

            case TankControlType.Mobile:
                if (player.MoveAction == null) Debug.LogWarning($"Player config {index} has no move action.", this);
                if (player.AimAction == null) Debug.LogWarning($"Player config {index} has no aim action.", this);
                break;
        }
    }
#endif
}
