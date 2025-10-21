using UnityEngine;

public class NewEmptyCSharpScript : MonoBehaviour
{
    Animator am;
    PlayerMovement pm;
    SpriteRenderer sr;

    void Start()
    {
        am = GetComponent<Animator>();
        pm = GetComponent<PlayerMovement>();
        sr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (pm.movement.x != 0 || pm.movement.y != 0)
        {
            am.SetBool("Move", true);
            SpriteDirectionChecker();
        }
        else
        {
            am.SetBool("Move", false);
        }
    }

    void SpriteDirectionChecker()
    {
        if (pm.lastHorizontalVector > 0)
        {
            sr.flipX = false;
        }
        else if (pm.movement.x < 0)
        {
            sr.flipX = true;
        }
    }
}
