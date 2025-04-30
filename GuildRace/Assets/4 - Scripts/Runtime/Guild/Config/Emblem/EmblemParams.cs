using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Game.Guild
{
    [Serializable]
    public class EmblemParams
    {
        [SerializeField] private List<AssetReference> symbols;
        [SerializeField] private List<AssetReference> divisions;
        [SerializeField] private List<Color> colors;

        public IReadOnlyList<AssetReference> Symbols => symbols;
        public IReadOnlyList<AssetReference> Divisions => divisions;
        public IReadOnlyList<Color> Colors => colors;

        public AssetReference GetSymbol(int index)
        {
            return symbols[index];
        }

        public AssetReference GetDivision(int index)
        {
            return divisions[index];
        }

        public Color GetColor(int index)
        {
            return colors[index];
        }
    }
}