using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

/// <summary>
/// 读取表格
/// </summary>
public class LoadTable : MonoBehaviour
{
    private string filePath;
    private HSSFWorkbook wk;
    private FileStream fs;          //文件流

    private ISheet sheet;           //工作表
    private IRow row;               //行
    private ICell cell;             //列

    private void Awake()
    {
        filePath = Application.dataPath + "/CustomGF/NPOI/david.xls";
    }

    private void Start()
    {
        LoadExcel();
        GetAllDatas();
    }

    /// <summary>
    /// 创建表格
    /// </summary>
    private void CreateExcel()
    {
        Debug.Log(1);
        wk = new HSSFWorkbook();
        sheet = wk.CreateSheet("mySheet");
        Debug.Log(2);

        for (int i = 0; i <= 20; i++)
        {
            row = sheet.CreateRow(i);
            cell = row.CreateCell(0);
            cell.SetCellValue(i);
        }

        fs = File.Create(filePath);
        wk.Write(fs);
        fs.Close();
        fs.Dispose();
        Debug.Log("创建表格成功");
    }

    /// <summary>
    /// 读取表格
    /// </summary>
    private void LoadExcel()
    {
        fs = File.OpenRead(filePath);
        wk = new HSSFWorkbook(fs);
        sheet = wk.GetSheetAt(0);
        for (int j = 1; j <= sheet.LastRowNum; j++)
        {
            row = sheet.GetRow(j);
            if (row != null)
            {
                for (int k = 0; k < row.LastCellNum; k++)
                {
                    //Debug.Log(row.GetCell(k).ToString());
                    results.Add(row.GetCell(k).ToString());
                }
            }
        }
    }

    /// <summary>
    /// 获取所有数据
    /// </summary>
    private void GetAllDatas()
    {
        for (int i = 0; i < results.Count; i += 6)
        {
            ItemInfo item;
            item.name = results[i];
            item.attack = int.Parse(results[i+1]);
            item.crit = int.Parse(results[i + 2]);
            item.panetrate = int.Parse(results[i + 3]);
            item.hp = int.Parse(results[i + 4]);
            item.mp = int.Parse(results[i + 5]);
            itemInfos.Add(item);
        }

        for (int i = 0; i < itemInfos.Count; i++)
        {
            Debug.Log(string.Format("物品名称:{0} 攻击力:{1} 暴击:{2} 穿透:{3} 血:{4} 蓝:{5}", 
                itemInfos[i].name, itemInfos[i].attack, itemInfos[i].crit, 
                itemInfos[i].panetrate, itemInfos[i].hp, itemInfos[i].mp));
        }
    }

    /// <summary>
    /// 接收结果
    /// </summary>
    public List<string> results = new List<string>();

    /// <summary>
    /// 物品信息
    /// </summary>
    public List<ItemInfo> itemInfos = new List<ItemInfo>();

    public struct ItemInfo
    {
        public string name;
        public int attack;
        public int crit;
        public int panetrate;
        public int hp;
        public int mp;
    }
}
