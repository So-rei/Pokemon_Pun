using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using static PKMN_CALC.Master._Master_Common;
using static PKMN_CALC.Master._Master_Data;
using Newtonsoft.Json;

namespace PKMN_CALC.Master
{
    /// <summary>
    /// ポケモンマスタ
    /// </summary>
    public class Master_Pokemon
    {
        //プロパティ
        public int Index { get; set; } //通しNo
        public int PokeDex_Index { get; set; } //図鑑No
        public int Form { get; set; }//必須でない 通常1 フォルムチェンジ=2以上
        public string Form_JPN { get; set; } //(アローラのすがた)　など
        public string M_POKENAME_JPN { get; set; } //日本語名
        public string M_POKENAME_ENG { get; set; } //英語名
        public int Genelation { get; set; } //初登場世代
        public eTypeNo M_TYPE_1 { get; set; }
        public eTypeNo? M_TYPE_2 { get; set; }//タイプ2は無いことがある
        public string Ability_1_JPN { get; set; } //特性1
        public string Ability_2_JPN { get; set; } //特性2
        public string Ability_3_JPN { get; set; } //特性3
        public int HP { get; set; } = 0;
        public int Attack { get; set; } = 0;
        public int Defense { get; set; } = 0;
        public int Sp_Atk { get; set; } = 0;
        public int Sp_Def { get; set; } = 0;
        public int Speed { get; set; } = 0;
        public int Sex_Per { get; set; } //性別：0性別なし　1♂のみ　2♂7:♀1　3♂1:♀1　4♂1：♀7　5♀のみ
        public int Juvenile { get; set; }//進化段階 1 = *段階進化の1段目
        public int Juvenile_Max { get; set; }//進化段階 3 = 3段階進化
        public string Legendary { get; set; }//通常=なし 準伝="pre" 伝="leg" 幻="ill"
        public int Mega { get; set; }//メガシンカポケモン=1
        public int Appearance_ShSw { get; set; } //剣盾に登場しない=0 登場=1,鎧=2,冠=3
        public int Gen9Spare { get; set; }//9世代用予備

        //HABCDSはStatusクラスとして運用する
        [JsonIgnore]
        public Status M_ST_SYUZOKU
        {
            get
            {
                return new Status((HP, Attack, Defense, Sp_Atk, Sp_Def, Speed));//ステータスセット(HABCDS) シリアライズ処理関数で一旦objectを介しているので、タプル等では名前解決が出来なくなる
            }
        }

    }
}
