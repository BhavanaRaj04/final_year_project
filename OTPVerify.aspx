<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OTPVerify.aspx.cs" Inherits="CloudBasedEncryptedCrime.OTPVerify" %>

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
                    <h1><a class="brand-logo" href="index.html">OTP Verify form</a></h1>
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
                                Enter OTP:
                                    <asp:TextBox runat="server" placeholder="Enter OTP" ID="txtOTP"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Enter OTP" ForeColor="Red" ValidationGroup="A" ControlToValidate="txtOTP"></asp:RequiredFieldValidator>
                                </div>
                              
                               
                                <asp:Button runat="server" Text="OTP Verify" ID="btnOTPVerify" 
                                    class="btn btn-style mt-3"  ValidationGroup="A" onclick="btnOTPVerify_Click"></asp:Button>
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
