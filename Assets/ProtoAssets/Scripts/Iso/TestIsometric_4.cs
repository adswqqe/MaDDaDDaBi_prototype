using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using UnityEngine.EventSystems;

public class TestIsometric_4 : MonoBehaviour
{
    public Button moveBtn;
    public Button buildingBtn;
    public int colCount, rowCount;
    public Vector2 startPos;

    private GameObject mapTile;
    private Coroutine routine;
    private int tileWidth = 64;
    private int tileHeight = 32;
    private int col = 0; //행
    private int row = 0; //열

    private GameObject character;
    private GameObject building;
    private GameObject motherNode;
    private GameObject targetNode;
    private GameObject selectedTile;

    private Dictionary<Vector2, GameObject> dicFIndTile = new Dictionary<Vector2, GameObject>();
    private List<GameObject> closeList = new List<GameObject>();
    private List<GameObject> movePositionList = new List<GameObject>();

    public GameObject 도착타일;
    public GameObject 말풍선;
    void Start()
    {
        this.CreateCharacter();
        this.CreateMap();
        this.TileCheck();
        this.targetNode = 도착타일;

        this.moveBtn.onClick.AddListener(() =>
        {
            this.FIndNextPos();
        });
    }

    void TileCheck()
    {
        if (this.routine != null)
        {
            StopCoroutine(this.routine);
        }
        this.routine = StartCoroutine(this.TileCheckImpl());
    }

    IEnumerator TileCheckImpl()
    {
            yield return null;
        
    }
    public void Createbuilding()
    {
        if (routine != null)
        {
            StopCoroutine(this.routine);
        }
        this.routine = StartCoroutine(this.CreatebuildingImpl());
    }

    private IEnumerator CreatebuildingImpl()
    {
        var building = Instantiate(Resources.Load<GameObject>("Prefabs/isoTile"));

        while (true)
        {
            Vector2 ray = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(ray, Vector2.zero);
            building.transform.position = ray;
            if (!EventSystem.current.IsPointerOverGameObject())
            {
                if (Input.GetMouseButtonDown(0))
                {
                    if (hit.collider != null)
                    {
                        hit.collider.gameObject.GetComponent<TestTile>().isBlock = true;

                        building.transform.position = hit.collider.gameObject.transform.position;
                        break;
                    }
                }
            }
            yield return null;
        }

        this.TileCheck();
    }

    private void FIndNextPos()
    {
        if (this.routine != null)
        {
            StopCoroutine(this.routine);
        }
        this.routine = StartCoroutine(this.FindNextPosImpl());
    }

    private IEnumerator FindNextPosImpl()
    {
        while (true)
        {
            var dir = (this.motherNode.transform.position - this.character.transform.position).normalized;
            var nextNode = Vector2.Distance(this.character.transform.position, this.motherNode.transform.position);
            var distance = Vector2.Distance(this.character.transform.position, this.targetNode.transform.position);

            this.character.transform.position += dir * 2f * Time.deltaTime;

            if (nextNode <= 0.05f)
            {
                if (distance <= 0.05f)
                {
                    Debug.Log("도착");
                    GameObject.Find("레벨시스템").GetComponent<레벨시스템>().경험치획득(10);
                    GameObject.Find("레벨시스템").GetComponent<레벨시스템>().평판획득(50);
                    GameObject.Find("골드시스템").GetComponent<골드시스템>().goldadd(20);
                    말풍선.SetActive(false);
                    this.TileCheck();
                    break;
                }

                this.FindNode();
                Debug.Log("다음");
            }
            yield return null;
        }
    }

    private void CharacterMovePoint()
    {
        var mother = this.motherNode.GetComponent<TestTile>().vec2;
        var data = this.character.GetComponent<TestTile>().vec2;
        var motherX = mother.x - 1;
        var motherY = mother.y - 1;
        var maxX = mother.x + 1;
        var maxY = mother.y + 1;
    }

    private void FindNode()
    {
        var mother = this.motherNode.GetComponent<TestTile>().vec2;
        var motherX = mother.x - 1;
        var motherY = mother.y - 1;
        var maxX = mother.x + 1;
        var maxY = mother.y + 1;

        this.character.GetComponent<TestTile>().vec2 = mother;

        if (motherX < 0)
        {
            motherX = 0;
        }
        if (motherY < 0)
        {
            motherY = 0;
        }

        for (int x = (int)motherX; x <= maxX; x++)
        {
            for (int y = (int)motherY; y <= maxY; y++)
            {
                if (x < this.colCount && y < this.rowCount)
                {
                    var data = new Vector2(x, y);

                    var tile = this.dicFIndTile[data];

                    if (tile.GetComponent<TestTile>().isBlock != true && !this.closeList.Contains(tile))
                    {
                        if (data.x == motherX && data.y == mother.y || data.y == mother.y)
                        {
                            this.movePositionList.Add(tile);
                        }
                        if (data.y == motherY && data.x == mother.x || data.x == mother.x)
                        {
                            this.movePositionList.Add(tile);
                        }

                        this.movePositionList.Remove(motherNode);
                        this.closeList.Add(motherNode);
                        if (!this.closeList.Contains(tile))
                        {
                            this.SetFGH();
                        }
                    }
                }
            }
        }
        this.NextNode();
        this.CharacterMovePoint();
    }

    private void NextNode()
    {
        IEnumerable<GameObject> sortNode = this.movePositionList.OrderBy(x => x.GetComponent<TestTile>().f);
        this.selectedTile = sortNode.FirstOrDefault<GameObject>();
        this.motherNode = this.selectedTile;
    }

    private void ClearMap()
    {
        Debug.Log("타일초기화");

        for (int i = 0; i < colCount; i++)
        {
            for (int j = 0; j < rowCount; j++)
            {
                var tiles = new Vector2(j, i);

                var tile = dicFIndTile[tiles];

                Debug.LogFormat("{0} {1}", tile.GetComponent<TestTile>().isBlock, tiles);

                tile.GetComponent<SpriteRenderer>().color = Color.white;
            }
        }
        this.closeList.Clear();
        this.targetNode = null;
        this.selectedTile = null;

        this.movePositionList.Clear();
    }


    #region FGH 활성화 비활성화
    private void SetActiveFGH(GameObject tile)
    {
        if (!this.movePositionList.Contains(tile))
        {
            tile.GetComponent<TestTile>().G.gameObject.SetActive(false);
            tile.GetComponent<TestTile>().F.gameObject.SetActive(false);
            tile.GetComponent<TestTile>().H.gameObject.SetActive(false);
        }
        else
        {
            tile.GetComponent<TestTile>().G.gameObject.SetActive(true);
            tile.GetComponent<TestTile>().F.gameObject.SetActive(true);
            tile.GetComponent<TestTile>().H.gameObject.SetActive(true);
        }
    }
    #endregion
    private void SetFGH()
    {
        foreach (var tile in this.movePositionList)
        {
            var getG = tile.GetComponent<TestTile>().g;
            var getH = tile.GetComponent<TestTile>().h;
            getG = Mathf.Round(Vector2.Distance(tile.GetComponent<TestTile>().vec2, this.motherNode.GetComponent<TestTile>().vec2) * 10);
            var dx = Mathf.Abs(tile.GetComponent<TestTile>().vec2.x - this.targetNode.GetComponent<TestTile>().vec2.x);
            var dy = Mathf.Abs(tile.GetComponent<TestTile>().vec2.y - this.targetNode.GetComponent<TestTile>().vec2.y);

            getH = (dx + dy) * 10;

            tile.GetComponent<TestTile>().f = getG + getH;
        }
    }

    #region 맵, 타일 생성
    private void CreateCharacter()
    {
        if (this.character == null)
        {
            this.character = Instantiate(Resources.Load<GameObject>("Prefabs/hero"));

            this.CreatedCharacter(character, this.startPos.x, this.startPos.y);
        }
    }

    private void CreateMap()
    {
        while (true)
        {
            this.mapTile = Instantiate(Resources.Load<GameObject>("Prefabs/tile"));

            this.mapTile.tag = "Tile";

            this.mapTile.transform.SetParent(this.transform);

            this.CreatedMap(this.mapTile, this.col, row);
            this.dicFIndTile.Add(new Vector2(col, row), mapTile);
            this.col++;

            if (this.col >= this.colCount)
            {
                this.col = 0;
                this.row++;
            }
            if (this.row >= this.rowCount)
            {
                row = 0;
                break;
            }
        }
    }
    private void CreatedCharacter(GameObject character, float x, float y)
    {
        var vec2 = new Vector2(x, y);

        if (character.GetComponentInChildren<TextMesh>())
        {
            character.GetComponentInChildren<TextMesh>().text = string.Format("({0},{1})", x, y);
            this.SetActiveFGH(character);
        }

        character.AddComponent<TestTile>();
        character.GetComponent<TestTile>().vec2 = vec2;

        var screen = this.MapToScreen(vec2);

        var screenPos = Camera.main.ScreenToWorldPoint(new Vector2(screen.x + 1024, screen.y + 384 + 500));

        character.transform.position = new Vector3(screenPos.x, screenPos.y, 0);
    }

    private void CreatedMap(GameObject tileMap, float x, float y)
    {
        var vec2 = new Vector2(x, y);

        if (tileMap.GetComponentInChildren<TextMesh>())
        {
            tileMap.GetComponentInChildren<TextMesh>().text = string.Format("({0},{1})", x, y);
            this.SetActiveFGH(tileMap);
        }

        if (vec2 == this.character.GetComponent<TestTile>().vec2)
        {
            this.motherNode = tileMap;
        }

        tileMap.AddComponent<TestTile>();
        tileMap.GetComponent<TestTile>().vec2 = vec2;

        var screen = this.MapToScreen(vec2);

        var screenPos = Camera.main.ScreenToWorldPoint(new Vector2(screen.x + 1024, screen.y + 384 + 500));

        tileMap.transform.position = new Vector3(screenPos.x, screenPos.y, 0);
    }

    public Vector2 MapToScreen(Vector2 mapPos)
    {
        var screenX = mapPos.x * this.tileWidth - (mapPos.y * this.tileWidth);
        var screenY = mapPos.y * -tileHeight - (mapPos.x * tileHeight);

        return new Vector2(screenX, screenY);
    }
    #endregion
}
