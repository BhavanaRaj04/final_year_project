<%@ Page Title="" Language="C#" MasterPageFile="~/ApplicationManager.Master" AutoEventWireup="true" CodeBehind="MapPoliceStaff.aspx.cs" Inherits="CloudBasedEncryptedCrime.MapPoliceStaff" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div class="forms">
        <h2 class="title1">
        </h2>
        <div class="form-grids row widget-shadow" data-example-id="basic-forms">
            <div class="form-title">
                <h4>
                    Map Police Staff to Police Station :</h4>
            </div>
            <div class="form-body">
                <div class="form-group">
                    <label>
                        Select Police Staff</label>
                    <asp:DropDownList ID="ddlPoliceStaff" runat="server" class="form-control">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Select Police Staff"
                        InitialValue="--Select--" ForeColor="Red" ValidationGroup="A" ControlToValidate="ddlPoliceStaff"></asp:RequiredFieldValidator>
                </div>
                <div class="form-group">
                    <label>
                        Select Police Station</label>
                    <asp:DropDownList ID="ddlPoliceStation" runat="server" class="form-control">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Select Police Station"
                        InitialValue="--Select--" ForeColor="Red" ValidationGroup="A" ControlToValidate="ddlPoliceStation"></asp:RequiredFieldValidator>
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
