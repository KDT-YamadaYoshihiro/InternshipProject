using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipData
{
	public int Id;
	public string Name;
	public int Type;
	public int Atk;
	public int Def;

	public EquipData(
		int Id
		, string Name
		, int Type
		, int Atk
		, int Def
		)
	{
		this.Id = Id;
		this.Name = Name;
		this.Type = Type;
		this.Atk = Atk;
		this.Def = Def;
	}
	public override string ToString()
	{
		return $"{Id}:{Name}";
	}

	public static List<EquipData> TestData = new List<EquipData>()
	{
		new EquipData(1,     "かたな",                   1,5,0)
		,new EquipData(2,    "かわのよろい（あたま）",    2,0,1)
		,new EquipData(3,    "かわのよろい（からだ）",    3,0,2)
		,new EquipData(4,    "かわのよろい（うで）",      4,0,1)
		,new EquipData(5,    "かわのよろい（あし）",      5,0,1)
		,new EquipData(6,    "どうのよろい（あたま）",    2,0,2)
		,new EquipData(7,    "どうのよろい（からだ）",    3,0,3)
		,new EquipData(8,    "どうのよろい（うで）",      4,0,2)
		,new EquipData(9,    "どうのよろい（あし）",      5,0,2)
		,new EquipData(10,   "てつのよろい（あたま）",    2,0,3)
		,new EquipData(11,   "てつのよろい（からだ）",    3,0,4)
		,new EquipData(12,   "てつのよろい（うで）",      4,0,3)
		,new EquipData(13,   "てつのよろい（あし）",      5,0,3)
		,new EquipData(14,   "ひのきのぼう",             1,1,0)
	};
}