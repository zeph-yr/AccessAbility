using System.Diagnostics;
using System.Net;
using System.Threading.Tasks;

namespace AccessAbility
{
    internal class Donate
    {
        internal static string donate_clickable_text = "<#00000000>------------<#ff0080ff><size=80%>♡ Donate";
        internal static string donate_clickable_hint = "If you'd like to support me";

        internal static string donate_modal_text_static_1 = "<size=80%><#ffff00ff><u>Support AccessAbility</u><size=75%><#cc99ffff>\nHave you have been enjoying my creations\nand you wish to support me?";
        internal static string donate_modal_text_static_2 = "<size=70%><#ff0080ff>With much love, ♡ Zeph<#00000000>------";

        internal static string donate_modal_text_dynamic = "";
        internal static string donate_modal_hint_dynamic = "";
        internal static string donate_update_dynamic = "";

        internal static void Refresh_Text()
        {
            if (donate_modal_text_dynamic == "")
            {
                _ = Get_Donate_Modal_Text();
            }
        }

        internal static void Patreon()
        {
            Process.Start("https://www.patreon.com/xeph_yr");
        }

        internal static void Kofi()
        {
            Process.Start("https://ko-fi.com/zeph_yr");
        }

        private static async Task Get_Donate_Modal_Text()
        {
            Plugin.Log.Debug("reply before: " + donate_modal_text_dynamic);

            string reply_text = "Loading...";
            string reply_hint = "";
            string reply_update = "";

            using (WebClient client = new WebClient())
            {
                try
                {
                    reply_text = await client.DownloadStringTaskAsync("https://raw.githubusercontent.com/zeph-yr/Shoutouts/main/README.md");
                }
                catch
                {
                    reply_text = "Loading failed. Pls ping Zeph on Discord, TY!";
                    Plugin.Log.Debug("Failed to fetch Donate info");
                }
                try
                {
                    reply_hint = await client.DownloadStringTaskAsync("https://raw.githubusercontent.com/zeph-yr/Shoutouts/main/hoverhints.txt");
                }
                catch
                {
                    Plugin.Log.Debug("Failed to fetch Donate info");
                }
                try
                {
                    reply_update = await client.DownloadStringTaskAsync("https://raw.githubusercontent.com/zeph-yr/Shoutouts/main/whatsnew.txt");
                    Plugin.Log.Debug("update text" + reply_update);

                }
                catch
                {
                    Plugin.Log.Debug("Failed to fetch Donate info");
                }
            }

            donate_modal_text_dynamic = reply_text;
            Plugin.Log.Debug("reply after: " + donate_modal_text_dynamic);

            int hint_start = reply_hint.IndexOf("[ACCESSABILITY]"); // 15
            int hint_end = reply_hint.IndexOf("###", hint_start);
            if (hint_start != -1)
            {
                //Logger.log.Debug("reply: " + reply_hint);
                //Logger.log.Debug("start: " + hint_start + " end: " + hint_end);
                donate_modal_hint_dynamic = reply_hint.Substring(hint_start + 15, hint_end - hint_start - 15); // Yes. And no, it's not wrong.
                Plugin.Log.Debug("hint:" + donate_modal_hint_dynamic);
            }

            Plugin.Log.Debug("update text" + reply_update);
            Plugin.Log.Debug("start" + reply_update.IndexOf("[ACCESSABILITY]"));

            int update_start = reply_update.IndexOf("[ACCESSABILITY]");
            int update_end = reply_update.IndexOf("###", update_start);
            if (update_start != -1)
            {
                donate_update_dynamic = reply_update.Substring(update_start + 15, update_end - update_start - 15);
                Plugin.Log.Debug("update: " + donate_update_dynamic);
            }

        }
    }
}