using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 图表
/// </summary>
public class Chart : MonoBehaviour
{
    private GameObject table;
    private WMG_Axis_Graph graph;
    private WMG_Series series_1;
    private WMG_Series series_2;
    public List<string> seriesDatas_1;
    public List<string> seriesDatas_2;

    private void Start()
    {
        CreateTable();
    }

    private void CreateTable()
    {
        table = Instantiate(Resources.Load<GameObject>("Prefabs/Graph"), transform, false);
        graph = table.GetComponent<WMG_Axis_Graph>();
        graph.xAxis.AxisMaxValue = 10;
        graph.yAxis.AxisMaxValue = 35;
        graph.yAxis.AxisNumTicks = 8;

        series_1 = graph.addSeries();
        series_2 = graph.addSeries();
        SetSeries(series_1, seriesDatas_1, Color.yellow);
        SetSeries(series_2, seriesDatas_2, Color.white);
    }

    private void SetSeries(WMG_Series _series,List<string> _seriesData,Color _color)
    {
        List<string> groups = new List<string>();
        List<Vector2> data = new List<Vector2>();
        _series.lineColor = _color;
        for (int i = 0; i < _seriesData.Count; i++)
        {
            string[] row = _seriesData[i].Split(',');
            groups.Add(row[0]);
            if (!string.IsNullOrEmpty(row[1]))
            {
                float y = float.Parse(row[1]);
                data.Add(new Vector2(i + 1, y));
            }
        }
        _series.seriesName = "数据信息";
        _series.UseXDistBetweenToSpace = true;
        _series.pointValues.SetList(data);

        graph.groups.SetList(groups);
        graph.useGroups = true;

        graph.xAxis.LabelType = WMG_Axis.labelTypes.groups;
        graph.xAxis.AxisNumTicks = groups.Count;
    }
}
