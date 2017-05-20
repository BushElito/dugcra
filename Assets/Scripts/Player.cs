using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float turnDelay;
    public GameObject moveEffect;
    private Rigidbody2D rb2D;
    private bool idle;
    public LayerMask fog;
    public LayerMask wall;
    public World fogWorld;
    public World world;
    private int horizontal;
    private int vertical;
    private bool loaded;

    public GameObject pauseMenu;
    public GameObject map;
    public bool isPaused = false;
    Animator anim;

    public ScoreManager scoreManager;
    private GameSounds gameSounds;
    private System.Random rnd;

    protected void Start()
    {
        loaded = false;
        idle = true;
        rb2D = GetComponent<Rigidbody2D>();
        gameSounds = GetComponent<GameSounds>();
        rnd = new System.Random();
        Time.timeScale = 1;

        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (!world.isPointGenerated)
        {
            return;
        }
        else if (!loaded)
        {

            loaded = true;
            Grid g = fogWorld.GetGrid(world.startingGrid.x, world.startingGrid.y);
            g.SetTile(world.startingPoint.x, world.startingPoint.y, new GridTile(GridTile.TileTypes.Empty));
            g.update = true;
            transform.position = new Vector3(Mathf.FloorToInt(world.startingPoint.x + g.pos.x) + 0.5f, Mathf.FloorToInt(world.startingPoint.y + g.pos.y) + 0.5f, transform.position.z);
            Camera.main.transform.position = new Vector3(Mathf.FloorToInt(world.startingPoint.x + g.pos.x) + 0.5f, Mathf.FloorToInt(world.startingPoint.y + g.pos.y) + 0.5f, transform.position.z);
        }
        horizontal = 0;
        vertical = 0;
        horizontal = (int)Input.GetAxisRaw("Horizontal");
        vertical = (int)Input.GetAxisRaw("Vertical");

        if (horizontal != 0)
        {
            vertical = 0;
        }
        if ((horizontal != 0 || vertical != 0) && idle)
        {
            idle = false;

            AttemptMove(horizontal, vertical);

        }
        if (Input.GetKeyDown(KeyCode.Escape) && !map.activeInHierarchy)
        {
            isPaused = !isPaused;
            Pause_action(isPaused);
            pauseMenu.SetActive(isPaused);
        }
        if (Input.GetKeyDown(KeyCode.F1))
        {
            foreach (var item in fogWorld.grids)
            {
                item.Value.clear = true;
            }
        }

    }
    public void Pause_action(bool pause = false)
    {
        isPaused = pause;
        if (!isPaused)
        {
            Time.timeScale = 1;
        }
        else
        {
            Time.timeScale = 0;
        }
    }
    public void Quit()
    {
        Application.Quit();
    }

    public void AttemptMove(int xDir, int yDir)
    {
        if (Time.timeScale == 1)
        {
            Vector2 end = rb2D.position + new Vector2(xDir, yDir);
            RaycastHit2D fogDetect;
            RaycastHit2D walldetect;

            fogDetect = Physics2D.Linecast(rb2D.position, end, fog);
            walldetect = Physics2D.Linecast(rb2D.position, end, wall); //nekeisti

            WorldPos pos = EditTerrain.GetBlockPos(fogDetect);
            //Debug.Log(pos.x + " " + pos.y);

            if (fogDetect)
            {
                fogWorld.SetTile(pos.x, pos.y, new GridTile(GridTile.TileTypes.Empty));
                scoreManager.AddPoints(1);
            }
            //if (fogDetect)
            //{
            //    GameObject.Find(fogDetect.transform.name).GetComponent<SpriteRenderer>().enabled = false;
            //    print(fogDetect.transform.name);
            //    GameObject.Find(fogDetect.transform.name).GetComponent<BoxCollider2D>().enabled = false;
            //    Destroy(GameObject.Find(fogDetect.transform.name).GetComponent<GameObject>());
            //}
            if (!walldetect && !fogDetect)
            {
                gameSounds.PlaySound(rnd.Next(0, 2));

                rb2D.MovePosition(end);
                Instantiate(moveEffect, transform.position, Quaternion.identity);
            }            
        }
        StartCoroutine(Delay());
    }

    public void LateUpdate()
    {
        if (Time.timeScale == 1 && !scoreManager.isDead)
        {
            if (horizontal > 0)
            {
                anim.SetInteger("Value", 2);
            }
            else if (horizontal < 0)
            {
                anim.SetInteger("Value", 1);
            }
            else if (vertical > 0)
            {
                anim.SetInteger("Value", 0);
            }
            else if (vertical < 0)
            {
                anim.SetInteger("Value", 3);
            }
        }
        else if (scoreManager.isDead)
        {
            anim.SetInteger("Value", 4);
        }
    }

    IEnumerator Delay()
    {
        yield return new WaitForSeconds(turnDelay);
        idle = true;
    }

}
