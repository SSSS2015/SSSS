using UnityEngine;
using System.Collections.Generic;
using System;
using JetBrains.Annotations;

public class SpringNode
{
    public SpringNode Left, Right, Up, Down;
    public SpringNode UR, DR, UL, DL;

    public Vector3 Pos;
    public Vector3 Origin; // Default position in mesh
    public Vector3 Velocity;
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
    public float kSpringForceOrigin = .0001f;
    public float kDamping = 0.99f;

    public float kMaterialVelScale = 10.0f;
    public float kMaterialPosScale = 0.15f;

    public const float kInnerRadius = 75.0f;
    public const float kOuterRadius = 100.0f;
    public const float kThetaDelta = 20.0f;

    public float Theta;

    public SpringGrid LeftGrid;
    public SpringGrid RightGrid;

    public SpringNode[] Nodes;

    [NonSerialized] private Vector3[] TempPositions;
    [NonSerialized] private Color[] TempColors;
    [NonSerialized] private int[] MeshIndices;

    private Mesh MyMesh;

    public void Initialize(int[] meshIndices, Vector3[] tempPositions, Color[] tempColors)
    {
        MeshIndices = meshIndices;
        TempPositions = tempPositions;
        TempColors = tempColors;

        MeshFilter filter = GetComponent<MeshFilter>();
        MyMesh = filter.mesh; // Clones mesh from sharedMesh
        CreateNodes();
    }

    [UsedImplicitly]
    void Update()
    {
        SimulateSpringForces(Time.deltaTime);
    }

    public static int GridIdx(int x, int y)
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

                thisNode.Pos = thisNode.Origin = pos;
                thisNode.Velocity = Vector3.zero;

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
            // So link the left-side nodes from the next grid to the 2nd-from-the-right nodes in this grid
            SpringNode thisNode = GetNode(kGridWidth - 2, y);
            SpringNode otherNode = RightGrid.GetNode(0, y);
            otherNode.Left = thisNode;

            otherNode = RightGrid.GetNode(0, y - 1);
            if (otherNode != null)
                otherNode.UL = thisNode;

            otherNode = RightGrid.GetNode(0, y + 1);
            if(otherNode != null)
                otherNode.DL = thisNode;
        }
    }

    public void UnlinkRight()
    {
        if (RightGrid == null)
            return;

        // unlink all the nodes
        for(int y = 0; y < kGridHeight; ++y)
        {
            SpringNode otherNode = RightGrid.GetNode(0, y);
            otherNode.Left = null;

            otherNode = RightGrid.GetNode(0, y - 1);
            if(otherNode != null)
                otherNode.UL = null;

            otherNode = RightGrid.GetNode(0, y + 1);
            if(otherNode != null)
                otherNode.DL = null;
        }

        RightGrid.LeftGrid = null;
        RightGrid = null;
    }

    private static void Spring(SpringNode node, SpringNode other, float dt, float dist, float force)
    {
        if (other == null)
            return;

        Vector3 posDelta = other.Pos - node.Pos;
        float lengthDelta = posDelta.magnitude;
        float springError = lengthDelta - dist;
        if(springError < 0)
        {
            // TODO: force to counteract compression is higher than force to counteract expansion?
            springError *= 1;
        }

        Vector3 accel = posDelta * ((springError / lengthDelta) * force * dt);
        node.Velocity += accel;
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

            // Spring force back towards origin
            Vector3 originDelta = node.Origin - node.Pos;
            float originDist2 = originDelta.sqrMagnitude;

            if(originDist2 > 0.000001f)
            {
                // accel towards origin is proportional to distance^3
                Vector3 accel = originDist2 * (originDelta * dt * kSpringForceOrigin);
                node.Velocity += accel;
            }
        }
    }

    public void AddOutwardForce(Vector2 center, float dist, float power)
    {
        Vector3 center3 = new Vector3(center.x, center.y, 0);
        float distSq = dist*dist;

        // TODO: Don't visit every node
        for(int i = 0; i < kGridHeight * kGridWidth; ++i)
        {
            SpringNode node = Nodes[i];
            Vector3 diff = node.Pos - center3;
            float diffMagSq = diff.sqrMagnitude;

            if(diffMagSq > 0.01f && diffMagSq < distSq)
            {
                //power *= 1 - (diff.magnitude / dist);
                node.Velocity += power * diff.normalized;
            }
        }
    }

    public void ResetNodePositions()
    {
        for (int i = 0; i < kGridHeight * kGridWidth; ++i)
        {
            Nodes[i].Pos += Nodes[i].Velocity;
            Nodes[i].Velocity *= kDamping;
        }
    }

    public void UpdateSeams()
    {
        if (RightGrid != null)
        {
            // Stitch right line of this as a copy of the left line of the adjacent mesh
            for (int y = 0; y < kGridHeight; ++y)
            {
                SpringNode node = GetNode(kGridWidth - 1, y);
                SpringNode otherNode = RightGrid.GetNode(0, y);
                node.Pos = otherNode.Pos;
                node.Velocity = otherNode.Velocity;
            }
        }
    }

    public void TransferToMesh()
    {
        if(MyMesh != null)
            TransferToMesh(MyMesh);
    }

    private void TransferToMesh(Mesh m)
    {
        for (int i = 0; i < kGridHeight * kGridWidth; ++i)
        {
            int index = MeshIndices[i];

            Vector3 pos = Nodes[i].Pos;
            Vector3 diff = pos - Nodes[i].Origin;
            Vector3 vel = Nodes[i].Velocity;

            float dx = Mathf.Clamp01(diff.x * kMaterialPosScale + 0.5f);
            float dy = Mathf.Clamp01(diff.y * kMaterialPosScale + 0.5f);
            float vx = Mathf.Clamp01(vel.x * kMaterialVelScale + 0.5f);
            float vy = Mathf.Clamp01(vel.y * kMaterialVelScale + 0.5f);

            TempColors[index] = new Color(dx, dy, vx, vy);
            TempPositions[index] = new Vector3(pos.x, pos.y, 0);
        }

        m.vertices = TempPositions;
        m.colors = TempColors;
        m.RecalculateBounds();
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

                if (radius < rows[i].Radius)
                    break;
            }

            //Debug.Log("Addrow " + radius);
            MeshRow row = new MeshRow(radius);
            rows.Insert(i, row);
            return row;
        }
    }

    static public int[] ImportMesh(Mesh m)
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

        int[] meshIndices = new int[kGridWidth * kGridHeight];
        for (int y = 0; y < rows.Count; ++y)
        {
            MeshRow row = rows[y];
            for(int x = 0; x < row.Entries.Count; ++ x)
            {
                meshIndices[GridIdx(x,y)] = row.Entries[x].Index;
            }
        }
        return meshIndices;
    }

    public bool InBounds(Vector3 pos, float slop)
    {
        Bounds b = MyMesh.bounds;
        Bounds zBounds = new Bounds(b.center, new Vector3(0, 0, 200));
        b.Expand(slop);
        b.Encapsulate(zBounds);
        
        return b.Contains(pos);
    }

    public void SeaLevel(float thetaRadians, ref float prevBest)
    {
        // TODO: check if this should be 0 or kGridHeight-1
        const int y = kGridHeight - 1;

        SpringNode prevNode = Nodes[GridIdx(0, y)];
        Vector2 prevPolar = World.Instance.GetPolarCoordinate(prevNode.Pos);

        for(int x = 1; x < kGridWidth; ++x)
        {
            SpringNode curNode = Nodes[GridIdx(x, y)];
            Vector2 curPolar = World.Instance.GetPolarCoordinate(curNode.Pos);

            // Check if this segment is forward facing
            float polarDiff = curPolar.y - prevPolar.y;
            if (polarDiff < -Mathf.PI)
                polarDiff += Mathf.PI * 2;

            if(polarDiff >= 0 && polarDiff < Mathf.PI)
            {
                // Check if the selected point is inside this line segment
                float pointDiff = thetaRadians - prevPolar.y;
                if (pointDiff < -Mathf.PI)
                    polarDiff += Mathf.PI * 2;

                if(pointDiff >= 0 && pointDiff <= polarDiff)
                {
                    // point is inside forward facing line segment, get sea level at that point
                    float newSeaLevel = curPolar.x;
                    if (polarDiff > 0.001f)
                        newSeaLevel = Mathf.Lerp(prevPolar.x, curPolar.x, pointDiff / polarDiff);

                    if (newSeaLevel > prevBest)
                        prevBest = newSeaLevel;
                }
            }

            prevPolar = curPolar;
        }
    }
}
