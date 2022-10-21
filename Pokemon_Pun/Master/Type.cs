using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.Reflection;
using static PKMN_CALC.Master._Master_Common;
using static PKMN_CALC.Master._Master_Data;
using PKMN_CALC.Utility;

namespace PKMN_CALC.Master
{
    //マスタランク
    public class Master_Type 
    {
        //プロパティ
        public eTypeNo M_TYPENO { get; set; }
        public string M_TYPENAME_JPN { get; set; }
        public string M_TYPENAME_ENG { get; set; }
        public double M_NORMAL { get ; set; }
        public double M_HONOO { get; set; }
        public double M_MIZU { get; set; }
        public double M_DENKI { get; set; }
        public double M_KUSA { get; set; }
        public double M_KOORI { get; set; }
        public double M_KAKUTOU { get; set; }
        public double M_DOKU { get; set; }
        public double M_JIMEN { get; set; }
        public double M_HIKOU { get; set; }
        public double M_ESP { get; set; }
        public double M_MUSI { get; set; }
        public double M_IWA { get; set; }
        public double M_GHOST { get; set; }
        public double M_DRAGON { get; set; }
        public double M_AKU { get; set; }
        public double M_HAGANE { get; set; }
        public double M_FAIRY { get; set; }

        public static eTypeNo GetCode(string TypeStr)
        {
            switch (TypeStr)
            {
                case "ノーマル":
                    return eTypeNo.M_NORMAL;
                case "ほのお":
                case "炎":
                    return eTypeNo.M_HONOO;
                case "みず":
                case "水":
                    return eTypeNo.M_MIZU;
                case "でんき":
                case "電":
                    return eTypeNo.M_DENKI;
                case "くさ":
                case "草":
                    return eTypeNo.M_KUSA;
                case "こおり":
                case "氷":
                    return eTypeNo.M_KOORI;
                case "かくとう":
                case "闘":
                    return eTypeNo.M_KAKUTOU;
                case "どく":
                case "毒":
                    return eTypeNo.M_DOKU;
                case "じめん":
                case "地":
                    return eTypeNo.M_JIMEN;
                case "ひこう":
                case "飛":
                    return eTypeNo.M_HIKOU;
                case "エスパー":
                case "超":
                    return eTypeNo.M_ESP;
                case "むし":
                case "虫":
                    return eTypeNo.M_MUSI;
                case "いわ":
                case "岩":
                    return eTypeNo.M_IWA;
                case "ゴースト":
                case "霊":
                    return eTypeNo.M_GHOST;
                case "ドラゴン":
                case "龍":
                    return eTypeNo.M_DRAGON;
                case "あく":
                case "悪":
                    return eTypeNo.M_AKU;
                case "はがね":
                case "鋼":
                    return eTypeNo.M_HAGANE;
                case "フェアリー":
                    return eTypeNo.M_FAIRY;
                default:
                    return eTypeNo.M_NULL;
            }
        }
    }
    public enum eTypeNo
    {
        M_NORMAL = 0,
        M_HONOO,
        M_MIZU,
        M_DENKI,
        M_KUSA,
        M_KOORI,
        M_KAKUTOU,
        M_DOKU,
        M_JIMEN,
        M_HIKOU,
        M_ESP,
        M_MUSI,
        M_IWA,
        M_GHOST,
        M_DRAGON,
        M_AKU,
        M_HAGANE,
        M_FAIRY,
        M_NULL //タイプなし　燃え尽きる・わるあがきなど用
    }

    /// <summary>
    /// タイプデータ*******************************************************************************************************
    /// 無効0 いまひとつ0.5 通常1またはnull 抜群2
    /// </summary>
   
}

