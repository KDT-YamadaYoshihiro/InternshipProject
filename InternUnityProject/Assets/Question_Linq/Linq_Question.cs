using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Linq_Question
{
	List<ActorData> actorDataList = null;
	List<EquipData> equipDataList = null;
	List<EquipTypeData> equipTypeDataList = null;
    /*=========================================================================*/
    /*
	 * メンバ変数で用意されたマスターデータリストを使い問題に答えよ
	 * 問題には Linq を使い解くこと
	 * 大問１はコード記述のみで良い。それ移行は「OutputLog」で結果をログ出力する事
	 *	ログの確認は Editor のメニュー「Intern/Execute Linq」で行えます
	 * 
	 * [用語]
	 *	戦闘力について	
	 *		Actorの基本パラメータと装備している装備のパラメータの合算値
	 *
	 *	強さについて	
	 *		パラメータの合算値
	 */
    /*=========================================================================*/
    /* 例 */
    /*{
		var list = hogeList
			.HogeHoge()
			.ToList();
		OutputLog(list);
	}*/
    public void Execute()
	{
		actorDataList = ActorData.TestData;
		equipDataList = EquipData.TestData;
		equipTypeDataList = EquipTypeData.TestData;

		//Q1();
		//Q2();
		//Q3();
		//Q4();
		Q5();
	}

	void Q1()
	{
        /*=========================================================================*/
        /*「大問１」*/
        /*
		 * 「Linq/ラムダを知る」「IEnumerable/Collection」「Select, Where を使う」
		 * 内容自体はリンクとラムダを知らない人用
		 * スキルやスケジュール等を考慮してスキップしても良い
		 */
        /*=========================================================================*/
        /* 「１－１」*/
        /*
		 * 「Actor.Name」が格納されている List<string> を作りなさい
		 */
        {
			var list = actorDataList
				.Select(actor => actor.Name).ToList();
        }
        /* 「１－２」*/
        /*
		 * 「Actor.Alignment = 2」の 「Actor.Name」が格納されている List<string> を作りなさい
		 */
        {
			var list  = actorDataList
				.Where(actor => actor.Alignment == 2)
				.Select(actor => actor.Name)
				.ToList();
        }
        /* 「１－３」*/
        /*
		 * 「Actor.Alignment = 2」の 「Actor.Name」が格納されている List<string> を作りなさい
		 * 「Actor.Alignment = 2」の 「Actor.Id」が格納されている List<int> を作りなさい
		 */
        {
			var actorList = actorDataList
				.Where(actor => actor.Alignment == 2)
				.ToList(); // ←速度を重視するなら
			var nameList = actorList
				.Select (actor => actor.Name)
				.ToList();

			var idList = actorList
				.Select(actor => actor.Id)
				.ToList();
        }
	}
	void Q2()
	{
        /*=========================================================================*/
        /*大問２*/
        /*
		 * 「ソート」
		 */
        /*=========================================================================*/
        /* 「２－１」*/
        /* 「Actor.Atk」が一番低い「Actor.Name」を表示しなさい。
		 */
        {
			var minAtkActor = actorDataList.OrderBy(actor => actor.Atk).FirstOrDefault();
			if(minAtkActor != null)
			{
				OutputLog(minAtkActor.Name);
			}
        }
        /* 「２－２」*/
        /* 「Actor.Alignment = 3」の中で「Actor.Hp」が一番高い Actor の名前を表示しなさい。
		 */
        {
			var maxHpActor = actorDataList.Where(actor => actor.Alignment == 3)
				.OrderByDescending(actor => actor.Hp)
				.FirstOrDefault();

			if(maxHpActor != null)
			{
				OutputLog(maxHpActor.Name);
			}

        }
        /* 「２－３」*/
        /* 「Actor.Atk」が大きい順に並べたリストを表示しなさい
         * 「Actor.Atk」の値が同じ場合は「Actor.Id」が低い順にしなさい
		 */
        {
			var atkActorList = actorDataList.OrderByDescending(actor => actor.Atk)
				.ThenBy(actor => actor.Id);

			//if(atkActorList != null)
			//{
			//	OutputLog(atkActorList.Name);
			//}

			foreach(var actor in atkActorList)
			{
				OutputLog($"{actor.Name} (Atk: {actor.Atk}, Id: {actor.Id})");
			}

        }
	}
	void Q3()
	{
        /*=========================================================================*/
        /*大問３*/
        /*
		 * 別マスターデータとの紐付けを行う
		 * Linq の順番を気をつける
		 */
        /*=========================================================================*/
        /* 「３－１」*/
        /*
		 * 武器タイプ(「EquipTypeData.ID = 1」)の「EquipTypeData.Name」を表示しなさい
		 * その装備タイプでつよさが低い順に並び替えを行ったつよさを格納した List<int> を作成し表示しなさい
		 */
        {
			var equipList = equipDataList
				.Where(equip => equip.Type == 1)
				.OrderBy(equip => equip.Atk + equip.Def)
				.ToList();

			// ログ出力	
			foreach (var equip in equipList)
			{
				OutputLog($"装備名: {equip.Name}");

			}

			var strengList = equipList
				.Select(equit => equit.Atk + equit.Def)
				.ToList();

			foreach (var streng in strengList)
			{
				OutputLog(streng.ToString());
			}

        }
        /* 「３－２」*/
        /*
		 * 足装備タイプ(「EquipTypeData.Id = 4」)の「EquipTypeData.Name」を表示しなさい
		 * その装備タイプでつよさが一番高い装備の「EquipData.Name」を表示しなさい
		 */
        {
			// 足装備はId = 5である。
            var equip = equipDataList.Where(equip => equip.Type == 5)
                .OrderByDescending(equip => equip.Atk + equip.Def)
                .FirstOrDefault();

			if(equip != null)
			{
				OutputLog(equip.Name);
			}
        }
    }
	void Q4()
	{
        /*=========================================================================*/
        /*大問４*/
        /*
		 * 実践 / 拡張メソッド
		 */
        /*=========================================================================*/
        /* 「４－１」*/
        /*
		 * 「Actor.Id = 1」が「EquipData.Id = 1」「EquipData.Id = 2」を装備しているときの戦闘力を表示しなさい
		 */
        {
			// キャラの取り出し
			var actor = actorDataList.FirstOrDefault(act => act.Id == 1);

			// 装備時の増加値
			var equipSum = equipDataList
				.Where(equip => equip.Id == 1 || equip.Id == 2)
				.Sum(equip => equip.Atk + equip.Def);

			// 戦闘力計算と表示
			if(actor != null)
			{
				int combatPower = actor.GetCombatPower(equipSum);
				OutputLog($"戦闘力:{combatPower}");

            }
			

        }
	}
	void Q5()
	{
        /*=========================================================================*/
        /*大問５*/
        /*
		 * 実践
		 */
        /*=========================================================================*/
        /* 「５－１」*/
        /*
		 * 「Actor.Alignment」の値が同じものの中でつよさが高い or 低いオブジェクトを抽出したリストを返す関数を作成しなさい
		 * 「Actor.Alignment」と「高い or 低い」は引数として指定することで変更可能なようにする
		 * つよさの値が同じ場合は「Actor.Id」が低い順にしなさい
		*/
        {
            /// <summary>
            /// つよさが高い or 低いオブジェクトを抽出したリストを返す関数
			/// arg_alignment
			/// arg_isDesc / true : 高い順, false : 低い順
            /// </summary>
            List<ActorData> GetOrderedActorList(int arg_alignment, bool arg_isDesc)
			{
				var actorList = actorDataList
					.Where(actor => actor.Alignment == arg_alignment);

				IOrderedEnumerable<ActorData> sorted;

				if (arg_isDesc)
				{
					sorted = actorList.OrderByDescending(actor => actor.Atk + actor.Def);
				}
				else
				{
					sorted = actorList.OrderBy(actor => actor.Atk + actor.Def);
				}

				return sorted.ThenBy(actor => actor.Id).ToList();


                //if(arg_isDesc)
                //{
                //                actorList = actorList.OrderByDescending(actor => actor.Atk + actor.Def)
                //		.ThenBy(actor => actor.Id);
                //}
                //else
                //{
                //                actorList = actorList.OrderBy(actor => actor.Atk + actor.Def)
                //		.ThenBy(actor => actor.Id);
                //            }
                //return actorList.ToList();

            }

			//Debug.Log(GetOrderedActorList(1, true));
			//Debug.Log(GetOrderedActorList(1, false));

			var results = GetOrderedActorList(1, true);
			foreach (var result in results)
			{
				OutputLog($"{result.Name}.強さ: {result.Atk + result.Def}");
			}

			//var results2 = GetOrderedActorList(1, false);
			//foreach (var result in results2)
			//{
			//	OutputLog($"{result.Name}.強さ: {result.Atk + result.Def}");
			//}

		}
	}


	[System.Diagnostics.Conditional("UNITY_EDITOR")]
	void OutputLog(object message)
	{
		Debug.Log(message);
	}
#if UNITY_EDITOR
	[UnityEditor.MenuItem("Intern/Execute Linq")]
	static void __ExecuteLinq()
	{
		new Linq_Question().Execute();
	}
#endif
}

public static class ActorExtensions
{
	public static int GetCombatPower(this ActorData arg_actor, int arg_wquipSum)
	{
		if(arg_actor == null)
		{
			return 0;
		}
		return (arg_actor.Atk + arg_actor.Def) + arg_wquipSum;
	}

}