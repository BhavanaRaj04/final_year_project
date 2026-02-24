<%@ Page Title="" Language="C#" MasterPageFile="~/PoliceStaff.Master" AutoEventWireup="true" CodeBehind="AddCrimeInvestigationLogs.aspx.cs" Inherits="CloudBasedEncryptedCrime.AddCrimeInvestigationLogs" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div class="forms">
        <h2 class="title1">
        </h2>
        <div class="form-grids row widget-shadow" data-example-id="basic-forms">
            <div class="form-title">
                <h4>
                    Add Crime Investigation Log:</h4>
            </div>
            <div class="form-body">
                <div class="form-group">
                    <label>
                        Select Crime Name</label>
                    <asp:DropDownList ID="ddlCrime" runat="server" class="form-control">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ErrorMessage="Select Crime"
                        ForeColor="Red" ValidationGroup="A" ControlToValidate="ddlCrime" InitialValue="--Select--"></asp:RequiredFieldValidator>
                </div>
                <div class="form-group">
                    <label>
                        Enter Description</label>
                    <asp:TextBox ID="txtDescription" runat="server" class="form-control" TextMode="MultiLine"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="Enter Description"
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
