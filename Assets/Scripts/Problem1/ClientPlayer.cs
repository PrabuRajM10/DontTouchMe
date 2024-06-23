using System;
using UnityEngine;

public class ClientPlayer : MonoBehaviour
{
    [SerializeField] private CharacterController characterController;
    [SerializeField] private float speed ;
    private Vector3 _moveDirection;
    [SerializeField] private float zOffset = 24f;

    private void OnValidate()
    {
        if (characterController == null) characterController = GetComponent<CharacterController>();
    }
    public void UpdatePosition(byte id ,params short[] pos )  
    {

        // Client will receive the decode variable along with the array of short values - which holds the xyz co-ordinates.
        var decodeType = (DecodeType)id;

        var newPos = GetPosition(pos, decodeType);

        transform.position = Vector3.MoveTowards(transform.position, newPos, speed * Time.deltaTime);
    }

    private Vector3 GetPosition(short[] pos, DecodeType decodeType)
    {
        var position = transform.position;
        Vector3 newPos;
        switch (decodeType)
        {
            case DecodeType.OnlyX:
                newPos = new Vector3(pos[0], position.y, position.z);
                Debug.LogFormat(" received  position - x : {0}", pos[0]);
                break;
            case DecodeType.OnlyY:
                newPos = new Vector3(position.x, pos[0], position.z);
                Debug.LogFormat(" received  position - y : {0}", pos[0]);
                break;
            case DecodeType.OnlyZ:
                newPos = new Vector3(position.x, position.y, pos[0] - zOffset); // since, to move the client on a different platform i have an offset from the local player (hard coded value)
                Debug.LogFormat(" received  position - z : {0}", pos[0]);
                break;
            case DecodeType.XandY:
                newPos = new Vector3(pos[0], pos[1], position.z);
                Debug.LogFormat(" received  position - X : {0}  y  : {1}", pos[0], pos[1]);
                break;
            case DecodeType.YandZ:
                newPos = new Vector3(position.x, pos[0], pos[1] - zOffset);
                Debug.LogFormat(" received  position - Y : {0}  Z  : {1}", pos[0], pos[1]);
                break;
            case DecodeType.ZandX:
                newPos = new Vector3(pos[1], position.y, pos[0] - zOffset);
                Debug.LogFormat(" received  position - Z : {0}  X  : {1}", pos[0], pos[1]);
                break;
            case DecodeType.XYZ:
                newPos = new Vector3(pos[0], pos[1], pos[2] - zOffset);
                Debug.LogFormat(" received  position - X : {0}  Y  : {1}  X  : {2}", pos[0], pos[1], pos[2]);
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }

        return newPos;
    }
}