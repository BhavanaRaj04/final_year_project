<%@ Page Title="" Language="C#" MasterPageFile="~/ApplicationManager.Master" AutoEventWireup="true" CodeBehind="AddPoliceStation.aspx.cs" Inherits="CloudBasedEncryptedCrime.AddPoliceStation" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div class="forms">
        <h2 class="title1">
        </h2>
        <div class="form-grids row widget-shadow" data-example-id="basic-forms">
            <div class="form-title">
                <h4>
                    Add Police Station :</h4>
            </div>
            <div class="form-body">
                <div class="form-group">
                    <label>
                        Select Area Name</label>
                    <asp:DropDownList ID="ddlArea" runat="server" class="form-control">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Select Area Name"
                        InitialValue="--Select--" ForeColor="Red" ValidationGroup="A" ControlToValidate="ddlArea"></asp:RequiredFieldValidator>
                </div>
                <div class="form-group">
                    <label>
                        Enter Police Station Name</label>
                    <asp:TextBox ID="txtName" runat="server" class="form-control"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ErrorMessage="Enter Name"
                        ForeColor="Red" ValidationGroup="A" ControlToValidate="txtName"></asp:RequiredFieldValidator>
                </div>
                <div class="form-group">
                    <label>
                        Enter Mobile No</label>
                    <asp:TextBox ID="txtMobileNo" runat="server" class="form-control"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="Enter MobileNo"
                        ForeColor="Red" ValidationGroup="A" ControlToValidate="txtMobileNo"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Only 10 Digits"
                        ValidationGroup="A" ForeColor="Red" ControlToValidate="txtMobileNo" ValidationExpression="[0-9]{10}"></asp:RegularExpressionValidator>
                </div>
                <div class="form-group">
                    <label>
                        Enter Email Id</label>
                    <asp:TextBox ID="txtEmailId" runat="server" class="form-control"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ErrorMessage="Enter Email Id"
                        ForeColor="Red" ValidationGroup="A" ControlToValidate="txtEmailId"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ControlToValidate="txtEmailId"
                        ErrorMessage="Invalid Email Id" ValidationGroup="A" ForeColor="Red" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                </div>
                <div class="form-group">
                    <label>
                        Enter Pincode</label>
                    <asp:TextBox ID="txtPincode" runat="server" class="form-control"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Enter Pincode"
                        ForeColor="Red" ValidationGroup="A" ControlToValidate="txtPincode"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ErrorMessage="Only 6 Digits"
                        ValidationGroup="A" ForeColor="Red" ControlToValidate="txtPincode" ValidationExpression="[0-9]{6}"></asp:RegularExpressionValidator>
                </div>
                <div class="form-group">
                    <label>
                        Enter Address</label>
                    <asp:TextBox ID="txtAddress" runat="server" class="form-control"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Enter Address"
                        ForeColor="Red" ValidationGroup="A" ControlToValidate="txtAddress"></asp:RequiredFieldValidator>
                </div>
                <asp:Label ID="lblMsg" runat="server" Font-Bold="True"></asp:Label>
                <div class="pull-right">
                    <asp:Button ID="btnSubmit" class="btn btn-default" runat="server" Text="Submit" OnClick="btnSubmit_Click"
                        ValidationGroup="A" />
                </div>
                <br />
            </div>
        </div>
    </div>
</asp:Content>
