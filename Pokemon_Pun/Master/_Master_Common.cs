using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PKMN_CALC.Utility;
using System.Reflection;
using System.Collections;
using Newtonsoft.Json;

namespace PKMN_CALC.Master
{

    /// <summary>
    /// 全マスタ共通で使用するコモンクラス
    /// </summary>
    public static class _Master_Common
    {
        static internal string sErrorPlace = typeof(_Master_Common).FullName + "," + MethodBase.GetCurrentMethod().Name;

        ///// <summary>
        ///// nullやstringの""を0に変換してint型で返す
        ///// マスタ内でしか使用しない
        ///// </summary>
        ///// <param name="j">object</param>
        ///// <returns>int</returns>
        //public static int CIntZero(object j)
        //{
        //    if (j is int) return (int)j;
        //    if (j is string && int.TryParse((string)j, out int i)) return i;

        //    return 0;
        //}

        ///// <summary>
        ///// タイプ倍率変換
        ///// </summary>
        ///// <param name="OFFeNo">攻撃側タイプ</param>
        ///// <param name="DEFeNo">防御側タイプ</param>
        ///// <returns></returns>
        //public static double GetTypeBairitu(eTypeNo OFFeNo, eTypeNo DEFeNo)
        //{
        //    var z = _Master_Data.MASTER_TYPE_LIST.Single(r => r.M_TYPENO == OFFeNo);

        //    switch (DEFeNo)
        //    {
        //        case eTypeNo.M_NORMAL:
        //            return z.M_NORMAL;
        //        case eTypeNo.M_HONOO:
        //            return z.M_HONOO;
        //        case eTypeNo.M_MIZU:
        //            return z.M_MIZU;
        //        case eTypeNo.M_DENKI:
        //            return z.M_DENKI;
        //        case eTypeNo.M_KUSA:
        //            return z.M_KUSA;
        //        case eTypeNo.M_KOORI:
        //            return z.M_KOORI;
        //        case eTypeNo.M_KAKUTOU:
        //            return z.M_KAKUTOU;
        //        case eTypeNo.M_DOKU:
        //            return z.M_DOKU;
        //        case eTypeNo.M_JIMEN:
        //            return z.M_JIMEN;
        //        case eTypeNo.M_HIKOU:
        //            return z.M_HIKOU;
        //        case eTypeNo.M_ESP:
        //            return z.M_ESP;
        //        case eTypeNo.M_MUSI:
        //            return z.M_MUSI;
        //        case eTypeNo.M_IWA:
        //            return z.M_IWA;
        //        case eTypeNo.M_GHOST:
        //            return z.M_GHOST;
        //        case eTypeNo.M_DRAGON:
        //            return z.M_DRAGON;
        //        case eTypeNo.M_AKU:
        //            return z.M_AKU;
        //        case eTypeNo.M_HAGANE:
        //            return z.M_HAGANE;
        //        case eTypeNo.M_FAIRY:
        //            return z.M_FAIRY;
        //    }

        //    OutputErrorLog(sErrorPlace, "異常な値");
        //    return -1;
        //}

        /// <summary>
        /// ステータス順　あちこちで使う
        /// </summary>
        public enum Stjyun
        {
            H = 0,
            A,
            B,
            C,
            D,
            S
        }
        
        /// <summary>
        /// ステータスを扱うクラス
        /// </summary>
        public class Status
        {
            private int[] _status = new int[6] { 0, 0, 0, 0, 0, 0 };

            //コンストラクタ　初期化
            public Status()
            {
                _status = new int[6] { 0, 0, 0, 0, 0, 0 };
            }
            //コンストラクタ   IEnumerable型
            public Status(IEnumerable<int> InitData = null)
            {
                if (InitData != null && InitData.Count() == 6)
                {
                    this.ST_IE = InitData;
                }
                //else 
                    //OutputErrorLog(sErrorPlace, "入力エラー");
            }
            //コンストラクタ タプル型
            public Status((int tH, int tA, int tB, int tC, int tD, int tS) tap)
            {
                _status[(int)Stjyun.H] = tap.tH;
                _status[(int)Stjyun.A] = tap.tA;
                _status[(int)Stjyun.B] = tap.tB;
                _status[(int)Stjyun.C] = tap.tC;
                _status[(int)Stjyun.D] = tap.tD;
                _status[(int)Stjyun.S] = tap.tS;
            }

            //IEnumerable型公開クラス
            [JsonIgnore] //(重複するのでファイルには保存しない)
            public IEnumerable<int> ST_IE
            {
                get { return _status; }
                set
                {
                    _status = (int[])value;

                    int i = 0;
                    foreach (int ivalue in value)
                    {
                        switch ((Stjyun)(i))
                        {
                            case Stjyun.H:
                            case Stjyun.A:
                            case Stjyun.B:
                            case Stjyun.C:
                            case Stjyun.D:
                            case Stjyun.S:
                                _status[i] = ivalue;
                                break;
                            default:
                                //OutputErrorLog(GetType().FullName + "," + MethodBase.GetCurrentMethod().Name, "STATUS 入力異常");
                                break;
                        }
                        i++;
                    }
                }
            }

            //タプル型公開クラス　
            //[JsonIgnore] //(重複するのでファイルには保存しない)
            //public (int tH, int tA, int tB, int tC, int tD, int tS) ST_TAP
            //{
            //    get { return (_status[(int)Stjyun.H], _status[(int)Stjyun.A], _status[(int)Stjyun.B], _status[(int)Stjyun.C], _status[(int)Stjyun.D], _status[(int)Stjyun.S]); }
            //    set
            //    {
            //        _status[(int)Stjyun.H] = ST_TAP.tH;
            //        _status[(int)Stjyun.A] = ST_TAP.tA;
            //        _status[(int)Stjyun.B] = ST_TAP.tB;
            //        _status[(int)Stjyun.C] = ST_TAP.tC;
            //        _status[(int)Stjyun.D] = ST_TAP.tD;
            //        _status[(int)Stjyun.S] = ST_TAP.tS;
            //    }
            //}
            
            //配列型公開クラス
            [JsonIgnore] //(重複するのでファイルには保存しない)
            public int[] ST { get { return _status; } set { _status = value; } }

            //int型公開クラス(個別)            
            public int H { get { return _status[(int)Stjyun.H]; } set { _status[(int)Stjyun.H] = value; } }
            public int A { get { return _status[(int)Stjyun.A]; } set { _status[(int)Stjyun.A] = value; } }
            public int B { get { return _status[(int)Stjyun.B]; } set { _status[(int)Stjyun.B] = value; } }
            public int C { get { return _status[(int)Stjyun.C]; } set { _status[(int)Stjyun.C] = value; } }
            public int D { get { return _status[(int)Stjyun.D]; } set { _status[(int)Stjyun.D] = value; } }
            public int S { get { return _status[(int)Stjyun.S]; } set { _status[(int)Stjyun.S] = value; } }

            public IEnumerator<int> GetEnumerator()
            {
                return ST_IE.GetEnumerator();
            }
        }

        /// <summary>
        /// 場のすべての状態（ステータスを含む）（ステータス上下、ターン数、持ち物など）
        /// </summary>
        public class Situation
        {
            public Master_Pokemon POKE;//ポケモン名
            public Status ST;//ステータス
            public int UP_A;//ステータス上昇A
            public int UP_B;//ステータス上昇B
            public int UP_C;//ステータス上昇C
            public int UP_D;//ステータス上昇D
            public int UP_S;//ステータス上昇S
            public int REST_HP;//残HP
            //public Master_Item ITEM;//持ち物

            ////その他経過ターン系            
            //public bool FIRST = true;//出て初ターンである（ねこだまし、であいがしら、たたみがえし用）
            //public int SONG = -1;//ほろびのうた
            //public int POISON = -1;//どくどくターン数

            //特殊
            //充電、みずびたし、そうでん、てだすけなど
        }

        public class Field
        {
            ////共通フィールド状態
            //public IEnumerable<OwnField> FIELD;//追い風、トリックルーム、ねばねばネットなど、共通でない場状態
            //public TapuField FIELD_T;//サイコ、グラス、エレキ、フィールド
            //public Climate CL;//天候
            //public int TURN;//合計ターン数
        }
    }
}
