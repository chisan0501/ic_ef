<%@ Page Title="Home Page" ClientIDMode="Static" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" EnableEventValidation="false" CodeBehind="Default.aspx.cs" Inherits="WebApplication1._Default" %>


<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
 
       <script>
         
           function myFunction() {
               var x = $("#progress_list :selected").text();
               $("#user_txt").val(x);
               var y = $("#progress_list").val();
               $("#user_name_text").val(x);
               $("#follower_text_box").val(y);
              
           }
</script>
     

    <div class="jumbotron">
        <h1>IG Search Tool</h1>
       
       
    </div>

    <div class="row">
        <div class="col-md-6">
                <div class="panel panel-default">
                <div class="panel-heading">Search</div>
                <div class="panel-body">
                User ID :
                <asp:TextBox placeholder="Enter User ID here" runat="server" ID="usernmae_txt"></asp:TextBox><br /><br />
                    User Name :
                <asp:TextBox placeholder="Enter User Name here" runat="server" ID="user_txt"></asp:TextBox><br /><br />
                   Access Token :
                    <asp:TextBox text="14004119.1fb234f.06c4339b9c774158a8d7d55209b04e3c" runat="server" ID="access_token_text" required="true" Width="413px" ></asp:TextBox> <br /><br />
                    Follower Limit : 
                    <asp:TextBox text="120000" runat="server" ID="follower_text" required="true"></asp:TextBox><br /><br />
                <asp:Button OnClick="submit" Text="Submit"  runat="server" />


                    <asp:CheckBox ID="is_save" runat="server" Checked="True" style="" Text="Mark User as Searched?" />


                </div>
</div>
       
        </div>
        <div class="col-md-6">

             <div class="panel panel-default">
                <div class="panel-heading">Search for User ID </div>
                <div class="panel-body"> <p>Enter UserName and Search for User ID</p>
                     User Name :
                <asp:TextBox placeholder="Enter User Name here" runat="server" ID="user_name_text"></asp:TextBox><br />  Followers :<asp:TextBox placeholder="# of Followers" runat="server" ID="follower_text_box"></asp:TextBox><br />Account Type :<asp:TextBox placeholder="Account Type" runat="server" ID="type_text"></asp:TextBox><br />
                      <asp:Button Text="Submit" onClick="nametoid_Click" runat="server" ID="nametoid"  />
        </div>
       </div>
    </div>
        </div>
      <div class="row">
        <div class="col-md-12">
            <h4>Fashion Account List in DB</h4>
            <asp:ListBox ID="progress_list" row="15" runat="server" Height="294px" onchange="myFunction()" Width="559px"></asp:ListBox>
<br><br>
        </div>
       </div>
 
</asp:Content>
