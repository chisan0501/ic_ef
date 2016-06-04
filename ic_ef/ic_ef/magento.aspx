<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="magento.aspx.cs" Inherits="ic_ef.Views.magento" ValidateRequest="false" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
     <meta charset="utf-8"/>
  <meta name="viewport" content="width=device-width, initial-scale=1"/>
  <link rel="stylesheet" href="http://maxcdn.bootstrapcdn.com/bootstrap/3.3.5/css/bootstrap.min.css"/>
  <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.11.3/jquery.min.js"></script>
  <script src="http://maxcdn.bootstrapcdn.com/bootstrap/3.3.5/js/bootstrap.min.js"></script>
<style>
    div.bottom ul{
 
    list-style-type: none;
    margin: 0;
    padding: 0;
    overflow: hidden;
    background-color: #333;
    
    top: 0;
    width: 100%;
}

div.bottom li {
    float: left;
}

div.bottom li a {
    display: block;
    color: white;
    text-align: center;
    padding: 14px 16px;
    text-decoration: none;
}

div.bottom li a:hover:not(.active) {
    background-color: #111;
}

div.bottom .active {
    background-color: #4CAF50;
}
 div.bottom       #title {
             background-color:green;
        }
 div.bottom       #header {
           
            text-align: center;
        }
div.bottom        nav {
	background-color: #fff;
	border: 1px solid #dedede;
	border-radius: 4px;
	box-shadow: 0 2px 2px -1px rgba(0, 0, 0, 0.055);
	color: #888;
	display: block;
	margin: 8px 22px 8px 22px;
	overflow: hidden;
	width: 90%; 
}
hr {
  
    height: 12px;
    border: 0;
    box-shadow: inset 0 12px 12px -12px rgba(0, 0, 0, 0.5);
}

div.img {
    margin: 5px;
    border: 1px solid #ccc;
    float: left;
    width: 200px;
}

div.img:hover {
    border: 1px solid #777;
}

div.img img {
    width: 100%;
    height: auto;
}

div.desc {
    padding: 15px;
    text-align: center;
}
</style>
  
</head>
<body>
    <form id="form1" runat="server">



<div class="row">
  <div class="col-sm-12">

      <h2>Magento Product Import Tools </h2>    
  </div>
</div>
<div class="row">
 
  <div class="col-sm-6">
      <div class="container">
  
         
  <ul class="nav nav-pills">
    <li class="active"><a data-toggle="pill" href="#home">General</a></li>

    <li><a data-toggle="pill" href="#menu1">Hardware Specs</a></li>
    <li><a data-toggle="pill" href="#menu2">Prices</a></li>
    <li><a data-toggle="pill" href="#menu3">Meta Information</a></li>
    <li><a data-toggle="pill" href="#menu4">Inventory</a></li>
    <li><a data-toggle="pill" href="#menu5">Websites</a></li>
  </ul>
  
  <div class="tab-content">
    <div id="home" class="tab-pane fade in active">
    <h3>General Info</h3>
   
   <table class="table table-striped" style="width:50%">
  <tr>
    <td>Status : <asp:DropDownList ID="status" runat="server">
        <asp:ListItem  Text="Enable" Value="Enable"></asp:ListItem>
        <asp:ListItem  selected="True" Text="Disable" Value="Disabled"></asp:ListItem>
                 </asp:DropDownList></td>
  </tr>
  <tr>
    <td>Is Computer? <asp:DropDownList ID="is_computer" runat="server">
          <asp:ListItem selected="True" Text="Yes" Value="1"></asp:ListItem>
        <asp:ListItem   Text="No" Value="0"></asp:ListItem>
                     </asp:DropDownList></td>
  </tr>
  <tr>	
    <td>Name : <asp:TextBox ID="name" runat="server" Width="280px"></asp:TextBox></td>
  </tr>
        <tr>
    <td>Brand : <asp:TextBox ID="brand" runat="server" Width="280px"></asp:TextBox></td>
  </tr>
        <tr>
    <td>Includes : <asp:TextBox ID="includes" runat="server" Width="280px"></asp:TextBox></td>
  </tr>
        <tr>
    <td>Short Description : <asp:TextBox ID="short_des" runat="server" Width="351px" Height="143px" TextMode="MultiLine"></asp:TextBox></td>
  </tr>
        <tr>
    <td>Description : <asp:TextBox ID="des" runat="server" Width="351px" Height="143px" TextMode="MultiLine"></asp:TextBox></td>
  </tr>
        <tr>
    <td>Software Description : <asp:TextBox ID="soft_des" runat="server" Width="351px" Height="143px" TextMode="MultiLine"></asp:TextBox></td>
  </tr>
      
        <tr>
    <td>SKU : <asp:TextBox ID="sku" runat="server" Width="280px"></asp:TextBox></td>
  </tr>
        <tr>
    <td>Weight : <asp:TextBox ID="weight" runat="server" Width="280px"></asp:TextBox></td>
  </tr>
</table>
   
    </div>
      
      
    <div id="menu1" class="tab-pane fade">


         <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        

          


      <h3>Hardware Specs</h3>
   
      <table class="table table-striped" style="width:50%">
  <tr>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
    <td>Asset Tag* : <asp:TextBox ID="asset_tag" runat="server" ControlToValidate="asset_tag"></asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Asset Tag is Required" ControlToValidate="asset_tag" ForeColor="Red" Display="Dynamic" ValidationGroup="AllValidators" Text=" *Required"></asp:RequiredFieldValidator>&nbsp&nbsp <asp:Button class="btn btn-primary btn-md" ID="load_info" runat="server" Text="Load Hardware Spec" onclick="load_info_Click" ValidationGroup="AllValidators" /><p><asp:Label ID="Label1" runat="server" Text="" ForeColor="Red"></asp:Label></p></td>
                </ContentTemplate>
              </asp:UpdatePanel>
  </tr>

          <tr><td>
              Computer Type : <asp:DropDownList ID="pc_type" runat="server">
                   <asp:ListItem selected="True" Text="" Value="0"></asp:ListItem>
                <asp:ListItem Text="Desktop" Value="Desktop"></asp:ListItem>
                <asp:ListItem Text="Laptop" Value="Laptop"></asp:ListItem>
                  <asp:ListItem Text="MAC" Value="MAC"></asp:ListItem>
                                 </asp:DropDownList>
              </td></tr>
          <tr><td>
              SKU Family : <asp:TextBox ID="sku_family" runat="server"></asp:TextBox> 
              </td></tr>
           <tr><td>
              Creation Date : <asp:TextBox ID="create_date" runat="server" OnLoad="create_date_Load"></asp:TextBox> 
              </td></tr>
            <tr><td>
              Software : <asp:TextBox ID="software" runat="server" Width="280px"></asp:TextBox> 
              </td></tr>
                      <tr><td>
              Video Card : <asp:TextBox ID="video_card" runat="server" Width="280px"></asp:TextBox> 
              </td></tr>
                      <tr><td>
              Screen Size : <asp:TextBox ID="screen" runat="server" Width="280px"></asp:TextBox> 
              </td></tr>
           <tr><td>
              Windows COA : <asp:TextBox ID="wcoa" runat="server" Width="280px"></asp:TextBox> 
              </td></tr>
           <tr><td>
              Office COA : <asp:TextBox ID="ocoa" runat="server" Width="280px"></asp:TextBox> 
              </td></tr>
            <tr><td>
              CPU : <asp:TextBox ID="cpu" Width="280px" runat="server"></asp:TextBox> 
              </td></tr>
            <tr><td>
              HDD : <asp:TextBox ID="hdd" Width="280px" runat="server"></asp:TextBox> 
              </td></tr>
          <tr><td>
              Optical Drive : <asp:TextBox ID="optical" Width="280px" runat="server"></asp:TextBox> 
              </td></tr>
           <tr><td>
              RAM : <asp:TextBox ID="ram" Width="280px" runat="server"></asp:TextBox> 
              </td></tr>
           <tr><td>
              Grade* : <asp:DropDownList ID="grade" runat="server">
                   <asp:ListItem Text="" Value="0"></asp:ListItem>
                   <asp:ListItem Text="Grade A" Value="A"></asp:ListItem>
                <asp:ListItem Text="Grade B" Value="B"></asp:ListItem>
                      </asp:DropDownList>
            <asp:CompareValidator
    ID="val14" runat="server" ControlToValidate="grade"
    ErrorMessage=" Required." Operator="NotEqual"
    ValueToCompare="0"
    ForeColor="Red" SetFocusOnError="true" ValidationGroup=	
"AllValidators" Text="*Required" Display="Dynamic" />
   

              </td></tr>
            <tr><td>
              Dimensions : <asp:TextBox ID="dimensions" Width="280px" runat="server"></asp:TextBox> 
              </td></tr>
                 <tr><td>
              Wireless Connectivity : <asp:DropDownList ID="wireless" runat="server">
                     <asp:ListItem selected="True" Text="" Value="0"></asp:ListItem>
                    <asp:ListItem Text="Yes" Value="yes"></asp:ListItem>
        <asp:ListItem   Text="No" Value="no"></asp:ListItem>
                                      </asp:DropDownList> 
              </td></tr>
          </table>
    </div>
    <div id="menu2" class="tab-pane fade">
      <h3>Price</h3>
      <table class="table table-striped" style="width:50%">
  <tr>
    <td>Price : <asp:TextBox ID="price_control" runat="server"></asp:TextBox></td>
  </tr>
            <tr>
    <td>Tax Class : <asp:DropDownList ID="tax_class" runat="server">
         <asp:ListItem  Text="" Value="0"></asp:ListItem>
                <asp:ListItem Text="None" Value="None"></asp:ListItem>
                <asp:ListItem selected="True" Text="Texable Goods" Value="Texable Goods"></asp:ListItem>
         <asp:ListItem Text="Shipping" Value="Shipping"></asp:ListItem>
                    </asp:DropDownList></td>
  </tr>
          </table>
    </div>
    <div id="menu3" class="tab-pane fade">
      <h3>Meta Information</h3>
            <table class="table table-striped" style="width:50%">
  <tr>
    <td>Meta Title : <asp:TextBox ID="meta_info" runat="server"></asp:TextBox></td>
  </tr>
            <tr>
    <td>Meta Description :  <asp:TextBox ID="meta_description" runat="server" Width="351px" Height="143px" TextMode="MultiLine"></asp:TextBox></td>
  </tr>
          </table>
    </div>
  <div id="menu4" class="tab-pane fade">
      <h3>Inventory</h3>
                 <table class="table table-striped" style="width:50%">   
  <tr>
    <td>Qty : <asp:TextBox ID="qty" runat="server" Text="0"></asp:TextBox></td>
  </tr>
            <tr>
    <td>Stock Availability :  <asp:DropDownList ID="stock_Availability" runat="server">
                <asp:ListItem selected="True" Text="In Stock" Value="0"></asp:ListItem>
                <asp:ListItem  Text="Out of Stock" Value="1"></asp:ListItem>
                              </asp:DropDownList></td>
  </tr>
   
          </table>
    </div>
  <div id="menu5" class="tab-pane fade">
      <h3>Product In Websites</h3>
      <asp:CheckBoxList ID="CheckBoxList1" runat="server">
           <asp:ListItem Text="Low Income Store" Value="red"></asp:ListItem>
          
                            <asp:ListItem Text="Main Website" Value="blue"></asp:ListItem>
                            <asp:ListItem Text="Retail Store" Value="green"></asp:ListItem>
      </asp:CheckBoxList>
    </div>
  </div>
</div>

  </div>
     <div class="col-sm-6">
         
        <p><asp:Button class="btn btn-primary btn-md" ID="export" runat="server" Text="Export to Magento" OnClientClick="return confirm('Are you sure you want to Import this Listing')" OnClick="export_Click" /></p> <br/>

 <h4>Latest Product SKU : </h4>   
         <br />
          <table class="table">
    <thead>
      <tr>
        <th>Product</th>
        <th>SKU</th>
      
      </tr>
    </thead>
    <tbody>
      <tr>
        <td>OEM Desktop</td>
        <td><%=oem_des_current%></td>
        
      </tr>
      <tr>
        <td>OEM Laptop</td>
        <td><%=oem_lap_current%></td>
        
      </tr>
      <tr>
        <td>MAR Desktop</td>
        <td><%=mar_des_current%></td>
       
      </tr>
             <tr>
        <td>MAR Laptop</td>
        <td><%=mar_lap_current%></td>
       
      </tr>
             <tr>
        <td>Apple Product</td>
        <td><%=apple_current%></td>
       
      </tr>
    </tbody>
  </table>
         <hr />
         <h4>Update Related/Cross-Sell/UP-Sell</h4>
         <br /><br />
         <div class="col-xs-4">
  <asp:TextBox class="form-control" ID="asset_tag_text" runat="server" placeholder="Enter Asset Tag only"></asp:TextBox><a href="#" title="Example Asset Tag :" data-toggle="popover" data-trigger="hover" data-content="12345"><span class="glyphicon glyphicon-question-sign"></span></a><asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Asset Tag is Required" ControlToValidate="related_text" ForeColor="Red" Display="Dynamic" ValidationGroup="UpdateValidators" Text=" *Required"></asp:RequiredFieldValidator>
  <script>
$(document).ready(function(){
    $('[data-toggle="popover"]').popover();   
});
</script>
</div>
    <div class="col-xs-4">
 
    <asp:TextBox class="form-control"  ID="related_text" runat="server" placeholder="Enter SKU only"></asp:TextBox><a href="#" title="Example SKU :" data-toggle="popover" data-trigger="hover" data-content="ICL12345"><span class="glyphicon glyphicon-question-sign"></span></a><asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="SKU is Required" ControlToValidate="related_text" ForeColor="Red" Display="Dynamic" ValidationGroup="UpdateValidators" Text=" *Required"></asp:RequiredFieldValidator>
</div>  
       
         
             
       <br/><br/><br/><br/><asp:Button  class="btn btn-primary btn-md" ID="related_update" runat="server" Text="Related Product Update" onclick="related_update_Click" ValidationGroup="UpdateValidators" />

     </div>
   
  </div>

<div class="row"> 
<div class="col-sm-12">
      <div class="container">
          <asp:Button class="btn btn-primary btn-md" ID="search_google" runat="server" Text="search Google Image" />
             <div id="run_behind" runat="server">





    </div>


          </div>
    </div>

</div>
<div class="row"> 
<div class="col-sm-12">
      <div class="container">
        
          <div id="google" runat="server"> </div>

          </div>
    </div>

</div>
 
    </form>
</body>
</html>