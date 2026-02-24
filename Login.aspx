<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="CloudBasedEncryptedCrime.Login" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html lang="zxx">
<head>
    <title>Cloud-Based Encrypted Crime Investigation & Evidence</title>
    <!-- Meta tag Keywords -->
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <meta charset="UTF-8" />
    <meta name="keywords" content="Flat lay login form Responsive web template, Bootstrap Web Templates, Flat Web Templates, Android Compatible web template, Smartphone Compatible web template, free webdesigns for Nokia, Samsung, LG, SonyEricsson, Motorola web design" />
    <!-- //Meta tag Keywords -->
    <%--<link href="//fonts.googleapis.com/css2?family=Nunito:wght@300;400;600&display=swap" rel="stylesheet">--%>
    <!--/Style-CSS -->
    <link rel="stylesheet" href="css/style_css.css" type="text/css" media="all" />
    <!--//Style-CSS -->
    <%--<script src="https://kit.fontawesome.com/af562a2a63.js" crossorigin="anonymous"></script>--%>
</head>
<body>
    <!-- form section start -->
    <section class="w3l-workinghny-form">
        <!-- /form -->
        <div class="workinghny-form-grid">
            <div class="wrapper">
                <div class="logo">
                    <h1><a class="brand-logo" href="index.html">login form</a></h1>
                    <!-- if logo is image enable this   
                        <a class="brand-logo" href="#index.html">
                            <img src="image-path" alt="Your logo" title="Your logo" style="height:35px;" />
                        </a> -->
                </div>
                <div class="workinghny-block-grid">
                    <div class="form-right-inf">
                       
                        <div class="login-form-content">
                            <%--<h2>Login with email</h2>--%>
                              <form id="form1" runat="server">
                               <div class="one-frm">
                                   Select User Type:<asp:DropDownList 
                          runat="server" ID="ddlUserType">
                          <asp:ListItem>--Select--</asp:ListItem>
                          <asp:ListItem>Application Manager</asp:ListItem>
                          <asp:ListItem>Police Station</asp:ListItem>
                          <asp:ListItem>FSL Staff</asp:ListItem>
                          <asp:ListItem>Police Staff</asp:ListItem>
                      </asp:DropDownList>
						 <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                          ErrorMessage="Select User Type" ForeColor="Red" ValidationGroup="A" 
                          ControlToValidate="ddlUserType" InitialValue="--Select--"></asp:RequiredFieldValidator>
                                </div>
                                <div class="one-frm">
                                Enter User Id:
                                    <asp:TextBox runat="server" placeholder="Enter User Id" ID="txtUserId"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Enter User Id" ForeColor="Red" ValidationGroup="A" ControlToValidate="txtUserId"></asp:RequiredFieldValidator>
                                </div>
                                <div class="one-frm">
                                Enter Password:
                                  <asp:TextBox runat="server" placeholder="Enter Password" ID="txtPassword" 
                            TextMode="Password"></asp:TextBox>
                             <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="Enter Password" ForeColor="Red" ValidationGroup="A" ControlToValidate="txtPassword"></asp:RequiredFieldValidator>
                                </div>
                               
                                <asp:Button runat="server" Text="Login" ID="btnLogin" class="btn btn-style mt-3"  ValidationGroup="A"
                        onclick="btnLogin_Click"></asp:Button>
                        <asp:Button runat="server" Text="Home" ID="btnHome" class="btn btn-style mt-3" onclick="btnHome_Click"></asp:Button>
                               
                               <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                            </form>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <!-- //form -->
        <!-- copyright-->
        <div class="copyright text-center">
            <div class="wrapper">
                <p class="copy-footer-29">© 2026 All rights reserved | Design by Cloud-Based Encrypted Crime Investigation & Evidence</p>
            </div>
        </div>
        <!-- //copyright-->
    </section>
    <!-- //form section start -->
</body>
</html>
