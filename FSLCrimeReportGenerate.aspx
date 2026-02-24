<%@ Page Title="" Language="C#" MasterPageFile="~/FSLStaff.Master" AutoEventWireup="true" CodeBehind="FSLCrimeReportGenerate.aspx.cs" Inherits="CloudBasedEncryptedCrime.FSLCrimeReportGenerate" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div class="forms">
        <h2 class="title1">
        </h2>
        <div class="form-grids row widget-shadow" data-example-id="basic-forms">
            <div class="form-title">
                <h4>
                    Forensic Data Log Information</h4>
            </div>
            <div class="form-body">
                <div class="form-group">
                    <label>
                        Select Police Station Name</label>
                    <asp:DropDownList ID="ddlPoliceStation" runat="server" class="form-control" AutoPostBack="True"
                        OnSelectedIndexChanged="ddlPoliceStation_SelectedIndexChanged">
                    </asp:DropDownList>
                </div>
                <div class="form-group">
                    <label>
                        Select Crime</label>
                    <asp:DropDownList ID="ddlCrime" runat="server" class="form-control">
                    </asp:DropDownList>
                </div>
                <div class="form-group">
                    <label>
                        Enter Report Details</label>
                    <asp:TextBox ID="txtDescription" runat="server" class="form-control"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Enter Report Details"
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
