using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Silksprite.AvatarTinkerVista.ReplaceMaterialTexture
{
    [Serializable]
    public class ReplaceMaterialTexture
    {
        // Mitigate Unity Issue https://issuetracker.unity3d.com/issues/reorderable-list-elements-cannot-be-edited-when-using-custom-editors-and-serialized-objects
#if UNITY_2020_2_OR_NEWER
        [NonReorderable]
#endif
        public Material[] materials = {};
        public List<TextureReplacement> replacements = new List<TextureReplacement>();

        IEnumerable<Texture> Textures()
        {
            return materials
                .Where(material => material)
                .SelectMany(material => material.GetTexturePropertyNameIDs().Select(material.GetTexture))
                .Distinct();
        }

        public IEnumerable<TextureReplacement> Replacements()
        {
            return Textures().Select(texture => replacements.FirstOrDefault(replacement => texture == replacement.oldTexture) ?? TextureReplacement.Identity(texture));
        }

        public void ReplaceTexture(Texture oldTexture, Texture newTexture)
        {
            replacements.RemoveAll(replacement => replacement.oldTexture == oldTexture);
            replacements.Add(new TextureReplacement(oldTexture, newTexture));
        }

        public void Apply()
        {
            foreach (var material in materials.Where(material => material))
            {
                foreach (var nameId in material.GetTexturePropertyNameIDs())
                {
                    var oldTexture = material.GetTexture(nameId);
                    var replacement = replacements.FirstOrDefault(r => oldTexture == r.oldTexture);
                    if (replacement != null)
                    {
                        material.SetTexture(nameId, replacement.newTexture);
                    }
                }
            }
            replacements.Clear();
        }

        [Serializable]
        public class TextureReplacement
        {
            public Texture oldTexture;
            public Texture newTexture;

            public TextureReplacement(Texture oldTexture, Texture newTexture)
            {
                this.oldTexture = oldTexture;
                this.newTexture = newTexture;
            }

            public static TextureReplacement Identity(Texture texture) => new TextureReplacement(texture, texture);
        }
    }
}