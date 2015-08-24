using UnityEngine;
using System.Collections;
using JetBrains.Annotations;
using System;

public class WaveManager : MonoBehaviour
{
    const int kNumSegments = 360 / 20;

    public GameObject WavePrefab;
    public Rigidbody Player;
    public float ForceDist = 3.0f;
    public float ForcePower = 1.0f;

    [NonSerialized] private int[] MeshIndices;
    [NonSerialized] private SpringGrid[] Grids;

    [UsedImplicitly]
	void Awake()
    {
        SpringGrid prefabGrid = WavePrefab.GetComponent<SpringGrid>();
        if (prefabGrid == null)
            throw new Exception("WaveManager::Start: WavePrefab has no SpringGrid component");

        // Get data for the grid mesh
        MeshFilter filter = WavePrefab.GetComponent<MeshFilter>();
        if (filter == null)
            throw new Exception("WaveManager::Start: WavePrefab has no Mesh component");
        MeshIndices = SpringGrid.ImportMesh(filter.sharedMesh);

        // Allocate some shared temporary space
        Vector3[] tempPositions = new Vector3[SpringGrid.kGridHeight * SpringGrid.kGridWidth];
        Color[] tempColors = new Color[SpringGrid.kGridHeight * SpringGrid.kGridWidth];

        // Spawn the grids
        Grids = new SpringGrid[kNumSegments];
        for(int i = 0; i < kNumSegments; ++i)
        {
            GameObject gridObj = GameObject.Instantiate(WavePrefab);
            gridObj.transform.SetParent(transform, false);
            SpringGrid grid = gridObj.GetComponent<SpringGrid>();
            grid.Theta = i * SpringGrid.kThetaDelta;
            grid.Initialize(MeshIndices, tempPositions, tempColors);

            Grids[i] = grid;
        }

        // Link all the grids together
        for(int i = 0; i < kNumSegments-1; ++i)
        {
            Grids[i].LinkRight(Grids[i + 1]);
        }
        Grids[kNumSegments - 1].LinkRight(Grids[0]);
	}
	
	// Update is called once per frame
	void Update ()
    {
        // HACK: handle pause because we don't handle it properly
        if (Time.deltaTime == 0)
            return;

        // TODO: We really only want to run simulation on segments near the player
	    for(int i = 0; i < kNumSegments; ++i)
        {
            Grids[i].SimulateSpringForces(Time.deltaTime);
        }

        // Apply any other forces here (motion from player?)
        if(Player != null)
        {
            Vector3 playerPos = Player.transform.position;
            for (int i = 0; i < kNumSegments; ++i)
            {
                SpringGrid grid = Grids[i];
                if (grid.InBounds(playerPos, ForceDist))
                {
                    float speed = Player.velocity.magnitude * (1.0f / 10.0f); // 10.0f = player max speed
                    grid.AddOutwardForce(new Vector2(playerPos.x, playerPos.y), ForceDist * speed, ForcePower * speed * Time.deltaTime);
                }
            }
        }

        for (int i = 0; i < kNumSegments; ++i)
        {
            Grids[i].ResetNodePositions();
        }

        for(int i = 0; i < kNumSegments; ++i)
        {
            Grids[i].UpdateSeams();
        }

        for(int i = 0; i < kNumSegments; ++i)
        {
            Grids[i].TransferToMesh();
        }
	}

    public float GetSeaLevel(float thetaRadians)
    {
        // Find the highest forward line segment at that theta
        float thetaDegrees = thetaRadians * Mathf.Rad2Deg;
        float searchBegin = thetaDegrees - 10.0f;

        // each segment is 20 degrees
        int segment = Mathf.FloorToInt(searchBegin / 20.0f);
        if (segment < 0)
            segment += kNumSegments;

        // wat
        if (segment >= kNumSegments)
        {
            Debug.LogErrorFormat("WaveManager::SeaLevel: theta {0} gives strange start segment, returning default", thetaDegrees);
            return World.Instance.SeaLevel;
        }

        // Search 2 grids to find matching segments at sea level
        float seaLevel = -1;  // any negative number works, sea level is always >= 0
        for(int i = 0; i < 2; ++i)
        {
            Grids[segment].SeaLevel(thetaRadians, ref seaLevel);

            segment++;
            if(segment == kNumSegments)
                segment = 0;
        }

        if (seaLevel < 0)
        {
            Debug.LogErrorFormat("WaveManager::SeaLevel: Couldn't find segment for theta {0}, returning default", thetaDegrees);
            return World.Instance.SeaLevel;
        }

        // The water texture is slightly below the polygon edge
        seaLevel -= 0.5f;

        //Debugging gizmos
        /* 
        Vector3 worldPoint = World.Instance.GetWorldCoordinate(new Vector2(seaLevel, thetaRadians));
        Vector3 worldPoint2 = World.Instance.GetWorldCoordinate(new Vector2(seaLevel + 10, thetaRadians));
        Debug.DrawLine(worldPoint, worldPoint2, Color.red);
         */

        return seaLevel;
    }
}
