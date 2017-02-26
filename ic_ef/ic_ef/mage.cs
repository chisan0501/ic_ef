using ic_ef.org.connectall;

using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ic_ef
{
    public class mage
    {
       
      
        private db_a094d4_icdbEntities db = new db_a094d4_icdbEntities();
       
        public mage()
        {
          
           

        }
        
        //get the asset's spec by the machine's serial number 
        public List<Models.magentoViewModel> get_spec (int asset_tag)
        {
            string result = (from b in db.rediscovery where b.ictag == asset_tag select b.serial).SingleOrDefault();
            
            
            var spec = (from t in db.production_log where t.serial == result from h in db.rediscovery where t.serial == h.serial select new Models.magentoViewModel { hdd = t.HDD, cpu = t.CPU, brand = t.Manufacture, ram = t.RAM, ictags = asset_tag, model = t.Model, serial = h.serial, screen_size = t.screen_size, video_card = t.video_card, optical = h.optical_drive, pallet_name = h.pallet }).ToList();
            if (spec.Count() == 0)
            {
             spec = rediscovery(asset_tag);
              return spec;
            }
            var temp_cpu = spec[0].cpu;
            var temp_hdd = spec[0].hdd;
           
            if (temp_hdd.Contains("GB"))
            {
                temp_hdd = temp_hdd.Replace("GB", "");
            }

            int formatted_hdd = int.Parse(temp_hdd);
            formatted_hdd = Convert.ToInt32(formatted_hdd * 1.024 * 1.024); formatted_hdd = Levenshtein.Round(formatted_hdd, 10);
            switch (formatted_hdd)
            {
                case 480:
                    formatted_hdd = 500;
                    temp_hdd = formatted_hdd + "GB HDD";
                    break;
                case 980:
                    formatted_hdd = 1000;
                    temp_hdd = formatted_hdd/1000 + "TB HDD";
                    break;
                default:
                    temp_hdd = formatted_hdd + "GB HDD";
                    break;
            }
            
          
            string formatted_cpu = Levenshtein.comput_title(temp_cpu);
            spec[0].cpu = formatted_cpu;
            spec[0].hdd = temp_hdd;
            return spec;
        }
        public List<Models.magentoViewModel> rediscovery(int asset_tag)
        {


            var result = (from t in db.rediscovery where t.ictag == asset_tag select new Models.magentoViewModel { hdd = t.hdd, cpu = t.cpu, brand = t.brand, ram = t.ram, ictags = asset_tag, model = t.model, serial = t.serial, screen_size = "NA", video_card = "NA", optical = t.optical_drive, pallet_name = t.pallet }).ToList();

            var temp_cpu = result[0].cpu;
            var temp_hdd = result[0].hdd;
            if (temp_hdd.Contains("GB"))
            {
                temp_hdd = temp_hdd.Replace("GB", "");
            }
            if (temp_hdd.Contains("SSD"))
            {
                temp_hdd = temp_hdd.Replace("SSD", "");
            }
            int formatted_hdd = int.Parse(temp_hdd);
            formatted_hdd = Convert.ToInt32(formatted_hdd * 1.024 * 1.024);
            formatted_hdd = Levenshtein.Round(formatted_hdd, 10);
            switch (formatted_hdd)
            {
                case 480:
                    formatted_hdd = 500;
                    temp_hdd = formatted_hdd + "GB HDD";
                    break;
                case 980:
                    formatted_hdd = 1000;
                    temp_hdd = formatted_hdd / 1000 + "TB HDD";
                    break;
                default:
                    temp_hdd = formatted_hdd + "GB HDD";
                    break;
            }
            string formatted_cpu = Levenshtein.comput_title(temp_cpu);
            result[0].cpu = formatted_cpu;
            result[0].hdd = temp_hdd;

            return result;


        }

      

        public static string image_to_64 (string Path)
        {
            using (Image image = Image.FromFile(Path))
            {
                using (MemoryStream m = new MemoryStream())
                {
                    image.Save(m, image.RawFormat);
                    byte[] imageBytes = m.ToArray();
                    // Convert byte[] to Base64 String
                    string base64String = Convert.ToBase64String(imageBytes);
                    return base64String;
                }

            }
        }

        public List<string> pallet_list ()
        {
            var pallet_list = (from t in db.label_menu where (t.name == "Mar (Desktop)" || t.name == "Mar (Laptop)" || t.name == "OEM (Desktop)" || t.name == "OEM (Laptop)") select t.product).ToList();
            return pallet_list;
        }

      

        public bool enable_product (string sku)
        {
            bool result;
            try
            {

                MagentoService mservice = new MagentoService();
                String mlogin = mservice.login("admin", "Interconnection123!");
                var item_to_edit = new catalogProductCreateEntity();
                item_to_edit.status = "1";
                result = mservice.catalogProductUpdate(mlogin, sku, item_to_edit, "", "SKU");
                
            }
            catch
            {
                result = false;
            }
            return result;
        }


        public Boolean cancel_order(string orderID,string status)
        {
            bool sucess = false;

            MagentoService mservice = new MagentoService();
            String mlogin = mservice.login("admin", "Interconnection123!");
         //  sucess = mservice.salesOrderCancel(mlogin, orderID);

            sucess = mservice.salesOrderAddComment(mlogin, orderID, status, null,null);

            return sucess;
        }

        public int update_qty(string sku,string qty,string pid)
        {
            MagentoService mservice = new MagentoService();
            String mlogin = mservice.login("admin", "Interconnection123!");
            var item_to_update = new catalogInventoryStockItemUpdateEntity();
            item_to_update.qty = qty;
            var result =  mservice.catalogInventoryStockItemUpdate(mlogin, sku, item_to_update);

            try
            {
                HttpWebRequest myHttpWebRequest = (HttpWebRequest)WebRequest.Create("http://connectall.org/update.php?product_id=" + pid + "&qty=" + qty);
                myHttpWebRequest.ContentType = "application/x-www-form-urlencoded";
                myHttpWebRequest.UserAgent = ".NET Framework Test Client";
                WebResponse wr = myHttpWebRequest.GetResponse();
            }
            catch (WebException wex)
            {
                var pageContent = new StreamReader(wex.Response.GetResponseStream())
                                      .ReadToEnd();
            }

            return result;
        }

        //this is currently connect to the dev site's SOAP Api
        //*** remeber to change it back to connect all SOAP API after live***
        public void quick_update(string qty,string sku, string path)
        {
            MagentoService mservice = new MagentoService();
            String mlogin = mservice.login("admin", "Interconnection123!");
            catalogInventoryStockItemUpdateEntity qty_update = new catalogInventoryStockItemUpdateEntity();

            qty_update.manage_stockSpecified = true;

            qty_update.manage_stock = 1;
            qty_update.qty = qty;
            if (qty == "0")
            {
                qty_update.is_in_stock = 0;
            }
            qty_update.is_in_stock = 1;
            qty_update.is_in_stockSpecified = true;
            mservice.catalogInventoryStockItemUpdate(
 mlogin, sku, qty_update);
            //catalogProductAttributeMediaCreateEntity photo = new catalogProductAttributeMediaCreateEntity();

            //string image64 = image_to_64(path);
            //var imageEntity = new catalogProductImageFileEntity();

            //imageEntity.content = image64;
            //imageEntity.name = "photo1";
            //imageEntity.mime = "image/jpeg";
            //photo.file = imageEntity;
            //photo.label = "label";
            //photo.position = "0";
            //photo.exclude = "0";
            //photo.types = new[] { "image", "small_image", "thumbnail" };

            //mservice.catalogProductAttributeMediaCreate(mlogin, retail.p_id, photo, "", "ID");
        }

        public salesOrderListEntity[] get_open_orders()
        {
            MagentoService mservice = new MagentoService();
            String mlogin = mservice.login("admin", "Interconnection123!");

            filters mf = new filters();
            complexFilter[] cpf = new complexFilter[1];
            complexFilter mcpf = new complexFilter();
            mcpf.key = "status";
            associativeEntity mas = new associativeEntity();
            mas.key = "in";
            mas.value = "pending,processing,approved_clearsale,analysing_clearsale,pending_clearsale";
            mcpf.value = mas;
            cpf[0] = mcpf;
            //mcpf = new complexFilter();
            //mcpf.key = "status";
            //mas = new associativeEntity();
            
            //mas.key = "eq";
            //mas.value = "processing";
            //mcpf.value = mas;
            //cpf[1] = mcpf;
            //mcpf = new complexFilter();
            //mcpf.key = "status";
            //mas = new associativeEntity();
            
            //mas.key = "eq";
            //mas.value = "approved_clearsale";
            //mcpf.value = mas;
            //cpf[2] = mcpf;
            //mcpf = new complexFilter();
            //mcpf.key = "status";
            //mas = new associativeEntity();
            
            //mas.key = "eq";
            //mas.value = "analysing_clearsale";
            //mcpf.value = mas;
            //cpf[3] = mcpf;
            //mcpf = new complexFilter();
            //mcpf.key = "status";
            //mas = new associativeEntity();
           
            //mas.key = "eq";
            //mas.value = "pending_clearsale";
            //mcpf.value = mas;
            //cpf[4] = mcpf;
            mf.complex_filter = cpf;
            var soe = mservice.salesOrderList(mlogin, mf);
            return soe;

        }
        public catalogProductEntity[] get_all_product()
        {
            MagentoService mservice = new MagentoService();
            String mlogin = mservice.login("admin", "Interconnection123!");

            var result = mservice.catalogProductList(mlogin,null,"");
           
            return result;
        }

        public salesOrderEntity get_order_detail(string order_id)
        {

            MagentoService mservice = new MagentoService();
            String mlogin = mservice.login("admin", "Interconnection123!");
            var result = mservice.salesOrderInfo(mlogin, order_id);

              return result;
        }



        public catalogInventoryStockItemEntity[] check_product (string sku)
        {
            
            MagentoService mservice = new MagentoService();
            
            String mlogin = mservice.login("admin", "Interconnection123!");
            string[] sku_arr = { sku };
            catalogInventoryStockItemEntity[] result = null;
            
            try
            {
               
               result = mservice.catalogInventoryStockItemList(mlogin, sku_arr);
                return result;
            }
            catch
            {
                
            }

            return result;
        }


        public void create_customer (List<Models.csv_import> customer)
        {
            int i = 1;
            foreach (var item in customer)
            {
                try
                {
                    MagentoService mservice = new MagentoService();
                    string mlogin = mservice.login("admin", "Interconnection123!");
                    customerCustomerEntityToCreate create_customer = new customerCustomerEntityToCreate();
                    create_customer.password = "interconnection123!";
                    create_customer.email = item.EMail;
                    create_customer.firstname = item.First_Name;
                    create_customer.lastname = item.Last_Name;
                    create_customer.website_id = 5;
                    create_customer.website_idSpecified = true;
                    create_customer.store_id = 5;
                    create_customer.store_idSpecified = true;
                    if (string.IsNullOrEmpty(item.EMail))
                    {
                        create_customer.email = create_customer.firstname + create_customer.lastname + "@noemail.com";
                    }
                    switch (item.Customer_Type)
                    {
                        case "Low Income":
                            create_customer.group_id = 14;
                            create_customer.group_idSpecified = true;
                            break;
                        case "NON-PROFIT":
                            create_customer.group_id = 15;
                            create_customer.group_idSpecified = true;
                            break;
                        default:
                            create_customer.group_id = 13;
                            create_customer.group_idSpecified = true;
                            break;
                    }
                    int customer_id = mservice.customerCustomerCreate(mlogin, create_customer);
                    if (!string.IsNullOrEmpty(item.Phone_1))
                    {
                        customerAddressEntityCreate address = new customerAddressEntityCreate();
                        address.telephone = item.Phone_1;
                        address.firstname = item.First_Name;
                        address.lastname = item.Last_Name;
                        address.company = item.Company;
                        address.country_id = "US";
                        address.city = "Seattle";
                        address.postcode = "00000";
                        address.region = "62";
                        
                        address.street = new string[]{ "Defaul Address"};
                        address.is_default_shipping = true;
                        address.is_default_shippingSpecified = true;
                        address.is_default_billing = true;
                        address.is_default_billingSpecified = true;
                        mservice.customerAddressCreate(mlogin, customer_id, address);
                        
                        System.Diagnostics.Debug.WriteLine("complete " + i++);
                    }
                    
                }
                catch(Exception e)
                {
                    System.Diagnostics.Debug.WriteLine(e);
                }
            }
        }

        public string create_order_ts(Models.ts_order[] tsorder)
        {
            string result = "";
            foreach(var order in tsorder)
            {
                try
                {
                    MagentoService mservice = new MagentoService();
                    String mlogin = mservice.login("admin", "Interconnection123!");
                    //create shopping cart 
                    var cart = mservice.shoppingCartCreate(mlogin, "1");
                    //set customer to cart 
                    shoppingCartCustomerEntity customer = new shoppingCartCustomerEntity();
                    customer.firstname = "import";
                    customer.lastname = "Orders";
                    customer.email = "import@interconnection.org";
                    customer.password = "111111";
                    customer.customer_id = 15795;
                    customer.customer_idSpecified = true;
                    customer.mode = "customer";
                    customer.website_id = 1;
                    mservice.shoppingCartCustomerSet(mlogin, cart, customer, "1");
                    //set customer address
                    List<shoppingCartCustomerAddressEntity> customer_Address_List = new List<shoppingCartCustomerAddressEntity>();
                    shoppingCartCustomerAddressEntity address = new shoppingCartCustomerAddressEntity();
                    address.mode = "billing";
                    address.firstname = order.first_name;
                    address.lastname = order.last_name;
                    address.street = order.Org_Street_Address;
                    address.city = order.Org_City;
                    address.region = order.Org_State ;
                    address.country_id = "US";
                    address.postcode = order.Org_Zip_Code;
                    address.telephone = order.Org_Phone;
                    address.is_default_billing = 1;
                    customer_Address_List.Add(address);
                    shoppingCartCustomerAddressEntity customer_Address_Billing = new shoppingCartCustomerAddressEntity();
                    customer_Address_Billing.mode = "shipping";
                    // If you have already a Magento User address list then use below field "address_id".
                    //customer_Address_Billing.address_id = Session["Customer_Address_ID"].ToString();
                    customer_Address_Billing.firstname = order.first_name;
                    customer_Address_Billing.lastname = order.last_name;
                    customer_Address_Billing.company = order.Org_Name;
                    customer_Address_Billing.street = order.Org_Street_Address;
                    customer_Address_Billing.city = order.Org_City;
                    customer_Address_Billing.country_id = "US";
                    customer_Address_Billing.region = order.Org_State;
                    customer_Address_Billing.postcode = order.Org_Zip_Code;
                    customer_Address_Billing.telephone = order.Org_Phone;
                    customer_Address_Billing.fax = "";
                    customer_Address_List.Add(customer_Address_Billing);
                    mservice.shoppingCartCustomerAddresses(mlogin, cart, customer_Address_List.ToArray(), "1");
                //add product
                var Products_list = new shoppingCartProductEntity() {
                    qty = order.Total_Product_Quantity,
                    qtySpecified = true,
                    sku = order.Item_ID 
                };
                    
                
                    
                    

                   
                    mservice.shoppingCartProductAdd(mlogin, cart, new shoppingCartProductEntity[] { Products_list }, null);
                    //add shipping method
                    var avaiable_method = mservice.shoppingCartShippingList(mlogin, cart, "1");

                    mservice.shoppingCartShippingMethod(mlogin, cart, "fedex_FEDEX_GROUND", "1");
                    //add payment method
                    var avaiable_payment = mservice.shoppingCartPaymentList(mlogin, cart, "1");
                    shoppingCartPaymentMethodEntity payment_method_entity = new shoppingCartPaymentMethodEntity();
                    payment_method_entity.method = "custompayment";
                 //   payment_method_entity.method = "authorizenet";
                    payment_method_entity.cc_cid = null;
                    payment_method_entity.cc_owner = null;
                    payment_method_entity.cc_number = null;
                    payment_method_entity.cc_type = null;
                    payment_method_entity.cc_exp_month = null;
                    payment_method_entity.cc_exp_year = null;

                    mservice.shoppingCartPaymentMethod(mlogin, cart, payment_method_entity, "1");
                    //var price_total = mservice.shoppingCartTotals(mlogin, cart, "1");
                    var order_ID = mservice.shoppingCartOrder(mlogin, cart, "1", null);
                    result += "<p style='color:green'>Order " + order.Item_ID + " for " + order.Org_Contact_Name + " Created\n</p>";

               
        
                }
                catch (Exception e)
                {
                    result += "<p style='color:red'>Order " +  order.Item_ID + " for " + order.Org_Contact_Name + "Failed\n Message : "+e.Message + "\n</p>";
                }
              


            }
            return result;
        }
        public string create_order_g360(Models.g360_order[] g360order)
        {
            string result = "";
            foreach (var order in g360order)
            {
                try
                {
                    MagentoService mservice = new MagentoService();
                    String mlogin = mservice.login("admin", "Interconnection123!");
                    //create shopping cart 
                    var cart = mservice.shoppingCartCreate(mlogin, "1");
                    //set customer to cart 
                    shoppingCartCustomerEntity customer = new shoppingCartCustomerEntity();
                    customer.firstname = order.Organization_Name;
                    customer.lastname = order.Organization_Name;
                    customer.email = order.Email;
                    customer.mode = "guest";
                    customer.website_id = 1;
                    mservice.shoppingCartCustomerSet(mlogin, cart, customer, "1");
                    //set customer address
                    List<shoppingCartCustomerAddressEntity> customer_Address_List = new List<shoppingCartCustomerAddressEntity>();
                    shoppingCartCustomerAddressEntity address = new shoppingCartCustomerAddressEntity();
                    address.mode = "billing";
                    address.firstname = order.Organization_Name;
                    address.lastname = order.Organization_Name;
                    address.street = order.Address1 + " " +order.Address2;
                    address.city = order.City;
                    address.region = order.State;
                    address.country_id = "US";
                    address.postcode = order.Zip;
                    address.telephone = order.Phone;
                    address.is_default_billing = 1;
                    customer_Address_List.Add(address);
                    shoppingCartCustomerAddressEntity customer_Address_Billing = new shoppingCartCustomerAddressEntity();
                    customer_Address_Billing.mode = "shipping";
                    // If you have already a Magento User address list then use below field "address_id".
                    //customer_Address_Billing.address_id = Session["Customer_Address_ID"].ToString();
                    customer_Address_Billing.firstname = order.Organization_Name;
                    customer_Address_Billing.lastname = order.Organization_Name;
                    customer_Address_Billing.company = order.Organization_Name;
                    customer_Address_Billing.street = order.Address1 + " " + order.Address2;
                    customer_Address_Billing.city = order.City;
                    customer_Address_Billing.country_id = "US";
                    customer_Address_Billing.region = order.State;
                    customer_Address_Billing.postcode = order.Zip;
                    customer_Address_Billing.telephone = order.Phone;
                    customer_Address_Billing.fax = "";
                    customer_Address_List.Add(customer_Address_Billing);
                    mservice.shoppingCartCustomerAddresses(mlogin, cart, customer_Address_List.ToArray(), "1");
                    //add product
                    var Products_list = new shoppingCartProductEntity()
                    {
                        qty = order.Quantity,
                        qtySpecified = true,
                        sku = order.Display_Name
                    };






                    mservice.shoppingCartProductAdd(mlogin, cart, new shoppingCartProductEntity[] { Products_list }, null);
                    //add shipping method
                    var avaiable_method = mservice.shoppingCartShippingList(mlogin, cart, "1");

                    mservice.shoppingCartShippingMethod(mlogin, cart, "storepickup_storepickup", "1");
                    //add payment method
                    var avaiable_payment = mservice.shoppingCartPaymentList(mlogin, cart, "1");
                    shoppingCartPaymentMethodEntity payment_method_entity = new shoppingCartPaymentMethodEntity();
                    // payment_method_entity.method = "custompayment";
                    payment_method_entity.method = "free";
                    payment_method_entity.cc_cid = null;
                    payment_method_entity.cc_owner = null;
                    payment_method_entity.cc_number = null;
                    payment_method_entity.cc_type = null;
                    payment_method_entity.cc_exp_month = null;
                    payment_method_entity.cc_exp_year = null;

                    mservice.shoppingCartPaymentMethod(mlogin, cart, payment_method_entity, "1");
                    //var price_total = mservice.shoppingCartTotals(mlogin, cart, "1");
                    var order_ID = mservice.shoppingCartOrder(mlogin, cart, "1", null);
                    result += "<p style='color:green'>Order " + order.Display_Name + " for " + order.Organization_Name + " Created\n</p>";



                }
                catch (Exception e)
                {
                    result += "<p style='color:red'>Order " + order.Display_Name + " for " + order.Organization_Name + "Failed\n Message : " + e.Message + "\n</p>";
                }



            }
            return result;
        }
        public bool update_price(string sku, string price)
        {
            bool success;
            try
            {
                MagentoService mservice = new MagentoService();
                String mlogin = mservice.login("admin", "Interconnection123!");
                catalogProductCreateEntity create = new catalogProductCreateEntity();
                create.price = price;
                mservice.catalogProductUpdate(mlogin, sku, create, null, null);
                success = true;
                return success;
            }

            catch
            {
                success = false;
                return success;
            }
            

        }

        //use for retail picklist
        public int retail_quick_import (Models.retail_quick_import retail)
        {

            MagentoService mservice = new MagentoService();
            String mlogin = mservice.login("admin", "Interconnection123!");

            catalogProductCreateEntity create = new catalogProductCreateEntity();
            catalogCategoryEntity add_cat = new catalogCategoryEntity();
            catalogInventoryStockItemUpdateEntity stock_data = new catalogInventoryStockItemUpdateEntity();

            stock_data.qty = "1";
            stock_data.manage_stock = 1;
            stock_data.use_config_manage_stock = 0;
            stock_data.is_in_stock = 1;
            create.description = retail.desc;
            create.name = retail.name;
            create.price = retail.price;
            create.short_description = retail.short_desc;
            create.status = retail.status;
            create.visibility = retail.visible;
            create.weight = retail.weight;
            create.website_ids = retail.webistes;
            create.tax_class_id = retail.tax_id;
            
            create.stock_data = stock_data;

            associativeEntity[] attributes = new associativeEntity[3];
            attributes[0] = new associativeEntity();
            attributes[0].key = "vendor_id";
            attributes[0].value = "11";
            attributes[1] = new associativeEntity();
            attributes[1].key = "approval";
            attributes[1].value = "2";
            attributes[2] = new associativeEntity();
            attributes[2].key = "vendor_sku";
            attributes[2].value = retail.sku + "_vendor";
            catalogProductAdditionalAttributesEntity additionalAttributes = new catalogProductAdditionalAttributesEntity();
           
            additionalAttributes.single_data = attributes;
            create.additional_attributes = additionalAttributes;
            int p_id = mservice.catalogProductCreate(
    mlogin, retail.type, retail.attr, retail.sku, create, "5");
            mservice.catalogCategoryAssignProduct(mlogin, 2, retail.sku, "0", "SKU");







            return p_id;

        }
    }
    
}