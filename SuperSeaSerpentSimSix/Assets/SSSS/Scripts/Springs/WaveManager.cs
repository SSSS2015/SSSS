using UnityEngine;
using System.Collections;
using JetBrains.Annotations;
using System;

public class WaveManager : MonoBehaviour
{
    const int kNumSegments = 360 / 20;

    public GameObject WavePrefab;
    public GameObject Player;
    public float ForceDist = 3.0f;
    public float ForcePower = 1.0f;

    [NonSerialized] private int[] MeshIndices;
    [NonSerialized] private SpringGrid[] Grids;

    [UsedImplicitly]
	void Start ()
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

        // Spawn the grids
        Grids = new SpringGrid[kNumSegments];
        for(int i = 0; i < kNumSegments; ++i)
        {
            GameObject gridObj = GameObject.Instantiate(WavePrefab);
            gridObj.transform.parent = transform;
            SpringGrid grid = gridObj.GetComponent<SpringGrid>();
            grid.Theta = i * SpringGrid.kThetaDelta;
            grid.Initialize(MeshIndices, tempPositions);

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
                    grid.AddOutwardForce(new Vector2(playerPos.x, playerPos.y), ForceDist, ForcePower);
                }
            }
        }

        for (int i = 0; i < kNumSegments; ++i)
        {
            Grids[i].ResetNodePositions();
        }
	}
}
