using UnityEngine;
public class ShipRandomizer : MonoBehaviour
{
    [SerializeField] private GameObject[] _ships;
    [SerializeField] private int[] _lengths;

    [SerializeField] private Vector3 _startPosition;

    private int[,] grid = new int[10, 10];

    private int x = 0;
    private int y = 0;

    public void RandomizeShips()
    {
        Debug.Log($"Width: {grid.GetLength(1)}, Height: {grid.GetLength(0)}");

        if (_ships.Length == _lengths.Length)
        {
            ClearGrid();

            for (int i = 0; i < _ships.Length; i++)
            {
                CreateShip(i);
            };
        }
        else
        {
            Debug.Log("Wrong inputs))");
        }
    }

    private void CreateShip(int i)
    {
            bool isHorizontal = ChooseDirection();

            int length = _lengths[i];

            ChooseCoordinates(isHorizontal, length, ref x, ref y);

        if (CheckZone(isHorizontal, length, x, y))
        {
            ChangeGrid(x, y, length, isHorizontal);

            _ships[i].SetActive(true);

            _ships[i].transform.position = new Vector3(_startPosition.x + x, _startPosition.y - y, 0);

            RotateShip(_ships[i], isHorizontal);
        }
        else
        {
            CreateShip(i);
        }
    }

    private void ChangeGrid(int x, int y, int length, bool isHorizontal)
    {
        if (isHorizontal)
        {
            for (int i = 0; i < length; i++)
            {
                grid[x + i, y] = 1;
            }
        }
        else
        {
            for (int i = 0; i < length; i++)
            {
                grid[x, y + i] = 1;
            }
        }
    }

    private void RotateShip(GameObject ship, bool isHorizontal)
    {
        if (isHorizontal)
        {
            ship.transform.rotation = Quaternion.Euler(0, 0, 90);
        }
        else
        {
            ship.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }

    private void ClearGrid()
    {
        for (int y = 0; y < grid.GetLength(0); y++)
        {
            for (int x = 0; x < grid.GetLength(1); x++)
            {
                grid[y, x] = 0;
            }
        }
    }

    private bool ChooseDirection()
    {
        if (Random.Range(0, 100) > 50)
        {
            return false;
        }

        return true;
    }

    private void ChooseCoordinates(bool isHorizontal, int length, ref int x, ref int y)
    {
        if (isHorizontal)
        {
            x = Random.Range(0, grid.GetLength(1) - length);
            y = Random.Range(0, grid.GetLength(0));
        }
        else
        {
            x = Random.Range(0, grid.GetLength(1));
            y = Random.Range(0, grid.GetLength(0) - length);
        }
    }

    private bool CheckZone(bool isHorizontal, int length, int x, int y)
    {
        for (int i = 0; i < length; i++)
        {
            if (isHorizontal)
            {
                if (grid[x + i , y] == 1)
                {
                    return false;
                }
            }
            else
            {
                if (grid[x, y + i] == 1)
                {
                    return false;
                }
            }
        }

        return true;
    }
}
