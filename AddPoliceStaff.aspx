<%@ Page Title="" Language="C#" MasterPageFile="~/ApplicationManager.Master" AutoEventWireup="true"
    CodeBehind="AddPoliceStaff.aspx.cs" Inherits="CloudBasedEncryptedCrime.AddPoliceStaff" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="forms">
        <h2 class="title1">
        </h2>
        <div class="form-grids row widget-shadow" data-example-id="basic-forms">
            <div class="form-title">
                <h4>
                    Add Police Staff :</h4>
            </div>
            <div class="form-body">
                <div class="form-group">
                    <label>
                        Select Role</label>
                    <asp:DropDownList ID="ddlRole" runat="server" class="form-control">
                        <asp:ListItem>--Select--</asp:ListItem>
                        <asp:ListItem>DGP</asp:ListItem>
                        <asp:ListItem>ADGP</asp:ListItem>
                        <asp:ListItem>IGP</asp:ListItem>
                        <asp:ListItem>SP</asp:ListItem>
                        <asp:ListItem>DSP</asp:ListItem>
                        <asp:ListItem>SI</asp:ListItem>
                        <asp:ListItem>ASI</asp:ListItem>
                        <asp:ListItem>Head Constable</asp:ListItem>
                        <asp:ListItem>Constable</asp:ListItem>
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Select Role"
                        InitialValue="--Select--" ForeColor="Red" ValidationGroup="A" ControlToValidate="ddlRole"></asp:RequiredFieldValidator>
                </div>
                <div class="form-group">
                    <label>
                        Enter Police Staff Name</label>
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
