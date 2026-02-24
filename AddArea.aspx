<%@ Page Title="" Language="C#" MasterPageFile="~/ApplicationManager.Master" AutoEventWireup="true" CodeBehind="AddArea.aspx.cs" Inherits="CloudBasedEncryptedCrime.AddArea" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div class="forms">
        <h2 class="title1">
        </h2>
        <div class="form-grids row widget-shadow" data-example-id="basic-forms">
            <div class="form-title">
                <h4>
                    Add Area:</h4>
            </div>
            <div class="form-body">
                <div class="form-group">
                    <label>
                        Enter Area Name</label>
                    <asp:TextBox ID="txtAreaName" runat="server" class="form-control"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Enter Area Name"
                        ControlToValidate="txtAreaName" ValidationGroup="A" ForeColor="Red"></asp:RequiredFieldValidator>
                </div>
                <asp:Label ID="lblMsg" runat="server" Font-Bold="True"></asp:Label>
                <div class="pull-right">
                    <asp:Button ID="btnSave" runat="server" Text="Save" ValidationGroup="A" class="btn btn-default"
                        Style="padding: 10px 20px;" OnClick="btnSave_Click" />
                </div>
                <br />
            </div>
        </div>
    </div>
</asp:Content>
