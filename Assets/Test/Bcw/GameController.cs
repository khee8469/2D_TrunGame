using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameController : MonoBehaviour
{
    [SerializeField]
    Player player;
    void OnMove(InputValue value)
    {
        Debug.Log("1");
    }
    void OnMouse(InputValue value)
    {
        if (coroutine != null)
            StopCoroutine(coroutine);

        Vector3 playerPos = player.transform.position;
        float x = value.Get<Vector2>().x;
        float y = value.Get<Vector2>().y;
        float z = Mathf.Abs(Camera.main.transform.position.z);
        Vector3 clikPos = Camera.main.ScreenToWorldPoint(new Vector3(x, y, z));

        coroutine = StartCoroutine(Move(playerPos, clikPos));
    }

    private void LateUpdate()
    {
        Camera.main.transform.position = new Vector3(player.transform.position.x, player.transform.position.y, Camera.main.transform.position.z);
    }

    Coroutine coroutine;
    IEnumerator Move(Vector3 start, Vector3 end)
    {
        float distance = Vector3.Distance(start, end);
        float time = 0;
        while (true)
        {
            time += Time.deltaTime / distance;
            player.transform.position = Vector3.Lerp(start, end, time);
            yield return null;
            if (time > 1)
                break;
        }
    }
}
