using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;
using static System.Console;
using PKMN_CALC.Master;
using static PKMN_CALC.Master._Master_Data;
using System.Reflection;

namespace PKMN_CALC.Utility
{
    /// <summary>
    /// シリアライズ基本クラス
    /// </summary>
    public static class Serialize
    {
        /// <summary>
        /// jsonファイル→マスタの方に対応した形でデシリアライズ→List<T>に返却
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="jMaster"></param>
        public static IEnumerable<T> FileLoadAndDeserialize<T>(IEnumerable<T> T1, string filename) where T:class
        {
            try
            {
                var text = File.ReadAllText(@filename, System.Text.Encoding.GetEncoding("utf-8"));
                return JsonConvert.DeserializeObject<IEnumerable<T>>(text);
            }
            catch (Exception ex)
            {
                //OutputErrorLog(typeof(Serialize).Name + "," + MethodBase.GetCurrentMethod().Name, ex.Message.ToString());
                return null;
            }
        }

        /// <summary>
        /// List<>をシリアライズ→ファイルに保存
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="savelist"></param>
        public static void SerializeAndFileSave<T>(string filepath,T cMT) where T: System.Collections.IEnumerable
        {
            try
            {
                //ストリームライターでファイルに保存
                System.IO.StreamWriter sw = new System.IO.StreamWriter(filepath, false, System.Text.Encoding.GetEncoding("utf-8"));
                sw.Write(JsonConvert.SerializeObject(cMT, Formatting.Indented));
                sw.Close();
            }
            catch (Exception ex)
            {
                //OutputErrorLog(typeof(Serialize).Name + "," + MethodBase.GetCurrentMethod().Name, ex.Message.ToString());
            }
        }
    }


}
