using UnityEngine;

public class MobileController : MonoBehaviour
{
    public PlayerController player;
    public NewMonoBehaviourScript weapon;

    // Update is called once per frame
    void Update()
    {
        Vector2 move = Vector2.zero;

        for (int i = 0; i < Input.touchCount; i++)
        {
            Touch touch = Input.GetTouch(i);
             if (touch.position.x < Screen.width * 0.5f){
                if (touch.phase == TouchPhase.Stationary || touch.phase == TouchPhase.Moved)
                {
                    if (touch.position.x < Screen.width * 0.25f)
                        move = Vector2.left;
                    else
                        move = Vector2.right;

                }
             }
             else{
                weapon.HandleTouchInput(touch);
             }
            
        }
        player.OnMoveMobile(move);
    }
}
