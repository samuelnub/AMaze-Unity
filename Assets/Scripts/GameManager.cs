using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

    public Maze mazePrefab;
    private Maze mazeInstance;

    private void Start()
    {
        this.BeginGame();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Return))
        {
            this.RestartGame();
        }
    }

    private void BeginGame()
    {
        this.mazeInstance = Instantiate(this.mazePrefab) as Maze;
        StartCoroutine(this.mazeInstance.Generate());
    }

    private void RestartGame()
    {
        StopAllCoroutines();
        Destroy(this.mazeInstance.gameObject);
        this.BeginGame();
    }
}
