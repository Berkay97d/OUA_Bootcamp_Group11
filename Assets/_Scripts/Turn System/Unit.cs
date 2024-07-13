using System.Collections;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public Team team;
    private int _playOrder;

    public void TakeTurn()
    {
        Debug.Log($"{name} from {team} is taking its turn.");
        GetComponent<MeshRenderer>().material.color = Color.red;
        StartCoroutine(EndTurnAfterDelay());
    }

    private IEnumerator EndTurnAfterDelay()
    {
        yield return new WaitForSeconds(2f);
        if (team == Team.White)
        {
            GetComponent<MeshRenderer>().material.color = Color.white;
        }
        else
        {
            GetComponent<MeshRenderer>().material.color = Color.black;
        }
        TurnController.SharedInstance.EndTurn();
    }

    public void SetPlayOrder(int order)
    {
        _playOrder = order;
    }
}
