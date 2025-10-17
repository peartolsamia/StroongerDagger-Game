using UnityEngine;

public class OneArmSculptureAI : MonoBehaviour
{
    public bool isAggro = false;

    public void StartChasing()
    {
        isAggro = true;
        Debug.Log("Oyuncu algýlandý, saldýrý moduna geçiliyor!");
    }
}
