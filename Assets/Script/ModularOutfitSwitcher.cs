using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D.Animation;
using UnityEngine.SceneManagement;

public class ModularOutfitSwitcher : MonoBehaviour
{
    [System.Serializable]
    public class SpritePart
    {
        public string category;
        public string label;
        public SpriteResolver targetResolver;
    }

    [System.Serializable]
    public class Outfit
    {
        public string outfitName;
        public List<SpritePart> parts;
    }

    public List<Outfit> outfits;

    [Header("Debug Outfit Index")]
    public int currentIndex = 0;

    [Header("Hair Specific Settings")]
    public SpriteResolver hairSpriteResolver;
    public List<string> hairLabels;
    private int currentHairIndex = 0;

    [Header("Hair Color Settings")]
    public List<Color> hairColors = new List<Color> { Color.black, Color.red, Color.yellow };
    private int currentHairColorIndex = 0;

    [Header("Skin Color Settings")]
    public List<Color> skinColors = new List<Color>();
    public List<SpriteResolver> skinResolvers = new List<SpriteResolver>();
    private int currentSkinColorIndex = 0;

    public void ApplyOutfit(int index)
    {
        if (index < 0 || index >= outfits.Count)
        {
            Debug.LogWarning("Outfit index out of range!");
            return;
        }

        Outfit selected = outfits[index];

        foreach (var part in selected.parts)
        {
            if (part.targetResolver != null)
            {
                if (part.label == "None")
                {
                    part.targetResolver.gameObject.SetActive(false);
                    continue;
                }

                part.targetResolver.gameObject.SetActive(true);
                part.targetResolver.SetCategoryAndLabel(part.category, part.label);
                part.targetResolver.ResolveSpriteToSpriteRenderer();

                // ✅ Cek apakah sprite berhasil dimuat
                SpriteRenderer renderer = part.targetResolver.GetComponent<SpriteRenderer>();
                if (renderer != null && renderer.sprite == null)
                {
                    Debug.LogWarning($"[Sprite MISSING] Category: {part.category}, Label: {part.label}");
                }

                if (part.category == "Hair" && hairLabels.Contains(part.label))
                {
                    currentHairIndex = hairLabels.IndexOf(part.label);
                }
            }
            else
            {
                Transform partObj = transform.Find(part.label);
                if (partObj != null)
                {
                    partObj.gameObject.SetActive(false);
                }
            }
        }

        currentIndex = index;
        Debug.Log($"Outfit applied: {selected.outfitName}");
    }

    [ContextMenu("Next Outfit")]
    public void NextOutfit()
    {
        int next = (currentIndex + 1) % outfits.Count;
        ApplyOutfit(next);
    }

    [ContextMenu("Previous Outfit")]
    public void PreviousOutfit()
    {
        int prev = (currentIndex - 1 + outfits.Count) % outfits.Count;
        ApplyOutfit(prev);
    }

    [ContextMenu("Next Hair")]
    public void NextHair()
    {
        if (hairSpriteResolver == null || hairLabels == null || hairLabels.Count == 0)
        {
            Debug.LogWarning("Hair Sprite Resolver or Hair Labels not set up!");
            return;
        }

        currentHairIndex = (currentHairIndex + 1) % hairLabels.Count;
        string nextHairLabel = hairLabels[currentHairIndex];

        hairSpriteResolver.SetCategoryAndLabel("Hair", nextHairLabel);
        hairSpriteResolver.ResolveSpriteToSpriteRenderer();
        Debug.Log($"Hair changed to: {nextHairLabel}");
    }

    [ContextMenu("Previous Hair")]
    public void PreviousHair()
    {
        if (hairSpriteResolver == null || hairLabels == null || hairLabels.Count == 0)
        {
            Debug.LogWarning("Hair Sprite Resolver or Hair Labels not set up!");
            return;
        }

        currentHairIndex = (currentHairIndex - 1 + hairLabels.Count) % hairLabels.Count;
        string prevHairLabel = hairLabels[currentHairIndex];

        hairSpriteResolver.SetCategoryAndLabel("Hair", prevHairLabel);
        hairSpriteResolver.ResolveSpriteToSpriteRenderer();
        Debug.Log($"Hair changed to: {prevHairLabel}");
    }

    [ContextMenu("Next Hair Color")]
    public void NextHairColor()
    {
        if (hairSpriteResolver == null || hairColors == null || hairColors.Count == 0)
        {
            Debug.LogWarning("Hair Sprite Resolver or Hair Colors not set up!");
            return;
        }

        currentHairColorIndex = (currentHairColorIndex + 1) % hairColors.Count;
        ApplyHairColor(hairColors[currentHairColorIndex]);
    }

    [ContextMenu("Previous Hair Color")]
    public void PreviousHairColor()
    {
        if (hairSpriteResolver == null || hairColors == null || hairColors.Count == 0)
        {
            Debug.LogWarning("Hair Sprite Resolver or Hair Colors not set up!");
            return;
        }

        currentHairColorIndex = (currentHairColorIndex - 1 + hairColors.Count) % hairColors.Count;
        ApplyHairColor(hairColors[currentHairColorIndex]);
    }

    private void ApplyHairColor(Color color)
    {
        color.a = 1f; // Hindari transparansi

        SpriteRenderer renderer = hairSpriteResolver.GetComponent<SpriteRenderer>();
        if (renderer != null)
        {
            renderer.color = color;
            Debug.Log($"Hair color changed to: {color}");
        }
        else
        {
            Debug.LogWarning("No SpriteRenderer found on Hair SpriteResolver!");
        }
    }

    [ContextMenu("Next Skin Color")]
    public void NextSkinColor()
    {
        if (skinColors == null || skinColors.Count == 0 || skinResolvers.Count == 0)
        {
            Debug.LogWarning("Skin colors or resolvers not properly set up.");
            return;
        }

        currentSkinColorIndex = (currentSkinColorIndex + 1) % skinColors.Count;
        ApplySkinColor(skinColors[currentSkinColorIndex]);
    }

    [ContextMenu("Previous Skin Color")]
    public void PreviousSkinColor()
    {
        if (skinColors == null || skinColors.Count == 0 || skinResolvers.Count == 0)
        {
            Debug.LogWarning("Skin colors or resolvers not properly set up.");
            return;
        }

        currentSkinColorIndex = (currentSkinColorIndex - 1 + skinColors.Count) % skinColors.Count;
        ApplySkinColor(skinColors[currentSkinColorIndex]);
    }

    private void ApplySkinColor(Color color)
    {
        color.a = 1f; // Hindari transparansi

        foreach (var resolver in skinResolvers)
        {
            if (resolver != null)
            {
                var renderer = resolver.GetComponent<SpriteRenderer>();
                if (renderer != null)
                {
                    renderer.color = color;
                }
            }
        }

        Debug.Log($"Skin color changed to: {color}");
    }

    public void SubmitCustomization()
    {
        // Simpan data ke class static
        CharacterCustomizationData.outfitIndex = currentIndex;
        CharacterCustomizationData.hairIndex = currentHairIndex;
        CharacterCustomizationData.hairColor = hairColors[currentHairColorIndex];
        CharacterCustomizationData.skinColor = skinColors[currentSkinColorIndex];

        Debug.Log("Customization saved and moving to next scene!");

        // Ganti "NextScene" dengan nama scene tujuanmu
        SceneManager.LoadScene("NextScene");
    }
}
