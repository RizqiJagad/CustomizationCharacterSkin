using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterCustomizationLoader : MonoBehaviour
{
    public ModularOutfitSwitcher outfitSwitcher;

    void Start()
    {
        if (outfitSwitcher == null)
        {
            Debug.LogError("CharacterCustomizationLoader: OutfitSwitcher belum di-assign!");
            return;
        }

        // Terapkan Outfit
        outfitSwitcher.ApplyOutfit(CharacterCustomizationData.outfitIndex);

        // Terapkan Rambut
        string selectedHair = outfitSwitcher.hairLabels[CharacterCustomizationData.hairIndex];
        outfitSwitcher.hairSpriteResolver.SetCategoryAndLabel("Hair", selectedHair);
        outfitSwitcher.hairSpriteResolver.ResolveSpriteToSpriteRenderer();

        // Terapkan warna rambut
        Color hairColor = CharacterCustomizationData.hairColor;
        hairColor.a = 1f; // Paksa alpha = 1
        var hairRenderer = outfitSwitcher.hairSpriteResolver.GetComponent<SpriteRenderer>();
        if (hairRenderer != null) hairRenderer.color = hairColor;

        // Terapkan warna kulit
        Color skinColor = CharacterCustomizationData.skinColor;
        skinColor.a = 1f; // Paksa alpha = 1
        foreach (var resolver in outfitSwitcher.skinResolvers)
        {
            if (resolver != null)
            {
                var renderer = resolver.GetComponent<SpriteRenderer>();
                if (renderer != null) renderer.color = skinColor;
            }
        }

        Debug.Log("Customization applied with fixed alpha!");
    }
}
