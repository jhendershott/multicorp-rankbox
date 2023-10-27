using System;
using System.Threading.Tasks;
using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.Interactivity;
using multicorp_bot.Helpers;

namespace multicorp_bot {
    class Program {
        static DiscordClient discord;
        static CommandsNextExtension commands;
        static InteractivityExtension interactivity;
        
       static async Task Main (string[] args) {
            string token = Environment.GetEnvironmentVariable("BOTTOKEN");
            discord = new DiscordClient(new DiscordConfiguration()
            {
                Token = token,
                TokenType = TokenType.Bot,
                Intents = DiscordIntents.All
            });

            var commands = discord.UseCommandsNext(new CommandsNextConfiguration()
                {
                    StringPrefixes = new[] { "!" },
                    CaseSensitive = false
                }
            );

            commands.RegisterCommands<Commands>();

            await discord.ConnectAsync ();
            await Task.Delay (-1);
        }

        //private static async Task Discord_MessageCreated(DiscordClient sender, DSharpPlus.EventArgs.MessageCreateEventArgs e)
        //{
        //    var command = new Commands();
        //    string[] messageStrings = new string[] { "bot", "multibot" };
        //    var strArray = e.Message.Content.Split(" ");
        //    string[] prohibChannelArr = new string[]
        //    {
        //        "command-chat",
        //        "officer-quarters",
        //        "op-planning",
        //        "frontline-news",
        //        "war-room-rp",
        //        "dispatch-rp",
        //        "war-assets-rp",
        //        "meta"
        //    };

        //    if ((e.Guild.Name == "MultiCorp" || e.Guild.Name == "Man vs Owlbear") && e.Author.Username != "MultiBot" && !prohibChannelArr.Contains(e.Channel.Name)) 
        //    {
        //        if (strArray.Intersect(messageStrings).Any() || e.MentionedUsers.Any(x => x.Username == "MultiBot"))
        //        {
        //            await Task.Run(() => command.SkynetProtocol(e));
        //        }
        //    }
        //    else
        //    {
        //        await Task.CompletedTask;
        //    }       
        //}

        static void CurrentDomain_ProcessExit(object sender, EventArgs e)
        {
            TelemetryHelper.Singleton.LogEvent("BOT STOP");
        }
    }
}
