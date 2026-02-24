<%@ Page Title="" Language="C#" MasterPageFile="~/PoliceStation.Master" AutoEventWireup="true"
    CodeBehind="AddCrime.aspx.cs" Inherits="CloudBasedEncryptedCrime.AddCrime" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="forms">
        <h2 class="title1">
        </h2>
        <div class="form-grids row widget-shadow" data-example-id="basic-forms">
            <div class="form-title">
                <h4>
                    Register Crime Information:</h4>
            </div>
            <div class="form-body">
                <div class="form-group">
                    <label>
                        Enter Crime Name</label>
                    <asp:TextBox ID="txtName" runat="server" class="form-control"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ErrorMessage="Enter Name"
                        ForeColor="Red" ValidationGroup="A" ControlToValidate="txtName"></asp:RequiredFieldValidator>
                </div>
                <div class="form-group">
                    <label>
                        Enter Crime Place</label>
                    <asp:TextBox ID="txtCrimePlace" runat="server" class="form-control"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="Enter Crime Place"
                        ForeColor="Red" ValidationGroup="A" ControlToValidate="txtCrimePlace"></asp:RequiredFieldValidator>
                </div>
                <div class="form-group">
                    <label>
                        Enter Description</label>
                    <asp:TextBox ID="txtDescription" runat="server" class="form-control"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Enter Description"
                        ForeColor="Red" ValidationGroup="A" ControlToValidate="txtDescription"></asp:RequiredFieldValidator>
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
