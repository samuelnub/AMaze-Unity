using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Maze : MonoBehaviour {

    public MazeCell mazeCellPrefab;

    public IntVector2 size;

    private MazeCell[,] cells;

    public float generationStepDelay;

    public IntVector2 RandomCoordinates //cool use of getters & setters
    {
        get
        {
            return new IntVector2(Random.Range(0, this.size.x), Random.Range(0, this.size.z));
        }
    }

    public bool ContainsCoordinates(IntVector2 coordinate)
    {
        return coordinate.x >= 0 && coordinate.x < this.size.x && coordinate.z >= 0 && coordinate.z < this.size.z;
    }

    public MazeCell GetCell(IntVector2 coordinates)
    {
        return cells[coordinates.x, coordinates.z];
    }

    private void DoFirstGenerationStep(List<MazeCell> activeCells)
    {
        activeCells.Add(this.CreateCell(RandomCoordinates));
    }

    private void DoNextGenerationStep(List<MazeCell> activeCells)
    {
        int currentIndex = activeCells.Count - 1;
        MazeCell currentCell = activeCells[currentIndex];
        MazeDirection direction = MazeDirections.RandomValue;
        IntVector2 coordinates = currentCell.coordinates + direction.ToIntVector2();
        if(this.ContainsCoordinates(coordinates) && this.GetCell(coordinates) == null)
        {
            activeCells.Add(this.CreateCell(coordinates));
        }
        else
        {
            activeCells.RemoveAt(currentIndex);
        }
    }

    public IEnumerator Generate()
    {
        WaitForSeconds delay = new WaitForSeconds(this.generationStepDelay);
        this.cells = new MazeCell[this.size.x, this.size.z];

        List<MazeCell> activeCells = new List<MazeCell>();
        this.DoFirstGenerationStep(activeCells);

        while(activeCells.Count > 0)
        {
            yield return delay;
            this.DoNextGenerationStep(activeCells);
        }
    }

    private MazeCell CreateCell(IntVector2 coordinates)
    {
        MazeCell newCell = Instantiate(this.mazeCellPrefab) as MazeCell;

        this.cells[coordinates.x, coordinates.z] = newCell;
        newCell.coordinates = coordinates;
        newCell.name = "Maze Cell " + coordinates.x + " " + coordinates.z;
        newCell.transform.parent = transform; //zeroed first
        newCell.transform.localPosition = new Vector3(
            coordinates.x - this.size.x * 0.5f + 0.5f, 
            0.0f, 
            coordinates.z - this.size.z * 0.5f + 0.5f);
        return newCell;
    }
    
}
