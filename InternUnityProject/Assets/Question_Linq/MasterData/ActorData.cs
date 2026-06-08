using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorData
{
	public int Id;
	public string Name;
	public int Alignment;
	public int Atk;
	public int Def;
	public int Int;
	public int Dex;
	public int Agi;
	public int Luk;
	public int Mov;
    public int Hp;

	public ActorData(
		int Id
		, string Name
		, int Alignment
		, int Atk
		, int Def
		, int Int
		, int Dex
		, int Agi
		, int Luk
		, int Mov
        , int Hp
        )
    {
		this.Id = Id;
		this.Name = Name;
		this.Alignment = Alignment;
		this.Atk = Atk;
		this.Def = Def;
		this.Int = Int;
		this.Dex = Dex;
		this.Agi = Agi;
		this.Luk = Luk;
		this.Mov = Mov;
		this.Hp = Hp;
    }

    public ActorData(
    ActorData data
    )
    {
        this.Id = data.Id;
        this.Name = data.Name;
        this.Alignment = data.Alignment;
        this.Atk = data.Atk;
        this.Def = data.Def;
        this.Int = data.Int;
        this.Dex = data.Dex;
        this.Agi = data.Agi;
        this.Luk = data.Luk;
        this.Mov = data.Mov;
        this.Hp = data.Hp;
    }

    public override string ToString()
	{
		return $"{Id}:{Name}";
	}

    public static List<ActorData> TestData = new List<ActorData>()
    {
        new     ActorData(1,   "ゴブリン1",    1,  60, 60, 5, 2,2,2,4, 350)
        ,new    ActorData(2,   "ゴブリン2",    1,  90, 60, 5, 2,2,2,3, 375)
        ,new    ActorData(3,   "ゴブリン3",    1, 120, 60, 5, 2,2,2,2, 400)

        ,new    ActorData(4,   "スライム1",    2,  90,120, 4, 3,3,3,4, 300)
        ,new    ActorData(5,   "スライム2",    2, 120, 90, 4, 3,3,3,3, 325)
        ,new    ActorData(6,   "スライム3",    2, 150, 90, 4, 3,3,3,2, 350)

        ,new    ActorData(7,   "ゴーレム1",    3, 150,120, 2, 4,4,4,4, 450)
        ,new    ActorData(8,   "ゴーレム2",    3, 180,120, 2, 4,4,4,3, 475)
        ,new    ActorData(9,   "ゴーレム3",    3, 210,120, 2, 4,4,4,2, 500)

        ,new    ActorData(10,  "ドワーフ1",    3,  60,120, 3, 2,4,2,4, 400)
        ,new    ActorData(11,  "ドワーフ2",    3,  90,120, 3, 2,4,2,3, 425)
        ,new    ActorData(12,  "ドワーフ3",    3, 120,120, 3, 2,4,2,2, 450)

        ,new    ActorData(13,  "グール1",      4, 120, 90, 2, 2,3,2,4, 350)
        ,new    ActorData(14,  "グール2",      4, 150, 90, 2, 2,3,2,3, 375)
        ,new    ActorData(15,  "グール3",      4, 180, 90, 2, 2,3,2,2, 400)
    };
}