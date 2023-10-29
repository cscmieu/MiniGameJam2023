using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CinematicManager : MonoBehaviour
{
    [SerializeField] private PlayerMovement player;
    [SerializeField] private Transform startPosition;
    [SerializeField] private Transform endRopePosition;
    [SerializeField] private Transform finalPosition;
    [SerializeField] private CameraMovement cam;
    [SerializeField] private float timeToDescend = 5f;
    [SerializeField] private TileDecayManager tileDecayManager;
    [SerializeField] private GameObject victoryScreen;
    
    private Vector3 velocity;

    private static readonly int       speed           = Animator.StringToHash("Speed");
    private static readonly int       isClimbingRope  = Animator.StringToHash("isClimbingRope");
    private static readonly int       isTouchingRope  = Animator.StringToHash("isTouchingRope");
    private static readonly int       isTouchingFloor = Animator.StringToHash("isTouchingFloor");
    private static readonly int       airSpeed        = Animator.StringToHash("AirSpeed");
    
    void Start()
    {
        AudioManager.Instance.PlayMusic("Descent");
        player.inCinematic = true;
        cam.isInStartCinematic = true;
        player.rb.isKinematic = true;
        player.transform.position = startPosition.position;
        cam.transform.position = startPosition.position;
        player.lamp.transform.localRotation = Quaternion.Euler(0,0,180);
        
        StartCoroutine(StartAnimCoroutine());
    }

    public void OnTriggerEnter2D(Collider2D col)
    {
        cam.isInEndCinematic = true;
        player.inCinematic = true;

        player.rb.AddForce(new Vector2(10,6), ForceMode2D.Impulse);
       
        Vector3 localScale = player.transform.localScale;
        localScale.x = 1f;
        player.transform.localScale = localScale;
        
        StartCoroutine(EndAnimCoroutine());
    }

    private IEnumerator StartAnimCoroutine()
    {
        player.playerAnimator.SetBool(isTouchingRope, true);
        player.playerAnimator.SetFloat(isClimbingRope, 1);
        
        while (Vector3.Distance(player.transform.position, endRopePosition.transform.position) > 2f)
        {
            player.transform.position = Vector3.SmoothDamp(player.transform.position, endRopePosition.position, ref velocity, timeToDescend);
            yield return null;
        }
        
        
        cam.isInStartCinematic = false;
        player.rb.isKinematic = false;
        player.playerAnimator.SetBool(isTouchingRope, false);
        player.playerAnimator.SetFloat(isClimbingRope, 0);
        player.playerAnimator.SetFloat(speed, 0);
        player.lamp.transform.localRotation = Quaternion.Euler(0,0,-90);
        
        yield return new WaitForSeconds(1);

        float time = .2f;
        while (time > 0)
        {
            player.playerAnimator.SetFloat(speed, 1);
            player.rb.velocity = new Vector2(8, 0);
            time -= Time.deltaTime;
            yield return null;
        }
        
        player.rb.velocity = new Vector2(0, 0);
        player.playerAnimator.SetFloat(speed, 0);
        
        yield return new WaitForSeconds(1);
        
        player.inCinematic = false;
        tileDecayManager.gameObject.SetActive(true);
        AudioManager.Instance.PlayMusic("MainMusic", true);
    }

    private IEnumerator EndAnimCoroutine()
    {
        player.playerAnimator.SetBool(isTouchingRope, false);
        player.playerAnimator.SetFloat(isClimbingRope, 0);
        
        yield return new WaitForSeconds(.2f);
        
        player.playerAnimator.SetBool(isTouchingFloor, true);
        player.playerAnimator.SetFloat(airSpeed, 0);
        
        float time = 2f;
        while (time > 0)
        {
            player.playerAnimator.SetFloat(speed, 1);
            player.rb.velocity = new Vector2(2, player.rb.velocity.y);
            time -= Time.deltaTime;
            yield return null;
        }
        
        player.rb.velocity = new Vector2(0, 0);
        player.playerAnimator.SetFloat(speed, 0);
        
        yield return new WaitForSeconds(2);
        
        victoryScreen.SetActive(true);
    }
}
