﻿using UnityEngine;
using System.Collections.Generic;
using System;
using JetBrains.Annotations;

public class SpringNode
{
    public SpringNode Left, Right, Up, Down;
    public SpringNode UR, DR, UL, DL;

    public Vector3 Pos;
    public Vector3 Delta;
    public bool Locked;
}

public class SpringGrid : MonoBehaviour
{
    public const int kGridHeight = 17 + 1;  // 17 squares tall
    public const int kGridWidth = 20 + 1;   // 20 squares wide

    private const float kHorizSpringDist = 25.0f / 17; // TODO
    private const float kDiagSpringDist = kHorizSpringDist * 1.41f; // sqrt(2)

    public float kSpringForceHoriz = .01f;
    public float kSpringForceVert = .01f;
    public float kSpringForceDiag = .005f;

    public const float kInnerRadius = 75.0f;
    public const float kOuterRadius = 100.0f;
    public const float kThetaDelta = 20.0f;

    public float Theta;

    public SpringGrid LeftGrid;
    public SpringGrid RightGrid;

    public SpringNode[] Nodes;

    private Vector3[] positions = new Vector3[kGridWidth * kGridHeight];
    private int[] meshIndices = new int[kGridWidth * kGridHeight];

    private Mesh MyMesh;

    [UsedImplicitly]
    void Start()
    {
        MeshFilter filter = GetComponent<MeshFilter>();
        if(filter != null)
        {
            MyMesh = filter.mesh; // Clones mesh from sharedMesh

            // TODO: This should only happen once, not for every segment
            if(MyMesh != null)
                ImportMesh(MyMesh);
        }

        CreateNodes();
    }

    [UsedImplicitly]
    void Update()
    {
        SimulateSpringForces(Time.deltaTime);
    }

    [UsedImplicitly]
    void LateUpdate()
    {
        ResetNodePositions();
        if(MyMesh != null)
            TransferToMesh(MyMesh);
    }

    public int GridIdx(int x, int y)
    {
        return y * kGridWidth + x;
    }

    public SpringNode GetNode(int x, int y)
    {
        if (x < 0 || x >= kGridWidth || y < 0 || y >= kGridHeight)
            return null;

        return Nodes[GridIdx(x, y)];
    }

    public void CreateNodes()
    {
        Nodes = new SpringNode[kGridHeight * kGridWidth];

        // create nodes
        for(int y = 0; y < kGridHeight; ++y)
        {
            for(int x = 0; x < kGridWidth; ++x)
            {
                Nodes[GridIdx(x, y)] = new SpringNode();
            }
        }

        const float xScale = 1.0f / (kGridWidth - 1);
        const float yScale = 1.0f / (kGridHeight - 1);

        for(int y = 0; y < kGridHeight; ++y)
        {
            for(int x = 0; x < kGridWidth; ++x)
            {
                SpringNode thisNode = GetNode(x,y);
                
                // Calculate position
                float theta = Mathf.Lerp(Theta, Theta + kThetaDelta, x * xScale);
                float height = Mathf.Lerp(kInnerRadius, kOuterRadius, y * yScale);

                Vector3 pos = World.Instance.GetWorldCoordinate(new Vector2(height, theta * Mathf.Deg2Rad));

                thisNode.Pos = pos;
                thisNode.Delta = Vector3.zero;

                thisNode.Down = GetNode(x, y-1);
                thisNode.Up = GetNode(x, y+1);
                thisNode.Left = GetNode(x-1, y);
                thisNode.Right = GetNode(x+1, y);

                thisNode.DR = GetNode(x + 1, y - 1);
                thisNode.DL = GetNode(x - 1, y - 1);
                thisNode.UR = GetNode(x + 1, y + 1);
                thisNode.UL = GetNode(x - 1, y + 1);

                thisNode.Locked = (y == 0);
            }
        }
    }

    public void LinkRight(SpringGrid rightGrid)
    {
        if (RightGrid != null)
            UnlinkRight();

        RightGrid = rightGrid;
        RightGrid.LeftGrid = this;

        for(int y = 0; y < kGridHeight; ++y)
        {
            // We leave the far-rightmost grid node as a mirror of the leftmost grid node in the next grid
            SpringNode thisNode = GetNode(kGridWidth - 2, y);
            SpringNode otherNode = RightGrid.GetNode(0, y);
            thisNode.Right = otherNode;
            otherNode.Left = thisNode;
        }
    }

    public void UnlinkRight()
    {
        if (RightGrid == null)
            return;

        for(int y = 0; y < kGridHeight; ++y)
        {
            SpringNode thisNode = GetNode(kGridWidth - 2, y);
            SpringNode otherNode = RightGrid.GetNode(0, y);
            thisNode.Right = null;
            otherNode.Left = null;
        }

        RightGrid.LeftGrid = null;
        RightGrid = null;
    }

    public void ResetNodePositions()
    {
        for(int i = 0; i < kGridHeight * kGridWidth; ++i)
        {
            Nodes[i].Pos += Nodes[i].Delta;
        }
    }

    private static void Spring(SpringNode node, SpringNode other, float dt, float dist, float force)
    {
        if (other == null)
            return;

        Vector3 posDelta = other.Pos - node.Pos;
        float lengthDelta = posDelta.magnitude;
        float springError = lengthDelta - dist;

        Vector3 accel = posDelta * (springError / lengthDelta) * force * dt;
        node.Delta += accel;
    }

    public void SimulateSpringForces(float dt)
    {
        for(int i = 0; i < kGridHeight * kGridWidth; ++i)
        {
            SpringNode node = Nodes[i];

            if (node.Locked)
                continue;

            Spring(node, node.Left, dt, kHorizSpringDist, kSpringForceHoriz);
            Spring(node, node.Right, dt, kHorizSpringDist, kSpringForceHoriz);
            Spring(node, node.Up, dt, kHorizSpringDist, kSpringForceVert);
            Spring(node, node.Down, dt, kHorizSpringDist, kSpringForceVert);
            Spring(node, node.UL, dt, kDiagSpringDist, kSpringForceDiag);
            Spring(node, node.DL, dt, kDiagSpringDist, kSpringForceDiag);
            Spring(node, node.UR, dt, kDiagSpringDist, kSpringForceDiag);
            Spring(node, node.DR, dt, kDiagSpringDist, kSpringForceDiag);
        }
    }

    public void AddOutwardForce(Vector2 center, float dist, float power)
    {
        
    }

    public void TransferToMesh(Mesh m)
    {
        for (int i = 0; i < kGridHeight * kGridWidth; ++i)
        {
            positions[meshIndices[i]] = Nodes[i].Pos;
        }
        m.vertices = positions;
    }

    private struct RowEntry
    {
        public readonly float Theta;
        public readonly int Index;

        public RowEntry(float theta, int index)
        {
            Theta = theta;
            Index = index;
        }
    }

    private struct MeshRow
    {
        public readonly float Radius;
        public readonly List<RowEntry> Entries;

        public MeshRow(float radius)
        {
            Radius = radius;
            Entries = new List<RowEntry>();
        }

        public void AddEntry(float theta, int index)
        {
            int i = 0;
            for(; i < Entries.Count; ++i)
            {
                if (theta > Entries[i].Theta)
                    break;
            }

            Entries.Insert(i, new RowEntry(theta, index));
        }

        public const float kRadiusEpsilon = 0.1f;

        public static MeshRow GetRow(List<MeshRow> rows, float radius)
        {
            int i = 0;
            for(; i < rows.Count; ++i)
            {
                if(Mathf.Abs(radius - rows[i].Radius) < kRadiusEpsilon)
                {
                    return rows[i];
                }

                if (radius > rows[i].Radius)
                    break;
            }

            //Debug.Log("Addrow " + radius);
            MeshRow row = new MeshRow(radius);
            rows.Insert(i, row);
            return row;
        }
    }

    public void ImportMesh(Mesh m)
    {
        Vector3[] positions = m.vertices;
        if(positions.Length != kGridHeight * kGridWidth)
            throw new Exception("SpringGrid::ImportMesh: bad mesh");

        List<MeshRow> rows = new List<MeshRow>();
        
        for (int i = 0; i < positions.Length; ++i)
        {
            Vector3 pos = positions[i];
            Vector2 pos2d = new Vector2(pos.x, pos.y);
            float radius = pos2d.magnitude;
            float theta = Mathf.Atan2(-pos2d.x,pos2d.y);

            MeshRow row = MeshRow.GetRow(rows, radius);
            row.AddEntry(theta, i);
        }

        if (rows.Count != kGridHeight)
            throw new Exception("SpringGrid::ImportMesh: Mesh not a grid (y)");

        for(int y = 0; y < rows.Count; ++y)
        {
            if(rows[y].Entries.Count != kGridWidth)
            {
                throw new Exception("SpringGrid::ImportMesh: Mesh not a grid (x)");
            }
        }

        for(int y = 0; y < rows.Count; ++y)
        {
            MeshRow row = rows[y];
            for(int x = 0; x < row.Entries.Count; ++ x)
            {
                meshIndices[GridIdx(x,y)] = row.Entries[x].Index;
            }
        }
    }
}