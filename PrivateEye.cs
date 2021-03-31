using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using ItemAPI;

namespace GuiltMod
{
    class PrivateEye : MonoBehaviour
    {
        public AIActor enemy = null;
        public static List<string> names;
        public static List<string> BloodTypes;
        public static List<string> Birthdays;
        public static List<string> pets;
        public static List<string> hobbies;
        public static List<string> jobs;
        public static List<string> foods;
        public static List<string> colors;
        bool infoSpawned = false;
        void Update()
        {
            if (enemy.healthHaver.IsDead == true && infoSpawned == false)
            {
                infoSpawned = true;
                var nameToPick = UnityEngine.Random.Range(0, names.Count);
                var obj = new GameObject();
                var item = obj.AddComponent<EmptyItem>();
                ItemBuilder.AddSpriteToObject(names[nameToPick]+ "'s Information", "GuiltMod/Resources/InfoSprite.png", obj);
                string shortDesc = "What have you done?!?!";

                var age = UnityEngine.Random.Range(0, 101);
                var BloodTypeToPick = UnityEngine.Random.Range(0, BloodTypes.Count);
                var BDToPick = UnityEngine.Random.Range(0, Birthdays.Count);

                List<string> family1 = new List<string>();
                string[] family2 = {""};
                int FamilyMembers = UnityEngine.Random.Range(0, 6);
                for (int i = 0; i < FamilyMembers; i++)
                {
                    var namesRandom = UnityEngine.Random.Range(0, names.Count);
                    family1.Add(names[namesRandom]);
                    family2 = family1.ToArray();
                }
                string family3 = string.Join(", ", family2);
                if(family2[0] == "")
                {
                    family3 = "none";
                }

                List<string> pet1 = new List<string>();
                string[] pet2 = {""};
                int PetAmoun = UnityEngine.Random.Range(0, 4);
                for (int i = 0; i < PetAmoun; i++)
                {
                    var PetRandom = UnityEngine.Random.Range(0, pets.Count);
                    pet1.Add(pets[PetRandom]);
                    pet2 = pet1.ToArray();
                }
                string pet3 = string.Join(", ", pet2);
                if (pet2[0] == "")
                {
                    pet3 = "none";
                }
                var HobbyToPick = UnityEngine.Random.Range(0, hobbies.Count);
                var JobToPick = UnityEngine.Random.Range(0, jobs.Count);
                var FoodToPick = UnityEngine.Random.Range(0, foods.Count);
                var ColorToPick = UnityEngine.Random.Range(0, colors.Count);
                string longDesc = "Name: " + names[nameToPick] +
                "\nage: " + age +
                "\nblood type: " + BloodTypes[BloodTypeToPick] +
                "\nDate Of Birth: " + Birthdays[BDToPick] +
                "\nfamily: " + family3 +
                "\npets: " + pet3 +
                "\nhobby: " + hobbies[HobbyToPick] +
                "\noccupation: " + jobs[JobToPick] +
                "\nfavorite food: " + foods[FoodToPick] +
                "\nfavorite color: " + colors[ColorToPick] +
                "\nDate Of Death: right now" +
                "\nCause Of Death: Murder" +
                "\nKiller: you";
                ItemBuilder.SetupItem(item, shortDesc, longDesc, "guilt");
                LootEngine.SpawnItem(obj, enemy.specRigidbody.UnitCenter, Vector2.zero, 1f, false, true, false);
                names.Remove(names[nameToPick]);
                var RandomNum = UnityEngine.Random.Range(0, 10);
                names.Add(name[nameToPick] + $"{RandomNum}");
                
            }
        }
    }
}
