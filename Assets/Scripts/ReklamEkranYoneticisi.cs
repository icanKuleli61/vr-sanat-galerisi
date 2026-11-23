
using System.Collections;
using UnityEngine;

public class ReklamEkranYoneticisi : MonoBehaviour
{
    [Header("Reklam Görselleri")]
    [SerializeField] private Texture2D[] reklamGorselleri;

    [Header("Ekran Ayarları")]
    [SerializeField] private Renderer ekranRenderer;

    [Header("Zaman Ayarları")]
    [SerializeField] private float gosterimSuresi = 20f;
    [SerializeField] private float gecisSuresi = 1.5f;

    private MaterialPropertyBlock propertyBlock;

    private static readonly int GorselA_ID =
        Shader.PropertyToID("_GorselA");

    private static readonly int GorselB_ID =
        Shader.PropertyToID("_GorselB");

    private static readonly int GecisOrani_ID =
        Shader.PropertyToID("_GecisOrani");

    private void Awake()
    {
        propertyBlock = new MaterialPropertyBlock();
    }

    private void Start()
    {
        if (ekranRenderer == null ||
            reklamGorselleri == null ||
            reklamGorselleri.Length < 2)
        {
            Debug.LogError("Reklam ekranı ayarlarını kontrol et!");
            return;
        }

        StartCoroutine(ReklamlariOynat());
    }

    private IEnumerator ReklamlariOynat()
    {
        int mevcutIndex = 0;

        while (true)
        {
            int sonrakiIndex =
                (mevcutIndex + 1) % reklamGorselleri.Length;

            GorselleriAyarla(
                reklamGorselleri[mevcutIndex],
                reklamGorselleri[sonrakiIndex],
                0f
            );

            yield return new WaitForSeconds(gosterimSuresi);

            float gecenSure = 0f;

            while (gecenSure < gecisSuresi)
            {
                gecenSure += Time.deltaTime;

                float oran = Mathf.Clamp01(
                    gecenSure / gecisSuresi
                );

                GecisOraniniAyarla(oran);

                yield return null;
            }

            mevcutIndex = sonrakiIndex;
        }
    }

    private void GorselleriAyarla(
        Texture2D gorselA,
        Texture2D gorselB,
        float oran)
    {
        ekranRenderer.GetPropertyBlock(propertyBlock);

        propertyBlock.SetTexture(GorselA_ID, gorselA);
        propertyBlock.SetTexture(GorselB_ID, gorselB);
        propertyBlock.SetFloat(GecisOrani_ID, oran);

        ekranRenderer.SetPropertyBlock(propertyBlock);
    }

    private void GecisOraniniAyarla(float oran)
    {
        ekranRenderer.GetPropertyBlock(propertyBlock);

        propertyBlock.SetFloat(GecisOrani_ID, oran);

        ekranRenderer.SetPropertyBlock(propertyBlock);
    }
}
