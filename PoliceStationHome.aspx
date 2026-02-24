<%@ Page Title="" Language="C#" MasterPageFile="~/PoliceStation.Master" AutoEventWireup="true" CodeBehind="PoliceStationHome.aspx.cs" Inherits="CloudBasedEncryptedCrime.PoliceStationHome" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div class="forms">
        <h2 class="title1">
        </h2>
        <div class="form-grids row widget-shadow" data-example-id="basic-forms">
            <div class="form-title">
                <h4>
                    Change Password:</h4>
            </div>
            <div class="form-body">
                <div class="form-group">
                    <label>
                        Old Password</label>
                    <asp:TextBox ID="txtOldPassword" class="form-control" runat="server" placeholder="Old Password"
                        TextMode="Password"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="Enter Old Password"
                        ControlToValidate="txtOldPassword" ForeColor="#FF3300" ValidationGroup="A"></asp:RequiredFieldValidator>
                </div>
                <div class="form-group">
                    <label>
                        New Password</label>
                    <asp:TextBox ID="txtNewPassword" class="form-control" runat="server" placeholder="New Password"
                        TextMode="Password"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Enter New Password"
                        ControlToValidate="txtNewPassword" ForeColor="#FF3300" ValidationGroup="A"></asp:RequiredFieldValidator>
                </div>
                <div class="form-group">
                    <label>
                        Confirm Password</label>
                    <asp:TextBox ID="txtConfirmPassword" class="form-control" runat="server" placeholder="Confirm Password"
                        TextMode="Password"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Enter Confirm Password"
                        ControlToValidate="txtConfirmPassword" ForeColor="#FF3300" ValidationGroup="A"></asp:RequiredFieldValidator>
                    <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="Password Not Match"
                        ControlToCompare="txtNewPassword" ControlToValidate="txtConfirmPassword" ForeColor="#FF3300"
                        ValidationGroup="A"></asp:CompareValidator>
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
