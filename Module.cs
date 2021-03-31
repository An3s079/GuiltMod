using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using Gungeon;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEngine;
using System.Globalization;
using Ionic.Zip;
using ItemAPI;

namespace GuiltMod
{
	public class Module : ETGModule
	{
		public static readonly string MOD_NAME = "guilt Mod";
		public static readonly string VERSION = "1.0.0";
		public static readonly string TEXT_COLOR = "#fc0303";

		public override void Start()
		{
			try
			{
				ItemBuilder.Init();
				ETGMod.AIActor.OnPostStart += GiveInfo;
                PrivateEye.names = LoadTxtFileFromLiterallyAnywhere("Names.txt");
                PrivateEye.BloodTypes = LoadTxtFileFromLiterallyAnywhere("BloodTypes.txt");
                PrivateEye.Birthdays = LoadTxtFileFromLiterallyAnywhere("DateOfBirths.txt");
                PrivateEye.pets = LoadTxtFileFromLiterallyAnywhere("Pets.txt");
                PrivateEye.hobbies = LoadTxtFileFromLiterallyAnywhere("Hobbies.txt");
                PrivateEye.jobs = LoadTxtFileFromLiterallyAnywhere("Jobs.txt");
                PrivateEye.foods = LoadTxtFileFromLiterallyAnywhere("Foods.txt");
                PrivateEye.colors = LoadTxtFileFromLiterallyAnywhere("Colors.txt");
            }
			catch (Exception e)
			{
				ETGModConsole.Log("mod Broke heres why: " + e);
			}
			Log($"{MOD_NAME} v{VERSION} started successfully.", TEXT_COLOR);
		}

        private void GiveInfo(AIActor enemy)
        {
			var PI = enemy.gameObject.AddComponent<PrivateEye>();
			PI.enemy = enemy;
        }

        public static void Log(string text, string color = "FFFFFF")
		{
			ETGModConsole.Log($"<color={color}>{text}</color>");
		}
        public List<string> LoadTxtFileFromLiterallyAnywhere(string name)
        {
            List<string> strings = null;
            if (File.Exists(this.Metadata.Archive))
            {
                ZipFile ModZIP = ZipFile.Read(this.Metadata.Archive);
                if (ModZIP != null && ModZIP.Entries.Count > 0)
                {
                    foreach (ZipEntry entry in ModZIP.Entries)
                    {
                        if (entry.FileName == name)
                        {

                            using (MemoryStream ms = new MemoryStream())
                            {

                                entry.Extract(ms);
                                StreamReader reader = new StreamReader(ms);
                                ms.Seek(0, SeekOrigin.Begin);
                                List<string> stringList = new List<string>();
                                string str = reader.ReadLine();
                                while (str != null)
                                {
                                    stringList.Add(str);
                                    str = reader.ReadLine();
                                }
                                strings = stringList.ToList();
                                break;
                            }
                        }
                    }
                }
            }
            else if (File.Exists(this.Metadata.Directory + "/" + name))
            {
                try
                {
                    var stringarray = File.ReadAllLines(this.Metadata.Directory + "/" + name);
                    strings = stringarray.ToList();
                }
                catch (Exception ex)
                {
                    Debug.LogError("Failed loading asset bundle from file.");
                    Debug.LogError(ex.ToString());

                }
            }
            else
            {
                Debug.LogError("Text file NOT FOUND!");
            }
            return strings;
        }

        public override void Exit() { }
		public override void Init() { }

	}
}
