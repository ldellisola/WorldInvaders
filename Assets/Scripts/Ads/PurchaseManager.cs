using UnityEngine;
using UnityEngine.Purchasing;

namespace Assets.Scripts.Ads
{
    public class PurchaseManager : MonoBehaviour, IStoreListener
    {
        public IStoreController StoreController { get; private set; }
        public IExtensionProvider extensionProvider { get; private set; }
        public AdManager AdManager;

        public bool IsInitialized => StoreController != null;

        public static string NoAdsProduct = "no_ads";

        // Update is called once per frame
        void Update()
        {
        
        }

        private void Start() {
#if UNITY_EDITOR || !UNITY_STANDALONE
            ConfigurationBuilder builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());

            string packageStoreId = "com.lucas.worldInvaders.NoAds";
            string storeName = GooglePlay.Name;

            builder.AddProduct(NoAdsProduct, ProductType.NonConsumable, new IDs() { { packageStoreId, storeName } });


            if (builder.products.Count != 0) {
                UnityPurchasing.Initialize(this, builder);
            }
#endif
        }


        public bool HasBoughtNoAds()
        {
            var product = StoreController.products.WithID(NoAdsProduct);

            return product != null && product.hasReceipt;
        }

        public void BuyNoAds()
        {
            Product product = StoreController.products.WithID(NoAdsProduct);

            if (product != null && product.availableToPurchase)
            {
                StoreController.InitiatePurchase(product);
            }
        }

        public void OnInitializeFailed(InitializationFailureReason error)
        {
            // Bloquear store
            Debug.LogError("ERROR");
            StoreController = null;
        }

        public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs purchaseEvent)
        {
            // Me viene el producto que compro por su ID
            Debug.Log("TRY ProcessPurchase " + purchaseEvent.purchasedProduct.definition.id);

            // verificar con Google que sea valido

            if (purchaseEvent.purchasedProduct.definition.id == NoAdsProduct)
            {
                AdManager.DisableAds();
            }

            ProductType productType = purchaseEvent.purchasedProduct.definition.type;

            return PurchaseProcessingResult.Complete;
        }

        public void OnPurchaseFailed(Product i, PurchaseFailureReason p)
        {
            // popup fallo la compra
            Debug.LogError("ERROR");

        }

        public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
        {
            this.StoreController = controller;
            this.extensionProvider = extensions;
        }
    }
}
