
using UnityEngine;

public class SeslendirmeButonu : MonoBehaviour
{
    [Header("Seslendirme Ayarları")]
    [SerializeField] private AudioClip sesDosyasi;

    public void SeslendirmeyiBaslat()
    {
        SesYoneticisi.Instance.SesCal(sesDosyasi);
    }

    public void SeslendirmeyiDurdur()
    {
        SesYoneticisi.Instance.SesDurdur();
    }
}
