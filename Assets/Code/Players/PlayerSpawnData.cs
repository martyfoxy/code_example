using System.Collections.Generic;
using Code.Data;
using UnityEngine;

namespace Code.Players
{
    /// <summary>
    /// По этим данным спавнится игрок
    /// </summary>
    public struct PlayerSpawnData
    {
        public int TeamId;
        public float Hp;
        public float Armor;
        public float Damage;
        public float Vampirism;
        public Transform SpawnPoint;
        public List<BuffModel> Buffs;
    }
}