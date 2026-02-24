<%@ Page Title="" Language="C#" MasterPageFile="~/PoliceStaff.Master" AutoEventWireup="true" CodeBehind="PoliceStaffViewFSLReport.aspx.cs" Inherits="CloudBasedEncryptedCrime.PoliceStaffViewFSLReport" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
 <div class="forms">
        <h2 class="title1">
        </h2>
        <div class="form-grids row widget-shadow" data-example-id="basic-forms">
            <div class="form-title">
                <h4>
                    View FSL Details:</h4>
            </div>
            <div class="form-body">
                <div class="form-group">
                    <label>
                        Select Crime Name</label>
                    <asp:DropDownList ID="ddlCrime" runat="server" class="form-control" AutoPostBack="True"
                        OnSelectedIndexChanged="ddlCrime_SelectedIndexChanged">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ErrorMessage="Select Crime"
                        ForeColor="Red" ValidationGroup="A" ControlToValidate="ddlCrime" InitialValue="--Select--"></asp:RequiredFieldValidator>
                </div>
                <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                <br />
                <div class="form-grids row widget-shadow" data-example-id="basic-forms">
                    <asp:Panel ID="Panel1" runat="server">
                        <div class="form-title">
                            <h4>
                                FSL Report:</h4>
                        </div>
                        <div class="form-body">
                            <asp:Table class="table table-striped table-bordered table-hover" ID="Table1" runat="server">
                            </asp:Table>
                        </div>
                    </asp:Panel>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
