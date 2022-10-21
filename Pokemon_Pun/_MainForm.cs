using PKMN_CALC.Master;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Reflection;
using static PKMN_CALC.Master._Master_Data;
using Newtonsoft.Json.Linq;

namespace Pokemon_Pun
{
    public partial class _MainForm : Form
    {
        List<Master_Pokemon> Master_Pokemons; //マスタデータ
        bool bDetailView = false;
        bool bCalcView = false;

        public _MainForm()
        {
            InitializeComponent();
            InitializeMaster();
        }
        
        void InitializeMaster()
        {
            bool bRet = false;
            var poke = LoadMaster(Master_ID.pokemon, ref bRet);

            if (!bRet)
            {
                MessageBox.Show("エラー。マスタファイルのロードに失敗しました。");
                return;
            }

            Master_Pokemons = ((IEnumerable<Master_Pokemon>)poke).ToList();
        }

        private void btnDetail_Click(object sender, EventArgs e)
        {
            panelDetail.Visible = bDetailView = !bDetailView;

            if (bDetailView)
            {
                btnDetail.Text = "－詳細設定";
                if (bCalcView)
                    panelDetail.Location = new Point(16, 426);
                else
                    panelDetail.Location = new Point(16, 185);
            }
            else
            {
                btnDetail.Text = "＋詳細設定";
            }
        }

        private void btnMainCalc_Click(object sender, EventArgs e)
        {
            if (txtGoro.Text.Length < 5 || txtGoro.Text.Length > 10)
            {
                MessageBox.Show("5文字以上10文字以内でよろしく！");
                return;
            }
            //if (numericUpDown1.Value > numericUpDown2.Value)
            //{
            //    MessageBox.Show("匹数範囲をあわせてください");
            //    return;
            //}

            ResetViewSet();

            StartMainCalc();

            ComboViewSet(txtGoro.Text.Length);

            panelCombo.Visible = bCalcView = true;
        }

        void ResetViewSet()
        {
            cb10.Visible = true;
            cb8_10.Visible = true;
            cb9_10.Visible = true;
            cb9.Visible = true;
            cb7_9.Visible = true;
            cb8_9.Visible = true;
            cb8.Visible = true;
            cb6_8.Visible = true;
            cb7_8.Visible = true;
            cb7.Visible = true;
            cb5_7.Visible = true;
            cb6_7.Visible = true;
            cb6.Visible = true;
            cb4_6.Visible = true;
            cb5_6.Visible = true;
            lbl10.Text = txtGoro.Text.Length > 9 ? txtGoro.Text[9].ToString() : "";
            lbl9.Text = txtGoro.Text.Length > 8 ? txtGoro.Text[8].ToString() : "";
            lbl8.Text = txtGoro.Text.Length > 7 ? txtGoro.Text[7].ToString() : "";
            lbl7.Text = txtGoro.Text.Length > 6 ? txtGoro.Text[6].ToString() : "";
            lbl6.Text = txtGoro.Text.Length > 5 ? txtGoro.Text[5].ToString() : "";
            lbl5.Text = txtGoro.Text[4].ToString();
            lbl4.Text = txtGoro.Text[3].ToString();
            lbl3.Text = txtGoro.Text[2].ToString();
            lbl2.Text = txtGoro.Text[1].ToString();
            lbl1.Text = txtGoro.Text[0].ToString();

            cb1.DataSource = null;
            cb2.DataSource = null;
            cb3.DataSource = null;
            cb4.DataSource = null;
            cb5.DataSource = null;
            cb1_2.DataSource = null;
            cb2_3.DataSource = null;
            cb3_4.DataSource = null;
            cb4_5.DataSource = null;
            cb1_3.DataSource = null;
            cb2_4.DataSource = null;
            cb3_5.DataSource = null;
            cb6.DataSource = null;
            cb5_6.DataSource = null;
            cb4_6.DataSource = null;
            cb7.DataSource = null;
            cb6_7.DataSource = null;
            cb5_7.DataSource = null;
            cb8.DataSource = null;
            cb7_8.DataSource = null;
            cb6_8.DataSource = null;
            cb9.DataSource = null;
            cb8_9.DataSource = null;
            cb7_9.DataSource = null;
            cb10.DataSource = null;
            cb9_10.DataSource = null;
            cb8_10.DataSource = null;
        }

        //メイン処理
        void StartMainCalc()
        {
            //詳細設定を元に、データを絞る--
            var leglist = new List<string>() { };
            if (checkBox1.Checked) leglist.Add("");
            if (checkBox2.Checked) leglist.Add("pre");
            if (checkBox3.Checked) leglist.Add("leg");
            if (checkBox4.Checked) leglist.Add("ill");
            bool isAllLeg = leglist.Count() == 4;
            //進化系
            //地方
            var poke_setend = new List<Master_Pokemon>();
            poke_setend = Master_Pokemons.Where(p => (isAllLeg || leglist.Contains(p.Legendary)) && p.Mega == 0 &&
                          ((chkShSw.Checked && p.Appearance_ShSw > 0) || (chkNotShSw.Checked && p.Appearance_ShSw == 0)) &&
                          (radJuv1.Checked || ((radJuv2.Checked && p.Juvenile == p.Juvenile_Max) || (radJuv3.Checked && p.Juvenile != p.Juvenile_Max))) &&
                          ((chkGen1.Checked && p.Genelation == 1) || (chkGen2.Checked && p.Genelation == 2) || (chkGen3.Checked && p.Genelation == 3) || (chkGen4.Checked && p.Genelation == 4) ||
                           (chkGen5.Checked && p.Genelation == 5) || (chkGen6.Checked && p.Genelation == 6) || (chkGen7.Checked && p.Genelation == 7) || (chkGen8.Checked && p.Genelation == 8))).ToList();

            if (rdoSort1.Checked) poke_setend = poke_setend.OrderBy(p => p.M_POKENAME_JPN).ToList();
            else if (rdoSort2.Checked) poke_setend = poke_setend.OrderByDescending(p => p.HP + p.Attack + p.Defense + p.Sp_Atk + p.Sp_Def + p.Speed).ToList();
            else if (rdoSort3.Checked) poke_setend = poke_setend.OrderByDescending(p => p.Speed).ToList();
            else if (rdoSort4.Checked) poke_setend = poke_setend.OrderBy(p => p.PokeDex_Index).ToList();
            //-----

            string value = txtGoro.Text;//語呂合わせの対象
           var udic = new Dictionary<string, List<string>>();//コレクション　マッチ文字,マッチする名前たち

            for (int i = 0; i < value.Length; i++)
                for (int c = 1; c < 4; c++)
                    if (!SetMatchParams(i, c)) break;

            cb1.DataSource = udic[value.Substring(0, 1)];
            cb2.DataSource = udic[value.Substring(1, 1)];
            cb3.DataSource = udic[value.Substring(2, 1)];
            cb4.DataSource = udic[value.Substring(3, 1)];
            cb5.DataSource = udic[value.Substring(4, 1)];
            cb1_2.DataSource = udic[value.Substring(0, 2)];
            cb2_3.DataSource = udic[value.Substring(1, 2)];
            cb3_4.DataSource = udic[value.Substring(2, 2)];
            cb4_5.DataSource = udic[value.Substring(3, 2)];
            cb1_3.DataSource = udic[value.Substring(0, 3)];
            cb2_4.DataSource = udic[value.Substring(1, 3)];
            cb3_5.DataSource = udic[value.Substring(2, 3)];

            if (value.Length > 5)
            {
                cb6.DataSource = udic[value.Substring(5, 1)];
                cb5_6.DataSource = udic[value.Substring(4, 2)];
                cb4_6.DataSource = udic[value.Substring(3, 3)];
            }
            if (value.Length > 6)
            {
                cb7.DataSource = udic[value.Substring(6, 1)];
                cb6_7.DataSource = udic[value.Substring(5, 2)];
                cb5_7.DataSource = udic[value.Substring(4, 3)];
            }
            if (value.Length > 7)
            {
                cb8.DataSource = udic[value.Substring(7, 1)];
                cb7_8.DataSource = udic[value.Substring(6, 2)];
                cb6_8.DataSource = udic[value.Substring(5, 3)];
            }
            if (value.Length > 8)
            {
                cb9.DataSource = udic[value.Substring(8, 1)];
                cb8_9.DataSource = udic[value.Substring(7, 2)];
                cb7_9.DataSource = udic[value.Substring(6, 3)];
            }
            if (value.Length > 9)
            {
                cb10.DataSource = udic[value.Substring(9, 1)];
                cb9_10.DataSource = udic[value.Substring(8, 2)];
                cb8_10.DataSource = udic[value.Substring(7, 3)];
            }

            //------
            bool SetMatchParams(int index, int cnt)
            {
                if (index + cnt > value.Length) return false;

                if (!udic.ContainsKey(value.Substring(index, cnt)))
                {
                    var ret = poke_setend.Where(p => p.M_POKENAME_JPN.Contains(value.Substring(index, cnt))).Select(p => p.M_POKENAME_JPN + (p.Form_JPN != "" ? ("(" + p.Form_JPN + ")") : "")).ToList();
                    udic.Add(value.Substring(index, cnt), ret);
                }
                return true;
            }
        }
        //void StartMainCalcAllComb()
        //{
        //var udic = new Dictionary<(int index, int cnt), List<string>>();//コレクション　<開始文字index,何文字か>,マッチする名前たち

        //for (int i = 0; i < value.Length; i++)
        //    for (int c = 1; c < 4; c++)
        //        if (!SetMatchParams(i, c)) break;
        ////------
        //bool SetMatchParams(int index, int cnt)
        //{
        //    if (index + cnt > value.Length) return false;

        //    var ret = poke_setend.Where(p => p.M_POKENAME_JPN.Contains(value.Substring(index, cnt))).Select(p => p.M_POKENAME_JPN + (p.Form == 2 ? ("(" + p.Form_JPN + ")") : "")).ToList();
        //    udic.Add((index, cnt), ret);

        //    return true;
        //}

        ////全組み合わせを出力----
        //int cmin = Math.Min(2, (int)numericUpDown1.Value);//何匹でその語呂合わせを実現するか
        //int cmax = Math.Min(value.Count(), (int)numericUpDown2.Value);
        //for (int c = cmin; c <= cmax; c++)
        //{
        //    var ret = getPtn(c, 0).ToList();

        //    foreach (var r in ret)
        //    {
        //        if (c == 2)
        //        {
        //            if (udic.TryGetValue((r[0], r[1] - r[0]), out List<string> v1) &&
        //                udic.TryGetValue((r[1], value.Length - r[1]), out List<string> v2))
        //            {
        //                foreach (var s1 in v1)
        //                    foreach (var s2 in v2)
        //                        Console.WriteLine(s1 + "," + s2);
        //            }
        //        }
        //        if (c == 3)
        //        {
        //            if (udic.TryGetValue((r[0], r[1] - r[0]), out List<string> v1) &&
        //                udic.TryGetValue((r[1], r[2] - r[1]), out List<string> v2) &&
        //                udic.TryGetValue((r[2], value.Length - r[2]), out List<string> v3))
        //            {
        //                foreach (var s1 in v1)
        //                    foreach (var s2 in v2)
        //                        foreach (var s3 in v3)
        //                            Console.WriteLine(s1 + "," + s2 + "," + s3);
        //            }
        //        }
        //        if (c == 4)
        //        {
        //            if (udic.TryGetValue((r[0], r[1] - r[0]), out List<string> v1) &&
        //                udic.TryGetValue((r[1], r[2] - r[1]), out List<string> v2) &&
        //                udic.TryGetValue((r[2], r[3] - r[2]), out List<string> v3) &&
        //                udic.TryGetValue((r[3], value.Length - r[3]), out List<string> v4))
        //            {
        //                try
        //                {
        //                    if (v1.Count() * v2.Count() * v3.Count() * v4.Count() > 1_000_000)
        //                    {
        //                        Console.WriteLine("1M件を超えてしまいます");
        //                        continue;
        //                    }
        //                }
        //                catch
        //                {
        //                    Console.WriteLine("1M件を超えてしまいます");
        //                    continue;
        //                }

        //                foreach (var s1 in v1)
        //                    foreach (var s2 in v2)
        //                        foreach (var s3 in v3)
        //                            foreach (var s4 in v4)
        //                            {
        //                                if (s1 == s2 || s1 == s3 || s1 == s4 || s2 == s3 || s2 == s4 || s3 == s4) continue;
        //                                Console.WriteLine(s1 + "," + s2 + "," + s3 + "," + s4);
        //                            }
        //            }
        //        }
        //        if (c == 5)
        //        {
        //            if (udic.TryGetValue((r[0], r[1] - r[0]), out List<string> v1) &&
        //                udic.TryGetValue((r[1], r[2] - r[1]), out List<string> v2) &&
        //                udic.TryGetValue((r[2], r[3] - r[2]), out List<string> v3) &&
        //                udic.TryGetValue((r[3], r[4] - r[3]), out List<string> v4) &&
        //                udic.TryGetValue((r[4], value.Length - r[4]), out List<string> v5))
        //            {
        //                try
        //                {
        //                    if (v1.Count() * v2.Count() * v3.Count() * v4.Count() * v5.Count() > 1_000_000)
        //                    {
        //                        Console.WriteLine("1M件を超えてしまいます");
        //                        continue;
        //                    }
        //                }
        //                catch
        //                {
        //                    Console.WriteLine("1M件を超えてしまいます");
        //                    continue;
        //                }

        //                foreach (var s1 in v1)
        //                    foreach (var s2 in v2)
        //                        foreach (var s3 in v3)
        //                            foreach (var s4 in v4)
        //                                foreach (var s5 in v5)
        //                                {
        //                                    if (s1 == s2 || s1 == s3 || s1 == s4 || s1 == s5 || s2 == s3 || s2 == s4 || s2 == s5 || s3 == s4 || s3 == s5 || s4 == s5) continue;
        //                                    Console.WriteLine(s1 + "," + s2 + "," + s3 + "," + s4 + "," + s5);
        //                                }
        //            }
        //        }
        //        if (c == 6)
        //        {
        //            if (udic.TryGetValue((r[0], r[1] - r[0]), out List<string> v1) &&
        //                udic.TryGetValue((r[1], r[2] - r[1]), out List<string> v2) &&
        //                udic.TryGetValue((r[2], r[3] - r[2]), out List<string> v3) &&
        //                udic.TryGetValue((r[3], r[4] - r[3]), out List<string> v4) &&
        //                udic.TryGetValue((r[4], r[5] - r[4]), out List<string> v5) &&
        //                udic.TryGetValue((r[5], value.Length - r[5]), out List<string> v6))
        //            {
        //                try
        //                {
        //                    if (v1.Count() * v2.Count() * v3.Count() * v4.Count() * v5.Count() * v6.Count() > 1_000_000)
        //                    {
        //                        Console.WriteLine("1M件を超えてしまいます");
        //                        continue;
        //                    }
        //                }
        //                catch
        //                {
        //                    Console.WriteLine("1M件を超えてしまいます");
        //                    continue;
        //                }

        //                foreach (var s1 in v1)
        //                    foreach (var s2 in v2)
        //                        foreach (var s3 in v3)
        //                            foreach (var s4 in v4)
        //                                foreach (var s5 in v5)
        //                                    foreach (var s6 in v6)
        //                                    {
        //                                        if (s1 == s2 || s1 == s3 || s1 == s4 || s1 == s5 || s2 == s3 || s2 == s4 || s2 == s5 || s3 == s4 || s3 == s5 || s4 == s5 ||
        //                                            s1 == s6 || s2 == s6 || s3 == s6 || s4 == s6 || s5 == s6) continue;
        //                                        Console.WriteLine(s1 + "," + s2 + "," + s3 + "," + s4 + "," + s5 + "," + s6);
        //                                    }
        //            }
        //        }
        //    }
        //}
        //return;

        //IEnumerable<int[]> getPtn(int cc, int strcnt)
        //{
        //    if (strcnt >= value.Length) yield break;
        //    if (cc == 1)
        //    {
        //        for (int i = 0; i < 3; i++)
        //            if (strcnt + i <= value.Length)
        //                yield return new int[1] { strcnt + i };
        //        yield break;
        //    }

        //    for (int i = 1; i < 4; i++)
        //    {
        //        foreach (var next in getPtn(cc - 1, strcnt + i))
        //        {
        //            var ptn = new int[cc];
        //            ptn[0] = strcnt;
        //            int j = 1;
        //            foreach (var n in next)
        //            {
        //                ptn[j] = n;
        //                j++;
        //            }
        //            yield return ptn;
        //        }
        //    }
        //}
        //}

        void ComboViewSet(int len)
        {

            if (len < 10)
            {
                cb10.Visible = false;
                cb8_10.Visible = false;
                cb9_10.Visible = false;
            }
            if (len < 9)
            {
                cb9.Visible = false;
                cb7_9.Visible = false;
                cb8_9.Visible = false;
            }
            if (len < 8)
            {
                cb8.Visible = false;
                cb6_8.Visible = false;
                cb7_8.Visible = false;
            }
            if (len < 7)
            {
                cb7.Visible = false;
                cb5_7.Visible = false;
                cb6_7.Visible = false;
            }
            if (len < 6)
            {
                cb6.Visible = false;
                cb4_6.Visible = false;
                cb5_6.Visible = false;
            }
        }
    }
}
