using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardCell : MonoBehaviour
{
    public int x, y;
    public GameObject black;
    public GameObject white;
    [HideInInspector] public GameObject currentPiece; // �� �κ��� public���� HideInInspector�� ����
    private Vector2 offset = new Vector2(0.5f, -0.5f); // �ٵ����� ������ �߽��� ���߱� ���� ������ ����

    public void PlacePiece(bool isBlack)
    {
        if (currentPiece != null) Destroy(currentPiece);
        Vector2 position = (Vector2)transform.position + offset;
        currentPiece = Instantiate(isBlack ? black : white, position, Quaternion.identity, transform);
    }

    public bool HasPiece()
    {
        return currentPiece != null;
    }

    public bool IsBlack()
    {
        if (currentPiece == null) return false;
        return currentPiece.GetComponent<SpriteRenderer>().color == Color.black;
    }
}
