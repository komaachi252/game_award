using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map_Data
{
    private int[,] m_map_data;
    private int m_height;
    private int m_width;
    public int Height
    {
        set { this.m_height = value; }
        get { return m_height; }
    }
    public int Width
    {
        set { this.m_width = value; }
        get { return m_width; }
    }
    public int[,] Map_data
    {
        set { this.m_map_data = value; }
        get { return this.m_map_data; }
    }
}