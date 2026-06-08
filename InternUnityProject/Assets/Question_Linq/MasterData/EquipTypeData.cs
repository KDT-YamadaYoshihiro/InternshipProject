using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipTypeData
{
	public int Id;
	public int Type;
	public string Name;

	public EquipTypeData(
		int Id
		, int Type
		, string Name
		)
	{
		this.Id = Id;
		this.Type = Type;
		this.Name = Name;
	}

	public static List<EquipTypeData> TestData = new List<EquipTypeData>()
	{
		new EquipTypeData(  1,  1,  "武器")
		,new EquipTypeData( 2,  2,  "頭装備")
		,new EquipTypeData( 3,  3,  "体装備")
		,new EquipTypeData( 4,  4,  "腕装備")
		,new EquipTypeData( 5,  5,  "足装備")
	};
}