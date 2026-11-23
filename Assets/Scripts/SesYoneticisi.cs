
using System.Collections;
using UnityEngine;

public class SesYoneticisi : MonoBehaviour
{
    public static SesYoneticisi Instance { get; private set; }

    [Header("Ses Kaynakları")]
    [SerializeField] private AudioSource seslendirme_kaynagi;
    [SerializeField] private AudioSource arka_plan_kaynagi;

    [Header("Arka Plan Müziği")]
    [SerializeField] private AudioClip arka_plan_muzigi;
    [SerializeField] private float normal_muzik_sesi = 0.25f;
    [SerializeField] private float kisik_muzik_sesi = 0.04f;

    private Coroutine seslendirme_kontrolu;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        seslendirme_kaynagi.playOnAwake = false;
        seslendirme_kaynagi.loop = false;

        arka_plan_kaynagi.playOnAwake = false;
        arka_plan_kaynagi.loop = true;
        arka_plan_kaynagi.volume = normal_muzik_sesi;
    }

    private void Start()
    {
        if (arka_plan_muzigi != null)
        {
            arka_plan_kaynagi.clip = arka_plan_muzigi;
            arka_plan_kaynagi.Play();
        }
    }

    public void SesCal(AudioClip ses_dosyasi)
    {
        if (ses_dosyasi == null) return;

        if (seslendirme_kontrolu != null)
            StopCoroutine(seslendirme_kontrolu);

        seslendirme_kaynagi.Stop();
        seslendirme_kaynagi.clip = ses_dosyasi;
        seslendirme_kaynagi.Play();

        arka_plan_kaynagi.volume = kisik_muzik_sesi;

        seslendirme_kontrolu =
            StartCoroutine(SeslendirmeBitisiniBekle());
    }

    private IEnumerator SeslendirmeBitisiniBekle()
    {
        while (seslendirme_kaynagi.isPlaying)
            yield return null;

        arka_plan_kaynagi.volume = normal_muzik_sesi;
        seslendirme_kontrolu = null;
    }

    public void SesDurdur()
    {
        seslendirme_kaynagi.Stop();

        if (seslendirme_kontrolu != null)
        {
            StopCoroutine(seslendirme_kontrolu);
            seslendirme_kontrolu = null;
        }

        arka_plan_kaynagi.volume = normal_muzik_sesi;
    }
}
