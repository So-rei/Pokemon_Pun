using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PKMN_CALC.Utility;
using System.Reflection;
using System.IO;

namespace PKMN_CALC.Master
{
    /// <summary>
    /// マスタの初期ロード・デバッグ画面からのセーブロードクラス
    /// </summary>
    public static class _Master_Data
    {

#if DEBUG
        // 各種マスタデータ(JSONファイルから)を読み込む
        private const string PROJECT_DIR_PATH_TXT = "ProjectDirPath.txt";//実行exeと同じ場所にあるこのtextの1行目に、実行slnのフォルダパスがセットされる(シェル実行で保存される)。
        private static readonly string PROJECT_DIR_PATH = @"../../..";   //以下のマスタjsonファイル全てを置いておくフォルダ。↑のファイルが正しく存在する時はその値に更新される。
        private const string PROJECT_FILE_PATH = "Master/Master_Json";   //実行slnからマスタjsonフォルダパスまでの間
#else
        //シェルは一旦ストップ
        //ECHO $(ProjectDir) > ProjectDirPath.txt
        
        // 各種マスタデータ(JSONファイルから)を読み込む
        private const string PROJECT_DIR_PATH_TXT = "ProjectDirPath.txt";//実行exeと同じ場所にあるこのtextの1行目に、実行slnのフォルダパスがセットされる(シェル実行で保存される)。
        private static readonly string PROJECT_DIR_PATH = @"";   //以下のマスタjsonファイル全てを置いておくフォルダ。↑のファイルが正しく存在する時はその値に更新される。
        private const string PROJECT_FILE_PATH = "";   //実行slnからマスタjsonフォルダパスまでの間
#endif
        //private static readonly string MASTER_EACHWAZA_JSON = "Master_EachPokeCanUseWaza.json";
        //private static readonly string MASTER_ITEM_JSON = "Master_Item.json";
        private static readonly string MASTER_POKEMON_JSON = "Master_Pokemon.json";
        //private static readonly string MASTER_RANK_JSON = "Master_Rank.json";
        //private static readonly string MASTER_SEIKAKU_JSON = "Master_Seikaku.json";
        //private static readonly string MASTER_TYPE_JSON = "Master_Type.json";
        //private static readonly string MASTER_WAZA_JSON = "Master_Waza.json";

        /// <summary>
        /// 全マスタを管理するID
        /// "特性マスタ", "覚えるわざマスタ", "アイテムマスタ", "ポケモンマスタ", "ランクマスタ", "性格マスタ", "タイプマスタ", "わざマスタ", "わざ特殊処理マスタ"
        /// </summary>
        public enum Master_ID
        {
            pokemon, tokusei, item, rank, seikaku, type, waza, wazasp, eachpokecanusewaza,
        }

        //公開する各種マスタプロパティ
        public static IEnumerable<Master_Pokemon> MASTER_POKEMON_LIST { get; private set; }
        //public List<Master_Ability> cMaster_Ability;
        //public static IEnumerable<Master_Item> MASTER_ITEM_LIST { get; private set; }
        //public static IEnumerable<Master_Rank> MASTER_RANK_LIST { get; private set; }
        //public static IEnumerable<Master_Seikaku> MASTER_SEIKAKU_LIST { get; private set; }
        //public static IEnumerable<Master_Type> MASTER_TYPE_LIST { get; private set; }
        //public static IEnumerable<Master_Waza> MASTER_WAZA_LIST { get; private set; }
        ////覚える技マスタはポケモンマスタ、わざマスタを参照するのでその２つの後にロードすること！
        //public static IEnumerable<Master_EachPokeCanUseWaza> MASTER_EACHWAZA_LIST { get; private set; }
        ////public IEnumerable<Master_Waza_Sp> MASTER_WAZA_LIST_Sp { get; set; }




        /// <summary>
        /// 静的コンストラクタ
        /// </summary>
        static _Master_Data()
        {
            PROJECT_DIR_PATH = GetProjectFolder();
            FileInit();// マスタデータは多いので、通常は明示的に起動時にロード
        }

        /// <summary>
        /// 明示的に起動時にロードする時用
        /// </summary>
        public static void Load_Data()
        {
            FileInit();// マスタデータは多いので、通常は明示的に起動時にロード
        }

        /// <summary>
        /// ビルド構成に依存しない実行パスを取得
        /// </summary>
        /// <returns></returns>
        private static string GetProjectFolder()
        {
            string projectDirPath = PROJECT_DIR_PATH;
            if (File.Exists(PROJECT_DIR_PATH_TXT))
            {
                using (var sr = new StreamReader(PROJECT_DIR_PATH_TXT, System.Text.Encoding.GetEncoding("shift_jis")))
                {
                    projectDirPath = sr.ReadToEnd().Trim('\\', ' ', '\r', '\n');
                }
            }
            return Path.Combine(projectDirPath, PROJECT_FILE_PATH);
        }

        /// <summary>
        /// ファイルデータ→マスタ　一括ロード
        /// マスタデータは多いので、通常は明示的に起動時にロード
        /// </summary>
        public static void FileInit()
        {
            //MASTER_ITEM_LIST = Serialize.FileLoadAndDeserialize(MASTER_ITEM_LIST, Path.Combine(PROJECT_DIR_PATH, MASTER_ITEM_JSON)) as IEnumerable<Master_Item>;
            MASTER_POKEMON_LIST = Serialize.FileLoadAndDeserialize(MASTER_POKEMON_LIST, Path.Combine(PROJECT_DIR_PATH, MASTER_POKEMON_JSON)) as IEnumerable<Master_Pokemon>;
            //MASTER_RANK_LIST = Serialize.FileLoadAndDeserialize(MASTER_RANK_LIST, Path.Combine(PROJECT_DIR_PATH, MASTER_RANK_JSON)) as IEnumerable<Master_Rank>;
            //MASTER_SEIKAKU_LIST = Serialize.FileLoadAndDeserialize(MASTER_SEIKAKU_LIST, Path.Combine(PROJECT_DIR_PATH, MASTER_SEIKAKU_JSON)) as IEnumerable<Master_Seikaku>;
            //MASTER_TYPE_LIST = Serialize.FileLoadAndDeserialize(MASTER_TYPE_LIST, Path.Combine(PROJECT_DIR_PATH, MASTER_TYPE_JSON)) as IEnumerable<Master_Type>;
            //MASTER_WAZA_LIST = Serialize.FileLoadAndDeserialize(MASTER_WAZA_LIST, Path.Combine(PROJECT_DIR_PATH, MASTER_WAZA_JSON)) as IEnumerable<Master_Waza>;
            //MASTER_EACHWAZA_LIST = Serialize.FileLoadAndDeserialize(MASTER_EACHWAZA_LIST, Path.Combine(PROJECT_DIR_PATH, MASTER_EACHWAZA_JSON)) as IEnumerable<Master_EachPokeCanUseWaza>;
        }



        //以下はマスタデータを編集画面から作成するときに使用する関数-------------------------------------------------------------------------------------------------------------------------------

        /// <summary>
        /// (画面で)指定したマスタIDから、マスタを取得
        /// </summary>
        /// <param name="m_id">マスタID</param>
        /// <param name="bRet"></param>
        /// <returns></returns>
        public static IEnumerable<object> LoadMaster(Master_ID m_id, ref bool bRet)
        {
            bRet = true;

            switch (m_id)
            {
                //case Master_ID.item:
                //    return MASTER_ITEM_LIST;
                case Master_ID.pokemon:
                    return MASTER_POKEMON_LIST;
                //case Master_ID.rank:
                //    return MASTER_RANK_LIST ;
                //case Master_ID.seikaku:
                //    return MASTER_SEIKAKU_LIST;
                //case Master_ID.type:
                //    return MASTER_TYPE_LIST;
                //case Master_ID.waza:
                //    return MASTER_WAZA_LIST;
                //case Master_ID.eachpokecanusewaza:
                //    return MASTER_EACHWAZA_LIST;
                default:
                    //エラー
                    bRet = false;
                    return null;
            }
        }

        ///// <summary>
        ///// 指定したID・データからマスタを取得し、ファイルにシリアライズ
        ///// </summary>
        ///// <param name="m_id">マスタID</param>
        ///// <param name="cMT">セットしたいマスタIDのデータ(IEnumerable<T>を実装していること)</param>
        ///// <param name="bRet"></param>
        //public static void SaveMaster(Master_ID m_id, object cMT, ref bool bRet)
        //{
        //    bRet = true;

        //    switch (m_id)
        //    {
        //        case Master_ID.item:
        //            MASTER_ITEM_LIST = cMT as IEnumerable<Master_Item>;
        //            Serialize.SerializeAndFileSave(Path.Combine(PROJECT_DIR_PATH, MASTER_ITEM_JSON), MASTER_ITEM_LIST);
        //            break;
        //        case Master_ID.pokemon:
        //            MASTER_POKEMON_LIST = cMT as IEnumerable<Master_Pokemon>;
        //            Serialize.SerializeAndFileSave(Path.Combine(PROJECT_DIR_PATH, MASTER_POKEMON_JSON), MASTER_POKEMON_LIST);
        //            break;
        //        case Master_ID.rank:
        //            MASTER_RANK_LIST = cMT as IEnumerable<Master_Rank>;
        //            Serialize.SerializeAndFileSave(Path.Combine(PROJECT_DIR_PATH, MASTER_RANK_JSON), MASTER_RANK_LIST);
        //            break;
        //        case Master_ID.seikaku:
        //            MASTER_SEIKAKU_LIST = cMT as IEnumerable<Master_Seikaku>;
        //            Serialize.SerializeAndFileSave(Path.Combine(PROJECT_DIR_PATH, MASTER_SEIKAKU_JSON), MASTER_SEIKAKU_LIST);
        //            break;
        //        case Master_ID.type:
        //            MASTER_TYPE_LIST = cMT as IEnumerable<Master_Type>;
        //            Serialize.SerializeAndFileSave(Path.Combine(PROJECT_DIR_PATH, MASTER_TYPE_JSON), MASTER_TYPE_LIST);
        //            break;
        //        case Master_ID.waza:
        //            MASTER_WAZA_LIST = cMT as IEnumerable<Master_Waza>;
        //            Serialize.SerializeAndFileSave(Path.Combine(PROJECT_DIR_PATH, MASTER_WAZA_JSON), MASTER_WAZA_LIST);
        //            break;
        //        case Master_ID.eachpokecanusewaza:
        //            MASTER_EACHWAZA_LIST = cMT as IEnumerable<Master_EachPokeCanUseWaza>;
        //            Serialize.SerializeAndFileSave(Path.Combine(PROJECT_DIR_PATH, MASTER_EACHWAZA_JSON), MASTER_EACHWAZA_LIST);
        //            break;
        //        default:
        //            //エラー
        //            bRet = false;
        //            return;
        //    }
        //}
    }

}
