using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PurchaseButton : MonoBehaviour
{
    public enum PurchaseType {removeAds,Runic1, Runic2, Runic5, Runic15, Runic30, Runic70, Runic150 };
    public PurchaseType purchaseType;

    public void ClickPurchaseButton()
    {
        switch (purchaseType)
        {
            case PurchaseType.removeAds:
                IAPManager.instance.BuyRemoveAds();
                break;
            case PurchaseType.Runic1:
                IAPManager.instance.Buy1Gear();
                break;
            case PurchaseType.Runic2:
                IAPManager.instance.Buy2Gear();
                break;
            case PurchaseType.Runic5:
                IAPManager.instance.Buy5Gear();
                break;
            case PurchaseType.Runic15:
                IAPManager.instance.Buy15Gear();
                break;
            case PurchaseType.Runic30:
                IAPManager.instance.Buy30Gear();
                break;
            case PurchaseType.Runic70:
                IAPManager.instance.Buy70Gear();
                break;
            case PurchaseType.Runic150:
                IAPManager.instance.Buy150Gear();
                break;
        }
    }
}
