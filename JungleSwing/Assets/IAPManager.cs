using System;
using UnityEngine;
using UnityEngine.Purchasing;


public class IAPManager : MonoBehaviour, IStoreListener
{
    private PlayerMovement player;
    public static IAPManager instance;
    private static IStoreController m_StoreController;
    private static IExtensionProvider m_StoreExtensionProvider;

    //Step 1 create your products
    private string removeAds = "remove_ads";  
    private string Runic1 = "runic_1";
    private string Runic2= "runic_2";
    private string Runic5 = "runic_5";
    private string Runic15 = "runic_15";
    private string Runic30= "runic_30";    
    private string Runic70 = "runic_70";    
    private string Runic150 = "runic_150";    


    //************************** Adjust these methods **************************************
    public void InitializePurchasing()
    {
        if (IsInitialized()) { return; }
        var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());

        //Step 2 choose if your product is a consumable or non consumable
        builder.AddProduct(removeAds, ProductType.NonConsumable);
        builder.AddProduct(Runic1, ProductType.Consumable);
        builder.AddProduct(Runic2, ProductType.Consumable);
        builder.AddProduct(Runic5, ProductType.Consumable);
        builder.AddProduct(Runic15, ProductType.Consumable);
        builder.AddProduct(Runic30, ProductType.Consumable);
        builder.AddProduct(Runic70, ProductType.Consumable);
        builder.AddProduct(Runic150, ProductType.Consumable);

        UnityPurchasing.Initialize(this, builder);
    }

    private bool IsInitialized()
    {
        return m_StoreController != null && m_StoreExtensionProvider != null;
    }

    //Step 3 Create methods
    public void BuyRemoveAds()
    {
        BuyProductID(removeAds);
    }

/*    public void Buy10Coins()
    {
        BuyProductID(coin10);
    }    
    public void Buy100Coins()
    {
        BuyProductID(coin100);
    }    
    public void Buy1000Coins()
    {
        BuyProductID(coin1000);
    } */   
    public void Buy1Gear()
    {
        BuyProductID(Runic1);
    }
    public void Buy2Gear()
    {
        BuyProductID(Runic2);
    }
    public void Buy5Gear()
    {
        BuyProductID(Runic5);
    }
    public void Buy15Gear()
    {
        BuyProductID(Runic15);
    }
    public void Buy30Gear()
    {
        BuyProductID(Runic30);
    }
    public void Buy70Gear()
    {
        BuyProductID(Runic70);
    }
    public void Buy150Gear()
    {
        BuyProductID(Runic150);
    }
    /*    public void Buy1Life()
        {
            BuyProductID(Life1);
        }
        public void Buy3Lifes()
        {
            BuyProductID(Life3);
        }
        public void Buy9Lifes()
        {
            BuyProductID(Life9);
        }*/



    //Step 4 modify purchasing
    public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs args)
    {
        if (String.Equals(args.purchasedProduct.definition.id, removeAds, StringComparison.Ordinal))
        {
            Debug.Log("RemoveAds Successful");
        }
/*        else if (String.Equals(args.purchasedProduct.definition.id, coin10, StringComparison.Ordinal))
        {
            Debug.Log("10 Coins Added");
        }
        else if (String.Equals(args.purchasedProduct.definition.id, coin100, StringComparison.Ordinal))
        {
            Debug.Log("100 Coins Added");
        }
        else if (String.Equals(args.purchasedProduct.definition.id, coin1000, StringComparison.Ordinal))
        {
            Debug.Log("1000 Coins Added");
        }*/
        else if (String.Equals(args.purchasedProduct.definition.id, Runic1, StringComparison.Ordinal))
        {
            player.Runic += 1;
        }
        else if (String.Equals(args.purchasedProduct.definition.id, Runic2, StringComparison.Ordinal))
        {
            player.Runic += 2;
        }
        else if (String.Equals(args.purchasedProduct.definition.id, Runic5, StringComparison.Ordinal))
        {
            player.Runic += 5;
        }
        else if (String.Equals(args.purchasedProduct.definition.id, Runic15, StringComparison.Ordinal))
        {
            player.Runic += 15;
        }
        else if (String.Equals(args.purchasedProduct.definition.id, Runic30, StringComparison.Ordinal))
        {
            player.Runic += 30;
        }
        else if (String.Equals(args.purchasedProduct.definition.id, Runic70, StringComparison.Ordinal))
        {
            player.Runic += 170;
        }
        else if (String.Equals(args.purchasedProduct.definition.id, Runic150, StringComparison.Ordinal))
        {
            player.Runic += 150;
        }
        /*        else if (String.Equals(args.purchasedProduct.definition.id, Life1, StringComparison.Ordinal))
                {

                }
                else if (String.Equals(args.purchasedProduct.definition.id, Life3, StringComparison.Ordinal))
                {
                    Debug.Log("3 Lifes Added");
                }
                else if (String.Equals(args.purchasedProduct.definition.id, Life9, StringComparison.Ordinal))
                {
                    Debug.Log("9 Lifes Added");
                }*/
        else
        {
            Debug.Log("Purchase Failed");
        }
        return PurchaseProcessingResult.Complete;
    }










    //**************************** Dont worry about these methods ***********************************
    private void Awake()
    {
        TestSingleton();
    }

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        if (m_StoreController == null) { InitializePurchasing(); }
    }

    void Update()
    {

    }
    private void TestSingleton()
    {
        if (instance != null) { Destroy(gameObject); return; }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void BuyProductID(string productId)
    {
        if (IsInitialized())
        {
            Product product = m_StoreController.products.WithID(productId);
            if (product != null && product.availableToPurchase)
            {
                Debug.Log(string.Format("Purchasing product asychronously: '{0}'", product.definition.id));
                m_StoreController.InitiatePurchase(product);
            }
            else
            {
                Debug.Log("BuyProductID: FAIL. Not purchasing product, either is not found or is not available for purchase");
            }
        }
        else
        {
            Debug.Log("BuyProductID FAIL. Not initialized.");
        }
    }

    public void RestorePurchases()
    {
        if (!IsInitialized())
        {
            Debug.Log("RestorePurchases FAIL. Not initialized.");
            return;
        }

        if (Application.platform == RuntimePlatform.IPhonePlayer ||
            Application.platform == RuntimePlatform.OSXPlayer)
        {
            Debug.Log("RestorePurchases started ...");

            var apple = m_StoreExtensionProvider.GetExtension<IAppleExtensions>();
            apple.RestoreTransactions((result) => {
                Debug.Log("RestorePurchases continuing: " + result + ". If no further messages, no purchases available to restore.");
            });
        }
        else
        {
            Debug.Log("RestorePurchases FAIL. Not supported on this platform. Current = " + Application.platform);
        }
    }

    public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
    {
        Debug.Log("OnInitialized: PASS");
        m_StoreController = controller;
        m_StoreExtensionProvider = extensions;
    }


    public void OnInitializeFailed(InitializationFailureReason error)
    {
        Debug.Log("OnInitializeFailed InitializationFailureReason:" + error);
    }

    public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
    {
        Debug.Log(string.Format("OnPurchaseFailed: FAIL. Product: '{0}', PurchaseFailureReason: {1}", product.definition.storeSpecificId, failureReason));
    }
}