using System.Collections;
using UnityEngine;

public class ClothStandView : ClothStand
{
    private IEnumerator loadingCorouyine = null;

    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
        ResetProgressBar();

        CheckAndStartTimer();
    }

    protected override void OnTriggerExit(Collider other)
    {
        base.OnTriggerExit(other);
        StopCoroutine(loadingCorouyine);
        loadingCorouyine = null;
        ResetProgressBar();
    }
    private void CheckAndStartTimer()
    {
        if (loadingCorouyine == null)
        {
            loadingCorouyine = StartClothLoadingTimer();
            StartCoroutine(loadingCorouyine);
        }
    }
    public IEnumerator StartClothLoadingTimer()
    {
        float currentTime = 0;

        while (currentTime < MaxTime)
        {
            currentTime += (Time.fixedDeltaTime *2);
            var percent = currentTime / MaxTime;
            slowDownFillImg.fillAmount = percent;
            yield return null;
        }

        yield return new WaitForSeconds(0.5f);
        OnTimerCompleted?.Invoke(standColor == StandColor.Red ? Color.red : Color.green);
        ResetProgressBar();
        yield return new WaitForSeconds(0.15f);
        loadingCorouyine = null;

        CheckAndStartTimer();
    }

    private void ResetProgressBar()
    {
        slowDownFillImg.fillAmount = 0;
    }
}
