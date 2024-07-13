using UnityEngine;

public class DemoSpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject _unitPrefab;

    public Unit SpawnKing()
    {
        var spawnPosition = new Vector3(0, 0, 50);
        var kingGameObject = Instantiate(_unitPrefab, spawnPosition, Quaternion.identity);
        var kingUnit = kingGameObject.GetComponent<Unit>();
        kingUnit.team = Team.White;
        kingUnit.SetPlayOrder(0);
        return kingUnit;
    }

    public Unit SpawnBlackUnit(int playOrder)
    {
        var spawnPosition = new Vector3(playOrder, 0, 45);
        var blackGameObject = Instantiate(_unitPrefab, spawnPosition, Quaternion.identity);
        var blackUnit = blackGameObject.GetComponent<Unit>();
        blackUnit.team = Team.Black;
        blackUnit.SetPlayOrder(playOrder);
        return blackUnit;
    }
}
