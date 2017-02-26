using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ic_ef.Models
{

   public class sku_qty
    {

        public string sku { get; set; }
        public int qty { get; set; }
    }

    public class get_open_order
    {

        
        public int orderID { get; set; }
        public string orderDate { get; set; }
        public string orderStatus { get; set; }
        public int item_num { get; set; }
        public string item_name { get; set; }
        public string qty { get; set; }
        public int count_qty { get; set; }
        public string sku { get; set; }
    }

    public class mark_shipped
    {
        public int orderId;
        public string carrierCode;
        public string shipDate;
        public string trackingNumber;
        public bool notifyCustomer;
        public bool notifySalesChannel;
    }
    public class shipstationModel
    {

    }
    public class shipstationProduct
    {

        public class Rootobject
        {
            public Product[] products { get; set; }
            public int total { get; set; }
            public int page { get; set; }
            public int pages { get; set; }
        }

        public class Product
        {
            public int productId { get; set; }
            public string sku { get; set; }
            public string name { get; set; }
            public int price { get; set; }
            public int? defaultCost { get; set; }
            public int length { get; set; }
            public int width { get; set; }
            public int height { get; set; }
            public int weightOz { get; set; }
            public object internalNotes { get; set; }
            public string fulfillmentSku { get; set; }
            public DateTime createDate { get; set; }
            public DateTime modifyDate { get; set; }
            public bool active { get; set; }
            public Productcategory productCategory { get; set; }
            public object productType { get; set; }
            public string warehouseLocation { get; set; }
            public string defaultCarrierCode { get; set; }
            public string defaultServiceCode { get; set; }
            public string defaultPackageCode { get; set; }
            public string defaultIntlCarrierCode { get; set; }
            public string defaultIntlServiceCode { get; set; }
            public string defaultIntlPackageCode { get; set; }
            public string defaultConfirmation { get; set; }
            public string defaultIntlConfirmation { get; set; }
            public string customsDescription { get; set; }
            public int? customsValue { get; set; }
            public string customsTariffNo { get; set; }
            public string customsCountryCode { get; set; }
            public object noCustoms { get; set; }
            public Tag[] tags { get; set; }
        }

        public class Productcategory
        {
            public int categoryId { get; set; }
            public string name { get; set; }
        }

        public class Tag
        {
            public int tagId { get; set; }
            public string name { get; set; }
        }

    }

    public class shipstationShipment {

        public class ShipTo
        {
            public string name { get; set; }
            public string company { get; set; }
            public string street1 { get; set; }
            public string street2 { get; set; }
            public object street3 { get; set; }
            public string city { get; set; }
            public string state { get; set; }
            public string postalCode { get; set; }
            public string country { get; set; }
            public string phone { get; set; }
            public object residential { get; set; }
            public object addressVerified { get; set; }
        }

        public class Weight
        {
            public double value { get; set; }
            public string units { get; set; }
        }

        public class Dimensions
        {
            public string units { get; set; }
            public double length { get; set; }
            public double width { get; set; }
            public double height { get; set; }
        }

        public class InsuranceOptions
        {
            public object provider { get; set; }
            public bool insureShipment { get; set; }
            public double insuredValue { get; set; }
        }

        public class AdvancedOptions
        {
            public object billToParty { get; set; }
            public object billToAccount { get; set; }
            public object billToPostalCode { get; set; }
            public object billToCountryCode { get; set; }
            public int storeId { get; set; }
        }

        public class Shipment
        {
            public int shipmentId { get; set; }
            public int orderId { get; set; }
            public string userId { get; set; }
            public string customerEmail { get; set; }
            public string orderNumber { get; set; }
            public string createDate { get; set; }
            public string shipDate { get; set; }
            public double shipmentCost { get; set; }
            public double insuranceCost { get; set; }
            public string trackingNumber { get; set; }
            public bool isReturnLabel { get; set; }
            public string batchNumber { get; set; }
            public string carrierCode { get; set; }
            public string serviceCode { get; set; }
            public string packageCode { get; set; }
            public string confirmation { get; set; }
            public int warehouseId { get; set; }
            public bool voided { get; set; }
            public string voidDate { get; set; }
            public bool marketplaceNotified { get; set; }
            public string notifyErrorMessage { get; set; }
            public ShipTo shipTo { get; set; }
            public Weight weight { get; set; }
            public Dimensions dimensions { get; set; }
            public InsuranceOptions insuranceOptions { get; set; }
            public AdvancedOptions advancedOptions { get; set; }
            public object shipmentItems { get; set; }
            public object labelData { get; set; }
            public object formData { get; set; }
        }

        public class RootObject
        {
            public List<Shipment> shipments { get; set; }
            public int total { get; set; }
            public int page { get; set; }
            public int pages { get; set; }
        }

    }

    public class shipstationOrder
    {

        public class BillTo
        {
            public string name { get; set; }
            public object company { get; set; }
            public object street1 { get; set; }
            public object street2 { get; set; }
            public object street3 { get; set; }
            public object city { get; set; }
            public object state { get; set; }
            public object postalCode { get; set; }
            public object country { get; set; }
            public object phone { get; set; }
            public object residential { get; set; }
            public object addressVerified { get; set; }
        }

        public class ShipTo
        {
            public string name { get; set; }
            public object company { get; set; }
            public string street1 { get; set; }
            public string street2 { get; set; }
            public object street3 { get; set; }
            public string city { get; set; }
            public string state { get; set; }
            public string postalCode { get; set; }
            public string country { get; set; }
            public string phone { get; set; }
            public bool residential { get; set; }
            public string addressVerified { get; set; }
        }

        public class Item
        {
            public int orderItemId { get; set; }
            public string lineItemKey { get; set; }
            public string sku { get; set; }
            public string name { get; set; }
            public string imageUrl { get; set; }
            public object weight { get; set; }
            public int quantity { get; set; }
            public double unitPrice { get; set; }
            public double taxAmount { get; set; }
            public object shippingAmount { get; set; }
            public object warehouseLocation { get; set; }
            public List<object> options { get; set; }
            public object productId { get; set; }
            public object fulfillmentSku { get; set; }
            public bool adjustment { get; set; }
            public object upc { get; set; }
            public string createDate { get; set; }
            public string modifyDate { get; set; }
        }

        public class Weight
        {
            public double value { get; set; }
            public string units { get; set; }
        }

        public class InsuranceOptions
        {
            public object provider { get; set; }
            public bool insureShipment { get; set; }
            public double insuredValue { get; set; }
        }

        public class InternationalOptions
        {
            public object customsItemId { get; set; }
            public object contents { get; set; }
            public object customsItems { get; set; }
            public object nonDelivery { get; set; }
        }

        public class AdvancedOptions
        {
            public int warehouseId { get; set; }
            public bool nonMachinable { get; set; }
            public bool saturdayDelivery { get; set; }
            public bool containsAlcohol { get; set; }
            public bool mergedOrSplit { get; set; }
            public List<object> mergedIds { get; set; }
            public object parentId { get; set; }
            public int storeId { get; set; }
            public object customField1 { get; set; }
            public object customField2 { get; set; }
            public object customField3 { get; set; }
            public object source { get; set; }
            public object billToParty { get; set; }
            public object billToAccount { get; set; }
            public object billToPostalCode { get; set; }
            public object billToCountryCode { get; set; }
            public object billToMyOtherAccount { get; set; }
        }

        public class RootObject
        {
            public int orderId { get; set; }
            public string orderNumber { get; set; }
            public string orderKey { get; set; }
            public string orderDate { get; set; }
            public string createDate { get; set; }
            public string modifyDate { get; set; }
            public string paymentDate { get; set; }
            public object shipByDate { get; set; }
            public string orderStatus { get; set; }
            public int customerId { get; set; }
            public string customerUsername { get; set; }
            public string customerEmail { get; set; }
            public BillTo billTo { get; set; }
            public ShipTo shipTo { get; set; }
            public List<Item> items { get; set; }
            public double orderTotal { get; set; }
            public double amountPaid { get; set; }
            public double taxAmount { get; set; }
            public double shippingAmount { get; set; }
            public object customerNotes { get; set; }
            public object internalNotes { get; set; }
            public bool gift { get; set; }
            public object giftMessage { get; set; }
            public string paymentMethod { get; set; }
            public string requestedShippingService { get; set; }
            public string carrierCode { get; set; }
            public string serviceCode { get; set; }
            public string packageCode { get; set; }
            public string confirmation { get; set; }
            public string shipDate { get; set; }
            public object holdUntilDate { get; set; }
            public Weight weight { get; set; }
            public object dimensions { get; set; }
            public InsuranceOptions insuranceOptions { get; set; }
            public InternationalOptions internationalOptions { get; set; }
            public AdvancedOptions advancedOptions { get; set; }
            public object tagIds { get; set; }
            public object userId { get; set; }
            public bool externallyFulfilled { get; set; }
            public object externallyFulfilledBy { get; set; }
        }

    }

    public class shipstationOrderOptions
    {

        public class BillTo
        {
            public string name { get; set; }
            public string company { get; set; }
            public object street1 { get; set; }
            public object street2 { get; set; }
            public object street3 { get; set; }
            public object city { get; set; }
            public object state { get; set; }
            public object postalCode { get; set; }
            public object country { get; set; }
            public string phone { get; set; }
            public object residential { get; set; }
            public object addressVerified { get; set; }
        }

        public class ShipTo
        {
            public string name { get; set; }
            public string company { get; set; }
            public string street1 { get; set; }
            public string street2 { get; set; }
            public string street3 { get; set; }
            public string city { get; set; }
            public string state { get; set; }
            public string postalCode { get; set; }
            public string country { get; set; }
            public string phone { get; set; }
            public bool residential { get; set; }
            public string addressVerified { get; set; }
        }

        public class Weight
        {
            public double value { get; set; }
            public string units { get; set; }
        }

        public class Item
        {
            public int orderItemId { get; set; }
            public string lineItemKey { get; set; }
            public string sku { get; set; }
            public string name { get; set; }
            public string imageUrl { get; set; }
            public Weight weight { get; set; }
            public int quantity { get; set; }
            public double unitPrice { get; set; }
            public double? taxAmount { get; set; }
            public object shippingAmount { get; set; }
            public object warehouseLocation { get; set; }
            public List<object> options { get; set; }
            public int? productId { get; set; }
            public string fulfillmentSku { get; set; }
            public bool adjustment { get; set; }
            public object upc { get; set; }
            public string createDate { get; set; }
            public string modifyDate { get; set; }
        }

        public class Weight2
        {
            public double value { get; set; }
            public string units { get; set; }
        }

        public class Dimensions
        {
            public string units { get; set; }
            public double length { get; set; }
            public double width { get; set; }
            public double height { get; set; }
        }

        public class InsuranceOptions
        {
            public string provider { get; set; }
            public bool insureShipment { get; set; }
            public double insuredValue { get; set; }
        }

        public class InternationalOptions
        {
            public object customsItemId { get; set; }
            public string contents { get; set; }
            public object customsItems { get; set; }
            public string nonDelivery { get; set; }
        }

        public class AdvancedOptions
        {
            public int warehouseId { get; set; }
            public bool nonMachinable { get; set; }
            public bool saturdayDelivery { get; set; }
            public bool containsAlcohol { get; set; }
            public bool mergedOrSplit { get; set; }
            public List<object> mergedIds { get; set; }
            public object parentId { get; set; }
            public int storeId { get; set; }
            public string customField1 { get; set; }
            public string customField2 { get; set; }
            public object customField3 { get; set; }
            public object source { get; set; }
            public object billToParty { get; set; }
            public object billToAccount { get; set; }
            public object billToPostalCode { get; set; }
            public object billToCountryCode { get; set; }
            public object billToMyOtherAccount { get; set; }
        }

        public class Order
        {
            public int orderId { get; set; }
            public string orderNumber { get; set; }
            public string orderKey { get; set; }
            public string orderDate { get; set; }
            public string createDate { get; set; }
            public string modifyDate { get; set; }
            public string paymentDate { get; set; }
            public object shipByDate { get; set; }
            public string orderStatus { get; set; }
            public int? customerId { get; set; }
            public string customerUsername { get; set; }
            public string customerEmail { get; set; }
            public BillTo billTo { get; set; }
            public ShipTo shipTo { get; set; }
            public List<Item> items { get; set; }
            public double orderTotal { get; set; }
            public double amountPaid { get; set; }
            public double taxAmount { get; set; }
            public double shippingAmount { get; set; }
            public object customerNotes { get; set; }
            public string internalNotes { get; set; }
            public bool gift { get; set; }
            public object giftMessage { get; set; }
            public string paymentMethod { get; set; }
            public string requestedShippingService { get; set; }
            public string carrierCode { get; set; }
            public string serviceCode { get; set; }
            public string packageCode { get; set; }
            public string confirmation { get; set; }
            public string shipDate { get; set; }
            public object holdUntilDate { get; set; }
            public Weight2 weight { get; set; }
            public Dimensions dimensions { get; set; }
            public InsuranceOptions insuranceOptions { get; set; }
            public InternationalOptions internationalOptions { get; set; }
            public AdvancedOptions advancedOptions { get; set; }
            public object tagIds { get; set; }
            public object userId { get; set; }
            public bool externallyFulfilled { get; set; }
            public object externallyFulfilledBy { get; set; }
        }

        public class RootObject
        {
            public List<Order> orders { get; set; }
            public int total { get; set; }
            public int page { get; set; }
            public int pages { get; set; }
        }

    }
}
